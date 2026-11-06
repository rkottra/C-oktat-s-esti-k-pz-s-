namespace HosokCsataja;

public class Harcos : Hos
{
    private const int Pancel = 3;

    public Harcos(string nev, int maxElet, int tamadas) : base(nev, maxElet, tamadas) { }

    public override int Sebzes(Random rnd)
    {
        int sebzes = base.Sebzes(rnd);
        if (rnd.Next(100) < 25)
        {
            megjegyzes = "KRITIKUS!";
            sebzes *= 2;
        }
        return sebzes;
    }

    // A páncél elnyel valamennyit, de az életet továbbra is az ős módosítja.
    public override void Serul(int pont)
    {
        base.Serul(Math.Max(1, pont - Pancel));
    }

    public override string Kaszt() => "harcos";
}
