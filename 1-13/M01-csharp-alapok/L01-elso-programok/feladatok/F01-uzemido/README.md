# 13-M01-L01-F01 · Üzemidő átváltása

**Nehézség:** ★☆☆ **Becsült idő:** 20 perc

## Feladat

Egy gép vezérlője másodpercekben tárolja, mióta üzemel. Készíts konzolos programot `Uzemido` néven, amely ezt az értéket olvashatóbb formára alakítja.

1. Hozz létre új .NET 8 konzolos projektet Visual Studióban (*Console App*, `Uzemido` néven).
2. Kérd be az üzemidőt másodpercben (egész szám, akár több milliárd is lehet – válassz ehhez megfelelő típust!).
3. Számold ki, hány nap, óra, perc és másodperc ez, és írd ki a minta szerint.
4. Írd ki azt is, hány **óra** ez tizedes törtként, két tizedesjegyre kerekítve.
5. Tegyél töréspontot a számolás első sorára, és lépésenként (F10) kövesd végig, hogyan változnak a változók a *Locals* ablakban.

## Minta kimenet

```
Üzemidő másodpercben: 200000
200000 másodperc = 2 nap 7 óra 33 perc 20 másodperc
Ez összesen 55,56 óra.
```

## Tippek

- Egy nap 86 400 másodperc, egy óra 3600, egy perc 60.
- Az egész osztás (`/`) és a maradékképzés (`%`) együtt adja meg a megoldást: `int nap = mp / 86400; int marad = mp % 86400;`
- Ha `int`-tel 3 000 000 000-t írsz be, a `Parse` hibát dob – próbáld ki, aztán használj `long`-ot!
- A tizedes törthez legalább az egyik operandus legyen valós: `mp / 3600.0`.

Megoldás: [../../megoldasok/F01-uzemido/](../../megoldasok/F01-uzemido/)
