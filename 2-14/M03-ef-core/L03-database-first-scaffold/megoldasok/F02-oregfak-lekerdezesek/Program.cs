using OregfakLekerdezesek.Data;

using var db = new OregfakContext();

// 1. Különböző fajok száma
Console.WriteLine($"Különböző fajok: {db.Fa.Select(f => f.Faj).Distinct().Count()}");

// 2. A legvastagabb fa
var legvastagabb = db.Fa.OrderByDescending(f => f.Kormeret).First();
Console.WriteLine($"Legvastagabb: {legvastagabb.Faj}, {legvastagabb.Kormeret} cm, " +
                  $"{legvastagabb.Telepules} ({legvastagabb.MeresEve})");

// 3. Fajonkénti átlag, csak a legalább 5 példánnyal rendelkező fajok (GROUP BY + HAVING)
Console.WriteLine("\nÁtlagos törzskerület (legalább 5 példány):");
var atlagok = db.Fa
    .GroupBy(f => f.Faj)
    .Where(g => g.Count() >= 5)
    .Select(g => new { Faj = g.Key, Db = g.Count(), Atlag = g.Average(f => f.Kormeret) })
    .OrderByDescending(x => x.Atlag)
    .ToList();
foreach (var x in atlagok)
    Console.WriteLine($"  {x.Faj,-20} {x.Db,3} db  {x.Atlag,4:F0} cm");

// 4. Mérési évenként a fák száma (a NULL évű fákat kihagyjuk)
Console.WriteLine("\nMérések évenként:");
var evek = db.Fa
    .Where(f => f.MeresEve != null)
    .GroupBy(f => f.MeresEve)
    .Select(g => new { Ev = g.Key, Db = g.Count() })
    .OrderBy(x => x.Ev)
    .ToList();
foreach (var x in evek)
    Console.WriteLine($"  {x.Ev}: {new string('*', x.Db)} {x.Db}");

// 5. Kategóriák: a Kategoria számított tulajdonság, ezért ELŐBB betöltjük (ToList), utána csoportosítunk
Console.WriteLine("\nKategóriák:");
var kategoriak = db.Fa.ToList()
    .GroupBy(f => f.Kategoria)
    .Select(g => $"{g.Key}: {g.Count()}");
Console.WriteLine("  " + string.Join(", ", kategoriak));

// 6. Bekért faj legvastagabb példánya
Console.Write("\nFaj: ");
string faj = Console.ReadLine() ?? "";
var peldany = db.Fa
    .Where(f => f.Faj == faj)
    .OrderByDescending(f => f.Kormeret)
    .FirstOrDefault();
Console.WriteLine(peldany == null
    ? "  Nincs ilyen faj az adatbázisban."
    : $"  A legvastagabb: {peldany.Telepules}, {peldany.Kormeret} cm");
