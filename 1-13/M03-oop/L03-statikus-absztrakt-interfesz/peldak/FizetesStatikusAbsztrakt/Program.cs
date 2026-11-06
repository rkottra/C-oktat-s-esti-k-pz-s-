using FizetesStatikusAbsztrakt;

Console.OutputEncoding = System.Text.Encoding.UTF8;

// Statikus metódus hívása az osztály nevén keresztül – objektum nem kell.
Console.WriteLine("Kerekítés 5 forintra:");
foreach (int ar in new[] { 1231, 1233, 1236, 1238, 1240 })
    Console.WriteLine($"  {ar} Ft → {Kerekites.OtForintra(ar)} Ft");
Console.WriteLine();

// FizetesiMod m = new FizetesiMod("x");   // HIBA: absztrakt osztályból nem lehet objektum

// Absztrakt típusú változók, konkrét objektumok
FizetesiMod keszpenz = new Keszpenz(10_000);
FizetesiMod kartya = new Bankkartya(50_000, 20_000);
FizetesiMod szep = new SzepKartya(6_000);

Console.WriteLine("Fizetések:");
keszpenz.Fizet(2_348, "pékáru");
kartya.Fizet(12_990, "cipő");
szep.Fizet(4_200, "ebéd");
keszpenz.Fizet(8_500, "tankolás");
kartya.Fizet(9_000, "kabát");           // napi limit
szep.Fizet(2_500, "vacsora");           // kevés keret
kartya.Fizet(6_490, "könyvek");

// Statikus tagok: az összes objektum közös adatai
Console.WriteLine();
Console.WriteLine($"Sikeres fizetések: {FizetesiMod.SikeresFizetesek}");
Console.WriteLine($"Összforgalom: {FizetesiMod.Osszforgalom:N0} Ft");

// A leszármazott saját adatai típusvizsgálat után
if (keszpenz is Keszpenz k) Console.WriteLine($"A tárcában maradt: {k.Penztarca:N0} Ft");
if (kartya is Bankkartya b) Console.WriteLine($"Bankszámla egyenlege: {b.Egyenleg:N0} Ft");
if (szep is SzepKartya s) Console.WriteLine($"SZÉP-kártya kerete: {s.Keret:N0} Ft");
