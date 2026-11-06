---
id: 13-M01-L02
cim: Elágazások, ciklusok, véletlenszámok
evfolyam: 1-13
modul: M01 – C# alapok
orak: 5–8.
het:
oraszam: 4
elofeltetel: 13-M01-L01
leiras: Elágazások és ciklusok gyakorlása matematikai feladatokon, véletlenszám-generálás (Random), lottó-szimuláció.
---

# 13-M01-L02 · Elágazások, ciklusok, véletlenszámok

## Célok

A lecke végére a tanuló:

- C#-szintaxissal ír egy-, két- és többágú elágazást (`if`, `else if`, `switch`), és összeveti a Pythonból ismert formával;
- használja a `while`, a `do … while` és a `for` ciklust, valamint a `break` és `continue` utasítást;
- a `Random` osztállyal adott tartományba eső egész és valós véletlenszámot állít elő;
- ismétlés nélküli véletlen húzást (lottó) valósít meg;
- matematikai feladatokat (oszthatóság, prímek, számlálás, statisztika) old meg ciklusokkal, és ügyel a típusok értékhatáraira.

## Elmélet

### Elágazások – Pythonból C#-ra

| | Python | C# |
|---|---|---|
| Feltétel | `if x > 0:` | `if (x > 0)` – **zárójel kötelező** |
| Blokk | behúzás | `{ … }` (egyetlen utasításnál elhagyható, de inkább ne) |
| Többágú | `elif` | `else if` |
| Érték szerinti választás | `match` / `if`-lánc | `switch` |

```csharp
if (pont >= 80)
{
    Console.WriteLine("jeles");
}
else if (pont >= 60)
{
    Console.WriteLine("jó");
}
else
{
    Console.WriteLine("javítani kell");
}
```

A `switch` egy **érték** alapján választ; minden ágat `break` zár le, a `default` a „minden más” ág:

```csharp
switch (jegy)
{
    case 5: Console.WriteLine("jeles"); break;
    case 4: Console.WriteLine("jó"); break;
    case 1: case 2: case 3: Console.WriteLine("legalább elégséges vagy elégtelen"); break;
    default: Console.WriteLine("nem osztályzat"); break;
}
```

### Ciklusok – Pythonból C#-ra

| Ciklus | Python | C# |
|---|---|---|
| Elöltesztelő | `while feltetel:` | `while (feltetel) { … }` |
| Hátultesztelő | **nincs** (`while True:` + `break`) | `do { … } while (feltetel);` – legalább egyszer lefut |
| Számlálós | `for i in range(1, 11):` | `for (int i = 1; i <= 10; i++) { … }` |
| Lépésköz | `for i in range(0, 100, 5):` | `for (int i = 0; i < 100; i += 5)` |
| Visszafelé | `for i in range(10, 0, -1):` | `for (int i = 10; i > 0; i--)` |
| Gyűjtemény bejárása | `for x in lista:` | `foreach (int x in lista)` |
| Kilépés, következő kör | `break`, `continue` | `break`, `continue` |

A `for` fejléce három részből áll: **kezdőérték; feltétel; léptetés**. A ciklusváltozó (`i`) csak a ciklus belsejében létezik.

A `do … while` tipikus felhasználása a **bemenet ellenőrzése**: addig kérdezünk, amíg jó választ nem kapunk.

```csharp
int szam;
do
{
    Console.Write("Szám 10 és 20 között: ");
} while (!int.TryParse(Console.ReadLine(), out szam) || szam < 10 || szam > 20);
```

### Véletlenszámok – a `Random` osztály

```csharp
Random r = new Random();         // egyszer hozd létre, a program elején!
int kocka = r.Next(1, 7);        // 1..6 – a felső határ NINCS benne
int index = r.Next(10);          // 0..9
double x = r.NextDouble();       // 0 <= x < 1
```

| Feladat | Megoldás |
|---|---|
| egész [a; b] (mindkét végpont) | `r.Next(a, b + 1)` |
| valós [a; b) | `a + r.NextDouble() * (b - a)` |
| valós [a; b], két tizedesre | `r.Next(a * 100, b * 100 + 1) / 100.0` (pl. 1,00–100,90: `r.Next(100, 10091) / 100.0`) |
| páros szám [10; 50] | `r.Next(5, 26) * 2` |
| 5-tel osztható [100; 200] | `r.Next(20, 41) * 5` |
| igaz/hamis 50–50% | `r.Next(2) == 0` |

Python-összevetés: `random.randint(1, 6)` mindkét végpontot tartalmazza, a `random.randrange(1, 7)` nem – a C# `Next` az utóbbira hasonlít.

**Mag (seed):** `new Random(42)` minden futásnál **ugyanazt** a sorozatot adja. Hibakereséskor nagyon hasznos, mert a hiba megismételhető; kész programban paraméter nélkül hozzuk létre.

### Ismétlés nélküli húzás (lottó)

Ötöslottó: 90 számból 5 különböző. A naiv `r.Next(1, 91)` ötször hívva ismétlődhet! Két megoldás:

1. **Ellenőrzés és újrahúzás**: minden új számot összevetünk az eddigiekkel; ha már volt, újra húzunk. Kevés számnál változókkal is megoldható (`do { b = r.Next(1, 91); } while (b == a);`), de kényelmesebb egy listával – a Python listájának C#-os megfelelőjével, amelyet a 13-M02-L04 leckében részletesen tanulunk:

   ```csharp
   List<int> szamok = new List<int>();
   while (szamok.Count < 5)
   {
       int uj = r.Next(1, 91);
       if (!szamok.Contains(uj))      // Pythonban: if uj not in szamok
           szamok.Add(uj);
   }
   ```

