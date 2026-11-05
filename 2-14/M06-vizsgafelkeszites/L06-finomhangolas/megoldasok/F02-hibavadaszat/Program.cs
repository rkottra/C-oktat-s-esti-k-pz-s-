using HibakJavitva;

// A [n] jelölések a README hibalistájára utalnak.

List<Elem> elemek = Elem.BetoltCsv("elemek.csv");
Console.WriteLine($"1. feladat: Elemek száma: {elemek.Count}");

// [5] A legnehezebb a legNAGYOBB atomtömegű: MaxBy (vagy OrderByDescending)
Elem legnehezebb = elemek.MaxBy(e => e.Atomtomeg)!;
Console.WriteLine($"2. feladat: Legnehezebb: {legnehezebb.Nev} ({legnehezebb.Atomtomeg})");

// [6] Kis-/nagybetű-független összehasonlítás, és nincs Average üres sorozaton
Console.Write("3. feladat: Kategória: ");
string kategoria = (Console.ReadLine() ?? "").Trim();
var kategoriaElemei = elemek
    .Where(e => e.Kategoria.Equals(kategoria, StringComparison.OrdinalIgnoreCase))
    .ToList();
Console.WriteLine(kategoriaElemei.Count == 0
    ? "\tNincs ilyen kategória."
    : $"\tÁtlagos atomtömeg: {kategoriaElemei.Average(e => e.Atomtomeg):F2}");

// [7] A fájlban "gáz" szerepel, ékezettel
// [8] using + try–catch: a fájl lezárul, hiba esetén érthető üzenet
try
{
    using StreamWriter iro = new StreamWriter("gazok.txt");
    foreach (Elem e in elemek.Where(e => e.Halmazallapot == "gáz"))
    {
        iro.WriteLine($"{e.Rendszam}\t{e.Vegyjel}\t{e.Nev}");
    }
    Console.WriteLine("4. feladat: A gazok.txt elkészült.");
}
catch (IOException hiba)
{
    Console.WriteLine($"4. feladat: Hiba a fájl írásakor: {hiba.Message}");
}
