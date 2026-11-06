using System.Globalization;
using Kolcsonzesek;

Console.OutputEncoding = System.Text.Encoding.UTF8;
CultureInfo hu = new CultureInfo("hu-HU");
DateTime ma = new DateTime(2026, 5, 15);     // rögzített vizsgálati nap

// 2. Beolvasás
List<Kolcsonzes> lista = new List<Kolcsonzes>();
string[] sorok = File.ReadAllLines("kolcsonzesek.csv");
for (int i = 1; i < sorok.Length; i++)
    if (sorok[i].Trim() != "")
        lista.Add(new Kolcsonzes(sorok[i]));
Console.WriteLine($"2. Kölcsönzések száma: {lista.Count}");

// 3. Kint lévők és ezek közül a lejártak
int kint = 0;
List<Kolcsonzes> lejartak = new List<Kolcsonzes>();
foreach (Kolcsonzes k in lista)
    if (k.Kint)
    {
        kint++;
        if (k.Hatarido < ma)
            lejartak.Add(k);
    }
Console.WriteLine($"3. Még kint: {kint} könyv, ebből lejárt:");
foreach (Kolcsonzes k in lejartak)
    Console.WriteLine($"   {k.Olvaso,-18} {k.Cim,-20} határidő: {k.Hatarido:yyyy.MM.dd.}  késés: {k.KesesNapok(ma)} nap");

// 4. Késve visszahozottak díja
int kesve = 0, befolyt = 0;
foreach (Kolcsonzes k in lista)
    if (!k.Kint && k.KesesNapok(ma) > 0)
    {
        kesve++;
        befolyt += k.Dij(ma);
    }
Console.WriteLine($"4. Késve visszahozott: {kesve} kölcsönzés, befolyt díj: {befolyt:N0} Ft");

// 5. Átlagos kölcsönzési idő a visszahozottaknál
int napOsszeg = 0, visszahozott = 0;
foreach (Kolcsonzes k in lista)
    if (!k.Kint)
    {
        napOsszeg += k.KolcsonzesiNapok;
        visszahozott++;
    }
Console.WriteLine($"5. Átlagos kölcsönzési idő: {(double)napOsszeg / visszahozott:F1} nap");

// 6. A hét napjai – megszámlálás DayOfWeek szerint indexelt tömbbe
string[] napNevek = { "vasárnap", "hétfő", "kedd", "szerda", "csütörtök", "péntek", "szombat" };
int[] napDb = new int[7];
foreach (Kolcsonzes k in lista)
    napDb[(int)k.KolcsonzesDatum.DayOfWeek]++;

int[] sorrend = { 1, 2, 3, 4, 5, 6, 0 };     // hétfőtől vasárnapig
List<string> reszek = new List<string>();
int maxNap = 1;
foreach (int n in sorrend)
{
    reszek.Add($"{napNevek[n]} {napDb[n]}");
    if (napDb[n] > napDb[maxNap])
        maxNap = n;
}
Console.WriteLine("6. Kölcsönzések a hét napjai szerint:");
Console.WriteLine($"   {string.Join(", ", reszek)}");
Console.WriteLine($"   A legtöbb: {napNevek[maxNap]} ({napDb[maxNap]} db)");

// 7. Hónaponkénti bontás
int[] honapDb = new int[13];                  // 1..12 indexek, a 0. nem használt
foreach (Kolcsonzes k in lista)
    honapDb[k.KolcsonzesDatum.Month]++;
List<string> honapok = new List<string>();
for (int h = 1; h <= 12; h++)
    if (honapDb[h] > 0)
        honapok.Add($"{new DateTime(2026, h, 1).ToString("MMMM", hu)}: {honapDb[h]}");
Console.WriteLine($"7. {string.Join(", ", honapok)}");

// 8. Felszólítások fájlba
List<string> levelek = new List<string>();
foreach (Kolcsonzes k in lejartak)
    levelek.Add($"Kedves {k.Olvaso}! A(z) „{k.Cim}” című könyv határideje {k.Hatarido:yyyy.MM.dd.} volt. " +
                $"Eddigi késedelmi díj: {k.Dij(ma):N0} Ft.");
File.WriteAllLines("felszolitasok.txt", levelek);
Console.WriteLine($"8. {levelek.Count} felszólítás elkészült: felszolitasok.txt");
