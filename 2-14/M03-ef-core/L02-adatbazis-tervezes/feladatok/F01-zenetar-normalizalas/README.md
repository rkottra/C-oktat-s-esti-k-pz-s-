# 14-M03-L02-F01 · Zenetár normalizálása

**Nehézség:** ★☆☆ **Becsült idő:** 40 perc

## Feladat

Egy lemezbolt eddig táblázatkezelőben tartotta nyilván az albumokat, egyetlen táblában:

| album_cim | ev | eloado | eloado_orszag | mufaj | dalok |
|---|---|---|---|---|---|
| 10000 lépés | 1969 | Omega | Magyarország | rock | Gyöngyhajú lány, 10000 lépés |
| Időrabló | 1977 | Omega | Magyarország | rock | Időrabló |
| Hungarian Rhapsody | 2012 | Queen | Egyesült Királyság | rock | Bohemian Rhapsody, Somebody to Love |
| Jazz | 1978 | Queen | Egyesült Királyság | rock | Mustapha, Bicycle Race |
| Kind of Blue | 1959 | Miles Davis | USA | jazz | So What, Blue in Green |

1. Írd le (a megoldásod elején, SQL-megjegyzésben), milyen **redundanciát** látsz, és mondj rá egy-egy példát a
   módosítási, törlési és bővítési anomáliára!
2. Melyik normálformát sérti a `dalok` oszlop? És az `eloado_orszag`?
3. Tervezd meg a táblákat (`eloado`, `mufaj`, `album`, `dal`) úgy, hogy minden adat csak egyszer szerepeljen:
   - mindegyiknek legyen `id` elsődleges kulcsa `AUTO_INCREMENT`-tel;
   - jelöld ki az idegen kulcsokat és a kapcsolatok típusát.
4. Készíts `zenetar.sql` szkriptet, amely létrehozza az `estis_zenetar` adatbázist, a táblákat
   (`ENGINE=InnoDB`, `utf8mb4_hungarian_ci`), és feltölti őket a fenti adatokkal.
5. Importáld phpMyAdminba, és nézd meg a **Tervező** fülön a kapcsolatokat.
6. Írj egy lekérdezést, amely kiírja az albumokat előadóval és műfajjal együtt, és egyet, amely megszámolja,
   hány dal van albumonként.

## Minta kimenet (6. pont)

```
album_cim            ev    eloado         mufaj
Kind of Blue         1959  Miles Davis    jazz
10000 lépés          1969  Omega          rock
Időrabló             1977  Omega          rock
...
```

## Tippek

- Egy dal egy albumhoz tartozik → a `dal` tábla kap `album_id` idegen kulcsot.
- A feltöltés sorrendje: `mufaj`, `eloado` → `album` → `dal`.
- Ha egy szövegben aposztróf van, SQL-ben duplázd meg: `'Rock''n''roll'`.

Megoldás: [../../megoldasok/F01-zenetar-normalizalas/](../../megoldasok/F01-zenetar-normalizalas/)
