using RetegzettKeszlet.Adatretag;
using RetegzettKeszlet.UzletiLogika;

// Megjelenítési réteg: csak kiír és bekér.
var repository = new CsvTermekRepository("termekek.csv");
var keszlet = new KeszletSzolgaltatas(repository);

foreach (string sor in repository.HibasSorok)
    Console.WriteLine($"Hibás sor: {sor}");
Console.WriteLine($"Beolvasott termékek: {keszlet.Darab}");

Console.WriteLine($"\nUtánrendelendő ({KeszletSzolgaltatas.UtanrendelesiHatar} db alatt):");
foreach (var t in keszlet.Utanrendelendok())
    Console.WriteLine($"  {t.Nev} – {t.Keszlet} db");

Console.WriteLine("\nKészletérték kategóriánként:");
foreach (var (kategoria, ertek) in keszlet.KeszletertekKategoriankent())
    Console.WriteLine($"  {kategoria}: {ertek:N0} Ft");

var legdragabb = keszlet.Legdragabb();
if (legdragabb != null)
    Console.WriteLine($"\nLegdrágább termék: {legdragabb.Nev} ({legdragabb.Ar:N0} Ft)");

Console.Write("\nKategória: ");
var talalatok = keszlet.Kategoriaban(Console.ReadLine() ?? "");
if (talalatok.Count == 0)
    Console.WriteLine("  Nincs ilyen kategória.");
foreach (var t in talalatok)
    Console.WriteLine($"  {t.Nev}: {t.Ar:N0} Ft, {t.Keszlet} db");
