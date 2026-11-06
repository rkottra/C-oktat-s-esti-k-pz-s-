using System.Globalization;

Console.OutputEncoding = System.Text.Encoding.UTF8;
CultureInfo hu = new CultureInfo("hu-HU");

// --- Létrehozás és összetevők ---
DateTime szuletes = new DateTime(2008, 3, 15);
DateTime ma = new DateTime(2026, 5, 15);          // rögzített „mai nap”, hogy az eredmény ellenőrizhető legyen
Console.WriteLine($"Születés: {szuletes:yyyy.MM.dd.}");
Console.WriteLine($"Év: {szuletes.Year}, hónap: {szuletes.Month}, nap: {szuletes.Day}");
Console.WriteLine($"A hét napja: {szuletes.DayOfWeek} / {szuletes.ToString("dddd", hu)}");
Console.WriteLine($"Az év {szuletes.DayOfYear}. napja");
Console.WriteLine($"Most (valódi rendszeridő): {DateTime.Now:yyyy.MM.dd. HH:mm:ss}");
Console.WriteLine();

// --- Hozzáadás és összehasonlítás ---
DateTime kolcsonzes = new DateTime(2026, 4, 20);
DateTime hatarido = kolcsonzes.AddDays(21);
Console.WriteLine($"Kölcsönzés: {kolcsonzes:yyyy.MM.dd.}, határidő: {hatarido:yyyy.MM.dd.}");
Console.WriteLine($"Lejárt? {(ma > hatarido ? "igen" : "nem")}");
Console.WriteLine($"Január 31. + 1 hónap: {new DateTime(2026, 1, 31).AddMonths(1):yyyy.MM.dd.}");
Console.WriteLine($"2028 februárjában {DateTime.DaysInMonth(2028, 2)} nap van (szökőév: {DateTime.IsLeapYear(2028)})");
Console.WriteLine();

// --- TimeSpan ---
TimeSpan eltelt = ma - szuletes;
Console.WriteLine($"Születés óta eltelt: {eltelt.Days} nap ({eltelt.TotalDays / 7:F1} hét)");

TimeSpan hosszu = new TimeSpan(1, 3, 20, 0);     // 1 nap 3 óra 20 perc
Console.WriteLine($"Hours = {hosszu.Hours}, TotalHours = {hosszu.TotalHours:F2}   ← nem ugyanaz!");

// Életkor helyesen
int kor = ma.Year - szuletes.Year;
if (szuletes.AddYears(kor) > ma)
    kor--;
Console.WriteLine($"Életkor: {kor} év");

// Aki holnap lesz 18, az ma még 17 – a napok / 365 módszer ezt elrontja (a szökőnapok miatt)
DateTime holnapSzulinap = new DateTime(2008, 5, 16);
int helyes = ma.Year - holnapSzulinap.Year;
if (holnapSzulinap.AddYears(helyes) > ma)
    helyes--;
int rossz = (int)((ma - holnapSzulinap).TotalDays / 365);
Console.WriteLine($"2008.05.16-án született: helyesen {helyes} éves, a napok/365 módszerrel {rossz} – hibás!");
Console.WriteLine();

// --- Beolvasás szövegből, formátummal ---
DateTime d1 = DateTime.ParseExact("2026-03-15", "yyyy-MM-dd", CultureInfo.InvariantCulture);
DateTime d2 = DateTime.ParseExact("2026.03.15. 14:05", "yyyy.MM.dd. HH:mm", CultureInfo.InvariantCulture);
TimeSpan ido = TimeSpan.ParseExact("08:45", @"hh\:mm", CultureInfo.InvariantCulture);
Console.WriteLine($"{d1:yyyy.MM.dd.}  |  {d2:yyyy.MM.dd. HH:mm}  |  {ido:hh\\:mm} = {ido.TotalMinutes} perc");

foreach (string probalt in new[] { "2026.02.28.", "2026.02.30.", "2026.2.3.", "tegnap" })
{
    bool ok = DateTime.TryParseExact(probalt, "yyyy.MM.dd.", CultureInfo.InvariantCulture,
                                     DateTimeStyles.None, out DateTime d);
    Console.WriteLine($"  {probalt,-12} → {(ok ? d.ToString("yyyy. MMMM d., dddd", hu) : "érvénytelen")}");
}
Console.WriteLine();

// --- A hét napjai magyarul saját tömbbel (a DayOfWeek vasárnappal, 0-val kezdődik) ---
string[] napok = { "vasárnap", "hétfő", "kedd", "szerda", "csütörtök", "péntek", "szombat" };
DateTime nap = new DateTime(2026, 5, 11);
for (int i = 0; i < 7; i++)
{
    DateTime d = nap.AddDays(i);
    bool hetvege = d.DayOfWeek == DayOfWeek.Saturday || d.DayOfWeek == DayOfWeek.Sunday;
    Console.WriteLine($"  {d:MM.dd.} {napok[(int)d.DayOfWeek],-10}{(hetvege ? "hétvége" : "")}");
}
