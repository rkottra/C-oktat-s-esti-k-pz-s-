using System.ComponentModel;

namespace KonyvtarDataGrid;

public class Konyv : INotifyPropertyChanged
{
    private string cim = "";
    private string szerzo = "";
    private int kiadasEve;
    private bool kolcsonozve;

    public event PropertyChangedEventHandler? PropertyChanged;

    private void Valtozott(string tulajdonsagNev) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(tulajdonsagNev));

    public string Cim
    {
        get => cim;
        set { cim = value; Valtozott(nameof(Cim)); }
    }

    public string Szerzo
    {
        get => szerzo;
        set { szerzo = value; Valtozott(nameof(Szerzo)); }
    }

    public int KiadasEve
    {
        get => kiadasEve;
        set { kiadasEve = value; Valtozott(nameof(KiadasEve)); }
    }

    public bool Kolcsonozve
    {
        get => kolcsonozve;
        set
        {
            kolcsonozve = value;
            Valtozott(nameof(Kolcsonozve));
            Valtozott(nameof(Allapot));   // a számított tulajdonság is változik!
        }
    }

    // Számított (csak olvasható) tulajdonság: nincs saját mezője, a Kolcsonozve-ből jön.
    public string Allapot => Kolcsonozve ? "kölcsönözve" : "a polcon";
}
