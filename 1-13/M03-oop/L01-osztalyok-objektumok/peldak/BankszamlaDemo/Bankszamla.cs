namespace BankszamlaDemo;

// Az osztály a "tervrajz": leírja, milyen adatai és műveletei lesznek
// minden bankszámla-objektumnak.
public class Bankszamla
{
    // --- Mező (adattag) ---
    // Privát: a Program.cs nem írhatja át közvetlenül.
    private string tulajdonos = "";

    // --- Tulajdonságok ---

    // Ellenőrzött tulajdonság saját privát mezővel.
    public string Tulajdonos
    {
        get { return tulajdonos; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("A tulajdonos neve nem lehet üres.");
            tulajdonos = value.Trim();
        }
    }

    // Kívülről csak olvasható: az egyenleg csak a Befizet és a Kivesz
    // metóduson keresztül változhat (private set).
    public int Egyenleg { get; private set; }

    // Számított tulajdonság: nincs mögötte mező, mindig újraszámolódik.
    public bool Ures => Egyenleg == 0;

    // --- Konstruktorok ---

    // Kétparaméteres konstruktor: tulajdonos és nyitó befizetés.
    public Bankszamla(string tulajdonos, int nyitoBefizetes)
    {
        // this.Tulajdonos: az objektum tulajdonsága; tulajdonos: a paraméter.
        // A tulajdonságnak adunk értéket, így az ellenőrzés itt is lefut.
        this.Tulajdonos = tulajdonos;
        if (nyitoBefizetes > 0)
            Befizet(nyitoBefizetes);
    }

    // Egyparaméteres konstruktor: üres számla. A : this(...) a fenti
    // konstruktort hívja, így nem kell a kódot megismételni.
    public Bankszamla(string tulajdonos) : this(tulajdonos, 0)
    {
    }

    // --- Metódusok (az objektum viselkedése) ---

    public void Befizet(int osszeg)
    {
        if (osszeg <= 0)
            throw new ArgumentException("Csak pozitív összeget lehet befizetni.");
        Egyenleg += osszeg;
    }

    // A visszatérési érték jelzi, sikerült-e a művelet.
    public bool Kivesz(int osszeg)
    {
        if (osszeg <= 0 || osszeg > Egyenleg)
            return false;
        Egyenleg -= osszeg;
        return true;
    }

    // A ToString felülírása: így a Console.WriteLine(szamla) értelmes szöveget ír ki.
    public override string ToString()
    {
        return $"{Tulajdonos,-12} {Egyenleg,10:N0} Ft";
    }
}
