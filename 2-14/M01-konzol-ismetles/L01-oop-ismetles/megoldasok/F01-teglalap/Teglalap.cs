namespace Teglalapok;

public class Teglalap
{
    private double szelesseg;
    private double magassag;

    public double Szelesseg
    {
        get => szelesseg;
        set => szelesseg = value > 0 ? value : throw new ArgumentException("A szélesség csak pozitív lehet.");
    }

    public double Magassag
    {
        get => magassag;
        set => magassag = value > 0 ? value : throw new ArgumentException("A magasság csak pozitív lehet.");
    }

    // Számított, csak olvasható tulajdonságok
    public double Terulet => Szelesseg * Magassag;
    public double Kerulet => 2 * (Szelesseg + Magassag);
    public double Atlo => Math.Sqrt(Szelesseg * Szelesseg + Magassag * Magassag);
    public bool Negyzet => Szelesseg == Magassag;

    public Teglalap(double szelesseg, double magassag)
    {
        Szelesseg = szelesseg;
        Magassag = magassag;
    }

    // Négyzet: a másik konstruktort hívjuk
    public Teglalap(double oldal) : this(oldal, oldal)
    {
    }

    public override string ToString() =>
        $"{(Negyzet ? "Négyzet" : "Téglalap")} {Szelesseg} x {Magassag}: T = {Terulet:F2}, K = {Kerulet:F2}, átló = {Atlo:F2}";
}
