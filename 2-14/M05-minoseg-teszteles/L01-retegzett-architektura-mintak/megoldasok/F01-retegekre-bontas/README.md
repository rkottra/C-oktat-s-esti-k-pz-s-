# Megoldás – 14-M05-L01-F01 · Rétegekre bontás

| Réteg | Fájl | Felelősség |
|---|---|---|
| Modell | `Modell/Termek.cs` | a termék adatai és a készletérték |
| Adatréteg | `Adatretag/ITermekRepository.cs`, `CsvTermekRepository.cs` | CSV beolvasása, hibás sorok gyűjtése |
| Üzleti logika | `UzletiLogika/KeszletSzolgaltatas.cs` | utánrendelés, kategóriánkénti érték, legdrágább, keresés |
| Megjelenítés | `Program.cs` | csak `Console.WriteLine` / `ReadLine` |

Ellenőrzés: futtasd a kiinduló és a megoldás programot ugyanazzal a bemenettel (pl. `Periféria`) – a kimenetnek meg kell egyeznie.
Figyeld meg: a négy párhuzamos lista helyett egyetlen `List<Termek>` van, és a `for` ciklusok helyére LINQ került.
