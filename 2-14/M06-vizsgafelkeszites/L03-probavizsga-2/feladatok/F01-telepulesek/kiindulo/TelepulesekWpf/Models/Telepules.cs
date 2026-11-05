namespace TelepulesekWpf.Models;

// Kiinduló modell a settlement táblához – az adatbázishoz kötést neked kell elvégezned.
public class Telepules
{
    public int Id { get; set; }
    public string Nev { get; set; } = "";
    public int MegyeId { get; set; }
    public string Jaras { get; set; } = "";
    public int Nepesseg { get; set; }
    public double Terulet { get; set; }
    public Megye? Megye { get; set; }
}
