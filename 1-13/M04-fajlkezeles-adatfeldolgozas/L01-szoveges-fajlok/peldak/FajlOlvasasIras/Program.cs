// Szöveges fájlok olvasása és írása – a File osztály és a StreamReader/StreamWriter

Console.OutputEncoding = System.Text.Encoding.UTF8;

string fajlnev = "bevasarlas.txt";

// --- 0. Létezik a fájl? ---
if (!File.Exists(fajlnev))
{
    Console.WriteLine($"Nem találom a fájlt: {Path.GetFullPath(fajlnev)}");
    Console.WriteLine("Ellenőrizd a Copy to Output Directory beállítást!");
    return;
}

// --- 1. Az egész fájl egyszerre: File.ReadAllLines ---
string[] sorok = File.ReadAllLines(fajlnev);
Console.WriteLine($"1) ReadAllLines – {sorok.Length} sor:");
for (int i = 0; i < sorok.Length; i++)
{
    Console.WriteLine($"   {i + 1}. {sorok[i]}");
}

// --- 2. Soronként: StreamReader (a using a végén bezárja a fájlt) ---
Console.WriteLine("\n2) StreamReader – a legalább 5 betűs tételek:");
using (StreamReader sr = new StreamReader(fajlnev))
{
    while (!sr.EndOfStream)
    {
        string sor = sr.ReadLine()!;
        if (sor.Length >= 5)
            Console.WriteLine($"   {sor}");
    }
}

// --- 3. Írás egyben: File.WriteAllLines (felülírja a fájlt) ---
List<string> nagybetus = new List<string>();
foreach (string sor in sorok)
{
    nagybetus.Add(sor.ToUpper());
}
File.WriteAllLines("bevasarlas_nagybetus.txt", nagybetus);
Console.WriteLine("\n3) Elkészült: bevasarlas_nagybetus.txt");

// --- 4. Írás soronként: StreamWriter, formázott sorokkal ---
using (StreamWriter sw = new StreamWriter("szorzotabla.txt"))
{
    for (int i = 1; i <= 10; i++)
    {
        for (int j = 1; j <= 10; j++)
        {
            sw.Write($"{i * j,4}");        // 4 karakter széles, jobbra igazítva
        }
        sw.WriteLine();                     // sor vége
    }
}
Console.WriteLine("4) Elkészült: szorzotabla.txt");

// --- 5. Hozzáfűzés: AppendAllText (naplózás) ---
File.AppendAllText("naplo.txt", $"{DateTime.Now:yyyy.MM.dd HH:mm:ss} – a program lefutott{Environment.NewLine}");
int futasokSzama = File.ReadAllLines("naplo.txt").Length;
Console.WriteLine($"5) naplo.txt – eddig {futasokSzama} futás");

// Hol vannak a kiírt fájlok?
Console.WriteLine($"\nA fájlok helye: {Path.GetFullPath(".")}");
