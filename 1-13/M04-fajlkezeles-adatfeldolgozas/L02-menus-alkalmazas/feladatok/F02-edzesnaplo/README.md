# 13-M04-L02-F02 · Edzésnapló

**Nehézség:** ★★☆ **Becsült idő:** 75 perc

## Feladat

Egy diák az edzéseit az [edzesek.csv](edzesek.csv) fájlban vezeti:

```
Datum;Sportag;Perc;Kcal
2026-03-02;futás;35;380
```

A dátum `ÉÉÉÉ-HH-NN` alakú szöveg; a dátumkezelést a következő lecke tanítja, itt még szövegként kezeljük. Készíts `Edzesnaplo` nevű menüs .NET 8 konzolos alkalmazást, amely nemcsak lekérdez, hanem **módosítja is** az adatokat, és el is menti őket.

1. **`Edzes`** osztály: a négy adat, konstruktor CSV-sorból és a négy adatból, valamint egy `CsvSor()` metódus, amely visszaalakítja az objektumot CSV-sorrá.
2. A menü:

   ```
   1 – Napló listázása
   2 – Új edzés rögzítése
   3 – Edzés törlése
   4 – Összesítés sportáganként
   5 – Havi összesítés
   6 – Csúcsok
   0 – Mentés és kilépés
   ```

   - **1:** sorszámozott lista (1-től), a végén az edzések száma.
   - **2:** bekéri az adatokat, ellenőrzéssel. A dátum 10 karakter hosszú legyen, a 4. és a 7. karaktere `-`, a többi számjegy. A sportág nem lehet üres, és kisbetűsen kerül tárolásra. A perc 1 és 300, a kcal 1 és 3000 közötti egész. Az új edzés a lista végére kerül, majd a lista dátum szerint rendeződik.
   - **3:** bekér egy sorszámot, megmutatja az edzést, és megerősítést kér (`i/n`). Csak `i` válasz esetén töröl.
   - **4:** sportáganként az alkalmak száma, az összes perc és kcal, valamint az átlagos edzéshossz.
   - **5:** bekér egy hónapot (`2026-03` alakban), és kiírja, hány edzés volt, összesen hány perc és kcal, és melyik sportágból volt a legtöbb.
   - **6:** a leghosszabb edzés, a legtöbb kalóriát égető edzés, és a legjobb „hatékonyság” (kcal/perc).
   - **0:** ha volt módosítás (új edzés vagy törlés), visszaírja a teljes listát az `edzesek.csv`-be, fejléccel együtt, és kiírja, hány edzést mentett.

## Minta kimenet (részlet)

```
Választás: 4
Sportág        Alkalom   Perc    Kcal   Átlag
futás                9    400    4350   44,4 perc
úszás                5    235    2220   47,0 perc
kerékpár             5    425    3590   85,0 perc
kondi                5    260    1580   52,0 perc

Választás: 5
Hónap (ÉÉÉÉ-HH): 2026-04
2026-04: 8 edzés, 493 perc, 4280 kcal; a legtöbb: futás (3 alkalom)
```

## Tippek

- A dátum ellenőrzését írd külön `static bool ErvenyesDatum(string s)` metódusba: hossz, kötőjelek és `char.IsDigit` a többi helyen.
- Az `ÉÉÉÉ-HH-NN` alakú szövegek ábécérendje egyben időrend is: `lista.Sort((a, b) => string.Compare(a.Datum, b.Datum));`
- A havi szűrés: `e.Datum.StartsWith(honap + "-")`.
- Egy `bool modosult` változóban jegyezd fel, kell-e menteni.

Megoldás: [../../megoldasok/F02-edzesnaplo/](../../megoldasok/F02-edzesnaplo/)
