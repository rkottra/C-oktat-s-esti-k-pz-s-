# 13-M03-L01-F03 · Mozijegy-foglalás

**Nehézség:** ★★★ **Becsült idő:** 60 perc

## Feladat

Egy kis mozi napi előadásaira lehet jegyet foglalni. Készíts .NET 8 konzolos alkalmazást `Mozijegy` néven, menüvel.

1. **`Eloadas`** osztály (külön fájlban):
   - `FilmCim` (szöveg, nem lehet üres) és `Kezdes` (szöveg, pl. `"18:30"`) – tulajdonságok;
   - `Ferohely` – **csak olvasható** (`{ get; }`), a konstruktorban kap értéket; legalább 1 legyen;
   - `Foglalt` – kívülről csak olvasható (`private set`), kezdetben 0;
   - `JegyAr` – egész, pozitív;
   - számított tulajdonságok: `Szabad` (szabad helyek száma), `Telt` (igaz, ha nincs szabad hely), `Kihasznaltsag` (foglalt / férőhely százalékban, tört szám), `Bevetel` (foglalt × jegyár);
   - `bool Foglal(int db)` – csak akkor foglal, ha `db` pozitív és van elég szabad hely; különben `false`;
   - `bool Lemond(int db)` – csak akkor mond le, ha `db` pozitív és legfeljebb annyi, amennyi foglalt;
   - `ToString()`: kezdés, cím, foglalt/férőhely, kihasználtság és `TELT HÁZ` jelzés, ha betelt.
2. A főprogram egy `List<Eloadas>` listában tároljon legalább 4 előadást (különböző férőhellyel és jegyárral), és jelenítsen meg egy menüt, amíg a felhasználó ki nem lép:

   ```
   1 – Előadások listázása
   2 – Jegyfoglalás
   3 – Lemondás
   4 – Statisztika
   0 – Kilépés
   ```

   - **Listázás:** sorszámmal (1-től) írja ki az előadásokat.
   - **Foglalás / lemondás:** kérje be az előadás sorszámát és a jegyek számát; írja ki, sikerült-e (és ha nem, miért nem: rossz sorszám vagy nincs elég hely / foglalás).
   - **Statisztika:** összes eladott jegy, összes bevétel, a legnagyobb kihasználtságú előadás, és a telt házas előadások száma.
   - Hibás (nem szám) bemenetre a program ne álljon le – használd az `int.TryParse`-t.

## Minta kimenet

```
1 – Előadások listázása  2 – Jegyfoglalás  3 – Lemondás  4 – Statisztika  0 – Kilépés
Választás: 2
Előadás sorszáma: 3
Jegyek száma: 5
Sikeres foglalás: 20:45 Dűne – második rész  30/30 (100 %) TELT HÁZ

Választás: 2
Előadás sorszáma: 3
Jegyek száma: 1
Nincs elég szabad hely (szabad: 0).

Választás: 4
Eladott jegyek: 74 db
Bevétel: 189 260 Ft
Legnagyobb kihasználtság: 20:45 Dűne – második rész (100 %)
Telt házas előadás: 1
```

## Tippek

- A csak olvasható tulajdonság (`public int Ferohely { get; }`) a konstruktorban még értéket kaphat, utána már nem.
- A menühöz `while (true)` ciklus és `switch` kényelmes; a `0`-nál `break`-kel vagy egy `bool fut` változóval lépj ki.
- A sorszámot alakítsd indexszé (`sorszam - 1`), és ellenőrizd, hogy `0 <= index < lista.Count`.
- A foglalás logikáját az osztályba tedd, ne a `Program.cs`-be: a főprogram csak meghívja a `Foglal`-t, és a visszatérési értéket nézi.

Megoldás: [../../megoldasok/F03-mozijegy/](../../megoldasok/F03-mozijegy/)
