namespace Allatorvos;

public class Gazdi
{
    public string Nev { get; }
    public string Telepules { get; }
    public List<Allat> Allatok { get; } = new();

    public Gazdi(string nev, string telepules)
    {
        Nev = nev;
        Telepules = telepules;
    }

    public void AllatotFelvesz(Allat allat) => Allatok.Add(allat);
}
