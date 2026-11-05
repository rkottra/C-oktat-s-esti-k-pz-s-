# Megoldás – 14-M06-L02-F01 · 1. próbavizsga: F1-pilóták

Futtatás előtt: XAMPP-ban fusson a MySQL, és legyen importálva a [f1pilots.sql](../../feladatok/F01-pilotak-wpf/f1pilots.sql) (létrehozza az `estis_f1pilots` adatbázist). Migráció nem kell: az adatbázis már létezik, a modellt attribútumokkal kötöttük hozzá. Ha a MySQL `root` felhasználójának van jelszava, írd be a `PilotakContext` kapcsolati szövegébe.

Felépítés: `Models/Pilota.cs` (entitás `[Table]`/`[Column]` attribútumokkal), `PilotakContext.cs` (kapcsolat), `PilotakViewModel.cs` (lista + betöltés, felvétel, törlés), `MainWindow` (felület, ellenőrzés).

Alternatíva a 2. feladathoz (Package Manager Console):

```
Scaffold-DbContext "server=localhost;database=estis_f1pilots;user=root;password=" Pomelo.EntityFrameworkCore.MySql -OutputDir Models -Context PilotakContext
```

## Javítókulcs (50 pont)

| # | Szempont | Pont |
|---|---|---|
| 1 | az adatbázis importálva, 800 sor | 4 |
| 2 | NuGet-csomagok (8.0.x) | 2 |
| | entitás a táblához kötve (attribútum vagy scaffold) | 4 |
| | DbContext helyes kapcsolati szöveggel, a program csatlakozik | 4 |
| 3 | ablakcím, kétoszlopos elrendezés, táblázat + törlés gomb + darabszám | 4 |
| | „Új pilóta” csoport a négy vezérlővel és a *Mentés* gombbal, megfelelő vezérlőtípusok | 4 |
| | reszponzív viselkedés (csillagos méretek, nincs fix ablakméretű elrendezés) | 2 |
| 4 | minden pilóta megjelenik, magyar fejlécek | 2 |
| | dátum `éééé.hh.nn` formában, a táblázat nem szerkeszthető | 2 |
| | darabszám és nemzetiség-lista (különböző értékek, ábécérend) | 2 |
| 5 | négy ellenőrzés, mindegyik külön hibaüzenettel (4 × 2) | 8 |
| | mentés az adatbázisba, a táblázat és a darabszám frissül, a mezők kiürülnek | 4 |
| 6 | figyelmeztetés kijelölés nélkül | 2 |
| | Igen/Nem megerősítés a pilóta nevével | 3 |
| | törlés az adatbázisból, frissítés | 3 |
| | **Összesen** | **50** |

Gyakori pontvesztések: a `SaveChanges()` hiányzik (a táblázatban látszik, újraindítás után eltűnik); a jövőbeli dátum nincs ellenőrizve; a megerősítő ablak „Nem” válaszra is töröl.
