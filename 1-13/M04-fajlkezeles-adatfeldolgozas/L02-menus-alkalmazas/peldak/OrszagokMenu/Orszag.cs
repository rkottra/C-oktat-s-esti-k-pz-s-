using System.Globalization;

namespace OrszagokMenu;

// europa.csv: Nev;Fovaros;Terulet;Nepesseg;Gdp   (terület km², GDP: egy főre jutó USD)
public class Orszag
{
    public string Nev { get; }
    public string Fovaros { get; }
    public double Terulet { get; }
    public int Nepesseg { get; }
    public int Gdp { get; }

    // CSV-sor → objektum
    public Orszag(string sor)
    {
        string[] m = sor.Split(';');
        Nev = m[0];
        Fovaros = m[1];
        Terulet = double.Parse(m[2], CultureInfo.InvariantCulture);
        Nepesseg = int.Parse(m[3]);
        Gdp = int.Parse(m[4]);
    }

    // Új ország felvételéhez
    public Orszag(string nev, string fovaros, double terulet, int nepesseg, int gdp)
    {
        Nev = nev;
        Fovaros = fovaros;
        Terulet = terulet;
        Nepesseg = nepesseg;
        Gdp = gdp;
    }

    public double Nepsuruseg => Terulet > 0 ? Nepesseg / Terulet : 0;

    // objektum → CSV-sor (a konstruktor párja, mentéshez)
    public string CsvSor() =>
        $"{Nev};{Fovaros};{Terulet.ToString(CultureInfo.InvariantCulture)};{Nepesseg};{Gdp}";

    public override string ToString() =>
        $"{Nev,-22}{Fovaros,-18}{Terulet,11:N0} km²{Nepesseg,13:N0} fő{Gdp,8:N0} $";
}
