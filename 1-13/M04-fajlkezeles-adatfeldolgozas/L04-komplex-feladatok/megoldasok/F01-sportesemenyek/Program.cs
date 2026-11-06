using System.Globalization;
using Sportesemenyek;

Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.InputEncoding = System.Text.Encoding.UTF8;
CultureInfo hu = new CultureInfo("hu-HU");

// 1. feladat – beolvasás
List<Merkozes> m = new List<Merkozes>();
string[] sorok = File.ReadAllLines("sportesemenyek.csv");
for (int i = 1; i < sorok.Length; i++)
    if (sorok[i].Trim() != "")
        m.Add(new Merkozes(sorok[i]));

// A sportágak listája az adatokból (csoportosítás első lépése)
List<string> sportagak = new List<string>();
foreach (Merkozes x in m)
    if (!sportagak.Contains(x.Sportag))
        sportagak.Add(x.Sportag);

// 2. feladat – darabszám és sportáganként
Console.WriteLine($"2. feladat: Mérkőzések száma: {m.Count}");
List<string> reszek = new List<string>();
foreach (string s in sportagak)
{
    int db = 0;
    foreach (Merkozes x in m)
        if (x.Sportag == s) db++;
    reszek.Add($"{s}: {db}");
}
Console.WriteLine($"   {string.Join(", ", reszek)}");

// 3. feladat – összegzés
long nezok = 0, bevetel = 0;
foreach (Merkozes x in m)
{
    nezok += x.Nezok;
    bevetel += x.Bevetel;
}
Console.WriteLine($"3. feladat: Nézők összesen: {nezok:N0} fő, jegybevétel: {bevetel:N0} Ft");

// 4. feladat – keresés
int k = 0;
while (k < m.Count && m[k].Jegyar != 0)
    k++;
if (k < m.Count)
    Console.WriteLine($"4. feladat: Volt ingyenes mérkőzés: {m[k].Datum:yyyy.MM.dd.}, {m[k].Sportag}, {m[k].Hazai} – {m[k].Vendeg}");
else
    Console.WriteLine("4. feladat: Nem volt ingyenes mérkőzés.");

// 5. feladat – maximumkiválasztás
Merkozes legtobb = m[0];
foreach (Merkozes x in m)
    if (x.Nezok > legtobb.Nezok)
        legtobb = x;
Console.WriteLine($"5. feladat: A legtöbb néző ({legtobb.Nezok} fő): {legtobb.Datum.ToString("yyyy.MM.dd. dddd", hu)}, " +
                  $"{legtobb.Sportag}, {legtobb.Eredmeny}");

// 6. feladat – döntetlenek sportáganként
reszek.Clear();
int osszDontetlen = 0;
foreach (string s in sportagak)
{
    int db = 0;
    foreach (Merkozes x in m)
        if (x.Sportag == s && x.Dontetlen) db++;
    osszDontetlen += db;
    reszek.Add($"{s}: {db}");
}
Console.WriteLine($"6. feladat: Döntetlenek: {osszDontetlen} ({string.Join(", ", reszek)})");

// 7. feladat – csapat mérlege
Console.Write("7. feladat: Csapat neve: ");
string csapat = (Console.ReadLine() ?? "").Trim();
int meccs = 0, gy = 0, d = 0, v = 0, lott = 0, kapott = 0;
string pontosNev = csapat;
foreach (Merkozes x in m)
{
    if (!x.Resztvevo(csapat)) continue;
    meccs++;
    pontosNev = string.Equals(x.Hazai, csapat, StringComparison.OrdinalIgnoreCase) ? x.Hazai : x.Vendeg;
    (int sajat, int ellenfel) = x.Pontok(csapat);
    lott += sajat;
    kapott += ellenfel;
    if (sajat > ellenfel) gy++;
    else if (sajat == ellenfel) d++;
    else v++;
}
if (meccs == 0)
    Console.WriteLine("   Nincs ilyen csapat.");
else
    Console.WriteLine($"   {pontosNev}: {meccs} mérkőzés, {gy} győzelem, {d} döntetlen, {v} vereség, pontok: {lott}:{kapott}");

// 8. feladat – feltételes átlagok
long hvNezo = 0, hkNezo = 0;
int hvDb = 0, hkDb = 0;
foreach (Merkozes x in m)
{
    if (x.Hetvege) { hvNezo += x.Nezok; hvDb++; }
    else { hkNezo += x.Nezok; hkDb++; }
}
Console.WriteLine($"8. feladat: Átlagos nézőszám hétvégén: {(double)hvNezo / hvDb:F1}, hétköznap: {(double)hkNezo / hkDb:F1}");

// 9. feladat – havi bevétel
long[] havi = new long[13];
foreach (Merkozes x in m)
    havi[x.Datum.Month] += x.Bevetel;
Console.WriteLine("9. feladat: Havi jegybevétel:");
for (int h = 1; h <= 12; h++)
    if (havi[h] > 0)
        Console.WriteLine($"   {new DateTime(2026, h, 1).ToString("MMMM", hu),-10}{havi[h],12:N0} Ft");

// 10. feladat – labdarúgó-tabella
List<CsapatEredmeny> tabella = new List<CsapatEredmeny>();
foreach (Merkozes x in m)
{
    if (x.Sportag != "labdarúgás") continue;
    Csapat(tabella, x.Hazai).Rogzit(x.HazaiPont, x.VendegPont);
    Csapat(tabella, x.Vendeg).Rogzit(x.VendegPont, x.HazaiPont);
}
tabella.Sort((a, b) => a.Pont != b.Pont ? b.Pont.CompareTo(a.Pont) : b.Golkulonbseg.CompareTo(a.Golkulonbseg));

List<string> ki = new List<string>();
for (int i = 0; i < tabella.Count; i++)
{
    CsapatEredmeny c = tabella[i];
    ki.Add($"{i + 1}. {c.Nev};{c.Merkozes};{c.Gyozelem};{c.Dontetlen};{c.Vereseg};{c.Lott}:{c.Kapott};{c.Pont}");
}
File.WriteAllLines("tabella.txt", ki);
Console.WriteLine($"10. feladat: A tabella elkészült: tabella.txt – az első helyezett: {tabella[0].Nev} ({tabella[0].Pont} pont)");

// Keresés a tabellában; ha nincs még ilyen csapat, létrehozzuk
static CsapatEredmeny Csapat(List<CsapatEredmeny> tabella, string nev)
{
    foreach (CsapatEredmeny c in tabella)
        if (c.Nev == nev)
            return c;
    CsapatEredmeny uj = new CsapatEredmeny(nev);
    tabella.Add(uj);
    return uj;
}
