---
id: 14-M01-L02
cim: Fájlkezelés és LINQ
evfolyam: 2-14
modul: M01 – Konzolos alkalmazások ismétlése
orak: 5–8.
het:
oraszam: 4
elofeltetel: 14-M01-L01
leiras: CSV/TXT beolvasása objektumlistába, hibás sorok kezelése; LINQ: Where, Select, OrderBy, GroupBy, aggregálás; lambda kifejezések.
---

# 14-M01-L02 · Fájlkezelés és LINQ

## Célok

A lecke végére a tanuló:

- szöveges (CSV/TXT) fájlt olvas be és ír ki a `File` osztály metódusaival;
- a fájl sorait objektumokká alakítja (konstruktor CSV-sorból, statikus betöltő metódus), és `List<T>`-ben tárolja;
- kezeli a hibás sorokat (`try`–`catch`, illetve `TryParse`), és helyesen bánik a tizedesponttal;
- lambda kifejezést ír, és érti, hogy a LINQ-metódusok paramétere egy függvény;
- LINQ-kel szűr, rendez, kivetít, csoportosít és aggregál (`Where`, `Select`, `OrderBy`, `GroupBy`, `Count`, `Sum`, `Average`, `Min`/`Max`);
- az eredményt fájlba írja.

Ez a **CSV → objektumlista → LINQ** minta a következő modulokban is végig visszatér: a WPF-ben (14-M02) ugyanezt a listát kötjük `DataGrid`-hez, az EF Core-ban (14-M03) pedig ugyanezekkel a LINQ-metódusokkal kérdezzük le az adatbázist.

## Elmélet

### Szövegfájlok kezelése

A `System.IO` névtér (a .NET 8-as projektekben automatikusan elérhető) `File` osztálya egyetlen hívással olvas vagy ír egy teljes fájlt:

| Metódus | Mit csinál |
|---|---|
| `File.ReadAllLines(utvonal)` | az összes sort `string[]`-ként adja vissza |
| `File.ReadLines(utvonal)` | soronként olvas (nagy fájlhoz, `foreach`-csel) |
| `File.ReadAllText(utvonal)` | a teljes tartalom egyetlen `string`-ként |
| `File.WriteAllLines(utvonal, sorok)` | sorok kiírása (a meglévő fájlt felülírja) |
| `File.AppendAllLines(utvonal, sorok)` | sorok hozzáfűzése a fájl végéhez |
| `File.Exists(utvonal)` | létezik-e a fájl |

A hosszabb, folyamatos írás-olvasáshoz a `StreamReader` / `StreamWriter` való. Ezeket `using`-gal nyissuk meg, így a fájl biztosan bezáródik:

```csharp
using (var sr = new StreamReader("adatok.txt"))
{
    while (!sr.EndOfStream)
        Console.WriteLine(sr.ReadLine());
}
```

Az alapértelmezett kódolás UTF-8, ez jól kezeli az ékezeteket.

**Hol keresi a program a fájlt?** A relatív útvonal (`"kutyak.csv"`) a *futtatási könyvtárhoz* képest értendő. Ez Visual Studióból indítva a `bin\Debug\net8.0` mappa, ezért az adatfájlnál be kell állítani: *Properties → Copy to Output Directory → Copy if newer*. A `.csproj`-ban ez így látszik:

```xml
<None Update="kutyak.csv">
  <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
</None>
```

### CSV → objektumlista

A CSV-fájl egy sora = egy objektum, egy oszlopa = egy tulajdonság. A bevált szerkezet:

1. **Modellosztály** a tulajdonságokkal (az előző lecke szerint).
2. **Konstruktor CSV-sorból**: `Split(';')`, majd típusonkénti átalakítás.
3. **Statikus betöltő metódus**, amely a fájl minden sorából (a fejléc kivételével) objektumot készít.

```csharp
public Kutya(string sor)
{
    string[] adatok = sor.Split(';');
    Nev  = adatok[1];
    Kor  = int.Parse(adatok[2]);
    Suly = double.Parse(adatok[3], CultureInfo.InvariantCulture);   // tizedespont!
    Chipes = adatok[6] == "igen";                                   // szöveg → bool
}

public static List<Kutya> FajlbolBetolt(string fajlnev)
{
    var lista = new List<Kutya>();
    foreach (string sor in File.ReadAllLines(fajlnev).Skip(1))      // fejléc kihagyása
        lista.Add(new Kutya(sor));
    return lista;
}
```

Rövidebb változat `yield return`-nel – a metódus egyenként „adja ki” az elemeket, a hívó gyűjti listába:

