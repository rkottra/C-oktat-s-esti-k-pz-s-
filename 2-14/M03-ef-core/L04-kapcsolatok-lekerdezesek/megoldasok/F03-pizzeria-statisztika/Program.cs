using Microsoft.EntityFrameworkCore;
using PizzeriaStatisztika.Models;

using var db = new EstisPizzeriaContext();

// 1. Az első 3 rendelés részletesen: több szintű betöltés
//    Rendeles → Vevo, Rendeles → Futar, Rendeles → Tetel → Pizza (ThenInclude)
Console.WriteLine("Az első 3 rendelés:");
var rendelesek = db.Rendeles
    .Include(r => r.Vevo)
    .Include(r => r.Futar)
    .Include(r => r.Tetel).ThenInclude(t => t.Pizza)
    .OrderBy(r => r.Idopont)
    .Take(3)
    .ToList();
foreach (var r in rendelesek)
{
    int ertek = r.Tetel.Sum(t => t.Db * t.Pizza.Ar);
    Console.WriteLine($"  #{r.Id} {r.Idopont:MM.dd. HH:mm}  {r.Vevo.Nev} ← {r.Futar.Nev}, {ertek} Ft");
    foreach (var t in r.Tetel)
        Console.WriteLine($"       {t.Db} × {t.Pizza.Nev}");
}

// 2. Vevőnként az elköltött összeg – SelectMany a rendelésekről a tételekre, SQL-ben összegezve
Console.WriteLine("\nVevők költése:");
var koltes = db.Vevo
    .Select(v => new
    {
        v.Nev,
        Osszeg = v.Rendeles.SelectMany(r => r.Tetel).Sum(t => t.Db * t.Pizza.Ar)
    })
    .OrderByDescending(x => x.Osszeg)
    .ToList();
foreach (var x in koltes)
    Console.WriteLine($"  {x.Nev,-16} {x.Osszeg,6} Ft");

// 3. Futáronként a szállítások és a kivitt pizzák száma
Console.WriteLine("\nFutárok:");
var futarok = db.Futar
    .Select(f => new
    {
        f.Nev,
        Szallitas = f.Rendeles.Count,
        Pizzak = f.Rendeles.SelectMany(r => r.Tetel).Sum(t => t.Db)
    })
    .OrderByDescending(x => x.Szallitas)
    .ToList();
foreach (var x in futarok)
    Console.WriteLine($"  {x.Nev,-16} {x.Szallitas} szállítás, {x.Pizzak} pizza");

// 4. Népszerűségi sorrend és a soha nem rendelt pizzák
Console.WriteLine("\nNépszerűség:");
var nepszeru = db.Pizza
    .Where(p => p.Tetel.Any())
    .Select(p => new { p.Nev, Db = p.Tetel.Sum(t => t.Db) })
    .OrderByDescending(x => x.Db).ThenBy(x => x.Nev)
    .ToList();
foreach (var x in nepszeru)
    Console.WriteLine($"  {x.Nev,-12} {x.Db,3} db");
var sosem = db.Pizza.Where(p => !p.Tetel.Any()).Select(p => p.Nev).ToList();
Console.WriteLine($"Még senki nem rendelte: {string.Join(", ", sosem)}");

// 5. Napi bevétel a tételek felől indulva (navigáció a GroupBy kulcsában)
Console.WriteLine("\nNapi bevétel:");
var napok = db.Tetel
    .GroupBy(t => t.Rendeles.Idopont.Date)
    .Select(g => new { Nap = g.Key, Bevetel = g.Sum(t => t.Db * t.Pizza.Ar) })
    .OrderBy(x => x.Nap)
    .ToList();
foreach (var x in napok)
    Console.WriteLine($"  {x.Nap:yyyy.MM.dd.}  {x.Bevetel,6} Ft");
var legjobb = napok.MaxBy(x => x.Bevetel)!;
Console.WriteLine($"A legjobb nap: {legjobb.Nap:yyyy.MM.dd.} ({legjobb.Bevetel} Ft)");

// 6. Bekért vevő kedvenc pizzája
Console.Write("\nVevő neve: ");
string nev = Console.ReadLine() ?? "";
var kedvenc = db.Tetel
    .Where(t => t.Rendeles.Vevo.Nev == nev)
    .GroupBy(t => t.Pizza.Nev)
    .Select(g => new { Pizza = g.Key, Db = g.Sum(t => t.Db) })
    .OrderByDescending(x => x.Db)
    .FirstOrDefault();
Console.WriteLine(kedvenc == null
    ? "  Nincs ilyen vevő, vagy még nem rendelt."
    : $"  Kedvence: {kedvenc.Pizza} ({kedvenc.Db} db)");
