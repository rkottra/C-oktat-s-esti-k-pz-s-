using System.Text.Encodings.Web;
using System.Text.Json;
using PilotakJson;

// 1. feladat
List<Pilota> pilotak = Pilota.BetoltJson("pilots.json");
Console.WriteLine($"1. feladat: Beolvasott pilóták: {pilotak.Count}");

// 2. feladat: születések évtizedenként (csak az ismert dátumúak)
Console.WriteLine("2. feladat: Születések évtizedenként:");
var evtizedek = pilotak
    .Where(p => p.SzuletesiDatum != null)
    .GroupBy(p => p.SzuletesiDatum!.Value.Year / 10 * 10)
    .OrderBy(g => g.Key);
foreach (var csoport in evtizedek)
{
    Console.WriteLine($"\t{csoport.Key}–{csoport.Key + 9}: {csoport.Count(),3} fő");
}

// 3. feladat: szökőnapon (február 29.) születettek
var szokonaposok = pilotak
    .Where(p => p.SzuletesiDatum is { Month: 2, Day: 29 })
    .Select(p => p.Nev)
    .ToList();
Console.WriteLine(szokonaposok.Count == 0
    ? "3. feladat: Nincs szökőnapon született pilóta."
    : $"3. feladat: Szökőnapon született: {string.Join(", ", szokonaposok)}");

// 4. feladat: egy nemzetiség pilótái névsorban
Console.Write("4. feladat: Nemzetiség (angolul, pl. Hungarian): ");
string nemzet = (Console.ReadLine() ?? "").Trim();
var nemzetPilotai = pilotak
    .Where(p => p.Nemzetiseg.Equals(nemzet, StringComparison.OrdinalIgnoreCase))
    .OrderBy(p => p.Nev)
    .ToList();
if (nemzetPilotai.Count == 0)
{
    Console.WriteLine("\tNincs ilyen nemzetiségű pilóta.");
}
foreach (Pilota p in nemzetPilotai)
{
    Console.WriteLine($"\t{p.Nev} ({p.SzuletesiDatum?.ToString("yyyy.MM.dd") ?? "ismeretlen"})");
}

// 5. feladat: női pilóták mentése új JSON-fájlba
var nok = pilotak.Where(p => p.Nem == "F").OrderBy(p => p.SzuletesiDatum).ToList();
var beallitasok = new JsonSerializerOptions
{
    WriteIndented = true,                                  // olvasható, tördelt kimenet
    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping  // ékezetek ne é alakban kerüljenek ki
};
try
{
    File.WriteAllText("noi_pilotak.json", JsonSerializer.Serialize(nok, beallitasok));
    Console.WriteLine($"5. feladat: {nok.Count} női pilóta mentve a noi_pilotak.json fájlba.");
}
catch (Exception)
{
    Console.WriteLine("5. feladat: Hiba a fájl írásakor!");
}
