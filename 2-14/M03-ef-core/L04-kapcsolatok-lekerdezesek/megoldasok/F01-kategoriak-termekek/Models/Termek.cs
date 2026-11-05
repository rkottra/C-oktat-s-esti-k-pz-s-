namespace Webshop.Models;

public class Termek
{
    public int Id { get; set; }
    public string Nev { get; set; } = "";
    public int Ar { get; set; }
    public int Keszlet { get; set; }

    // Idegen kulcs + hivatkozás navigáció
    public int KategoriaId { get; set; }
    public Kategoria Kategoria { get; set; } = null!;
}
