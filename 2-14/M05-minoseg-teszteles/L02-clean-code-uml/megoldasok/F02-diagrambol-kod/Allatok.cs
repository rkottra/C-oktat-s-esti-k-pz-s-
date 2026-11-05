namespace Allatorvos;

public abstract class Allat
{
    public string Nev { get; }
    public int SzuletesiEv { get; }

    protected Allat(string nev, int szuletesiEv)
    {
        Nev = nev;
        SzuletesiEv = szuletesiEv;
    }

    public int Kor(int evszam) => evszam - SzuletesiEv;

    public abstract int OltasiDij();

    // Bővítés a diagramhoz képest (a megoldás README-jében be van rajzolva): így nem kell típusvizsgálat a kiíráshoz.
    public abstract string Leiras();
}

public class Kutya : Allat
{
    public string Fajta { get; }

    public Kutya(string nev, int szuletesiEv, string fajta) : base(nev, szuletesiEv)
    {
        Fajta = fajta;
    }

    public override int OltasiDij() => 6500;

    public override string Leiras() => $"kutya ({Fajta})";
}

public class Macska : Allat
{
    public bool Benti { get; }

    public Macska(string nev, int szuletesiEv, bool benti) : base(nev, szuletesiEv)
    {
        Benti = benti;
    }

    public override int OltasiDij() => Benti ? 4500 : 5500;

    public override string Leiras() => Benti ? "macska (benti)" : "macska (kijáró)";
}
