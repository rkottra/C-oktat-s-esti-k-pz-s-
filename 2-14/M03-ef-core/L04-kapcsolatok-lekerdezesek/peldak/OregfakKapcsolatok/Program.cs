using Microsoft.EntityFrameworkCore;
using OregfakKapcsolatok.Models;

// A Models mappa a 14-M03-L03 példájával azonos módon, Scaffold-DbContext-tel készült.
// A scaffold a megye_id idegen kulcsból mindkét navigációs tulajdonságot legenerálta:
//   Fa.Megye  (hivatkozás)   és   Megye.Fas  (gyűjtemény)
using var db = new EstisOregfakContext();

// --- 1. Include: fák a vármegyéjükkel ---
Console.WriteLine("A 10 legvastagabb fa:");
var legvastagabbak = db.Fas
    .Include(f => f.Megye)
    .OrderByDescending(f => f.Kormeret)
    .Take(10)
    .ToList();
foreach (var f in legvastagabbak)
    Console.WriteLine($"  {f.Kormeret,5} cm  {f.Faj,-20} {f.Telepules} ({f.Megye.Nev})");

// --- 2. Szűrés a kapcsolt tábla mezőjére (Include nem kell, a Where-ből JOIN lesz) ---
var zalaiak = db.Fas.Count(f => f.Megye.Nev == "Zala");
Console.WriteLine($"\nZala vármegyei fák: {zalaiak}");

// --- 3. Csoportosítás a "1" oldal szerint: vármegyénként a fák száma ---
// A gyűjtemény-navigációval (m.Fas) sokszor egyszerűbb, mint a GroupBy.
Console.WriteLine("\nVármegyénként:");
var megyenkent = db.Megyes
    .Select(m => new
    {
        m.Nev,
        Db = m.Fas.Count(),
        Legvastagabb = m.Fas.Max(f => (int?)f.Kormeret)   // int?: üres megyénél null, nem kivétel
    })
    .OrderByDescending(x => x.Db)
    .ToList();
foreach (var x in megyenkent)
    Console.WriteLine($"  {x.Nev,-24} {x.Db,3} fa, legvastagabb: {x.Legvastagabb} cm");

// --- 4. Ugyanez GroupBy-jal, a "N" oldalról indulva ---
var top3 = db.Fas
    .GroupBy(f => f.Megye.Nev)
    .Select(g => new { Megye = g.Key, Db = g.Count() })
    .OrderByDescending(x => x.Db)
    .Take(3)
    .ToList();
Console.WriteLine($"\nTop 3 vármegye: {string.Join(", ", top3.Select(x => $"{x.Megye} ({x.Db})"))}");

// --- 5. Egy vármegye a fáival (Include a gyűjteményre) ---
Console.Write("\nVármegye neve: ");
string nev = Console.ReadLine() ?? "";
var megye = db.Megyes
    .Include(m => m.Fas.OrderByDescending(f => f.Kormeret))   // szűrt/rendezett Include (EF Core 5+)
    .FirstOrDefault(m => m.Nev == nev);

if (megye == null)
    Console.WriteLine("  Nincs ilyen vármegye.");
else
    foreach (var f in megye.Fas)
        Console.WriteLine($"  {f.Faj,-20} {f.Kormeret,5} cm  {f.Telepules}");
