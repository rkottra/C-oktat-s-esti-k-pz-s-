using OregfakForce.Models;

using var db = new EstisOregfakContext();

// Az újragenerálás után a Fa osztálynak van Vedett (bool) és Megjegyzes (string?) tulajdonsága.
var vedettek = db.Fas
    .Where(f => f.Vedett)
    .OrderByDescending(f => f.Kormeret)
    .ToList();

Console.WriteLine($"Védett fák: {vedettek.Count}");
foreach (var f in vedettek)
    Console.WriteLine($"{f.Faj,-20} {f.Kormeret,6} cm  {f.Telepules}{(f.Ritka() ? "  (ritka)" : "")}");

// Fajonkénti csoportosítás a memóriában (a lista már betöltve)
Console.WriteLine("\nVédett fák fajonként:");
foreach (var g in vedettek.GroupBy(f => f.Faj).OrderByDescending(g => g.Count()))
    Console.WriteLine($"  {g.Key}: {string.Join(", ", g.Select(f => f.Telepules))}");

// Módosítás és mentés az új oszlopba
var legvastagabb = vedettek.First();
legvastagabb.Megjegyzes = "Az adatbázis legvastagabb fája.";
db.SaveChanges();
Console.WriteLine($"\nMegjegyzés mentve: {legvastagabb.Faj} ({legvastagabb.Telepules})");
