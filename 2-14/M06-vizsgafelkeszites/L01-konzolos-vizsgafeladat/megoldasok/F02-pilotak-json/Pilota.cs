using System.Text.Json;
using System.Text.Json.Serialization;

namespace PilotakJson;

// pilots.json: [ { "id": 1, "name": "...", "gender": "M", "birthdate": "1911-06-24", "nation": "..." }, ... ]
public class Pilota
{
    // A JSON-kulcs és a C#-tulajdonság neve eltér, ezért megadjuk a megfeleltetést
    [JsonPropertyName("id")] public int Id { get; set; }
    [JsonPropertyName("name")] public string Nev { get; set; } = "";
    [JsonPropertyName("gender")] public string Nem { get; set; } = "";
    [JsonPropertyName("birthdate")] public DateTime? SzuletesiDatum { get; set; }   // null is lehet
    [JsonPropertyName("nation")] public string Nemzetiseg { get; set; } = "";

    public static List<Pilota> BetoltJson(string fajlnev)
    {
        string szoveg = File.ReadAllText(fajlnev);
        return JsonSerializer.Deserialize<List<Pilota>>(szoveg) ?? new List<Pilota>();
    }
}
