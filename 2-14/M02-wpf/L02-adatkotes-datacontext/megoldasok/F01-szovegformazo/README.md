# Megoldás – 14-M02-L02-F01 · Szövegformázó

Nyisd meg a `SzovegFormazo.csproj` fájlt Visual Studióban, és futtasd (F5).

- A `MainWindow.xaml.cs` szándékosan üres: minden kapcsolat `ElementName`-es `Binding`.
- A láthatóságot a beépített `BooleanToVisibilityConverter` alakítja át (`Window.Resources`-ban, `StaticResource`-ként hivatkozva).
- A betűméret-`TextBox` kötése alapból kétirányú, az `UpdateSourceTrigger=PropertyChanged` miatt gépelés közben is mozgatja a csúszkát.
