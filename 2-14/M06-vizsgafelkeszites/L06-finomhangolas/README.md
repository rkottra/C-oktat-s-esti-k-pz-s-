---
id: 14-M06-L06
cim: Finomhangolás és rutin
evfolyam: 2-14
modul: M06 – Vizsgafelkészítés
orak: 85–88.
het:
oraszam: 4
elofeltetel: 14-M06-L05
leiras: Tipikus hibák, gyorsbillentyűk, saját kódminták és sablonok a vizsgára.
---

# 14-M06-L06 · Finomhangolás és rutin

## Célok

A lecke végére a tanuló:

- rendelkezik saját, kipróbált kódsablonokkal (CSV/JSON-beolvasó, DbContext-váz, WPF DataGrid-váz), és ezekből gyorsan indul;
- egy vizsga előtti és vizsga közbeni ellenőrzőlista alapján dolgozik;
- felismeri a leggyakoribb hibaüzeneteket, és tudja a javításukat;
- használja a Visual Studio legfontosabb gyorsbillentyűit és a hibakeresőt.

## Elmélet

### Vizsgarutin – ellenőrzőlista

**Előtte (otthon / a vizsga reggelén)**
- [ ] A XAMPP elindul, a MySQL fut; a phpMyAdmin megnyílik (`http://localhost/phpmyadmin`).
- [ ] Visual Studióban egy próba-WPF-projekt létrehozható, a NuGet-csomagok települnek.
- [ ] Ismered a saját sablonjaidat (ez a lecke `peldak/` mappája) – tudod, mit hol találsz bennük.

**A vizsga első 10 percében**
- [ ] A feladatlap végigolvasása; a pontozás szerinti fontossági sorrend megjelölése.
- [ ] SQL-szkript importálása, a sorok számának ellenőrzése.
- [ ] Projektek létrehozása **a megadott néven**; adatfájl hozzáadása, *Copy to Output Directory*.

**Munka közben**
- [ ] Minden részfeladat után fordítás és futtatás.
- [ ] Kiírás összevetése a mintával (sorszám, szóközök, tizedesjegyek, dátumformátum).
- [ ] Elakadásnál időkorlát (10 perc), utána továbblépés.

**Az utolsó 10 percben**
- [ ] Ablakcímek, feliratok, oszlopfejlécek a minta szerint.
- [ ] Próbakód, kikommentezett részek törlése; a nem forduló részek kikommentezése.
- [ ] Mindkét projekt fordul; leadás `bin`/`obj` nélkül.

### Gyakori hibák és megoldásuk

| Hibaüzenet / tünet | Ok | Megoldás |
|---|---|---|
| `FileNotFoundException` | az adatfájl nincs a `bin/Debug/net8.0` mappában | *Properties → Copy to Output Directory → Copy if newer* |
| `FormatException` számnál | tizedes pont/vessző, vagy a fejlécet is feldolgozod | `CultureInfo.InvariantCulture`; `Skip(1)` |
| `FormatException` / `null` hiányzó mezőnél | üres vagy `null` szöveg | előbb vizsgálat, nullable típus (`double?`, `DateTime?`) |
| `InvalidOperationException: Sequence contains no elements` | `First()`, `Max()`, `Average()` üres sorozaton | `FirstOrDefault()`, `MaxBy()`, előtte `Any()` |
| `NullReferenceException` | `null` objektum tulajdonságát olvasod (pl. `t.Megye.Nev` `Include` nélkül) | `Include`; `?.` és `??` operátor |
| Üres kimeneti fájl | a `StreamWriter` nincs lezárva | `using` |
| `Unable to connect to any of the specified MySQL hosts` | nem fut a MySQL | XAMPP → MySQL → Start |
| `Access denied for user 'root'` | rossz jelszó a kapcsolati szövegben | `password=` (XAMPP-ban alapból üres) |
| `Unknown database` / `Table … doesn't exist` | rossz adatbázis- vagy táblanév | `[Table("…")]`, a kapcsolati szöveg ellenőrzése |
| `Unknown column …` | a tulajdonság neve eltér az oszlopétól | `[Column("…")]` |
| A mentett rekord újraindítás után eltűnik | hiányzik a `SaveChanges()` | `db.SaveChanges();` |
| Üres a táblázat | nincs `DataContext` / `ItemsSource`, vagy elírt `Binding` | *Output* ablakban a binding-hibák; nevek ellenőrzése |
| A legördülő listában osztálynév látszik | hiányzik a `DisplayMemberPath` | `DisplayMemberPath="Nev"` |
| `The name 'File' does not exist` WPF-ben | WPF-projektben a `System.IO` nincs automatikusan importálva | `using System.IO;` |
| NuGet-verzióütközés | a csomag főverziója nem illik a .NET-hez | .NET 8 → 8.0.x csomagok |

