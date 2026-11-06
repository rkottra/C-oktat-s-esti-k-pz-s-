namespace Kosar;

public class Termek
{
    public string Nev { get; set; } = "";
    public int Ar { get; set; }
    public string Kategoria { get; set; } = "";

    public override string ToString() => $"{Nev} – {Ar:N0} Ft";
}

public class Tetel
{
    public Termek Termek { get; }
    public int Mennyiseg { get; set; }

    public Tetel(Termek termek, int mennyiseg)
    {
        Termek = termek;
        Mennyiseg = mennyiseg;
    }

    public int Reszosszeg => Termek.Ar * Mennyiseg;

    public override string ToString() => $"{Mennyiseg} × {Termek.Nev} ({Termek.Ar:N0} Ft) = {Reszosszeg:N0} Ft";
}
