using System.Globalization;
using OrszagokMenu;

Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.InputEncoding = System.Text.Encoding.UTF8;

const string Fajl = "europa.csv";
List<Orszag> orszagok = Beolvas(Fajl);
Console.WriteLine($"Beolvasva: {orszagok.Count} ország.");

bool fut = true;
bool modosult = false;
while (fut)
{
    Fejlec("ORSZÁGOK – FŐMENÜ");
    Console.WriteLine("1 – Összes ország");
    Console.WriteLine("2 – Keresés névrészletre");
    Console.WriteLine("3 – Melyik ország fővárosa…?");
    Console.WriteLine("4 – Népesség szerinti szűrés");
    Console.WriteLine("5 – Statisztika");
    Console.WriteLine("6 – Rendezés");
    Console.WriteLine("7 – Új ország felvétele");
    Console.WriteLine("8 – Ország törlése");
    Console.WriteLine("0 – Kilépés");

    switch (BekerEgesz("Választás: ", 0, 8))
    {
        case 1: Listaz(orszagok); break;
        case 2: KeresNevreszletre(orszagok); break;
        case 3: KeresFovarosra(orszagok); break;
        case 4: SzuresNepessegre(orszagok); break;
        case 5: Statisztika(orszagok); break;
        case 6: Rendezes(orszagok); break;
        case 7: modosult |= UjOrszag(orszagok); break;
        case 8: modosult |= Torles(orszagok); break;
        case 0: fut = false; break;
    }
    if (fut)
        Varakozas();
}

if (modosult)
{
    Ment(orszagok, Fajl);
    Console.WriteLine($"A változások elmentve: {Fajl}");
}
Console.WriteLine("Viszlát!");

// ---------- Fájlkezelés ----------

static List<Orszag> Beolvas(string fajlnev)
{
    List<Orszag> lista = new List<Orszag>();
    try
    {
        string[] sorok = File.ReadAllLines(fajlnev);
        for (int i = 1; i < sorok.Length; i++)
            if (sorok[i].Trim() != "")
                lista.Add(new Orszag(sorok[i]));
    }
    catch (FileNotFoundException)
    {
        Hiba($"Nem található a(z) {fajlnev} fájl – üres listával indulunk.");
    }
    catch (FormatException)
    {
        Hiba($"Hibás adat a(z) {fajlnev} fájlban.");
    }
    return lista;
}

static void Ment(List<Orszag> lista, string fajlnev)
{
    List<string> sorok = new List<string> { "Nev;Fovaros;Terulet;Nepesseg;Gdp" };
    foreach (Orszag o in lista)
        sorok.Add(o.CsvSor());
    File.WriteAllLines(fajlnev, sorok);
}

// ---------- Menüpontok ----------

static void Listaz(List<Orszag> lista)
{
    Fejlec($"{"Név",-22}{"Főváros",-18}{"Terület",15}{"Népesség",16}{"GDP/fő",10}");
    foreach (Orszag o in lista)
        Console.WriteLine(o);
    Console.WriteLine($"Összesen: {lista.Count} ország");
}

static void KeresNevreszletre(List<Orszag> lista)
{
    string resz = BekerSzoveg("Névrészlet: ");
    List<Orszag> talalat = new List<Orszag>();
    foreach (Orszag o in lista)
        if (o.Nev.Contains(resz, StringComparison.OrdinalIgnoreCase))
            talalat.Add(o);

    if (talalat.Count == 0)
        Hiba("Nincs találat.");
    else
        Listaz(talalat);
}

static void KeresFovarosra(List<Orszag> lista)
{
    string fovaros = BekerSzoveg("Főváros: ");
    int i = 0;
    while (i < lista.Count && !string.Equals(lista[i].Fovaros, fovaros, StringComparison.OrdinalIgnoreCase))
        i++;
    if (i < lista.Count)
        Console.WriteLine($"{lista[i].Fovaros} {lista[i].Nev} fővárosa.");
    else
        Hiba($"{fovaros} nem európai főváros (vagy nincs az adatok között).");
}