### Visual Studio gyorsbillentyűk

| Billentyű | Funkció |
|---|---|
| **F5** / **Ctrl+F5** | futtatás hibakeresővel / nélküle |
| **Ctrl+Shift+B** | fordítás (Build Solution) |
| **F9** | töréspont be/ki |
| **F10** / **F11** / **Shift+F11** | lépés át / bele / ki a metódusból |
| **Ctrl+.** | gyorsjavítás: hiányzó `using`, metódus generálása, átnevezés |
| **Ctrl+R, R** | átnevezés mindenhol (Rename) |
| **F12** / **Shift+F12** | ugrás a definícióra / hivatkozások keresése |
| **Ctrl+K, C** / **Ctrl+K, U** | kijelölt sorok kommentezése / visszaállítása |
| **Ctrl+K, D** | dokumentum formázása (behúzások rendbetétele) |
| **Ctrl+D** | sor másolása (duplikálás) |
| **Alt+↑** / **Alt+↓** | sor mozgatása fel/le |
| **Ctrl+Space** | IntelliSense-javaslatok |
| **Ctrl+H** | keresés és csere |
| **Ctrl+Q** | keresés a menükben és beállításokban |
| `prop` + Tab Tab | tulajdonság-kódrészlet (`public int MyProperty { get; set; }`) |
| `ctor` + Tab Tab | konstruktor-kódrészlet |
| `cw` + Tab Tab | `Console.WriteLine();` |
| `foreach` / `for` / `try` + Tab Tab | ciklus- és hibakezelés-vázak |

### Saját kódsablonok

A `peldak/` mappában három **forduló, kipróbált** sablon van. Ne gépeld be fejből újra, amit itt megtalálsz – de értsd minden sorát, mert a vizsgán csak az használható, amit hoztál vagy fejből tudsz (a vizsgaszabályzat szerint).

| Sablon | Mikor? | Mit írj át? |
|---|---|---|
| `SablonCsvKonzol` | konzolos rész | `Rekord` → saját osztály, mezők és indexek, fájlnév, a fölösleges (CSV vagy JSON) betöltő törlése |
| `SablonEfKonzol` | grafikus rész eleje: kapcsolat kipróbálása | `[Table]`/`[Column]` nevek, adatbázisnév, `DbSet`-ek; a `.csproj` csomaghivatkozásai |
| `SablonWpfDataGrid` | grafikus rész: főablak + párbeszédablak + ellenőrzés | oszlopok, mezők, adatforrás (CSV → `DbContext`); az `Ellenor` osztály változtatás nélkül használható |

### Tipikus hibák a hajrában

- Az utolsó percben „gyors” javítás, amitől a projekt nem fordul – a leadás előtti utolsó lépés mindig egy sikeres fordítás.
- A mintában szereplő értékek begépelése számolás helyett (pontlevonás).
- Más néven mentett projekt, mint amit a feladatlap kér.

## Példák

| Projekt | Mit mutat be |
|---|---|
| [peldak/SablonCsvKonzol](peldak/SablonCsvKonzol/) | CSV- és JSON-betöltő nullable mezőkkel, szélsőérték/átlag üres sorozat elleni védelemmel, keresés, csoportosítás magyar ábécérenddel, fájlírás (`minta.csv`) |
| [peldak/SablonEfKonzol](peldak/SablonEfKonzol/) | entitáspár `[Table]`/`[Column]`/`[ForeignKey]` attribútumokkal, `DbContext`, `Scaffold-DbContext` parancs, kapcsolatpróba érthető hibaüzenettel, `Include`, csoportosítás |
| [peldak/SablonWpfDataGrid](peldak/SablonWpfDataGrid/) | reszponzív főablak szűrővel és darabszámmal, modális felvételi párbeszédablak, törlés megerősítéssel, újrahasznosítható `Ellenor` segédosztály |

## Feladatok

| Azonosító | Feladat | Nehézség | Megoldás |
|---|---|---|---|
| 14-M06-L06-F01 | [Sablonból 30 perc alatt](feladatok/F01-sablonbol-gyorsan/) | ★★☆ | [megoldás](megoldasok/F01-sablonbol-gyorsan/) |
| 14-M06-L06-F02 | [Hibavadászat a hibakeresővel](feladatok/F02-hibavadaszat/) | ★★☆ | [megoldás](megoldasok/F02-hibavadaszat/) |

## Kapcsolódó

- Előző lecke: 14-M06-L05 (Számonkérés és kódátnézés)
- Következő lecke: 14-M06-L07 (Év végi értékelés)
- Tanmenet: [2-14](../../../tanmenet/2-14.md)
