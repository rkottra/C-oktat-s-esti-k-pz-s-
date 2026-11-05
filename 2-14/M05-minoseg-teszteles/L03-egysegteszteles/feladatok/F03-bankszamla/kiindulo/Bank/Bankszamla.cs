namespace Bank;

// Egyszerű bankszámla. A szabályokat a feladat README-je írja le.
public class Bankszamla
{
    public string Tulajdonos { get; }
    public int Egyenleg { get; private set; }

    public Bankszamla(string tulajdonos, int nyitoEgyenleg = 0)
    {
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
        if (osszeg >= Egyenleg)
            throw new InvalidOperationException("Nincs elég fedezet.");
        Egyenleg -= osszeg;
    }

    public void Atutal(Bankszamla cel, int osszeg)
    {
        if (cel == null)
            throw new ArgumentNullException(nameof(cel));
        if (cel == this)
            throw new InvalidOperationException("Saját számlára nem lehet utalni.");

        cel.Befizet(osszeg);
        Kivesz(osszeg);
    }
}
