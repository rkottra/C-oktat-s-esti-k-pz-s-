using RetegzettEpuletek.Adatretag;
using RetegzettEpuletek.Kozos;
using RetegzettEpuletek.Modell;
using RetegzettEpuletek.UzletiLogika;

// Megjelenítési réteg: csak bekér és kiír. Minden számolást az üzleti logikára bíz.

// Itt dől el, melyik adatréteget használjuk. Próbáld ki: new MemoriaEpuletRepository()
IEpuletRepository repository = new CsvEpuletRepository("epuletek.csv");
var szolgaltatas = new EpuletSzolgaltatas(repository);

Console.WriteLine("Épületek magasság szerint:");
foreach (Epulet e in szolgaltatas.MagassagSzerint())
    Console.WriteLine($"  {e}");

Console.WriteLine($"\nA legmagasabb: {szolgaltatas.Legmagasabb()?.Nev ?? "nincs adat"}");
Console.WriteLine($"Átlagos magasság: {szolgaltatas.AtlagMagassag():F1} m");

Console.WriteLine("\nÉpületek országonként:");
foreach (var (orszag, darab) in szolgaltatas.OrszagonkentDarab())
    Console.WriteLine($"  {orszag}: {darab}");

Console.Write("\nMinimális magasság (m): ");
if (int.TryParse(Console.ReadLine(), out int hatar))
{
    var lista = szolgaltatas.MagasabbMint(hatar);
    Console.WriteLine(lista.Count == 0
        ? "  Nincs ilyen épület."
        : "  " + string.Join(", ", lista.Select(e => e.Nev)));
}

// Hibás adat felvételi kísérlete: a szabályt az üzleti réteg ellenőrzi, a felület csak kiírja az üzenetet.
try
{
    szolgaltatas.UjEpulet(new Epulet { Nev = "Jövőbeli torony", Varos = "Szeged", Orszag = "Magyarország", Epult = 2999, Magassag = 120 });
}
catch (ArgumentException ex)
{
    Console.WriteLine($"\nNem sikerült felvenni: {ex.Message}");
}

Console.WriteLine("\nNapló (Singleton):");
foreach (string bejegyzes in Naplo.Peldany.Bejegyzesek)
    Console.WriteLine($"  {bejegyzes}");
