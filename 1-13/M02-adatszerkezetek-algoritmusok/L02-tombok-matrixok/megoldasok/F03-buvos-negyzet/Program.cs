Console.OutputEncoding = System.Text.Encoding.UTF8;

// ---------- A) Beolvasás és ellenőrzés ----------
int n;
Console.Write("n = ");
while (!int.TryParse(Console.ReadLine(), out n) || n < 3 || n > 9)
    Console.Write("3 és 9 közötti egész számot adj meg! n = ");

int[,] negyzet = new int[n, n];
for (int s = 0; s < n; s++)
{
    bool jo = false;
    while (!jo)
    {
        Console.Write($"{s + 1}. sor: ");
        string[] darabok = (Console.ReadLine() ?? "").Split(' ', StringSplitOptions.RemoveEmptyEntries);
        jo = darabok.Length == n;
        for (int o = 0; jo && o < n; o++)
            jo = int.TryParse(darabok[o], out negyzet[s, o]);
        if (!jo)
            Console.WriteLine($"Pontosan {n} egész számot adj meg szóközzel elválasztva!");
    }
}

Kiir(negyzet);
Eredmeny(negyzet);

// ---------- B) Előállítás sziámi módszerrel ----------
int pn = n % 2 == 1 ? n : 5;          // páros n esetén egy 5×5-ös példát mutatunk
int[,] sziami = new int[pn, pn];
int sor = 0, oszlop = pn / 2;
for (int szam = 1; szam <= pn * pn; szam++)
{
    sziami[sor, oszlop] = szam;
    int ujSor = (sor - 1 + pn) % pn;
    int ujOszlop = (oszlop + 1) % pn;
    if (sziami[ujSor, ujOszlop] != 0)   // foglalt: az előző alá
    {
        ujSor = (sor + 1) % pn;
        ujOszlop = oszlop;
    }
    sor = ujSor;
    oszlop = ujOszlop;
}

Console.WriteLine();
Console.WriteLine($"Sziámi módszerrel előállított {pn}×{pn}-ös négyzet:");
Kiir(sziami);
Eredmeny(sziami);

// ---------- Lokális függvények ----------

// Kiírja a négyzetet a sor- és oszlopösszegekkel, valamint az átlók összegével
static void Kiir(int[,] q)
{
    int n = q.GetLength(0);
    for (int s = 0; s < n; s++)
    {
        int sorOsszeg = 0;
        for (int o = 0; o < n; o++)
        {
            Console.Write($"{q[s, o],4}");
            sorOsszeg += q[s, o];
        }
        Console.WriteLine($" | {sorOsszeg,3}");
    }
    Console.WriteLine(" " + new string('-', 4 * n));
    for (int o = 0; o < n; o++)
    {
        int oszlopOsszeg = 0;
        for (int s = 0; s < n; s++)
            oszlopOsszeg += q[s, o];
        Console.Write($"{oszlopOsszeg,4}");
    }
    Console.WriteLine();

    int fo = 0, mellek = 0;
    for (int i = 0; i < n; i++)
    {
        fo += q[i, i];
        mellek += q[i, n - 1 - i];
    }
    Console.WriteLine($"Főátló: {fo}, mellékátló: {mellek}");
}

// Visszaadja az első hibát szövegesen, vagy null-t, ha a négyzet bűvös
static string? Ellenoriz(int[,] q)
{
    int n = q.GetLength(0);
    int buvos = n * (n * n + 1) / 2;

    // Minden szám 1..n² között, és mindegyik csak egyszer
    bool[] volt = new bool[n * n + 1];
    for (int s = 0; s < n; s++)
        for (int o = 0; o < n; o++)
        {
            int x = q[s, o];
            if (x < 1 || x > n * n)
                return $"A {x} szám kívül esik az 1..{n * n} tartományon.";
            if (volt[x])
                return $"A {x} szám többször szerepel.";
            volt[x] = true;
        }

    for (int s = 0; s < n; s++)
    {
        int osszeg = 0;
        for (int o = 0; o < n; o++)
            osszeg += q[s, o];
        if (osszeg != buvos)
            return $"A {s + 1}. sor összege {osszeg}, nem {buvos}.";
    }

    for (int o = 0; o < n; o++)
    {
        int osszeg = 0;
        for (int s = 0; s < n; s++)
            osszeg += q[s, o];
        if (osszeg != buvos)
            return $"A {o + 1}. oszlop összege {osszeg}, nem {buvos}.";
    }

    int fo = 0, mellek = 0;
    for (int i = 0; i < n; i++)
    {
        fo += q[i, i];
        mellek += q[i, n - 1 - i];
    }
    if (fo != buvos) return $"A főátló összege {fo}, nem {buvos}.";
    if (mellek != buvos) return $"A mellékátló összege {mellek}, nem {buvos}.";

    return null;
}

static void Eredmeny(int[,] q)
{
    int n = q.GetLength(0);
    string? hiba = Ellenoriz(q);
    if (hiba == null)
        Console.WriteLine($"A négyzet bűvös (bűvös összeg: {n * (n * n + 1) / 2}).");
    else
        Console.WriteLine($"A négyzet nem bűvös. {hiba}");
}
