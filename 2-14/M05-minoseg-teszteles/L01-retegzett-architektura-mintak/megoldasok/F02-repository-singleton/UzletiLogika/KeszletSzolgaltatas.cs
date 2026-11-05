using KeszletMintak.Adatretag;
using KeszletMintak.Modell;

namespace KeszletMintak.UzletiLogika;

public class KeszletSzolgaltatas
{
    private readonly List<Termek> termekek;

    public KeszletSzolgaltatas(ITermekRepository repository)
    {
        termekek = repository.Osszes();
    }

    public int Darab => termekek.Count;

    // A határt már nem konstans adja, hanem a beállítások (Singleton).
    public List<Termek> Utanrendelendok() =>
        termekek.Where(t => t.Keszlet < Beallitasok.Peldany.UtanrendelesiHatar).ToList();

    public Dictionary<string, long> KeszletertekKategoriankent() =>
        termekek.GroupBy(t => t.Kategoria)
                .OrderBy(g => g.Key)
                .ToDictionary(g => g.Key, g => g.Sum(t => t.Keszletertek));

    public Termek? Legdragabb() => termekek.MaxBy(t => t.Ar);

    public List<Termek> Kategoriaban(string kategoria) =>
        termekek.Where(t => t.Kategoria.Equals(kategoria, StringComparison.OrdinalIgnoreCase)).ToList();
}
