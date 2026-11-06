namespace Edzesnaplo;

// edzesek.csv: Datum;Sportag;Perc;Kcal
public class Edzes
{
    public string Datum { get; }
    public string Sportag { get; }
    public int Perc { get; }
    public int Kcal { get; }

    public Edzes(string sor)
    {
        string[] m = sor.Split(';');
        Datum = m[0];
        Sportag = m[1];
        Perc = int.Parse(m[2]);
        Kcal = int.Parse(m[3]);
    }

    public Edzes(string datum, string sportag, int perc, int kcal)
    {
        Datum = datum;
        Sportag = sportag;
        Perc = perc;
        Kcal = kcal;
    }

    public double KcalPerPerc => (double)Kcal / Perc;

    public string CsvSor() => $"{Datum};{Sportag};{Perc};{Kcal}";

    public override string ToString() => $"{Datum}  {Sportag,-10}{Perc,5} perc{Kcal,6} kcal";
}
