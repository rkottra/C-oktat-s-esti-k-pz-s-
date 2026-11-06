using Atvalto;

Console.OutputEncoding = System.Text.Encoding.UTF8;

Console.WriteLine("1 – km → mérföld      2 – mérföld → km");
Console.WriteLine("3 – °C → °F           4 – °F → °C");
Console.WriteLine("5 – kg → font         6 – font → kg");
Console.WriteLine("7 – Ft → EUR          8 – EUR → Ft");
Console.WriteLine($"9 – Árfolyam beállítása (most: {Mertekegyseg.EuroArfolyam} Ft/EUR)");
Console.WriteLine("0 – Kilépés");

bool fut = true;
while (fut)
{
    Console.Write("Választás: ");
    string valasztas = (Console.ReadLine() ?? "0").Trim();
    switch (valasztas)
    {
        case "1": Atvalt("km", "mérföld", Mertekegyseg.KmbolMerfold); break;
        case "2": Atvalt("mérföld", "km", Mertekegyseg.MerfoldbolKm); break;
        case "3": Atvalt("°C", "°F", Mertekegyseg.CelsiusbolFahrenheit); break;
        case "4": Atvalt("°F", "°C", Mertekegyseg.FahrenheitbolCelsius); break;
        case "5": Atvalt("kg", "font", Mertekegyseg.KgbolFont); break;
        case "6": Atvalt("font", "kg", Mertekegyseg.FontbolKg); break;
        case "7": Atvalt("Ft", "EUR", Mertekegyseg.ForintbolEuro); break;
        case "8": Atvalt("EUR", "Ft", Mertekegyseg.EurobolForint); break;
        case "9":
            double uj = BekerSzam("Új árfolyam: ");
            try
            {
                Mertekegyseg.EuroArfolyam = uj;
                Console.WriteLine($"Az árfolyam mostantól {Mertekegyseg.EuroArfolyam} Ft/EUR.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Hiba: {ex.Message}");
            }
            break;
        case "0": fut = false; break;
        default: Console.WriteLine("Nincs ilyen menüpont."); break;
    }
}
Console.WriteLine($"Összesen {Mertekegyseg.Atvaltasok} átváltás történt.");

// A metódust paraméterként adjuk át (Func<double, double>), így egyetlen kiíró kód elég.
static void Atvalt(string mibol, string mibe, Func<double, double> atvalto)
{
    double ertek = BekerSzam("Érték: ");
    Console.WriteLine($"{ertek} {mibol} = {atvalto(ertek):F2} {mibe}");
}

static double BekerSzam(string kerdes)
{
    while (true)
    {
        Console.Write(kerdes);
        if (double.TryParse((Console.ReadLine() ?? "").Replace('.', ','), out double x))
            return x;
        Console.WriteLine("Számot adj meg!");
    }
}

// Válaszok:
// - A Mertekegyseg statikus osztály: nincs (és nem is lehet) konstruktora, ezért nem lehet
//   belőle objektumot létrehozni. Nincs is rá szükség, mert nincs olyan adat, ami egy-egy
//   „mértékegység-objektumhoz” tartozna – az átváltás csak a bemenettől függ.
// - Az Atvaltasok egyetlen, közös számláló: bárhonnan, bármelyik metódus növeli,
//   és a program végén egy helyről kiolvasható.
