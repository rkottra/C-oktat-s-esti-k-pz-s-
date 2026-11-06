---
id: 13-M02-L04
cim: Metódusok, struktúrák, listák
evfolyam: 1-13
modul: M02 – Adatszerkezetek és algoritmusok
orak: 21–24.
het:
oraszam: 4
elofeltetel: 13-M02-L03
leiras: Saját metódusok (paraméter, visszatérési érték), névterek, a program szerkezete, struct és enum, a `List<T>` használata.
---

# 13-M02-L04 · Metódusok, struktúrák, listák

## Célok

A lecke végére a tanuló:

- saját metódust ír paraméterekkel és visszatérési értékkel, és a programot kisebb, újrahasznosítható részekre bontja;
- érti az érték szerinti és a `ref` / `out` paraméterátadás különbségét, és használ alapértelmezett paramétert és túlterhelést;
- ismeri a C# program szerkezetét (`using`, `namespace`, `class Program`, `Main`) és a felső szintű utasítások (top-level statements) viszonyát;
- `enum` típussal rögzített értékkészletet, `struct` típussal összetartozó adatokat ír le;
- a `List<T>` alapműveleteit (`Add`, `Insert`, `Remove`, `RemoveAt`, `Contains`, `IndexOf`, `Count`, `Sort`) használja, és a programozási tételeket listán is megírja.

## Elmélet

### Saját metódusok

A Pythonban megismert `def` függvény C#-os megfelelője a **metódus**. A különbség az, hogy C#-ban a paraméterek és a visszatérési érték **típusát is meg kell adni**.

```python
def terulet(a, b):
    return a * b
```

```csharp
static double Terulet(double a, double b)
{
    return a * b;
}
```

| Rész | Jelentése |
|---|---|
| `static` | a metódus az osztályhoz tartozik, nem kell hozzá objektum (az OOP-modulban lesz róla szó részletesen) |
| `double` | a visszatérési érték típusa; ha nincs ilyen, `void` |
| `Terulet` | a név, C#-ban **nagy kezdőbetűvel** (PascalCase) |
| `(double a, double b)` | a paraméterek típussal |
| `return` | visszaadja az értéket, és kilép a metódusból |

Egysoros metódus rövidebben: `static double Terulet(double a, double b) => a * b;`

A metódusokat a program végén, a felső szintű utasítások **után** is megírhatjuk (lokális függvények); a fájl elejéről is meghívhatók.

### Paraméterátadás

- **Érték szerint** (alapeset): a metódus a paraméter **másolatát** kapja; ha megváltoztatja, a hívó változója nem változik.
- **`ref`**: a metódus a hívó változóját kapja meg, és módosíthatja. Hívásnál is ki kell írni: `Csere(ref x, ref y);`
- **`out`**: a metódus ebbe a változóba ír eredményt (ilyen az `int.TryParse(s, out int szam)` is). Így egy metódus több értéket is „visszaadhat”.
- **Tömb és lista** paraméter: ezek referenciatípusok, ezért a metódus a hívó tömbjének **elemeit** módosíthatja `ref` nélkül is (ezért működött az `Array.Sort(t)`).

```csharp
static void MinMax(int[] t, out int min, out int max)
{
    min = t[0]; max = t[0];
    foreach (int x in t)
    {
        if (x < min) min = x;
        if (x > max) max = x;
    }
}

MinMax(szamok, out int legkisebb, out int legnagyobb);
```

**Alapértelmezett paraméter:** `static void Vonal(int hossz = 20, char jel = '-')` – hívható `Vonal()`, `Vonal(30)`, `Vonal(30, '=')` alakban is.

**Túlterhelés (overloading):** ugyanaz a név, eltérő paraméterlista. A fordító a hívás argumentumai alapján választ:

```csharp
static int Osszeg(int[] t) { ... }
static double Osszeg(double[] t) { ... }
```

### A program szerkezete és a névterek

Eddig **felső szintű utasításokkal** (top-level statements) írtunk programot: a `Program.cs` első sorától futó kód. Ezt a fordító valójában az alábbi, „klasszikus” szerkezetbe csomagolja – régebbi tankönyvekben, vizsgafeladatokban és a Visual Studio egyes sablonjaiban ezzel találkozunk:

