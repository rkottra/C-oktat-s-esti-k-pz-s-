namespace Allatorvos;

public class Rendelo
{
    private readonly List<Gazdi> gazdik = new();

    public IReadOnlyList<Gazdi> Gazdik => gazdik;

    public void UjGazdi(Gazdi gazdi) => gazdik.Add(gazdi);

    public List<Allat> OsszesAllat() => gazdik.SelectMany(g => g.Allatok).ToList();

    public int OltasiBevetel() => OsszesAllat().Sum(a => a.OltasiDij());

    public Allat? Legidosebb(int evszam) => OsszesAllat().MaxBy(a => a.Kor(evszam));
}
