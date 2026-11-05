# Megoldás – 14-M06-L03-F02 · Megyeszűrő és összesítés

Az F01 grafikus megoldásának kiegészítése (`TelepulesekSzures`); a modellek, a context és az `UjTelepulesAblak` változatlanok. Futtatás előtt: MySQL fut, `settlements.sql` importálva.

Lényeg a `MainWindow.xaml.cs`-ben:

- „álmegye” (`Id = 0`) a lista elején;
- `IQueryable`-re feltételesen fűzött `Where` – a szűrés SQL-ben fut;
- az összesítő felirat a ténylegesen megjelenített listából számol.

Ellenőrzés: Nógrád → 6 város, 83 350 lakos; (összes megye) → 346 város.
