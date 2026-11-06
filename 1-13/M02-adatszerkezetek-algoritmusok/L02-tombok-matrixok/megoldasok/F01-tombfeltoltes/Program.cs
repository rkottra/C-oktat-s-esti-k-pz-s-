Console.OutputEncoding = System.Text.Encoding.UTF8;

// 1. Feltöltés képlettel
int[] parosok = new int[10];
int[] negyzetek = new int[10];
for (int i = 0; i < parosok.Length; i++)
{
    parosok[i] = 2 * (i + 1);
    negyzetek[i] = (i + 1) * (i + 1);
}
Console.WriteLine($"Páros számok: {string.Join(", ", parosok)}");
Console.WriteLine($"Négyzetszámok: {string.Join(", ", negyzetek)}");
Console.WriteLine();

// 2. Feltöltés véletlenszámokkal – egyetlen Random objektum a ciklus előtt
Random rnd = new Random();
int[] veletlen = new int[15];
for (int i = 0; i < veletlen.Length; i++)
    veletlen[i] = rnd.Next(1, 101);   // 1..100, a felső határ kizárt

Console.WriteLine($"Véletlen tömb: {string.Join(", ", veletlen)}");
Console.WriteLine($"Összeg: {veletlen.Sum()}, legkisebb: {veletlen.Min()}, legnagyobb: {veletlen.Max()}");
Array.Sort(veletlen);
Console.WriteLine($"Rendezve: {string.Join(", ", veletlen)}");
Console.WriteLine();

// 3. Feltöltés billentyűzetről, ellenőrzött beolvasással
int[] homersekletek = new int[7];
for (int i = 0; i < homersekletek.Length; i++)
{
    int ertek;
    Console.Write($"{i + 1}. nap hőmérséklete: ");
    while (!int.TryParse(Console.ReadLine(), out ertek))
    {
        Console.WriteLine("Egész számot adj meg!");
        Console.Write($"{i + 1}. nap hőmérséklete: ");
    }
    homersekletek[i] = ertek;
}

int osszeg = 0;
foreach (int h in homersekletek)
    osszeg += h;
double atlag = (double)osszeg / homersekletek.Length;
Console.WriteLine($"Heti átlag: {atlag:F1} °C");

Console.Write("Átlag feletti napok hőmérséklete:");
foreach (int h in homersekletek)
    if (h > atlag)
        Console.Write($" {h}");
Console.WriteLine();
