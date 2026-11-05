using Teglalapok;

var teglalapok = new List<Teglalap>
{
    new Teglalap(3, 4),
    new Teglalap(5),
    new Teglalap(2.5, 10),
    new Teglalap(7, 1.5),
    new Teglalap(4),
};

Console.WriteLine("Téglalapok:");
foreach (var t in teglalapok)
    Console.WriteLine($"  {t}");

int negyzetek = 0;
double osszTerulet = 0;
Teglalap legnagyobb = teglalapok[0];
foreach (var t in teglalapok)
{
    if (t.Negyzet) negyzetek++;
    osszTerulet += t.Terulet;
    if (t.Terulet > legnagyobb.Terulet) legnagyobb = t;
}
Console.WriteLine($"Négyzetek száma: {negyzetek}");
Console.WriteLine($"Összterület: {osszTerulet:F2}");
Console.WriteLine($"Legnagyobb területű: {legnagyobb}");

// Új téglalap a felhasználótól, hibakezeléssel
Console.Write("\nÚj téglalap szélessége: ");
string? szoveg1 = Console.ReadLine();
Console.Write("Új téglalap magassága: ");
string? szoveg2 = Console.ReadLine();
try
{
    var uj = new Teglalap(double.Parse(szoveg1 ?? ""), double.Parse(szoveg2 ?? ""));
    teglalapok.Add(uj);
    Console.WriteLine($"Felvéve: {uj}");
}
catch (FormatException)
{
    Console.WriteLine("Hiba: nem számot adtál meg.");
}
catch (ArgumentException ex)
{
    Console.WriteLine($"Hiba: {ex.Message}");
}
Console.WriteLine($"A listában {teglalapok.Count} téglalap van.");
