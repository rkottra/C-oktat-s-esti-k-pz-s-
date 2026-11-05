using WebshopRendelesek;

List<Rendeles> rendelesek = Rendeles.FajlbolBetolt("rendelesek.csv", out int hibas);
var jelentes = new List<string>();          // ide gyűjtjük a jelentes.txt sorait

// Segédfüggvény: egyszerre ír a konzolra és a jelentésbe
void Kiir(string szoveg)
{
    Console.WriteLine(szoveg);
    jelentes.Add(szoveg);
}

// 1.
int bevetel = rendelesek.Sum(r => r.Osszeg);
Kiir($"1. Rendelések: {rendelesek.Count} db (hibás sor: {hibas}), teljes bevétel: {bevetel:N0} Ft");

// 2.
Kiir($"2. Legnagyobb értékű rendelés: {rendelesek.MaxBy(r => r.Osszeg)}");

// 3.
var kategoriak = rendelesek.GroupBy(r => r.Kategoria)
                           .Select(cs => new
                           {
                               Kategoria = cs.Key,
                               Darab = cs.Sum(r => r.Darab),
                               Bevetel = cs.Sum(r => r.Osszeg),
                           })
                           .OrderByDescending(k => k.Bevetel)
                           .ToList();
Kiir("3. Kategóriánként:");
foreach (var k in kategoriak)
    Kiir($"   {k.Kategoria,-11} {k.Darab,3} db {k.Bevetel,10:N0} Ft ({100.0 * k.Bevetel / bevetel,4:F1}%)");

// 4.
Kiir("4. Havi bevétel:");
var havi = rendelesek.GroupBy(r => new DateTime(r.Datum.Year, r.Datum.Month, 1))   // a hónap első napja a kulcs
                     .OrderBy(cs => cs.Key);
foreach (var ho in havi)
    Kiir($"   {ho.Key,-17:yyyy. MMMM} {ho.Sum(r => r.Osszeg),10:N0} Ft ({ho.Count()} rendelés)");

// 5.
Kiir("5. A 3 legtöbbet eladott termék:");
var top3 = rendelesek.GroupBy(r => r.Termek)
                     .Select(cs => new { Termek = cs.Key, Darab = cs.Sum(r => r.Darab) })
                     .OrderByDescending(t => t.Darab)
                     .ThenBy(t => t.Termek)
                     .Take(3);
foreach (var t in top3)
    Kiir($"   {t.Termek}: {t.Darab} db");

// 6.
var nagyok = rendelesek.Where(r => r.Osszeg > 50000).ToList();
Kiir(nagyok.Count == 0
    ? "6. Nincs 50 000 Ft feletti rendelés."
    : $"6. 50 000 Ft feletti rendelések: {nagyok.Count} db, átlagértékük {nagyok.Average(r => r.Osszeg):N0} Ft");

// 7.
var legforgalmasabb = rendelesek.GroupBy(r => r.Datum)
                                .OrderByDescending(cs => cs.Count())
                                .ThenBy(cs => cs.Key)
                                .First();
Kiir($"7. A legtöbb rendelés napja: {legforgalmasabb.Key:yyyy.MM.dd.} ({legforgalmasabb.Count()} db)");

// 8. (nem kerül a jelentésbe)
Console.Write("8. Kategória: ");
string keresett = (Console.ReadLine() ?? "").Trim();
var termekek = rendelesek.Where(r => r.Kategoria.Equals(keresett, StringComparison.OrdinalIgnoreCase))
                         .Select(r => r.Termek)
                         .Distinct()
                         .OrderBy(t => t)
                         .ToList();
Console.WriteLine(termekek.Count == 0
    ? "   Nincs ilyen kategória."
    : $"   Termékek: {string.Join(", ", termekek)}");

// 9.
File.WriteAllLines("jelentes.txt", jelentes);
File.WriteAllLines("kategoriak.csv",
    kategoriak.Select(k => $"{k.Kategoria};{k.Darab};{k.Bevetel}").Prepend("kategoria;darab;bevetel"));
Console.WriteLine("9. jelentes.txt és kategoriak.csv kiírva.");
