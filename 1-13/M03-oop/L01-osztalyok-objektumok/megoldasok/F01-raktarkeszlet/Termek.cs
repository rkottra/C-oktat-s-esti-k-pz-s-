namespace Raktarkeszlet;

public class Termek
{
    private string nev = "";
    private int ar;

    public string Nev
    {
        get { return nev; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("A terméknév nem lehet üres.");
            nev = value.Trim();
        }
    }

    public int Ar
    {
        get { return ar; }
        set
        {
            if (value <= 0)
                throw new ArgumentException("Az ár csak pozitív lehet.");
            ar = value;
        }
    }

    // Kívülről csak olvasható: csak a Bevetelez és az Elad módosítja.
    public int Keszlet { get; private set; }

    // Számított tulajdonság.
    public int Ertek => Ar * Keszlet;

    public Termek(string nev, int ar, int keszlet)
    {
        Nev = nev;
        Ar = ar;
        if (keszlet < 0)
            throw new ArgumentException("A készlet nem lehet negatív.");
        Keszlet = keszlet;
    }

    public void Bevetelez(int db)
    {
        if (db <= 0)
            throw new ArgumentException("Csak pozitív darabszámot lehet bevételezni.");
        Keszlet += db;
    }

    public bool Elad(int db)
    {
        if (db <= 0 || db > Keszlet)
            return false;
        Keszlet -= db;
        return true;
    }

    public override string ToString()
    {
        return $"{Nev,-18}{Ar,8:N0} Ft{Keszlet,5} db{Ertek,10:N0} Ft";
    }
}
