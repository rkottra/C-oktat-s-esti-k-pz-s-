namespace Ingatlaniroda;

public class Ingatlan
{
    private string cim = "";
    private int alapterulet;
    private int negyzetmeterAr;

    public string Cim
    {
        get => cim;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("A cím nem lehet üres.");
            cim = value.Trim();
        }
    }

    public int Alapterulet
    {
        get => alapterulet;
        set
        {
            if (value < 10 || value > 1000)
                throw new ArgumentException($"{Cim}: az alapterület 10 és 1000 m² között lehet.");
            alapterulet = value;
        }
    }

    public int NegyzetmeterAr
    {
        get => negyzetmeterAr;
        set
        {
            if (value <= 0)
                throw new ArgumentException($"{Cim}: a négyzetméterár csak pozitív lehet.");
            negyzetmeterAr = value;
        }
    }

    public Ingatlan(string cim, int alapterulet, int negyzetmeterAr)
    {
        Cim = cim;
        Alapterulet = alapterulet;
        NegyzetmeterAr = negyzetmeterAr;
    }

    public virtual long Iranyar() => (long)Alapterulet * NegyzetmeterAr;

    public virtual string Tipus() => "ingatlan";

    public override string ToString()
    {
        return $"{Tipus(),-12} {Cim,-32} {Alapterulet,4} m²  {Iranyar(),14:N0} Ft";
    }
}
