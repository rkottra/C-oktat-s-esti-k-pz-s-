namespace HaromszogTulajdonsagok;

// Egységbezárás: az adatok (oldalak) és a rajtuk dolgozó műveletek egy osztályban.
public class Haromszog
{
    // Privát mezők: kívülről nem írhatók közvetlenül.
    private double a;
    private double b;
    private double c;

    // Tulajdonság (property) ellenőrzéssel: a set minden értékadáskor lefut.
    public double A
    {
        get { return a; }
        set
        {
            if (value <= 0)
                throw new ArgumentException("Az oldal hossza csak pozitív lehet.");
            a = value;
        }
    }

    // Ugyanez rövidebben, "=>" (expression body) formában.
    public double B
    {
        get => b;
        set => b = value > 0 ? value : throw new ArgumentException("Az oldal hossza csak pozitív lehet.");
    }

    public double C
    {
        get => c;
        set => c = value > 0 ? value : throw new ArgumentException("Az oldal hossza csak pozitív lehet.");
    }

    // Automatikus tulajdonság: nincs ellenőrzés, a fordító készíti el a mezőt.
    public string Nev { get; set; } = "névtelen";

    // Csak olvasható, számított tulajdonságok: nincs set, nincs mögöttes mező.
    public double Kerulet => A + B + C;

    public bool Szerkesztheto => A + B > C && A + C > B && B + C > A;

    public double Terulet
    {
        get
        {
            if (!Szerkesztheto) return 0;
            double s = Kerulet / 2;                       // Hérón-képlet
            return Math.Sqrt(s * (s - A) * (s - B) * (s - C));
        }
    }

    // Konstruktor: a példányosításkor fut le, itt adunk kezdőértéket.
    // Az értékadás a tulajdonságokon keresztül történik, így az ellenőrzés itt is érvényes.
    public Haromszog(string nev, double a, double b, double c)
    {
        Nev = nev;
        A = a;
        B = b;
        C = c;
    }

    // Konstruktor-túlterhelés: szabályos háromszög egyetlen oldalból.
    // A ": this(...)" a másik konstruktort hívja, így nem ismételjük a kódot.
    public Haromszog(string nev, double oldal) : this(nev, oldal, oldal, oldal)
    {
    }

    // A ToString felüldefiniálása: ezt használja a Console.WriteLine és a $"{...}".
    public override string ToString()
    {
        string allapot = Szerkesztheto ? $"K = {Kerulet:F1}, T = {Terulet:F2}" : "nem szerkeszthető";
        return $"{Nev} ({A}; {B}; {C}) – {allapot}";
    }
}
