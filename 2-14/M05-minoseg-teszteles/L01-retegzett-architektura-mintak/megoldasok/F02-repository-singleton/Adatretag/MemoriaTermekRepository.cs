using KeszletMintak.Modell;

namespace KeszletMintak.Adatretag;

// Második adatréteg-megvalósítás: fájl nélkül, fix adatokkal.
public class MemoriaTermekRepository : ITermekRepository
{
    public List<Termek> Osszes() => new()
    {
        new Termek { Nev = "Pendrive 64 GB", Kategoria = "Tároló", Ar = 3990, Keszlet = 25 },
        new Termek { Nev = "Külső SSD 1 TB", Kategoria = "Tároló", Ar = 34990, Keszlet = 2 },
        new Termek { Nev = "Vezeték nélküli egér", Kategoria = "Periféria", Ar = 6990, Keszlet = 9 },
        new Termek { Nev = "HDMI kábel 2 m", Kategoria = "Kábel", Ar = 2490, Keszlet = 1 },
    };
}
