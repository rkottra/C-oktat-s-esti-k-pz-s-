# 13-M02-L02-F03 · Bűvös négyzet

**Nehézség:** ★★★ **Becsült idő:** 90 perc

## Feladat

Egy n×n-es négyzet **bűvös**, ha az 1, 2, …, n² számok mindegyike pontosan egyszer szerepel benne, és minden sorának, minden oszlopának és mindkét átlójának ugyanannyi az összege. Ez a **bűvös összeg**: n·(n²+1)/2. A 3×3-as bűvös négyzet bűvös összege például 15.

Készíts `BuvosNegyzet` nevű programot, amely két részből áll.

**A) Ellenőrzés.** Kérd be n értékét (3 és 9 között), majd soronként a négyzet elemeit. Egy sorban az n darab számot szóközzel elválasztva kell megadni. A program írja ki a négyzetet igazítva, a sorösszegeket a sorok végére, az oszlopösszegeket alá, majd döntse el, hogy bűvös-e. Ha nem az, írja ki az **első** okot, például:

- `A 2. sor összege 14, nem 15.`
- `A 7 szám többször szerepel.` / `A 10 szám kívül esik az 1..9 tartományon.`

**B) Előállítás.** Páratlan n esetén a program **állítson elő** egy bűvös négyzetet a „sziámi módszerrel”, majd ellenőrizze ugyanazzal a kóddal:

1. Az 1-est a legfelső sor középső cellájába írjuk.
2. A következő számot mindig az előzőtől **egyet fel, egyet jobbra** írjuk. Ha kilépünk a négyzetből, a túloldalon lépünk vissza: felül kilépve az alsó sorba, jobbra kilépve a bal szélső oszlopba.
3. Ha ez a cella már foglalt, akkor helyette az előző szám **alá** írjuk.

## Minta bemenet és kimenet

```
n = 3
1. sor: 2 7 6
2. sor: 9 5 1
3. sor: 4 3 8
   2   7   6 |  15
   9   5   1 |  15
   4   3   8 |  15
 ------------
  15  15  15
Főátló: 15, mellékátló: 15
A négyzet bűvös (bűvös összeg: 15).

Sziámi módszerrel előállított 5×5-ös négyzet:
  17  24   1   8  15 |  65
  23   5   7  14  16 |  65
   4   6  13  20  22 |  65
  10  12  19  21   3 |  65
  11  18  25   2   9 |  65
 --------------------
  65  65  65  65  65
Főátló: 65, mellékátló: 65
A négyzet bűvös (bűvös összeg: 65).
```

## Tippek

- A sorok beolvasása: `Split(' ', StringSplitOptions.RemoveEmptyEntries)`, majd minden darabra `int.TryParse`. Hibás sor esetén kérd be újra.
- Az „egyszer szerepel” feltétel ellenőrzéséhez használj egy `bool[] volt = new bool[n * n + 1]` tömböt: a `volt[x]` jelzi, hogy az `x` szám előfordult-e már.
- Az ellenőrzést, a kiírást és a beolvasást érdemes **lokális függvénybe** tenni (`static string? Ellenoriz(int[,] q) { ... }`), hogy a B) részben ne kelljen megismételni. A függvényekről részletesen a 13-M02-L04 leckében lesz szó.
- A „túloldalon visszalépés” maradékos osztással: `sor = (sor - 1 + n) % n`, `oszlop = (oszlop + 1) % n`.

Megoldás: [../../megoldasok/F03-buvos-negyzet/](../../megoldasok/F03-buvos-negyzet/)
