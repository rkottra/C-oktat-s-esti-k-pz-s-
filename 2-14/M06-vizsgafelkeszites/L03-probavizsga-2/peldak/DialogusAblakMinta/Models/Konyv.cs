namespace DialogusAblakMinta.Models;

// A "több" oldal (mint a település): idegen kulcs + navigációs tulajdonság
public class Konyv
{
    public string Cim { get; set; } = "";
    public string Szerzo { get; set; } = "";
    public int MufajId { get; set; }
    public Mufaj? Mufaj { get; set; }
    public int KiadasEve { get; set; }
}
