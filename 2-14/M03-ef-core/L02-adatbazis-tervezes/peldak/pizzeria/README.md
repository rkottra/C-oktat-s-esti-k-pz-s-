# Pizzéria adatbázis – bemutató példa

| Fájl | Tartalom |
|---|---|
| [pizzeria.sql](pizzeria.sql) | adatbázis és öt tábla létrehozása kulcsokkal, kapcsolatokkal és mintaadatokkal |
| [lekerdezesek.sql](lekerdezesek.sql) | JOIN-ok 1:N és N:M kapcsolaton át, csoportosítás, LEFT JOIN, idegenkulcs-hibák |

## Az adatbázis szerkezete

```
vevo (id PK, nev, cim)                   futar (id PK, nev, telefon UNIQUE)
   1                                        1
   │                                        │
   N                                        N
rendeles (id PK, vevo_id FK, futar_id FK, idopont)
   1
   │
   N
tetel (rendeles_id PK+FK, pizza_id PK+FK, db)  N ───── 1  pizza (id PK, nev, ar)
```

A `rendeles` és a `pizza` között **N:M** kapcsolat van (egy rendelésben több pizza, egy pizza sok rendelésben),
ezt a `tetel` kapcsolótábla bontja két 1:N kapcsolatra. A kapcsolat saját adata (darabszám) is ide kerül.

## Kipróbálás

1. XAMPP Control Panel → indítsd el a **MySQL**-t (és az Apache-ot, ha phpMyAdmint használsz).
2. phpMyAdmin (`http://localhost/phpmyadmin`) → **Importálás** → válaszd a `pizzeria.sql` fájlt → **Importálás**.
   *Vagy* MySQL Workbench → *File → Run SQL Script…*
3. Nézd meg a kapcsolatokat: phpMyAdmin → `estis_pizzeria` → **Tervező** fül
   (Workbench-ben: *Database → Reverse Engineer…* → EER-diagram).
4. Futtasd egyenként a `lekerdezesek.sql` utasításait (SQL fül), és értelmezd az eredményt.

Ugyanezt az adatbázist használja a [14-M03-L03](../../../L03-database-first-scaffold/) (scaffold) és a
[14-M03-L04](../../../L04-kapcsolatok-lekerdezesek/) (lekérdezések EF Core-ral) egy-egy feladata.
