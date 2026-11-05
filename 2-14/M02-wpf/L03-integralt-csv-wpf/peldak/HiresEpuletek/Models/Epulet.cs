using System.ComponentModel;
using System.Globalization;

namespace HiresEpuletek.Models;

// Egy CSV-sor = egy Epulet objektum.
// INotifyPropertyChanged: a módosítás azonnal látszik a listában is.
public class Epulet : INotifyPropertyChanged
{
    private int id;
    private string nev = "";
    private string varos = "";
    private string orszag = "";
    private int epitesEve;
    private double magassag;
    private bool latogathato;

    public event PropertyChangedEventHandler? PropertyChanged;

    private void Valtozott(string tulajdonsagNev) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(tulajdonsagNev));

    public int Id { get => id; set { id = value; Valtozott(nameof(Id)); } }
    public string Nev { get => nev; set { nev = value; Valtozott(nameof(Nev)); } }
    public string Varos { get => varos; set { varos = value; Valtozott(nameof(Varos)); } }
    public string Orszag { get => orszag; set { orszag = value; Valtozott(nameof(Orszag)); } }
    public int EpitesEve { get => epitesEve; set { epitesEve = value; Valtozott(nameof(EpitesEve)); } }
    public double Magassag { get => magassag; set { magassag = value; Valtozott(nameof(Magassag)); } }
    public bool Latogathato { get => latogathato; set { latogathato = value; Valtozott(nameof(Latogathato)); } }

    // A fájlban tizedesPONT van (553.3), ezért kultúrafüggetlen (Invariant) formátummal olvasunk és írunk.
    // A magyar beállítású gépen a double.Parse("553.3") hibát dobna, vagy rossz értéket adna!
    public static Epulet CsvSorbol(string sor)
    {
        string[] r = sor.Split(';');
        return new Epulet
        {
            Id = int.Parse(r[0]),
            Nev = r[1],
            Varos = r[2],
            Orszag = r[3],
            EpitesEve = int.Parse(r[4]),          // negatív is lehet: i. e. évszám
            Magassag = double.Parse(r[5], CultureInfo.InvariantCulture),
            Latogathato = r[6].Trim().ToLower() == "true"
        };
    }

    public string CsvSorra() =>
        string.Join(";", Id, Nev, Varos, Orszag, EpitesEve,
            Magassag.ToString(CultureInfo.InvariantCulture), Latogathato ? "true" : "false");

    // Szerkesztéshez másolatot készítünk, hogy a "Mégse" gomb el tudja dobni a változtatásokat.
    public Epulet Masolat() => (Epulet)MemberwiseClone();

    public void AdatokAtvetele(Epulet masik)
    {
        Nev = masik.Nev;
        Varos = masik.Varos;
        Orszag = masik.Orszag;
        EpitesEve = masik.EpitesEve;
        Magassag = masik.Magassag;
        Latogathato = masik.Latogathato;
    }
}