```csharp
public static IEnumerable<Kutya> FajlbolBetolt(string fajlnev)
{
    foreach (string sor in File.ReadAllLines(fajlnev).Skip(1))
        yield return new Kutya(sor);
}
// használat: List<Kutya> kutyak = Kutya.FajlbolBetolt("kutyak.csv").ToList();
```

### Tizedesjel és kultúra

A magyar Windows **tizedesvesszőt** vár, a CSV-fájlokban viszont általában **tizedespont** van. A `double.Parse("18.5")` magyar gépen hibát dob (vagy rossz értéket ad). Megoldás: kultúrafüggetlen átalakítás.

| Feladat | Kód |
|---|---|
| beolvasás tiszta fájlból | `double.Parse(s, CultureInfo.InvariantCulture)` |
| beolvasás ellenőrzéssel | `double.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out double x)` |
| kiírás fájlba ponttal | `x.ToString(CultureInfo.InvariantCulture)` |

A `CultureInfo` és a `NumberStyles` a `System.Globalization` névtérben van. A `NumberStyles.Float` azért kell, mert enélkül a `"12,4"` ezres elválasztós számként **124** lenne – hiba helyett csendben rossz adat.

### Hibás sorok kezelése

A valódi adatfájlokban lehetnek hiányzó vagy rossz formátumú értékek. Egyetlen hibás sor miatt nem állhat le a program: a sort kihagyjuk, és jelezzük.

**1. Kivétellel:** a konstruktor kivételt dob, a betöltő sorról sorra elkapja.

```csharp
try
{
    lista.Add(new Kutya(sorok[i]));
}
catch (FormatException ex)
{
    hibak.Add($"{i + 1}. sor: {ex.Message}");
}
```

**2. `TryParse`-szal:** kivétel nélkül, a feldolgozó `null`-t ad vissza hibás sornál.

```csharp
if (a.Length != 4) return null;
if (!double.TryParse(a[2], NumberStyles.Float, CultureInfo.InvariantCulture, out double t)) return null;
```

Ellenőrizni érdemes: az oszlopok számát, az üres mezőket, a számok és dátumok formátumát, az értéktartományt (pl. negatív súly).

### Lambda kifejezések

A LINQ-metódusok paramétere egy **függvény**, amely minden elemre lefut. Ezt háromféleképpen adhatjuk át:

```csharp
static bool KanE(Kutya k) => k.Nem == "kan";
kutyak.Count(KanE);                                        // elnevezett metódus
kutyak.Count(delegate (Kutya k) { return k.Nem == "kan"; }); // névtelen metódus (régi forma)
kutyak.Count(k => k.Nem == "kan");                          // lambda kifejezés
```

A lambda alakja: `paraméter(ek) => kifejezés`. A `k` egy tetszőleges név, amely „az éppen vizsgált elemet” jelenti. Több paraméter: `(a, b) => a + b`; utasításblokk: `x => { ...; return ...; }`. Változóba is tehető: `Func<int, bool> paros = x => x % 2 == 0;`.

### LINQ-metódusok

| Csoport | Metódus | Eredmény | Példa |
|---|---|---|---|
| szűrés | `Where` | sorozat | `kutyak.Where(k => k.Kor > 10)` |
| kivetítés | `Select` | sorozat | `kutyak.Select(k => k.Nev)` |
| rendezés | `OrderBy`, `OrderByDescending`, `ThenBy` | sorozat | `.OrderByDescending(k => k.Kor).ThenBy(k => k.Nev)` |
| darabolás | `Take`, `Skip`, `Distinct` | sorozat | `.Take(3)` |
| megszámolás | `Count` | `int` | `kutyak.Count(k => k.Chipes)` |
| aggregálás | `Sum`, `Average`, `Min`, `Max` | szám | `kutyak.Average(k => k.Suly)` |
| szélsőérték-objektum | `MaxBy`, `MinBy` | egy elem | `kutyak.MaxBy(k => k.Suly)` |
| eldöntés | `Any`, `All` | `bool` | `kutyak.Any(k => k.Kor <= 1)` |
| keresés | `First`, `FirstOrDefault` | egy elem | `kutyak.FirstOrDefault(k => k.Nev == "Bodri")` |
| csoportosítás | `GroupBy` | csoportok | `kutyak.GroupBy(k => k.Fajta)` |
| átalakítás | `ToList`, `ToArray` | lista / tömb | `.ToList()` |

A metódusok **láncolhatók**, mert a legtöbb újabb sorozatot ad vissza:

```csharp
var nevek = kutyak.Where(k => !k.Oltott)
                  .OrderBy(k => k.Nev)
                  .Select(k => k.Nev)
                  .ToList();
```

Ugyanez **lekérdező szintaxissal** (az SQL-hez hasonló; a fordító a fenti metódushívásokká alakítja):

```csharp
var nevek = (from k in kutyak
             where !k.Oltott
             orderby k.Nev
             select k.Nev).ToList();
```

