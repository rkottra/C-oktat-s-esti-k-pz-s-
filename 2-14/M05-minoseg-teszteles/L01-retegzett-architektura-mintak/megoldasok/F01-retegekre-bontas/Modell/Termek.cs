namespace RetegzettKeszlet.Modell;

public class Termek
{
    public string Nev { get; set; } = "";
    public string Kategoria { get; set; } = "";
    public int Ar { get; set; }
    public int Keszlet { get; set; }

    // Egy termék készletértéke – ez a termékhez tartozó tudás, ezért itt a helye.
    public long Keszletertek => (long)Ar * Keszlet;
}
