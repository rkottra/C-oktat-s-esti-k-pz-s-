# Megoldás – 14-M04-L04-F01 · Bevételezés CSV-fájlból

WPF-projekt: `RaktarImport.csproj` (`net8.0-windows`). Futtatás: XAMPP-ban indítsd el a MySQL-t, majd F5.
Az első indítás az `EnsureCreated()` hívással létrehozza az `estis_raktar` adatbázist a kezdő adatokkal – migráció nem kell.
Ha a `root` felhasználónak van jelszava, írd be a `RaktarContext.cs` kapcsolati szövegébe.

A `termekek-import.csv` fordításkor a program mappájába másolódik, a fájlválasztó ott nyílik meg. A második futtatás már csak a darabszámokat növeli (minden cikkszám létezik).

Feladat: [../../feladatok/F01-csv-import/](../../feladatok/F01-csv-import/)
