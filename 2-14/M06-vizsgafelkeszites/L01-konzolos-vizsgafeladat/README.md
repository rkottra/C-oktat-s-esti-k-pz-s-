---
id: 14-M06-L01
cim: Konzolos vizsgafeladat
evfolyam: 2-14
modul: M06 – Vizsgafelkészítés
orak: 65–68.
het:
oraszam: 4
elofeltetel: 14-M05-L03
leiras: Vizsgaszerű konzolos feladat: beolvasás fájlból, LINQ-lekérdezések, statisztikák.
---

# 14-M06-L01 · Konzolos vizsgafeladat

## Célok

A lecke végére a tanuló:

- ismeri a szoftverfejlesztő-tesztelő vizsga asztali feladatának felépítését és a konzolos rész tipikus lépéseit;
- egy feladatlap alapján önállóan megtervezi az adatosztályt és a statikus betöltő metódust;
- CSV- és JSON-állományt objektumlistába olvas, kezeli a hiányzó (null) értékeket;
- LINQ-kel megszámol, szűr, csoportosít, sorba rendez, szélsőértéket keres;
- az eredményt a minta szerinti formában írja képernyőre, illetve fájlba, hibakezeléssel;
- időre dolgozik, és a pontozási útmutató alapján ellenőrzi a saját munkáját.

## Elmélet

### A vizsga asztali feladatának szerkezete

A szoftverfejlesztő-tesztelő vizsga asztali alkalmazásos feladata jellemzően egyetlen adatforrásra (pl. pilóták, települések) épül, és két részből áll:

| Rész | Tipikus tartalom | Ebben a modulban |
|---|---|---|
| **Konzolos rész** | adatosztály, beolvasás fájlból (CSV vagy JSON), lekérdezések, statisztika, fájlba írás | *ez a lecke* |
| **Grafikus rész** | SQL-szkript importálása MySQL-be, EF Core-kapcsolat, WPF-felület táblázattal, új rekord felvétele ellenőrzéssel, törlés | 14-M06-L02, L03, L04 |

A feladatlap általános szabályai (a mi próbavizsgáinkon is ezek érvényesek):

- minden képernyőre írt részeredmény elé kerüljön ki a feladat sorszáma (`3. feladat: …`);
- a kiírás formája kövesse a mintát (szóközök, tabulátor, dátumformátum);
- az ékezet nélküli azonosítók és kiírások is elfogadottak;
- a bemeneti fájl szerkezetét nem kell ellenőrizni, de a program **tetszőleges, azonos szerkezetű** adatokkal is helyesen működjön (ne „égessünk be” eredményt!).

### Lépésről lépésre – a konzolos rész menete

1. **Projekt** (*Console App*, .NET 8) a feladatlapon megadott néven. Az adatfájlt húzd be a projektbe, és állítsd be: *Properties → Copy to Output Directory → Copy if newer*. Csak így találja meg a program relatív útvonallal (`"pilots.csv"`).
2. **Adatosztály**: egy tulajdonság oszloponként, megfelelő típussal (`int`, `double`, `DateTime?`, `string`). A hiányozható mező legyen **nullable** (`DateTime?`, `double?`).
3. **Konstruktor egy sorból** + **statikus betöltő metódus** (`BetoltCsv(string fajlnev)` → `List<T>`). A fejlécet a `Skip(1)` hagyja ki.
4. **Feladatok sorban**, mindegyik után futtatás és összevetés a mintával.
5. **Fájlba írás** `try`–`catch`-csel, `using`-gal (így a fájl biztosan lezárul).

### Beolvasás: CSV és JSON

| | CSV (`;`-vel tagolt) | JSON |
|---|---|---|
| Beolvasás | `File.ReadAllLines(f).Skip(1).Select(s => new T(s))` | `JsonSerializer.Deserialize<List<T>>(File.ReadAllText(f))` |
| Mezők | `sor.Split(';')`, majd indexelés | a kulcsnevek alapján automatikusan |
| Eltérő név | – | `[JsonPropertyName("name")] public string Nev {…}` |
| Számok, dátumok | `int.Parse`, `double.Parse(…, CultureInfo.InvariantCulture)` | automatikus (ISO dátum: `"1911-06-24"`) |
| Hiányzó érték | pl. `"null"` vagy üres mező → kézzel `null` | `null` → nullable tulajdonságba |

> **Tizedes pont!** Magyar Windows-on a `double.Parse("88.05")` hibát dob vagy rossz értéket ad, mert a magyar formátum tizedesvesszőt vár. Mindig használd: `double.Parse(szoveg, CultureInfo.InvariantCulture)` (`using System.Globalization;`).

