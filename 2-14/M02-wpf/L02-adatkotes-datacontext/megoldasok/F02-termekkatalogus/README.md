# Megoldás – 14-M02-L02-F02 · Termékkatalógus

Nyisd meg a `Termekkatalogus.csproj` fájlt Visual Studióban, és futtasd (F5).

- `Termek.cs`: `INotifyPropertyChanged`, minden setter értesítést küld (`Valtozott(nameof(...))`).
- A szerkesztő `GroupBox` `DataContext`-je a lista `SelectedItem`-je, így a `TextBox`-ok kötéseiben elég a tulajdonság neve.
- A 10% akció gomb csak az objektum `Ar` tulajdonságát írja át – a lista és a mező a változásértesítés miatt frissül.