```csharp
using System;                      // névterek importálása (Python: import)

namespace Geometria                // a program saját névtere
{
    internal class Program         // minden kód osztályban van
    {
        static void Main(string[] args)   // a program belépési pontja
        {
            Console.WriteLine(Terulet(3, 4));
        }

        static double Terulet(double a, double b) => a * b;
    }
}
```

- A **névtér** (`namespace`) a típusokat csoportosítja, mint a mappák a fájlokat; így két különböző könyvtárban lehet ugyanolyan nevű osztály. A `Console` teljes neve `System.Console`, a `List<T>`-é `System.Collections.Generic.List<T>`.
- A `using` utasítás a névtér nevének kiírását spórolja meg. A projektben bekapcsolt `ImplicitUsings` a leggyakoribbakat (`System`, `System.IO`, `System.Linq`, `System.Collections.Generic` …) automatikusan importálja.
- Egyszerűbb, fájl szintű névtér: `namespace Geometria;` – ilyenkor nem kell kapcsos zárójel.
- A `Main` metódus `args` paramétere a parancssori argumentumokat tartalmazza.

### Felsorolás: enum

Ha egy változó csak néhány előre rögzített értéket vehet fel (napok, évszakok, menüpontok, szakok), az `enum` olvashatóbb és biztonságosabb, mint a „bűvös számok” vagy a szövegek.

```csharp
enum Szak { Szoftverfejleszto, Rendszergazda, Grafikus }

Szak sz = Szak.Rendszergazda;
Console.WriteLine(sz);              // Rendszergazda – a neve íródik ki
Console.WriteLine((int)sz);         // 1 – belül egész szám, 0-tól
Szak masik = (Szak)2;               // Grafikus
bool ok = Enum.TryParse("Grafikus", out Szak beolvasott);
```

Az `enum` remekül illik a `switch`-hez, és a fordító szól, ha elírjuk az értéket (egy `"rendszergazda"` szövegnél ezt nem tenné).

### Összetett adat: struct

A párhuzamos tömbök (`nevek[i]`, `pontok[i]`, `szakok[i]`) könnyen elcsúsznak egymáshoz képest. Az összetartozó adatokat egy **struktúrába** foglaljuk:

```csharp
struct Diak
{
    public string Nev;
    public int Pont;
    public Szak Szak;

    public Diak(string nev, int pont, Szak szak)   // konstruktor
    {
        Nev = nev; Pont = pont; Szak = szak;
    }

    public override string ToString() => $"{Nev} ({Szak}, {Pont} pont)";
}

Diak d = new Diak("Kiss Anna", 87, Szak.Grafikus);
Console.WriteLine(d.Nev);   // mezőelérés ponttal
Console.WriteLine(d);       // a ToString() hívódik meg
```

A `struct` **értéktípus**: értékadáskor és paraméterátadáskor **lemásolódik**. A típusdefiníciók (`struct`, `enum`) felső szintű utasításos programban a fájl **végére** kerülnek. A `struct` a következő modulban tárgyalt `class` egyszerűbb rokona – a különbségről ott lesz szó részletesen.

### Lista: List<T>

A `List<T>` a „változó méretű tömb” – ez áll a legközelebb a Python-listához. A `T` helyére az elemek típusa kerül: `List<int>`, `List<string>`, `List<Diak>`.

| Művelet | C# | Python |
|---|---|---|
| létrehozás | `var l = new List<int>();` vagy `List<int> l = [];` | `l = []` |
| hozzáfűzés a végére | `l.Add(5);` | `l.append(5)` |
| beszúrás | `l.Insert(0, 5);` | `l.insert(0, 5)` |
| törlés érték szerint | `l.Remove(5);` (az első előfordulást) | `l.remove(5)` |
| törlés index szerint | `l.RemoveAt(0);` | `del l[0]` |
| elemszám | `l.Count` | `len(l)` |
| benne van-e? | `l.Contains(5)` | `5 in l` |
| hol van? | `l.IndexOf(5)` (ha nincs: `-1`) | `l.index(5)` |
| rendezés | `l.Sort();` | `l.sort()` |
| ürítés | `l.Clear();` | `l.clear()` |

