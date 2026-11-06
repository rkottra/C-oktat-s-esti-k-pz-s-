using Edzesnaplo;

Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.InputEncoding = System.Text.Encoding.UTF8;

const string Fajl = "edzesek.csv";
List<Edzes> naplo = Beolvas(Fajl);
bool modosult = false;

bool fut = true;
while (fut)
{
    Fejlec("EDZÉSNAPLÓ");
    Console.WriteLine("1 – Napló listázása");
    Console.WriteLine("2 – Új edzés rögzítése");
    Console.WriteLine("3 – Edzés törlése");
    Console.WriteLine("4 – Összesítés sportáganként");
    Console.WriteLine("5 – Havi összesítés");
    Console.WriteLine("6 – Csúcsok");
    Console.WriteLine("0 – Mentés és kilépés");

    switch (BekerEgesz("Választás: ", 0, 6))
    {
        case 1: Listaz(naplo); break;
        case 2: UjEdzes(naplo); modosult = true; break;
        case 3: if (Torles(naplo)) modosult = true; break;
        case 4: Sportagankent(naplo); break;
        case 5: Havi(naplo); break;
        case 6: Csucsok(naplo); break;
        case 0: fut = false; break;
    }
    if (fut)
        Varakozas();
}

if (modosult)
{
    Ment(naplo, Fajl);
    Console.WriteLine($"Mentve: {naplo.Count} edzés → {Fajl}");
}
Console.WriteLine("Jó edzést!");

// ---------- Fájlkezelés ----------

static List<Edzes> Beolvas(string fajlnev)
{
    List<Edzes> lista = new List<Edzes>();
    if (!File.Exists(fajlnev))
        return lista;                       // első indításkor üres naplóval kezdünk
    string[] sorok = File.ReadAllLines(fajlnev);
    for (int i = 1; i < sorok.Length; i++)
        if (sorok[i].Trim() != "")
            lista.Add(new Edzes(sorok[i]));
    return lista;
}

static void Ment(List<Edzes> lista, string fajlnev)
{
    List<string> sorok = new List<string> { "Datum;Sportag;Perc;Kcal" };
    foreach (Edzes e in lista)
        sorok.Add(e.CsvSor());
    File.WriteAllLines(fajlnev, sorok);
}

// ---------- Menüpontok ----------

static void Listaz(List<Edzes> lista)
{
    for (int i = 0; i < lista.Count; i++)
        Console.WriteLine($"{i + 1,3}. {lista[i]}");
    Console.WriteLine($"Összesen {lista.Count} edzés.");
}

static void UjEdzes(List<Edzes> lista)
{
    string datum;
    while (true)
    {
        datum = BekerSzoveg("Dátum (ÉÉÉÉ-HH-NN): ");
        if (ErvenyesDatum(datum))
            break;
        Hiba("A dátum formátuma: ÉÉÉÉ-HH-NN, pl. 2026-04-20");
    }
    string sportag = BekerSzoveg("Sportág: ").ToLower();
    int perc = BekerEgesz("Időtartam (perc): ", 1, 300);
    int kcal = BekerEgesz("Elégetett kalória: ", 1, 3000);

    lista.Add(new Edzes(datum, sportag, perc, kcal));
    lista.Sort((a, b) => string.Compare(a.Datum, b.Datum));
    Console.WriteLine("Rögzítve.");
}

static bool ErvenyesDatum(string s)
{
    if (s.Length != 10 || s[4] != '-' || s[7] != '-')
        return false;
    for (int i = 0; i < s.Length; i++)
        if (i != 4 && i != 7 && !char.IsDigit(s[i]))
            return false;
    return true;
}

static bool Torles(List<Edzes> lista)
{
    if (lista.Count == 0) { Hiba("A napló üres."); return false; }

    int sorszam = BekerEgesz($"Törlendő sorszám (1–{lista.Count}): ", 1, lista.Count);
    Console.WriteLine(lista[sorszam - 1]);
    Console.Write("Biztosan törlöd? (i/n): ");
    if ((Console.ReadLine() ?? "").Trim().ToLower() != "i")
    {
        Console.WriteLine("Mégsem töröltem.");
        return false;
    }
    lista.RemoveAt(sorszam - 1);
    Console.WriteLine("Törölve.");
    return true;
}

static List<string> Sportagak(List<Edzes> lista)
{
    List<string> s = new List<string>();
    foreach (Edzes e in lista)
        if (!s.Contains(e.Sportag))
            s.Add(e.Sportag);
    return s;
}

static void Sportagankent(List<Edzes> lista)
{
    Fejlec($"{"Sportág",-14}{"Alkalom",8}{"Perc",7}{"Kcal",8}   Átlag");
    foreach (string sp in Sportagak(lista))
    {
        int db = 0, perc = 0, kcal = 0;
        foreach (Edzes e in lista)
            if (e.Sportag == sp)
            {
                db++;
                perc += e.Perc;
                kcal += e.Kcal;
            }
        Console.WriteLine($"{sp,-14}{db,8}{perc,7}{kcal,8}   {(double)perc / db:F1} perc");
    }
}

static void Havi(List<Edzes> lista)
{
    string honap;
    while (true)
    {
        honap = BekerSzoveg("Hónap (ÉÉÉÉ-HH): ");
        if (honap.Length == 7 && ErvenyesDatum(honap + "-01"))
            break;
        Hiba("A formátum: ÉÉÉÉ-HH, pl. 2026-03");
    }

    List<Edzes> haviak = new List<Edzes>();
    foreach (Edzes e in lista)
        if (e.Datum.StartsWith(honap + "-"))
            haviak.Add(e);
    if (haviak.Count == 0)
    {
        Console.WriteLine($"{honap}: nem volt edzés.");
        return;
    }

    int perc = 0, kcal = 0;
    foreach (Edzes e in haviak)
    {
        perc += e.Perc;
        kcal += e.Kcal;
    }

    // Melyik sportágból volt a legtöbb? – megszámlálás sportáganként + maximumkiválasztás
    string legtobbSportag = "";
    int legtobbDb = 0;
    foreach (string sp in Sportagak(haviak))
    {
        int db = 0;
        foreach (Edzes e in haviak)
            if (e.Sportag == sp)
                db++;
        if (db > legtobbDb)
        {
            legtobbDb = db;
            legtobbSportag = sp;
        }
    }
    Console.WriteLine($"{honap}: {haviak.Count} edzés, {perc} perc, {kcal} kcal; a legtöbb: {legtobbSportag} ({legtobbDb} alkalom)");
}

static void Csucsok(List<Edzes> lista)
{
    if (lista.Count == 0) { Hiba("A napló üres."); return; }

    Edzes leghosszabb = lista[0], legtobbKcal = lista[0], leghatekonyabb = lista[0];
    foreach (Edzes e in lista)
    {
        if (e.Perc > leghosszabb.Perc) leghosszabb = e;
        if (e.Kcal > legtobbKcal.Kcal) legtobbKcal = e;
        if (e.KcalPerPerc > leghatekonyabb.KcalPerPerc) leghatekonyabb = e;
    }
    Console.WriteLine($"Leghosszabb:     {leghosszabb}");
    Console.WriteLine($"Legtöbb kcal:    {legtobbKcal}");
    Console.WriteLine($"Leghatékonyabb:  {leghatekonyabb}  ({leghatekonyabb.KcalPerPerc:F1} kcal/perc)");
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
