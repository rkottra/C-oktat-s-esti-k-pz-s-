namespace Mozijegy;

public class Eloadas
{
    private string filmCim = "";
    private int jegyAr;

    public string FilmCim
    {
        get { return filmCim; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("A film címe nem lehet üres.");
            filmCim = value.Trim();
        }
    }

    public string Kezdes { get; set; }

    // Csak olvasható: csak a konstruktorban kaphat értéket.
    public int Ferohely { get; }

    // Kívülről csak olvasható: a Foglal és a Lemond módosítja.
    public int Foglalt { get; private set; }

    public int JegyAr
    {
        get { return jegyAr; }
        set
        {
            if (value <= 0)
                throw new ArgumentException("A jegyár csak pozitív lehet.");
            jegyAr = value;
        }
    }

    // Számított tulajdonságok
    public int Szabad => Ferohely - Foglalt;
    public bool Telt => Szabad == 0;
    public double Kihasznaltsag => 100.0 * Foglalt / Ferohely;
    public int Bevetel => Foglalt * JegyAr;

    public Eloadas(string filmCim, string kezdes, int ferohely, int jegyAr)
    {
        if (ferohely < 1)
            throw new ArgumentException("A férőhely legalább 1 legyen.");
        FilmCim = filmCim;
        Kezdes = kezdes;
        Ferohely = ferohely;
        JegyAr = jegyAr;
    }

    public bool Foglal(int db)
    {
        if (db <= 0 || db > Szabad)
            return false;
        Foglalt += db;
        return true;
    }

    public bool Lemond(int db)
    {
        if (db <= 0 || db > Foglalt)
            return false;
        Foglalt -= db;
        return true;
    }

    public override string ToString()
    {
        string telt = Telt ? " TELT HÁZ" : "";
        return $"{Kezdes} {FilmCim,-26} {Foglalt,3}/{Ferohely,-3} ({Kihasznaltsag:F0} %){telt}";
    }
}
