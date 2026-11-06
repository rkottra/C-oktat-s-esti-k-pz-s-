---
id: 13-M02-L03
cim: Programozási tételek
evfolyam: 1-13
modul: M02 – Adatszerkezetek és algoritmusok
orak: 17–20.
het:
oraszam: 4
elofeltetel: 13-M02-L02
leiras: Összegzés, megszámlálás, eldöntés, kiválasztás, keresés, maximumkiválasztás, kiválogatás, szétválogatás, metszet, unió, rendezés.
---

# 13-M02-L03 · Programozási tételek

## Célok

A lecke végére a tanuló:

- felismeri, hogy egy szövegesen megfogalmazott feladat melyik programozási tétellel (vagy tételek kombinációjával) oldható meg;
- fejből, „sablonként” meg tudja írni C#-ban az összegzés, megszámlálás, eldöntés, kiválasztás, keresés és maximumkiválasztás algoritmusát;
- egy tömbből kiválogat, szétválogat, két tömb metszetét és unióját előállítja;
- legalább egy egyszerű rendezést (minimumkiválasztásos vagy buborékrendezés) önállóan megír, és érti, miért lassú nagy adatmennyiségre;
- tudja, melyik tételnek melyik beépített C#-megoldás (LINQ-metódus) a megfelelője, és mikor szabad azt használni.

## Elmélet

### Mi az a programozási tétel?

A feladatok jelentős része néhány **visszatérő mintára** épül: „hány olyan van, ami…”, „van-e olyan, ami…”, „melyik a legnagyobb…”. Ezeknek a mintáknak bevált, bizonyítottan helyes megoldása van – ezeket hívjuk **programozási tételeknek**. Ha a feladatban felismerjük a tételt, a megoldás vázát már tudjuk; csak a feltételt és az adatokat kell behelyettesíteni.

Az alábbiakban mindig egy `int[] t` tömbön dolgozunk, `n = t.Length`. A „T tulajdonság” egy tetszőleges feltétel, például `t[i] % 2 == 0` (páros) vagy `t[i] > 100`.

### Egy sorozathoz egy értéket rendelő tételek

**Összegzés** – a sorozat elemeinek összege (vagy szorzata, összefűzése…).

```csharp
int osszeg = 0;
for (int i = 0; i < n; i++)
    osszeg += t[i];
```

**Megszámlálás** – hány T tulajdonságú elem van?

```csharp
int db = 0;
for (int i = 0; i < n; i++)
    if (t[i] % 2 == 0)
        db++;
```

**Eldöntés** – van-e T tulajdonságú elem? Az első találatnál megállunk, nem nézzük végig a tömböt.

```csharp
int i = 0;
while (i < n && !(t[i] > 100))
    i++;
bool van = i < n;
```

A `while` feltételében a sorrend fontos: előbb az `i < n`, mert ha az hamis, a `t[i]` már nem létezik (az `&&` rövidzáras, a második felét ekkor ki sem értékeli). A „minden elem T tulajdonságú-e?” kérdés ugyanez fordítva: van-e **nem** T tulajdonságú?

**Kiválasztás** – melyik az (első) T tulajdonságú elem, ha **biztosan tudjuk, hogy van**? Nincs szükség `i < n` ellenőrzésre.

```csharp
int i = 0;
while (!(t[i] > 100))
    i++;
int hely = i;
```

**Keresés** – az eldöntés és a kiválasztás együtt: van-e, és ha igen, hol?

```csharp
int i = 0;
while (i < n && !(t[i] > 100))
    i++;
if (i < n)
    Console.WriteLine($"Az első 100 feletti elem: {t[i]}, a(z) {i}. indexen");
else
    Console.WriteLine("Nincs 100 feletti elem.");
```

**Maximumkiválasztás** – melyik a legnagyobb elem? Az **indexét** érdemes tárolni, abból az érték is megvan.

