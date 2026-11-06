namespace Ingatlaniroda;

public class CsaladiHaz : Ingatlan
{
    private const int TelekNegyzetmeterAr = 18_000;
    private const int GarazsAr = 2_500_000;

    public int TelekTerulet { get; set; }
    public bool Garazs { get; set; }

    public CsaladiHaz(string cim, int alapterulet, int negyzetmeterAr, int telekTerulet, bool garazs)
        : base(cim, alapterulet, negyzetmeterAr)
    {
        TelekTerulet = telekTerulet;
        Garazs = garazs;
    }

    public override long Iranyar()
    {
        long ar = base.Iranyar() + (long)TelekTerulet * TelekNegyzetmeterAr;
        if (Garazs)
            ar += GarazsAr;
        return ar;
    }

    public override string Tipus() => "családi ház";
}
