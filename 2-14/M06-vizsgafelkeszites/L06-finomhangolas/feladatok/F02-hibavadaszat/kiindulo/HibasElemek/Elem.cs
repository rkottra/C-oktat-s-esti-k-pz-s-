namespace HibasElemek;

// FIGYELEM: ez a projekt szándékosan hibás – a hibakereső gyakorláshoz készült.
public class Elem
{
    public int Rendszam { get; set; }
    public string Vegyjel { get; set; } = "";
    public string Nev { get; set; } = "";
    public string Kategoria { get; set; } = "";
    public double Atomtomeg { get; set; }
    public double Elektronegativitas { get; set; }
    public string Halmazallapot { get; set; } = "";

    public Elem(string sor)
    {
        string[] a = sor.Split(';');
        Rendszam = int.Parse(a[0]);
        Vegyjel = a[1];
        Nev = a[2];
        Kategoria = a[3];
        Atomtomeg = double.Parse(a[6]);
        Elektronegativitas = double.Parse(a[7]);
        Halmazallapot = a[8];
    }

    public static List<Elem> BetoltCsv(string fajlnev)
    {
        var lista = new List<Elem>();
        foreach (string sor in File.ReadAllLines(fajlnev))
        {
            lista.Add(new Elem(sor));
        }
        return lista;
    }
}
