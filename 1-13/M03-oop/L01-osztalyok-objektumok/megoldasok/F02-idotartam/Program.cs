using Idotartamok;

// --- Átváltások számított tulajdonságokkal ---
Idotartam ido = new Idotartam(5400);
Console.WriteLine($"{ido.Masodpercek} mp = {ido.Percek} perc = {ido.Orak} óra ({ido})");
ido.Percek = 45;
Console.WriteLine($"Percek = 45 után: {ido} ({ido.Masodpercek} mp)");

// --- Heti edzések listában ---
List<Idotartam> edzesek = new List<Idotartam>
{
    new Idotartam(0, 42, 10),
    new Idotartam(3900),            // 65 perc
    new Idotartam(0, 51, 45),
    new Idotartam(1, 25, 30),
    new Idotartam(0, 53, 30)
};

Console.WriteLine("\nHeti edzések:");
for (int i = 0; i < edzesek.Count; i++)
    Console.WriteLine($"  {i + 1}. {edzesek[i]}");

Idotartam ossz = new Idotartam(0);
foreach (Idotartam e in edzesek)
    ossz.Hozzaad(e);
Console.WriteLine($"Heti összidő: {ossz}");

Idotartam leghosszabb = edzesek[0];
foreach (Idotartam e in edzesek)
    if (e.Masodpercek > leghosszabb.Masodpercek)
        leghosszabb = e;
Console.WriteLine($"Leghosszabb edzés: {leghosszabb}");
Console.WriteLine($"Átlagos edzés: {ossz.Percek / edzesek.Count:F1} perc");

// --- Új edzés bekérése ---
Console.WriteLine();
try
{
    Console.Write("Új edzés – óra: ");
    int ora = int.Parse(Console.ReadLine() ?? "");
    Console.Write("perc: ");
    int perc = int.Parse(Console.ReadLine() ?? "");
    Console.Write("másodperc: ");
    int mp = int.Parse(Console.ReadLine() ?? "");

    Idotartam uj = new Idotartam(ora, perc, mp);
    edzesek.Add(uj);
    ossz.Hozzaad(uj);
    Console.WriteLine($"Új heti összidő: {ossz}");
}
catch (FormatException)
{
    Console.WriteLine("Hiba: egész számot kell megadni.");
}
catch (ArgumentException ex)
{
    Console.WriteLine($"Hiba: {ex.Message}");
}
