---
id: 13-M04-L01
cim: Szöveges fájlok és CSV feldolgozása
evfolyam: 1-13
modul: M04 – Fájlkezelés és adatfeldolgozás
orak: 37–40.
het:
oraszam: 4
elofeltetel: 13-M03-L03
leiras: Fájl beolvasása (File.ReadAllLines, StreamReader), sorok feldarabolása, objektumlista építése, fájlba írás.
---

# 13-M04-L01 · Szöveges fájlok és CSV feldolgozása

## Célok

A lecke végére a tanuló:

- szöveges fájlt olvas be soronként (`File.ReadAllLines`, `StreamReader`), és fájlba ír (`File.WriteAllLines`, `StreamWriter`);
- tudja, hol keresi a program a fájlt, és beállítja az adatfájl másolását a futtatási könyvtárba;
- egy CSV-sort `Split`-tel mezőkre bont, és a mezőket a megfelelő típusra alakítja;
- a CSV-fájl minden sorából objektumot készít, és ezeket `List<T>`-ben tárolja (**CSV-sor → objektum → lista**);
- a listán ciklussal alkalmazza a programozási tételeket (összegzés, megszámlálás, maximumkiválasztás, kiválogatás, keresés);
- az eredményt új szöveges fájlba menti.

Pythonban már olvastatok fájlt (`open`, `with`, `split`); itt ugyanezt csináljuk C#-ban, és az M03-ban tanult **osztályokba** töltjük az adatokat. Ez a minta a modul minden további leckéjében (és a 2/14. évfolyamon is) visszatér.

## Elmélet

### Python és C# – ugyanaz más szavakkal

| Python | C# |
|---|---|
| `with open("adat.txt", encoding="utf-8") as f:` | `using (StreamReader sr = new StreamReader("adat.txt"))` |
| `for sor in f:` | `while (!sr.EndOfStream) { string sor = sr.ReadLine()!; … }` |
| `sorok = f.read().splitlines()` | `string[] sorok = File.ReadAllLines("adat.txt");` |
| `sor.strip().split(";")` | `sor.Trim().Split(';')` |
| `int(x)`, `float(x)` | `int.Parse(x)`, `double.Parse(x, CultureInfo.InvariantCulture)` |
| `lista.append(o)` | `lista.Add(o);` |
| `f.write(szoveg + "\n")` | `sw.WriteLine(szoveg);` |

### A `File` osztály – egy hívás, egy egész fájl

A `System.IO` névtér osztályai a .NET 8-as projektben `using` nélkül is elérhetők (*implicit usings*).

| Metódus | Mit csinál |
|---|---|
| `File.ReadAllLines(utvonal)` | az összes sort `string[]` tömbként adja vissza |
| `File.ReadAllText(utvonal)` | a teljes tartalom egyetlen `string`-ként |
| `File.WriteAllLines(utvonal, sorok)` | sorokat ír ki (a meglévő fájlt **felülírja**) |
| `File.WriteAllText(utvonal, szoveg)` | szöveget ír ki (felülír) |
| `File.AppendAllText(utvonal, szoveg)` | a fájl végéhez fűz (ha nincs fájl, létrehozza) |
| `File.Exists(utvonal)` | `true`, ha a fájl létezik |

Kis és közepes fájlokhoz (néhány ezer sor) a `ReadAllLines` a legkényelmesebb: utána a sorok egy tömbben vannak, és indexelhetők.

### `StreamReader` és `StreamWriter` – soronként

A fájlkezelés klasszikus lépései: **megnyitás → olvasás/írás → bezárás**. Ha a bezárás elmarad, az írott adat egy része a memóriában ragadhat, a fájl pedig zárolva maradhat. A `using` blokk a végén automatikusan bezárja a fájlt, akkor is, ha közben hiba történik.

```csharp
// olvasás soronként
using (StreamReader sr = new StreamReader("nevek.txt"))
{
    string fejlec = sr.ReadLine()!;      // az első sort külön is beolvashatjuk
    while (!sr.EndOfStream)
    {
        string sor = sr.ReadLine()!;
        Console.WriteLine(sor);
    }
}

// írás soronként (false = felülírás, true = hozzáfűzés)
using (StreamWriter sw = new StreamWriter("eredmeny.txt", false))
{
    sw.WriteLine("Első sor");
    sw.Write("Ez nem tör sort, ");
    sw.WriteLine("ez igen.");
}
```

A `Write`/`WriteLine` pontosan úgy működik, mint a `Console`-é, így a megszokott `$"…"` szövegformázás is használható. A `ReadLine()` után álló `!` azt jelzi a fordítónak, hogy itt biztosan nem kapunk `null`-t (a `while` feltétele ezt garantálja).

