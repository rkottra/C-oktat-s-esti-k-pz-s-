---
id: 13-M02-L02
cim: Tömbök és mátrixok
evfolyam: 1-13
modul: M02 – Adatszerkezetek és algoritmusok
orak: 13–16.
het:
oraszam: 4
elofeltetel: 13-M02-L01
leiras: Egy- és kétdimenziós tömbök létrehozása, feltöltése, bejárása; mátrixműveletek.
---

# 13-M02-L02 · Tömbök és mátrixok

## Célok

A lecke végére a tanuló:

- tudja, miben különbözik a Python-lista, a C# tömb és a C# `List<T>`, és mikor melyiket érdemes használni;
- egy- és kétdimenziós tömböt deklarál, kezdőértékkel lát el, feltölt (képlettel, véletlenszámokkal, billentyűzetről);
- tömböt `for` és `foreach` ciklussal bejár, és ismeri a `Length`, `GetLength`, `Array.Sort`, `Array.Resize` használatát;
- mátrixot sorról sorra és oszlopról oszlopra bejár, sor- és oszlopösszeget, átlót számol, mátrixot tükröz és összead.

## Elmélet

### Python-lista, C# tömb, C# lista

Pythonban egyetlen „mindenes” adatszerkezetet használtunk: a listát. C#-ban két gyakori megfelelője van:

| | Python `list` | C# tömb: `int[]` | C# lista: `List<int>` |
|---|---|---|---|
| létrehozás | `t = [3, 5, 7]` | `int[] t = { 3, 5, 7 };` | `List<int> l = new List<int> { 3, 5, 7 };` |
| üres, adott mérettel | `t = [0] * 10` | `int[] t = new int[10];` (csupa 0) | – (a lista üresen indul) |
| elem típusa | bármi, akár vegyesen | **egyféle**, rögzített | **egyféle**, rögzített |
| méret | változhat | **rögzített** a létrehozáskor | változhat (`Add`, `Remove`) |
| elemszám | `len(t)` | `t.Length` | `l.Count` |
| indexelés | `t[0]`, `t[-1]` | `t[0]`, `t[^1]` | `l[0]`, `l[^1]` |
| hozzáfűzés | `t.append(9)` | nincs (csak új tömbbel) | `l.Add(9)` |
| kiírás | `print(t)` | `string.Join(", ", t)` | `string.Join(", ", l)` |

Mikor melyik? Ha **előre tudjuk az elemszámot** (12 hónap, 10×10-es tábla, 80 mérés), a tömb egyszerű és gyors. Ha az elemszám **menet közben dől el** (fájlból olvasunk, a felhasználó bővíti), a `List<T>` kényelmesebb – részletesen a 13-M02-L04 leckében. A kettő könnyen átalakítható: `tomb.ToList()`, `lista.ToArray()`.

### Egydimenziós tömb (vektor)

```csharp
int[] a = new int[5];                 // 5 elem, mind 0 (string[] esetén null, bool[] esetén false)
int[] b = new int[] { 12, 23, 34 };   // kezdőértékkel, a méret a felsorolásból adódik
int[] c = { 12, 23, 34 };             // ugyanez rövidebben
string[] gyumolcsok = { "alma", "körte", "barack" };

a[0] = 15;                            // írás
int elso = c[0];                      // olvasás
int utolso = c[c.Length - 1];         // vagy c[^1]
```

Az index **0-tól `Length - 1`-ig** megy. A pszeudokódban és a régi tankönyvekben gyakran 1-től indexelnek – C#-ban (és Pythonban) mindig 0-tól.

### Bejárás: for vagy foreach?

```csharp
for (int i = 0; i < t.Length; i++)    // ha kell az index, vagy módosítjuk az elemeket
    t[i] = rnd.Next(1, 101);

foreach (int elem in t)               // ha csak olvassuk az elemeket (Python: for elem in t:)
    Console.Write($"{elem} ");
```

A `foreach` ciklusváltozója **csak olvasható** – az elemeket feltölteni vagy átírni `for` ciklussal kell.

### Hasznos beépített műveletek

| Művelet | Kód | Python megfelelő |
|---|---|---|
| rendezés helyben | `Array.Sort(t);` | `t.sort()` |
| megfordítás helyben | `Array.Reverse(t);` | `t.reverse()` |
| keresés | `Array.IndexOf(t, 34)` (ha nincs: `-1`) | `t.index(34)` (ha nincs: hiba) |
| tartalmazás | `t.Contains(34)` | `34 in t` |
| átméretezés | `Array.Resize(ref t, 20);` | – |
| másolat | `int[] m = (int[])t.Clone();` | `m = t.copy()` |
| összeg, min, max | `t.Sum()`, `t.Min()`, `t.Max()` | `sum(t)`, `min(t)`, `max(t)` |

A `Sum`, `Min`, `Max` kényelmes ellenőrzésre, de a 13-M02-L03 leckében ezeket **magunk is megírjuk** – a dolgozatban is ez lesz a cél.

### Tömb mint referencia

A tömb **referenciatípus**: az `int[] masik = t;` nem másol, hanem ugyanarra a tömbre mutat – ahogy Pythonban a `masik = t` is. Ha `masik[0]`-t módosítjuk, `t[0]` is változik. Valódi másolat: `Clone()` vagy `ToArray()`.

### Elem törlése és beszúrása tömbben

