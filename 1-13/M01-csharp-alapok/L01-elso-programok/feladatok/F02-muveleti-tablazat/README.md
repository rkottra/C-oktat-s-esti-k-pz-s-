# 13-M01-L01-F02 · Műveleti táblázat

**Nehézség:** ★★☆ **Becsült idő:** 30 perc

## Feladat

Készíts konzolos programot `MuveletiTablazat` néven, amely két bekért számmal elvégzi az alapműveleteket, és az eredményeket rendezett táblázatban írja ki.

1. Kérj be két **egész** számot (`a`, `b`).
2. Írd ki táblázatosan: összeg, különbség, szorzat, egész osztás hányadosa, maradék, valós osztás eredménye (3 tizedesjegyre).
   A művelet neve balra igazítva, 20 karakter széles oszlopban, az eredmény jobbra igazítva, 10 karakter széles oszlopban jelenjen meg.
3. Írd ki logikai értékként is:
   - `a` páros-e,
   - `a` osztható-e `b`-vel,
   - mindkét szám pozitív-e.
4. A fejléc legyen sárga, a logikai `True` értékek zöldek, a `False` értékek pirosak.
   (Ehhez elég a feltételes operátor: `Console.ForegroundColor = paros ? ConsoleColor.Green : ConsoleColor.Red;`)
5. Ha `b` nulla, az osztásos sorok helyett a „nullával nem osztunk” szöveg jelenjen meg.

## Minta kimenet

```
a = 17
b = 5
MŰVELET               EREDMÉNY
összeg                      22
különbség                   12
szorzat                     85
egész osztás                 3
maradék                      2
valós osztás             3,400
a páros?                 False
a osztható b-vel?        False
mindkettő pozitív?        True
```

## Tippek

- Igazítás: `$"{"összeg",-20}{osszeg,10}"` – a negatív szélesség balra, a pozitív jobbra igazít.
- Egész / egész = egész! A valós osztáshoz: `(double)a / b`.
- A `bool` értéket is lehet formázni szélességgel: `{paros,10}`.
- A színt minden kiírás előtt állítsd be, a végén `Console.ResetColor()`.

Megoldás: [../../megoldasok/F02-muveleti-tablazat/](../../megoldasok/F02-muveleti-tablazat/)
