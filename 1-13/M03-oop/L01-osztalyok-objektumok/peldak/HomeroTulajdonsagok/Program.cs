using HomeroTulajdonsagok;

// --- 1. lépés: publikus mező – nincs védelem ---
var h1 = new HomeroPublikusMezovel();
h1.celsius = -500;                       // fizikailag lehetetlen, mégis bekerül
Console.WriteLine($"Publikus mező:  {h1.celsius} °C  <- hibás érték bekerült!");

// --- 2. lépés: metódusok – védett, de kényelmetlen ---
var h2 = new HomeroMetodusokkal();
h2.CelsiusBeallit(-500);                 // csendben figyelmen kívül hagyja
h2.CelsiusBeallit(21.5);
Console.WriteLine($"Metódusokkal:   {h2.CelsiusLekerdez()} °C");

// --- 3. lépés: tulajdonság – változóként használható ÉS védett ---
var nappali = new Homero("Nappali", 21.5);
Console.WriteLine($"Tulajdonsággal: {nappali}");

Console.WriteLine("\nÁtírjuk Fahrenheitben (-4 °F):");
nappali.Fahrenheit = -4;                 // a Fahrenheit set-je visszaszámol Celsiusra
Console.WriteLine(nappali);

Console.WriteLine("\nÁtírjuk Kelvinben (300 K):");
nappali.Kelvin = 300;
Console.WriteLine(nappali);

// Hibás érték: a Celsius set-je kivételt dob – akkor is, ha Kelvinben adjuk meg.
Console.WriteLine();
try
{
    nappali.Kelvin = -10;
}
catch (ArgumentException ex)
{
    Console.WriteLine($"Hiba: {ex.Message}");
}
Console.WriteLine($"Az érték nem változott: {nappali.Celsius:F2} °C");

// Bekérés a felhasználótól, hibakezeléssel
Console.Write("\nAdj meg egy hőmérsékletet Celsiusban: ");
try
{
    double c = double.Parse(Console.ReadLine() ?? "");
    var kint = new Homero("Kint", c);
    Console.WriteLine(kint);
}
catch (FormatException)
{
    Console.WriteLine("Ez nem szám.");
}
catch (ArgumentException ex)
{
    Console.WriteLine($"Hiba: {ex.Message}");
}
