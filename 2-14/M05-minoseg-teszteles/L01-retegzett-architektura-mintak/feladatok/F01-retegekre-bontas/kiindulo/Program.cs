// FIGYELEM: ez a program szándékosan "egy tömbben" készült – minden egy helyen van.
// Működik, de nehéz módosítani, újrahasznosítani és tesztelni. A feladat: bontsd rétegekre!

var nevek = new List<string>();
var kategoriak = new List<string>();
var arak = new List<int>();
var keszletek = new List<int>();

foreach (var sor in File.ReadAllLines("termekek.csv").Skip(1))
{
    var r = sor.Split(';');
    if (r.Length != 4 || !int.TryParse(r[2], out int ar) || !int.TryParse(r[3], out int db))
    {
        Console.WriteLine($"Hibás sor: {sor}");
        continue;
    }
    nevek.Add(r[0]);
    kategoriak.Add(r[1]);
    arak.Add(ar);
    keszletek.Add(db);
}

Console.WriteLine($"Beolvasott termékek: {nevek.Count}");

Console.WriteLine("\nUtánrendelendő (5 db alatt):");
for (int i = 0; i < nevek.Count; i++)
{
    if (keszletek[i] < 5)
        Console.WriteLine($"  {nevek[i]} – {keszletek[i]} db");
}

Console.WriteLine("\nKészletérték kategóriánként:");
var kat = kategoriak.Distinct().OrderBy(k => k).ToList();
foreach (var k in kat)
{
    long ossz = 0;
    for (int i = 0; i < nevek.Count; i++)
        if (kategoriak[i] == k)
            ossz += (long)arak[i] * keszletek[i];
    Console.WriteLine($"  {k}: {ossz:N0} Ft");
}

int max = 0;
for (int i = 1; i < nevek.Count; i++)
    if (arak[i] > arak[max]) max = i;
Console.WriteLine($"\nLegdrágább termék: {nevek[max]} ({arak[max]:N0} Ft)");

Console.Write("\nKategória: ");
string be = Console.ReadLine() ?? "";
bool volt = false;
for (int i = 0; i < nevek.Count; i++)
{
    if (kategoriak[i].Equals(be, StringComparison.OrdinalIgnoreCase))
    {
        Console.WriteLine($"  {nevek[i]}: {arak[i]:N0} Ft, {keszletek[i]} db");
        volt = true;
    }
}
if (!volt) Console.WriteLine("  Nincs ilyen kategória.");
