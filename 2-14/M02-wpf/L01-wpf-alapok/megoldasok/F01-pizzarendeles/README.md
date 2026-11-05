# Megoldás – 14-M02-L01-F01 · Pizzarendelő űrlap

Nyisd meg a `Pizzarendeles.csproj` fájlt Visual Studióban, és futtasd (F5).

- Az árak a `ComboBoxItem`-ek `Tag` tulajdonságában vannak, így a XAML-ben egy helyen módosíthatók.
- Az árszámítás egyetlen `Vegosszeg()` metódusban van; hibás darabszám esetén -1-et ad vissza, ezt mindkét gomb kezeli.
