# 13-M05-L01-F01 · Számláló ablak

**Nehézség:** ★☆☆ **Becsült idő:** 30 perc

## Feladat

Készíts WPF-alkalmazást `Szamlalo` néven, amely egy számot mutat, és gombokkal lehet növelni, csökkenteni.

1. Az ablak ne legyen átméretezhető (`ResizeMode="NoResize"`), és a képernyő közepén jelenjen meg.
2. Az elrendezés `Grid` legyen: **három egyforma szélességű oszlop** és **két egyforma magasságú sor**, minden elem körül 5-ös margó.
3. Az első sorban, mindhárom oszlopon átérve (`Grid.ColumnSpan`), nagy betűvel a számláló értéke áll (kezdetben 0).
4. A második sorban három gomb: **Csökkent**, **Nulláz**, **Növel**.
5. Működés:
   - a **Növel** eggyel növeli, a **Csökkent** eggyel csökkenti, a **Nulláz** 0-ra állítja az értéket;
   - az érték csak **0 és 100** között lehet;
   - ha az érték elérte a határt, a megfelelő gomb legyen letiltva (`IsEnabled = false`); 0-nál a Nulláz gomb is.

## Minta

```
┌──────────────────────────────┐
│              17              │
├─────────┬─────────┬──────────┤
│Csökkent │ Nulláz  │  Növel   │
└─────────┴─────────┴──────────┘
```

## Tippek

- Az értéket egy `int` **mezőben** tárold (`private int ertek;`), ne a `Label` szövegéből olvasd vissza.
- Írj egy `Frissit()` metódust, amely kiírja az értéket és beállítja a három gomb `IsEnabled` tulajdonságát. Minden eseménykezelő végén ezt hívd meg – így a szabályok egy helyen vannak.
- A határokat érdemes konstansként megadni: `private const int Maximum = 100;`.

Megoldás: [../../megoldasok/F01-szamlalo/](../../megoldasok/F01-szamlalo/)
