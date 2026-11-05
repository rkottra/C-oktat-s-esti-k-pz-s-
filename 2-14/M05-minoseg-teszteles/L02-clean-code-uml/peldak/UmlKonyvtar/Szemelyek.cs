namespace UmlKonyvtar;

// UML: <<abstract>> Szemely, a Szerep() dőlt betűs (absztrakt) művelet.
public abstract class Szemely
{
    public string Nev { get; }

    protected Szemely(string nev)
    {
        Nev = nev;
    }

    public abstract string Szerep();

    public override string ToString() => $"{Nev} ({Szerep()})";
}

// UML: Szemely <|-- Olvaso (öröklődés: üres háromszögű nyíl az ősosztály felé)
public class Olvaso : Szemely
{
    public int OlvasojegySzam { get; }

    public Olvaso(string nev, int olvasojegySzam) : base(nev)
    {
        OlvasojegySzam = olvasojegySzam;
    }

    public override string Szerep() => "olvasó";
}

// UML: Szemely <|-- Konyvtaros
public class Konyvtaros : Szemely
{
    public Konyvtaros(string nev) : base(nev) { }

    public override string Szerep() => "könyvtáros";
}
