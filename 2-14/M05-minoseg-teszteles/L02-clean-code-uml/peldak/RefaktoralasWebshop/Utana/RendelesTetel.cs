namespace RefaktoralasWebshop.Utana;

public class RendelesTetel
{
    public string Nev { get; }
    public int Egysegar { get; }
    public int Darab { get; }

    public RendelesTetel(string nev, int egysegar, int darab)
    {
        Nev = nev;
        Egysegar = egysegar;
        Darab = darab;
    }

    public int Osszeg => Egysegar * Darab;

    // A szövegfeldolgozás külön helyre került: a kalkulátornak nem kell tudnia, hogyan néz ki egy sor.
    public static RendelesTetel Feldolgoz(string sor)
    {
        string[] mezok = sor.Split(';');
        return new RendelesTetel(mezok[0], int.Parse(mezok[1]), int.Parse(mezok[2]));
    }
}
