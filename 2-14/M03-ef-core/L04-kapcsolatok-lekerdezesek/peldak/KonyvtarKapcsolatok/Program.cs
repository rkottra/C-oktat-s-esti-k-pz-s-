using KonyvtarKapcsolatok;
using KonyvtarKapcsolatok.Models;
using Microsoft.EntityFrameworkCore;   // az Include ebben a névtérben van

using var db = new KonyvtarContext();

// --- Felvétel navigáción keresztül: a könyveket a szerző listájába tesszük ---
// Elég a szerzőt hozzáadni; az EF Core a könyveket is beszúrja, és kitölti a SzerzoId-t.
if (!db.Szerzok.Any())
{
    db.Szerzok.AddRange(
        new Szerzo
        {
            Nev = "Gárdonyi Géza", SzuletesiEv = 1863,
            Konyvek =
            {
                new Konyv { Cim = "Egri csillagok", KiadasEve = 1901, Ar = 3490 },
                new Konyv { Cim = "A láthatatlan ember", KiadasEve = 1902, Ar = 2990 }
            }
        },
        new Szerzo
        {
            Nev = "Fekete István", SzuletesiEv = 1900,
            Konyvek =
            {
                new Konyv { Cim = "Tüskevár", KiadasEve = 1957, Ar = 3190 },
                new Konyv { Cim = "Vuk", KiadasEve = 1965, Ar = 2490 },
                new Konyv { Cim = "Kele", KiadasEve = 1955, Ar = 2690 }
            }
        },
        new Szerzo { Nev = "Szabó Magda", SzuletesiEv = 1917 });   // még nincs könyve
    db.SaveChanges();
    Console.WriteLine("Mintaadatok felvéve.\n");
}

// --- 1. Include: a könyvekkel együtt a szerzőjüket is betöltjük (SQL: JOIN) ---
Console.WriteLine("Könyvek szerzővel (Include):");
var konyvek = db.Konyvek
    .Include(k => k.Szerzo)
    .OrderBy(k => k.Szerzo.Nev).ThenBy(k => k.KiadasEve)
    .ToList();
foreach (var k in konyvek)
    Console.WriteLine($"  {k.Szerzo.Nev,-15} {k.Cim} ({k.KiadasEve})");

// --- 2. Fordított irány: szerzők a könyveikkel ---
Console.WriteLine("\nSzerzők és könyveik:");
foreach (var sz in db.Szerzok.Include(s => s.Konyvek).ToList())
{
    string lista = sz.Konyvek.Count == 0 ? "–" : string.Join(", ", sz.Konyvek.Select(k => k.Cim));
    Console.WriteLine($"  {sz.Nev} ({sz.Konyvek.Count} db): {lista}");
}

// --- 3. Navigáció a lekérdezésben: Select / Where esetén NEM kell Include ---
// Az EF Core a k.Szerzo.Nev-ből magától JOIN-t készít.
var regiek = db.Konyvek
    .Where(k => k.Szerzo.SzuletesiEv < 1900)
    .Select(k => new { k.Cim, Szerzo = k.Szerzo.Nev })
    .ToList();
Console.WriteLine($"\n1900 előtt született szerzők könyvei: {string.Join(", ", regiek.Select(x => x.Cim))}");

// --- 4. Összesítés a gyűjteményen át ---
var stat = db.Szerzok
    .Select(s => new { s.Nev, Db = s.Konyvek.Count, Osszar = s.Konyvek.Sum(k => k.Ar) })
    .OrderByDescending(x => x.Db)
    .ToList();
Console.WriteLine("\nSzerzőnként:");
foreach (var x in stat)
    Console.WriteLine($"  {x.Nev,-15} {x.Db} könyv, összesen {x.Osszar} Ft");

// --- 5. Any: van-e könyve? ---
var konyvNelkul = db.Szerzok.Where(s => !s.Konyvek.Any()).Select(s => s.Nev).ToList();
Console.WriteLine($"\nKönyv nélküli szerzők: {string.Join(", ", konyvNelkul)}");

// --- 6. Új könyv meglévő szerzőhöz: elég az idegen kulcsot megadni ---
var szabo = db.Szerzok.First(s => s.Nev == "Szabó Magda");
if (!db.Konyvek.Any(k => k.Cim == "Abigél"))
{
    db.Konyvek.Add(new Konyv { Cim = "Abigél", KiadasEve = 1970, Ar = 3990, SzerzoId = szabo.Id });
    db.SaveChanges();
    Console.WriteLine("Felvéve: Abigél (Szabó Magda)");
}

// --- 7. Gyakori hiba: Include nélkül a navigációs tulajdonság nincs betöltve ---
using var ujDb = new KonyvtarContext();          // friss context, üres memóriával
var elso = ujDb.Konyvek.First();
Console.WriteLine($"\nInclude nélkül a szerző betöltve? {(elso.Szerzo != null ? "igen" : "nem (null!)")}");
