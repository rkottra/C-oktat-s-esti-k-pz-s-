using System.Globalization;

namespace Orszagok;

// Formátum: nev;fovaros;foldresz;terulet_km2;allamforma;nepesseg_ezer_fo
public class Orszag
{
    public string Nev { get; set; }
    public string Fovaros { get; set; }
    public string Foldresz { get; set; }
    public double Terulet { get; set; }        // km²
    public string Allamforma { get; set; }
    public int Nepesseg { get; set; }          // ezer fő

    // Számított tulajdonság: fő / km²
    public double Nepsuruseg => Nepesseg * 1000.0 / Terulet;

    public Orszag(string sor)
    {
        string[] adatok = sor.Split(';');
        Nev = adatok[0];
        Fovaros = adatok[1];
        Foldresz = adatok[2];
        Terulet = double.Parse(adatok[3], CultureInfo.InvariantCulture);
        Allamforma = adatok[4];
        Nepesseg = int.Parse(adatok[5]);
    }

    public override string ToString() => $"{Nev} ({Fovaros}, {Foldresz})";

    // yield return: a sorokat egyenként adja vissza, a hívó .ToList()-tel gyűjti listába.
    public static IEnumerable<Orszag> FajlbolBetolt(string fajlnev)
    {
        foreach (string sor in File.ReadAllLines(fajlnev).Skip(1))   // Skip(1): fejléc kihagyása
        {
            if (sor.Trim() != "")
                yield return new Orszag(sor);
        }
    }
}
