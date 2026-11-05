# Megoldás – 14-M04-L03-F02 · Ellenőrzött mentés

WPF-projekt: `RaktarValidacio.csproj` (`net8.0-windows`). Futtatás: XAMPP-ban indítsd el a MySQL-t, majd F5.
Az első indítás az `EnsureCreated()` hívással létrehozza az `estis_raktar` adatbázist a kezdő adatokkal – migráció nem kell.
Ha a `root` felhasználónak van jelszava, írd be a `RaktarContext.cs` kapcsolati szövegébe.

Kipróbálandó: hibás adatok mentése (minden hiba egyszerre jelenik meg), majd a MySQL leállítása futás közben és mentés – az alkalmazás üzenetet ad, nem omlik össze.

Feladat: [../../feladatok/F02-validacio/](../../feladatok/F02-validacio/)
