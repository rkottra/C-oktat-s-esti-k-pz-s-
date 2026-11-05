using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TelepulesekKonzol;

// settlements.csv: id;name;countyname;region;population;areasize
public class Telepules
{
    [JsonPropertyName("id")] public int Id { get; set; }
    [JsonPropertyName("name")] public string Nev { get; set; } = "";
    [JsonPropertyName("countyname")] public string Megye { get; set; } = "";
    [JsonPropertyName("region")] public string Jaras { get; set; } = "";
    [JsonPropertyName("population")] public int Nepesseg { get; set; }
    [JsonPropertyName("areasize")] public double Terulet { get; set; }   // km²

    // Népsűrűség fő/km²-ben – számított tulajdonság, nem a fájlból jön
    public double Nepsuruseg => Nepesseg / Terulet;

    // A vizsgán elég az egyik betöltőt elkészíteni; itt mindkettő szerepel.
    public static List<Telepules> BetoltCsv(string fajlnev)
    {
        var lista = new List<Telepules>();
        foreach (string sor in File.ReadAllLines(fajlnev).Skip(1))
        {
            if (sor.Trim() == "") continue;
            string[] a = sor.Split(';');
            lista.Add(new Telepules
            {
                Id = int.Parse(a[0]),
                Nev = a[1],
                Megye = a[2],
                Jaras = a[3],
                Nepesseg = int.Parse(a[4]),
                Terulet = double.Parse(a[5], CultureInfo.InvariantCulture)   // tizedes pont!
            });
        }
        return lista;
    }

    public static List<Telepules> BetoltJson(string fajlnev)
        => JsonSerializer.Deserialize<List<Telepules>>(File.ReadAllText(fajlnev)) ?? new List<Telepules>();
}
