using System.Globalization;

Console.OutputEncoding = System.Text.Encoding.UTF8;

// --- Automatikus (implicit): kisebb típusból nagyobba, adatvesztés nélkül ---
int egesz = 7;
long hosszu = egesz;
double valos = egesz;
Console.WriteLine($"int → long → double: {egesz} → {hosszu} → {valos}");

// --- Kényszerített (explicit, cast): adatvesztés lehet ---
double pi = 3.99;
int levagott = (int)pi;                   // a tört rész elvész, NEM kerekít
Console.WriteLine($"(int)3.99 = {levagott}");
Console.WriteLine($"7 / 2 = {7 / 2},  (double)7 / 2 = {(double)7 / 2}");

// karakter ↔ kód
char betu = 'A';
Console.WriteLine($"(int)'A' = {(int)betu},  (char)66 = {(char)66},  'a' + 1 = {(char)('a' + 1)}");
Console.WriteLine($"'7' számjegy értéke: {'7' - '0'}");

// szám ↔ enum
Evszak e = (Evszak)2;
Console.WriteLine($"(Evszak)2 = {e},  (int)Evszak.Tel = {(int)Evszak.Tel}");
Console.WriteLine();

// --- Szövegből: Parse, TryParse, Convert ---
int a = int.Parse("42");
double b = double.Parse("3.75", CultureInfo.InvariantCulture);  // fájlból: mindig InvariantCulture!
double c = double.Parse("3,75", new CultureInfo("hu-HU"));      // magyar tizedesvessző
Console.WriteLine($"int.Parse(\"42\") = {a}, \"3.75\" (invariant) = {b}, \"3,75\" (hu) = {c}");

foreach (string s in new[] { "123", "12a", "", " 56 ", "99999999999" })
{
    bool ok = int.TryParse(s, out int x);
    Console.WriteLine($"  int.TryParse(\"{s}\") → {(ok ? x.ToString() : "sikertelen")}");
}

try
{
    int hibas = int.Parse("tizenkettő");
}
catch (FormatException ex)
{
    Console.WriteLine($"int.Parse(\"tizenkettő\") → FormatException: {ex.Message}");
}

Console.WriteLine($"Convert.ToInt32(\"15\") = {Convert.ToInt32("15")}, Convert.ToBoolean(\"True\") = {Convert.ToBoolean("True")}");
Console.WriteLine($"Enum.Parse<Evszak>(\"Nyar\") = {Enum.Parse<Evszak>("Nyar")}");
Console.WriteLine();

// --- Kerekítés: három módszer, három eredmény ---
Console.WriteLine($"{"érték",6}{"(int)",8}{"Convert",9}{"Round",7}{"Round(Away)",13}");
foreach (double v in new[] { 1.5, 2.5, 2.7, -2.5 })
{
    Console.WriteLine($"{v,6}{(int)v,8}{Convert.ToInt32(v),9}{Math.Round(v),7}" +
                      $"{Math.Round(v, MidpointRounding.AwayFromZero),13}");
}
Console.WriteLine();

// --- Szöveggé: formázás ---
double ar = 1234567.891;
Console.WriteLine($"ToString(): {ar}");
Console.WriteLine($"F2: {ar:F2},  N0: {ar:N0},  N2: {ar:N2},  0.0: {ar.ToString("0.0")}");
Console.WriteLine($"Invariant: {ar.ToString(CultureInfo.InvariantCulture)}   ← így írjunk fájlba");

enum Evszak { Tavasz, Nyar, Osz, Tel }
