namespace RefaktoralasMinta;

// ÁTNÉZÉS UTÁN – adatosztály + tiszta, tesztelhető számoló metódusok

public class Eredmeny
{
    public string Nev { get; }
    public int Pont { get; }
    public int MaxPont { get; }

    // Számított tulajdonság: a százalékot egy helyen számoljuk
    public double Szazalek => 100.0 * Pont / MaxPont;

    public Eredmeny(string nev, int pont, int maxPont)
    {
        Nev = nev;
        Pont = pont;
        MaxPont = maxPont;
    }

    public static List<Eredmeny> Feldolgoz(IEnumerable<string> sorok)
        => sorok.Skip(1)
            .Select(sor => sor.Split(';'))
            .Select(a => new Eredmeny(a[0], int.Parse(a[1]), int.Parse(a[2])))
            .ToList();
}

public static class Ertekeles
{
    // Elnevezett konstans a varázsszám helyett
    public const double MegfeleltHatar = 40.0;

    public static int MegfeleltekSzama(List<Eredmeny> eredmenyek)
        => eredmenyek.Count(e => e.Szazalek >= MegfeleltHatar);

    // MaxBy: üres listára null-t ad, nem dob kivételt
    public static Eredmeny? Legjobb(List<Eredmeny> eredmenyek)
        => eredmenyek.MaxBy(e => e.Szazalek);
}
