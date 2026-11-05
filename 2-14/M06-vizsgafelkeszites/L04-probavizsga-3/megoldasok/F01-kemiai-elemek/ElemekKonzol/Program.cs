using System.Globalization;
using ElemekKonzol;

var magyar = StringComparer.Create(new CultureInfo("hu-HU"), ignoreCase: true);

// 2. feladat
List<Elem> elemek = Elem.BetoltCsv("elemek.csv");

// 3. feladat
Console.WriteLine($"3. feladat: Elemek száma: {elemek.Count}");

// 4. feladat
List<Elem> gazok = elemek.Where(e => e.Halmazallapot == "gáz").ToList();
Console.WriteLine($"4. feladat: Gázhalmazállapotú elemek ({gazok.Count} db): {string.Join(", ", gazok.Select(e => e.Vegyjel))}");

// 5. feladat
Elem legnagyobb = elemek
    .Where(e => e.Elektronegativitas != null)
    .OrderByDescending(e => e.Elektronegativitas)
    .First();
Console.WriteLine("5. feladat:");
Console.WriteLine($"\tLegnagyobb elektronegativitás: {legnagyobb.Nev} ({legnagyobb.Vegyjel}): {legnagyobb.Elektronegativitas:F2}");
Console.WriteLine($"\tElektronegativitás nélkül: {elemek.Count(e => e.Elektronegativitas == null)} elem");

// 6. feladat
Console.WriteLine("6. feladat: Kategóriák:");
foreach (var g in elemek.GroupBy(e => e.Kategoria).OrderBy(g => g.Key, magyar))
{
    Console.WriteLine($"\t{g.Key,-14}{g.Count(),3} elem, átlagos atomtömeg: {g.Average(e => e.Atomtomeg):F2}");
}

// 7. feladat
Console.Write("7. feladat: Adjon meg egy vegyjelet: ");
string bemenet = (Console.ReadLine() ?? "").Trim();
// Kis- és nagybetűt nem különböztetünk meg: "fe", "FE", "Fe" egyaránt a vasat jelenti
string vegyjel = bemenet.Length == 0 ? "" : char.ToUpper(bemenet[0]) + bemenet[1..].ToLower();
if (!Elem.ErvenyesVegyjel(vegyjel) || !vegyjel.All(char.IsLetter))
{
    Console.WriteLine("\tÉrvénytelen vegyjel.");
}
else
{
    Elem? talalat = elemek.FirstOrDefault(e => e.Vegyjel == vegyjel);
    Console.WriteLine(talalat is null
        ? "\tNincs ilyen vegyjelű elem."
        : $"\t{talalat.Rendszam}. {talalat.Nev} – {talalat.Kategoria}, {talalat.Periodus}. periódus, {talalat.Csoport}. csoport");
}

// 8. feladat
try
{
    using StreamWriter iro = new StreamWriter("periodusok.txt");
    foreach (var g in elemek.GroupBy(e => e.Periodus).OrderBy(g => g.Key))
    {
        string jelek = string.Join(" ", g.OrderBy(e => e.Rendszam).Select(e => e.Vegyjel));
        iro.WriteLine($"{g.Key}. periódus ({g.Count()} elem): {jelek}");
    }
    iro.WriteLine($"Összesen: {elemek.Count} elem");
    Console.WriteLine("8. feladat: A periodusok.txt elkészült.");
}
catch (Exception)
{
    Console.WriteLine("8. feladat: Hiba a fájl írásakor!");
}
