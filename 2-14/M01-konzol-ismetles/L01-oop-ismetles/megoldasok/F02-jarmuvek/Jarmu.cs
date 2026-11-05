namespace Jarmuvek;

public abstract class Jarmu
{
    private string rendszam = "";
    private int gyartasiEv;

    public string Rendszam
    {
        get => rendszam;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("A rendszám nem lehet üres.");
            rendszam = value.Trim().ToUpper();
        }
    }

    public int GyartasiEv
    {
        get => gyartasiEv;
        set
        {
            if (value < 1950 || value > DateTime.Today.Year)
                throw new ArgumentException($"Érvénytelen gyártási év: {value}");
            gyartasiEv = value;
        }
    }

    public int Kor => DateTime.Today.Year - GyartasiEv;

    protected Jarmu(string rendszam, int gyartasiEv)
    {
        Rendszam = rendszam;
        GyartasiEv = gyartasiEv;
    }

    // Minden járműtípus maga mondja meg, mennyi az éves díja.
    public abstract int EvesDij();

    // Alapértelmezett típusnév, a leszármazottak felülírják.
    public virtual string Tipus => "jármű";

    public override string ToString() => $"{Rendszam,-9} {Tipus,-12} {GyartasiEv} ({Kor,2} éves)  díj: {EvesDij(),7:N0} Ft";
}
