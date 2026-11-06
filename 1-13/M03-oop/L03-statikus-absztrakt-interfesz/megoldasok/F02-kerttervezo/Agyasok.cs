namespace Kerttervezo;

public class TeglalapAgyas : Agyas
{
    public double A { get; }
    public double B { get; }

    public TeglalapAgyas(string noveny, double vetomagPerNm, double a, double b)
        : base(noveny, vetomagPerNm)
    {
        A = a;
        B = b;
    }

    public override double Terulet() => A * B;
    public override double Kerulet() => 2 * (A + B);
    public override string Alak() => "téglalap";
}

public class KorAgyas : Agyas
{
    public double Sugar { get; }

    public KorAgyas(string noveny, double vetomagPerNm, double sugar)
        : base(noveny, vetomagPerNm)
    {
        Sugar = sugar;
    }

    public override double Terulet() => Sugar * Sugar * Math.PI;
    public override double Kerulet() => 2 * Sugar * Math.PI;
    public override string Alak() => "kör";
}

public class HaromszogAgyas : Agyas
{
    public double A { get; }
    public double B { get; }
    public double C { get; }

    public HaromszogAgyas(string noveny, double vetomagPerNm, double a, double b, double c)
        : base(noveny, vetomagPerNm)     // ez már lefutott, mire az ellenőrzéshez érünk!
    {
        if (a + b <= c || a + c <= b || b + c <= a)
            throw new ArgumentException($"{a}, {b}, {c} oldalakból nem szerkeszthető háromszög.");
        A = a;
        B = b;
        C = c;
    }

    public override double Terulet()
    {
        double s = (A + B + C) / 2;
        return Math.Sqrt(s * (s - A) * (s - B) * (s - C));
    }

    public override double Kerulet() => A + B + C;
    public override string Alak() => "háromszög";
}

// Nagy téglalap (a × b), amelynek egyik sarkából egy kisebb (c × d) téglalapot kivágunk.
public class LAgyas : Agyas
{
    public double A { get; }
    public double B { get; }
    public double C { get; }
    public double D { get; }

    public LAgyas(string noveny, double vetomagPerNm, double a, double b, double c, double d)
        : base(noveny, vetomagPerNm)
    {
        if (c >= a || d >= b)
            throw new ArgumentException("A kivágott rész kisebb kell legyen az egész ágyásnál.");
        A = a; B = b; C = c; D = d;
    }

    public override double Terulet() => A * B - C * D;
    public override double Kerulet() => 2 * (A + B);   // a sarok „befelé lépése” nem változtat rajta
    public override string Alak() => "L-alak";
}
