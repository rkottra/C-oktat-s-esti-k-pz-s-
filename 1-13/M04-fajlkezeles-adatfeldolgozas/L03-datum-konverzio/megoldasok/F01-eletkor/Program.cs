using System.Globalization;

Console.OutputEncoding = System.Text.Encoding.UTF8;
CultureInfo hu = new CultureInfo("hu-HU");

DateTime szuletes = BekerDatum("Születési dátum (ÉÉÉÉ.HH.NN.): ", false);
DateTime ma;
while (true)
{
    ma = BekerDatum("Mai dátum (ÉÉÉÉ.HH.NN., Enter = ma): ", true);
    if (ma >= szuletes) break;
    Console.WriteLine("A mai nap nem lehet a születés előtt!");
}
Console.WriteLine();

// A hét napja
Console.WriteLine($"Születésed napja: {szuletes.ToString("dddd", hu)}");

// Életkor helyesen
int kor = ma.Year - szuletes.Year;
if (szuletes.AddYears(kor) > ma)
    kor--;
Console.WriteLine($"Életkor: {kor} év");

// Eltelt napok, hetek
int napok = (ma - szuletes).Days;
Console.WriteLine($"Eddig {napok} napot ({napok / 7} hetet) éltél.");

// Következő születésnap
DateTime kovetkezo = szuletes.AddYears(kor + 1);
if (szuletes.AddYears(kor) == ma)
    Console.WriteLine("Ma van a születésnapod – Boldog születésnapot!");
else
    Console.WriteLine($"Következő születésnap: {Datum(kovetkezo)}, {(kovetkezo - ma).Days} nap múlva");

// 10 000. nap (a születés napja az 1. nap)
DateTime tizezer = szuletes.AddDays(9999);
string mikor = tizezer < ma ? "már elmúlt" : tizezer == ma ? "ma van!" : "még hátravan";
Console.WriteLine($"A 10 000. napod: {Datum(tizezer)} – {mikor}");

// 18. születésnap
DateTime nagykoru = szuletes.AddYears(18);
Console.WriteLine($"18 éves {(nagykoru <= ma ? "lettél" : "leszel")}: {Datum(nagykoru)}");

// ---------- Metódusok ----------

string Datum(DateTime d) => $"{d:yyyy.MM.dd.} ({d.ToString("dddd", hu)})";

static DateTime BekerDatum(string kerdes, bool lehetUres)
{
    while (true)
    {
        Console.Write(kerdes);
        string s = (Console.ReadLine() ?? "").Trim();
        if (lehetUres && s == "")
            return DateTime.Today;
        if (DateTime.TryParseExact(s, "yyyy.MM.dd.", CultureInfo.InvariantCulture,
                                   DateTimeStyles.None, out DateTime d))
            return d;
        Console.WriteLine("Érvénytelen dátum!");
    }
}
