# 14-M03-L04-F02 · Öreg fák vármegyénként

**Nehézség:** ★★☆ **Becsült idő:** 50 perc

## Feladat

Az `estis_oregfak` adatbázist (import: [oregfak.sql](../../../L03-database-first-scaffold/peldak/OregfakScaffold/oregfak.sql))
már scaffoldoltad a 14-M03-L03-ban. Most a `megye` 1:N `fa` kapcsolatot használd ki.

1. Új .NET 8 konzolos projekt `OregfakMegyenkent` néven; scaffoldold az adatbázist a `Models` mappába
   (a lecke példájával azonos paranccsal).
2. A program írja ki:
   1. vármegyénként a fák számát, csökkenő sorrendben (egyenlőség esetén név szerint);
   2. minden vármegye legvastagabb fáját (faj, település, törzskerület), vármegye szerint ábécérendben;
   3. az országos átlagos törzskerületet, és azokat a vármegyéket, ahol az átlag ennél nagyobb;
   4. azokat a fajokat, amelyek legalább 5 különböző vármegyében előfordulnak;
   5. kérjen be egy fajnevet, és írja ki, mely vármegyékben fordul elő (ábécérendben, ismétlés nélkül).
3. Legalább egy lekérdezés használjon `Include`-ot, legalább egy pedig navigációs tulajdonságot a `Select`-ben.

## Minta kimenet

```
Fák száma vármegyénként:
  Veszprém                  30
  Somogy                    26
  Baranya                   20
  ...
A vármegyék legvastagabb fái:
  Bács-Kiskun              fűz (Gemenc, 725 cm)
  Baranya                  fekete nyár (Drávapalkonya, 870 cm)
  ...
Országos átlag: 530 cm. Ennél vastagabb átlagú vármegyék:
  Komárom-Esztergom        699 cm
  ...
Legalább 5 vármegyében:
  kocsányos tölgy      15 vármegye
  ...
Faj: szelídgesztenye
  Baranya, Somogy, Vas, Zala
```

## Tippek

- Vármegyénként számolni a legegyszerűbb a gyűjteményből: `db.Megyes.Select(m => new { m.Nev, Db = m.Fas.Count() })`.
- „A legvastagabb fa” egy **elem**, nem érték: töltsd be a vármegyéket a fáikkal (`Include`), és a memóriában
  használd a `MaxBy`-t.
- Különböző vármegyék száma egy csoportban: `g.Select(f => f.MegyeId).Distinct().Count()`.
- Ha a scaffoldnál `-NoPluralize`-t használtál, a nevek `Fa`, `Megye` (nem `Fas`, `Megyes`).

Megoldás: [../../megoldasok/F02-oregfak-megyenkent/](../../megoldasok/F02-oregfak-megyenkent/)
