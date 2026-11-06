namespace AlkalmazottakOrokles;

// Ősosztály: a közös adatok és műveletek itt vannak, egyszer megírva.
public class Alkalmazott
{
    private string nev = "";        // private: a leszármazott sem éri el közvetlenül
    private int alapber;

    // protected: a leszármazottak kódja elérheti, a Program.cs nem.
    protected string beosztas = "alkalmazott";

    public string Nev
    {
        get => nev;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("A név nem lehet üres.");
            nev = value.Trim();
        }
    }

    public int Alapber
    {
        get => alapber;
        set
        {
            if (value < 290800)     // a 2025-ös minimálbér
                throw new ArgumentException($"{Nev}: az alapbér nem lehet a minimálbér alatt.");
            alapber = value;
        }
    }

    public Alkalmazott(string nev, int alapber)
    {
        Nev = nev;
        Alapber = alapber;
    }

    // virtual: a leszármazott felülírhatja.
    public virtual int Fizetes()
    {
        return Alapber;
    }

    public override string ToString()
    {
        return $"{Nev,-16} {beosztas,-12} {Fizetes(),9:N0} Ft";
    }
}
