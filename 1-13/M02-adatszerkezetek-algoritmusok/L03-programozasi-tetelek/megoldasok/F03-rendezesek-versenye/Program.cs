using System.Diagnostics;

Console.OutputEncoding = System.Text.Encoding.UTF8;

Random rnd = new Random();

// 1. Bemutató egy kis tömbön – mindegyik rendezés másolatot kap
int[] kicsi = new int[10];
for (int i = 0; i < kicsi.Length; i++)
    kicsi[i] = rnd.Next(1, 100);

int[] m1 = (int[])kicsi.Clone(); Minimum(m1);
int[] m2 = (int[])kicsi.Clone(); Buborek(m2);
int[] m3 = (int[])kicsi.Clone(); Beszuras(m3);
Console.WriteLine($"Eredeti:      {string.Join(" ", kicsi)}");
Console.WriteLine($"Minimumkiv.:  {string.Join(" ", m1)}");
Console.WriteLine($"Buborék:      {string.Join(" ", m2)}");
Console.WriteLine($"Beszúrásos:   {string.Join(" ", m3)}");

// 2. Mérés véletlen tömbökön
foreach (int n in new[] { 1000, 2000, 4000 })
{
    int[] t = new int[n];
    for (int i = 0; i < n; i++)
        t[i] = rnd.Next(1, 1_000_000);
    Meres($"Véletlen tömb, n = {n}", t);
}

// 3. Mérés már rendezett tömbön
int[] rendezett = new int[4000];
for (int i = 0; i < rendezett.Length; i++)
    rendezett[i] = i;
Meres("Rendezett tömb, n = 4000", rendezett);

// ---------- Függvények ----------

static void Meres(string cim, int[] eredeti)
{
    Console.WriteLine();
    Console.WriteLine(cim);
    Console.WriteLine($"{"Algoritmus",-12}{"Összehasonlítás",18}{"Mozgatás",12}{"Idő (ms)",13}");

    Sor("Minimumkiv.", eredeti, Minimum);
    Sor("Buborék", eredeti, Buborek);
    Sor("Beszúrásos", eredeti, Beszuras);

    int[] masolat = (int[])eredeti.Clone();
    Stopwatch sw = Stopwatch.StartNew();
    Array.Sort(masolat);
    sw.Stop();
    Console.WriteLine($"{"Array.Sort",-12}{"–",18}{"–",12}{sw.Elapsed.TotalMilliseconds,13:F2}");
}

// Egy algoritmus lefuttatása másolaton, és az eredmény egy táblázatsorban
static void Sor(string nev, int[] eredeti, Func<int[], (long, long)> rendezes)
{
    int[] masolat = (int[])eredeti.Clone();
    Stopwatch sw = Stopwatch.StartNew();
    (long hasonlit, long mozgat) = rendezes(masolat);
    sw.Stop();
    Console.WriteLine($"{nev,-12}{hasonlit,18}{mozgat,12}{sw.Elapsed.TotalMilliseconds,13:F2}");
}

static (long, long) Minimum(int[] t)
{
    long hasonlit = 0, mozgat = 0;
    for (int i = 0; i < t.Length - 1; i++)
    {
        int minI = i;
        for (int j = i + 1; j < t.Length; j++)
        {
            hasonlit++;
            if (t[j] < t[minI])
                minI = j;
        }
        if (minI != i)
        {
            (t[i], t[minI]) = (t[minI], t[i]);
            mozgat += 3;
        }
    }
    return (hasonlit, mozgat);
}

static (long, long) Buborek(int[] t)
{
    long hasonlit = 0, mozgat = 0;
    for (int vege = t.Length - 1; vege > 0; vege--)
    {
        bool voltCsere = false;
        for (int j = 0; j < vege; j++)
        {
            hasonlit++;
            if (t[j] > t[j + 1])
            {
                (t[j], t[j + 1]) = (t[j + 1], t[j]);
                mozgat += 3;
                voltCsere = true;
            }
        }
        if (!voltCsere)
            break;              // rendezett – nincs több dolgunk
    }
    return (hasonlit, mozgat);
}

static (long, long) Beszuras(int[] t)
{
    long hasonlit = 0, mozgat = 0;
    for (int i = 1; i < t.Length; i++)
    {
        int elem = t[i];
        int j = i - 1;
        while (j >= 0)
        {
            hasonlit++;
            if (t[j] <= elem)
                break;
            t[j + 1] = t[j];    // léptetés jobbra
            mozgat++;
            j--;
        }
        if (j + 1 != i)
        {
            t[j + 1] = elem;    // beírás a helyére
            mozgat++;
        }
    }
    return (hasonlit, mozgat);
}

// Válaszok:
// - Az elemszám duplázásakor az összehasonlítások száma kb. a négyszeresére nő (n²-tel arányos),
//   az idő is nagyjából négyszereződik. Az Array.Sort n·log n-es, ezért nagyságrendekkel gyorsabb.
// - Rendezett tömbön a javított buborék egyetlen menet (n-1 összehasonlítás) után leáll,
//   és a beszúrásos rendezés is csak n-1 összehasonlítást végez, mozgatás nélkül.
//   A minimumkiválasztásos rendezés ilyenkor is végignézi az összes párt.