```csharp
int maxI = 0;
for (int i = 1; i < n; i++)
    if (t[i] > t[maxI])
        maxI = i;
```

Szigorú `>` esetén az első, `>=` esetén az utolsó legnagyobbat kapjuk. Minimumhoz a relációt kell megfordítani. Ne induljunk `max = 0`-ból: ha minden elem negatív, rossz eredményt kapnánk – mindig az első elemmel kezdjünk.

### Egy sorozathoz sorozatot rendelő tételek

**Kiválogatás** – a T tulajdonságú elemek egy új tömbbe (vagy listába).

```csharp
int[] paros = new int[n];   // legrosszabb esetben mind ide kerül
int db = 0;
for (int i = 0; i < n; i++)
    if (t[i] % 2 == 0)
        paros[db++] = t[i];
Array.Resize(ref paros, db);
```

**Szétválogatás** – a T tulajdonságúak az egyik, a többiek a másik tömbbe. Ugyanaz, mint a kiválogatás, csak `else` ággal és két számlálóval.

**Metszet** – két (ismétlődés nélküli) sorozat közös elemei: az első sorozat elemei közül kiválogatjuk azokat, amelyek a másodikban is szerepelnek (kiválogatás + eldöntés egymásba ágyazva).

```csharp
int[] metszet = new int[a.Length];
int db = 0;
for (int i = 0; i < a.Length; i++)
{
    int j = 0;
    while (j < b.Length && b[j] != a[i])
        j++;
    if (j < b.Length)
        metszet[db++] = a[i];
}
Array.Resize(ref metszet, db);
```

**Unió** – az összes elem, ismétlés nélkül: az első sorozatot átmásoljuk, majd a második elemei közül hozzáírjuk azokat, amelyek az elsőben **nincsenek** benne.

### Rendezés

A rendezés a sorozat elemeit növekvő (vagy csökkenő) sorrendbe állítja **helyben**, cserékkel. Két egyszerű módszer:

**Minimumkiválasztásos rendezés** – az `i`. helyre a hátralévő rész legkisebb elemét cseréljük.

```csharp
for (int i = 0; i < n - 1; i++)
{
    int minI = i;
    for (int j = i + 1; j < n; j++)
        if (t[j] < t[minI])
            minI = j;
    (t[i], t[minI]) = (t[minI], t[i]);   // csere – Pythonban: a, b = b, a
}
```

**Buborékrendezés** – a szomszédos, rossz sorrendű párokat cseréljük; minden menet végére a legnagyobb a helyére „buborékol”.

```csharp
for (int vege = n - 1; vege > 0; vege--)
    for (int j = 0; j < vege; j++)
        if (t[j] > t[j + 1])
            (t[j], t[j + 1]) = (t[j + 1], t[j]);
```

Mindkettő nagyjából n²/2 összehasonlítást végez: 1000 elemnél ez félmillió, egymillió elemnél félbillió – ezért a valódi programokban a beépített, sokkal gyorsabb `Array.Sort` / `List.Sort` kell. A saját rendezést a gondolkodás miatt tanuljuk (és mert a dolgozatban meg kell írni).

### Tételek és beépített megfelelőik

| Tétel | Saját kód | LINQ / beépített | Python |
|---|---|---|---|
| összegzés | `for` + `+=` | `t.Sum()` | `sum(t)` |
| megszámlálás | `for` + `if` + `db++` | `t.Count(x => x % 2 == 0)` | `sum(1 for x in t if x % 2 == 0)` |
| eldöntés | `while` + `i < n` | `t.Any(x => x > 100)` | `any(x > 100 for x in t)` |
| minden elem T? | `while`, fordított feltétel | `t.All(x => x > 0)` | `all(x > 0 for x in t)` |
| keresés | `while` + index | `Array.FindIndex(t, x => x > 100)` | – |
| maximum | `for` + `maxI` | `t.Max()` | `max(t)` |
| kiválogatás | `for` + `if` + új tömb | `t.Where(x => x % 2 == 0).ToArray()` | `[x for x in t if x % 2 == 0]` |
| metszet / unió | egymásba ágyazott ciklus | `a.Intersect(b)`, `a.Union(b)` | `set(a) & set(b)`, `set(a) \| set(b)` |
| rendezés | min. kiválasztás, buborék | `Array.Sort(t)` | `t.sort()` |

