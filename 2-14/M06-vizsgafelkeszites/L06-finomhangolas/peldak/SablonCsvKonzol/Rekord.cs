using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SablonCsvKonzol;

// SABLON – adatosztály CSV- és JSON-betöltővel.
// Vizsgán: nevezd át az osztályt és a tulajdonságokat, igazítsd a mezőkhöz, a fölösleges betöltőt töröld.
// minta.csv: id;nev;ertek;datum   (az ertek és a datum hiányozhat)
public class Rekord
{
    [JsonPropertyName("id")] public int Id { get; set; }
    [JsonPropertyName("nev")] public string Nev { get; set; } = "";
    [JsonPropertyName("ertek")] public double? Ertek { get; set; }        // hiányozhat → nullable
    [JsonPropertyName("datum")] public DateTime? Datum { get; set; }      // hiányozhat → nullable

    // A JSON-deszerializáláshoz paraméter nélküli konstruktor kell
    public Rekord() { }

    // CSV-sorból
    public Rekord(string sor)
    {
        string[] a = sor.Split(';');
        Id = int.Parse(a[0]);
        Nev = a[1];
        Ertek = UresVagyNull(a[2]) ? null : double.Parse(a[2], CultureInfo.InvariantCulture);
        Datum = UresVagyNull(a[3]) ? null : DateTime.Parse(a[3], CultureInfo.InvariantCulture);
    }

    private static bool UresVagyNull(string s) => s.Trim() == "" || s.Trim().ToLower() == "null";

    public static List<Rekord> BetoltCsv(string fajlnev)
        => File.ReadAllLines(fajlnev)
            .Skip(1)                          // fejléc
            .Where(sor => sor.Trim() != "")   // üres sorok
            .Select(sor => new Rekord(sor))
            .ToList();

    public static List<Rekord> BetoltJson(string fajlnev)
        => JsonSerializer.Deserialize<List<Rekord>>(File.ReadAllText(fajlnev)) ?? new List<Rekord>();

    // Egységes kiírási formák
    public string ErtekSzoveg => Ertek?.ToString("F2") ?? "nincs adat";
    public string DatumSzoveg => Datum?.ToString("yyyy.MM.dd") ?? "ismeretlen";

    public override string ToString() => $"{Id}\t{Nev}\t{ErtekSzoveg}\t{DatumSzoveg}";
}
