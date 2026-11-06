# Megoldás – 13-M03-L03-F01 · Mértékegység-átváltó

- `Mertekegyseg.cs`: `static class` két `const` állandóval, ellenőrzött statikus tulajdonsággal (`EuroArfolyam`), kívülről csak olvasható statikus számlálóval (`Atvaltasok`) és nyolc statikus átváltó metódussal.
- `Program.cs`: menü `switch`-csel, `try`–`catch` az érvénytelen árfolyamra, és egyetlen `Atvalt` segédmetódus, amely az átváltó metódust **paraméterként** kapja (`Func<double, double>`). A metódusnév zárójel nélkül (`Mertekegyseg.KmbolMerfold`) magát a metódust jelenti, nem a hívását. Kezdőknek elég, ha minden ágban külön kérik be az értéket és írják ki az eredményt.

Megjegyzések:

- A `const` mező automatikusan statikus, ezért `Mertekegyseg.KmPerMerfold` alakban érhető el, és nem kell elé `static`.
- Az árfolyam ellenőrzése a tulajdonságban van, nem a főprogramban. Így bárhonnan állítják be, rossz érték nem kerülhet bele.
- Az `Atvaltasok` csak akkor nő, ha valóban történt átváltás. Az árfolyam beállítása nem számít bele.

Futtatás: F5 (Visual Studio) vagy `dotnet run`.