static void SzuresNepessegre(List<Orszag> lista)
{
    int min = BekerEgesz("Legalább hány millió lakos? ", 0, 200);
    int max = BekerEgesz("Legfeljebb hány millió lakos? ", min, 200);
    List<Orszag> talalat = new List<Orszag>();
    foreach (Orszag o in lista)
        if (o.Nepesseg >= min * 1_000_000 && o.Nepesseg <= max * 1_000_000)
            talalat.Add(o);
    Listaz(talalat);
}

static void Statisztika(List<Orszag> lista)
{
    if (lista.Count == 0) { Hiba("Nincs adat."); return; }

    long nepesseg = 0;
    double terulet = 0;
    Orszag legnagyobb = lista[0], legsurubb = lista[0], leggazdagabb = lista[0];
    foreach (Orszag o in lista)
    {
        nepesseg += o.Nepesseg;
        terulet += o.Terulet;
        if (o.Terulet > legnagyobb.Terulet) legnagyobb = o;
        if (o.Nepsuruseg > legsurubb.Nepsuruseg) legsurubb = o;
        if (o.Gdp > leggazdagabb.Gdp) leggazdagabb = o;
    }
    Fejlec("STATISZTIKA");
    Console.WriteLine($"Országok száma:      {lista.Count}");
    Console.WriteLine($"Összes népesség:     {nepesseg:N0} fő");
    Console.WriteLine($"Összes terület:      {terulet:N0} km²");
    Console.WriteLine($"Legnagyobb területű: {legnagyobb.Nev} ({legnagyobb.Terulet:N0} km²)");
    Console.WriteLine($"Legsűrűbben lakott:  {legsurubb.Nev} ({legsurubb.Nepsuruseg:N0} fő/km²)");
    Console.WriteLine($"Legmagasabb GDP/fő:  {leggazdagabb.Nev} ({leggazdagabb.Gdp:N0} $)");
}

// Almenü
static void Rendezes(List<Orszag> lista)
{
    Fejlec("RENDEZÉS");
    Console.WriteLine("1 – név szerint");
    Console.WriteLine("2 – terület szerint (csökkenő)");
    Console.WriteLine("3 – népesség szerint (csökkenő)");
    Console.WriteLine("0 – vissza");
    switch (BekerEgesz("Választás: ", 0, 3))
    {
        case 1: lista.Sort((a, b) => string.Compare(a.Nev, b.Nev)); break;
        case 2: lista.Sort((a, b) => b.Terulet.CompareTo(a.Terulet)); break;
        case 3: lista.Sort((a, b) => b.Nepesseg.CompareTo(a.Nepesseg)); break;
        case 0: return;
    }
    Listaz(lista);
}

static bool UjOrszag(List<Orszag> lista)
{
    string nev = BekerSzoveg("Név: ");
    foreach (Orszag o in lista)
        if (string.Equals(o.Nev, nev, StringComparison.OrdinalIgnoreCase))
        {
            Hiba("Ez az ország már szerepel.");
            return false;
        }
    string fovaros = BekerSzoveg("Főváros: ");
    int terulet = BekerEgesz("Terület (km²): ", 1, 20_000_000);
    int nepesseg = BekerEgesz("Népesség (fő): ", 0, 2_000_000_000);
    int gdp = BekerEgesz("GDP/fő (USD): ", 0, 1_000_000);
    lista.Add(new Orszag(nev, fovaros, terulet, nepesseg, gdp));
    Console.WriteLine($"{nev} felvéve.");
    return true;
}

static bool Torles(List<Orszag> lista)
{
    string nev = BekerSzoveg("Törlendő ország neve: ");
    int i = 0;
    while (i < lista.Count && !string.Equals(lista[i].Nev, nev, StringComparison.OrdinalIgnoreCase))
        i++;
    if (i == lista.Count)
    {
        Hiba("Nincs ilyen ország.");
        return false;
    }
    Console.WriteLine($"{lista[i].Nev} törölve.");
    lista.RemoveAt(i);
    return true;
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
    Console.WriteLine(cim);
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
    if (!Console.IsOutputRedirected)
        Console.Clear();
}
