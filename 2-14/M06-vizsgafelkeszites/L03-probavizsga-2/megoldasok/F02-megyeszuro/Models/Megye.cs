using System.ComponentModel.DataAnnotations.Schema;

namespace TelepulesekSzures.Models;

[Table("county")]
public class Megye
{
    [Column("id")] public int Id { get; set; }
    [Column("name")] public string Nev { get; set; } = "";

    // Egy megyéhez több település tartozik (egy-a-többhöz kapcsolat "több" oldala)
    public List<Telepules> Telepulesek { get; set; } = new List<Telepules>();
}
