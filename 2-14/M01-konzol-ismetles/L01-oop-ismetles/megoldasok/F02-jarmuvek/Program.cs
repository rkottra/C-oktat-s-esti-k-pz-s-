using Jarmuvek;

var flotta = new List<Jarmu>
{
    new Szemelyauto("abc-123", 2019, 5),
    new Szemelyauto("AA-BB-101", 2008, 7),
    new Teherauto("TRK-404", 2015, 7.5),
    new Teherauto("TRK-405", 2021, 3.5),
    new Motor("M-777", 2012, 650),
};

// Hibás adat: a konstruktor a tulajdonságon keresztül ellenőriz
try
{
    flotta.Add(new Motor("M-1", 1890, 125));
}
catch (ArgumentException ex)
{
    Console.WriteLine($"Nem vettük fel: {ex.Message}\n");
}

Console.WriteLine("A flotta járművei:");
foreach (var j in flotta)
    Console.WriteLine($"  {j}");

int osszDij = 0;
int szemelyautok = 0, teherautok = 0, motorok = 0;
double osszTeherbiras = 0;
Jarmu legidosebb = flotta[0];
foreach (var j in flotta)
{
    osszDij += j.EvesDij();             // polimorfizmus: mindig a saját EvesDij fut
    if (j.GyartasiEv < legidosebb.GyartasiEv) legidosebb = j;

    if (j is Szemelyauto) szemelyautok++;
    else if (j is Teherauto t)
    {
        teherautok++;
        osszTeherbiras += t.Teherbiras;
    }
    else if (j is Motor) motorok++;
}

Console.WriteLine($"\nÉves díjak összesen: {osszDij:N0} Ft");
Console.WriteLine($"Személyautó: {szemelyautok}, teherautó: {teherautok}, motor: {motorok}");
Console.WriteLine($"Legidősebb jármű: {legidosebb.Rendszam} ({legidosebb.GyartasiEv})");
if (teherautok > 0)
    Console.WriteLine($"Teherautók átlagos teherbírása: {osszTeherbiras / teherautok:F2} t");
