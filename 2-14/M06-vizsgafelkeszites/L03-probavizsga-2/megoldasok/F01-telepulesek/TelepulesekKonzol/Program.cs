using System.Globalization;
using TelepulesekKonzol;

// Magyar ábécérend (pl. "Cs" a "C" után, az ékezetes betűk a helyükön)
StringComparer magyar = StringComparer.Create(new CultureInfo("hu-HU"), ignoreCase: true);

// 2. feladat – a CSV és a JSON betöltő ugyanazt adja
List<Telepules> telepulesek = Telepules.BetoltCsv("settlements.csv");
// List<Telepules> telepulesek = Telepules.BetoltJson("settlements.json");

// 3. feladat
Console.WriteLine($"3. feladat: Városok száma: {telepulesek.Count}");

// 4. feladat
var megyek = telepulesek
    .GroupBy(t => t.Megye)
    .Select(g => new
    {
        Megye = g.Key,
        Lakossag = g.Sum(t => t.Nepesseg),
        Terulet = g.Sum(t => t.Terulet),
        Jarasok = g.Select(t => t.Jaras).Where(j => j != "").Distinct().Count(),   // Budapestnek nincs járása
        Varosok = g.ToList()
    })
    .ToList();
var legkevesebb = megyek.OrderBy(m => m.Lakossag).First();
Console.WriteLine("4. feladat: A városokban legkevesebben élő megye:");
Console.WriteLine($"\t{legkevesebb.Megye}: {legkevesebb.Lakossag} fő, összterület: {legkevesebb.Terulet:F2} km²");

// 5. feladat
Telepules legsurubb = telepulesek.OrderByDescending(t => t.Nepsuruseg).First();
Console.WriteLine($"5. feladat: Legnagyobb népsűrűség: {legsurubb.Nev} ({legsurubb.Nepsuruseg:F0} fő/km²)");

// 6. feladat
Console.Write("6. feladat: Adja meg egy város nevét: ");
string keresett = (Console.ReadLine() ?? "").Trim();
Telepules? varos = telepulesek.FirstOrDefault(t => magyar.Equals(t.Nev, keresett));
if (varos is null)
{
    Console.WriteLine("\tNincs ilyen város az állományban.");
}
else
{
    string jaras = varos.Jaras == "" ? "–" : varos.Jaras;
    Console.WriteLine($"\tMegye: {varos.Megye}, járás: {jaras}, népesség: {varos.Nepesseg} fő, népsűrűség: {varos.Nepsuruseg:F0} fő/km²");
}

// 7. feladat
try
{
    using StreamWriter iro = new StreamWriter("telepulesek_megyenkent.txt");
    foreach (var m in megyek.OrderBy(m => m.Megye, magyar))
    {
        iro.WriteLine($"{m.Megye} – járások száma: {m.Jarasok}");
        foreach (Telepules t in m.Varosok.OrderBy(t => t.Nev, magyar))
        {
            iro.WriteLine($"\t{t.Nev}");
        }
    }
    Console.WriteLine("7. feladat: A telepulesek_megyenkent.txt fájl elkészült.");
}
catch (Exception)
{
    Console.WriteLine("7. feladat: Hiba a fájl írása során!");
}
