using Olimpiak;

// 1. feladat
List<Olimpia> olimpiak = Olimpia.BetoltCsv("olimpiak.csv");

// 2. feladat
Console.WriteLine($"2. feladat: Nyári olimpiák száma: {olimpiak.Count}");

// 3. feladat
Console.WriteLine("3. feladat: Rendezések kontinensenként:");
foreach (var (kontinens, darab) in OlimpiaStatisztika.KontinensenkentiDarab(olimpiak))
{
    Console.WriteLine($"\t{kontinens}: {darab}");
}

// 4. feladat
var varosok = OlimpiaStatisztika.TobbszorRendezoVarosok(olimpiak)
    .Select(v => $"{v.Varos} ({v.Darab})");
Console.WriteLine($"4. feladat: Többször rendező városok: {string.Join(", ", varosok)}");

// 5. feladat
foreach (Olimpia o in OlimpiaStatisztika.HalasztottJatekok(olimpiak))
{
    Console.WriteLine($"5. feladat: A {o.Ev}. évi játékokat {o.MegrendezesEve}-ben rendezték ({o.Varos}).");
}

// 6. feladat
Console.Write("6. feladat: Adjon meg egy országot: ");
string orszag = Console.ReadLine() ?? "";
List<int> evek = OlimpiaStatisztika.RendezesiEvek(olimpiak, orszag);
Console.WriteLine(evek.Count == 0
    ? "\tEz az ország még nem rendezett nyári olimpiát."
    : $"\t{evek.Count} alkalommal: {string.Join(", ", evek)}");
