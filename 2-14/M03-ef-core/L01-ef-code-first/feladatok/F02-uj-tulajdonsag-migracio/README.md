# 14-M03-L01-F02 · Új tulajdonság és migráció

**Nehézség:** ★★☆ **Becsült idő:** 20 perc

## Feladat

A bemutató példát ([EfCodeFirstKonyvtar](../../peldak/EfCodeFirstKonyvtar/)) bővítsd:

1. A `Konyv` osztály kapjon egy új, `bool` típusú `Kolcsonozheto` tulajdonságot, alapértéke `true`.
2. Hozz létre új migrációt `KolcsonozhetoMezo` néven, és frissítsd az adatbázist.
   Nézd meg phpMyAdminban, hogyan változott a `Konyvek` tábla!
3. A program írja ki külön a kölcsönözhető és a nem kölcsönözhető könyveket.
4. Az „Egri csillagok” legyen nem kölcsönözhető (módosítás + mentés).

## Ellenőrző kérdések

- Mi történik, ha a migráció kihagyásával futtatod a programot?
- Mit tartalmaz a `Migrations` mappában létrejött új fájl `Up` és `Down` metódusa?

Megoldás: [../../megoldasok/F02-uj-tulajdonsag-migracio/](../../megoldasok/F02-uj-tulajdonsag-migracio/)
