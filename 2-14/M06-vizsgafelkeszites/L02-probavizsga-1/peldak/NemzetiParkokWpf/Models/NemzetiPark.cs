using System.ComponentModel.DataAnnotations.Schema;

namespace NemzetiParkokWpf.Models;

// A [Table] és [Column] attribútumokkal a magyar C#-nevek a meglévő tábla/oszlopnevekhez köthetők.
// (Ha a nevek egyeznének, az attribútum el is hagyható.)
[Table("nemzeti_park")]
public class NemzetiPark
{
    [Column("id")] public int Id { get; set; }
    [Column("nev")] public string Nev { get; set; } = "";
    [Column("szekhely")] public string Szekhely { get; set; } = "";
    [Column("alapitas_eve")] public int AlapitasEve { get; set; }
}
