// Véletlenszámok a Random osztállyal
// Pythonban: import random; random.randint(1, 6)
// C#-ban:    Random r = new Random(); r.Next(1, 7)   ← a felső határ NINCS benne!

Random r = new Random();
// Random r = new Random(2026);   // „mag” (seed) megadásával minden futásnál ugyanaz a sorozat – hibakereséshez hasznos

// ---------- 1. Egész számok ----------
Console.WriteLine("1. Tíz kockadobás (1–6):");
for (int i = 0; i < 10; i++)
{
    Console.Write($"{r.Next(1, 7)} ");
}
Console.WriteLine();

// Páros szám 10 és 50 között: generálj 5..25 közöttit, és szorozd 2-vel
int paros = r.Next(5, 26) * 2;
// 5-tel osztható szám 100 és 200 között
int otteloszthato = r.Next(20, 41) * 5;
Console.WriteLine($"   Páros szám [10; 50]: {paros}, 5-tel osztható [100; 200]: {otteloszthato}");

// ---------- 2. Valós számok ----------
// NextDouble(): 0 <= x < 1  →  eltolás és nyújtás: min + x * (max - min)
double also = -38.96, felso = 47.5;
double valos = also + r.NextDouble() * (felso - also);
Console.WriteLine($"\n2. Valós szám [{also}; {felso}): {valos:F2}");

// Ha a felső végpont is kell, és elég 2 tizedes: egészet generálunk, és osztunk 100-zal
double ketTizedes = r.Next(100, 10091) / 100.0;   // 1,00 .. 100,90 mindkét végponttal
Console.WriteLine($"   Két tizedesjegyű szám [1,00; 100,90]: {ketTizedes:F2}");

// ---------- 3. Szimuláció: két kockával 1000 dobás ----------
int tizenketto = 0;
int hetes = 0;
for (int i = 0; i < 1000; i++)
{
    int osszeg = r.Next(1, 7) + r.Next(1, 7);
    if (osszeg == 12)
    {
        tizenketto++;
    }
    else if (osszeg == 7)    // Pythonban: elif
    {
        hetes++;
    }
}
Console.WriteLine($"\n3. 1000 dobás két kockával: 12-es {tizenketto}× (várható ≈ 28), 7-es {hetes}× (várható ≈ 167)");

// ---------- 4. Pénzfeldobás grafikusan ----------
Console.WriteLine("\n4. 60 pénzfeldobás (█ = fej, ░ = írás):");
int fej = 0;
for (int i = 1; i <= 60; i++)
{
    bool fejetDobtunk = r.Next(2) == 0;    // Next(2): 0 vagy 1
    if (fejetDobtunk)
    {
        fej++;
    }
    Console.Write(fejetDobtunk ? '█' : '░');
    if (i % 30 == 0)
    {
        Console.WriteLine();     // 30 dobás után új sor
    }
}
Console.WriteLine($"   Fej: {fej}, írás: {60 - fej}");

// ---------- 5. do-while: addig dobunk, amíg hatost nem kapunk ----------
// Pythonban nincs hátultesztelő ciklus – C#-ban van: legalább egyszer lefut.
int dobasokSzama = 0;
int dobas;
do
{
    dobas = r.Next(1, 7);
    dobasokSzama++;
} while (dobas != 6);
Console.WriteLine($"\n5. Az első hatos a(z) {dobasokSzama}. dobásra jött.");
