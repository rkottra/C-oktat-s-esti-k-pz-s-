namespace AllatmenhelyOop;

// Absztrakt ősosztály: önmagában nem példányosítható (new Allat(...) hiba),
// csak a leszármazottai. A közös adatok és műveletek itt vannak.
public abstract class Allat
{
    private string nev = "";
    private int kor;

    public int Id { get; set; }

    // Ellenőrzött tulajdonság: üres nevet nem fogad el, a szóközöket levágja.
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

    public int Kor
    {
        get => kor;
        set => kor = value >= 0 ? value : throw new ArgumentException("Az életkor nem lehet negatív.");
    }

    public double Suly { get; set; }

    // protected: csak ez az osztály és a leszármazottai látják.
    protected string Faj { get; }

    protected Allat(string faj, int id, string nev, int kor, double suly)
    {
        Faj = faj;
        Id = id;
        Nev = nev;
        Kor = kor;
        Suly = suly;
    }

    // Absztrakt metódus: nincs törzse, minden leszármazottnak kötelező megírnia (override).
    public abstract string HangotAd();

    // Virtuális metódus: van alapértelmezett működése, a leszármazott felülírhatja.
    public virtual string Bemutatkozik() => $"{Nev} vagyok, egy {Kor} éves {Faj}.";

    public override string ToString() => $"#{Id,-3} {Faj,-6} {Nev,-10} {Kor,2} év {Suly,5:F1} kg";
}
