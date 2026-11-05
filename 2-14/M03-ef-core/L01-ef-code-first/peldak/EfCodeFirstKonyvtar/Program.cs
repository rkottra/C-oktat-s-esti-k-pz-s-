using EfCodeFirstKonyvtar;
using EfCodeFirstKonyvtar.Models;

using var db = new KonyvtarContext();

// --- CREATE: új sorok felvétele (csak ha még üres a tábla) ---
if (!db.Konyvek.Any())
{
    db.Konyvek.AddRange(
        new Konyv { Cim = "Egri csillagok", Szerzo = "Gárdonyi Géza", KiadasEve = 1901, Ar = 3490, Mufaj = "regény" },
        new Konyv { Cim = "A Pál utcai fiúk", Szerzo = "Molnár Ferenc", KiadasEve = 1907, Ar = 2990, Mufaj = "ifjúsági" },
        new Konyv { Cim = "Az ember tragédiája", Szerzo = "Madách Imre", KiadasEve = 1861, Ar = 2490 },
        new Konyv { Cim = "Tüskevár", Szerzo = "Fekete István", KiadasEve = 1957, Ar = 3190, Mufaj = "ifjúsági" });
    db.SaveChanges();   // csak itt kerül be ténylegesen az adatbázisba!
    Console.WriteLine("Mintaadatok felvéve.\n");
}

// --- READ: lekérdezés LINQ-kel (SQL SELECT-té fordul) ---
Console.WriteLine("Összes könyv kiadási év szerint:");
foreach (var k in db.Konyvek.OrderBy(k => k.KiadasEve))
    Console.WriteLine(k);

var ifjusagi = db.Konyvek.Where(k => k.Mufaj == "ifjúsági").Select(k => k.Cim).ToList();
Console.WriteLine($"\nIfjúsági regények: {string.Join(", ", ifjusagi)}");
Console.WriteLine($"Átlagár: {db.Konyvek.Average(k => k.Ar):F0} Ft");

// --- UPDATE: betöltött entitás módosítása, majd mentés ---
var tuskevar = db.Konyvek.FirstOrDefault(k => k.Cim == "Tüskevár");
if (tuskevar != null)
{
    tuskevar.Ar += 200;
    db.SaveChanges();
    Console.WriteLine($"\nÁremelés után: {tuskevar}");
}

// --- DELETE: entitás törlése ---
var torlendo = db.Konyvek.FirstOrDefault(k => k.Mufaj == null);
if (torlendo != null)
{
    db.Konyvek.Remove(torlendo);
    db.SaveChanges();
    Console.WriteLine($"Törölve: {torlendo.Cim}");
}

Console.WriteLine($"\nKönyvek száma az adatbázisban: {db.Konyvek.Count()}");
