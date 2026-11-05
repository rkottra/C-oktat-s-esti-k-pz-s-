using RetegzettEpuletek.Kozos;
using RetegzettEpuletek.Modell;

namespace RetegzettEpuletek.Adatretag;

// Adatréteg: CSV-fájlból olvas és CSV-fájlba ír. Csak ez az osztály tudja, hogy fájl van.
public class CsvEpuletRepository : IEpuletRepository
{
    private readonly string fajlnev;
    private List<Epulet>? betoltott;   // az első Osszes() hívásig null

    public CsvEpuletRepository(string fajlnev)
    {
        this.fajlnev = fajlnev;
    }

    public List<Epulet> Osszes()
    {
        betoltott ??= Beolvas();          // a fájlt csak egyszer olvassuk be
        return new List<Epulet>(betoltott);
    }

    public void Hozzaad(Epulet e)
    {
        betoltott ??= Beolvas();
        betoltott.Add(e);
        File.AppendAllText(fajlnev, $"{e.Nev};{e.Varos};{e.Orszag};{e.Epult};{e.Magassag}{Environment.NewLine}");
    }

    private List<Epulet> Beolvas()
    {
        var epuletek = new List<Epulet>();
        foreach (string sor in File.ReadLines(fajlnev).Skip(1))   // fejléc kihagyása
        {
            Epulet? epulet = SorbolEpulet(sor);
            if (epulet == null)
                Naplo.Peldany.Ir($"Hibás sor kihagyva: {sor}");
            else
                epuletek.Add(epulet);
        }
        return epuletek;
    }

    private static Epulet? SorbolEpulet(string sor)
    {
        string[] mezok = sor.Split(';');
        if (mezok.Length != 5
            || !int.TryParse(mezok[3], out int epult)
            || !int.TryParse(mezok[4], out int magassag))
            return null;

        return new Epulet { Nev = mezok[0], Varos = mezok[1], Orszag = mezok[2], Epult = epult, Magassag = magassag };
    }
}
