namespace Jarmuvek;

public class Teherauto : Jarmu
{
    public double Teherbiras { get; set; }   // tonnában

    public Teherauto(string rendszam, int gyartasiEv, double teherbiras) : base(rendszam, gyartasiEv)
    {
        Teherbiras = teherbiras;
    }

    public override string Tipus => "teherautó";

    // 50 000 Ft + tonnánként 15 000 Ft
    public override int EvesDij() => 50000 + (int)(Teherbiras * 15000);

    public override string ToString() => base.ToString() + $"  {Teherbiras} t";
}
