namespace UmlKonyvtar;

// UML: <<interface>> IKeresheto
public interface IKeresheto
{
    bool Illeszkedik(string kifejezes);
}

// UML: IKeresheto <|.. Konyv (megvalósítás: szaggatott vonal, üres háromszög)
public class Konyv : IKeresheto
{
    public string Cim { get; }
    public string Szerzo { get; }
    public int KiadasEve { get; }

    public Konyv(string cim, string szerzo, int kiadasEve)
    {
        Cim = cim;
        Szerzo = szerzo;
        KiadasEve = kiadasEve;
    }

    public bool Illeszkedik(string kifejezes) =>
        Cim.Contains(kifejezes, StringComparison.OrdinalIgnoreCase)
        || Szerzo.Contains(kifejezes, StringComparison.OrdinalIgnoreCase);

    public override string ToString() => $"{Szerzo}: {Cim} ({KiadasEve})";
}
