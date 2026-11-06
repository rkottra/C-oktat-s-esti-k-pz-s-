// enum, struct és List<T> – egy kis könyvtári nyilvántartás.
Console.OutputEncoding = System.Text.Encoding.UTF8;

// --- List<T> alapműveletek egyszerű típuson ---
List<string> szinek = new List<string> { "piros", "zöld" };
szinek.Add("kék");
szinek.Insert(0, "sárga");
szinek.Remove("zöld");
Console.WriteLine($"Színek ({szinek.Count}): {string.Join(", ", szinek)}");
Console.WriteLine($"Van kék? {szinek.Contains("kék")}, hol? {szinek.IndexOf("kék")}");
szinek.Sort();
Console.WriteLine($"Rendezve: {string.Join(", ", szinek)}");
Console.WriteLine();

// --- Lista struktúrákból ---
List<Konyv> konyvek = new List<Konyv>
{
    new Konyv("Egri csillagok", "Gárdonyi Géza", 1899, Mufaj.Regeny),
    new Konyv("A Pál utcai fiúk", "Molnár Ferenc", 1906, Mufaj.Ifjusagi),
    new Konyv("Toldi", "Arany János", 1846, Mufaj.Vers),
    new Konyv("Az ember tragédiája", "Madách Imre", 1861, Mufaj.Drama),
    new Konyv("Légy jó mindhalálig", "Móricz Zsigmond", 1920, Mufaj.Ifjusagi),
    new Konyv("Szent Péter esernyője", "Mikszáth Kálmán", 1895, Mufaj.Regeny),
};
konyvek.Add(new Konyv("A kőszívű ember fiai", "Jókai Mór", 1869, Mufaj.Regeny));

Console.WriteLine("Az összes könyv:");
foreach (Konyv k in konyvek)
    Console.WriteLine($"  {k}");

// --- Programozási tételek listán: Count a Length helyett ---
int regenyek = 0;
foreach (Konyv k in konyvek)
    if (k.Mufaj == Mufaj.Regeny)
        regenyek++;
Console.WriteLine($"\nRegények száma: {regenyek}");

int legregebbi = 0;
for (int i = 1; i < konyvek.Count; i++)
    if (konyvek[i].Ev < konyvek[legregebbi].Ev)
        legregebbi = i;
Console.WriteLine($"A legrégebbi: {konyvek[legregebbi]}");

// Kiválogatás új listába – nem kell előre méretezni
List<Konyv> huszadik = new List<Konyv>();
foreach (Konyv k in konyvek)
    if (k.Ev >= 1900)
        huszadik.Add(k);
Console.WriteLine($"1900-tól megjelent: {huszadik.Count} db");

// --- enum bejárása és switch ---
Console.WriteLine("\nMűfajonként:");
foreach (Mufaj m in Enum.GetValues<Mufaj>())
{
    int db = 0;
    foreach (Konyv k in konyvek)
        if (k.Mufaj == m) db++;
    Console.WriteLine($"  {MufajNev(m),-10} {db} db");
}

// --- Rendezés mező szerint ---
konyvek.Sort((a, b) => a.Ev.CompareTo(b.Ev));
Console.WriteLine("\nMegjelenés szerint rendezve:");
foreach (Konyv k in konyvek)
    Console.WriteLine($"  {k.Ev} {k.Cim}");

// --- Struct módosítása listában: másolat → módosítás → visszaírás ---
// konyvek[0].Ev = 1847;   // fordítási hiba: az indexelés másolatot ad
Konyv elso = konyvek[0];
elso.Ev = 1847;
konyvek[0] = elso;
Console.WriteLine($"\nJavítva: {konyvek[0]}");

// --- Törlés feltétel szerint ---
konyvek.RemoveAll(k => k.Mufaj == Mufaj.Drama);
Console.WriteLine($"A drámák törlése után {konyvek.Count} könyv maradt.");

// ---------- Metódus és típusdefiníciók a fájl végén ----------

static string MufajNev(Mufaj m)
{
    switch (m)
    {
        case Mufaj.Regeny: return "regény";
        case Mufaj.Ifjusagi: return "ifjúsági";
        case Mufaj.Vers: return "verses";
        case Mufaj.Drama: return "dráma";
        default: return "?";
    }
}

enum Mufaj { Regeny, Ifjusagi, Vers, Drama }

struct Konyv
{
    public string Cim;
    public string Szerzo;
    public int Ev;
    public Mufaj Mufaj;

    public Konyv(string cim, string szerzo, int ev, Mufaj mufaj)
    {
        Cim = cim;
        Szerzo = szerzo;
        Ev = ev;
        Mufaj = mufaj;
    }

    public override string ToString() => $"{Szerzo}: {Cim} ({Ev}, {Mufaj})";
}
