namespace HosokCsataja;

public class Magus : Hos
{
    private const int MaxMana = 30;
    private const int VarazslatAra = 10;

    public int Mana { get; private set; } = 20;

    public Magus(string nev, int maxElet, int tamadas) : base(nev, maxElet, tamadas) { }

    public override int Sebzes(Random rnd)
    {
        if (Mana >= VarazslatAra)
        {
            Mana -= VarazslatAra;
            megjegyzes = "varázslat";
            return base.Sebzes(rnd) * 3;
        }
        megjegyzes = "bottal";
        return base.Sebzes(rnd) / 2;
    }

    public override void KorVege()
    {
        Mana = Math.Min(MaxMana, Mana + 4);
    }

    public override string Kaszt() => "mágus";
}
