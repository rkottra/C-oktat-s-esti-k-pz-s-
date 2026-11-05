# Megoldás – 14-M01-L02-F03 · Webshop-rendelések kimutatása

- `Rendeles.cs`: csak olvasható tulajdonságok, `DateTime.ParseExact`, számított `Osszeg`, `FajlbolBetolt` `out` paraméterrel a hibás sorok számához.
- `Program.cs`: többszintű kimutatás (`GroupBy` kategóriára, hónapra, termékre és napra), százalékszámítás, `Distinct`; helyi `Kiir` függvény, amely a konzolra és a jelentésbe is ír.

Kimenet: `jelentes.txt` és `kategoriak.csv` a futtatási könyvtárban (Visual Studióból: `bin\Debug\net8.0`).