A tömb mérete rögzített, ezért törlésnél az utána következő elemeket **egy hellyel balra léptetjük**, majd (ha kell) csökkentjük a méretet:

```csharp
for (int i = torlendo; i < t.Length - 1; i++)
    t[i] = t[i + 1];
Array.Resize(ref t, t.Length - 1);
```

Beszúrásnál fordítva: megnöveljük a méretet, hátulról haladva jobbra léptetünk, és a felszabadult helyre írunk. (Listánál ezt a `RemoveAt` és az `Insert` elvégzi helyettünk.)

### Kétdimenziós tömb (mátrix)

```csharp
int[,] m = new int[3, 4];             // 3 sor, 4 oszlop
int[,] n = {
    { 1, 2, 3 },
    { 4, 5, 6 }
};                                    // 2×3-as, sorfolytonosan megadva
m[1, 2] = 7;                          // 1. sor, 2. oszlop (0-tól számolva!)

for (int sor = 0; sor < m.GetLength(0); sor++)               // GetLength(0): sorok száma
{
    for (int oszlop = 0; oszlop < m.GetLength(1); oszlop++)  // GetLength(1): oszlopok száma
        Console.Write($"{m[sor, oszlop],4}");                // 4 karakter szélesen, jobbra igazítva
    Console.WriteLine();
}
```

Pythonban listák listájával dolgoztunk (`m[1][2]`). C#-ban is van ilyen, ez a **tömbök tömbje** (jagged array): `int[][] j = new int[3][];` – itt minden sor különböző hosszú lehet, az elérés `j[1][2]`. Téglalap alakú táblázatnál az `int[,]` az egyszerűbb. Figyelem: mátrixnál a `Length` az **összes** elem száma (3×4 = 12).

### Tipikus mátrixműveletek

| Feladat | Ötlet |
|---|---|
| sorösszeg | külső ciklus a sorokon, belső az oszlopokon; az összeget a külső ciklusban nullázzuk |
| oszlopösszeg | a két ciklus fordítva: a külső megy az oszlopokon |
| főátló | azok az elemek, ahol `sor == oszlop` (csak négyzetes mátrixnál) |
| mellékátló | `oszlop == n - 1 - sor` |
| főátló feletti rész | `oszlop > sor` |
| tükrözés a főátlóra (transzponálás) | `m[sor, oszlop]` ↔ `m[oszlop, sor]` csere, de **csak a főátló feletti elemekre** (különben mindent kétszer cserélnénk, és visszakapnánk az eredetit) |
| összeadás | azonos méretű mátrixoknál `c[s, o] = a[s, o] + b[s, o]` |

### Tipikus hibák

- **`IndexOutOfRangeException`**: `t[t.Length]` már nem létezik – a ciklusfeltétel `i < t.Length`, nem `<=`. Negatív index sincs: a `t[-1]` Pythonban működik, C#-ban kivétel; helyette `t[^1]`.
- **`Length` és `GetLength` keverése mátrixnál**: `m.Length` az összes elem száma; a sorok/oszlopok száma `GetLength(0)` és `GetLength(1)`.
- **Kiírás `Console.WriteLine(t)`-vel**: a tömb tartalma helyett a típus nevét írja ki (`System.Int32[]`). Helyette `string.Join(", ", t)` vagy ciklus.
- **Írás `foreach`-ben**: fordítási hiba – feltöltéshez `for` kell.
- **`new Random()` a ciklus belsejében**: egyetlen `Random` objektumot hozzunk létre a ciklus előtt. Ismételhető sorozathoz (teszteléshez) adjunk magot: `new Random(42)`.
- **`rnd.Next(1, 100)` felső határa kizárt**: 1 és 100 között `rnd.Next(1, 101)` kell (Pythonban a `randint(1, 100)` zárt intervallum volt!).

## Példák

| Projekt | Mit mutat be |
|---|---|
| [peldak/TombAlapok](peldak/TombAlapok/) | tömb létrehozása többféleképpen, feltöltés képlettel és véletlenszámmal, Fibonacci-sorozat, `for`/`foreach`, beépített műveletek, referencia vs. másolat, törlés és beszúrás léptetéssel, összevetés a `List<int>`-tel |
| [peldak/MatrixMuveletek](peldak/MatrixMuveletek/) | mátrix kezdőértékkel és véletlenszámokkal, formázott kiírás, sor- és oszlopösszegek, átlók, transzponálás helyben, két mátrix összege, ciklikus (latin) négyzet |

## Feladatok

| Azonosító | Feladat | Nehézség | Megoldás |
|---|---|---|---|
| 13-M02-L02-F01 | [Tömbök feltöltése](feladatok/F01-tombfeltoltes/) | ★☆☆ | [megoldás](megoldasok/F01-tombfeltoltes/) |
| 13-M02-L02-F02 | [Domborzat – repülés a hegyek felett](feladatok/F02-domborzat/) | ★★☆ | [megoldás](megoldasok/F02-domborzat/) |
| 13-M02-L02-F03 | [Bűvös négyzet](feladatok/F03-buvos-negyzet/) | ★★★ | [megoldás](megoldasok/F03-buvos-negyzet/) |

## Kapcsolódó

- Előző lecke: 13-M02-L01 (Karakterláncok kezelése)
- Következő lecke: 13-M02-L03 (Programozási tételek)
- Tanmenet: [1-13](../../../tanmenet/1-13-2felev.md)
