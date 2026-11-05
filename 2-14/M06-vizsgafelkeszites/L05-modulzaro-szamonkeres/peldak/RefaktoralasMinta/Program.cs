using RefaktoralasMinta;

// Ugyanaz a feladat kétféleképpen: "Hány vizsgázó érte el a megfelelt szintet (legalább 40%),
// és mennyi a legjobb eredmény?" – az adatsor kitalált, csak a szerkezet a lényeg.
string[] sorok =
{
    "nev;pont;max",
    "A vizsgázó;62;100",
    "B vizsgázó;35;100",
    "C vizsgázó;48;100",
    "D vizsgázó;91;100",
    "E vizsgázó;47;50"      // 94% – pontra kevesebb, százalékra a legjobb!
};

Console.WriteLine("Átnézés előtt:");
Elotte.Futtat(sorok);

Console.WriteLine();
Console.WriteLine("Átnézés után:");
List<Eredmeny> eredmenyek = Eredmeny.Feldolgoz(sorok);
Console.WriteLine($"\tMegfelelt: {Ertekeles.MegfeleltekSzama(eredmenyek)} fő");
Console.WriteLine($"\tLegjobb: {Ertekeles.Legjobb(eredmenyek)?.Nev ?? "nincs adat"}");
