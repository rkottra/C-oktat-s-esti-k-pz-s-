using System.Globalization;

namespace PilotakKonzol;

// pilots.csv: id;name;gender;birthdate;nation
public class Pilota
{
    public int Id { get; set; }
    public string Nev { get; set; } = "";
    public char Nem { get; set; }
    public DateTime? SzuletesiDatum { get; set; }   // ismeretlen is lehet ("null")
    public string Nemzetiseg { get; set; } = "";

    public Pilota(string sor)
    {
        string[] adatok = sor.Split(';');
        Id = int.Parse(adatok[0]);
        Nev = adatok[1];
        Nem = adatok[2][0];
        // A hiányzó dátumot a fájl "null" szöveggel jelöli
        SzuletesiDatum = adatok[3] == "" || adatok[3] == "null"
            ? null
            : DateTime.Parse(adatok[3], CultureInfo.InvariantCulture);
        Nemzetiseg = adatok[4];
    }

    public static List<Pilota> BetoltCsv(string fajlnev)
    {
        return File.ReadAllLines(fajlnev)
            .Skip(1)
            .Where(sor => sor.Trim() != "")
            .Select(sor => new Pilota(sor))
            .ToList();
    }

    // A dátum egységes, vizsgán megszokott formában (pl. 1911.06.24)
    public string DatumSzoveg => SzuletesiDatum?.ToString("yyyy.MM.dd") ?? "ismeretlen";

    // Igaz, ha a név valamelyik szava a megadott részlettel kezdődik (kis/nagybetű nem számít)
    public bool NevReszletEgyezik(string reszlet)
    {
        return Nev.Split(' ', '-')
            .Any(szo => szo.StartsWith(reszlet, StringComparison.OrdinalIgnoreCase));
    }
}
