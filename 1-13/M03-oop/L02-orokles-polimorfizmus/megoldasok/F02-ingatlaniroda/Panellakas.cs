namespace Ingatlaniroda;

// Többszintű öröklődés: Ingatlan → Lakas → Panellakas
public class Panellakas : Lakas
{
    public Panellakas(string cim, int alapterulet, int negyzetmeterAr, int emelet, bool vanLift)
        : base(cim, alapterulet, negyzetmeterAr, emelet, vanLift)
    {
    }

    // A base.Iranyar() már a Lakas szabályával (lift) számol, erre jön a további 15%.
    public override long Iranyar() => base.Iranyar() * 85 / 100;

    public override string Tipus() => "panellakás";
}
