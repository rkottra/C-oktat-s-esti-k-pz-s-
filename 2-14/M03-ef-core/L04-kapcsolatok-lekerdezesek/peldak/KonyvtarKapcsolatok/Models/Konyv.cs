namespace KonyvtarKapcsolatok.Models;

// Az "N" oldal: minden könyvnek pontosan egy szerzője van.
public class Konyv
{
    public int Id { get; set; }
    public string Cim { get; set; } = "";
    public int KiadasEve { get; set; }
    public int Ar { get; set; }

    // Idegenkulcs-tulajdonság: a név (SzerzoId = navigáció neve + Id) alapján
    // az EF Core magától felismeri, és FOREIGN KEY-t készít belőle.
    public int SzerzoId { get; set; }

    // Hivatkozás navigációs tulajdonság: a könyv szerzője (objektumként).
    // Csak akkor van kitöltve, ha betöltjük (Include) – különben null.
    public Szerzo Szerzo { get; set; } = null!;
}
