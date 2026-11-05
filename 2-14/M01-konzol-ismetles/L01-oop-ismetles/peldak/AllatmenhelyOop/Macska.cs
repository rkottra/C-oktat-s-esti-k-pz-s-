namespace AllatmenhelyOop;

public class Macska : Allat
{
    public bool Benti { get; set; }

    public Macska(int id, string nev, int kor, double suly, bool benti)
        : base("macska", id, nev, kor, suly)
    {
        Benti = benti;
    }

    public override string HangotAd() => "Miaú!";

    // A Bemutatkozik-ot nem írjuk felül: az Allat-beli változat fut le.

    public override string ToString() => base.ToString() + (Benti ? "  benti" : "  kinti");
}
