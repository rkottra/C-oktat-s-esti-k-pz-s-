# Megoldás – 14-M01-L02-F01 · Országok adatai

- `Orszag.cs`: konstruktor CSV-sorból (`InvariantCulture` a tizedesponthoz), számított `Nepsuruseg`, statikus `FajlbolBetolt` `yield return`-nel.
- `Program.cs`: a 2–11. kérdés LINQ-kel (`Count`, `GroupBy`, `MaxBy`/`MinBy`, `Average`, `Where`, `OrderByDescending`, `Take`, `Distinct`, `FirstOrDefault`), majd `File.WriteAllLines` + `Prepend` a fejléchez.

Az `orszagok.csv` a projekt része, és a fordításkor a kimeneti mappába másolódik (lásd a `.csproj`-ban: `CopyToOutputDirectory`).
Az `europa.csv` a program futtatási könyvtárában jön létre (Visual Studióból indítva: `bin\Debug\net8.0`).
