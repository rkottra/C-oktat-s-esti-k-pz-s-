// 13-M04-L01-F03 · Mátrix fájlba írása és visszaolvasása – megoldás

Console.OutputEncoding = System.Text.Encoding.UTF8;

const int N = 10;
string fajlnev = "matrix.txt";

// 1. Mátrix feltöltése véletlenszámokkal
Random rnd = new Random();
int[,] matrix = new int[N, N];
for (int i = 0; i < N; i++)
    for (int j = 0; j < N; j++)
        matrix[i, j] = rnd.Next(10, 100);

// 2. Kiírás fájlba: soronként, vesszővel elválasztva, szóköz nélkül
using (StreamWriter sw = new StreamWriter(fajlnev))
{
    for (int i = 0; i < N; i++)
    {
        string sor = "";
        for (int j = 0; j < N; j++)
        {
            sor += matrix[i, j];
            if (j < N - 1) sor += ",";
        }
        sw.WriteLine(sor);
    }
}
Console.WriteLine($"A mátrix kiírva: {fajlnev}\n");

// 3. Visszaolvasás egy MÁSIK mátrixba – így bizonyítjuk, hogy a fájlból dolgozunk
string[] sorok = File.ReadAllLines(fajlnev);
int sorDb = sorok.Length;
int oszlopDb = sorok[0].Split(',').Length;     // a méretet a fájlból állapítjuk meg
int[,] beolvasott = new int[sorDb, oszlopDb];
for (int i = 0; i < sorDb; i++)
{
    string[] elemek = sorok[i].Split(',');
    for (int j = 0; j < oszlopDb; j++)
        beolvasott[i, j] = int.Parse(elemek[j]);
}

// 4. Kiírás igazítva, sorösszegekkel; 5. oszlopösszegek alul
int[] oszlopOsszeg = new int[oszlopDb];
int max = beolvasott[0, 0], maxSor = 0, maxOszlop = 0;
for (int i = 0; i < sorDb; i++)
{
    int sorOsszeg = 0;
    for (int j = 0; j < oszlopDb; j++)
    {
        Console.Write($"{beolvasott[i, j],5}");
        sorOsszeg += beolvasott[i, j];
        oszlopOsszeg[j] += beolvasott[i, j];
        if (beolvasott[i, j] > max)
        {
            max = beolvasott[i, j];
            maxSor = i;
            maxOszlop = j;
        }
    }
    Console.WriteLine($" |{sorOsszeg,6}");
}
Console.WriteLine(new string('-', oszlopDb * 5 + 8));
int teljes = 0;
for (int j = 0; j < oszlopDb; j++)
{
    Console.Write($"{oszlopOsszeg[j],5}");
    teljes += oszlopOsszeg[j];
}
Console.WriteLine($" |{teljes,6}");

// 6. A legnagyobb elem és helye
Console.WriteLine($"\nA legnagyobb elem: {max} ({maxSor + 1}. sor, {maxOszlop + 1}. oszlop)");
Console.WriteLine($"Az elemek átlaga: {(double)teljes / (sorDb * oszlopDb):F2}");
