using VilagbajnokokKonzol;

// 2. feladat: beolvasás
List<Vilagbajnok> bajnokok = Vilagbajnok.BetoltCsv("vilagbajnokok.csv");

// 3. feladat: egyszerű megszámolás
Console.WriteLine($"3. feladat: Szezonok száma az állományban: {bajnokok.Count}");

// 4. feladat: feltételes megszámolás és kiválasztás
int nemetek = bajnokok.Count(b => b.Nemzetiseg == "német");
Console.WriteLine($"4. feladat: Német bajnoki címek száma: {nemetek}");

// 5. feladat: csoportosítás – ki nyerte a legtöbb címet?
var legtobb = bajnokok
    .GroupBy(b => b.Nev)
    .Select(g => new { Nev = g.Key, Cimek = g.Count() })
    .OrderByDescending(x => x.Cimek)
    .First();
Console.WriteLine($"5. feladat: Legtöbb cím: {legtobb.Nev} ({legtobb.Cimek} db)");

// 6. feladat: statisztika csapatonként, csökkenő sorrendben
Console.WriteLine("6. feladat: Címek csapatonként:");
foreach (var csoport in bajnokok.GroupBy(b => b.Csapat).OrderByDescending(g => g.Count()).ThenBy(g => g.Key))
{
    Console.WriteLine($"\t{csoport.Key,-10} {csoport.Count(),2} db");
}

// 7. feladat: bekérés és keresés – "nincs találat" ágat is kezelni kell!
Console.Write("7. feladat: Adjon meg egy évet: ");
string bemenet = Console.ReadLine() ?? "";
if (int.TryParse(bemenet, out int ev))
{
    Vilagbajnok? talalat = bajnokok.FirstOrDefault(b => b.Ev == ev);
    Console.WriteLine(talalat is null
        ? $"\tNincs adat a(z) {ev}. évről."
        : $"\tA(z) {ev}. év bajnoka: {talalat.Nev}");
}
else
{
    Console.WriteLine("\tNem évszámot adott meg.");
}

// 8. feladat: fájlba írás csoportosítva, hibakezeléssel
try
{
    // a using gondoskodik a fájl lezárásáról (Flush + Close)
    using StreamWriter iro = new StreamWriter("bajnokok_nemzetenkent.txt");
    foreach (var csoport in bajnokok.GroupBy(b => b.Nemzetiseg).OrderBy(g => g.Key))
    {
        iro.WriteLine($"{csoport.Key} ({csoport.Count()} cím):");
        foreach (Vilagbajnok b in csoport.OrderBy(b => b.Ev))
        {
            iro.WriteLine($"\t{b}");
        }
    }
    Console.WriteLine("8. feladat: A bajnokok_nemzetenkent.txt elkészült.");
}
catch (IOException hiba)
{
    Console.WriteLine($"8. feladat: Hiba a fájl írásakor: {hiba.Message}");
}
