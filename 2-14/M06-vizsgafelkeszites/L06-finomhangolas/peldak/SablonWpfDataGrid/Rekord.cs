using System.IO;
using System.Globalization;

namespace SablonWpfDataGrid;

// SABLON – adatosztály. Adatbázisos változatban ez az entitás ([Table]/[Column] attribútumokkal),
// és a betöltés a DbContext-ből történik (lásd SablonEfKonzol).
public class Rekord
{
    public int Id { get; set; }
    public string Nev { get; set; } = "";
    public double? Ertek { get; set; }
    public DateTime? Datum { get; set; }

    public static List<Rekord> BetoltCsv(string fajlnev)
        => File.ReadAllLines(fajlnev)
            .Skip(1)
            .Where(sor => sor.Trim() != "")
            .Select(sor =>
            {
                string[] a = sor.Split(';');
                return new Rekord
                {
                    Id = int.Parse(a[0]),
                    Nev = a[1],
                    Ertek = a[2] == "" ? null : double.Parse(a[2], CultureInfo.InvariantCulture),
                    Datum = a[3] == "" ? null : DateTime.Parse(a[3], CultureInfo.InvariantCulture)
                };
            })
            .ToList();
}
