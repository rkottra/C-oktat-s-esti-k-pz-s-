Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.InputEncoding = System.Text.Encoding.UTF8;

string[] sorok =
{
    "Kiss Anna;Szoftverfejleszto;48;41",
    "Nagy Bence;Rendszergazda;39;35",
    "Tóth Csaba;Szoftverfejleszto;45;44",
    "Szabó Dóra;Grafikus;50;38",
    "Horváth Emese;Szoftverfejleszto;37;29",
    "Varga Gergő;Rendszergazda;44;47",
    "Molnár Hanna;Grafikus;41;45",
    "Farkas Ivett;Szoftverfejleszto;49;46",
    "Balogh János;Rendszergazda;30;26",
    "Papp Kitti;Grafikus;35;31",
    "Lakatos Márk;Szoftverfejleszto;42;44",
    "Fekete Nóra;Rendszergazda;46;40",
};
int[] keret = { 3, 2, 2 };   // Szak sorrendjében

// 2. Beolvasás listába
List<Jelentkezo> jelentkezok = new List<Jelentkezo>();
foreach (string sor in sorok)
    jelentkezok.Add(Feldolgoz(sor));

// 3. Létszám és átlag szakonként
Console.WriteLine($"Jelentkezők: {jelentkezok.Count}");
foreach (Szak sz in Enum.GetValues<Szak>())
    Console.WriteLine($"  {sz}: {Letszam(jelentkezok, sz)} fő, átlag {Atlag(jelentkezok, sz):F1} pont");

// 4. Maximum
Jelentkezo legjobb = Legjobb(jelentkezok);
Console.WriteLine($"Legmagasabb összpontszám: {legjobb.Nev} ({legjobb.Szak}), {legjobb.Osszpont()} pont");

// 5. Kiválogatás
List<Jelentkezo> javitok = IrasbelinJobb(jelentkezok);
if (javitok.Count == 0)
{
    Console.WriteLine("Senkinek sem volt jobb az írásbelije a hozott pontjánál.");
}
else
{
    List<string> nevek = new List<string>();
    foreach (Jelentkezo j in javitok)
        nevek.Add(j.Nev);
    Console.WriteLine($"Írásbelin jobb, mint a hozott pont: {string.Join(", ", nevek)}");
}

// 6. Rangsor szakonként
foreach (Szak sz in Enum.GetValues<Szak>())
{
    Console.WriteLine();
    Console.WriteLine($"{sz} (keret: {keret[(int)sz]})");
    List<Jelentkezo> rangsor = Rangsor(jelentkezok, sz);
    for (int i = 0; i < rangsor.Count; i++)
    {
        string allapot = i < keret[(int)sz] ? "FELVÉVE" : "várólista";
        Console.WriteLine($"  {i + 1}. {rangsor[i].Nev,-20}{rangsor[i].Osszpont(),4}  {allapot}");
    }
}

// 7. Keresés név szerint
Console.WriteLine();
Console.Write("Név: ");
string keresett = (Console.ReadLine() ?? "").Trim();
int hely = Kereses(jelentkezok, keresett);
if (hely == -1)
{
    Console.WriteLine("Nincs ilyen jelentkező.");
}
else
{
    Jelentkezo j = jelentkezok[hely];
    List<Jelentkezo> rangsor = Rangsor(jelentkezok, j.Szak);
    int helyezes = Kereses(rangsor, j.Nev) + 1;
    string felveve = helyezes <= keret[(int)j.Szak] ? "felvéve" : "várólistán";
    Console.WriteLine($"{j.Nev} – {j.Szak}, {j.Osszpont()} pont, {helyezes}. hely, {felveve}");
}

// ---------- Metódusok ----------

static Jelentkezo Feldolgoz(string sor)
{
    string[] d = sor.Split(';');
    return new Jelentkezo(d[0], Enum.Parse<Szak>(d[1]), int.Parse(d[2]), int.Parse(d[3]));
}

// Megszámlálás
static int Letszam(List<Jelentkezo> lista, Szak szak)
{
    int db = 0;
    foreach (Jelentkezo j in lista)
        if (j.Szak == szak)
            db++;
    return db;
}

// Összegzés + megszámlálás egy szakra
static double Atlag(List<Jelentkezo> lista, Szak szak)
{
    int osszeg = 0, db = 0;
    foreach (Jelentkezo j in lista)
        if (j.Szak == szak)
        {
            osszeg += j.Osszpont();
            db++;
        }
    return db == 0 ? 0 : (double)osszeg / db;
}

// Maximumkiválasztás
static Jelentkezo Legjobb(List<Jelentkezo> lista)
{
    int maxI = 0;
    for (int i = 1; i < lista.Count; i++)
        if (lista[i].Osszpont() > lista[maxI].Osszpont())
            maxI = i;
    return lista[maxI];
}

// Kiválogatás
static List<Jelentkezo> IrasbelinJobb(List<Jelentkezo> lista)
{
    List<Jelentkezo> eredmeny = new List<Jelentkezo>();
    foreach (Jelentkezo j in lista)
        if (j.Irasbeli > j.Hozott)
            eredmeny.Add(j);
    return eredmeny;
}

// Kiválogatás egy szakra, majd rendezés: összpont, aztán írásbeli szerint csökkenően
static List<Jelentkezo> Rangsor(List<Jelentkezo> lista, Szak szak)
{
    List<Jelentkezo> rangsor = new List<Jelentkezo>();
    foreach (Jelentkezo j in lista)
        if (j.Szak == szak)
            rangsor.Add(j);
    rangsor.Sort((a, b) => b.Osszpont() != a.Osszpont()
        ? b.Osszpont().CompareTo(a.Osszpont())
        : b.Irasbeli.CompareTo(a.Irasbeli));
    return rangsor;
}

// Keresés kisbetű-függetlenül; -1, ha nincs
static int Kereses(List<Jelentkezo> lista, string nev)
{
    int i = 0;
    while (i < lista.Count && !string.Equals(lista[i].Nev, nev, StringComparison.OrdinalIgnoreCase))
        i++;
    return i < lista.Count ? i : -1;
}

// ---------- Típusok ----------

enum Szak { Szoftverfejleszto, Rendszergazda, Grafikus }

struct Jelentkezo
{
    public string Nev;
    public Szak Szak;
    public int Hozott;
    public int Irasbeli;

    public Jelentkezo(string nev, Szak szak, int hozott, int irasbeli)
    {
        Nev = nev;
        Szak = szak;
        Hozott = hozott;
        Irasbeli = irasbeli;
    }

    public int Osszpont() => Hozott + Irasbeli;
}
