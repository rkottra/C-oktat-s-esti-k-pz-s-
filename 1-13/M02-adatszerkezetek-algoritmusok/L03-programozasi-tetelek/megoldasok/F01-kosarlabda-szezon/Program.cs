Console.OutputEncoding = System.Text.Encoding.UTF8;

int[] sajat    = { 78, 65, 91, 70, 84, 59, 102, 88, 73, 95, 67, 81, 99, 76, 85, 90 };
int[] ellenfel = { 72, 70, 85, 70, 61, 66,  98, 90, 80, 74, 69, 79, 80, 71, 85, 87 };
int n = sajat.Length;

// 1. Összegzés
int osszes = 0;
for (int i = 0; i < n; i++)
    osszes += sajat[i];
Console.WriteLine($"1. Összesen {osszes} pont, átlagosan {(double)osszes / n:F2} pont mérkőzésenként.");

// 2. Három megszámlálás egy ciklusban
int gyozelem = 0, dontetlen = 0, vereseg = 0;
for (int i = 0; i < n; i++)
{
    if (sajat[i] > ellenfel[i]) gyozelem++;
    else if (sajat[i] == ellenfel[i]) dontetlen++;
    else vereseg++;
}
Console.WriteLine($"2. Győzelem: {gyozelem}, döntetlen: {dontetlen}, vereség: {vereseg}");

// 3. Eldöntés
int e = 0;
while (e < n && sajat[e] < 100)
    e++;
Console.WriteLine($"3. 100 pontos mérkőzés: {(e < n ? "volt" : "nem volt")}");

// 4. Kiválasztás – biztosan van vereség, nem kell i < n
int k = 0;
while (!(sajat[k] < ellenfel[k]))
    k++;
Console.WriteLine($"4. Az első vereség a {k + 1}. mérkőzésen volt ({sajat[k]}:{ellenfel[k]}).");

// 5. Keresés
int s = 0;
while (s < n && !(sajat[s] - ellenfel[s] > 20))
    s++;
if (s < n)
    Console.WriteLine($"5. 20 pontnál nagyobb győzelem: a {s + 1}. mérkőzésen, {sajat[s]}:{ellenfel[s]}");
else
    Console.WriteLine("5. 20 pontnál nagyobb győzelem: nem volt");

// 6. Maximumkiválasztás a különbségeken
int maxI = 0;
for (int i = 1; i < n; i++)
    if (sajat[i] - ellenfel[i] > sajat[maxI] - ellenfel[maxI])
        maxI = i;
Console.WriteLine($"6. A legnagyobb győzelem: a {maxI + 1}. mérkőzés, {sajat[maxI]}:{ellenfel[maxI]} (+{sajat[maxI] - ellenfel[maxI]})");

// 7. Eldöntés „minden” változatban: van-e olyan, ami NEM felel meg?
int m = 0;
while (m < n && sajat[m] >= 60)
    m++;
Console.WriteLine($"7. Minden mérkőzésen legalább 60 pont: {(m == n ? "igen" : "nem")}");
