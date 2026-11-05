namespace VilagbajnokokKonzol;

// Egy sor a vilagbajnokok.csv-ből: ev;nev;nemzetiseg;csapat
public class Vilagbajnok
{
    public int Ev { get; set; }
    public string Nev { get; set; } = "";
    public string Nemzetiseg { get; set; } = "";
    public string Csapat { get; set; } = "";

    // Konstruktor, amely egy CSV-sorból tölti fel az objektumot
    public Vilagbajnok(string sor)
    {
        string[] adatok = sor.Split(';');
        Ev = int.Parse(adatok[0]);
        Nev = adatok[1];
        Nemzetiseg = adatok[2];
        Csapat = adatok[3];
    }

    // Statikus "gyártó" metódus: a teljes fájlból listát készít.
    // Vizsgán gyakran pontosan ilyen nevű és szignatúrájú metódust kérnek.
    public static List<Vilagbajnok> BetoltCsv(string fajlnev)
    {
        return File.ReadAllLines(fajlnev)
            .Skip(1)                          // fejléc kihagyása
            .Where(sor => sor.Trim() != "")   // üres sor (pl. a fájl végén) kihagyása
            .Select(sor => new Vilagbajnok(sor))
            .ToList();
    }

    public override string ToString() => $"{Ev}: {Nev} ({Csapat})";
}