### A leggyakoribb LINQ-minták a vizsgán

| Kérdés | Minta |
|---|---|
| Hány darab? | `lista.Count` / `lista.Count(p => feltétel)` |
| Van-e ilyen? | `lista.Any(p => feltétel)` |
| Legnagyobb / legkisebb elem | `lista.OrderBy(p => p.X).First()` vagy `lista.MaxBy(p => p.X)` |
| Összeg, átlag | `lista.Sum(p => p.X)`, `lista.Average(p => p.X)` |
| Csoportonkénti darabszám | `lista.GroupBy(p => p.Kulcs).Select(g => new { g.Key, Db = g.Count() })` |
| Első *n* csökkenő sorrendben | `.OrderByDescending(…).Take(n)` |
| Keresés, „nincs találat” ággal | `FirstOrDefault(…)` → `if (talalat is null) …` |
| Kis- és nagybetű-független egyezés | `s.StartsWith(r, StringComparison.OrdinalIgnoreCase)` |
| Lista kiírása vesszővel | `string.Join(", ", lista.Select(p => p.Nev))` |

### Kiírás és fájlírás

```csharp
Console.WriteLine($"5. feladat: Legidősebb: {p.Nev} ({p.SzuletesiDatum:yyyy.MM.dd})");
Console.WriteLine($"\t{csoport.Key,-12} {csoport.Count(),3} fő");   // igazítás: ,-12 balra, ,3 jobbra

try
{
    using StreamWriter iro = new StreamWriter("eredmeny.txt");
    iro.WriteLine("…");
    Console.WriteLine("A fájl elkészült.");
}
catch (IOException) { Console.WriteLine("Hiba a fájl írásakor!"); }
```

### Időbeosztás (90 perces konzolos részre)

| Perc | Teendő |
|---|---|
| 0–5 | feladatlap végigolvasása, a minták megnézése |
| 5–25 | projekt, adatfájl, osztály, betöltés – **futtasd le**, és írd ki a darabszámot |
| 25–75 | lekérdezések sorban; amit nem tudsz, hagyd ki, és térj vissza rá |
| 75–90 | fájlírás, kiírások összevetése a mintával, felesleges próbakód törlése |

### Tipikus hibák

- **`FileNotFoundException`**: az adatfájl nem másolódik a kimeneti mappába (*Copy to Output Directory*).
- **Rossz számok**: tizedes pont/vessző – `CultureInfo.InvariantCulture` hiányzik.
- **`FormatException` dátumnál**: a hiányzó dátum `"null"` szövegként érkezik, ezt előbb vizsgálni kell.
- **`InvalidOperationException: Sequence contains no elements`**: `First()` üres sorozaton – használj `FirstOrDefault()`-ot és kezeld a `null`-t.
- **Üres a kimeneti fájl**: nincs `using` vagy `Close()`, így a puffer nem íródik ki.
- **Beégetett eredmény**: a minta értékét kiírni számolás helyett pontlevonás – más bemenetre hibás.
- **A fejléc adatként kerül a listába**: kimaradt a `Skip(1)`.

## Példák

| Projekt | Mit mutat be |
|---|---|
| [peldak/VilagbajnokokKonzol](peldak/VilagbajnokokKonzol/) | F1-világbajnokok 2000–2024 (`vilagbajnokok.csv`): osztály + statikus `BetoltCsv`, megszámolás, `GroupBy`, bekérés „nincs találat” ággal, csoportos fájlírás `using`-gal és `try`–`catch`-csel |

Futtatás: nyisd meg a `.csproj`-t Visual Studióban, F5. A létrehozott `bajnokok_nemzetenkent.txt` a `bin/Debug/net8.0/` mappában jön létre.

## Feladatok

| Azonosító | Feladat | Nehézség | Megoldás |
|---|---|---|---|
| 14-M06-L01-F01 | [Pilóták – konzolos vizsgafeladat (90 perc, 30 pont)](feladatok/F01-pilotak-konzol/) | ★★☆ | [megoldás](megoldasok/F01-pilotak-konzol/) |
| 14-M06-L01-F02 | [Pilóták JSON-ból – kiegészítő feladat](feladatok/F02-pilotak-json/) | ★★☆ | [megoldás](megoldasok/F02-pilotak-json/) |

## Kapcsolódó

- Előző lecke: 14-M05-L03 (Egységtesztelés)
- Következő lecke: 14-M06-L02 (1. próbavizsga: CSV → MySQL → WPF)
- Tanmenet: [2-14](../../../tanmenet/2-14.md)
