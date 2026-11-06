using Osztalynaplo;

Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.InputEncoding = System.Text.Encoding.UTF8;

const string DiakFajl = "diakok.csv";
const string JegyFajl = "jegyek.csv";

// 1. Diákok beolvasása
List<Diak> diakok = new List<Diak>();
string[] dSorok = File.ReadAllLines(DiakFajl);
for (int i = 1; i < dSorok.Length; i++)
    if (dSorok[i].Trim() != "")
        diakok.Add(new Diak(dSorok[i]));

// 2. Jegyek beolvasása és hozzárendelése a diákokhoz (a két fájl összekapcsolása)
string[] jSorok = File.ReadAllLines(JegyFajl);
int jegyDb = 0;
for (int i = 1; i < jSorok.Length; i++)
{
    if (jSorok[i].Trim() == "") continue;
    string[] m = jSorok[i].Split(';');
    Diak? d = Keres(diakok, m[0]);
    if (d == null)
    {
        Hiba($"Figyelmeztetés: ismeretlen azonosító a jegyek között ({i + 1}. sor: {m[0]}) – kihagyva.");
        continue;
    }
    d.Jegyek.Add(new Jegy(m[1], int.Parse(m[2])));
    jegyDb++;
}
Console.WriteLine($"Beolvasva: {diakok.Count} diák, {jegyDb} jegy.");

bool fut = true;
while (fut)
{
    Fejlec("OSZTÁLYNAPLÓ");
    Console.WriteLine("1 – Diákok listája átlaggal");
    Console.WriteLine("2 – Diák adatlapja");
    Console.WriteLine("3 – Tantárgyi átlagok osztályonként");
    Console.WriteLine("4 – Bukásra állók");
    Console.WriteLine("5 – Új jegy beírása");
    Console.WriteLine("6 – Rangsor mentése");
    Console.WriteLine("0 – Kilépés");

    switch (BekerEgesz("Választás: ", 0, 6))
    {
        case 1: Lista(diakok); break;
        case 2: Adatlap(diakok); break;
        case 3: TantargyiAtlagok(diakok); break;
        case 4: Bukasra(diakok); break;
        case 5: UjJegy(diakok, JegyFajl); break;
        case 6: Rangsor(diakok, "rangsor.txt"); break;
        case 0: fut = false; break;
    }
    if (fut)
        Varakozas();
}

// ---------- Segéd-lekérdezések ----------

static Diak? Keres(List<Diak> diakok, string azonosito)
{
    int i = 0;
    while (i < diakok.Count && !string.Equals(diakok[i].Azonosito, azonosito, StringComparison.OrdinalIgnoreCase))
        i++;
    return i < diakok.Count ? diakok[i] : null;
}

static List<string> Osztalyok(List<Diak> diakok)
{
    List<string> o = new List<string>();
    foreach (Diak d in diakok)
        if (!o.Contains(d.Osztaly))
            o.Add(d.Osztaly);
    o.Sort();
    return o;
}

static List<string> Tantargyak(List<Diak> diakok)
{
    List<string> t = new List<string>();
    foreach (Diak d in diakok)
        foreach (string tt in d.Tantargyak())
            if (!t.Contains(tt))
                t.Add(tt);
    return t;
}

// ---------- Menüpontok ----------

static void Lista(List<Diak> diakok)
{
    List<Diak> rendezett = new List<Diak>(diakok);
    rendezett.Sort((a, b) => a.Osztaly != b.Osztaly
        ? string.Compare(a.Osztaly, b.Osztaly)
        : string.Compare(a.Nev, b.Nev));
    Fejlec($"{"Az.",-5}{"Név",-20}{"Oszt.",-6}{"Jegyek",10}{"Átlag",7}");
    foreach (Diak d in rendezett)
        Console.WriteLine(d);
}

