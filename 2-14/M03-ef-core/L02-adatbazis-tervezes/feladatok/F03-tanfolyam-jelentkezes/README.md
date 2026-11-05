# 14-M03-L02-F03 · Tanfolyami jelentkezések (N:M)

**Nehézség:** ★★★ **Becsült idő:** 60 perc

## Feladat

Egy felnőttképző központ nyilvántartást szeretne a tanfolyamairól. A megrendelő leírása:

> Minden tanfolyamnak van neve, kezdési dátuma, ára és maximális létszáma. Egy tanfolyamot egy oktató tart, de egy
> oktató több tanfolyamot is vezethet; az oktatónak a nevét és a szakterületét tartjuk nyilván. A résztvevőknek a
> nevét és az e-mail-címét tároljuk – két résztvevőnek nem lehet ugyanaz az e-mail-címe. Egy résztvevő több
> tanfolyamra is jelentkezhet, de ugyanarra csak egyszer. Minden jelentkezésnél rögzítjük a jelentkezés dátumát és
> azt, hogy a díjat kifizette-e már.

1. Gyűjtsd ki az egyedeket, a tulajdonságaikat és a kapcsolatokat (számossággal)! Rajzolj ábrát.
2. Készíts `tanfolyam.sql` szkriptet az `estis_tanfolyam` adatbázishoz: táblák kulcsokkal, `AUTO_INCREMENT`-tel,
   `UNIQUE` megszorítással, idegen kulcsokkal. A kapcsolótábla elsődleges kulcsa akadályozza meg, hogy valaki
   kétszer jelentkezzen ugyanarra!
3. Töltsd fel (kitalált) adatokkal: legalább 3 oktató, 5 tanfolyam, 8 résztvevő, 12 jelentkezés.
   Legyen olyan tanfolyam, amely betelt, és olyan, amelyre senki nem jelentkezett.
4. Próbáld ki, hogy a dupla jelentkezés és a nem létező tanfolyamra jelentkezés hibát ad!
5. Lekérdezések:
   - tanfolyamonként a jelentkezők száma és a maximális létszám (a jelentkező nélküli tanfolyam is jelenjen meg 0-val);
   - a betelt tanfolyamok neve;
   - azok a résztvevők, akik legalább két tanfolyamra jelentkeztek;
   - a ki nem fizetett díjak összege;
   - oktatónként a tanfolyamaira jelentkezettek száma.

## Minta kimenet (5. pont, első lekérdezés, a megoldás adataival)

```
nev               jelentkezok  max_letszam
C# alapok                   4            4
SQL kezdőknek               3            5
Excel haladó                2            6
WPF felületek               3            4
Python bevezető             0            5
```

## Tippek

- Három „valódi” egyed (oktató, tanfolyam, résztvevő) és egy kapcsolótábla kell.
- Gondolj a 2NF-re: a `jelentkezes` táblába **ne** kerüljön a tanfolyam ára – az csak a tanfolyamtól függ.
- Logikai mező MySQL-ben: `fizetve BOOL NOT NULL DEFAULT 0`.
- A 0-val megjelenő sorhoz `LEFT JOIN` és `COUNT(j.resztvevo_id)` kell (a `COUNT(*)` 1-et adna).
- Betelt: `HAVING COUNT(*) >= t.max_letszam`.

Megoldás: [../../megoldasok/F03-tanfolyam-jelentkezes/](../../megoldasok/F03-tanfolyam-jelentkezes/)
