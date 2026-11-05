using System.Globalization;

namespace IdojarasMeresek;

// Formátum: datum;varos;homerseklet;csapadek_mm
public class Meres
{
    public DateTime Datum { get; set; }
    public string Varos { get; set; } = "";
    public double Homerseklet { get; set; }
    public double Csapadek { get; set; }

    public bool Esos => Csapadek > 0;

    public override string ToString() => $"{Datum:yyyy.MM.dd.} {Varos}: {Homerseklet} °C, {Csapadek} mm";

    // Kivétel helyett TryParse: hibás sornál null-t ad vissza.
    // NumberStyles.Float: csak tizedespontot fogad el, a "12,4" így hibás lesz
    // (az egyszerű double.Parse(..., InvariantCulture) ezres elválasztónak venné a vesszőt → 124!).
    public static Meres? Feldolgoz(string sor)
    {
        string[] a = sor.Split(';');
        if (a.Length != 4) return null;

        if (!DateTime.TryParseExact(a[0], "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime datum))
            return null;
        if (a[1].Trim() == "") return null;
        if (!double.TryParse(a[2], NumberStyles.Float, CultureInfo.InvariantCulture, out double homerseklet))
            return null;
        if (!double.TryParse(a[3], NumberStyles.Float, CultureInfo.InvariantCulture, out double csapadek) || csapadek < 0)
            return null;

        return new Meres { Datum = datum, Varos = a[1].Trim(), Homerseklet = homerseklet, Csapadek = csapadek };
    }

    // A hibás sorokat (sorszámmal) a hibak listába gyűjti.
    public static List<Meres> FajlbolBetolt(string fajlnev, List<string> hibak)
    {
        var meresek = new List<Meres>();
        string[] sorok = File.ReadAllLines(fajlnev);
        for (int i = 1; i < sorok.Length; i++)
        {
            Meres? m = Feldolgoz(sorok[i]);
            if (m != null)
                meresek.Add(m);
            else
                hibak.Add($"{i + 1}. sor: {sorok[i]}");
        }
        return meresek;
    }
}
