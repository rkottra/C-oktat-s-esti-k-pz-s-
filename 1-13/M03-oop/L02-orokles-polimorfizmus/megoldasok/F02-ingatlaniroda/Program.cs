using Ingatlaniroda;

Console.OutputEncoding = System.Text.Encoding.UTF8;

List<Ingatlan> hirdetesek = new List<Ingatlan>
{
    new Lakas("Szeged, Kárász u. 3.", 62, 600_000, 2, false),
    new Panellakas("Debrecen, Sámsoni út 12.", 54, 570_000, 4, false),
    new CsaladiHaz("Gödöllő, Fenyves u. 8.", 120, 650_000, 800, true),
    new Lakas("Budapest, Bartók Béla út 41.", 48, 1_100_000, 5, false),
    new Panellakas("Miskolc, Győri kapu 20.", 68, 420_000, 7, true),
    new CsaladiHaz("Kecskemét, Mező u. 15.", 95, 480_000, 650, false),
    new Lakas("Pécs, Király u. 9.", 75, 560_000, 1, false),
    new CsaladiHaz("Zalaegerszeg, Domb u. 2.", 140, 400_000, 1200, true),
};

// Rendezés irányár szerint – mindegyiknél a saját Iranyar() fut le
hirdetesek.Sort((a, b) => a.Iranyar().CompareTo(b.Iranyar()));
Console.WriteLine("Hirdetések irányár szerint:");
foreach (Ingatlan i in hirdetesek)
    Console.WriteLine($"  {i}");

// Kiválogatás keretösszeg alapján
Console.WriteLine();
double keret;
Console.Write("Keretösszeg (millió Ft): ");
while (!double.TryParse(Console.ReadLine(), out keret) || keret <= 0)
    Console.Write("Pozitív számot adj meg! Keretösszeg (millió Ft): ");

List<Ingatlan> keretbe = new List<Ingatlan>();
foreach (Ingatlan i in hirdetesek)
    if (i.Iranyar() <= keret * 1_000_000)
        keretbe.Add(i);
Console.WriteLine($"A keretbe fér: {keretbe.Count} hirdetés");
foreach (Ingatlan i in keretbe)
    Console.WriteLine($"  {i}");

// Típusonkénti statisztika – a Tipus() szövege alapján, hogy a panellakás ne számítson lakásnak
Console.WriteLine();
Console.WriteLine("Típusonként:");
foreach (string tipus in new[] { "lakás", "panellakás", "családi ház" })
{
    int db = 0;
    double nmArOsszeg = 0;
    foreach (Ingatlan i in hirdetesek)
        if (i.Tipus() == tipus)
        {
            db++;
            nmArOsszeg += (double)i.Iranyar() / i.Alapterulet;
        }
    if (db > 0)
        Console.WriteLine($"  {tipus,-12} {db} db, átlag {nmArOsszeg / db:N0} Ft/m²");
}

// Maximumkiválasztás csak a családi házak között
CsaladiHaz? legnagyobbTelek = null;
foreach (Ingatlan i in hirdetesek)
    if (i is CsaladiHaz h && (legnagyobbTelek == null || h.TelekTerulet > legnagyobbTelek.TelekTerulet))
        legnagyobbTelek = h;
if (legnagyobbTelek != null)
    Console.WriteLine($"\nLegnagyobb telek: {legnagyobbTelek.Cim} ({legnagyobbTelek.TelekTerulet} m²)");
