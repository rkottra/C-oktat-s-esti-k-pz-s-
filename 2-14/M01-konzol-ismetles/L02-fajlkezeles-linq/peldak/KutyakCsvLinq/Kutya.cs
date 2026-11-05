using System.Globalization;

namespace KutyakCsvLinq;

// Egy objektum = a CSV-fájl egy sora.
public class Kutya
{
    public int Id { get; set; }
    public string Nev { get; set; } = "";
    public int Kor { get; set; }
    public double Suly { get; set; }
    public string Nem { get; set; } = "";
    public string Fajta { get; set; } = "";
    public bool Chipes { get; set; }
    public bool Oltott { get; set; }

    // Számított tulajdonság: a LINQ-lekérdezésekben ugyanúgy használható, mint a többi.
    public bool Kolyok => Kor <= 1;

    // Konstruktor egy CSV-sorból. Hibás sornál kivételt dob, ezt a beolvasó kezeli.
    // Formátum: id;nev;kor;suly_kg;nem;fajta;chipes;oltott
    public Kutya(string sor)
    {
        string[] adatok = sor.Split(';');
        if (adatok.Length != 8)
            throw new FormatException($"8 adat helyett {adatok.Length} van a sorban.");

        Id = EgeszSzam(adatok[0], "azonosító");
        Nev = adatok[1];
        Kor = EgeszSzam(adatok[2], "életkor");
        // A fájlban tizedespont van; a magyar Windows tizedesvesszőt várna,
        // ezért kultúrafüggetlen (InvariantCulture) átalakítást kérünk.
        if (!double.TryParse(adatok[3], NumberStyles.Float, CultureInfo.InvariantCulture, out double suly))
            throw new FormatException($"érvénytelen súly: '{adatok[3]}'");
        Suly = suly;
        Nem = adatok[4];
        Fajta = adatok[5];
        Chipes = adatok[6] == "igen";
        Oltott = adatok[7] == "igen";
    }

    // Segédmetódus: egész szám TryParse-szal, saját (magyar) hibaüzenettel.
    private static int EgeszSzam(string szoveg, string mezo)
    {
        if (!int.TryParse(szoveg, out int ertek))
            throw new FormatException($"érvénytelen {mezo}: '{szoveg}'");
        return ertek;
    }

    // Visszaalakítás CSV-sorrá (fájlba íráshoz).
    public string CsvSor() =>
        string.Join(";", Id, Nev, Kor, Suly.ToString(CultureInfo.InvariantCulture), Nem, Fajta,
                    Chipes ? "igen" : "nem", Oltott ? "igen" : "nem");

    public override string ToString() => $"{Nev} ({Fajta}, {Kor} éves, {Suly} kg)";

    // Statikus "gyártó" metódus: a teljes fájlt objektumlistává alakítja.
    // A hibás sorokat kihagyja, és a hibaüzenetüket a hibak listába gyűjti.
    public static List<Kutya> FajlbolBetolt(string fajlnev, List<string> hibak)
    {
        var kutyak = new List<Kutya>();
        string[] sorok = File.ReadAllLines(fajlnev);

        for (int i = 1; i < sorok.Length; i++)          // az 1. (0. indexű) sor a fejléc
        {
            if (string.IsNullOrWhiteSpace(sorok[i])) continue;
            try
            {
                kutyak.Add(new Kutya(sorok[i]));
            }
            catch (FormatException ex)
            {
                hibak.Add($"{i + 1}. sor: {ex.Message} | {sorok[i]}");
            }
        }
        return kutyak;
    }
}
