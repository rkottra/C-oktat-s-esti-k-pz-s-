namespace KonyvtarKapcsolatok.Models;

// Az "1" oldal: egy szerzőnek több könyve lehet.
public class Szerzo
{
    public int Id { get; set; }
    public string Nev { get; set; } = "";
    public int SzuletesiEv { get; set; }

    // Gyűjtemény navigációs tulajdonság: a szerző könyvei.
    // Az adatbázisban NINCS ilyen oszlop – az EF Core a Konyvek.SzerzoId alapján tölti ki.
    public List<Konyv> Konyvek { get; set; } = new();
}
