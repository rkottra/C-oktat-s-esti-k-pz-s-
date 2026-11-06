namespace AlkalmazottakOrokles;

// sealed: a Gyakornok-ból már nem lehet tovább örökölni.
public sealed class Gyakornok : Alkalmazott
{
    public string Iskola { get; set; }

    public Gyakornok(string nev, int alapber, string iskola)
        : base(nev, alapber)
    {
        Iskola = iskola;
        beosztas = "gyakornok";
    }

    // Felülírás: teljesen más számítás, az ős változatát nem használjuk.
    public override int Fizetes()
    {
        return Alapber * 60 / 100;
    }
}