Az `x => x > 100` egy **lambdakifejezés**: rövid, név nélküli függvény (Pythonban `lambda x: x > 100`). **Dolgozatban és a lecke feladataiban a tételeket saját kóddal kell megírni**; a LINQ-változattal az eredményt ellenőrizhetjük.

### Hogyan ismerjük fel a tételt?

| A feladat szövegében… | Tétel |
|---|---|
| „mennyi az összes…”, „átlagosan…” | összegzés (+ osztás) |
| „hány olyan…”, „hány napon…” | megszámlálás |
| „volt-e olyan…”, „előfordult-e…”, „minden…?” | eldöntés |
| „hányadik napon először…” (tudjuk, hogy van) | kiválasztás |
| „volt-e, és ha igen, mikor…” | keresés |
| „melyik a legnagyobb / legkisebb / leghosszabb…” | maximum- / minimumkiválasztás |
| „sorold fel / gyűjtsd ki azokat, amelyek…” | kiválogatás |
| „mindkettőben…” / „bármelyikben…” | metszet / unió |

### Tipikus hibák

- **`while (t[i] != keresett && i < n)`**: rossz sorrend – ha nincs találat, a `t[n]` kivételt dob. Mindig `i < n &&` legyen elöl.
- **Az eredmény kiírása a ciklus belsejében**: az összeg / darabszám csak a ciklus **után** végleges.
- **Gyűjtőváltozó inicializálása a cikluson belül**: `int db = 0;` a ciklus elé kerül.
- **`max = 0` kezdőérték**: negatív számoknál hibás; helyette `maxI = 0` és a ciklus 1-től.
- **Csere segédváltozó nélkül**: `t[i] = t[j]; t[j] = t[i];` mindkettőt ugyanarra állítja. Helyes: `(t[i], t[j]) = (t[j], t[i]);` vagy `int s = t[i]; t[i] = t[j]; t[j] = s;`.
- **Kiválogatásnál a felesleges nullák**: az eredménytömb végén `Array.Resize(ref eredmeny, db)` nélkül ott maradnak az üres helyek.

## Példák

| Projekt | Mit mutat be |
|---|---|
| [peldak/AlapTetelek](peldak/AlapTetelek/) | egy havi hőmérséklet-sorozaton az összegzés, megszámlálás, eldöntés, kiválasztás, keresés és maximumkiválasztás saját kóddal, mellette ellenőrzésként a LINQ-megfelelő |
| [peldak/OsszetettTetelek](peldak/OsszetettTetelek/) | kiválogatás, szétválogatás, metszet, unió, minimumkiválasztásos és buborékrendezés két számsorozaton |

## Feladatok

| Azonosító | Feladat | Nehézség | Megoldás |
|---|---|---|---|
| 13-M02-L03-F01 | [Kosárlabda-szezon](feladatok/F01-kosarlabda-szezon/) | ★☆☆ | [megoldás](megoldasok/F01-kosarlabda-szezon/) |
| 13-M02-L03-F02 | [Szakkörök](feladatok/F02-szakkorok/) | ★★☆ | [megoldás](megoldasok/F02-szakkorok/) |
| 13-M02-L03-F03 | [Rendezések versenye](feladatok/F03-rendezesek-versenye/) | ★★★ | [megoldás](megoldasok/F03-rendezesek-versenye/) |

## Kapcsolódó

- Előző lecke: 13-M02-L02 (Tömbök és mátrixok)
- Következő lecke: 13-M02-L04 (Metódusok, struktúrák, listák)
- Tanmenet: [1-13](../../../tanmenet/1-13-2felev.md)
