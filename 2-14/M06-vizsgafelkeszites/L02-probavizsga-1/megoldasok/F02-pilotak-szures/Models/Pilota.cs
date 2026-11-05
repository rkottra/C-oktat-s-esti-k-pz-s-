using System.ComponentModel.DataAnnotations.Schema;

namespace PilotakSzures.Models;

// A pilots tábla egy sora. A tábla és az oszlopok angol nevűek (így kaptuk az SQL-szkriptben),
// a C#-tulajdonságok magyarok – a megfeleltetést az attribútumok végzik.
[Table("pilots")]
public class Pilota
{
    [Column("id")] public int Id { get; set; }
    [Column("name")] public string Nev { get; set; } = "";
    [Column("gender")] public string Nem { get; set; } = "";          // CHAR(1): "M" vagy "F"
    [Column("birthdate")] public DateTime? SzuletesiDatum { get; set; } // NULL is lehet
    [Column("nation")] public string Nemzetiseg { get; set; } = "";
}
