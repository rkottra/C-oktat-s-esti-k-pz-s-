# 14-M03-L02-F02 · Könyvtárak: kulcsok és kapcsolatok utólag

**Nehézség:** ★★☆ **Becsült idő:** 45 perc

## Feladat

A [konyvtarak-kulcs-nelkul.sql](konyvtarak-kulcs-nelkul.sql) szkript két vármegye könyvtárait tartalmazza három
táblában (`megye`, `telepules`, `konyvtar`), de valaki elfelejtette beállítani a kulcsokat és a kapcsolatokat.

1. Importáld a szkriptet phpMyAdminba (létrejön az `estis_konyvtarak` adatbázis).
2. Rajzold le (papíron vagy a Tervezőben), milyen kapcsolatok vannak a táblák között, és milyen típusúak!
3. Jelöld ki az **elsődleges kulcsokat** `ALTER TABLE` utasításokkal:
   - `megye.id` és `konyvtar.id` – mesterséges kulcsok, kapjanak `AUTO_INCREMENT`-et is;
   - `telepules.irsz` – természetes kulcs (ebben az adatkészletben minden irányítószám egyedi).
4. Hozd létre az **idegen kulcsokat**: `telepules.megye_id → megye.id`, `konyvtar.irsz → telepules.irsz`.
   Az egyik nem fog sikerülni! Írj lekérdezést, amely megkeresi a hibás sort (olyan könyvtár, amelynek az
   irányítószáma nincs a `telepules` táblában), javítsd az adatot (a könyvtár valójában Szekszárdon van,
   irányítószáma `7100`), majd hozd létre a kapcsolatot.
5. Ellenőrizd a **Tervező** fülön a kapcsolatokat.
6. Lekérdezések:
   - a könyvtárak neve a település nevével, település szerint rendezve;
   - vármegyénként hány könyvtár van;
   - az 5 legtöbb könyvtárral rendelkező település.

## Minta kimenet (6. pont, utolsó lekérdezés)

```
nev              db
Szekszárd         8
Salgótarján       7
Balassagyarmat    5
Paks              5
...
```

## Tippek

- Elsődleges kulcs utólag: `ALTER TABLE megye ADD PRIMARY KEY (id);`
- AUTO_INCREMENT utólag: `ALTER TABLE megye MODIFY id INT NOT NULL AUTO_INCREMENT;`
- A hiányzó szülőt `LEFT JOIN … WHERE t.irsz IS NULL` mutatja meg.
- Az idegen kulcs és a hivatkozott kulcs típusa egyezzen meg (`CHAR(4)` – `CHAR(4)`).

Megoldás: [../../megoldasok/F02-konyvtarak-kulcsok/](../../megoldasok/F02-konyvtarak-kulcsok/)
