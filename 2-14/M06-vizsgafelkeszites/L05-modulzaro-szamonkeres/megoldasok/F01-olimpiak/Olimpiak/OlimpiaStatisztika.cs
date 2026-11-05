namespace Olimpiak;

// Tiszta (mellékhatás nélküli) számoló metódusok: nem olvasnak fájlt, nem írnak képernyőre,
// ezért egységtesztekkel könnyen ellenőrizhetők.
public static class OlimpiaStatisztika
{
    public static int RendezesekSzama(List<Olimpia> olimpiak, string orszag)
        => olimpiak.Count(o => o.Orszag.Equals(orszag.Trim(), StringComparison.OrdinalIgnoreCase));

    public static List<int> RendezesiEvek(List<Olimpia> olimpiak, string orszag)
        => olimpiak
            .Where(o => o.Orszag.Equals(orszag.Trim(), StringComparison.OrdinalIgnoreCase))
            .Select(o => o.Ev)
            .OrderBy(ev => ev)
            .ToList();

    // Kontinens és rendezések száma, csökkenő sorrendben
    public static List<(string Kontinens, int Darab)> KontinensenkentiDarab(List<Olimpia> olimpiak)
        => olimpiak
            .GroupBy(o => o.Kontinens)
            .Select(g => (g.Key, g.Count()))
            .OrderByDescending(x => x.Item2)
            .ThenBy(x => x.Key)
            .ToList();

    // Város és rendezések száma; csak a legalább kétszer rendezők
    public static List<(string Varos, int Darab)> TobbszorRendezoVarosok(List<Olimpia> olimpiak)
        => olimpiak
            .GroupBy(o => o.Varos)
            .Where(g => g.Count() >= 2)
            .Select(g => (g.Key, g.Count()))
            .OrderByDescending(x => x.Item2)
            .ThenBy(x => x.Key)
            .ToList();

    public static List<Olimpia> HalasztottJatekok(List<Olimpia> olimpiak)
        => olimpiak.Where(o => o.MegrendezesEve != o.Ev).ToList();
}
