/**
 * Esti C# oktatás – Google Classroom-szinkron (Google Apps Script).
 *
 * A GitHubról letölti a scripts/classroom/anyagok.json jegyzéket, és a megadott kurzusban
 *  - létrehozza a hiányzó témákat (modulonként egyet),
 *  - leckénként egy közzétett tananyagot hoz létre, vagy frissíti, ha a jegyzékben változott.
 *
 * A Classroom-azonosítók csak itt, a szkript tulajdonságai között tárolódnak – a repóba nem kerülnek.
 * Beállítás és használat: docs/classroom-feltoltes.md
 */

// A kurzus azonosítója: a Classroom-link /c/ utáni része (pl. "MTIzNDU2Nzg5MDEy") vagy a számos azonosító.
// CSAK az Apps Script szerkesztőben töltsd ki, a repóba visszaírni tilos!
const KURZUS = '';

const JEGYZEK_URL =
  'https://raw.githubusercontent.com/rkottra/C-oktat-s-esti-k-pz-s-/main/scripts/classroom/anyagok.json';

const IDOKORLAT_MS = 5 * 60 * 1000;   // az Apps Script 6 perc után leállítja a futást

/** Fő függvény: a kurzus szinkronizálása a jegyzékkel. Bármikor újrafuttatható. */
function szinkron() {
  const kurzusId = kurzusAzonosito_();
  const kurzus = Classroom.Courses.get(kurzusId);
  Logger.log('Kurzus: %s (%s)', kurzus.name, kurzusId);

  const valasz = UrlFetchApp.fetch(JEGYZEK_URL + '?t=' + Date.now(), { muteHttpExceptions: true });
  if (valasz.getResponseCode() !== 200) {
    throw new Error('A jegyzék nem tölthető le (' + valasz.getResponseCode() + '): ' + JEGYZEK_URL);
  }
  const anyagok = JSON.parse(valasz.getContentText('UTF-8')).anyagok;

  const props = PropertiesService.getScriptProperties();
  const temak = temakBetoltese_(kurzusId);
  const kezdes = Date.now();
  const db = { uj: 0, frissitett: 0, ujraletrehozott: 0, valtozatlan: 0 };

  // A Classroom az újabb elemet teszi felülre, ezért visszafelé haladunk:
  // így a tanmenet első modulja és leckéje kerül legfelülre.
  const sorrend = anyagok.slice().reverse();
  for (let i = 0; i < sorrend.length; i++) {
    if (Date.now() - kezdes > IDOKORLAT_MS) {
      Logger.log('Időkorlát közelében megálltam (%s / %s). Futtasd újra, onnan folytatja.', i, sorrend.length);
      break;
    }
    const a = sorrend[i];

    let temaId = temak[a.tema];
    if (!temaId) {
      temaId = Classroom.Courses.Topics.create({ name: a.tema }, kurzusId).topicId;
      temak[a.tema] = temaId;
      Logger.log('Új téma: %s', a.tema);
    }

    const szovegKivonat = kivonat_([a.cim, a.leiras, a.tema].join('\u0000'));
    const linkKivonat = kivonat_(a.linkek.join('\u0000'));
    const kulcs = 'anyag_' + kurzusId + '_' + a.id;
    const regi = JSON.parse(props.getProperty(kulcs) || 'null');

    if (regi && regi.szoveg === szovegKivonat && regi.linkek === linkKivonat) {
      db.valtozatlan++;
      continue;
    }

    let anyagId = null;
    if (regi && regi.linkek === linkKivonat) {
      // Csak a szöveg vagy a téma változott: helyben frissítjük
      try {
        Classroom.Courses.CourseWorkMaterials.patch(
          { title: a.cim, description: a.leiras, topicId: temaId },
          kurzusId, regi.anyagId, { updateMask: 'title,description,topicId' });
        anyagId = regi.anyagId;
        db.frissitett++;
      } catch (e) {
        Logger.log('%s: a frissítés nem sikerült (%s), újra létrehozom.', a.id, e.message);
      }
    } else if (regi) {
      // A mellékletek az API-n keresztül nem módosíthatók: törlés és újra létrehozás
      try {
        Classroom.Courses.CourseWorkMaterials.remove(kurzusId, regi.anyagId);
      } catch (e) {
        Logger.log('%s: a régi anyag nem törölhető (%s).', a.id, e.message);
      }
    }

    if (!anyagId) {
      anyagId = Classroom.Courses.CourseWorkMaterials.create({
        title: a.cim,
        description: a.leiras,
        topicId: temaId,
        state: 'PUBLISHED',
        materials: a.linkek.map(function (url) { return { link: { url: url } }; }),
      }, kurzusId).id;
      if (regi) db.ujraletrehozott++; else db.uj++;
      Logger.log('%s: %s', regi ? 'Újra létrehozva' : 'Létrehozva', a.cim);
    }

    props.setProperty(kulcs, JSON.stringify({ anyagId: anyagId, szoveg: szovegKivonat, linkek: linkKivonat }));
  }

  // Figyelmeztetés: korábban feltöltött, de a jegyzékből már hiányzó leckék (ezeket NEM töröljük)
  const ervenyes = {};
  anyagok.forEach(function (a) { ervenyes['anyag_' + kurzusId + '_' + a.id] = true; });
  props.getKeys().forEach(function (k) {
    if (k.indexOf('anyag_' + kurzusId + '_') === 0 && !ervenyes[k]) {
      Logger.log('Figyelem: %s már nincs a jegyzékben, a Classroomban kézzel törölhető.', k);
    }
  });

  Logger.log('Kész. Új: %s, frissített: %s, újra létrehozott: %s, változatlan: %s',
    db.uj, db.frissitett, db.ujraletrehozott, db.valtozatlan);
}

