using System.ComponentModel.DataAnnotations;

namespace KonyvesboltImportExport.Models;

// A "egy" oldal: egy kiadónak sok könyve lehet.
public class Kiado
{
    public int Id { get; set; }

    [MaxLength(60)]
    public string Nev { get; set; } = "";

    public List<Konyv> Konyvek { get; set; } = new();
}
