Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.InputEncoding = System.Text.Encoding.UTF8;

List<string> lista = new List<string> { "tej", "kenyér", "alma" };

Console.WriteLine("1 – Hozzáadás  2 – Törlés névvel  3 – Törlés sorszámmal");
Console.WriteLine("4 – Listázás  5 – Rendezés  6 – Keresés  0 – Kilépés");

Menupont valasztas;
do
{
    valasztas = BekerMenupont();
    switch (valasztas)
    {
        case Menupont.Hozzaadas: Hozzaad(lista); break;
        case Menupont.TorlesNevvel: TorolNevvel(lista); break;
        case Menupont.TorlesSorszammal: TorolSorszammal(lista); break;
        case Menupont.Listazas: Listaz(lista); break;
        case Menupont.Rendezes: lista.Sort(); break;
        case Menupont.Kereses: Keres(lista); break;
    }
} while (valasztas != Menupont.Kilepes);

// ---------- Metódusok ----------

static Menupont BekerMenupont()
{
    while (true)
    {
        Console.Write("Választás: ");
        string? sor = Console.ReadLine();
        if (sor == null)                       // a bemenet véget ért
            return Menupont.Kilepes;
        if (int.TryParse(sor, out int szam) && Enum.IsDefined(typeof(Menupont), szam))
            return (Menupont)szam;
        Console.WriteLine("0 és 6 közötti számot adj meg!");
    }
}

// Keresés tétele kisbetű-függetlenül; -1, ha nincs ilyen tétel
static int Hely(List<string> lista, string nev)
{
    int i = 0;
    while (i < lista.Count && !string.Equals(lista[i], nev, StringComparison.OrdinalIgnoreCase))
        i++;
    return i < lista.Count ? i : -1;
}

static void Hozzaad(List<string> lista)
{
    Console.Write("Tétel: ");
    string tetel = (Console.ReadLine() ?? "").Trim();
    if (tetel == "")
        return;
    if (Hely(lista, tetel) != -1)
        Console.WriteLine($"A(z) „{tetel}” már szerepel a listán.");
    else
        lista.Add(tetel);
}

static void TorolNevvel(List<string> lista)
{
    Console.Write("Törlendő tétel: ");
    string nev = (Console.ReadLine() ?? "").Trim();
    int hely = Hely(lista, nev);
    if (hely == -1)
        Console.WriteLine($"A(z) „{nev}” nincs a listán.");
    else
        lista.RemoveAt(hely);
}

static void TorolSorszammal(List<string> lista)
{
    Console.Write("Sorszám: ");
    if (int.TryParse(Console.ReadLine(), out int sorszam) && sorszam >= 1 && sorszam <= lista.Count)
        lista.RemoveAt(sorszam - 1);
    else
        Console.WriteLine($"Nincs ilyen sorszám (1–{lista.Count}).");
}

static void Listaz(List<string> lista)
{
    if (lista.Count == 0)
    {
        Console.WriteLine("A lista üres.");
        return;
    }
    for (int i = 0; i < lista.Count; i++)
        Console.WriteLine($"{i + 1}. {lista[i]}");
    Console.WriteLine($"Összesen {lista.Count} tétel.");
}

static void Keres(List<string> lista)
{
    Console.Write("Keresett részlet: ");
    string reszlet = Console.ReadLine() ?? "";
    List<string> talalatok = new List<string>();
    foreach (string tetel in lista)
        if (tetel.Contains(reszlet, StringComparison.OrdinalIgnoreCase))
            talalatok.Add(tetel);
    Console.WriteLine($"Találatok ({talalatok.Count}): {string.Join(", ", talalatok)}");
}

enum Menupont { Kilepes, Hozzaadas, TorlesNevvel, TorlesSorszammal, Listazas, Rendezes, Kereses }
