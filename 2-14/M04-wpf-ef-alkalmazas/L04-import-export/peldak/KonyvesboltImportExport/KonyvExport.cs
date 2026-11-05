using System.Xml.Serialization;

namespace KonyvesboltImportExport;

// Exportáláshoz külön, "lapos" osztály (DTO – Data Transfer Object):
//  - nincs benne navigációs tulajdonság, így nem lesz körkörös hivatkozás (Konyv → Kiado → Konyvek → ...);
//  - a kiadó neve egyszerű szövegként kerül a fájlba;
//  - az XmlSerializer-nek publikus, paraméter nélküli konstruktor kell – ennek az osztálynak van.
[XmlType("Konyv")]                 // XML-ben <Konyv> elemek legyenek (ne <KonyvExport>)
public class KonyvExport
{
    public string Cim { get; set; } = "";
    public string Szerzo { get; set; } = "";

    [XmlAttribute("ev")]           // XML-ben attribútum lesz: <Konyv ev="1901">
    public int KiadasEve { get; set; }

    public int Ar { get; set; }
    public int Keszlet { get; set; }
    public string Kiado { get; set; } = "";
}
