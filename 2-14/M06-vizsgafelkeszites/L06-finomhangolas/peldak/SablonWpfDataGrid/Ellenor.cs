using System.Globalization;
using System.Windows;

namespace SablonWpfDataGrid;

// SABLON – újrahasznosítható ellenőrző segédmetódusok.
// Mindegyik true-t ad, ha az érték jó; különben hibaüzenetet mutat és false-t ad.
// Használat:  if (!Ellenor.KotelezoSzoveg(txtNev.Text, "név")) return;
public static class Ellenor
{
    public static void Hiba(string uzenet)
        => MessageBox.Show(uzenet, "Hibás adat", MessageBoxButton.OK, MessageBoxImage.Warning);

    public static bool KotelezoSzoveg(string szoveg, string mezoNev)
    {
        if (szoveg.Trim() != "") return true;
        Hiba($"A(z) {mezoNev} megadása kötelező!");
        return false;
    }

    public static bool EgeszIntervallumban(string szoveg, string mezoNev, int min, int max, out int ertek)
    {
        if (int.TryParse(szoveg.Trim(), out ertek) && ertek >= min && ertek <= max) return true;
        Hiba($"A(z) {mezoNev} {min} és {max} közötti egész szám legyen!");
        return false;
    }

    // Tizedesvessző és -pont egyaránt elfogadott
    public static bool PozitivValos(string szoveg, string mezoNev, out double ertek)
    {
        string s = szoveg.Trim().Replace(',', '.');
        if (double.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out ertek) && ertek > 0) return true;
        Hiba($"A(z) {mezoNev} 0-nál nagyobb szám legyen!");
        return false;
    }

    public static bool Kivalasztva(object? kivalasztott, string mezoNev)
    {
        if (kivalasztott is not null) return true;
        Hiba($"Válassz a(z) {mezoNev} listából!");
        return false;
    }

    public static bool MultbeliDatum(DateTime? datum, string mezoNev)
    {
        if (datum is DateTime d && d.Date < DateTime.Today) return true;
        Hiba($"A(z) {mezoNev} kötelező, és a mai napnál korábbi legyen!");
        return false;
    }

    public static bool Megerosites(string kerdes)
        => MessageBox.Show(kerdes, "Megerősítés", MessageBoxButton.YesNo, MessageBoxImage.Question)
           == MessageBoxResult.Yes;
}
