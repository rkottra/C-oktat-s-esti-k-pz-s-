using System.Globalization;

namespace ElemekKonzol;

// elemek.csv: rendszam;vegyjel;nev;kategoria;periodus;csoport;atomtomeg;elektronegativitas;halmazallapot
public class Elem
{
    public int Rendszam { get; set; }
    public string Vegyjel { get; set; } = "";
    public string Nev { get; set; } = "";
    public string Kategoria { get; set; } = "";
    public int Periodus { get; set; }
    public int Csoport { get; set; }
    public double Atomtomeg { get; set; }
    public double? Elektronegativitas { get; set; }   // a nemesgázok többségénél üres
    public string Halmazallapot { get; set; } = "";

    public Elem(string sor)
    {
        string[] a = sor.Split(';');
        Rendszam = int.Parse(a[0]);
        Vegyjel = a[1];
        Nev = a[2];
        Kategoria = a[3];
        Periodus = int.Parse(a[4]);
        Csoport = int.Parse(a[5]);
        Atomtomeg = double.Parse(a[6], CultureInfo.InvariantCulture);
        Elektronegativitas = a[7] == "" ? null : double.Parse(a[7], CultureInfo.InvariantCulture);
        Halmazallapot = a[8];
    }

    public static List<Elem> BetoltCsv(string fajlnev)
        => File.ReadAllLines(fajlnev)
            .Skip(1)
            .Where(sor => sor.Trim() != "")
            .Select(sor => new Elem(sor))
            .ToList();

    // Vegyjel formai ellenőrzése: 1–2 betű, az első nagy, a második (ha van) kicsi
    public static bool ErvenyesVegyjel(string s)
        => s.Length is 1 or 2
           && char.IsUpper(s[0])
           && (s.Length == 1 || char.IsLower(s[1]));
}
