namespace PilotakWpf.Models;

// Kiinduló modellosztály. Az adatbázis-kapcsolathoz egészítsd ki
// (pl. [Table]/[Column] attribútumokkal), vagy generáld újra Scaffold-DbContext-tel.
public class Pilota
{
    public int Id { get; set; }
    public string Nev { get; set; } = "";
    public string Nem { get; set; } = "";
    public DateTime? SzuletesiDatum { get; set; }
    public string Nemzetiseg { get; set; } = "";
}
