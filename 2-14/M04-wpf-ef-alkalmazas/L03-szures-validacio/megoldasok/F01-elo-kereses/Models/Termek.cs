using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RaktarKereses.Models;

public class Termek
{
    public int Id { get; set; }

    [MaxLength(20)]
    public string Cikkszam { get; set; } = "";

    [MaxLength(80)]
    public string Nev { get; set; } = "";

    public int Ar { get; set; }
    public int Darab { get; set; }

    public int KategoriaId { get; set; }
    public Kategoria? Kategoria { get; set; }

    // Számított érték: nem kerül az adatbázisba, csak a felületen jelenik meg.
    [NotMapped]
    public int Ertek => Ar * Darab;
}
