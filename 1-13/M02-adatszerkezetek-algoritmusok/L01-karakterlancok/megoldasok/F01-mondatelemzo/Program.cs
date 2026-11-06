Console.OutputEncoding = System.Text.Encoding.UTF8;

Console.Write("Írj be egy mondatot: ");
string mondat = Console.ReadLine() ?? "";

// 1. Visszafelé
string forditott = "";
for (int i = mondat.Length - 1; i >= 0; i--)
    forditott += mondat[i];
Console.WriteLine($"Visszafelé: {forditott}");

// 2. 'a' betűk – kisbetűsítés után elég a 'a'-t keresni
int aDb = 0;
foreach (char c in mondat.ToLower())
    if (c == 'a') aDb++;
Console.WriteLine($"'a' betűk: {aDb}");

// 3. Betűk (az ékezetesek is)
int betuDb = 0;
foreach (char c in mondat)
    if (char.IsLetter(c)) betuDb++;
Console.WriteLine($"Betűk: {betuDb}");

// 4. Szavak
string[] szavak = mondat.Split(' ', StringSplitOptions.RemoveEmptyEntries);
Console.WriteLine($"Szavak: {szavak.Length}");

// Extra: Split nélkül – új szó kezdődik, ha nem szóköz jön szóköz (vagy a mondat eleje) után
int szoDb = 0;
for (int i = 0; i < mondat.Length; i++)
    if (mondat[i] != ' ' && (i == 0 || mondat[i - 1] == ' '))
        szoDb++;
Console.WriteLine($"Szavak (Split nélkül): {szoDb}");

// 5. Leghosszabb szó (maximumkiválasztás)
string leghosszabb = "";
foreach (string szo in szavak)
{
    string tiszta = szo.Trim('.', ',', '!', '?', ';', ':');
    if (tiszta.Length > leghosszabb.Length)
        leghosszabb = tiszta;
}
Console.WriteLine($"Leghosszabb szó: {leghosszabb}");
