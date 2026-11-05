# 14-M06-L06-F01 · Sablonból 30 perc alatt

**Nehézség:** ★★☆ **Időkeret:** 30 perc (stopperrel!)

## Feladat

Gyakorold, hogy a [SablonWpfDataGrid](../../peldak/SablonWpfDataGrid/) sablonból mennyi idő alatt jutsz el egy működő alkalmazásig. Mérd az időt, és jegyezd fel, hol akadtál el.

A [nemzeti_parkok.csv](nemzeti_parkok.csv) Magyarország 10 nemzeti parkját tartalmazza (`nev;szekhely;alapitas_eve`, UTF-8, fejléccel).

Készíts `NemzetiParkokCsvWpf` nevű WPF-alkalmazást:

1. A CSV a program mellé másolódik, és induláskor beolvasod egy `NemzetiPark` osztály listájába.
2. Az ablak címe `Magyarország nemzeti parkjai`; középen nem szerkeszthető táblázat (*Név*, *Székhely*, *Alapítás éve*), alapítás éve, azon belül név szerint rendezve.
3. Felül *Keresés* szövegdoboz: gépelés közben azokra a parkokra szűr, amelyek nevében **vagy** székhelyében szerepel a beírt szöveg (kis-/nagybetű nem számít).
4. Mellette *Alapítás* legördülő lista: `(mind)`, majd az adatokban előforduló évtizedek `1970–1979` alakban. A két szűrő együtt működjön.
5. Alul `Megjelenítve: N park` felirat, és *Megjelenítettek mentése* gomb, amely a látható sorokat fejléccel együtt a `szurt_parkok.csv` állományba menti, majd üzenetben jelzi a mentett sorok számát.

## Minta

```
Keresés: [duna   ]  Alapítás: [1990–1999 ▼]
 Név                         | Székhely | Alapítás éve
 Duna-Dráva Nemzeti Park     | Pécs     | 1996
 Duna-Ipoly Nemzeti Park     | Budapest | 1997
[Megjelenítettek mentése]                Megjelenítve: 2 park
```

## Önértékelés

| Idő | Értékelés |
|---|---|
| ≤ 20 perc | vizsgarutin megvan |
| 20–30 perc | jó – nézd meg, melyik lépés vitte az időt |
| > 30 perc | gyakorold újra holnap, sablonból, fejből |

## Tippek

- Ne gépeld újra, ami a sablonban már megvan: másold át a `MainWindow` rácsát és a `Frissites()` metódust, és csak a neveket írd át (Ctrl+H, illetve átnevezés: Ctrl+R, R).
- WPF-projektben a `File` és a `StreamWriter` miatt kell a `using System.IO;`.

Megoldás: [../../megoldasok/F01-sablonbol-gyorsan/](../../megoldasok/F01-sablonbol-gyorsan/)
