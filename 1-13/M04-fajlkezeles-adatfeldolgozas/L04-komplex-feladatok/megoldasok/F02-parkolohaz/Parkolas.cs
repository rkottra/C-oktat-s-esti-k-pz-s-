using System.Globalization;

namespace Parkolohaz;

// Rendszam;Be;Ki;Berlet   (Be, Ki: yyyy-MM-dd HH:mm; Berlet: igen/nem)
public class Parkolas
{
    public const int IngyenesPerc = 30;
    public const int OraDij = 400;
    public const int NapiMax = 4000;

    public string Rendszam { get; }
    public DateTime Be { get; }
    public DateTime Ki { get; }
    public bool Berlet { get; }

    public Parkolas(string sor)
    {
        string[] m = sor.Split(';');
        Rendszam = m[0];
        Be = DateTime.ParseExact(m[1], "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
        Ki = DateTime.ParseExact(m[2], "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
        Berlet = m[3] == "igen";
    }

    public TimeSpan Idotartam => Ki - Be;

    public int Dij
    {
        get
        {
            int perc = (int)Idotartam.TotalMinutes;
            if (Berlet || perc <= IngyenesPerc)
                return 0;
            int napok = perc / 1440;                     // teljes 24 órák
            int maradek = perc % 1440;
            int megkezdettOrak = (maradek + 59) / 60;    // felfelé kerekítés egész számokkal
            return napok * NapiMax + Math.Min(megkezdettOrak * OraDij, NapiMax);
        }
    }

    public bool BentVolt(DateTime idopont) => Be <= idopont && idopont < Ki;

    public static string OraPerc(TimeSpan t) => $"{(int)t.TotalHours}:{t.Minutes:00}";

    // ABC-123
    public static bool RegiFormatum(string r) =>
        r.Length == 7 &&
        char.IsLetter(r[0]) && char.IsLetter(r[1]) && char.IsLetter(r[2]) &&
        r[3] == '-' &&
        char.IsDigit(r[4]) && char.IsDigit(r[5]) && char.IsDigit(r[6]);

    // AB CD-123
    public static bool UjFormatum(string r) =>
        r.Length == 9 &&
        char.IsLetter(r[0]) && char.IsLetter(r[1]) && r[2] == ' ' &&
        char.IsLetter(r[3]) && char.IsLetter(r[4]) && r[5] == '-' &&
        char.IsDigit(r[6]) && char.IsDigit(r[7]) && char.IsDigit(r[8]);
}
