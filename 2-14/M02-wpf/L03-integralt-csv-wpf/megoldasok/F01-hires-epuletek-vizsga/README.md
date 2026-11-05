# Megoldás – 14-M02-L03-F01 · Híres épületek (vizsgaszerű feladat)

Nyisd meg a `HiresEpuletekVizsga.csproj` fájlt Visual Studióban, és futtasd (F5). Az `epuletek.csv` a projekt mappájában van,
fordításkor a program mellé másolódik (`CopyToOutputDirectory` a csproj-ban).

| Rész | Hol található |
|---|---|
| 1. Adatmodell, beolvasás | `Epulet.cs` (`CsvSorbol`, `InvariantCulture`), `MainWindow` konstruktora (`try`–`catch`) |
| 2. Táblázat | `MainWindow.xaml` – `DataGrid` saját oszlopokkal; `EpitesEveSzoveg` számított tulajdonság |
| 3. Szűrés | `Szures()` – mindhárom szűrő eseménye a `Szuro_Valtozott` kezelőt hívja |
| 4. Statisztika | `StatisztikaFrissitese()` – üres találati listát is kezel |
| 5. Részletek | jobb oldali `GroupBox`, `DataContext="{Binding ElementName=EpuletTabla, Path=SelectedItem}"`; a látogathatóság színe kódból |

Megjegyzések:

- A `Window` `Language="hu-HU"` beállítása miatt a kötések is tizedesvesszővel formáznak.
- A `Kor` tulajdonság az aktuális évből számol, ezért az ellenőrző értékek évről évre változnak (pl. 2026-ban a Parthenón 2457 éves).
