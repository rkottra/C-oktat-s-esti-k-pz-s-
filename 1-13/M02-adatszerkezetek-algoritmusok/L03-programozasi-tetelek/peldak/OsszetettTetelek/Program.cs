// Sorozatot előállító tételek és két egyszerű rendezés.
Console.OutputEncoding = System.Text.Encoding.UTF8;

int[] a = { 12, 7, 33, 4, 18, 25, 9, 40, 15, 2 };
int[] b = { 9, 18, 3, 40, 21, 7, 30 };
Console.WriteLine($"A: {string.Join(" ", a)}");
Console.WriteLine($"B: {string.Join(" ", b)}");
Console.WriteLine();

// ---- Kiválogatás: A páros elemei ----
int[] parosak = new int[a.Length];
int db = 0;
for (int i = 0; i < a.Length; i++)
    if (a[i] % 2 == 0)
        parosak[db++] = a[i];
Array.Resize(ref parosak, db);
Console.WriteLine($"Kiválogatás – A páros elemei: {string.Join(" ", parosak)}");

// ---- Szétválogatás: A elemei 15 alatt / 15 vagy felett ----
int[] kicsik = new int[a.Length];
int[] nagyok = new int[a.Length];
int dbK = 0, dbN = 0;
for (int i = 0; i < a.Length; i++)
{
    if (a[i] < 15)
        kicsik[dbK++] = a[i];
    else
        nagyok[dbN++] = a[i];
}
Array.Resize(ref kicsik, dbK);
Array.Resize(ref nagyok, dbN);
Console.WriteLine($"Szétválogatás – 15 alatt: {string.Join(" ", kicsik)} | 15 vagy több: {string.Join(" ", nagyok)}");

// ---- Metszet: A elemei, amelyek B-ben is benne vannak (kiválogatás + eldöntés) ----
int[] metszet = new int[Math.Min(a.Length, b.Length)];
int dbM = 0;
for (int i = 0; i < a.Length; i++)
{
    int j = 0;
    while (j < b.Length && b[j] != a[i])
        j++;
    if (j < b.Length)
        metszet[dbM++] = a[i];
}
Array.Resize(ref metszet, dbM);
Console.WriteLine($"Metszet: {string.Join(" ", metszet)}   (LINQ: {string.Join(" ", a.Intersect(b))})");

// ---- Unió: A összes eleme + B-ből azok, amelyek A-ban nincsenek ----
int[] unio = new int[a.Length + b.Length];
int dbU = 0;
for (int i = 0; i < a.Length; i++)
    unio[dbU++] = a[i];
for (int j = 0; j < b.Length; j++)
{
    int i = 0;
    while (i < a.Length && a[i] != b[j])
        i++;
    if (i == a.Length)          // nincs benne A-ban
        unio[dbU++] = b[j];
}
Array.Resize(ref unio, dbU);
Console.WriteLine($"Unió: {string.Join(" ", unio)}   (LINQ: {string.Join(" ", a.Union(b))})");
Console.WriteLine();

// ---- Minimumkiválasztásos rendezés (másolaton, hogy A megmaradjon) ----
int[] r1 = (int[])a.Clone();
for (int i = 0; i < r1.Length - 1; i++)
{
    int minI = i;
    for (int j = i + 1; j < r1.Length; j++)
        if (r1[j] < r1[minI])
            minI = j;
    (r1[i], r1[minI]) = (r1[minI], r1[i]);
    Console.WriteLine($"  {i + 1}. menet után: {string.Join(" ", r1)}");
}
Console.WriteLine($"Minimumkiválasztásos rendezés: {string.Join(" ", r1)}");
Console.WriteLine();

// ---- Buborékrendezés, csökkenő sorrendbe ----
int[] r2 = (int[])b.Clone();
for (int vege = r2.Length - 1; vege > 0; vege--)
    for (int j = 0; j < vege; j++)
        if (r2[j] < r2[j + 1])      // csökkenőhöz a reláció fordított
            (r2[j], r2[j + 1]) = (r2[j + 1], r2[j]);
Console.WriteLine($"Buborékrendezés (csökkenő) – B: {string.Join(" ", r2)}");
