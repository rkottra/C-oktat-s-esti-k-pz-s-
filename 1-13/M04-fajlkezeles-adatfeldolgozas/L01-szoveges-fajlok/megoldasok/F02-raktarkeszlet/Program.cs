// 13-M04-L01-F02 · Raktárkészlet CSV-ből – megoldás

Console.OutputEncoding = System.Text.Encoding.UTF8;

// 1. Beolvasás: CSV-sor → Termek → List<Termek>
List<Termek> termekek = new List<Termek>();
string[] sorok = File.ReadAllLines("termekek.csv");
for (int i = 1; i < sorok.Length; i++)
{
    termekek.Add(new Termek(sorok[i]));
}
Console.WriteLine($"1. Beolvasott termékek: {termekek.Count} db");

// 2. Átlagár (összegzés)
int arOsszeg = 0;
foreach (Termek t in termekek)
{
    arOsszeg += t.Ar;
}
Console.WriteLine($"2. Átlagár: {(double)arOsszeg / termekek.Count:N0} Ft");

// 3. A „Hardver” kategória termékei (kiválogatás)
Console.WriteLine("3. Hardver kategória:");
foreach (Termek t in termekek)
{
    if (t.Kategoria == "Hardver")
        Console.WriteLine($"   {t.Nev}");
}

// 4. A legkisebb készletű termék (minimumkiválasztás)
Termek legkevesebb = termekek[0];
foreach (Termek t in termekek)
{
    if (t.Keszlet < legkevesebb.Keszlet)
        legkevesebb = t;
}
Console.WriteLine($"4. Legkisebb készlet: {legkevesebb}");

// 5. A raktár teljes értéke (összegzés számított tulajdonságon)
long raktarErtek = 0;
foreach (Termek t in termekek)
{
    raktarErtek += t.KeszletErtek;
}
Console.WriteLine($"5. A raktárkészlet értéke: {raktarErtek:N0} Ft");

// 6. Hány termék készült 2021 előtt? (megszámlálás)
int regiek = 0;
foreach (Termek t in termekek)
{
    if (t.GyartasiEv < 2021) regiek++;
}
Console.WriteLine($"6. 2021 előtt gyártott termékek: {regiek} db");

// 7. Bekért kategória termékei, és van-e közöttük 100 000 Ft feletti (keresés)
Console.Write("7. Kategória: ");
string kategoria = Console.ReadLine() ?? "";
int db = 0;
Termek? dragaTalalat = null;
foreach (Termek t in termekek)
{
    if (t.Kategoria.ToLower() == kategoria.ToLower())
    {
        db++;
        if (dragaTalalat == null && t.Ar > 100000)
            dragaTalalat = t;
    }
}
if (db == 0)
    Console.WriteLine("   Nincs ilyen kategória.");
else
{
    Console.WriteLine($"   {db} termék tartozik ide.");
    Console.WriteLine(dragaTalalat == null
        ? "   Nincs köztük 100 000 Ft-nál drágább."
        : $"   Van 100 000 Ft-nál drágább, pl.: {dragaTalalat.Nev}");
}

// 8. Rendelési lista: a 10 db-nál kisebb készletű termékek fájlba
using (StreamWriter sw = new StreamWriter("rendelendo.txt"))
{
    sw.WriteLine("Nev;Keszlet;Rendelendo");
    foreach (Termek t in termekek)
    {
        if (t.Keszlet < 10)
            sw.WriteLine($"{t.Nev};{t.Keszlet};{10 - t.Keszlet}");
    }
}
Console.WriteLine("8. A rendelési lista elkészült: rendelendo.txt");
