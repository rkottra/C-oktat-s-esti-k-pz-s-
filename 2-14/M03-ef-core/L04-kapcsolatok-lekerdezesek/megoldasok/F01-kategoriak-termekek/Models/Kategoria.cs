namespace Webshop.Models;

public class Kategoria
{
    public int Id { get; set; }
    public string Nev { get; set; } = "";

    // 1:N – egy kategóriában több termék
    public List<Termek> Termekek { get; set; } = new();
}
