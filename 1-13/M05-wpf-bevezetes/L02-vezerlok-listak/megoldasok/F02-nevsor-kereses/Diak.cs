namespace NevsorKereses;

public class Diak
{
    public string Nev { get; set; } = "";
    public string Osztaly { get; set; } = "";
    public int Szuletesiev { get; set; }
    public double Atlag { get; set; }

    public override string ToString() => $"{Nev} ({Osztaly})";
}
