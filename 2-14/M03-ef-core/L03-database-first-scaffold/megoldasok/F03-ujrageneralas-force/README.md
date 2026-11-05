# Megoldás – 14-M03-L03-F03 · Változott az adatbázis: újragenerálás -Force kapcsolóval

Futtatás előtt: `oregfak.sql`, majd a feladat `valtozas.sql` szkriptje. A `Models` mappa a módosított adatbázisból,
a feladatban megadott `Scaffold-DbContext … -Force` paranccsal készült.

**Válaszok:**

- *4. pont:* a program újragenerálás nélkül is fut, mert az EF Core csak azokat az oszlopokat kérdezi le, amelyeket
  a modell ismer – az új oszlopokról egyszerűen nem tud. (Fordítva viszont hiba lenne: ha egy oszlopot **törölnének**
  vagy átneveznének, `Unknown column` hibát kapnánk.)
- *5. pont:* a `-Force` a `Models` mappa generált fájljait teljesen felülírja, ezért a kézzel beírt `Ritka()` metódus
  **elveszett**, a program nem fordult le. A `Kiegeszitesek/FaKiegeszites.cs` megmaradt, mert azt nem a scaffold készítette.
- Új tulajdonságok: `bool Vedett` (az oszlop `NOT NULL`, ezért nem nullázható típus) és `string? Megjegyzes`
  (az oszlop `NULL`-t is megenged, ezért kérdőjeles).
- `-Force` nélkül a scaffold nem írja felül a már létező fájlokat, hanem hibával leáll.
- Code First: **C# → adatbázis** (migráció). Database First: **adatbázis → C#** (újragenerálás). Egy projektben
  az egyiket válasszuk, és ne keverjük őket.
