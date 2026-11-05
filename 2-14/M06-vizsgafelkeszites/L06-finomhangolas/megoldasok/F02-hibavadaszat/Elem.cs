using System.Globalization;

namespace HibakJavitva;

public class Elem
{
    public int Rendszam { get; set; }
    public string Vegyjel { get; set; } = "";
    public string Nev { get; set; } = "";
    public string Kategoria { get; set; } = "";
    public double Atomtomeg { get; set; }
    public double? Elektronegativitas { get; set; }   // [4] hiányozhat → nullable
    public string Halmazallapot { get; set; } = "";

    public Elem(string sor)
    {
        string[] a = sor.Split(';');
        Rendszam = int.Parse(a[0]);
        Vegyjel = a[1];
        Nev = a[2];
        Kategoria = a[3];
        Atomtomeg = double.Parse(a[6], CultureInfo.InvariantCulture);                 // [3] tizedes pont
        Elektronegativitas = a[7] == "" ? null : double.Parse(a[7], CultureInfo.InvariantCulture);   // [4]
        Halmazallapot = a[8];
    }

    public static List<Elem> BetoltCsv(string fajlnev)
        => File.ReadAllLines(fajlnev)
            .Skip(1)                          // [2] fejléc kihagyása
            .Where(sor => sor.Trim() != "")
            .Select(sor => new Elem(sor))
            .ToList();
}
