using KeszletMintak;
using KeszletMintak.Adatretag;
using KeszletMintak.UzletiLogika;

var beallitasok = Beallitasok.Peldany;

// Singleton ellenőrzése: kétszer kérjük el, ugyanazt az objektumot kapjuk.
Console.WriteLine($"Ugyanaz a példány? {ReferenceEquals(beallitasok, Beallitasok.Peldany)}");

Console.Write("Utánrendelési határ (Enter = 5): ");
if (int.TryParse(Console.ReadLine(), out int hatar))
    beallitasok.UtanrendelesiHatar = hatar;

Console.Write("Adatforrás (1 = CSV-fájl, 2 = memória): ");
ITermekRepository repository = Console.ReadLine() == "2"
    ? new MemoriaTermekRepository()
    : new CsvTermekRepository(beallitasok.CsvFajl);

// Innentől a kód nem tudja, és nem is érdekli, melyik adatforrás van mögötte.
var keszlet = new KeszletSzolgaltatas(repository);

if (repository is CsvTermekRepository csv)
    foreach (string sor in csv.HibasSorok)
        Console.WriteLine($"Hibás sor: {sor}");
Console.WriteLine($"Beolvasott termékek: {keszlet.Darab}");

Console.WriteLine($"\nUtánrendelendő ({beallitasok.UtanrendelesiHatar} db alatt):");
foreach (var t in keszlet.Utanrendelendok())
    Console.WriteLine($"  {t.Nev} – {t.Keszlet} db");

Console.WriteLine("\nKészletérték kategóriánként:");
foreach (var (kategoria, ertek) in keszlet.KeszletertekKategoriankent())
    Console.WriteLine($"  {kategoria}: {ertek:N0} Ft");

var legdragabb = keszlet.Legdragabb();
if (legdragabb != null)
    Console.WriteLine($"\nLegdrágább termék: {legdragabb.Nev} ({legdragabb.Ar:N0} Ft)");
