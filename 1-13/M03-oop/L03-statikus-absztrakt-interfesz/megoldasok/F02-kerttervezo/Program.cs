using Kerttervezo;

Console.OutputEncoding = System.Text.Encoding.UTF8;

// Agyas a = new Agyas("bab", 5);
// → CS0144: Cannot create an instance of the abstract type or interface 'Agyas'

List<Agyas> kert = new List<Agyas>
{
    new TeglalapAgyas("paradicsom", 2, 3, 2),
    new KorAgyas("sárgarépa", 4, 1),
    new HaromszogAgyas("hagyma", 6, 3, 4, 5),
    new LAgyas("paradicsom", 2, 4, 3, 2, 1.5),
    new TeglalapAgyas("bab", 8, 1.2, 4),
};

try
{
    kert.Add(new HaromszogAgyas("retek", 5, 1, 2, 10));
}
catch (ArgumentException ex)
{
    Console.WriteLine($"Hiba: {ex.Message}");
}

kert.Add(new KorAgyas("saláta", 3, 1.5));
Console.WriteLine();

foreach (Agyas a in kert)
    Console.WriteLine(a);

// Összegzés – mindegyiknél a saját Terulet/Kerulet fut le
double osszTerulet = 0, szegely = 0;
foreach (Agyas a in kert)
{
    osszTerulet += a.Terulet();
    szegely += a.Kerulet();
}
Console.WriteLine($"\nÖsszes terület: {osszTerulet:F2} m², szegély: {szegely:F2} m");

// Különböző növénynevek kigyűjtése, majd növényenkénti összegzés
List<string> novenyek = new List<string>();
foreach (Agyas a in kert)
    if (!novenyek.Contains(a.Noveny))
        novenyek.Add(a.Noveny);

Console.WriteLine("Vetőmag növényenként:");
foreach (string n in novenyek)
{
    double gramm = 0;
    foreach (Agyas a in kert)
        if (a.Noveny == n)
            gramm += a.Vetomag();
    Console.WriteLine($"  {n,-12} {gramm,6:F1} g");
}

// Maximumkiválasztás
Agyas legnagyobb = kert[0];
foreach (Agyas a in kert)
    if (a.Terulet() > legnagyobb.Terulet())
        legnagyobb = a;
Console.WriteLine($"\nLegnagyobb ágyás: {legnagyobb.Azonosito} ({legnagyobb.Alak()}, {legnagyobb.Terulet():F2} m²)");

// Rendezés terület szerint csökkenően
kert.Sort((x, y) => y.Terulet().CompareTo(x.Terulet()));
Console.WriteLine("Terület szerint csökkenő sorrendben:");
foreach (Agyas a in kert)
    Console.WriteLine($"  {a.Azonosito,-3} {a.Terulet(),6:F2} m²");

// A statikus számláló a hibás háromszöget is beleszámolta (az ős konstruktora már lefutott),
// ezért hiányzik az azonosítók közül egy szám.
Console.WriteLine($"\nLétrehozott ágyás-objektumok: {Agyas.Darabszam}, a kertben: {kert.Count}");
