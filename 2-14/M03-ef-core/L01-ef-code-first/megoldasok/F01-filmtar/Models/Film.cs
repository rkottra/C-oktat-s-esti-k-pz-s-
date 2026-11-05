namespace Filmtar.Models;

public class Film
{
    public int Id { get; set; }
    public string Cim { get; set; } = "";
    public string Rendezo { get; set; } = "";
    public int Ev { get; set; }
    public int Hossz { get; set; }
    public double? Ertekeles { get; set; }
}
