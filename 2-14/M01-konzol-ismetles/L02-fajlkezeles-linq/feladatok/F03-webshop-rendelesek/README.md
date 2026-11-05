# 14-M01-L02-F03 · Webshop-rendelések kimutatása

**Nehézség:** ★★★ **Becsült idő:** 70 perc

## Feladat

Egy számítástechnikai webshop három hónapnyi rendelése van a [rendelesek.csv](rendelesek.csv) fájlban:

```
rendelesszam;datum;termek;kategoria;darab;egysegar
R1001;2026-10-25;Webkamera;Periféria;2;15990
R1002;2026-10-16;USB-C kábel;Kiegészítő;1;2490
```

Készíts .NET 8 konzolos alkalmazást `WebshopRendelesek` néven, amely kimutatást készít a vezetőségnek.

1. `Rendeles` osztály: a hat adat **csak olvasható** tulajdonságokban (`{ get; }`), a dátum `DateTime` típusú; számított `Osszeg` tulajdonság (darab × egységár); konstruktor CSV-sorból; `ToString()`.
2. Statikus `FajlbolBetolt(string fajlnev, out int hibasSorok)` metódus: a hibás sorokat kihagyja, és a számukat az `out` paraméterben adja vissza.
3. LINQ-kel válaszold meg:
   1. Hány rendelés van (és hány hibás sor), mennyi a teljes bevétel?
   2. Melyik a legnagyobb értékű rendelés?
   3. Kategóriánként az eladott darabszám, a bevétel és a teljes bevételből való részesedés (%), bevétel szerint csökkenő sorrendben.
   4. Havi bevétel és rendelésszám, időrendben (a hónap nevével: *2026. október*).
   5. Melyik 3 termékből adták el a legtöbb darabot? (Holtversenynél név szerint.)
   6. Hány 50 000 Ft feletti rendelés van, és mennyi az átlagértékük?
   7. Melyik napon érkezett a legtöbb rendelés?
   8. Kérj be egy kategórianevet, és listázd ábécérendben, ismétlés nélkül, milyen termékeket rendeltek belőle!
4. Az 1–7. pont eredménye kerüljön a `jelentes.txt` fájlba is (pontosan úgy, ahogy a konzolon megjelent), a 3. pont adatai pedig a `kategoriak.csv` fájlba (`kategoria;darab;bevetel`, fejléccel).

## Minta kimenet

```
1. Rendelések: 48 db (hibás sor: 0), teljes bevétel: 1 599 320 Ft
2. Legnagyobb értékű rendelés: R1012 2026.10.10. 27" monitor – 2 x 79 990 Ft = 159 980 Ft
3. Kategóriánként:
   Monitor      14 db    849 860 Ft (53,1%)
   Periféria    18 db    332 820 Ft (20,8%)
   ...
4. Havi bevétel:
   2026. szeptember     295 840 Ft (10 rendelés)
   2026. október        965 590 Ft (28 rendelés)
   2026. november       337 890 Ft (10 rendelés)
5. A 3 legtöbbet eladott termék:
   24" monitor: 9 db
   ...
8. Kategória: audio
   Termékek: Hangszóró, Headset
```

## Tippek

- Dátum beolvasása: `DateTime.ParseExact(a[1], "yyyy-MM-dd", CultureInfo.InvariantCulture)`; kiírás hónapnévvel: `{datum:yyyy. MMMM}`.
- Havonkénti csoportosítás: a kulcs lehet `new DateTime(r.Datum.Year, r.Datum.Month, 1)` – így időrendbe is rendezhető.
- Egyszerre konzolra és listába írni egy kis helyi függvénnyel lehet: `void Kiir(string s) { Console.WriteLine(s); jelentes.Add(s); }`.
- Ismétlés nélküli lista: `Distinct()`.
- Gondold végig: mi történik a százalékszámításnál, ha `int`-et osztasz `int`-tel? (`100.0 * ...`)

Megoldás: [../../megoldasok/F03-webshop-rendelesek/](../../megoldasok/F03-webshop-rendelesek/)
