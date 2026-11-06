# 13-M05-L01-F02 · Számológép valós számokkal

**Nehézség:** ★★☆ **Becsült idő:** 60 perc

## Feladat

A bemutató számológépet ([peldak/Szamologep](../../peldak/Szamologep/)) bővítsd! Hozz létre új projektet `SzamologepBovitett` néven, és másold át bele a példa XAML-jét és kódját (a névteret írd át).

1. Bővítsd a rácsot egy ötödik gombsorral. A gombok elrendezése:

   ```
   CE   C   ⌫   /
   7    8   9   *
   4    5   6   -
   1    2   3   +
   ±    0   ,   =
   ```

2. A számológép **valós számokkal** dolgozzon (`double`), a kijelzőn tizedes**vessző** legyen.
3. Az új gombok:
   - **,** – tizedesvesszőt ír a szám végére; egy számban csak egy vessző lehet; ha új szám kezdődik, `0,` jelenjen meg;
   - **±** – előjelet vált;
   - **⌫** – törli az utolsó karaktert; ha nem marad semmi, `0` legyen a kijelzőn;
   - **CE** – csak az épp beírt számot törli, a félbehagyott művelet megmarad; a **C** mindent töröl.
4. Nullával osztáskor továbbra is hibaüzenet jelenjen meg.

## Minta

```
2,5 * 4 =          → 10
0,1 + 0,2 =        → 0,3
12 ⌫               → 1
5 + 3 CE 4 =       → 9
```

## Tippek

- A szöveg → szám átalakításnál add meg a magyar formátumot, így nem függ a gép nyelvi beállításától:
  `double.Parse(Kijelzo.Text, new CultureInfo("hu-HU"))`, kiírásnál `eredmeny.ToString(new CultureInfo("hu-HU"))`.
- Van-e már vessző a számban? `Kijelzo.Text.Contains(',')`.
- A `0,1 + 0,2` a `double` pontatlansága miatt `0,30000000000000004` lenne – kerekíts kiírás előtt: `Math.Round(eredmeny, 10)`.
- A `switch` kifejezés tömören adja vissza az eredményt: `double eredmeny = muvelet switch { "+" => a + b, … };`

Megoldás: [../../megoldasok/F02-szamologep-bovites/](../../megoldasok/F02-szamologep-bovites/)
