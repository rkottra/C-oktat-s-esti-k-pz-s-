namespace UmlKonyvtar;

public class Konyvtar
{
    // UML: Konyvtar "1" *-- "*" Konyv (kompozíció: a könyvtár "birtokolja" a könyveit)
    private readonly List<Konyv> konyvek = new();
    // UML: Konyvtar "1" o-- "*" Olvaso (aggregáció: az olvasó a könyvtár nélkül is létezik)
    private readonly List<Olvaso> olvasok = new();
    // UML: Konyvtar "1" *-- "*" Kolcsonzes
    private readonly List<Kolcsonzes> kolcsonzesek = new();

    public void UjKonyv(Konyv konyv) => konyvek.Add(konyv);

    public void UjOlvaso(Olvaso olvaso) => olvasok.Add(olvaso);

    public Kolcsonzes Kolcsonoz(Olvaso olvaso, Konyv konyv, DateOnly datum)
    {
        if (!Elerheto(konyv))
            throw new InvalidOperationException($"A(z) „{konyv.Cim}” jelenleg ki van kölcsönözve.");

        var kolcsonzes = new Kolcsonzes(konyv, olvaso, datum);
        kolcsonzesek.Add(kolcsonzes);
        return kolcsonzes;
    }

    public bool Elerheto(Konyv konyv) =>
        !kolcsonzesek.Any(k => k.Konyv == konyv && !k.Visszahozva);

    public List<Konyv> Keres(string kifejezes) =>
        konyvek.Where(k => k.Illeszkedik(kifejezes)).ToList();

    public List<Kolcsonzes> Kesesben(DateOnly ma) =>
        kolcsonzesek.Where(k => k.Kesik(ma)).ToList();
}
