// 13-M01-L02-F03 · Rulett-statisztika (tömb és lista nélkül)

const int Porgetesek = 200;
const int SoronkentDb = 20;

Random r = new Random();

// Számlálók – egyszerű változók
int tucat1 = 0, tucat2 = 0, tucat3 = 0;
int nulla = 0, paros = 0, paratlan = 0;

for (int i = 1; i <= Porgetesek; i++)
{
    int szam = r.Next(0, 37);    // 0..36

    // Szín és számlálás
    Console.BackgroundColor = ConsoleColor.DarkGreen;
    if (szam == 0)
    {
        Console.ForegroundColor = ConsoleColor.White;
        nulla++;
    }
    else if (szam % 2 == 0)
    {
        Console.ForegroundColor = ConsoleColor.Black;
        paros++;
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Red;
        paratlan++;
    }

    // Tucat: egész osztással (1–12 → 1, 13–24 → 2, 25–36 → 3)
    if (szam != 0)
    {
        switch ((szam - 1) / 12 + 1)
        {
            case 1: tucat1++; break;
            case 2: tucat2++; break;
            case 3: tucat3++; break;
        }
    }

    Console.Write($"{szam,3}");

    if (i % SoronkentDb == 0)
    {
        Console.ResetColor();    // sortörés előtt vissza, különben a sor vége is zöld lesz
        Console.WriteLine();
    }
}
Console.ResetColor();

// Statisztika – valós osztás: 100.0
Console.WriteLine();
Console.WriteLine($"1. tucat (1–12):  {tucat1 * 100.0 / Porgetesek,5:F1} %");
Console.WriteLine($"2. tucat (13–24): {tucat2 * 100.0 / Porgetesek,5:F1} %");
Console.WriteLine($"3. tucat (25–36): {tucat3 * 100.0 / Porgetesek,5:F1} %");
Console.WriteLine($"nulla:            {nulla * 100.0 / Porgetesek,5:F1} %");
Console.WriteLine($"páros: {paros}, páratlan: {paratlan}");
