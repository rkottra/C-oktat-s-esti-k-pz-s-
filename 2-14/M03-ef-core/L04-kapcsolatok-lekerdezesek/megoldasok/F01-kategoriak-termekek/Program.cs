using Microsoft.EntityFrameworkCore;
using Webshop;
using Webshop.Models;

using var db = new WebshopContext();

// 1. Mintaadatok: a termékeket a kategória Termekek listájába tesszük
if (!db.Kategoriak.Any())
{
    db.Kategoriak.AddRange(
        new Kategoria
        {
            Nev = "Elektronika",
            Termekek =
            {
                new Termek { Nev = "Fülhallgató", Ar = 12990, Keszlet = 15 },
                new Termek { Nev = "Egér", Ar = 5990, Keszlet = 40 },
                new Termek { Nev = "Billentyűzet", Ar = 14990, Keszlet = 0 }
            }
        },
        new Kategoria
        {
            Nev = "Könyv",
            Termekek =
            {
                new Termek { Nev = "C# kezdőknek", Ar = 8990, Keszlet = 12 },
                new Termek { Nev = "SQL zsebkönyv", Ar = 4990, Keszlet = 5 }
            }
        },
        new Kategoria
        {
            Nev = "Háztartás",
            Termekek =
            {
                new Termek { Nev = "Bögre", Ar = 1990, Keszlet = 100 },
                new Termek { Nev = "Teafőző", Ar = 9990, Keszlet = 3 },
                new Termek { Nev = "Kávédaráló", Ar = 7490, Keszlet = 0 }
            }
        },
        new Kategoria { Nev = "Játékok" });
    db.SaveChanges();
}

// 2. Termékek kategóriával (Include)
Console.WriteLine("Termékek:");
foreach (var t in db.Termekek.Include(t => t.Kategoria)
                             .OrderBy(t => t.Kategoria.Nev).ThenBy(t => t.Ar))
    Console.WriteLine($"  {t.Kategoria.Nev,-12} {t.Nev,-15} {t.Ar,6} Ft");

// 3. Kategóriánként darab és átlagár – az üres kategória is megjelenik
Console.WriteLine("\nKategóriák:");
var stat = db.Kategoriak
    .Select(k => new
    {
        k.Nev,
        Db = k.Termekek.Count,
        Atlag = k.Termekek.Average(t => (double?)t.Ar)   // üres kategóriánál null
    })
    .ToList();
foreach (var x in stat)
    Console.WriteLine($"  {x.Nev,-12} {x.Db} termék, átlagár: {(x.Atlag == null ? "–" : $"{x.Atlag:F0} Ft")}");

// 4. Kategóriák, amelyekben van elfogyott termék (Any a gyűjteményen)
var hianyos = db.Kategoriak
    .Where(k => k.Termekek.Any(t => t.Keszlet == 0))
    .Select(k => k.Nev)
    .ToList();
Console.WriteLine($"\nElfogyott termék van: {string.Join(", ", hianyos)}");

// 5. Kategóriánként a legdrágább termék
Console.WriteLine("\nLegdrágább kategóriánként:");
var kategoriak = db.Kategoriak.Include(k => k.Termekek).ToList();
foreach (var k in kategoriak.Where(k => k.Termekek.Count > 0))
{
    var max = k.Termekek.MaxBy(t => t.Ar)!;
    Console.WriteLine($"  {k.Nev,-12} {max.Nev} ({max.Ar} Ft)");
}

// 6. Új termék egy meglévő kategóriába – a navigációs listán keresztül
var jatekok = kategoriak.First(k => k.Nev == "Játékok");
if (jatekok.Termekek.Count == 0)
{
    jatekok.Termekek.Add(new Termek { Nev = "Társasjáték", Ar = 6990, Keszlet = 8 });
    db.SaveChanges();   // a KategoriaId-t az EF Core tölti ki
    Console.WriteLine("\nFelvéve: Társasjáték (Játékok)");
}
