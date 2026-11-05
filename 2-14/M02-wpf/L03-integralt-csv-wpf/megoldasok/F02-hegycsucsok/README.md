# Megoldás – 14-M02-L03-F02 · Hegycsúcsok böngészője

Nyisd meg a `Hegycsucsok.csproj` fájlt Visual Studióban, és futtasd (F5). A `hegycsucsok.csv` fordításkor a program mellé másolódik.

- Beolvasás egy LINQ-lánccal: `File.ReadAllLines(...).Skip(1).Where(...).Select(Hegycsucs.CsvSorbol).ToList()`.
- A `Frissites()` metódus szűr, rendez, beállítja a `ListBox` `ItemsSource`-át és az összegző sort.
- A részletező panel kötései a `ListBox` `SelectedItem`-jére mutatnak; a `Nyolcezres` számított tulajdonság egyirányú kötéssel jelenik meg.
