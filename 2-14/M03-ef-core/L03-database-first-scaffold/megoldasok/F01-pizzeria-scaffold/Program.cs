// Válaszok a 4. pont kérdéseire:
// - 6 osztály: Futar, Pizza, Rendeles, Tetel, Vevo és a context: EstisPizzeriaContext
//   (a név az adatbázis nevéből képződik: estis_pizzeria → EstisPizzeria + Context).
// - A Tetel-nek nincs Id-ja: összetett kulcsa van, az OnModelCreating-ben:
//   entity.HasKey(e => new { e.RendelesId, e.PizzaId })
// - rendeles.vevo_id → Rendeles.VevoId (int); magára a vevőre a Rendeles.Vevo navigációs tulajdonság hivatkozik.
// - entity.HasIndex(e => e.Telefon, "uq_futar_telefon").IsUnique();
using PizzeriaScaffold.Models;

using var db = new EstisPizzeriaContext();

// -NoPluralize miatt a DbSet-ek neve megegyezik az osztályéval: db.Pizza, db.Futar, db.Rendeles
Console.WriteLine("Pizzák ár szerint:");
foreach (var p in db.Pizza.OrderBy(p => p.Ar))
    Console.WriteLine($"  {p.Nev,-12} {p.Ar,5} Ft");

Console.WriteLine($"Átlagár: {db.Pizza.Average(p => p.Ar):F0} Ft");

var olcsok = db.Pizza.Where(p => p.Ar < 2800).Select(p => p.Nev).ToList();
Console.WriteLine($"2800 Ft alatt: {string.Join(", ", olcsok)}");

Console.WriteLine("\nFutárok:");
foreach (var f in db.Futar.OrderBy(f => f.Nev))
    Console.WriteLine($"  {f.Nev,-16} {f.Telefon}");

Console.WriteLine($"\nRendelések száma: {db.Rendeles.Count()}");
Console.WriteLine($"Első rendelés:    {db.Rendeles.Min(r => r.Idopont):yyyy.MM.dd. HH:mm}");
Console.WriteLine($"Utolsó rendelés:  {db.Rendeles.Max(r => r.Idopont):yyyy.MM.dd. HH:mm}");
// A DateTime.Hour is SQL-lé fordul (HOUR(idopont))
Console.WriteLine($"Délelőtti rendelések: {db.Rendeles.Count(r => r.Idopont.Hour < 12)}");

// Felvétel a generált osztállyal – ugyanúgy, mint Code Firstnél
if (!db.Pizza.Any(p => p.Nev == "Bolognai"))
{
    db.Pizza.Add(new Pizza { Nev = "Bolognai", Ar = 2990 });
    db.SaveChanges();
    Console.WriteLine("\nÚj pizza felvéve: Bolognai");
}
