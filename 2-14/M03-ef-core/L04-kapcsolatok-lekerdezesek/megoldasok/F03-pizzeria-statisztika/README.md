# Megoldás – 14-M03-L04-F03 · Pizzéria-statisztika

Futtatás előtt importáld a feladat `pizzeria.sql` szkriptjét. A `Models` mappa ezzel a paranccsal készült:

```
Scaffold-DbContext "server=localhost;database=estis_pizzeria;user=root;password=" Pomelo.EntityFrameworkCore.MySql -OutputDir Models -NoPluralize -Force
```

Várt eredmények a mintaadatokkal:

| Kérdés | Eredmény |
|---|---|
| legtöbbet költő vevő | Éhes Elemér, 27 100 Ft |
| legtöbbet szállító futár | Robogó Rita: 6 szállítás, 13 pizza |
| legnépszerűbb pizza | Hawaii, 11 db |
| sosem rendelt | Gombás |
| legjobb nap | 2026.10.07., 28 900 Ft |

A lekérdezések két stílusa:

- **betöltés + memóriában számolás** (1. pont): `Include`/`ThenInclude`, utána sima LINQ az objektumokon – akkor jó,
  ha az objektumokra magukra is szükség van (kiírjuk a tételeket);
- **vetítés (Select) navigációval** (2–6. pont): az EF Core az egészet egy SQL-lekérdezéssé alakítja, csak az
  eredmény jön át – nagy adatbázisnál ez a gyorsabb, és `Include` sem kell hozzá.
