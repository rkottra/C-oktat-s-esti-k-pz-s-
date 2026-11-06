namespace AlakzatokInterfesz;

// Több interfész egy osztályban. A CompareTo az ISikidom-on keresztül örökölt IComparable<ISikidom> része.
public class Kor : ISikidom, IAtmeretezheto
{
    public double Sugar { get; private set; }
    public string Nev => $"kör (r = {Sugar})";

    public Kor(double sugar) { Sugar = sugar; }

    public double Terulet() => Sugar * Sugar * Math.PI;    // interfész tag: nincs override
    public double Kerulet() => 2 * Sugar * Math.PI;
    public void Nagyit(double szorzo) => Sugar *= szorzo;

    public int CompareTo(ISikidom? masik) => masik == null ? 1 : Terulet().CompareTo(masik.Terulet());
}

public class Teglalap : ISikidom, IAtmeretezheto
{
    public double A { get; private set; }
    public double B { get; private set; }
    public string Nev => $"téglalap ({A} × {B})";

    public Teglalap(double a, double b) { A = a; B = b; }

    public double Terulet() => A * B;
    public double Kerulet() => 2 * (A + B);
    public void Nagyit(double szorzo) { A *= szorzo; B *= szorzo; }

    public int CompareTo(ISikidom? masik) => masik == null ? 1 : Terulet().CompareTo(masik.Terulet());
}

// Ez csak síkidom, nem átméretezhető (pl. egy telek, amelynek oldalai adottak).
public class Haromszog : ISikidom
{
    public double A { get; }
    public double B { get; }
    public double C { get; }
    public string Nev => $"háromszög ({A}, {B}, {C})";

    public Haromszog(double a, double b, double c) { A = a; B = b; C = c; }

    public double Terulet()
    {
        double s = (A + B + C) / 2;
        return Math.Sqrt(s * (s - A) * (s - B) * (s - C));
    }

    public double Kerulet() => A + B + C;

    public int CompareTo(ISikidom? masik) => masik == null ? 1 : Terulet().CompareTo(masik.Terulet());
}
