Console.OutputEncoding = System.Text.Encoding.UTF8;

Console.Write("Teljes név: ");
string bekert = Console.ReadLine() ?? "";

// 1. Szavakra bontás – a RemoveEmptyEntries a felesleges szóközöket is eltünteti
string[] tagok = bekert.Split(' ', StringSplitOptions.RemoveEmptyEntries);
if (tagok.Length < 2)
{
    Console.WriteLine("Hiba: adj meg vezeték- és keresztnevet is!");
    return;
}

// Nagy kezdőbetű, a többi kicsi – a tömb elemeit helyben átírjuk
for (int i = 0; i < tagok.Length; i++)
    tagok[i] = tagok[i][..1].ToUpper() + tagok[i][1..].ToLower();

string nev = string.Join(" ", tagok);
Console.WriteLine($"Név: {nev}");

// 2. Vezeték- és keresztnév
string vezeteknev = tagok[0];
string keresztnev = string.Join(" ", tagok[1..]);
Console.WriteLine($"Vezetéknév: {vezeteknev}, keresztnév: {keresztnev}");

// 3. Monogram
string monogram = "";
foreach (string tag in tagok)
    monogram += tag[0] + ". ";
Console.WriteLine($"Monogram: {monogram.TrimEnd()}");

// 4. Felhasználónév ékezetek nélkül
string vezetek5 = vezeteknev.Length > 5 ? vezeteknev[..5] : vezeteknev;
string felhasznalo = (vezetek5 + tagok[1][0]).ToLower();
const string Ekezetes = "áéíóöőúüű";
const string Ekezetlen = "aeiooouuu";
for (int i = 0; i < Ekezetes.Length; i++)
    felhasznalo = felhasznalo.Replace(Ekezetes[i], Ekezetlen[i]);
Console.WriteLine($"Felhasználónév: {felhasznalo}");

// 5. Névkártya: 30 karakter belső szélesség, középre igazítva
const int Szelesseg = 30;
int bal = (Szelesseg - nev.Length) / 2;
string kozepre = nev.PadLeft(bal + nev.Length).PadRight(Szelesseg);
string keret = new string('*', Szelesseg + 2);
Console.WriteLine(keret);
Console.WriteLine($"*{kozepre}*");
Console.WriteLine(keret);
