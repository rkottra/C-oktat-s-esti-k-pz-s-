using System.ComponentModel.DataAnnotations;

namespace KonyvesboltImportExport.Models;

// A "több" oldal: minden könyv pontosan egy kiadóhoz tartozik.
public class Konyv
{
    public int Id { get; set; }

    [MaxLength(100)]
    public string Cim { get; set; } = "";

    [MaxLength(60)]
    public string Szerzo { get; set; } = "";

    public int KiadasEve { get; set; }
    public int Ar { get; set; }
    public int Keszlet { get; set; }

    // Idegen kulcs + navigációs tulajdonság (Include-dal töltjük be).
    public int KiadoId { get; set; }
    public Kiado? Kiado { get; set; }
}
