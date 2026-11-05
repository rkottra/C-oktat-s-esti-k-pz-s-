namespace Bank;

// Egyszerű bankszámla – javított változat.
public class Bankszamla
{
    public string Tulajdonos { get; }
    public int Egyenleg { get; private set; }

    public Bankszamla(string tulajdonos, int nyitoEgyenleg = 0)
    {
        // 3. hiba javítása: negatív nyitóegyenleg nem megengedett.
        if (nyitoEgyenleg < 0)
            throw new ArgumentOutOfRangeException(nameof(nyitoEgyenleg), "A nyitóegyenleg nem lehet negatív.");

        Tulajdonos = tulajdonos;
        Egyenleg = nyitoEgyenleg;
    }

    public void Befizet(int osszeg)
    {
        if (osszeg <= 0)
            throw new ArgumentOutOfRangeException(nameof(osszeg), "Csak pozitív összeget lehet befizetni.");
        Egyenleg += osszeg;
    }

    public void Kivesz(int osszeg)
    {
        if (osszeg <= 0)
            throw new ArgumentOutOfRangeException(nameof(osszeg), "Csak pozitív összeget lehet kivenni.");
        if (osszeg > Egyenleg)                       // 1. hiba javítása: >= helyett > (a teljes egyenleg felvehető)
            throw new InvalidOperationException("Nincs elég fedezet.");
        Egyenleg -= osszeg;
    }

    public void Atutal(Bankszamla cel, int osszeg)
    {
        if (cel == null)
            throw new ArgumentNullException(nameof(cel));
        if (cel == this)
            throw new InvalidOperationException("Saját számlára nem lehet utalni.");

        // 2. hiba javítása: előbb a levonás (ez ellenőrzi a fedezetet), csak siker esetén a jóváírás.
        Kivesz(osszeg);
        cel.Befizet(osszeg);
    }
}
