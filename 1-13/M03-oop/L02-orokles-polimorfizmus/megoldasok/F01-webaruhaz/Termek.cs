namespace Webaruhaz;

public class Termek
{
    private string nev = "";
    private int nettoAr;

    public string Nev
    {
        get => nev;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("A terméknév nem lehet üres.");
            nev = value.Trim();
        }
    }

    public int NettoAr
    {
        get => nettoAr;
        set
        {
            if (value <= 0)
                throw new ArgumentException($"{Nev}: a nettó ár csak pozitív lehet.");
            nettoAr = value;
        }
    }

    public Termek(string nev, int nettoAr)
    {
        Nev = nev;
        NettoAr = nettoAr;
    }

    // Az általános ÁFA-kulcs; a leszármazottak felülírhatják.
    public virtual int AfaKulcs() => 27;

    // Nem virtuális: mindenkinél ugyanígy számol, de a (felülírt) AfaKulcs()-ot hívja.
    public int BruttoAr() => (int)Math.Round(NettoAr * (100 + AfaKulcs()) / 100.0);

    public override string ToString()
    {
        return $"{Nev,-38} nettó {NettoAr,7:N0} Ft  ÁFA {AfaKulcs(),2}%  bruttó {BruttoAr(),7:N0} Ft";
    }
}
