namespace Webaruhaz;

public class Konyv : Termek
{
    public string Szerzo { get; set; }

    public Konyv(string cim, int nettoAr, string szerzo)
        : base($"{cim} – {szerzo}", nettoAr)
    {
        Szerzo = szerzo;
    }

    public override int AfaKulcs() => 5;
}
