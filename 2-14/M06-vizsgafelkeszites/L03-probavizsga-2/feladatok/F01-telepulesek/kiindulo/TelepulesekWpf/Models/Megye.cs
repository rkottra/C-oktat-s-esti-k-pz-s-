namespace TelepulesekWpf.Models;

// Kiinduló modell a county táblához – az adatbázishoz kötést neked kell elvégezned.
public class Megye
{
    public int Id { get; set; }
    public string Nev { get; set; } = "";
    public List<Telepules> Telepulesek { get; set; } = new List<Telepules>();
}
