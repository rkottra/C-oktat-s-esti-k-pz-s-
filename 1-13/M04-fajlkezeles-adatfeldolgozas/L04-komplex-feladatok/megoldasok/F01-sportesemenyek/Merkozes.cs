using System.Globalization;

namespace Sportesemenyek;

// Datum;Kezdes;Sportag;Hazai;Vendeg;HazaiPont;VendegPont;Nezok;Jegyar
public class Merkozes
{
    public DateTime Datum { get; }
    public TimeSpan Kezdes { get; }
    public string Sportag { get; }
    public string Hazai { get; }
    public string Vendeg { get; }
    public int HazaiPont { get; }
    public int VendegPont { get; }
    public int Nezok { get; }
    public int Jegyar { get; }

    public Merkozes(string sor)
    {
        string[] m = sor.Split(';');
        Datum = DateTime.ParseExact(m[0], "yyyy-MM-dd", CultureInfo.InvariantCulture);
        Kezdes = TimeSpan.ParseExact(m[1], @"hh\:mm", CultureInfo.InvariantCulture);
        Sportag = m[2];
        Hazai = m[3];
        Vendeg = m[4];
        HazaiPont = int.Parse(m[5]);
        VendegPont = int.Parse(m[6]);
        Nezok = int.Parse(m[7]);
        Jegyar = int.Parse(m[8]);
    }

    public long Bevetel => (long)Nezok * Jegyar;
    public bool Dontetlen => HazaiPont == VendegPont;
    public bool Hetvege => Datum.DayOfWeek == DayOfWeek.Saturday || Datum.DayOfWeek == DayOfWeek.Sunday;
    public string Eredmeny => $"{Hazai} – {Vendeg} {HazaiPont}:{VendegPont}";

    public bool Resztvevo(string csapat) =>
        string.Equals(Hazai, csapat, StringComparison.OrdinalIgnoreCase) ||
        string.Equals(Vendeg, csapat, StringComparison.OrdinalIgnoreCase);

    // A csapat saját és az ellenfél pontjai – így a hazai/vendég esetet egy helyen kezeljük
    public (int sajat, int ellenfel) Pontok(string csapat) =>
        string.Equals(Hazai, csapat, StringComparison.OrdinalIgnoreCase)
            ? (HazaiPont, VendegPont)
            : (VendegPont, HazaiPont);
}
