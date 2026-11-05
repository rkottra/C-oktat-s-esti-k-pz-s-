using System.ComponentModel.DataAnnotations.Schema;

namespace ElemekWpf.Models;

[Table("kategoria")]
public class Kategoria
{
    [Column("id")] public int Id { get; set; }
    [Column("nev")] public string Nev { get; set; } = "";
}
