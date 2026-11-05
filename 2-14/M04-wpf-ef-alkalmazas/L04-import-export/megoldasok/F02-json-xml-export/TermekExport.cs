using System.Xml.Serialization;

namespace RaktarExport;

// Lapos exportosztály (DTO): nincs benne navigációs tulajdonság.
[XmlType("Termek")]
public class TermekExport
{
    [XmlAttribute("cikkszam")]
    public string Cikkszam { get; set; } = "";
    public string Nev { get; set; } = "";
    public string Kategoria { get; set; } = "";
    public int Ar { get; set; }
    public int Darab { get; set; }
}
