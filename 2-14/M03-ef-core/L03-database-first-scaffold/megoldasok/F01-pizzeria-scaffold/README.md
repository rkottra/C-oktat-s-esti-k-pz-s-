# Megoldás – 14-M03-L03-F01 · Pizzéria adatbázis scaffoldolása

Futtatás előtt importáld a feladat `pizzeria.sql` szkriptjét (XAMPP MySQL fut).

A `Models` mappa a következő paranccsal készült (Package Manager Console, egy sorban):

```
Scaffold-DbContext "server=localhost;database=estis_pizzeria;user=root;password=" Pomelo.EntityFrameworkCore.MySql -OutputDir Models -NoPluralize -Force
```

A generált fájlokat azért tartalmazza a megoldás, hogy adatbázis nélkül is össze lehessen vetni a sajátoddal.
Apróbb eltérések (sorrend, `HasColumnType("int(11)")` vagy `"int"`, a `ServerVersion.Parse(...)` verziószáma) a
MySQL/MariaDB verziójától függenek – ezek nem hibák.

A 4. pont válaszai a `Program.cs` elején, megjegyzésben vannak.

A `#warning` fordítási figyelmeztetést a scaffold szándékosan teszi a kapcsolati szöveg elé (lásd a lecke elméletét).
