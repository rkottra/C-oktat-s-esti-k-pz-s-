namespace HosokCsataja;

public class Ijasz : Hos
{
    public Ijasz(string nev, int maxElet, int tamadas) : base(nev, maxElet, tamadas) { }

    public override int Sebzes(Random rnd)
    {
        if (rnd.Next(100) < 30)
        {
            megjegyzes = "két nyíl";
            return base.Sebzes(rnd) + base.Sebzes(rnd);
        }
        return base.Sebzes(rnd);
    }

    public override string Kaszt() => "íjász";
}
