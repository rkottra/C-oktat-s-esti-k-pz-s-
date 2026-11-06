// 13-M01-L02-F02 · Prímszámok

// ---------- 1. Prímteszt ----------
Cim("1. Prímteszt");
Console.Write("Szám: ");
int n = int.Parse(Console.ReadLine()!);

if (Prim(n))
{
    Console.WriteLine($"{n} prím.");
}
else
{
    Console.Write($"{n} nem prím. Osztói:");
    for (int oszto = 2; oszto < n; oszto++)
    {
        if (n % oszto == 0)
        {
            Console.Write($" {oszto}");
        }
    }
    Console.WriteLine();
}

// ---------- 2. Az első 50 prím ----------
Cim("\n2. Az első 50 prím");
int talalt = 0;
int szam = 2;
while (talalt < 50)
{
    if (Prim(szam))
    {
        talalt++;
        Console.Write($"{szam,6}");
        if (talalt % 10 == 0)
        {
            Console.WriteLine();
        }
    }
    szam++;
}

// ---------- 3. Az 5000. prím ----------
Cim("\n3. Az 5000. prím");
talalt = 0;
szam = 1;
while (talalt < 5000)
{
    szam++;
    if (Prim(szam))
    {
        talalt++;
    }
}
Console.WriteLine(szam);

// ---------- 4. Ikerprímek ----------
Cim("\n4. Ikerprímek 200 alatt");
for (int p = 3; p + 2 < 200; p += 2)     // páros szám (2 kivételével) nem prím, ezért kettesével lépünk
{
    if (Prim(p) && Prim(p + 2))
    {
        Console.Write($"({p}, {p + 2}) ");
    }
}
Console.WriteLine();


// ---------- Segédfüggvények (helyi függvények a fájl végén) ----------
// Pythonban: def prim(n): ...   – C#-ban a paraméter és a visszatérési érték típusát is meg kell adni.
static bool Prim(int n)
{
    if (n < 2)
    {
        return false;
    }
    for (int oszto = 2; oszto * oszto <= n; oszto++)   // elég √n-ig vizsgálni
    {
        if (n % oszto == 0)
        {
            return false;
        }
    }
    return true;
}

static void Cim(string szoveg)
{
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine(szoveg);
    Console.ResetColor();
}
