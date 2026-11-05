using Orszagok;

List<Orszag> orszagok = Orszag.FajlbolBetolt("orszagok.csv").ToList();

// 2.
Console.WriteLine($"2. Országok száma: {orszagok.Count}");

// 3.
Console.WriteLine("3. Földrészenként:");
var foldreszek = orszagok.GroupBy(o => o.Foldresz)
                         .Select(cs => new { Foldresz = cs.Key, Darab = cs.Count(), Nepesseg = cs.Sum(o => o.Nepesseg) })
                         .OrderByDescending(f => f.Darab);
foreach (var f in foldreszek)
    Console.WriteLine($"   {f.Foldresz,-8} {f.Darab,3} ország, {f.Nepesseg / 1000.0,8:F1} millió fő");

// 4.
Orszag legnagyobb = orszagok.MaxBy(o => o.Terulet)!;
Orszag legkisebb = orszagok.MinBy(o => o.Terulet)!;
Console.WriteLine($"4. Legnagyobb területű: {legnagyobb.Nev} ({legnagyobb.Terulet:N0} km²), legkisebb: {legkisebb.Nev} ({legkisebb.Terulet} km²)");

// 5.
Console.WriteLine($"5. M betűvel kezdődő nevű országok: {orszagok.Count(o => o.Nev.StartsWith("M"))}");

// 6.
double atlag = orszagok.Average(o => o.Terulet);
var nagyEuropaiak = orszagok.Where(o => o.Foldresz == "Európa" && o.Terulet > atlag)
                            .OrderBy(o => o.Nev)
                            .Select(o => o.Nev);
Console.WriteLine($"6. Átlagos terület: {atlag:N0} km²");
Console.WriteLine($"   Ennél nagyobb európai országok: {string.Join(", ", nagyEuropaiak)}");

// 7.
Console.WriteLine("7. A legsűrűbben lakott 5 ország:");
foreach (var o in orszagok.OrderByDescending(o => o.Nepsuruseg).Take(5))
    Console.WriteLine($"   {o.Nev,-16} {o.Nepsuruseg,8:N0} fő/km²");

// 8.
var milliardosok = orszagok.Where(o => o.Nepesseg > 1_000_000).Select(o => o.Nev).ToList();
Console.WriteLine(milliardosok.Any()
    ? $"8. 1 milliárdnál népesebb: {string.Join(", ", milliardosok)}"
    : "8. Nincs 1 milliárdnál népesebb ország.");

// 9.
int monarchiak = orszagok.Count(o => o.Allamforma.Contains("monarchia") || o.Allamforma.Contains("királyság"));
Console.WriteLine($"9. Monarchiák és királyságok: {monarchiak} db; különböző államformák: {orszagok.Select(o => o.Allamforma).Distinct().Count()}");

// 10.
Console.Write("10. Főváros: ");
string fovaros = (Console.ReadLine() ?? "").Trim();
Orszag? talalt = orszagok.FirstOrDefault(o => o.Fovaros.Equals(fovaros, StringComparison.OrdinalIgnoreCase));
Console.WriteLine(talalt == null ? "   Nincs ilyen főváros az adatok között." : $"   {talalt.Fovaros} → {talalt.Nev}");

// 11.
var europaSorok = orszagok.Where(o => o.Foldresz == "Európa")
                          .OrderByDescending(o => o.Nepesseg)
                          .Select(o => $"{o.Nev};{o.Fovaros};{o.Nepesseg}")
                          .Prepend("nev;fovaros;nepesseg_ezer_fo")      // fejléc a lista elejére
                          .ToList();
File.WriteAllLines("europa.csv", europaSorok);
Console.WriteLine($"11. europa.csv kiírva ({europaSorok.Count - 1} ország).");
