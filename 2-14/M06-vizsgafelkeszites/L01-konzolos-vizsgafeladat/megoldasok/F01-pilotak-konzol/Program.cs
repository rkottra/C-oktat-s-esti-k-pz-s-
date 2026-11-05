using PilotakKonzol;

// 2. feladat
List<Pilota> pilotak = Pilota.BetoltCsv("pilots.csv");

// 3. feladat
Console.WriteLine($"3. feladat: Pilóták száma: {pilotak.Count}");

// 4. feladat
List<Pilota> nok = pilotak.Where(p => p.Nem == 'F').ToList();
Console.WriteLine($"4. feladat: Női pilóták ({nok.Count} fő): {string.Join(", ", nok.Select(p => p.Nev))}");

// 5. feladat
Pilota legidosebb = pilotak
    .Where(p => p.SzuletesiDatum != null)
    .OrderBy(p => p.SzuletesiDatum)
    .First();
int ismeretlen = pilotak.Count(p => p.SzuletesiDatum == null);
Console.WriteLine("5. feladat:");
Console.WriteLine($"\tLegidősebb pilóta: {legidosebb.Nev} ({legidosebb.DatumSzoveg}, {legidosebb.Nemzetiseg})");
Console.WriteLine($"\tIsmeretlen születési dátum: {ismeretlen} fő");

// 6. feladat
Console.Write("6. feladat: Adjon meg egy névrészletet: ");
string reszlet = (Console.ReadLine() ?? "").Trim();
List<Pilota> talalatok = reszlet == ""
    ? new List<Pilota>()
    : pilotak.Where(p => p.NevReszletEgyezik(reszlet)).ToList();
if (talalatok.Count == 0)
{
    Console.WriteLine($"\tNincs a(z) \"{reszlet}\" névrészlettel kezdődő nevű pilóta.");
}
foreach (Pilota p in talalatok)
{
    Console.WriteLine($"\t{p.Id}\t{p.Nev}\t{p.DatumSzoveg}\t{p.Nem}\t{p.Nemzetiseg}");
}

// 7. feladat
Console.WriteLine("7. feladat: Az 5 legtöbb pilótát adó nemzet:");
var top5 = pilotak
    .GroupBy(p => p.Nemzetiseg)
    .OrderByDescending(g => g.Count())
    .ThenBy(g => g.Key)
    .Take(5);
foreach (var csoport in top5)
{
    Console.WriteLine($"\t{csoport.Key}: {csoport.Count()} fő");
}

// 8. feladat
Console.Write("8. feladat: Adja meg a nemzetiség kezdőbetűjét: ");
string betu = (Console.ReadLine() ?? "").Trim();
try
{
    using StreamWriter iro = new StreamWriter("pilota_csoportok.txt");
    var csoportok = pilotak
        .Where(p => betu != "" && p.Nemzetiseg.StartsWith(betu, StringComparison.OrdinalIgnoreCase))
        .GroupBy(p => p.Nemzetiseg)
        .OrderBy(g => g.Key);
    foreach (var csoport in csoportok)
    {
        iro.WriteLine($"{csoport.Key} ({csoport.Count()} fő):");
        // ismert dátumok növekvő sorrendben, az ismeretlenek a végén
        foreach (Pilota p in csoport.OrderBy(p => p.SzuletesiDatum ?? DateTime.MaxValue))
        {
            iro.WriteLine($"\t{p.Nev} - {p.DatumSzoveg}");
        }
    }
    Console.WriteLine("\tA pilota_csoportok.txt fájl elkészült.");
}
catch (Exception)
{
    Console.WriteLine("\tHiba! A fájl írása sikertelen.");
}
