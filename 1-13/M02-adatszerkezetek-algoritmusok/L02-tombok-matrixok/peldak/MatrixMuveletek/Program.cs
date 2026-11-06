// Kétdimenziós tömbök (mátrixok) bejárása és tipikus műveletei
Console.OutputEncoding = System.Text.Encoding.UTF8;

// --- 1. Mátrix kezdőértékkel (sorfolytonosan) ---
int[,] a = {
    { 1, 2, 3 },
    { 4, 5, 6 },
    { 7, 8, 9 }
};
Console.WriteLine($"A mátrix: {a.GetLength(0)} sor, {a.GetLength(1)} oszlop, összesen {a.Length} elem");
for (int sor = 0; sor < a.GetLength(0); sor++)
{
    for (int oszlop = 0; oszlop < a.GetLength(1); oszlop++)
        Console.Write($"{a[sor, oszlop],4}");
    Console.WriteLine();
}

// --- 2. Véletlen mátrix, sorösszegek a sorok végén, oszlopösszegek alul ---
Random rnd = new Random(7);
int[,] m = new int[4, 6];
for (int sor = 0; sor < m.GetLength(0); sor++)
    for (int oszlop = 0; oszlop < m.GetLength(1); oszlop++)
        m[sor, oszlop] = rnd.Next(0, 100);

Console.WriteLine("\nVéletlen 4×6-os mátrix, sorösszegekkel és oszlopösszegekkel:");
for (int sor = 0; sor < m.GetLength(0); sor++)
{
    int sorOsszeg = 0;                          // minden sornál újrakezdjük
    for (int oszlop = 0; oszlop < m.GetLength(1); oszlop++)
    {
        Console.Write($"{m[sor, oszlop],5}");
        sorOsszeg += m[sor, oszlop];
    }
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine($" |{sorOsszeg,5}");
    Console.ResetColor();
}
Console.WriteLine(new string('-', 5 * m.GetLength(1)));
Console.ForegroundColor = ConsoleColor.Yellow;
for (int oszlop = 0; oszlop < m.GetLength(1); oszlop++)  // külső ciklus az oszlopokon!
{
    int oszlopOsszeg = 0;
    for (int sor = 0; sor < m.GetLength(0); sor++)
        oszlopOsszeg += m[sor, oszlop];
    Console.Write($"{oszlopOsszeg,5}");
}
Console.ResetColor();
Console.WriteLine();

// --- 3. Átlók (négyzetes mátrix) ---
int n = a.GetLength(0);
int foatlo = 0, mellekatlo = 0;
for (int i = 0; i < n; i++)
{
    foatlo += a[i, i];
    mellekatlo += a[i, n - 1 - i];
}
Console.WriteLine($"\nAz A mátrix főátlójának összege: {foatlo}, mellékátlójáé: {mellekatlo}");

// --- 4. Tükrözés a főátlóra helyben: csak a főátló FELETTI elemeket cseréljük ---
for (int sor = 0; sor < n; sor++)
{
    for (int oszlop = sor + 1; oszlop < n; oszlop++)
    {
        int seged = a[sor, oszlop];
        a[sor, oszlop] = a[oszlop, sor];
        a[oszlop, sor] = seged;
    }
}
Console.WriteLine("Transzponálva:");
for (int sor = 0; sor < n; sor++)
{
    for (int oszlop = 0; oszlop < n; oszlop++)
        Console.Write($"{a[sor, oszlop],4}");
    Console.WriteLine();
}

// --- 5. Két mátrix összege: elemenként ---
int[,] b = {
    { 10, 10, 10 },
    { 20, 20, 20 },
    { 30, 30, 30 }
};
int[,] osszeg = new int[n, n];
for (int sor = 0; sor < n; sor++)
    for (int oszlop = 0; oszlop < n; oszlop++)
        osszeg[sor, oszlop] = a[sor, oszlop] + b[sor, oszlop];
Console.WriteLine("A(transzponált) + B:");
for (int sor = 0; sor < n; sor++)
{
    for (int oszlop = 0; oszlop < n; oszlop++)
        Console.Write($"{osszeg[sor, oszlop],4}");
    Console.WriteLine();
}

// --- 6. Ciklikus (latin) négyzet: minden sorban és oszlopban 1..k mindegyike pontosan egyszer ---
int k = 5;
int[,] latin = new int[k, k];
for (int sor = 0; sor < k; sor++)
    for (int oszlop = 0; oszlop < k; oszlop++)
        latin[sor, oszlop] = (sor + oszlop) % k + 1;   // soronként eggyel eltolva
Console.WriteLine($"\n{k}×{k}-es ciklikus négyzet:");
for (int sor = 0; sor < k; sor++)
{
    for (int oszlop = 0; oszlop < k; oszlop++)
        Console.Write($"{latin[sor, oszlop],3}");
    Console.WriteLine();
}

// --- 7. Tömbök tömbje (jagged): soronként eltérő hosszúság ---
int[][] haromszog = new int[4][];
for (int sor = 0; sor < haromszog.Length; sor++)
{
    haromszog[sor] = new int[sor + 1];
    for (int i = 0; i <= sor; i++)
        haromszog[sor][i] = sor + 1;
}
Console.WriteLine("\nTömbök tömbje (Pythonban: listák listája):");
foreach (int[] sor in haromszog)
    Console.WriteLine(string.Join(" ", sor));
