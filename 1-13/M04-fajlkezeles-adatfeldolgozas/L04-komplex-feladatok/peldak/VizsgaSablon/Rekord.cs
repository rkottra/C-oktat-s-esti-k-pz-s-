using System.Globalization;

namespace VizsgaSablon;

// 1. lépés: az osztály a fájl oszlopai szerint.
// A mintafájl (adatok.csv) oszlopai: Datum;Nev;Kategoria;Ertek
public class Rekord
{
    public DateTime Datum { get; }
    public string Nev { get; }
    public string Kategoria { get; }
    public int Ertek { get; }

    // Konstruktor egy CSV-sorból
    public Rekord(string sor)
    {
        string[] m = sor.Split(';');
        Datum = DateTime.ParseExact(m[0], "yyyy-MM-dd", CultureInfo.InvariantCulture);
        Nev = m[1];
        Kategoria = m[2];
        Ertek = int.Parse(m[3]);
    }

    // 2. lépés: számított tulajdonságok – amit TÖBB feladat is használ
    public bool Hetvege => Datum.DayOfWeek == DayOfWeek.Saturday || Datum.DayOfWeek == DayOfWeek.Sunday;

    public override string ToString() => $"{Datum:yyyy.MM.dd.} {Nev} ({Kategoria}): {Ertek}";
}
