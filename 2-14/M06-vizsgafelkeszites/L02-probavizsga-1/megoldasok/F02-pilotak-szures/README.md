# Megoldás – 14-M06-L02-F02 · Szűrés a pilóták között

Az F01 megoldásának kiegészítése (`PilotakSzures`). Futtatás előtti teendők ugyanazok, mint az F01-nél (fusson a MySQL, legyen importálva az `f1pilots.sql`).

Ami új:

- `PilotakViewModel.Szures(nemzetiseg, nevReszlet)` – `IQueryable`-re fűzött `Where`-ek, a szűrés az adatbázisban fut;
- `Allapot` tulajdonság `INotifyPropertyChanged`-del a „Megjelenítve: x / y” felirathoz;
- a szűrősáv vezérlői közös eseménykezelőt használnak (`Szuro_Valtozott`); a `betoltve` jelző megakadályozza, hogy az ablak felépítése közben, illetve a szűrők visszaállítása alatt fölöslegesen fusson a lekérdezés.

Ellenőrzés: `British` + `Hill` → 2 találat; csak `Hungarian` → 1 találat.
