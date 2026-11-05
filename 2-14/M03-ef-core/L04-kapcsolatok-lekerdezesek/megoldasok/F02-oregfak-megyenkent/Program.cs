using Microsoft.EntityFrameworkCore;
using OregfakMegyenkent.Models;

using var db = new EstisOregfakContext();

// 1. Vármegyénként a fák száma (gyűjtemény-navigáció: m.Fas)
Console.WriteLine("Fák száma vármegyénként:");
var darabok = db.Megyes
    .Select(m => new { m.Nev, Db = m.Fas.Count() })
    .OrderByDescending(x => x.Db).ThenBy(x => x.Nev)
    .ToList();
foreach (var x in darabok)
    Console.WriteLine($"  {x.Nev,-24} {x.Db,3}");

// 2. Minden vármegye legvastagabb fája – Include, majd a memóriában MaxBy
Console.WriteLine("\nA vármegyék legvastagabb fái:");
var megyek = db.Megyes.Include(m => m.Fas).OrderBy(m => m.Nev).ToList();
foreach (var m in megyek.Where(m => m.Fas.Count > 0))
{
    var fa = m.Fas.MaxBy(f => f.Kormeret)!;
    Console.WriteLine($"  {m.Nev,-24} {fa.Faj} ({fa.Telepules}, {fa.Kormeret} cm)");
}

// 3. Az országos átlagnál vastagabb átlagú vármegyék
double orszagos = db.Fas.Average(f => f.Kormeret);
var vastagabb = db.Megyes
    .Where(m => m.Fas.Any())
    .Select(m => new { m.Nev, Atlag = m.Fas.Average(f => f.Kormeret) })
    .Where(x => x.Atlag > orszagos)
    .OrderByDescending(x => x.Atlag)
    .ToList();
Console.WriteLine($"\nOrszágos átlag: {orszagos:F0} cm. Ennél vastagabb átlagú vármegyék:");
foreach (var x in vastagabb)
    Console.WriteLine($"  {x.Nev,-24} {x.Atlag:F0} cm");

// 4. Fajok, amelyek legalább 5 vármegyében előfordulnak (COUNT(DISTINCT megye_id))
var elterjedt = db.Fas
    .GroupBy(f => f.Faj)
    .Select(g => new { Faj = g.Key, Megyek = g.Select(f => f.MegyeId).Distinct().Count() })
    .Where(x => x.Megyek >= 5)
    .OrderByDescending(x => x.Megyek)
    .ToList();
Console.WriteLine("\nLegalább 5 vármegyében:");
foreach (var x in elterjedt)
    Console.WriteLine($"  {x.Faj,-20} {x.Megyek} vármegye");

// 5. Bekért faj: mely vármegyékben fordul elő? (navigáció a Select-ben → JOIN)
Console.Write("\nFaj: ");
string faj = Console.ReadLine() ?? "";
var hol = db.Fas
    .Where(f => f.Faj == faj)
    .Select(f => f.Megye.Nev)
    .Distinct()
    .OrderBy(n => n)
    .ToList();
Console.WriteLine(hol.Count == 0 ? "  Nincs ilyen faj." : "  " + string.Join(", ", hol));
