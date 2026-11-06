// CSV-sor → objektum → List<Orszag>, majd feldolgozás programozási tételekkel

Console.OutputEncoding = System.Text.Encoding.UTF8;

// --- Beolvasás ---
List<Orszag> orszagok = new List<Orszag>();
string[] sorok = File.ReadAllLines("europa.csv");
for (int i = 1; i < sorok.Length; i++)          // i = 1: a fejlécet (0. sor) kihagyjuk
{
    if (sorok[i].Trim() == "") continue;        // üres sor a fájl végén
    orszagok.Add(new Orszag(sorok[i]));
}
Console.WriteLine($"Beolvasva: {orszagok.Count} ország\n");

// --- Az első öt ország (a ToString felüldefiniálása miatt szépen jelenik meg) ---
Console.WriteLine("Az első öt ország:");
for (int i = 0; i < 5; i++)
{
    Console.WriteLine($"  {orszagok[i]}");
}

// --- Összegzés: a teljes népesség ---
long osszNepesseg = 0;                           // long: az összeg nem fér el int-ben!
foreach (Orszag o in orszagok)
{
    osszNepesseg += o.Nepesseg;
}
Console.WriteLine($"\nÖsszes lakos: {osszNepesseg:N0} fő");
Console.WriteLine($"Átlagos népesség: {(double)osszNepesseg / orszagok.Count:N0} fő");

// --- Megszámlálás: hány ország kisebb 10 000 km²-nél? ---
int kicsik = 0;
foreach (Orszag o in orszagok)
{
    if (o.Terulet < 10000)
        kicsik++;
}
Console.WriteLine($"10 000 km²-nél kisebb országok: {kicsik} db");

// --- Maximumkiválasztás: a legnagyobb népsűrűségű ország ---
Orszag legsurubb = orszagok[0];
foreach (Orszag o in orszagok)
{
    if (o.Nepsuruseg > legsurubb.Nepsuruseg)
        legsurubb = o;
}
Console.WriteLine($"Legsűrűbben lakott: {legsurubb.Nev} ({legsurubb.Nepsuruseg:N0} fő/km²)");

// --- Keresés: van-e olyan ország, amelynek Bern a fővárosa? ---
int index = 0;
while (index < orszagok.Count && orszagok[index].Fovaros != "Bern")
{
    index++;
}
if (index < orszagok.Count)
    Console.WriteLine($"Bern {orszagok[index].Nev} fővárosa.");
else
    Console.WriteLine("Nincs Bern fővárosú ország.");

// --- Kiválogatás + fájlba írás: az S betűvel kezdődő országok ---
List<string> kimenet = new List<string>();
foreach (Orszag o in orszagok)
{
    if (o.Nev.StartsWith("S"))
        kimenet.Add($"{o.Nev};{o.Terulet}");    // magyar gépen a tört tizedesvesszővel íródik ki, ezért ; az elválasztó
}
File.WriteAllLines("s_betus.txt", kimenet);
Console.WriteLine($"\n{kimenet.Count} S betűs ország kiírva ide: {Path.GetFullPath("s_betus.txt")}");
