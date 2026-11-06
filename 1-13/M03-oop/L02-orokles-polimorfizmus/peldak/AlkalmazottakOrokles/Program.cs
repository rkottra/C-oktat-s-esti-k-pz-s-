using AlkalmazottakOrokles;

// Ős típusú lista: bármelyik leszármazott belefér.
List<Alkalmazott> dolgozok = new List<Alkalmazott>
{
    new Alkalmazott("Kovács Péter", 520000),
    new Vezeto("Szilágyi Réka", 650000, 200000, 6),
    new Gyakornok("Horváth Bence", 400000, "Esti technikum"),
    new Alkalmazott("Lakatos Edit", 480000),
    new Vezeto("Fodor Gábor", 700000, 150000, 3),
    new Gyakornok("Varga Lili", 380000, "Szakgimnázium")
};

// Többalakúság: ugyanaz a hívás (ToString, Fizetes), de mindig
// az objektum VALÓDI típusának metódusa fut le.
Console.WriteLine("Dolgozók:");
foreach (Alkalmazott a in dolgozok)
    Console.WriteLine("  " + a);

int berkoltseg = 0;
foreach (Alkalmazott a in dolgozok)
    berkoltseg += a.Fizetes();
Console.WriteLine($"\nHavi bérköltség: {berkoltseg:N0} Ft");

// A leszármazott saját tagjait (Potlek, Iskola) csak típusvizsgálat után érjük el.
Console.WriteLine("\nVezetők pótléka:");
int beosztottak = 0;
foreach (Alkalmazott a in dolgozok)
{
    if (a is Vezeto v)
    {
        Console.WriteLine($"  {v.Nev}: {v.Potlek:N0} Ft");
        beosztottak += v.BeosztottakSzama;
    }
}
Console.WriteLine($"Beosztottak összesen: {beosztottak} fő");

int gyakornokDb = 0;
foreach (Alkalmazott a in dolgozok)
    if (a is Gyakornok)
        gyakornokDb++;
Console.WriteLine($"Gyakornokok száma: {gyakornokDb}");

// Hibás adat: az ős ellenőrzése a leszármazott létrehozásakor is lefut.
try
{
    dolgozok.Add(new Gyakornok("Teszt Elek", 100000, "Ismeretlen"));
}
catch (ArgumentException ex)
{
    Console.WriteLine($"\nHiba: {ex.Message}");
}

// Ezek NEM fordulnak le – próbáld ki, milyen hibát kapsz:
// Console.WriteLine(dolgozok[1].Potlek);   // Alkalmazott típuson nincs Potlek
// dolgozok[0].beosztas = "főnök";          // protected: kívülről nem elérhető
// class NyariGyakornok : Gyakornok { }     // a Gyakornok sealed
