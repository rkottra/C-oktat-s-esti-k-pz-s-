using RetegzettEpuletek.Modell;

namespace RetegzettEpuletek.Adatretag;

// Ugyanaz a felület, más megvalósítás: az adatok csak a memóriában vannak.
// Hasznos bemutatóhoz és teszteléshez (14-M05-L03): nem kell hozzá fájl.
public class MemoriaEpuletRepository : IEpuletRepository
{
    private readonly List<Epulet> epuletek = new()
    {
        new Epulet { Nev = "Halászbástya", Varos = "Budapest", Orszag = "Magyarország", Epult = 1902, Magassag = 21 },
        new Epulet { Nev = "Big Ben", Varos = "London", Orszag = "Egyesült Királyság", Epult = 1859, Magassag = 96 },
    };

    public List<Epulet> Osszes() => new(epuletek);   // másolatot adunk vissza, hogy kívülről ne írják át

    public void Hozzaad(Epulet epulet) => epuletek.Add(epulet);
}