### Csoportosítás és névtelen típus

A `GroupBy` a kulcs szerint csoportokra bontja a listát. Minden csoportnak van `Key` tulajdonsága, és maga is egy sorozat, ezért aggregálható. A `Select`-ben **névtelen típussal** (`new { ... }`) állítjuk elő a kimutatás sorait:

```csharp
var fajtak = kutyak.GroupBy(k => k.Fajta)
                   .Select(cs => new
                   {
                       Fajta = cs.Key,
                       Darab = cs.Count(),
                       AtlagSuly = cs.Average(k => k.Suly),
                   })
                   .OrderByDescending(f => f.Darab);
```

### Késleltetett végrehajtás

A `Where`, `Select`, `OrderBy` stb. még nem futtatja le a lekérdezést, csak „receptet” ad; a végrehajtás a bejáráskor (`foreach`, `Count()`, `ToList()`) történik. Ha az eredményt többször használjuk, vagy a forráslista közben változhat, tegyük `ToList()`-be.

### Eredmény fájlba írása

```csharp
File.WriteAllLines("fajtak.txt", fajtak.Select(f => $"{f.Fajta}: {f.Darab} db"));

var sorok = kutyak.Where(k => !k.Chipes)
                  .Select(k => k.CsvSor())
                  .Prepend("id;nev;kor;suly_kg;nem;fajta;chipes;oltott");   // fejléc
File.WriteAllLines("chip_nelkul.csv", sorok);
```

### Tipikus hibák

- **`FileNotFoundException`:** az adatfájl nincs a futtatási könyvtárban – állítsd be a *Copy to Output Directory* tulajdonságot.
- **`FormatException` a számoknál:** tizedespont vagy -vessző eltérés; használj `CultureInfo.InvariantCulture`-t.
- **A fejlécsort is objektummá akarja alakítani** (`FormatException` az első sornál): hiányzik a `Skip(1)`, vagy a ciklus `i = 0`-ról indul.
- **`IndexOutOfRangeException`:** rövidebb a sor, mint várt (rossz elválasztó, hiányzó oszlop) – ellenőrizd a `Split` utáni darabszámot.
- **`InvalidOperationException: Sequence contains no elements`:** `First()`, `Average()`, `Max()` üres sorozaton. Használj `FirstOrDefault()`-ot, vagy előbb ellenőrizd `Any()`-vel.
- **`NullReferenceException` a `FirstOrDefault` után:** ha nincs találat, az eredmény `null` – vizsgáld meg, mielőtt használod.
- **Egész osztás:** `100 * resz / egesz` két `int`-tel egészre kerekít; írd `100.0 * ...` alakban.

## Példák

| Projekt | Mit mutat be |
|---|---|
| [peldak/KutyakCsvLinq](peldak/KutyakCsvLinq/) | `kutyak.csv` (2 szándékosan hibás sorral) → `List<Kutya>`; hibás sorok gyűjtése; `Count`, `Where`, lekérdező szintaxis, `Average`, `MaxBy`, `Any`/`All`, `FirstOrDefault`, `OrderByDescending`/`ThenBy`/`Take`, névtelen típus, `GroupBy`; kiírás TXT-be és CSV-be |
| [peldak/LambdaLinqAlapok](peldak/LambdaLinqAlapok/) | elnevezett metódus / `delegate` / lambda összevetése, `Func<>`, a LINQ-metódusok számlistán, láncolás, lekérdező szintaxis, késleltetett végrehajtás, `Enumerable.Range` |

Futtatás: nyisd meg a `.csproj` fájlt Visual Studióban, majd F5. A kiírt fájlok a `bin\Debug\net8.0` mappában jönnek létre (a program a végén ki is írja az útvonalat).

## Feladatok

| Azonosító | Feladat | Nehézség | Megoldás |
|---|---|---|---|
| 14-M01-L02-F01 | [Országok adatai](feladatok/F01-orszagok/) | ★★☆ | [megoldás](megoldasok/F01-orszagok/) |
| 14-M01-L02-F02 | [Időjárás-mérések hibás sorokkal](feladatok/F02-idojaras-meresek/) | ★★☆ | [megoldás](megoldasok/F02-idojaras-meresek/) |
| 14-M01-L02-F03 | [Webshop-rendelések kimutatása](feladatok/F03-webshop-rendelesek/) | ★★★ | [megoldás](megoldasok/F03-webshop-rendelesek/) |

## Kapcsolódó

- Előző lecke: 14-M01-L01 (OOP ismétlés)
- Következő lecke: 14-M02-L01 (WPF alapok: XAML, elrendezés, vezérlők)
- Tanmenet: [2-14](../../../tanmenet/2-14.md)
