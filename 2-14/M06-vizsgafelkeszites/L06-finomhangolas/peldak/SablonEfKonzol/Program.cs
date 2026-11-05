using Microsoft.EntityFrameworkCore;
using SablonEfKonzol;
using SablonEfKonzol.Models;

// SABLON – első próba az adatbázis-kapcsolatra. Vizsgán a WPF-projekt elején is érdemes
// egy ilyen gyors ellenőrzést futtatni, mielőtt a felületet építed.
try
{
    using var db = new SablonContext();

    // Lekérdezés kapcsolt adattal: Include nélkül a Szulo null lenne
    var elsoTiz = db.Gyerekek
        .Include(g => g.Szulo)
        .OrderBy(g => g.Nev)
        .Take(10)
        .ToList();

    Console.WriteLine($"Szülő rekordok: {db.Szulok.Count()}, gyerek rekordok: {db.Gyerekek.Count()}");
    foreach (Gyerek g in elsoTiz)
    {
        Console.WriteLine($"\t{g.Nev} – {g.Szulo?.Nev ?? "?"}");
    }

    // Csoportosítás az adatbázisban: szülőnként a gyerekek száma
    var stat = db.Szulok
        .Select(s => new { s.Nev, Darab = s.Gyerekek.Count })
        .OrderByDescending(x => x.Darab)
        .ToList();
    stat.ForEach(x => Console.WriteLine($"\t{x.Nev}: {x.Darab}"));

    // Felvétel / törlés váz:
    // db.Gyerekek.Add(new Gyerek { Nev = "…", SzuloId = 1 });
    // db.Gyerekek.Remove(valamelyik);
    // db.SaveChanges();   ← enélkül semmi nem kerül az adatbázisba!
}
catch (Exception hiba)
{
    // A leggyakoribb okok: nem fut a MySQL, rossz adatbázisnév/jelszó, eltérő tábla- vagy oszlopnév
    Console.WriteLine("Adatbázis-hiba: " + hiba.Message);
    Console.WriteLine("Ellenőrizd: XAMPP → MySQL fut? Importáltad az SQL-t? Jó a kapcsolati szöveg és a [Table]/[Column] nevek?");
}
