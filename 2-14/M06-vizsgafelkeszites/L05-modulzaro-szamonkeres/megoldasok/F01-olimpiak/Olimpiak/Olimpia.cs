namespace Olimpiak;

// olimpiak.csv: ev;varos;orszag;kontinens;megrendezes_eve
public class Olimpia
{
    public int Ev { get; }
    public string Varos { get; }
    public string Orszag { get; }
    public string Kontinens { get; }
    public int MegrendezesEve { get; }

    // A tesztek ezzel a konstruktorral készítenek adatot, fájl nélkül
    public Olimpia(int ev, string varos, string orszag, string kontinens, int megrendezesEve)
    {
        Ev = ev;
        Varos = varos;
        Orszag = orszag;
        Kontinens = kontinens;
        MegrendezesEve = megrendezesEve;
    }

    public static Olimpia Sorbol(string sor)
    {
        string[] a = sor.Split(';');
        return new Olimpia(int.Parse(a[0]), a[1], a[2], a[3], int.Parse(a[4]));
    }

    public static List<Olimpia> BetoltCsv(string fajlnev)
        => File.ReadAllLines(fajlnev)
            .Skip(1)
            .Where(sor => sor.Trim() != "")
            .Select(Sorbol)
            .ToList();
}