### Hol keresi a program a fájlt?

A relatív útvonal (`"europa.csv"`) a **futtatási könyvtárhoz** képest értendő. Visual Studióból indítva ez a `bin\Debug\net8.0` mappa, *nem* a projekt mappája! Ezért az adatfájlt hozzá kell adni a projekthez, és be kell állítani: *Solution Explorerben a fájlon jobb klikk → Properties → Copy to Output Directory → Copy if newer*. A `.csproj`-ban ez így néz ki:

```xml
<ItemGroup>
  <None Update="europa.csv">
    <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
  </None>
</ItemGroup>
```

Abszolút útvonalat (`@"C:\adatok\europa.csv"`) is megadhatunk – a `@` miatt a `\` nem vezérlőkarakter –, de így a program csak azon a gépen működik.

A fájlba írt eredmény is a futtatási könyvtárba kerül. Hogy hol van ez, kiírathatjuk: `Console.WriteLine(Path.GetFullPath("eredmeny.txt"));`.

### A CSV formátum

A **CSV** (*comma-separated values*) olyan szöveges fájl, amelyben **egy sor = egy rekord**, a mezőket elválasztó karakter tagolja. A magyar Excel pontosvesszőt (`;`) használ, a nemzetközi fájlok vesszőt (`,`). Az első sor gyakran **fejléc** (az oszlopok nevei) – ezt nem kell feldolgozni.

```
Nev;Fovaros;Terulet;Nepesseg;Gdp
Albánia;Tirana;28748;3600000;4900
Vatikán;Vatikán;0.44;900;0
```

Egy sor feldolgozása:

```csharp
string sor = "Albánia;Tirana;28748;3600000;4900";
string[] mezok = sor.Split(';');          // ["Albánia", "Tirana", "28748", "3600000", "4900"]
string nev = mezok[0];
int nepesseg = int.Parse(mezok[3]);
double terulet = double.Parse(mezok[2], CultureInfo.InvariantCulture);
```

**Tizedesjel:** a magyar Windows tizedes*vesszőt* vár, a fájlokban viszont többnyire tizedes*pont* áll. A `double.Parse("0.44")` magyar gépen hibát dob vagy rossz értéket ad, ezért valós számnál adjuk meg a kultúrafüggetlen formátumot: `CultureInfo.InvariantCulture` (`using System.Globalization;`). A típusátalakítást a 13-M04-L03 lecke részletesen tárgyalja.

### CSV-sor → objektum → lista

A mezőket ne tömbként hurcoljuk a programban: készítsünk **osztályt**, amelynek egy-egy tulajdonsága egy-egy oszlop. A sort a **konstruktor** dolgozza fel.

```csharp
class Orszag
{
    public string Nev { get; set; }
    public string Fovaros { get; set; }
    public double Terulet { get; set; }
    public int Nepesseg { get; set; }

    public Orszag(string sor)                       // egy CSV-sorból készül az objektum
    {
        string[] m = sor.Split(';');
        Nev = m[0];
        Fovaros = m[1];
        Terulet = double.Parse(m[2], CultureInfo.InvariantCulture);
        Nepesseg = int.Parse(m[3]);
    }

