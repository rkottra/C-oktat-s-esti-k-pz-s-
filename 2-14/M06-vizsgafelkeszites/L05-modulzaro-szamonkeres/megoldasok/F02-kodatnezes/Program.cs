using OlimpiaJavitott;

// A szögletes zárójeles jelölések a README hibalistájára utalnak.

List<Olimpia> olimpiak = Olimpia.BetoltCsv("olimpiak.csv");

Console.WriteLine($"2. feladat: Olimpiák száma: {olimpiak.Count}");

// [R2] Ismétlődő számláló ciklusok helyett egyetlen csoportosítás – minden kontinensre működik
Console.WriteLine("3. feladat: Rendezések kontinensenként:");
foreach (var g in olimpiak.GroupBy(o => o.Kontinens).OrderByDescending(g => g.Count()))
{
    Console.WriteLine($"\t{g.Key}: {g.Count()}");
}

// [H2] [T1] Dupla ciklus helyett GroupBy: minden város egyszer szerepel, O(n) futásidő
var tobbszorosek = olimpiak
    .GroupBy(o => o.Varos)
    .Where(g => g.Count() > 1)
    .Select(g => $"{g.Key} ({g.Count()})");
Console.WriteLine($"4. feladat: Többször rendező városok: {string.Join(", ", tobbszorosek)}");

// [H3] A "nincs találat" üzenet csak egyszer, a keresés UTÁN jelenhet meg
// [H4] Kis- és nagybetű, valamint a szóközök ne számítsanak
Console.Write("6. feladat: Ország: ");
string orszag = (Console.ReadLine() ?? "").Trim();
List<int> evek = olimpiak
    .Where(o => o.Orszag.Equals(orszag, StringComparison.OrdinalIgnoreCase))
    .Select(o => o.Ev)
    .ToList();
Console.WriteLine(evek.Count == 0
    ? "\tEz az ország nem rendezett nyári olimpiát."
    : $"\t{string.Join(", ", evek)}");

// [H5] using: a fájl biztosan lezárul, a tartalom kiíródik
// [R3] Az üres catch helyett tájékoztató üzenet, és csak a fájlműveleti hibát fogjuk el
try
{
    using StreamWriter iro = new StreamWriter("europai.txt");
    foreach (Olimpia o in olimpiak.Where(o => o.Kontinens == "Európa"))
    {
        iro.WriteLine($"{o.Ev} {o.Varos}");
    }
    Console.WriteLine("7. feladat: Az europai.txt elkészült.");
}
catch (IOException hiba)
{
    Console.WriteLine($"7. feladat: Hiba a fájl írásakor: {hiba.Message}");
}

// [R4] A kikommentezett kód és a Console.ReadKey() törölve
