// Felső szintű utasítások (top-level statements): nincs kiírva a Main,
// a fordító „odaképzeli”. Rövid programokhoz ez a kényelmesebb.

// ---------- 1. Egyszerű típusok ----------
int egesz = 17;                 // Pythonban: egesz = 17   (a típus itt kötelező!)
long nagyEgesz = 9_000_000_000; // az int ekkorát már nem bír el
double valos = 3.75;            // a forráskódban MINDIG tizedespont
decimal ar = 1999.90m;          // pénzhez: m utótag
bool igaz = true;               // kisbetűvel! (Pythonban True)
char betu = 'A';                // egyetlen karakter, aposztróffal
string szoveg = "Helló, C#!";   // szöveg, idézőjellel
var automatikus = 42;           // var: a fordító kitalálja a típust (itt int), de utána sem változhat

Console.WriteLine("1. Típusok");
Console.WriteLine($"  int: {egesz}, long: {nagyEgesz}, double: {valos}, decimal: {ar}");
Console.WriteLine($"  bool: {igaz}, char: {betu}, string: {szoveg}, var: {automatikus}");
Console.WriteLine($"  int határai: {int.MinValue} .. {int.MaxValue}");

// ---------- 2. Aritmetikai operátorok ----------
Console.WriteLine("\n2. Operátorok");
Console.WriteLine($"  17 / 5   = {17 / 5}     ← két egész: EGÉSZ osztás (Pythonban //)");
Console.WriteLine($"  17 % 5   = {17 % 5}     ← maradék");
Console.WriteLine($"  17 / 5.0 = {17 / 5.0}   ← ha az egyik valós, valós osztás");
Console.WriteLine($"  (double)17 / 5 = {(double)17 / 5}  ← típuskényszerítés (cast)");
Console.WriteLine($"  Math.Pow(2, 10) = {Math.Pow(2, 10)}  ← hatványozás (Pythonban 2 ** 10)");

int szamlalo = 10;
szamlalo++;          // szamlalo = szamlalo + 1  (Pythonban nincs ++)
szamlalo += 5;       // összevont értékadás, mint Pythonban
szamlalo %= 4;
Console.WriteLine($"  szamlalo a műveletek után: {szamlalo}");

// ---------- 3. Relációs és logikai operátorok ----------
int x = 7;
bool ketJegyu = x >= 10 && x <= 99;   // Pythonban: 10 <= x <= 99  vagy  and
bool parosVagyNagy = x % 2 == 0 || x > 5;
Console.WriteLine($"\n3. Logikai: kétjegyű? {ketJegyu}, páros vagy 5-nél nagyobb? {parosVagyNagy}, nem kétjegyű? {!ketJegyu}");

// ---------- 4. Formázott kiírás ----------
Console.WriteLine("\n4. Formázás");
double pi = Math.PI;
Console.WriteLine($"  [{pi}]  [{pi:F2}]  [{pi,10:F3}]  [{pi,-10:F1}]");
Console.WriteLine($"  [{egesz:D5}]  [{egesz:X}]  [{1234567.891:N2}]");
Console.WriteLine("  Régi stílus: {0} + {1} = {2}", 2, 3, 2 + 3);

// ---------- 5. Színek, képernyőkezelés ----------
Console.WriteLine("\n5. Színek – nyomj meg egy billentyűt!");
Console.ReadKey(true);
Console.BackgroundColor = ConsoleColor.DarkBlue;
Console.Clear();                                // a háttérszínnel töröl
Console.ForegroundColor = ConsoleColor.Yellow;
Console.SetCursorPosition(10, 3);               // (oszlop, sor) – 0-tól számozva
Console.Write("Ez a 10. oszlop, 3. sor.");
Console.ForegroundColor = ConsoleColor.Red;
Console.SetCursorPosition(Console.WindowWidth / 2 - 4, Console.WindowHeight / 2);
Console.Write("KÖZÉPEN");
Console.ResetColor();                           // vissza az alapszínekhez
Console.SetCursorPosition(0, Console.WindowHeight - 2);
