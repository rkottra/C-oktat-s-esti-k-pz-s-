namespace FizetesStatikusAbsztrakt;

public class Keszpenz : FizetesiMod
{
    public int Penztarca { get; private set; }

    public Keszpenz(int penztarca) : base("készpénz")
    {
        Penztarca = penztarca;
    }

    // Statikus segédosztály használata
    protected override int Fizetendo(int osszeg) => Kerekites.OtForintra(osszeg);

    protected override bool Engedelyez(int osszeg, out string ok)
    {
        ok = "nincs elég pénz a tárcában";
        if (osszeg > Penztarca)
            return false;
        Penztarca -= osszeg;
        return true;
    }
}

public class Bankkartya : FizetesiMod
{
    public int Egyenleg { get; private set; }
    public int NapiLimit { get; }
    private int maiKoltes;

    public Bankkartya(int egyenleg, int napiLimit) : base("bankkártya")
    {
        Egyenleg = egyenleg;
        NapiLimit = napiLimit;
    }

    protected override int Fizetendo(int osszeg) => osszeg;   // pontos összeg

    protected override bool Engedelyez(int osszeg, out string ok)
    {
        if (osszeg > Egyenleg) { ok = "nincs fedezet"; return false; }
        if (maiKoltes + osszeg > NapiLimit) { ok = "napi limit túllépve"; return false; }
        ok = "";
        Egyenleg -= osszeg;
        maiKoltes += osszeg;
        return true;
    }
}

public class SzepKartya : FizetesiMod
{
    public int Keret { get; private set; }

    public SzepKartya(int keret) : base("SZÉP-kártya")
    {
        Keret = keret;
    }

    protected override int Fizetendo(int osszeg) => osszeg;

    protected override bool Engedelyez(int osszeg, out string ok)
    {
        ok = "a SZÉP-kártya kerete nem elég";
        if (osszeg > Keret)
            return false;
        Keret -= osszeg;
        return true;
    }
}