    public double Nepsuruseg => Nepesseg / Terulet;  // számított tulajdonság
}
```

A beolvasás a főprogramban – a fejlécet kihagyjuk, ezért a ciklus az **1-es** indextől indul:

```csharp
List<Orszag> orszagok = new List<Orszag>();
string[] sorok = File.ReadAllLines("europa.csv");
for (int i = 1; i < sorok.Length; i++)
{
    orszagok.Add(new Orszag(sorok[i]));
}
Console.WriteLine($"Beolvasva: {orszagok.Count} ország");
```

Ugyanez `foreach`-csel: `foreach (string sor in File.ReadAllLines("europa.csv").Skip(1))`.

### Feldolgozás programozási tételekkel

A lista elemein ugyanazok a tételek működnek, mint az M02-ben a tömbökön – csak az elem most egy objektum, és a tulajdonságát vizsgáljuk.

| Tétel | Kérdés | Váz |
|---|---|---|
| összegzés | Mennyi az összlakosság? | `osszeg += o.Nepesseg;` |
| megszámlálás | Hány ország kisebb 10 000 km²-nél? | `if (o.Terulet < 10000) db++;` |
| maximumkiválasztás | Melyik a legnépesebb? | `if (o.Nepesseg > max.Nepesseg) max = o;` |
| kiválogatás | Az S betűs országok listája | `if (o.Nev.StartsWith("S")) sLista.Add(o);` |
| keresés / eldöntés | Van-e Bern fővárosú ország? | `while (i < n && orszagok[i].Fovaros != "Bern") i++;` |

```csharp
Orszag legnepesebb = orszagok[0];             // a maximumot az első elemmel indítjuk
foreach (Orszag o in orszagok)
{
    if (o.Nepesseg > legnepesebb.Nepesseg)
        legnepesebb = o;
}
Console.WriteLine($"Legnépesebb: {legnepesebb.Nev}");
```

### Eredmény fájlba írása

Az eredménysorokat gyűjtsük egy `List<string>`-be, majd írjuk ki egyben – vagy írjunk soronként `StreamWriter`-rel:

```csharp
List<string> kimenet = new List<string>();
foreach (Orszag o in orszagok)
{
    if (o.Nev.StartsWith("S"))
        kimenet.Add($"{o.Nev};{o.Terulet}");
}
File.WriteAllLines("s_betus.txt", kimenet);
```

> **Kitekintés – LINQ.** Ugyanezek a kérdések egy-egy sorban is megválaszolhatók: `orszagok.Sum(o => o.Nepesseg)`, `orszagok.Count(o => o.Terulet < 10000)`, `orszagok.MaxBy(o => o.Nepesseg)`. Ezt a 2/14. évfolyamon (14-M01-L02) tanuljuk részletesen; most a ciklusos megoldást gyakoroljuk, mert az mutatja meg, *mi történik* a háttérben.

### Tipikus hibák

- **`FileNotFoundException`:** a fájl nincs a `bin\Debug\net8.0` mappában – állítsd be a *Copy to Output Directory* tulajdonságot, vagy ellenőrizd a fájlnevet (kis- és nagybetű, kiterjesztés: a Windows elrejtheti a `.txt` végződést!).
- **`FormatException` az első sornál:** a fejlécsort is számmá akarja alakítani – a ciklus `i = 1`-től induljon.
- **`FormatException` valós számnál:** tizedespont és tizedesvessző keveredése – használd a `CultureInfo.InvariantCulture`-t.
- **`IndexOutOfRangeException`:** rossz elválasztó (`','` helyett `';'`), vagy üres sor a fájl végén. Üres sort így hagyhatsz ki: `if (sor.Trim() == "") continue;`.
- **Üres a kiírt fájl:** a `StreamWriter`-t nem zártad be (hiányzik a `using` vagy a `Close()`).
- **Olvashatatlan ékezetek:** a fájl nem UTF-8 kódolású (pl. régi Excel-mentés). Mentsd újra UTF-8-ként, vagy add meg a kódolást: `File.ReadAllLines(f, Encoding.Latin1)`.

## Példák

| Projekt | Mit mutat be |
|---|---|
| [peldak/FajlOlvasasIras](peldak/FajlOlvasasIras/) | `File.Exists`, `ReadAllLines`, `StreamReader` soronként, `WriteAllLines`, `StreamWriter` (`using`), `AppendAllText` naplózás; a futtatási könyvtár kiírása |
| [peldak/EuropaCsv](peldak/EuropaCsv/) | `europa.csv` → `Orszag` objektumok `List<Orszag>`-ban; összegzés, megszámlálás, maximumkiválasztás, kiválogatás, keresés ciklussal; eredmény mentése `s_betus.txt`-be |

Futtatás: nyisd meg a `.csproj` fájlt Visual Studióban, majd F5. A kiírt fájlok a `bin\Debug\net8.0` mappában jönnek létre.

## Feladatok

| Azonosító | Feladat | Nehézség | Megoldás |
|---|---|---|---|
| 13-M04-L01-F01 | [Számok statisztikája fájlból](feladatok/F01-szamok-statisztika/) | ★☆☆ | [megoldás](megoldasok/F01-szamok-statisztika/) |
| 13-M04-L01-F02 | [Raktárkészlet CSV-ből](feladatok/F02-raktarkeszlet/) | ★★☆ | [megoldás](megoldasok/F02-raktarkeszlet/) |
| 13-M04-L01-F03 | [Mátrix fájlba írása és visszaolvasása](feladatok/F03-matrix-fajlban/) | ★★★ | [megoldás](megoldasok/F03-matrix-fajlban/) |

## Kapcsolódó

- Előző lecke: 13-M03-L03 (Statikus tagok, absztrakt osztályok, interfészek)
- Következő lecke: 13-M04-L02 (Menüs konzolos alkalmazás)
- Magasabb szinten (LINQ-kel): 14-M01-L02 (Fájlkezelés és LINQ)
- Tanmenet: [1-13](../../../tanmenet/1-13-2felev.md)
