using RetegzettEpuletek.Adatretag;
using RetegzettEpuletek.Kozos;
using RetegzettEpuletek.Modell;

namespace RetegzettEpuletek.UzletiLogika;

// Üzleti logika: számol, szűr, ellenőriz. Nem ír a konzolra és nem olvas fájlt.
public class EpuletSzolgaltatas
{
    private readonly IEpuletRepository repository;

    // Az adatréteget kívülről kapja meg (konstruktoron át) – így cserélhető.
    public EpuletSzolgaltatas(IEpuletRepository repository)
    {
        this.repository = repository;
    }

    public List<Epulet> MagassagSzerint() =>
        repository.Osszes().OrderByDescending(e => e.Magassag).ToList();

    public Epulet? Legmagasabb() => MagassagSzerint().FirstOrDefault();

    public List<Epulet> MagasabbMint(int meter) =>
        repository.Osszes().Where(e => e.Magassag > meter).OrderBy(e => e.Nev).ToList();

    public Dictionary<string, int> OrszagonkentDarab() =>
        repository.Osszes()
                  .GroupBy(e => e.Orszag)
                  .OrderBy(g => g.Key)
                  .ToDictionary(g => g.Key, g => g.Count());

    public double AtlagMagassag()
    {
        var epuletek = repository.Osszes();
        return epuletek.Count == 0 ? 0 : epuletek.Average(e => e.Magassag);
    }

    // Az üzleti szabályok itt vannak, nem a felületen: bármelyik felületről jön az adat, ugyanúgy ellenőrződik.
    public void UjEpulet(Epulet epulet)
    {
        if (string.IsNullOrWhiteSpace(epulet.Nev))
            throw new ArgumentException("Az épület neve nem lehet üres.");
        if (epulet.Magassag <= 0)
            throw new ArgumentException("A magasságnak pozitívnak kell lennie.");
        if (epulet.Epult > DateTime.Now.Year)
            throw new ArgumentException("Az építés éve nem lehet a jövőben.");

        repository.Hozzaad(epulet);
        Naplo.Peldany.Ir($"Új épület felvéve: {epulet.Nev}");
    }
}
