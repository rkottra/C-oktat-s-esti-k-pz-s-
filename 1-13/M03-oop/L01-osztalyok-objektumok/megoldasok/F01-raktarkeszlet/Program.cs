using Raktarkeszlet;

List<Termek> termekek = new List<Termek>
{
    new Termek("Tej 1 l", 459, 24),
    new Termek("Kenyér 1 kg", 899, 12),
    new Termek("Vaj 250 g", 1190, 0),
    new Termek("Alma 1 kg", 649, 30),
    new Termek("Kávé 250 g", 2790, 4)
};

Console.WriteLine("Raktárkészlet:");
foreach (Termek t in termekek)
    Console.WriteLine("  " + t);

// Eladás – a visszatérési érték mondja meg, sikerült-e
Console.WriteLine();
Termek elso = termekek[0];
Console.WriteLine($"Eladás ({elso.Nev}, 3 db): {(elso.Elad(3) ? "sikerült" : "nincs elég készlet")}");
Termek masodik = termekek[1];
Console.WriteLine($"Eladás ({masodik.Nev}, 100 db): {(masodik.Elad(100) ? "sikerült" : "nincs elég készlet")}");

// Bevételezés a nulla készletű termékből (kiválasztás tétele)
foreach (Termek t in termekek)
{
    if (t.Keszlet == 0)
    {
        t.Bevetelez(20);
        Console.WriteLine($"Bevételezés: {t.Nev} +20 db");
        break;
    }
}

// Összegzés
int osszErtek = 0;
foreach (Termek t in termekek)
    osszErtek += t.Ertek;
Console.WriteLine($"\nRaktárkészlet értéke: {osszErtek:N0} Ft");

// Megszámlálás
int keves = 0;
foreach (Termek t in termekek)
    if (t.Keszlet < 5)
        keves++;
Console.WriteLine($"5 db alatti készlet: {keves} termék");

// Minimumkeresés
Termek legolcsobb = termekek[0];
foreach (Termek t in termekek)
    if (t.Ar < legolcsobb.Ar)
        legolcsobb = t;
Console.WriteLine($"Legolcsóbb: {legolcsobb.Nev} ({legolcsobb.Ar:N0} Ft)");

// Hibás adat
try
{
    Termek hibas = new Termek("Ingyen kifli", -50, 10);
    termekek.Add(hibas);
}
catch (ArgumentException ex)
{
    Console.WriteLine($"Hiba: {ex.Message}");
}
