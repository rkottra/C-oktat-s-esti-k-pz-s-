using System.Globalization;

// Egy CSV-sor = egy Orszag objektum. A fájl oszlopai:
// Nev;Fovaros;Terulet;Nepesseg;Gdp   (terület km²-ben, GDP: egy főre jutó, USD)
class Orszag
{
    public string Nev { get; set; }
    public string Fovaros { get; set; }
    public double Terulet { get; set; }
    public int Nepesseg { get; set; }
    public int Gdp { get; set; }

    // A konstruktor egy teljes CSV-sort kap, és szétszedi.
    public Orszag(string sor)
    {
        string[] mezok = sor.Split(';');
        Nev = mezok[0];
        Fovaros = mezok[1];
        Terulet = double.Parse(mezok[2], CultureInfo.InvariantCulture);  // tizedespont!
        Nepesseg = int.Parse(mezok[3]);
        Gdp = int.Parse(mezok[4]);
    }

    // Számított tulajdonság: nem a fájlból jön, hanem a többi adatból.
    public double Nepsuruseg => Nepesseg / Terulet;

    public override string ToString()
    {
        return $"{Nev,-22} {Fovaros,-18} {Terulet,10:N0} km²  {Nepesseg,12:N0} fő";
    }
}
