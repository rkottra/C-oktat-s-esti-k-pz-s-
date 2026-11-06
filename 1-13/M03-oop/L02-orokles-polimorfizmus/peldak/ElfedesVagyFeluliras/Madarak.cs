namespace ElfedesVagyFeluliras;

public class Madar
{
    // protected: a leszármazottak látják
    protected string nev;

    public Madar(string nev)
    {
        this.nev = nev;
    }

    // NEM virtuális: a leszármazott legfeljebb elfedheti.
    public void Bemutatkozik()
    {
        Console.WriteLine($"  [Madar.Bemutatkozik] {nev}: egy madár vagyok.");
    }

    // Virtuális: a leszármazott felülírhatja.
    public virtual void Enekel()
    {
        Console.WriteLine($"  [Madar.Enekel] {nev}: csip-csip.");
    }
}

public class Rigo : Madar
{
    public Rigo(string nev) : base(nev) { }

    // Elfedés: a "new" jelzi, hogy szándékos (nélküle a fordító figyelmeztet).
    // Madar típusú változón keresztül EZ NEM fut le.
    public new void Bemutatkozik()
    {
        Console.WriteLine($"  [Rigo.Bemutatkozik] {nev}: rigó vagyok!");
    }

    // Felülírás: Madar típusú változón keresztül is ez fut le.
    public override void Enekel()
    {
        Console.WriteLine($"  [Rigo.Enekel] {nev}: fütty-fütty-tilio!");
    }
}

public class Cinege : Madar
{
    public Cinege(string nev) : base(nev) { }

    public override void Enekel()
    {
        // base.Enekel(): előbb az ős változata, aztán a saját kiegészítés
        base.Enekel();
        Console.WriteLine($"  [Cinege.Enekel] {nev}: nyitni-kék!");
    }
}
