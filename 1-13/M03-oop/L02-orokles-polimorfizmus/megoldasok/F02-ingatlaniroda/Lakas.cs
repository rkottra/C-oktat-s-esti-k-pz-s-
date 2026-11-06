namespace Ingatlaniroda;

public class Lakas : Ingatlan
{
    public int Emelet { get; set; }
    public bool VanLift { get; set; }

    public Lakas(string cim, int alapterulet, int negyzetmeterAr, int emelet, bool vanLift)
        : base(cim, alapterulet, negyzetmeterAr)
    {
        Emelet = emelet;
        VanLift = vanLift;
    }

    public override long Iranyar()
    {
        long ar = base.Iranyar();
        if (Emelet >= 3 && !VanLift)
            ar = ar * 90 / 100;         // 10% kedvezmény: magasan, lift nélkül
        return ar;
    }

    public override string Tipus() => "lakás";
}
