namespace Edzoterem;

public class Tag
{
    public int TagSzam { get; }
    public string Nev { get; }
    public Berlet? Berlet { get; set; }   // 0..1 – lehet, hogy nincs bérlete

    public Tag(int tagSzam, string nev)
    {
        TagSzam = tagSzam;
        Nev = nev;
    }
}

public class Belepes
{
    public int TagSzam { get; }
    public DateOnly Nap { get; }

    public Belepes(int tagSzam, DateOnly nap)
    {
        TagSzam = tagSzam;
        Nap = nap;
    }
}
