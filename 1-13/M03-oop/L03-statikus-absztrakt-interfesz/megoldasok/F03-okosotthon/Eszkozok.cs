namespace Okosotthon;

public class Lampa : Eszkoz, IKapcsolhato, ISzabalyozhato, IFogyaszto
{
    private int szint = 100;
    public double MaxWatt { get; }
    public bool Bekapcsolva { get; private set; }

    public int MinSzint => 0;
    public int MaxSzint => 100;
    public int Szint
    {
        get => szint;
        set => szint = Math.Clamp(value, MinSzint, MaxSzint);
    }

    public Lampa(string nev, string helyiseg, double maxWatt) : base(nev, helyiseg)
    {
        MaxWatt = maxWatt;
    }

    public void Be() => Bekapcsolva = true;
    public void Ki() => Bekapcsolva = false;
    public double Teljesitmeny() => Bekapcsolva ? MaxWatt * Szint / 100 : 0;

    public override string Allapot() =>
        Bekapcsolva ? $"BE, {Szint}%, {Teljesitmeny():0.#} W" : "KI";
}

public class Termosztat : Eszkoz, ISzabalyozhato, IFogyaszto
{
    private int szint = 20;

    public int MinSzint => 16;
    public int MaxSzint => 28;
    public int Szint
    {
        get => szint;
        set => szint = Math.Clamp(value, MinSzint, MaxSzint);
    }

    public Termosztat(string nev, string helyiseg) : base(nev, helyiseg) { }

    public double Teljesitmeny() => (Szint - MinSzint) * 150;

    public override string Allapot() => $"{Szint} °C, fűtés {Teljesitmeny():0} W";
}

public class Redony : Eszkoz, ISzabalyozhato
{
    private int szint;

    public int MinSzint => 0;
    public int MaxSzint => 100;
    public int Szint
    {
        get => szint;
        set => szint = Math.Clamp(value, MinSzint, MaxSzint);
    }

    public Redony(string nev, string helyiseg) : base(nev, helyiseg) { }

    public override string Allapot() => $"{Szint}% nyitva";
}

public class Konnektor : Eszkoz, IKapcsolhato, IFogyaszto
{
    public string Keszulek { get; }
    public double KeszulekWatt { get; }
    public bool Bekapcsolva { get; private set; }

    public Konnektor(string nev, string helyiseg, string keszulek, double keszulekWatt) : base(nev, helyiseg)
    {
        Keszulek = keszulek;
        KeszulekWatt = keszulekWatt;
    }

    public void Be() => Bekapcsolva = true;
    public void Ki() => Bekapcsolva = false;
    public double Teljesitmeny() => Bekapcsolva ? KeszulekWatt : 0;

    public override string Allapot() =>
        Bekapcsolva ? $"BE ({Keszulek}), {KeszulekWatt:0} W" : $"KI ({Keszulek})";
}
