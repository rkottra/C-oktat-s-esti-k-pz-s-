Console.OutputEncoding = System.Text.Encoding.UTF8;

string valasztas;
do
{
    Console.WriteLine("1 – Kör");
    Console.WriteLine("2 – Téglalap");
    Console.WriteLine("3 – Háromszög (három oldalból)");
    Console.WriteLine("0 – Kilépés");
    Console.Write("Választás: ");
    valasztas = (Console.ReadLine() ?? "").Trim();

    switch (valasztas)
    {
        case "1":
            double r = BekerPozitiv("r = ");
            Eredmeny("Kör", KorKerulet(r), KorTerulet(r));
            break;
        case "2":
            double a = BekerPozitiv("a = ");
            double b = BekerPozitiv("b = ");
            Eredmeny("Téglalap", TeglalapKerulet(a, b), TeglalapTerulet(a, b));
            break;
        case "3":
            double x = BekerPozitiv("a = ");
            double y = BekerPozitiv("b = ");
            double z = BekerPozitiv("c = ");
            if (Szerkesztheto(x, y, z))
                Eredmeny("Háromszög", x + y + z, HaromszogTerulet(x, y, z));
            else
                Console.WriteLine("Ebből a három oldalból nem szerkeszthető háromszög.");
            break;
        case "0":
            Console.WriteLine("Viszlát!");
            break;
        default:
            Console.WriteLine("Nincs ilyen menüpont.");
            break;
    }
    Console.WriteLine();
} while (valasztas != "0");

// ---------- Metódusok ----------

static double BekerPozitiv(string kerdes)
{
    while (true)
    {
        Console.Write(kerdes);
        if (double.TryParse(Console.ReadLine(), out double ertek) && ertek > 0)
            return ertek;
        Console.WriteLine("Pozitív számot adj meg!");
    }
}

static double KorKerulet(double r) => 2 * r * Math.PI;
static double KorTerulet(double r) => r * r * Math.PI;

static double TeglalapKerulet(double a, double b) => 2 * (a + b);
static double TeglalapTerulet(double a, double b) => a * b;

static bool Szerkesztheto(double a, double b, double c) =>
    a + b > c && a + c > b && b + c > a;

static double HaromszogTerulet(double a, double b, double c)
{
    double s = (a + b + c) / 2;
    return Math.Sqrt(s * (s - a) * (s - b) * (s - c));
}

static void Eredmeny(string idom, double kerulet, double terulet)
{
    Console.WriteLine($"{idom} – kerület: {kerulet:F2}, terület: {terulet:F2}");
}
