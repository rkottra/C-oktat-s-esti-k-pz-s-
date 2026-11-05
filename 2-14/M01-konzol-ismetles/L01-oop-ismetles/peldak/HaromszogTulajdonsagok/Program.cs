using HaromszogTulajdonsagok;

// --- Példányosítás két különböző konstruktorral ---
var derekszogu = new Haromszog("derékszögű", 3, 4, 5);
var szabalyos = new Haromszog("szabályos", 6);

Console.WriteLine(derekszogu);           // a felüldefiniált ToString fut le
Console.WriteLine(szabalyos);

// --- Tulajdonság írása és olvasása ---
derekszogu.C = 9;                        // a set lefut, 9 pozitív, elfogadja
Console.WriteLine($"Módosítás után: {derekszogu}");
Console.WriteLine($"Szerkeszthető? {(derekszogu.Szerkesztheto ? "igen" : "nem")}");

// --- Az ellenőrzés működés közben ---
try
{
    derekszogu.A = -2;                   // a set kivételt dob
}
catch (ArgumentException ex)
{
    Console.WriteLine($"Hiba: {ex.Message}");
}
Console.WriteLine($"Az A oldal maradt: {derekszogu.A}");

// derekszogu.Kerulet = 10;             // fordítási hiba: a Kerulet csak olvasható

// --- Objektumok listában ---
var lista = new List<Haromszog>
{
    derekszogu,
    szabalyos,
    new Haromszog("egyenlő szárú", 5, 5, 8),
    new Haromszog("tompaszögű", 2, 3, 4),
};

Console.WriteLine("\nSzerkeszthető háromszögek:");
Haromszog? legnagyobb = null;
foreach (var h in lista)
{
    if (!h.Szerkesztheto) continue;
    Console.WriteLine($"  {h}");
    if (legnagyobb == null || h.Terulet > legnagyobb.Terulet)
        legnagyobb = h;
}
Console.WriteLine($"Legnagyobb területű: {legnagyobb?.Nev}");
