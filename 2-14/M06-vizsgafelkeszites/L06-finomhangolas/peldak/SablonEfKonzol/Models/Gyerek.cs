using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SablonEfKonzol.Models;

// SABLON – a "több" oldal (pl. település, elem). Írd át a tábla- és oszlopneveket!
[Table("gyerek_tabla")]
public class Gyerek
{
    // Ha a kulcsot NEM az adatbázis generálja (nincs AUTO_INCREMENT), vedd ki a kommentet:
    // [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [Key]
    [Column("id")] public int Id { get; set; }

    [Column("nev")] public string Nev { get; set; } = "";
    [Column("ertek")] public double? Ertek { get; set; }         // NULL-t engedő oszlop → nullable
    [Column("datum")] public DateTime? Datum { get; set; }

    [Column("szulo_id")] public int SzuloId { get; set; }        // idegen kulcs

    [ForeignKey(nameof(SzuloId))]
    public Szulo? Szulo { get; set; }                            // navigációs tulajdonság
}
