# Megoldás – 14-M03-L04-F01 · Webshop: kategóriák és termékek

Futtatás előtt (XAMPP MySQL fut), Package Manager Console-ban:

```
Add-Migration Kezdo
Update-Database
```

A `Migrations` mappát szándékosan nem tartalmazza a megoldás – a fenti parancsok létrehozzák.

A migráció a `Termekek` táblában `KategoriaId` oszlopot, rá indexet (`IX_Termekek_KategoriaId`) és idegen kulcsot
(`FK_Termekek_Kategoriak_KategoriaId`, `ON DELETE CASCADE`) hoz létre – mindezt pusztán a névkonvenciókból.
Kötelező kapcsolatnál a törlés alapértelmezése a kaszkád: egy kategória törlése a termékeit is törli.
Ha ezt nem szeretnénk, az `OnModelCreating`-ben `.OnDelete(DeleteBehavior.Restrict)` kell (lásd a bemutató példát).
