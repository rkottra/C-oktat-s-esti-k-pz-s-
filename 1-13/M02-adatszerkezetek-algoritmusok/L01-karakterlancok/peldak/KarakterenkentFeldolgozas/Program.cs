// Egy bekért mondat elemzése karakterenként
using System.Text;

Console.OutputEncoding = Encoding.UTF8;

Console.Write("Írj be egy mondatot: ");
string mondat = Console.ReadLine() ?? "";
if (mondat.Trim() == "")
    mondat = "Az esti képzés 2026-ban is 18 alkalmas, és jó hangulatú!";   // alapértelmezett, ha üres
Console.WriteLine($"Vizsgált mondat: {mondat}\n");

// 1. Számlálás karakterenként (Pythonban: for c in mondat:)
const string Maganhangzok = "aáeéiíoóöőuúüű";
int betu = 0, szamjegy = 0, szokoz = 0, irasjel = 0, maganhangzo = 0;
foreach (char c in mondat)
{
    if (char.IsLetter(c))
    {
        betu++;
        if (Maganhangzok.Contains(char.ToLower(c)))
            maganhangzo++;
    }
    else if (char.IsDigit(c)) szamjegy++;
    else if (char.IsWhiteSpace(c)) szokoz++;
    else if (char.IsPunctuation(c)) irasjel++;
}
Console.WriteLine($"Betű: {betu} (ebből magánhangzó: {maganhangzo}), számjegy: {szamjegy}, szóköz: {szokoz}, írásjel: {irasjel}");

// 2. Megfordítás ciklussal: visszafelé járjuk be az indexeket
string forditott = "";
for (int i = mondat.Length - 1; i >= 0; i--)
    forditott += mondat[i];
Console.WriteLine($"Visszafelé: {forditott}");

// 3. Minden szó nagy kezdőbetűvel – Split nélkül: akkor nagybetű, ha előtte szóköz volt
StringBuilder sb = new StringBuilder();
bool szoEleje = true;
foreach (char c in mondat)
{
    sb.Append(szoEleje ? char.ToUpper(c) : c);
    szoEleje = c == ' ';
}
Console.WriteLine($"Nagy kezdőbetűk: {sb}");

// 4. Egy adott betű előfordulása (Pythonban: mondat.lower().count("a"))
int aBetuk = 0;
foreach (char c in mondat.ToLower())
    if (c == 'a') aBetuk++;
Console.WriteLine($"'a' betűk száma (kis- és nagybetű együtt): {aBetuk}");

// 5. Betűstatisztika: angol ábécé betűi – a char valójában szám, így indexként is használható
int[] darab = new int[26];
foreach (char c in mondat.ToLower())
    if (c >= 'a' && c <= 'z')
        darab[c - 'a']++;          // 'a' -> 0, 'b' -> 1, ...
Console.Write("Betűstatisztika (ékezet nélküli betűk): ");
for (int i = 0; i < darab.Length; i++)
    if (darab[i] > 0)
        Console.Write($"{(char)('a' + i)}:{darab[i]} ");
Console.WriteLine();
