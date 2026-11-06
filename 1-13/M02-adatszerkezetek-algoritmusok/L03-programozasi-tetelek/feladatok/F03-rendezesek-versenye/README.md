# 13-M02-L03-F03 · Rendezések versenye

**Nehézség:** ★★★ **Becsült idő:** 90 perc

## Feladat

Készíts `RendezesekVersenye` nevű programot, amely három saját rendezőalgoritmust hasonlít össze egymással és a beépített `Array.Sort`-tal.

**Az algoritmusok:**

1. **Minimumkiválasztásos rendezés.**
2. **Javított buborékrendezés:** ha egy menetben nem volt csere, a tömb már rendezett, a rendezés leáll.
3. **Beszúrásos rendezés:** a tömb elejét rendezettnek tekintjük. A következő elemet kivesszük, a nála nagyobbakat egy hellyel jobbra léptetjük, és a felszabadult helyre írjuk be. (Úgy, ahogy a kártyalapokat rendezzük a kézben.)

Mindhárom algoritmus számolja az **összehasonlításokat** (két tömbelem összevetése) és a **mozgatásokat** (egy csere 3 mozgatás, egy léptetés vagy beírás 1).

**A program lépései:**

1. Egy 10 elemű, 1 és 99 közötti véletlen tömbön mutasd meg mindhárom rendezés eredményét. Mindegyik ugyanannak a tömbnek a **másolatát** kapja.
2. Készíts táblázatot a 1000, 2000 és 4000 elemű véletlen tömbökre: az összehasonlítások és a mozgatások száma, valamint az idő milliszekundumban, mindhárom algoritmusra és az `Array.Sort`-ra (ennél csak az idő).
3. Futtasd le a mérést 4000 elemű, **már rendezett** tömbre is.
4. Írd a program végére megjegyzésben a válaszaidat: mi történik az összehasonlítások számával, ha az elemszám duplázódik? Melyik algoritmus használja ki, hogy a tömb már rendezett?

## Minta kimenet (a számok gépenként és futásonként eltérnek)

```
Eredeti:      42 7 88 15 63 3 91 27 50 19
Minimumkiv.:  3 7 15 19 27 42 50 63 88 91
Buborék:      3 7 15 19 27 42 50 63 88 91
Beszúrásos:   3 7 15 19 27 42 50 63 88 91

Véletlen tömb, n = 1000
Algoritmus        Összehasonlítás    Mozgatás     Idő (ms)
Minimumkiv.               499500        2997         1,21
Buborék                   498870      743589         2,85
Beszúrásos                248862      249861         0,78
Array.Sort                     –           –         0,05
...
Rendezett tömb, n = 4000
Buborék                     3999           0         0,01
...
```

## Tippek

- A számlálókat egyszerűbb a rendezés ciklusán belül, helyi változókban vezetni, és egy-egy sorban kiírni őket a rendezés végén. Ha már ismered a metódusokat, egy `static (long hasonlit, long mozgat) Minimum(int[] t)` alakú függvény visszaadhatja mindkettőt.
- A számlálók legyenek `long` típusúak: 4000 elemnél a buborékrendezés mozgatásainak száma milliós nagyságrendű.
- Időmérés: `var sw = System.Diagnostics.Stopwatch.StartNew();` … `sw.Stop();` majd `sw.Elapsed.TotalMilliseconds`.
- Táblázatos kiírás: `$"{nev,-12}{hasonlit,18}{mozgat,12}{ido,13:F2}"`. A negatív szélesség balra, a pozitív jobbra igazít.
- A beszúrásos rendezés belső ciklusa: `while (j >= 0 && t[j] > elem) { t[j + 1] = t[j]; j--; }`. Figyelj rá, hogy az utolsó, sikertelen összehasonlítást is számold!

Megoldás: [../../megoldasok/F03-rendezesek-versenye/](../../megoldasok/F03-rendezesek-versenye/)
