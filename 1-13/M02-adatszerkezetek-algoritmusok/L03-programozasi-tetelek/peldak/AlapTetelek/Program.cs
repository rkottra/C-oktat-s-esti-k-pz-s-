// Alap programozási tételek egy havi hőmérséklet-sorozaton.
// Minden tételt saját kóddal írunk meg, és mellé LINQ-val ellenőrizzük.
Console.OutputEncoding = System.Text.Encoding.UTF8;

int[] t = { 3, 5, 2, -1, -4, 0, 6, 8, 9, 7, 4, 2, 1, -2, -3, 0, 3, 5, 7, 10, 12, 11, 9, 8, 6, 4, 5, 7, 9, 11 };
int n = t.Length;
Console.WriteLine($"Napi középhőmérsékletek ({n} nap): {string.Join(" ", t)}");
Console.WriteLine();

// ---- Összegzés ----
int osszeg = 0;
for (int i = 0; i < n; i++)
    osszeg += t[i];
double atlag = (double)osszeg / n;
Console.WriteLine($"Összegzés: az összeg {osszeg}, az átlag {atlag:F2} °C   (LINQ: {t.Average():F2})");

// ---- Megszámlálás ----
int fagyos = 0;
for (int i = 0; i < n; i++)
    if (t[i] < 0)
        fagyos++;
Console.WriteLine($"Megszámlálás: {fagyos} fagyos nap   (LINQ: {t.Count(x => x < 0)})");

// ---- Eldöntés: volt-e 10 fok feletti nap? ----
int e = 0;
while (e < n && !(t[e] > 10))
    e++;
bool voltMeleg = e < n;
Console.WriteLine($"Eldöntés: volt 10 °C feletti nap? {(voltMeleg ? "igen" : "nem")}   (LINQ: {t.Any(x => x > 10)})");

// ---- Eldöntés fordítva: minden nap -5 fok felett volt? = nincs olyan, ami nem ----
int m = 0;
while (m < n && t[m] > -5)
    m++;
bool mindEnyhe = m == n;
Console.WriteLine($"Eldöntés (minden): minden nap -5 °C felett? {(mindEnyhe ? "igen" : "nem")}   (LINQ: {t.All(x => x > -5)})");

// ---- Kiválasztás: tudjuk, hogy van fagyos nap – hányadik az első? ----
int k = 0;
while (!(t[k] < 0))
    k++;
Console.WriteLine($"Kiválasztás: az első fagyos nap a {k + 1}. nap ({t[k]} °C)");

// ---- Keresés: volt-e 15 fokos nap, és ha igen, mikor? ----
int s = 0;
while (s < n && t[s] != 15)
    s++;
if (s < n)
    Console.WriteLine($"Keresés: 15 °C a {s + 1}. napon");
else
    Console.WriteLine($"Keresés: nem volt 15 °C-os nap   (LINQ index: {Array.FindIndex(t, x => x == 15)})");

// ---- Maximum- és minimumkiválasztás ----
int maxI = 0, minI = 0;
for (int i = 1; i < n; i++)
{
    if (t[i] > t[maxI]) maxI = i;
    if (t[i] < t[minI]) minI = i;
}
Console.WriteLine($"Maximum: a legmelegebb a {maxI + 1}. nap, {t[maxI]} °C   (LINQ: {t.Max()})");
Console.WriteLine($"Minimum: a leghidegebb a {minI + 1}. nap, {t[minI]} °C   (LINQ: {t.Min()})");

// ---- Tételek kombinálása: a legnagyobb napi emelkedés (maximum a különbségeken) ----
int legjobbNap = 1;
for (int i = 2; i < n; i++)
    if (t[i] - t[i - 1] > t[legjobbNap] - t[legjobbNap - 1])
        legjobbNap = i;
Console.WriteLine($"Kombinált: a legnagyobb emelkedés a {legjobbNap}. és {legjobbNap + 1}. nap között, " +
                  $"{t[legjobbNap] - t[legjobbNap - 1]} fok");
