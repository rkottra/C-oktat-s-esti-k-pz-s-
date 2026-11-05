using HibasElemek;

// FIGYELEM: ez a program szándékosan hibás – a hibakereső gyakorláshoz készült.

List<Elem> elemek = Elem.BetoltCsv("elemek.csv");
Console.WriteLine($"1. feladat: Elemek száma: {elemek.Count}");

// 2. feladat: a legnehezebb elem
Elem legnehezebb = elemek.OrderBy(e => e.Atomtomeg).First();
Console.WriteLine($"2. feladat: Legnehezebb: {legnehezebb.Nev} ({legnehezebb.Atomtomeg})");

// 3. feladat: egy kategória átlagos atomtömege
Console.Write("3. feladat: Kategória: ");
string kategoria = Console.ReadLine() ?? "";
double atlag = elemek.Where(e => e.Kategoria == kategoria).Average(e => e.Atomtomeg);
Console.WriteLine($"\tÁtlagos atomtömeg: {atlag:F2}");

// 4. feladat: a gázok listája fájlba
StreamWriter iro = new StreamWriter("gazok.txt");
foreach (Elem e in elemek.Where(e => e.Halmazallapot == "gaz"))
{
    iro.WriteLine($"{e.Rendszam}\t{e.Vegyjel}\t{e.Nev}");
}
Console.WriteLine("4. feladat: gazok.txt kész");
