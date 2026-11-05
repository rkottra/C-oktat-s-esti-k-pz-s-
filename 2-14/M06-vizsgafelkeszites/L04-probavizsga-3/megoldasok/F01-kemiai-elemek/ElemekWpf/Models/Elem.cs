using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElemekWpf.Models;

[Table("elem")]
public class Elem
{
    // A rendszám a kulcs, de NEM automatikusan számozott: a felhasználó adja meg
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [Column("rendszam")] public int Rendszam { get; set; }

    [Column("vegyjel")] public string Vegyjel { get; set; } = "";
    [Column("nev")] public string Nev { get; set; } = "";
    [Column("kategoriaid")] public int KategoriaId { get; set; }
    [Column("periodus")] public int Periodus { get; set; }
    [Column("csoport")] public int Csoport { get; set; }
    [Column("atomtomeg")] public double Atomtomeg { get; set; }
    [Column("elektronegativitas")] public double? Elektronegativitas { get; set; }
    [Column("halmazallapot")] public string Halmazallapot { get; set; } = "";

    [ForeignKey(nameof(KategoriaId))]
    public Kategoria? Kategoria { get; set; }
}
