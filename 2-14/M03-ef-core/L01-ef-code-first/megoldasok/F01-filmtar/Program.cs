using Filmtar;
using Filmtar.Models;

using var db = new FilmtarContext();

if (!db.Filmek.Any())
{
    db.Filmek.AddRange(
        new Film { Cim = "Oppenheimer", Rendezo = "Christopher Nolan", Ev = 2023, Hossz = 180, Ertekeles = 8.3 },
        new Film { Cim = "Eredet", Rendezo = "Christopher Nolan", Ev = 2010, Hossz = 148, Ertekeles = 8.8 },
        new Film { Cim = "A Gyűrűk Ura: A király visszatér", Rendezo = "Peter Jackson", Ev = 2003, Hossz = 201, Ertekeles = 9.0 },
        new Film { Cim = "Saul fia", Rendezo = "Nemes Jeles László", Ev = 2015, Hossz = 107, Ertekeles = 7.4 },
        new Film { Cim = "Testről és lélekről", Rendezo = "Enyedi Ildikó", Ev = 2017, Hossz = 116 });
    db.SaveChanges();
}

Console.WriteLine("Filmek (legújabb elöl):");
foreach (var f in db.Filmek.OrderByDescending(f => f.Ev))
    Console.WriteLine($"  {f.Ev}  {f.Cim} – {f.Rendezo} ({f.Hossz} perc)");

var hosszuak = db.Filmek.Where(f => f.Hossz > 120).Select(f => f.Cim).ToList();
Console.WriteLine($"2 óránál hosszabb: {string.Join(", ", hosszuak)}");

Console.WriteLine($"Értékelés nélkül: {db.Filmek.Count(f => f.Ertekeles == null)} film");

Console.Write("Rendező: ");
string nev = Console.ReadLine() ?? "";
var filmjei = db.Filmek.Where(f => f.Rendezo.Contains(nev)).ToList();
if (filmjei.Count == 0)
    Console.WriteLine("  Nincs ilyen rendező az adatbázisban.");
foreach (var f in filmjei)
    Console.WriteLine($"  {f.Cim} ({f.Ev})");