static void Adatlap(List<Diak> diakok)
{
    string resz = BekerSzoveg("Névrészlet: ");
    List<Diak> talalat = new List<Diak>();
    foreach (Diak d in diakok)
        if (d.Nev.Contains(resz, StringComparison.OrdinalIgnoreCase))
            talalat.Add(d);

    if (talalat.Count == 0) { Hiba("Nincs ilyen diák."); return; }

    Diak valasztott = talalat[0];
    if (talalat.Count > 1)
    {
        for (int i = 0; i < talalat.Count; i++)
            Console.WriteLine($"{i + 1} – {talalat[i].Nev} ({talalat[i].Osztaly})");
        valasztott = talalat[BekerEgesz("Melyik? ", 1, talalat.Count) - 1];
    }

    Fejlec($"{valasztott.Nev} ({valasztott.Osztaly}, {valasztott.Azonosito})");
    foreach (string t in valasztott.Tantargyak())
    {
        List<int> jegyek = new List<int>();
        foreach (Jegy j in valasztott.Jegyek)
            if (j.Tantargy == t)
                jegyek.Add(j.Ertek);
        Console.WriteLine($"  {t,-13}{string.Join(" ", jegyek),-14} átlag: {valasztott.Atlag(t):F2}");
    }
    Console.WriteLine($"  Összesített átlag: {valasztott.Atlag():F2}");
}

static void TantargyiAtlagok(List<Diak> diakok)
{
    List<string> osztalyok = Osztalyok(diakok);
    Console.Write($"{"Tantárgy",-14}");
    foreach (string o in osztalyok)
        Console.Write($"{o,8}");
    Console.WriteLine();

    foreach (string t in Tantargyak(diakok))
    {
        Console.Write($"{t,-14}");
        foreach (string o in osztalyok)
        {
            int osszeg = 0, db = 0;
            foreach (Diak d in diakok)
                if (d.Osztaly == o)
                    foreach (Jegy j in d.Jegyek)
                        if (j.Tantargy == t)
                        {
                            osszeg += j.Ertek;
                            db++;
                        }
            Console.Write(db == 0 ? $"{"–",8}" : $"{(double)osszeg / db,8:F2}");
        }
        Console.WriteLine();
    }
}

static void Bukasra(List<Diak> diakok)
{
    Console.WriteLine("Bukásra áll:");
    int db = 0;
    foreach (Diak d in diakok)
    {
        if (!d.Bukik()) continue;
        foreach (string t in d.Tantargyak())
            if (d.Atlag(t) < 2.0)
            {
                Console.WriteLine($"  {d.Nev} ({d.Osztaly}) – {t}: {d.Atlag(t):F2}");
                db++;
            }
    }
    if (db == 0)
        Console.WriteLine("  senki");
}

static void UjJegy(List<Diak> diakok, string fajl)
{
    Diak? d;
    while (true)
    {
        d = Keres(diakok, BekerSzoveg("Diák azonosítója: "));
        if (d != null) break;
        Hiba("Nincs ilyen azonosító.");
    }
    Console.WriteLine($"{d.Nev} ({d.Osztaly})");

    List<string> tantargyak = Tantargyak(diakok);
    for (int i = 0; i < tantargyak.Count; i++)
        Console.WriteLine($"{i + 1} – {tantargyak[i]}");
    string tantargy = tantargyak[BekerEgesz("Tantárgy: ", 1, tantargyak.Count) - 1];
    int jegy = BekerEgesz("Jegy (1–5): ", 1, 5);

    d.Jegyek.Add(new Jegy(tantargy, jegy));
    File.AppendAllText(fajl, $"{d.Azonosito};{tantargy};{jegy}{Environment.NewLine}");
    Console.WriteLine($"Beírva és mentve. {tantargy} átlaga most: {d.Atlag(tantargy):F2}");
}

static void Rangsor(List<Diak> diakok, string fajl)
{
    List<string> sorok = new List<string>();
    foreach (string o in Osztalyok(diakok))
    {
        List<Diak> osztaly = new List<Diak>();
        foreach (Diak d in diakok)
            if (d.Osztaly == o)
                osztaly.Add(d);
        osztaly.Sort((a, b) => b.Atlag().CompareTo(a.Atlag()));

        sorok.Add($"{o} osztály");
        for (int i = 0; i < osztaly.Count; i++)
            sorok.Add($"{i + 1}. {osztaly[i].Nev} – {osztaly[i].Atlag():F2}");
        sorok.Add("");
    }
    File.WriteAllLines(fajl, sorok);
    Console.WriteLine($"A rangsor elmentve: {fajl}");
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
