# 14-M05-L03-F01 · Mozijegy – határértékek tesztelése

**Nehézség:** ★☆☆ **Becsült idő:** 35 perc

## Feladat

A [kiindulo/Mozijegy](kiindulo/Mozijegy/) osztálykönyvtárban a `JegyarSzamito.Ar(eletkor, nap)` metódus számolja ki egy mozijegy árát.
A specifikáció (ez a helyes működés):

| Szabály | Érték |
|---|---|
| alapár | 2800 Ft |
| gyerek: **14 év alatt** | 50% kedvezmény |
| nyugdíjas: **65 éves kortól** | 30% kedvezmény |
| keddi nap | a fenti árból további 20% kedvezmény |
| érvényes életkor | 0–120; más érték esetén `ArgumentOutOfRangeException` |

A kód **legalább egy hibát tartalmaz**. Ne a kódot olvasva keresd – írj teszteket a specifikáció alapján, és a tesztek mutassák meg!

1. Nyisd meg a kiinduló projektet, majd adj a megoldáshoz egy új **MSTest Test Project**-et `MozijegyTesztek` néven (.NET 8).
2. A tesztprojektben: *Dependencies → Add Project Reference… → Mozijegy*.
3. Írj teszteket `[DataRow]`-okkal:
   - felnőtt hétköznap;
   - az életkor **határértékei**: 0, 13, 14, 64, 65, 120;
   - keddi ár felnőttre, gyerekre, nyugdíjasra;
   - érvénytelen életkor (-1, 121) → kivétel.
4. Futtasd a teszteket (*Test → Test Explorer → Run All*). Jegyezd fel, melyik teszt piros, és mit vártál / mit kaptál.
5. Javítsd ki a hibá(ka)t a `Mozijegy` projektben, és futtasd újra: minden teszt legyen zöld.

## Minta (Test Explorer)

```
✔ Ar_Felnott_Alapar
✖ Ar_EletkorHatarai (14,2800)   Assert.AreEqual failed. Expected:<2800>. Actual:<1400>.
...
```

## Tippek

- Elvárt értékek: 2800 · 50% = 1400; 2800 · 70% = 1960; kedden 2240 / 1120 / 1568.
- A határérték-tesztelés lényege: a határ **mindkét oldalát** és magát a határt is kipróbálod.
- A hét napja: `DayOfWeek.Tuesday`. A `DataRow`-ba enum is írható.

Megoldás: [../../megoldasok/F01-mozijegy/](../../megoldasok/F01-mozijegy/)
