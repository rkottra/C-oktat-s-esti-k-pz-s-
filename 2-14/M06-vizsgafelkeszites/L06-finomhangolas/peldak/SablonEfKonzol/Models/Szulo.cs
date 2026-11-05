using System.ComponentModel.DataAnnotations.Schema;

namespace SablonEfKonzol.Models;

// SABLON – az "egy" oldal (pl. megye, kategória). Írd át a tábla- és oszlopneveket!
[Table("szulo_tabla")]
public class Szulo
{
    [Column("id")] public int Id { get; set; }
    [Column("nev")] public string Nev { get; set; } = "";

    public List<Gyerek> Gyerekek { get; set; } = new List<Gyerek>();
}
