---
id: 14-M06-L03
cim: 2. próbavizsga: több tábla, szűrések
evfolyam: 2-14
modul: M06 – Vizsgafelkészítés
orak: 73–76.
het:
oraszam: 4
elofeltetel: 14-M06-L02
leiras: Összetettebb feladatsor kapcsolt táblákkal és szűrésekkel (Settlements).
---

# 14-M06-L03 · 2. próbavizsga: több tábla, szűrések

## Célok

A lecke végére a tanuló:

- egy teljes, konzolos és grafikus részt is tartalmazó vizsgafeladatot old meg időre;
- csoportosító lekérdezésekkel (összeg, darab, különböző értékek száma) statisztikát készít;
- két, idegen kulccsal kapcsolt táblát EF Core-ral kezel: navigációs tulajdonság, `Include`;
- legördülő listát objektumokkal tölt fel (`DisplayMemberPath`, `SelectedItem`);
- modális párbeszédablakból ellenőrzött adatot ad vissza a főablaknak (`ShowDialog`, `DialogResult`).

## Elmélet

### Két tábla, egy kapcsolat

```
county (1) ───────< settlement (N)
  id   ◄──────────── countyid
  name               name, region, population, areasize
```

| Fogalom | C#-ban | Mire jó |
|---|---|---|
| idegen kulcs | `[Column("countyid")] public int MegyeId { get; set; }` | ezt mentjük az adatbázisba |
| navigációs tulajdonság | `[ForeignKey(nameof(MegyeId))] public Megye? Megye { get; set; }` | `t.Megye.Nev` – a kapcsolt rekord adatai |
| visszairányú lista (opcionális) | `public List<Telepules> Telepulesek { get; set; }` a `Megye`-ben | `m.Telepulesek.Count` |
| betöltés | `db.Telepulesek.Include(t => t.Megye)` | `Include` nélkül a `Megye` **null** |

A táblázatban a kapcsolt adat pont-jelöléssel köthető: `Binding="{Binding Megye.Nev}"`.

### Statisztika csoportosítással (konzolos rész)

```csharp
var megyek = telepulesek
    .GroupBy(t => t.Megye)
    .Select(g => new
    {
        Megye    = g.Key,
        Lakossag = g.Sum(t => t.Nepesseg),
        Terulet  = g.Sum(t => t.Terulet),
        Jarasok  = g.Select(t => t.Jaras).Where(j => j != "").Distinct().Count()
    })
    .ToList();
var legkisebb = megyek.OrderBy(m => m.Lakossag).First();   // vagy: megyek.MinBy(m => m.Lakossag)
```

Számított tulajdonság az osztályban: `public double Nepsuruseg => Nepesseg / Terulet;` – nem a fájlból jön, mindig aktuális.

**Magyar ábécérend.** A `"Cs"`, `"Gy"` stb. kettős betűk és az ékezetes betűk helyes rendezéséhez: `StringComparer.Create(new CultureInfo("hu-HU"), true)` (az adatbázisban ezt az `utf8mb4_hungarian_ci` rendezés adja).

### Legördülő lista objektumokkal

```xml
<ComboBox x:Name="cbMegye" DisplayMemberPath="Nev" />
```

```csharp
cbMegye.ItemsSource = db.Megyek.OrderBy(m => m.Nev).ToList();
if (cbMegye.SelectedItem is not Megye megye) { /* nincs kiválasztva */ }
uj.MegyeId = megye.Id;
```

| Tulajdonság | Jelentés |
|---|---|
| `DisplayMemberPath` | az elem melyik tulajdonsága látszik a listában |
| `SelectedItem` | a kiválasztott **objektum** (`null`, ha nincs) |
| `SelectedValuePath` + `SelectedValue` | a kiválasztott objektum egy tulajdonsága (pl. `Id`) |
| `SelectedIndex = -1` | nincs kiválasztás (alapállapot) |

### Modális párbeszédablak

1. *Project → Add → Window (WPF)*: pl. `UjTelepulesAblak`.
2. A konstruktor paraméterként kapja, amire szüksége van (pl. a megyék listáját).
3. Sikeres ellenőrzés után az eredményt egy publikus tulajdonságba teszi, és `DialogResult = true;` – ez be is zárja.
4. A *Mégse* gombon `IsCancel="True"` – külön kód nélkül bezár, az eredmény `false`.
5. A hívó oldal:

```csharp
var ablak = new UjTelepulesAblak(megyek) { Owner = this };
if (ablak.ShowDialog() == true && ablak.UjTelepules is not null)
{
    db.Telepulesek.Add(ablak.UjTelepules);
    db.SaveChanges();
    Betoltes();
}
```

> A `Show()` nem modális: a kód azonnal továbbfut, és nincs visszatérési érték – vizsgán a `ShowDialog()` kell.

### Számok ellenőrzése

```csharp
if (!int.TryParse(txtNepesseg.Text, out int nepesseg) || nepesseg <= 0) { … }

string s = txtTerulet.Text.Trim().Replace(',', '.');   // vessző és pont is jó
if (!double.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out double terulet) || terulet <= 0) { … }
```

### Tipikus hibák

- **Üres a Megye oszlop**: hiányzik az `Include(t => t.Megye)`.
- **A legördülő listában `TelepulesekWpf.Models.Megye` szöveg látszik**: hiányzik a `DisplayMemberPath="Nev"`.
- **„Cannot add or update a child row: a foreign key constraint fails”**: a `MegyeId` 0 maradt (nem a kiválasztott megye azonosítóját mentetted).
- **A párbeszédablak bezárul hibás adatnál is**: a hibaüzenet után hiányzik a `return`, vagy a `DialogResult = true` az ellenőrzés előtt áll.
- **„DialogResult can be set only after Window is created and shown as dialog”**: `Show()`-val nyitottad meg `ShowDialog()` helyett.
- **Budapest „1 járással” szerepel**: az üres járásnevet is megszámoltad.

## Példák

| Projekt | Mit mutat be |
|---|---|
| [peldak/DialogusAblakMinta](peldak/DialogusAblakMinta/) | adatbázis nélküli, memóriabeli „műfaj–könyv” kapcsolat: táblázat a kapcsolt objektum nevével (`Mufaj.Nev`), modális `UjKonyvAblak` legördülő listával (`DisplayMemberPath`, `SelectedValuePath`), ellenőrzés, `DialogResult`, `IsDefault`/`IsCancel` |

## Feladatok

| Azonosító | Feladat | Nehézség | Megoldás |
|---|---|---|---|
| 14-M06-L03-F01 | [2. próbavizsga: Magyarország városai (150 perc, 60 pont)](feladatok/F01-telepulesek/) | ★★★ | [megoldás](megoldasok/F01-telepulesek/) |
| 14-M06-L03-F02 | [Megyeszűrő és összesítés](feladatok/F02-megyeszuro/) | ★★★ | [megoldás](megoldasok/F02-megyeszuro/) |

## Kapcsolódó

- Előző lecke: 14-M06-L02 (1. próbavizsga: CSV → MySQL → WPF)
- Következő lecke: 14-M06-L04 (3. próbavizsga: vizsgaszimuláció)
- Tanmenet: [2-14](../../../tanmenet/2-14.md)
