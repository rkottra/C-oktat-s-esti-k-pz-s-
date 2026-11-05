using System.IO;

namespace NemzetiParkokCsvWpf;

// A SablonWpfDataGrid Rekord osztályából átírva
public class NemzetiPark
{
    public string Nev { get; set; } = "";
    public string Szekhely { get; set; } = "";
    public int AlapitasEve { get; set; }

    public static List<NemzetiPark> BetoltCsv(string fajlnev)
        => File.ReadAllLines(fajlnev)
            .Skip(1)
            .Where(sor => sor.Trim() != "")
            .Select(sor =>
            {
                string[] a = sor.Split(';');
                return new NemzetiPark { Nev = a[0], Szekhely = a[1], AlapitasEve = int.Parse(a[2]) };
            })
            .ToList();

    public string CsvSor() => $"{Nev};{Szekhely};{AlapitasEve}";
}
