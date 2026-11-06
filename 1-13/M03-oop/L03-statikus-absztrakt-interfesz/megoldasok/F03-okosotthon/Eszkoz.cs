namespace Okosotthon;

public abstract class Eszkoz
{
    public static int Darab { get; private set; }

    public string Nev { get; }
    public string Helyiseg { get; }

    protected Eszkoz(string nev, string helyiseg)
    {
        Nev = nev;
        Helyiseg = helyiseg;
        Darab++;
    }

    public abstract string Allapot();

    public override string ToString() => $"[{Helyiseg}] {Nev} – {Allapot()}";
}
