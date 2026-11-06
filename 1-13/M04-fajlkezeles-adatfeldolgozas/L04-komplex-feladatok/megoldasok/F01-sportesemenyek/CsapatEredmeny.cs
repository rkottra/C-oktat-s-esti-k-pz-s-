namespace Sportesemenyek;

// A tabella egy sora
public class CsapatEredmeny
{
    public string Nev { get; }
    public int Gyozelem { get; private set; }
    public int Dontetlen { get; private set; }
    public int Vereseg { get; private set; }
    public int Lott { get; private set; }
    public int Kapott { get; private set; }

    public CsapatEredmeny(string nev) { Nev = nev; }

    public int Merkozes => Gyozelem + Dontetlen + Vereseg;
    public int Pont => 3 * Gyozelem + Dontetlen;
    public int Golkulonbseg => Lott - Kapott;

    public void Rogzit(int sajat, int ellenfel)
    {
        Lott += sajat;
        Kapott += ellenfel;
        if (sajat > ellenfel) Gyozelem++;
        else if (sajat == ellenfel) Dontetlen++;
        else Vereseg++;
    }
}
