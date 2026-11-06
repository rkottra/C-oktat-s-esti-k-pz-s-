namespace Osztalynaplo;

public class Jegy
{
    public string Tantargy { get; }
    public int Ertek { get; }

    public Jegy(string tantargy, int ertek)
    {
        Tantargy = tantargy;
        Ertek = ertek;
    }
}

public class Diak
{
    public string Azonosito { get; }
    public string Nev { get; }
    public string Osztaly { get; }
    public List<Jegy> Jegyek { get; } = new List<Jegy>();

    public Diak(string sor)
    {
        string[] m = sor.Split(';');
        Azonosito = m[0];
        Nev = m[1];
        Osztaly = m[2];
    }

    public double Atlag()
    {
        if (Jegyek.Count == 0) return 0;
        int osszeg = 0;
        foreach (Jegy j in Jegyek)
            osszeg += j.Ertek;
        return (double)osszeg / Jegyek.Count;
    }

    // Túlterhelés: ugyanaz a név, más paraméterlista
    public double Atlag(string tantargy)
    {
        int osszeg = 0, db = 0;
        foreach (Jegy j in Jegyek)
            if (j.Tantargy == tantargy)
            {
                osszeg += j.Ertek;
                db++;
            }
        return db == 0 ? 0 : (double)osszeg / db;
    }

    public List<string> Tantargyak()
    {
        List<string> t = new List<string>();
        foreach (Jegy j in Jegyek)
            if (!t.Contains(j.Tantargy))
                t.Add(j.Tantargy);
        return t;
    }

    public bool Bukik()
    {
        foreach (string t in Tantargyak())
            if (Atlag(t) < 2.0)
                return true;
        return false;
    }

    public override string ToString() =>
        $"{Azonosito,-5}{Nev,-20}{Osztaly,-6}{Jegyek.Count,5} jegy{Atlag(),7:F2}";
}