/** Segéd: kiírja a naplóba a saját (tanárként elérhető) kurzusokat az azonosítóikkal. */
function kurzusokListazasa() {
  let pageToken;
  do {
    const v = Classroom.Courses.list({ teacherId: 'me', courseStates: ['ACTIVE'], pageSize: 100, pageToken: pageToken });
    (v.courses || []).forEach(function (c) { Logger.log('%s  |  %s', c.id, c.name); });
    pageToken = v.nextPageToken;
  } while (pageToken);
}

/**
 * Segéd: elfelejti, mit töltött fel a szkript ebbe a kurzusba (a Classroomban nem töröl semmit).
 * Akkor kell, ha a feltöltött anyagokat kézzel törölted, és mindent újra fel akarsz tölteni.
 */
function allapotTorlese() {
  const kurzusId = kurzusAzonosito_();
  const props = PropertiesService.getScriptProperties();
  props.getKeys().forEach(function (k) {
    if (k.indexOf('anyag_' + kurzusId + '_') === 0) props.deleteProperty(k);
  });
  Logger.log('A(z) %s kurzus feltöltési állapota törölve.', kurzusId);
}

// ---- belső függvények ----

function kurzusAzonosito_() {
  const k = String(KURZUS).trim();
  if (!k) throw new Error('Töltsd ki a KURZUS konstanst (lásd docs/classroom-feltoltes.md)!');
  if (/^\d+$/.test(k)) return k;
  // A Classroom-linkben a számos azonosító base64-kódolva szerepel
  return Utilities.newBlob(Utilities.base64Decode(k)).getDataAsString();
}

function temakBetoltese_(kurzusId) {
  const temak = {};
  let pageToken;
  do {
    const v = Classroom.Courses.Topics.list(kurzusId, { pageSize: 100, pageToken: pageToken });
    (v.topic || []).forEach(function (t) { temak[t.name] = t.topicId; });
    pageToken = v.nextPageToken;
  } while (pageToken);
  return temak;
}

function kivonat_(szoveg) {
  return Utilities.base64Encode(
    Utilities.computeDigest(Utilities.DigestAlgorithm.SHA_256, szoveg, Utilities.Charset.UTF_8));
}