A lista elemei indexelhetők (`l[0]`), és bejárhatók `for`- és `foreach`-ciklussal is. A programozási tételek ugyanúgy működnek, csak `Length` helyett `Count` kell. Kiválogatásnál a lista különösen kényelmes: nem kell előre méretezni, és nem kell `Array.Resize`.

```csharp
List<Diak> diakok = new List<Diak>();
diakok.Add(new Diak("Kiss Anna", 87, Szak.Grafikus));

List<Diak> grafikusok = new List<Diak>();
foreach (Diak d in diakok)
    if (d.Szak == Szak.Grafikus)
        grafikusok.Add(d);
```

Rendezés mező szerint: `diakok.Sort((a, b) => a.Pont.CompareTo(b.Pont));` – a lambda megmondja, hogyan kell két elemet összehasonlítani.

### Tipikus hibák

- **Hiányzó `return`**: ha a metódus nem `void`, minden ágon vissza kell adnia értéket – különben fordítási hiba („not all code paths return a value”).
- **A visszaadott érték eldobása**: `Negyzet(5);` önmagában semmit sem csinál; az eredményt el kell tárolni vagy ki kell írni.
- **`ref` / `out` hiánya a hívásnál**: a hívásnál is ki kell írni a kulcsszót.
- **Struct elemének módosítása listában**: a `diakok[0].Pont = 90;` fordítási hiba, mert az indexelés **másolatot** ad vissza. Helyesen: `Diak d = diakok[0]; d.Pont = 90; diakok[0] = d;`
- **Lista módosítása `foreach` közben**: `foreach (var x in l) l.Remove(x);` kivételt dob. Törléshez visszafelé haladó `for` ciklus kell, vagy `l.RemoveAll(x => …)`.
- **Típusdefiníció a felső szintű utasítások előtt**: a `struct` / `enum` csak a fájl végére (vagy külön fájlba) kerülhet.

### Dolgozat: alapalgoritmusok

A modul végén (24. óra) dolgozat: egy szöveges feladatleírásból **tömb vagy lista** adatain kell programozási tételeket saját kóddal megírni (összegzés, megszámlálás, eldöntés, keresés, maximumkiválasztás, kiválogatás, egy egyszerű rendezés), lehetőleg külön metódusokba szervezve. Gyakorlásra a 13-M02-L03 és ennek a leckének a feladatai szolgálnak.

## Példák

| Projekt | Mit mutat be |
|---|---|
| [peldak/Metodusok](peldak/Metodusok/) | klasszikus programszerkezet (`namespace`, `class Program`, `Main`), metódus visszatérési értékkel és `void`, érték / `ref` / `out` paraméter, alapértelmezett paraméter, túlterhelés, ellenőrzött beolvasás metódusba szervezve |
| [peldak/StructEnumLista](peldak/StructEnumLista/) | `enum` és `struct` definiálása, `List<T>` alapműveletei, programozási tételek listán, kiválogatás új listába, rendezés mező szerint, struct-elem módosítása listában |

## Feladatok

| Azonosító | Feladat | Nehézség | Megoldás |
|---|---|---|---|
| 13-M02-L04-F01 | [Geometriai számológép metódusokkal](feladatok/F01-geometria/) | ★☆☆ | [megoldás](megoldasok/F01-geometria/) |
| 13-M02-L04-F02 | [Bevásárlólista](feladatok/F02-bevasarlolista/) | ★★☆ | [megoldás](megoldasok/F02-bevasarlolista/) |
| 13-M02-L04-F03 | [Felvételi rangsor](feladatok/F03-felveteli-rangsor/) | ★★★ | [megoldás](megoldasok/F03-felveteli-rangsor/) |

## Kapcsolódó

- Előző lecke: 13-M02-L03 (Programozási tételek)
- Következő modul: 13-M03 (Objektumorientált programozás) – a `struct` után a `class`
- Tanmenet: [1-13](../../../tanmenet/1-13-2felev.md)
