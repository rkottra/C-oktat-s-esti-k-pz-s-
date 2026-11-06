// 13-M04-L01-F01 · Számok statisztikája fájlból – megoldás

Console.OutputEncoding = System.Text.Encoding.UTF8;

// 1. Beolvasás listába
List<int> szamok = new List<int>();
foreach (string sor in File.ReadAllLines("szamok.txt"))
{
    if (sor.Trim() != "")
        szamok.Add(int.Parse(sor));
}
Console.WriteLine($"1. A fájlban {szamok.Count} szám van.");

// 2. Összegzés és átlag
int osszeg = 0;
foreach (int x in szamok)
{
    osszeg += x;
}
double atlag = (double)osszeg / szamok.Count;      // (double) nélkül egész osztás lenne!
Console.WriteLine($"2. Összeg: {osszeg}, átlag: {atlag:F2}");

// 3. Minimum és maximum – a maximumnál a helyét (sorszámát) is megjegyezzük
int min = szamok[0];
int maxIndex = 0;
for (int i = 1; i < szamok.Count; i++)
{
    if (szamok[i] < min) min = szamok[i];
    if (szamok[i] > szamok[maxIndex]) maxIndex = i;
}
Console.WriteLine($"3. Legkisebb: {min}, legnagyobb: {szamok[maxIndex]} (a fájl {maxIndex + 1}. sorában)");

// 4. Megszámlálás
int pozitiv = 0, negativ = 0, nulla = 0;
foreach (int x in szamok)
{
    if (x > 0) pozitiv++;
    else if (x < 0) negativ++;
    else nulla++;
}
Console.WriteLine($"4. Pozitív: {pozitiv}, negatív: {negativ}, nulla: {nulla}");

// 5. Eldöntés: van-e 900-nál nagyobb abszolút értékű szám?
int j = 0;
while (j < szamok.Count && Math.Abs(szamok[j]) <= 900)
{
    j++;
}
if (j < szamok.Count)
    Console.WriteLine($"5. Van 900-nál nagyobb abszolút értékű szám, az első: {szamok[j]}");
else
    Console.WriteLine("5. Nincs 900-nál nagyobb abszolút értékű szám.");

// 6. Kiválogatás fájlba: a páros számok, soronként 10 darab, vesszővel elválasztva
List<string> kimenet = new List<string>();
string aktualisSor = "";
int dbSorban = 0;
foreach (int x in szamok)
{
    if (x % 2 == 0)
    {
        aktualisSor += (dbSorban == 0 ? "" : ",") + x;
        dbSorban++;
        if (dbSorban == 10)                 // betelt a sor: elmentjük, újat kezdünk
        {
            kimenet.Add(aktualisSor);
            aktualisSor = "";
            dbSorban = 0;
        }
    }
}
if (dbSorban > 0) kimenet.Add(aktualisSor);  // a maradék, nem teli sor
File.WriteAllLines("parosak.txt", kimenet);
Console.WriteLine($"6. A páros számok kiírva: parosak.txt ({kimenet.Count} sor)");

// 7. (Szorgalmi) Medián és szórás
List<int> rendezett = new List<int>(szamok);   // másolat, hogy az eredeti sorrend megmaradjon
rendezett.Sort();
int n = rendezett.Count;
double median = n % 2 == 1
    ? rendezett[n / 2]
    : (rendezett[n / 2 - 1] + rendezett[n / 2]) / 2.0;

double negyzetOsszeg = 0;
foreach (int x in szamok)
{
    negyzetOsszeg += (x - atlag) * (x - atlag);
}
double szoras = Math.Sqrt(negyzetOsszeg / n);
Console.WriteLine($"7. Medián: {median}, szórás: {szoras:F2}");
