using System.Globalization;

namespace Munkaido;

// munkaido.csv: Dolgozo;Datum;Be;Ki   (Datum: yyyy-MM-dd, Be/Ki: HH:mm)
public class Jelenlet
{
    public static readonly TimeSpan KesesHatar = new TimeSpan(9, 0, 0);
    public static readonly TimeSpan NapiMunkaido = new TimeSpan(8, 0, 0);
    public static readonly TimeSpan EbedHatar = new TimeSpan(6, 0, 0);
    public static readonly TimeSpan Ebedszunet = new TimeSpan(0, 30, 0);

    public string Dolgozo { get; }
    public DateTime Datum { get; }
    public TimeSpan Be { get; }
    public TimeSpan Ki { get; }

    public Jelenlet(string sor)
    {
        string[] m = sor.Split(';');
        Dolgozo = m[0];
        Datum = DateTime.ParseExact(m[1], "yyyy-MM-dd", CultureInfo.InvariantCulture);
        Be = TimeSpan.ParseExact(m[2], @"hh\:mm", CultureInfo.InvariantCulture);
        Ki = TimeSpan.ParseExact(m[3], @"hh\:mm", CultureInfo.InvariantCulture);
    }

    public TimeSpan BentToltott => Ki - Be;
    public TimeSpan Ledolgozott => BentToltott > EbedHatar ? BentToltott - Ebedszunet : BentToltott;
    public TimeSpan Tulora => Ledolgozott > NapiMunkaido ? Ledolgozott - NapiMunkaido : TimeSpan.Zero;
    public bool Kesett => Be > KesesHatar;
    public bool Hetvege => Datum.DayOfWeek == DayOfWeek.Saturday || Datum.DayOfWeek == DayOfWeek.Sunday;

    // 24 óránál hosszabb időtartamhoz is jó formátum: 163:40
    public static string OraPerc(TimeSpan t) => $"{(int)t.TotalHours}:{t.Minutes:00}";
}
