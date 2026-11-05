namespace MvvmEpuletek.Model;

// Model: az adat és az adathoz tartozó szabályok. Nem tud a felületről.
public class Epulet
{
    public string Nev { get; set; } = "";
    public string Varos { get; set; } = "";
    public int Epult { get; set; }
    public int Magassag { get; set; }

    // Egyszerű adatforrás a bemutatóhoz; valódi alkalmazásban itt egy repository állna (CSV, EF Core).
    public static List<Epulet> Mintaadatok() => new()
    {
        new Epulet { Nev = "Burdzs Kalifa", Varos = "Dubaj", Epult = 2010, Magassag = 828 },
        new Epulet { Nev = "Eiffel-torony", Varos = "Párizs", Epult = 1889, Magassag = 330 },
        new Epulet { Nev = "Empire State Building", Varos = "New York", Epult = 1931, Magassag = 443 },
        new Epulet { Nev = "Országház", Varos = "Budapest", Epult = 1904, Magassag = 96 },
        new Epulet { Nev = "Szent István-bazilika", Varos = "Budapest", Epult = 1905, Magassag = 96 },
        new Epulet { Nev = "Tokyo Skytree", Varos = "Tokió", Epult = 2012, Magassag = 634 },
        new Epulet { Nev = "Kölni dóm", Varos = "Köln", Epult = 1880, Magassag = 157 },
    };
}
