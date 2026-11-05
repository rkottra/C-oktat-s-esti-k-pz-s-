namespace Hegycsucsok;

public class Hegycsucs
{
    public string Nev { get; set; } = "";
    public string Hegyseg { get; set; } = "";
    public string Orszag { get; set; } = "";
    public string Kontinens { get; set; } = "";
    public int Magassag { get; set; }
    public int ElsoMegmaszas { get; set; }

    public bool Nyolcezres => Magassag >= 8000;

    public static Hegycsucs CsvSorbol(string sor)
    {
        string[] r = sor.Split(';');
        return new Hegycsucs
        {
            Nev = r[0],
            Hegyseg = r[1],
            Orszag = r[2],
            Kontinens = r[3],
            Magassag = int.Parse(r[4]),
            ElsoMegmaszas = int.Parse(r[5])
        };
    }
}
