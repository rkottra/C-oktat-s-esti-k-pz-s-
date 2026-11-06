// Újrahasznosítható menüváz: másold át a saját programodba, és cseréld ki a menüpontokat.
Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.InputEncoding = System.Text.Encoding.UTF8;

List<string> nevek = new List<string> { "Anna", "Bence", "Csilla" };   // itt lenne a fájlból beolvasott lista

bool fut = true;
while (fut)
{
    Fejlec("FŐMENÜ");
    Console.WriteLine("1 – Listázás");
    Console.WriteLine("2 – Új név felvétele");
    Console.WriteLine("3 – Rendezés (almenü)");
    Console.WriteLine("0 – Kilépés");
    int valasztas = BekerEgesz("Választás: ", 0, 3);

    switch (valasztas)
    {
        case 1: Listaz(nevek); break;
        case 2: nevek.Add(BekerSzoveg("Név: ")); break;
        case 3: RendezesAlmenu(nevek); break;
        case 0: fut = false; break;
    }
    if (fut)
        Varakozas();
}
Console.WriteLine("Viszlát!");

// ---------- Menüpontok ----------

static void Listaz(List<string> lista)
{
    Fejlec("NEVEK");
    for (int i = 0; i < lista.Count; i++)
        Console.WriteLine($"{i + 1,3}. {lista[i]}");
    Console.WriteLine($"Összesen: {lista.Count}");
}

// Almenü: ugyanaz a minta egy metóduson belül
static void RendezesAlmenu(List<string> lista)
{
    Fejlec("RENDEZÉS");
    Console.WriteLine("1 – ábécérendben");
    Console.WriteLine("2 – fordított ábécérendben");
    Console.WriteLine("0 – vissza");
    switch (BekerEgesz("Választás: ", 0, 2))
    {
        case 1: lista.Sort(); break;
        case 2: lista.Sort((a, b) => b.CompareTo(a)); break;
        case 0: return;
    }
    Listaz(lista);
}

// ---------- Segédmetódusok ----------

static int BekerEgesz(string kerdes, int min, int max)
{
    while (true)
    {
        Console.Write(kerdes);
        if (int.TryParse(Console.ReadLine(), out int x) && x >= min && x <= max)
            return x;
        Hiba($"{min} és {max} közötti egész számot adj meg!");
    }
}

static string BekerSzoveg(string kerdes)
{
    while (true)
    {
        Console.Write(kerdes);
        string s = (Console.ReadLine() ?? "").Trim();
        if (s != "")
            return s;
        Hiba("Nem lehet üres!");
    }
}

static void Fejlec(string cim)
{
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine($"=== {cim} ===");
    Console.ResetColor();
}

static void Hiba(string uzenet)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine(uzenet);
    Console.ResetColor();
}

static void Varakozas()
{
    Console.WriteLine("\nNyomj Entert a folytatáshoz...");
    Console.ReadLine();
    if (!Console.IsOutputRedirected)    // átirányított kimenetnél (pl. tesztelés) nem törlünk
        Console.Clear();
}
