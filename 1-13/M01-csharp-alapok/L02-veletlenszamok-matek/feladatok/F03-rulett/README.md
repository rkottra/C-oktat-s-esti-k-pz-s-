# 13-M01-L02-F03 · Rulett-statisztika

**Nehézség:** ★★★ **Becsült idő:** 50 perc

## Feladat

Készíts konzolos programot `Rulett` néven, amely 200 rulettpörgetést szimulál és megjelenít.

1. Generálj 200 véletlen pörgetést; egy pörgetés eredménye 0 és 36 közötti egész szám.
2. Az eredményeket rajzold ki a képernyőre úgy, hogy **egy sorba 20 pörgetés** kerüljön, minden szám 3 karakter széles mezőben, jobbra igazítva.
3. Minden szám **zöld háttéren** jelenjen meg, az előtér színe:
   - páros szám: fekete,
   - páratlan szám: piros,
   - nulla: fehér.
4. A táblázat alá készíts statisztikát: a pörgetések hány százaléka esett
   - az 1. tucatba (1–12), a 2. tucatba (13–24) és a 3. tucatba (25–36),
   - a nullára,
   - illetve hány páros és hány páratlan szám jött ki.
   A százalékokat egy tizedesjegyre kerekítve írd ki.
5. A megoldáshoz **ne használj tömböt vagy listát** – a számlálók legyenek egyszerű változók.

## Minta kimenet

```
 17 32  0  5 26 11 30  8 19 ...   (20 szám soronként, színezve)
 ...

1. tucat (1–12):   33,5 %
2. tucat (13–24):  31,0 %
3. tucat (25–36):  32,5 %
nulla:              3,0 %
páros: 97, páratlan: 97
```

## Tippek

- `r.Next(0, 37)` adja a 0–36 tartományt.
- A szín kiválasztása egy `if – else if – else` szerkezet; a háttér: `Console.BackgroundColor = ConsoleColor.DarkGreen;`
- Sortörés minden 20. szám után: `if (i % 20 == 0) Console.WriteLine();` (ha `i` 1-től számol).
- A sor végén állítsd vissza a háttérszínt (`Console.ResetColor()`) a sortörés előtt, különben a sor hátralévő része is zöld lesz.
- Melyik tucatba esik egy 1–36 közötti szám? `(szam - 1) / 12 + 1` – egész osztással egyetlen kifejezés! (Vagy `switch`/`if`-ek.)
- A nulla nem páros és nem páratlan a rulettben – a páros/páratlan számlálóba ne számold bele.
- Százalék: `tucat1 * 100.0 / 200` – figyelj a valós osztásra!

Megoldás: [../../megoldasok/F03-rulett/](../../megoldasok/F03-rulett/)
