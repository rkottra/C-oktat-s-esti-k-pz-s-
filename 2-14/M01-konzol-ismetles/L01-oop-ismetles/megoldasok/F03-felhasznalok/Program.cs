using Felhasznalok;

var felhasznalok = new List<Felhasznalo>
{
    new Felhasznalo("anna.k", "Tavasz2026"),
    new Felhasznalo("Bela"),                     // kezdőjelszót kap; a név kisbetűs lesz
    new Admin("admin", "Admin2026!", "felhasználók", "mentés", "naplók"),
    new Vendeg(),
};

// Hibás adatok: a konstruktor kivételt dob
string[][] hibasak = { new[] { "xy", "Jelszo123" }, new[] { "csaba", "rovid" } };
foreach (var adat in hibasak)
{
    try
    {
        felhasznalok.Add(new Felhasznalo(adat[0], adat[1]));
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine($"Nem jött létre ({adat[0]}): {ex.Message}");
    }
}

Console.WriteLine("\nFelhasználók:");
foreach (var f in felhasznalok)
    Console.WriteLine($"  {f}");

// Jelszócsere
var bela = felhasznalok[1];
Console.WriteLine($"\nJelszócsere rossz régi jelszóval: {(bela.JelszoCsere("valami", "Uj12345678") ? "sikerült" : "nem sikerült")}");
Console.WriteLine($"Jelszócsere jó régi jelszóval:   {(bela.JelszoCsere("Kezdo123", "Uj12345678") ? "sikerült" : "nem sikerült")}");

// Bejelentkezés legfeljebb 3 próbálkozással
Console.WriteLine("\n--- Bejelentkezés ---");
Felhasznalo? belepett = null;
for (int proba = 1; proba <= 3 && belepett == null; proba++)
{
    Console.Write("Felhasználónév: ");
    string nev = (Console.ReadLine() ?? "").Trim().ToLower();
    Console.Write("Jelszó: ");
    string jelszo = Console.ReadLine() ?? "";

    Felhasznalo? talalt = null;
    foreach (var f in felhasznalok)
    {
        if (f.Felhasznalonev == nev) talalt = f;
    }

    if (talalt != null && talalt.Ellenoriz(jelszo))   // Vendeg esetén a felülírt Ellenoriz fut
        belepett = talalt;
    else
        Console.WriteLine($"Hibás felhasználónév vagy jelszó ({proba}/3).");
}

if (belepett == null)
    Console.WriteLine("A belépés letiltva.");
else
{
    Console.WriteLine($"Üdv, {belepett.Felhasznalonev}! Jogosultság: {belepett.Jogosultsag}");
    if (belepett is Admin admin)
        Console.WriteLine($"Kezelt modulok: {string.Join(", ", admin.Modulok)}");
}
