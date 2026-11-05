using KutyakCsvLinq;

// ===== 1. Beolvasás: CSV → List<Kutya> =====
var hibak = new List<string>();
List<Kutya> kutyak = Kutya.FajlbolBetolt("kutyak.csv", hibak);

Console.WriteLine($"Beolvasva: {kutyak.Count} kutya, kihagyott hibás sor: {hibak.Count}");
foreach (string h in hibak)
    Console.WriteLine($"  {h}");

// ===== 2. Megszámolás, szűrés =====
Console.WriteLine($"\nKanok száma: {kutyak.Count(k => k.Nem == "kan")}");

// Ugyanez háromféleképpen: Where + Count, lekérdező szintaxis, elnevezett metódus
int kanok2 = kutyak.Where(k => k.Nem == "kan").Count();
int kanok3 = (from k in kutyak
              where k.Nem == "kan"
              select k).Count();
int kanok4 = kutyak.Count(KanE);
Console.WriteLine($"  (ellenőrzés: {kanok2}, {kanok3}, {kanok4})");

var oltatlanok = kutyak.Where(k => !k.Oltott).ToList();
Console.WriteLine($"Oltásra vár: {string.Join(", ", oltatlanok.Select(k => k.Nev))}");

// ===== 3. Aggregálás: Sum, Average, Min, Max =====
Console.WriteLine($"\nÁtlagos súly: {kutyak.Average(k => k.Suly):F1} kg");
Console.WriteLine($"Pulik átlagos súlya: {kutyak.Where(k => k.Fajta == "Puli").Average(k => k.Suly):F1} kg");
Console.WriteLine($"Legfiatalabb kor: {kutyak.Min(k => k.Kor)} év, legidősebb: {kutyak.Max(k => k.Kor)} év");

// MaxBy: nem a legnagyobb értéket, hanem a hozzá tartozó objektumot adja vissza
Kutya legnehezebb = kutyak.MaxBy(k => k.Suly)!;
Console.WriteLine($"Legnehezebb: {legnehezebb}");

// ===== 4. Keresés: Any, All, FirstOrDefault =====
Console.WriteLine($"\nVan kölyökkutya? {(kutyak.Any(k => k.Kolyok) ? "igen" : "nem")}");
Console.WriteLine($"Mind chipes? {(kutyak.All(k => k.Chipes) ? "igen" : "nem")}");

Console.Write("Keresett név: ");
string keresett = Console.ReadLine() ?? "";
Kutya? talalt = kutyak.FirstOrDefault(k => k.Nev.Equals(keresett, StringComparison.OrdinalIgnoreCase));
Console.WriteLine(talalt == null ? "Nincs ilyen nevű kutya." : $"Találat: {talalt}");

// ===== 5. Rendezés és kivetítés (Select) =====
Console.WriteLine("\nA 3 legidősebb (azonos kornál név szerint):");
var legidosebbek = kutyak.OrderByDescending(k => k.Kor).ThenBy(k => k.Nev).Take(3);
foreach (var k in legidosebbek)
    Console.WriteLine($"  {k.Nev,-10} {k.Kor} év");

// Névtelen (anonim) típus: csak a szükséges adatok
var nevjegyek = kutyak.Where(k => k.Kor >= 13)
                      .Select(k => new { k.Nev, Leiras = $"{k.Fajta}, {k.Nem}" });
Console.WriteLine("13 évesek és idősebbek:");
foreach (var n in nevjegyek)
    Console.WriteLine($"  {n.Nev}: {n.Leiras}");

// ===== 6. Csoportosítás: GroupBy =====
Console.WriteLine("\nFajtánként:");
var fajtak = kutyak.GroupBy(k => k.Fajta)
                   .Select(cs => new
                   {
                       Fajta = cs.Key,                       // a csoport kulcsa
                       Darab = cs.Count(),
                       AtlagSuly = cs.Average(k => k.Suly),
                   })
                   .OrderByDescending(f => f.Darab)
                   .ThenBy(f => f.Fajta)
                   .ToList();
foreach (var f in fajtak)
    Console.WriteLine($"  {f.Fajta,-14} {f.Darab} db, átlag {f.AtlagSuly,5:F1} kg");

// ===== 7. Fájlba írás =====
// a) statisztika szövegfájlba
File.WriteAllLines("fajtak.txt", fajtak.Select(f => $"{f.Fajta}: {f.Darab} db"));

// b) szűrt adatok CSV-be, fejléccel együtt
var sorok = new List<string> { "id;nev;kor;suly_kg;nem;fajta;chipes;oltott" };
sorok.AddRange(kutyak.Where(k => !k.Chipes).Select(k => k.CsvSor()));
File.WriteAllLines("chip_nelkul.csv", sorok);

Console.WriteLine($"\nKiírva: fajtak.txt, chip_nelkul.csv ({sorok.Count - 1} kutya)");
Console.WriteLine($"Helyük: {Environment.CurrentDirectory}");

// Elnevezett metódus feltételként (a lambda helyett is átadható)
static bool KanE(Kutya k) => k.Nem == "kan";
