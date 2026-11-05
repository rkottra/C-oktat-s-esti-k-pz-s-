namespace RetegzettEpuletek.Modell;

// Modell: csak adatot hordoz, semmit nem tud fájlról vagy konzolról.
public class Epulet
{
    public string Nev { get; set; } = "";
    public string Varos { get; set; } = "";
    public string Orszag { get; set; } = "";
    public int Epult { get; set; }
    public int Magassag { get; set; }   // méter

    public override string ToString() => $"{Nev} ({Varos}, {Epult}) – {Magassag} m";
}
