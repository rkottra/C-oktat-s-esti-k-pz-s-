namespace Felhasznalok;

// sealed: ebből az osztályból már nem lehet tovább örökölni.
public sealed class Vendeg : Felhasznalo
{
    public Vendeg() : base("vendeg", "")
    {
    }

    // A vendégnek nincs jelszava: üres jelszóval léphet be.
    public override bool Ellenoriz(string jelszo) => jelszo == "";

    public override string Jogosultsag => "vendég";
}
