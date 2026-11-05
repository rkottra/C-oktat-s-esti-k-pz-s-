# Megoldás – 14-M06-L06-F01 · Sablonból 30 perc alatt

A `SablonWpfDataGrid` sablon átírt változata: `NemzetiPark.cs` (a sablon `Rekord` osztálya helyett), `MainWindow` két szűrővel és CSV-mentéssel.

Figyelj meg:

- az évtizedlista az adatokból készül (`Distinct()`), nem kézzel beírva;
- a `Szuro_Valtozott` kezelő az `InitializeComponent` alatt még nem talál adatot – ezért az `osszes is not null` ellenőrzés;
- a mentés a **megjelenített** (`Lista`) elemeket írja ki, nem az összeset.

Ellenőrzés: `duna` + `1990–1999` → 2 park; `(mind)` + üres keresés → 10 park.
