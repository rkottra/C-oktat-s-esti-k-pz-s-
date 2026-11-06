namespace KonyvtarKatalogus;

// konyvek.csv: Azonosito;Cim;Szerzo;Mufaj;BeszerzesEve;Peldany;Kolcsonzott
public class Konyv
{
    public string Azonosito { get; }
    public string Cim { get; }
    public string Szerzo { get; }
    public string Mufaj { get; }
    public int BeszerzesEve { get; }
    public int Peldany { get; }
    public int Kolcsonzott { get; }

    public Konyv(string sor)
    {
        string[] m = sor.Split(';');
        Azonosito = m[0];
        Cim = m[1];
        Szerzo = m[2];
        Mufaj = m[3];
        BeszerzesEve = int.Parse(m[4]);
        Peldany = int.Parse(m[5]);
        Kolcsonzott = int.Parse(m[6]);
    }

    public int Elerheto => Peldany - Kolcsonzott;
    public double Kihasznaltsag => Peldany == 0 ? 0 : 100.0 * Kolcsonzott / Peldany;

    public override string ToString() =>
        $"{Azonosito,-5}{Cim,-34}{Szerzo,-26}{Mufaj,-14}{Elerheto,3}/{Peldany,-3}";
}
