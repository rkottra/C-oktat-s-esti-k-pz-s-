using System.ComponentModel.DataAnnotations.Schema;

namespace TelepulesekSzures.Models;

[Table("settlement")]
public class Telepules
{
    [Column("id")] public int Id { get; set; }
    [Column("name")] public string Nev { get; set; } = "";
    [Column("countyid")] public int MegyeId { get; set; }          // idegen kulcs
    [Column("region")] public string Jaras { get; set; } = "";
    [Column("population")] public int Nepesseg { get; set; }
    [Column("areasize")] public double Terulet { get; set; }

    // Navigációs tulajdonság: Include(t => t.Megye) tölti be.
    // A [ForeignKey] mondja meg, melyik tulajdonság az idegen kulcs (a név nem "MegyeId"-konvenciós oszlopra mutat).
    [ForeignKey(nameof(MegyeId))]
    public Megye? Megye { get; set; }
}
