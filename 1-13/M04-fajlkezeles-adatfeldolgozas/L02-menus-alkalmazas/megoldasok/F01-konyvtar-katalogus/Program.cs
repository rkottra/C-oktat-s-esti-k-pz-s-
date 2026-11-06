using KonyvtarKatalogus;

Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.InputEncoding = System.Text.Encoding.UTF8;

List<Konyv> konyvek = new List<Konyv>();
try
{
    string[] sorok = File.ReadAllLines("konyvek.csv");
    for (int i = 1; i < sorok.Length; i++)
        if (sorok[i].Trim() != "")
            konyvek.Add(new Konyv(sorok[i]));
}
catch (FileNotFoundException)
{
    Hiba("Nem található a konyvek.csv fájl.");
    return;
}

bool fut = true;
while (fut)
{
    Fejlec("KÖNYVTÁRI KATALÓGUS");
    Console.WriteLine("1 – Teljes katalógus");
    Console.WriteLine("2 – Keresés szerzőre (névrészlet)");
    Console.WriteLine("3 – Böngészés műfaj szerint");
    Console.WriteLine("4 – Kölcsönözhető-e? (cím alapján)");
    Console.WriteLine("5 – Statisztika");
    Console.WriteLine("6 – Népszerűségi toplista mentése");
    Console.WriteLine("0 – Kilépés");

    switch (BekerEgesz("Választás: ", 0, 6))
    {
        case 1: Listaz(konyvek); break;
        case 2: SzerzoKereses(konyvek); break;
        case 3: MufajAlmenu(konyvek); break;
        case 4: Kolcsonozheto(konyvek); break;
        case 5: Statisztika(konyvek); break;
        case 6: ToplistaMentes(konyvek, "toplista.txt"); break;
        case 0: fut = false; break;
    }
    if (fut)
        Varakozas();
}
Console.WriteLine("Viszlát!");

// ---------- Menüpontok ----------

static void Listaz(List<Konyv> lista)
{
    Fejlec($"{"Az.",-5}{"Cím",-34}{"Szerző",-26}{"Műfaj",-14}{"Polcon",7}");
    foreach (Konyv k in lista)
        Console.WriteLine(k);
    Console.WriteLine($"{lista.Count} cím");
}

static void SzerzoKereses(List<Konyv> lista)
{
    string resz = BekerSzoveg("Szerző (részlet): ");
    List<Konyv> talalat = new List<Konyv>();
    foreach (Konyv k in lista)
        if (k.Szerzo.Contains(resz, StringComparison.OrdinalIgnoreCase))
            talalat.Add(k);

    if (talalat.Count == 0)
        Hiba("Nincs ilyen szerzőtől könyv.");
    else
        Listaz(talalat);
}

static List<string> Mufajok(List<Konyv> lista)
{
    List<string> mufajok = new List<string>();
    foreach (Konyv k in lista)
        if (!mufajok.Contains(k.Mufaj))
            mufajok.Add(k.Mufaj);
    return mufajok;
}

static void MufajAlmenu(List<Konyv> lista)
{
    List<string> mufajok = Mufajok(lista);
    Fejlec("MŰFAJOK");
    for (int i = 0; i < mufajok.Count; i++)
        Console.WriteLine($"{i + 1} – {mufajok[i]}");
    Console.WriteLine("0 – vissza");

    int valasztas = BekerEgesz("Választás: ", 0, mufajok.Count);
    if (valasztas == 0)
        return;

    string mufaj = mufajok[valasztas - 1];
    List<Konyv> talalat = new List<Konyv>();
    foreach (Konyv k in lista)
        if (k.Mufaj == mufaj)
            talalat.Add(k);
    Listaz(talalat);
}

static void Kolcsonozheto(List<Konyv> lista)
{
    string cim = BekerSzoveg("Cím: ");
    int i = 0;
    while (i < lista.Count && !string.Equals(lista[i].Cim, cim, StringComparison.OrdinalIgnoreCase))
        i++;

    if (i == lista.Count)
    {
        Hiba("Nincs ilyen című könyv a katalógusban.");
        return;
    }
    Konyv k = lista[i];
    if (k.Elerheto > 0)
        Console.WriteLine($"{k.Cim} ({k.Szerzo}): {k.Elerheto} példány kölcsönözhető.");
    else
        Console.WriteLine($"{k.Cim} ({k.Szerzo}): minden példány ki van kölcsönözve ({k.Kolcsonzott}/{k.Peldany}).");
}

static void Statisztika(List<Konyv> lista)
{
    int osszes = 0, kint = 0;
    List<string> elfogyott = new List<string>();
    foreach (Konyv k in lista)
    {
        osszes += k.Peldany;
        kint += k.Kolcsonzott;
        if (k.Elerheto == 0)
            elfogyott.Add(k.Cim);
    }
    Fejlec("STATISZTIKA");
    Console.WriteLine($"Összes példány: {osszes}, ebből kikölcsönözve: {kint} ({100.0 * kint / osszes:F1} %)");
    Console.WriteLine($"Teljesen kikölcsönzött címek: {string.Join(", ", elfogyott)}");

    Console.WriteLine("Műfajonként:");
    foreach (string m in Mufajok(lista))
    {
        int db = 0;
        foreach (Konyv k in lista)
            if (k.Mufaj == m)
                db++;
        Console.WriteLine($"  {m,-15}{db,3} cím");
    }
}

static void ToplistaMentes(List<Konyv> lista, string fajlnev)
{
    List<Konyv> rendezett = new List<Konyv>(lista);
    rendezett.Sort((a, b) => b.Kihasznaltsag.CompareTo(a.Kihasznaltsag));

    List<string> sorok = new List<string>();
    for (int i = 0; i < rendezett.Count; i++)
        sorok.Add($"{i + 1};{rendezett[i].Cim};{rendezett[i].Kihasznaltsag:F0}%");
    File.WriteAllLines(fajlnev, sorok);
    Console.WriteLine($"A toplista elmentve: {fajlnev} ({sorok.Count} sor)");
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
