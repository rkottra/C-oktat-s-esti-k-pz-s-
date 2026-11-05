using RetegzettKeszlet.Modell;

namespace RetegzettKeszlet.Adatretag;

public class CsvTermekRepository : ITermekRepository
{
    private readonly string fajlnev;

    // Az adatréteg nem ír a konzolra: a hibás sorokat összegyűjti, a felület dönti el, mit kezd velük.
    public List<string> HibasSorok { get; } = new();

    public CsvTermekRepository(string fajlnev)
    {
        this.fajlnev = fajlnev;
    }

    public List<Termek> Osszes()
    {
        HibasSorok.Clear();
        var termekek = new List<Termek>();
        foreach (string sor in File.ReadLines(fajlnev).Skip(1))
        {
            string[] mezok = sor.Split(';');
            if (mezok.Length != 4
                || !int.TryParse(mezok[2], out int ar)
                || !int.TryParse(mezok[3], out int keszlet))
            {
                HibasSorok.Add(sor);
                continue;
            }
            termekek.Add(new Termek { Nev = mezok[0], Kategoria = mezok[1], Ar = ar, Keszlet = keszlet });
        }
        return termekek;
    }
}
