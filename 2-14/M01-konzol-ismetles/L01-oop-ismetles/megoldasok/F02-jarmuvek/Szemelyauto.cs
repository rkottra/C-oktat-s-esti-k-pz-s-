namespace Jarmuvek;

public class Szemelyauto : Jarmu
{
    public int Ulesek { get; set; }

    public Szemelyauto(string rendszam, int gyartasiEv, int ulesek) : base(rendszam, gyartasiEv)
    {
        Ulesek = ulesek;
    }

    public override string Tipus => "személyautó";

    // 30 000 Ft, 10 évnél idősebb autónál +10 000 Ft
    public override int EvesDij() => Kor > 10 ? 40000 : 30000;

    public override string ToString() => base.ToString() + $"  {Ulesek} ülés";
}
