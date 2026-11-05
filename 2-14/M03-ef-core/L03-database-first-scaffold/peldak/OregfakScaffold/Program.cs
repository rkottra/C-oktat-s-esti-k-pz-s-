using OregfakScaffold.Models;

// A context osztályt és az entitásokat a Scaffold-DbContext generálta (Models mappa).
// Használatuk pontosan olyan, mint Code Firstnél – csak migráció nincs: az adatbázis már létezik.
using var db = new EstisOregfakContext();

Console.WriteLine($"Vármegyék: {db.Megyes.Count()}, nyilvántartott fák: {db.Fas.Count()}");

// --- Rendezés, Take: a legnagyobb törzskerületű fák ---
Console.WriteLine("\nAz 5 legvastagabb fa:");
foreach (var fa in db.Fas.OrderByDescending(f => f.Kormeret).Take(5))
    Console.WriteLine($"  {fa}");        // a ToString-et mi írtuk (Kiegeszitesek/FaKiegeszites.cs)

// --- Összesítés ---
Console.WriteLine($"\nÁtlagos törzskerület: {db.Fas.Average(f => f.Kormeret):F0} cm");
Console.WriteLine($"2010-ben vagy később mért fák: {db.Fas.Count(f => f.MeresEve >= 2010)}");

// --- Csoportosítás: a leggyakoribb fajok ---
// A GroupBy + Select is SQL-lé fordul (GROUP BY faj).
var fajok = db.Fas
    .GroupBy(f => f.Faj)
    .Select(g => new { Faj = g.Key, Darab = g.Count() })
    .OrderByDescending(x => x.Darab)
    .Take(5)
    .ToList();

Console.WriteLine("\nLeggyakoribb fajok:");
foreach (var x in fajok)
    Console.WriteLine($"  {x.Faj,-22} {x.Darab,3} db");

// --- Keresés bekért szövegre ---
Console.Write("\nTelepülés (vagy részlete): ");
string keresett = Console.ReadLine() ?? "";
var talalatok = db.Fas
    .Where(f => f.Telepules.Contains(keresett))
    .OrderBy(f => f.Telepules)
    .ToList();                           // ToList után már a memóriában dolgozunk

if (talalatok.Count == 0)
    Console.WriteLine("  Nincs ilyen település az adatbázisban.");
foreach (var fa in talalatok)
    Console.WriteLine($"  {fa}  – {fa.KormeretMeterben:F2} m");

// A felvétel, módosítás, törlés (Add, Remove, SaveChanges) is ugyanúgy működik, mint Code Firstnél.