2. **Halmaz**: a `HashSet<int>` eleve nem tárol ismétlődő elemet, így elég addig hozzáadni, amíg 5 eleme nem lesz.

### Matematikai feladatok – hasznos minták

| Kérdés | C# kifejezés |
|---|---|
| `n` páros-e? | `n % 2 == 0` |
| `a` osztható-e `b`-vel? | `a % b == 0` |
| `n` utolsó számjegye, levágása | `n % 10`, `n / 10` |
| négyzetgyök, abszolút érték, kerekítés | `Math.Sqrt(x)`, `Math.Abs(x)`, `Math.Round(x, 2)` |
| `n` prím-e? | osztót keresünk 2-től √n-ig: `for (int d = 2; d * d <= n; d++)` |
| LNKO (euklideszi algoritmus) | `while (b != 0) { int m = a % b; a = b; b = m; }` → eredmény: `a` |

**Értékhatárok.** Pythonban az egész számok tetszőlegesen nagyok lehetnek; C#-ban **nem**. Az `int` 2 147 483 647 fölött túlcsordul – figyelmeztetés nélkül negatív számba „fordul át”:

```csharp
int f = 1;
for (int i = 1; i <= 13; i++) f *= i;   // 13! = 6 227 020 800 nem fér bele → hibás eredmény
long g = 1;
for (int i = 1; i <= 20; i++) g *= i;   // long-ban 20!-ig helyes
```

Ha a túlcsordulást hibaként szeretnéd észrevenni: `checked { … }` blokk – ekkor `OverflowException` keletkezik.

### Tipikus hibák

- **`if (x > 0);`** – a pontosvessző üres utasítást zár le, a blokk feltétel nélkül lefut.
- **`for (int i = 0; i <= 10; i++)`** 11-szer fut le – a Python `range(10)` megfelelője `i < 10`.
- **`r.Next(1, 6)`** kockadobásnak: a 6 sosem jön ki, mert a felső határ kizárt.
- **`new Random()` a cikluson belül**: nagyon gyors ciklusban régebbi .NET-ben ugyanazokat a számokat adhatta; egyébként is fölösleges – egyszer, a ciklus előtt hozd létre.
- **Rossz logikai feltétel a bemenet-ellenőrzésnél**: „addig kérdezünk, amíg a válasz nem `i` és nem `n`” → `v != 'i' && v != 'n'`. A `v != 'i' || v != 'n'` **mindig igaz**, így végtelen ciklus lesz.
- **Hiányzó `break` a `switch` ágában** → fordítási hiba (*„Control cannot fall through from one case label”*).
- **Túlcsordulás**: faktoriális, hatványok, nagy szorzatok `int`-ben. Használj `long`-ot, és gondold végig, meddig elég.
- **Egész osztás százalékszámításnál**: `db / 200 * 100` szinte mindig 0 – helyesen `db * 100.0 / 200`.

## Példák

| Projekt | Mit mutat be |
|---|---|
| [peldak/VeletlenSzamok](peldak/VeletlenSzamok/) | `Random.Next` és `NextDouble`, egész és valós véletlenszám adott tartományban, páros / 5-tel osztható szám, 1000 kockadobás statisztikája (`if – else if`), pénzfeldobás karakterekkel, `do … while` |
| [peldak/Lotto](peldak/Lotto/) | Választás menüből (`switch`), ötös- és hatoslottó ismétlés nélküli húzása listával, ugyanez lista nélkül `do … while`-okkal, tippek ellenőrzése `TryParse`-szal |

Futtatás: nyisd meg a `.csproj` fájlt Visual Studióban, és indítsd el F5-tel. Próbáld ki a `new Random(2026)` sort is: többszöri futtatásra mit tapasztalsz?

## Feladatok

| Azonosító | Feladat | Nehézség | Megoldás |
|---|---|---|---|
| 13-M01-L02-F01 | [Számkitalálós játék](feladatok/F01-szamkitalalos/) | ★☆☆ | [megoldás](megoldasok/F01-szamkitalalos/) |
| 13-M01-L02-F02 | [Prímszámok](feladatok/F02-primszamok/) | ★★☆ | [megoldás](megoldasok/F02-primszamok/) |
| 13-M01-L02-F03 | [Rulett-statisztika](feladatok/F03-rulett/) | ★★★ | [megoldás](megoldasok/F03-rulett/) |

További gyakorlásra (megoldás nélkül, órai vagy otthoni feladatnak):

- Írd ki 1-től 50-ig a számokat vesszővel elválasztva (az utolsó után ne legyen vessző), a párosakat pirossal, a páratlanokat kékkel.
- Kérj be egy egész számot, és írd fel kettő hatványának és egy páratlan számnak a szorzataként (pl. `120 = 2*2*2*15`).
- Kérj be három számot, és döntsd el, lehetnek-e egy háromszög oldalai; illetve számtani sorozatot alkotnak-e.
- Írd ki a Fibonacci-sorozat első 40 elemét. (Mekkora típus kell hozzá?)
- Melyik a legnagyobb `n`, amelynek a faktoriálisát még helyesen kiszámolja a program `long` típussal?
- Keresd meg a 10 000 alatti tökéletes számokat (amelyek egyenlők az önmaguknál kisebb osztóik összegével, pl. 6 = 1 + 2 + 3).
- Kérj be egy pozitív egészet, és írd ki kettes számrendszerben (maradékos osztással).
- Szökőév-e a bekért évszám? (4-gyel osztható, de 100-zal nem – kivéve, ha 400-zal is.)

## Kapcsolódó

- Előző lecke: 13-M01-L01 (Első C# programok: típusok, operátorok, konzol)
- Következő lecke: 13-M02-L01 (Karakterláncok kezelése)
- Tanmenet: [1-13](../../../tanmenet/1-13-2felev.md)
