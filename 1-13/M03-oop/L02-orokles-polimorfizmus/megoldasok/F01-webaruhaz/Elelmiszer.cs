namespace Webaruhaz;

public class Elelmiszer : Termek
{
    public bool Romlando { get; set; }

    public Elelmiszer(string nev, int nettoAr, bool romlando)
        : base(nev, nettoAr)
    {
        Romlando = romlando;
    }

    public override int AfaKulcs() => 18;

    public override string ToString()
    {
        return Romlando ? base.ToString() + "  (hűtve tárolandó)" : base.ToString();
    }
}
