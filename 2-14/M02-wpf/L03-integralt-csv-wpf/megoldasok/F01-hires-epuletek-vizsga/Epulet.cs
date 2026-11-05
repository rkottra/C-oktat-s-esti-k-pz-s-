using System.Globalization;

namespace HiresEpuletekVizsga;

public class Epulet
{
    public int Id { get; set; }
    public string Nev { get; set; } = "";
    public string Varos { get; set; } = "";
    public string Orszag { get; set; } = "";
    public int EpitesEve { get; set; }
    public double Magassag { get; set; }
    public bool Latogathato { get; set; }

    // Számított tulajdonságok – a felület ugyanúgy kötni tud hozzájuk, mint a többihez.
    public string Hely => $"{Varos}, {Orszag}";
    public string EpitesEveSzoveg => EpitesEve < 0 ? $"i. e. {-EpitesEve}" : EpitesEve.ToString();

    // i. e. → i. sz. között nincs 0. év, ezért negatív évszámnál eggyel kevesebb az eltelt idő.
    public int Kor => EpitesEve < 0 ? DateTime.Now.Year - EpitesEve - 1 : DateTime.Now.Year - EpitesEve;

    public static Epulet CsvSorbol(string sor)
    {
        string[] r = sor.Split(';');
        return new Epulet
        {
            Id = int.Parse(r[0]),
            Nev = r[1],
            Varos = r[2],
            Orszag = r[3],
            EpitesEve = int.Parse(r[4]),
            Magassag = double.Parse(r[5], CultureInfo.InvariantCulture),   // tizedespont a fájlban!
            Latogathato = r[6].Trim().Equals("true", StringComparison.OrdinalIgnoreCase)
        };
    }
}
