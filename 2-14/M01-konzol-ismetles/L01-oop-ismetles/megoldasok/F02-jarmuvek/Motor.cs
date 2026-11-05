namespace Jarmuvek;

public class Motor : Jarmu
{
    public int Hengerurtartalom { get; set; }   // cm3

    public Motor(string rendszam, int gyartasiEv, int hengerurtartalom) : base(rendszam, gyartasiEv)
    {
        Hengerurtartalom = hengerurtartalom;
    }

    public override string Tipus => "motor";

    public override int EvesDij() => 12000;

    public override string ToString() => base.ToString() + $"  {Hengerurtartalom} cm3";
}
