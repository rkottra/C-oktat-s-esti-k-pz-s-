Console.OutputEncoding = System.Text.Encoding.UTF8;

const int N = 40;
const int Sorok = 10;
const int Lepcso = 150;

// Adatok előállítása fix maggal – így minden futáskor ugyanazt kapjuk
Random rnd = new Random(21);
int[] m = new int[N];
m[0] = rnd.Next(0, 301);
for (int i = 1; i < N; i++)
{
    int uj = m[i - 1] + rnd.Next(-150, 151);
    if (uj < 0) uj = 0;
    if (uj > 1500) uj = 1500;
    m[i] = uj;
}

// 1. Kiírás
Console.WriteLine($"Mérések: {string.Join(" ", m)}");

// 2. Maximumkiválasztás – az indexet tároljuk
int maxIndex = 0;
for (int i = 1; i < N; i++)
    if (m[i] > m[maxIndex])
        maxIndex = i;
Console.WriteLine($"Legnagyobb magasság: {m[maxIndex]} m, először a {maxIndex + 1}. km-nél");

// 3. Keresés: van-e 0 érték?
int t = 0;
while (t < N && m[t] != 0)
    t++;
if (t < N)
    Console.WriteLine($"Tenger fölött: igen, először a {t + 1}. km-nél");
else
    Console.WriteLine("Tenger fölött: nem");

// 4. Csúcsok megszámlálása – az első és az utolsó elem kimarad
int csucsok = 0;
for (int i = 1; i < N - 1; i++)
    if (m[i] > m[i - 1] && m[i] > m[i + 1])
        csucsok++;
Console.WriteLine($"Csúcsok száma: {csucsok}");

// 5. Leghosszabb szigorúan emelkedő szakasz
int aktHossz = 1, legjobbHossz = 1, legjobbVeg = 0;
for (int i = 1; i < N; i++)
{
    if (m[i] > m[i - 1])
        aktHossz++;
    else
        aktHossz = 1;

    if (aktHossz > legjobbHossz)
    {
        legjobbHossz = aktHossz;
        legjobbVeg = i;
    }
}
int legjobbKezd = legjobbVeg - legjobbHossz + 1;
Console.WriteLine($"Leghosszabb emelkedő: {legjobbKezd + 1}. km – {legjobbVeg + 1}. km ({legjobbHossz} mérés)");

// 6. Profil rajzolása mátrixba
char[,] profil = new char[Sorok, N];
for (int s = 0; s < Sorok; s++)
    for (int o = 0; o < N; o++)
        profil[s, o] = ' ';

for (int o = 0; o < N; o++)
{
    if (m[o] == 0)
    {
        profil[Sorok - 1, o] = '~';
        continue;
    }
    int magassag = (m[o] + Lepcso - 1) / Lepcso;   // felfelé kerekítés
    if (magassag > Sorok) magassag = Sorok;
    for (int k = 0; k < magassag; k++)
        profil[Sorok - 1 - k, o] = '#';              // alulról felfelé
}

Console.WriteLine();
Console.WriteLine("Profil:");
for (int s = 0; s < Sorok; s++)
{
    for (int o = 0; o < N; o++)
        Console.Write(profil[s, o]);
    Console.WriteLine();
}
