using System.Globalization;

namespace WebshopRendelesek;

// Formátum: rendelesszam;datum;termek;kategoria;darab;egysegar
public class Rendeles
{
    public string Rendelesszam { get; }
    public DateTime Datum { get; }
    public string Termek { get; }
    public string Kategoria { get; }
    public int Darab { get; }
    public int Egysegar { get; }

    public int Osszeg => Darab * Egysegar;

    public Rendeles(string sor)
    {
        string[] a = sor.Split(';');
        if (a.Length != 6)
            throw new FormatException($"6 adat helyett {a.Length}");
        Rendelesszam = a[0];
        Datum = DateTime.ParseExact(a[1], "yyyy-MM-dd", CultureInfo.InvariantCulture);
        Termek = a[2];
        Kategoria = a[3];
        Darab = int.Parse(a[4]);
        Egysegar = int.Parse(a[5]);
    }

    public override string ToString() =>
        $"{Rendelesszam} {Datum:yyyy.MM.dd.} {Termek} – {Darab} x {Egysegar:N0} Ft = {Osszeg:N0} Ft";

    public static List<Rendeles> FajlbolBetolt(string fajlnev, out int hibasSorok)
    {
        var lista = new List<Rendeles>();
        hibasSorok = 0;
        foreach (string sor in File.ReadLines(fajlnev).Skip(1))
        {
            try
            {
                lista.Add(new Rendeles(sor));
            }
            catch (FormatException)
            {
                hibasSorok++;
            }
        }
        return lista;
    }
}
