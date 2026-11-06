namespace Idotartamok;

public class Idotartam
{
    private int masodpercek;    // az egyetlen tárolt adat

    public int Masodpercek
    {
        get { return masodpercek; }
        set
        {
            if (value < 0)
                throw new ArgumentException("Az időtartam nem lehet negatív.");
            masodpercek = value;
        }
    }

    // Számított tulajdonságok get-tel és set-tel: a set a Masodpercek
    // tulajdonságot írja, így az ellenőrzés itt is lefut.
    public double Percek
    {
        get { return Masodpercek / 60.0; }
        set { Masodpercek = (int)Math.Round(value * 60); }
    }

    public double Orak
    {
        get { return Masodpercek / 3600.0; }
        set { Masodpercek = (int)Math.Round(value * 3600); }
    }

    // Csak olvasható részek az óó:pp:mm alakhoz.
    public int Ora => Masodpercek / 3600;
    public int Perc => Masodpercek % 3600 / 60;
    public int Mp => Masodpercek % 60;

    public Idotartam(int masodpercek)
    {
        Masodpercek = masodpercek;
    }

    public Idotartam(int ora, int perc, int mp) : this(ora * 3600 + perc * 60 + mp)
    {
        if (perc < 0 || perc > 59 || mp < 0 || mp > 59)
            throw new ArgumentException("A perc és a másodperc 0 és 59 között lehet.");
    }

    public void Hozzaad(Idotartam masik)
    {
        Masodpercek += masik.Masodpercek;
    }

    public override string ToString()
    {
        return $"{Ora:D2}:{Perc:D2}:{Mp:D2}";
    }
}
