namespace UmlKonyvtar;

// UML: Kolcsonzes "*" --> "1" Konyv és Kolcsonzes "*" --> "1" Olvaso (asszociáció: egy kölcsönzés egy könyvről és egy olvasóról szól)
public class Kolcsonzes
{
    public const int KolcsonzesiNapok = 21;

    public Konyv Konyv { get; }
    public Olvaso Olvaso { get; }
    public DateOnly Kezdet { get; }
    public bool Visszahozva { get; set; }

    public Kolcsonzes(Konyv konyv, Olvaso olvaso, DateOnly kezdet)
    {
        Konyv = konyv;
        Olvaso = olvaso;
        Kezdet = kezdet;
    }

    public DateOnly Hatarido() => Kezdet.AddDays(KolcsonzesiNapok);

    public bool Kesik(DateOnly ma) => !Visszahozva && ma > Hatarido();
}
