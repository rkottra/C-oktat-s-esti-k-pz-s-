# 14-M04-L01-F01 · Raktári terméklista

**Nehézség:** ★☆☆ **Becsült idő:** 30 perc

## Feladat

Készíts WPF-alkalmazást (`net8.0-windows`) `RaktarLista` néven, amely egy barkácsáruház raktárkészletét jeleníti meg.

A feladatsor (L01–L04) végig ugyanazt a modellt és az `estis_raktar` adatbázist használja:

- `Kategoria`: `Id`, `Nev` (max. 50 karakter), `Termekek` (lista);
- `Termek`: `Id`, `Cikkszam` (max. 20), `Nev` (max. 80), `Ar` (Ft, egész), `Darab` (egész), `KategoriaId`, `Kategoria`;
  valamint egy számított, nem tárolt tulajdonság: `[NotMapped] public int Ertek => Ar * Darab;`.

Kezdő adatok (`HasData`): 4 kategória (Kéziszerszám, Elektromos gép, Kertészet, Festék) és 12 termék, pl.
`KS-001 Kalapács 500 g, 3290 Ft, 25 db` – a teljes listát átmásolhatod a megoldás `RaktarContext.cs` fájljából.
Az adatbázist az ablak konstruktorában a `db.Database.EnsureCreated()` hozza létre.

1. Telepítsd a NuGet-csomagokat: `Pomelo.EntityFrameworkCore.MySql` (8.0.2) és `Microsoft.EntityFrameworkCore.Tools` (8.0.x).
2. Készítsd el a két entitást a `Models` mappában és a `RaktarContext` osztályt a kezdő adatokkal.
3. Az ablakban egy `DataGrid` mutassa a termékeket **név szerint rendezve**, kézzel megadott oszlopokkal:
   Cikkszám, Név, Kategória (a kategória **neve**!), Ár, Darab, Érték. Az ár és az érték ezres tagolással jelenjen meg.
4. A táblázat ne legyen szerkeszthető.
5. A táblázat alatt egy sor: hány termék van, és mennyi a teljes készlet értéke.

## Minta kimenet

```
┌──────────┬──────────────────────────┬────────────────┬────────┬───────┬───────────┐
│ Cikkszám │ Név                      │ Kategória      │ Ár (Ft)│ Darab │ Érték (Ft)│
├──────────┼──────────────────────────┼────────────────┼────────┼───────┼───────────┤
│ EG-001   │ Akkus fúrócsavarozó 18 V │ Elektromos gép │ 32 990 │     4 │   131 960 │
│ KT-003   │ Ásó                      │ Kertészet      │  5 490 │     2 │    10 980 │
│ ...                                                                               │
└───────────────────────────────────────────────────────────────────────────────────┘
12 termék, összérték: 741 260 Ft
```

## Tippek

- `AutoGenerateColumns="False"`, `IsReadOnly="True"`, oszlopok: `DataGridTextColumn`.
- A kategória nevéhez `Include(t => t.Kategoria)` és `Binding="{Binding Kategoria.Nev}"` kell.
- Ezres tagolás: `StringFormat={}{0:N0}`.
- Az `Ertek` nem adatbázis-oszlop, ezért az összegzést a `ToList()` után, a memóriában végezd.

Megoldás: [../../megoldasok/F01-raktar-lista/](../../megoldasok/F01-raktar-lista/)
