# Megoldás – 14-M03-L04-F02 · Öreg fák vármegyénként

Futtatás előtt importáld az `oregfak.sql` szkriptet (14-M03-L03 példa). A `Models` mappa a 14-M03-L03 példájával
azonos `Scaffold-DbContext … -OutputDir Models -Force` paranccsal készült (`EstisOregfakContext`, `Fas`, `Megyes`).

Megjegyzések a megoldáshoz:

- Az 1., 3. és 4. lekérdezés teljes egészében SQL-ben fut (a gyűjtemény-navigációból al-lekérdezés / JOIN lesz).
- A 2. pontban az `Include(m => m.Fas)` minden fát betölt – kis adatbázisnál ez a legegyszerűbb és legolvashatóbb.
- Az 5. pontban a `Select(f => f.Megye.Nev)` miatt nincs szükség `Include`-ra: csak a nevet kérjük le, nem az objektumot.
