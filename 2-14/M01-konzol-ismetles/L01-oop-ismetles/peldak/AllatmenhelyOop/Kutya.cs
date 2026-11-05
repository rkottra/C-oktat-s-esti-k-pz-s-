namespace AllatmenhelyOop;

// Öröklődés: a Kutya mindent megkap az Allat-tól, és újakat is hozzátesz.
public class Kutya : Allat
{
    public string Fajta { get; set; }
    public bool Chipes { get; set; }

    // A ": base(...)" az ősosztály konstruktorát hívja meg az örökölt adatokkal.
    public Kutya(int id, string nev, int kor, double suly, string fajta, bool chipes)
        : base("kutya", id, nev, kor, suly)
    {
        Fajta = fajta;
        Chipes = chipes;
    }

    // Rövidebb konstruktor: a ": this(...)" a fenti konstruktort hívja alapértékekkel.
    public Kutya(int id, string nev) : this(id, nev, 0, 0, "ismeretlen", false)
    {
    }

    // Az absztrakt metódus kötelező megvalósítása.
    public override string HangotAd() => "Vau-vau!";

    // A virtuális metódus kiegészítése: base.Bemutatkozik() az ős változatát hívja.
    public override string Bemutatkozik() => base.Bemutatkozik() + $" Fajtám: {Fajta}.";

    public override string ToString() => base.ToString() + $"  {Fajta}{(Chipes ? ", chipes" : "")}";
}
