using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Koltsegvetes;

public class Tetel : INotifyPropertyChanged
{
    private string megnevezes = "";
    private string kategoria = "";
    private DateTime datum = DateTime.Today;
    private int osszeg;

    public string Megnevezes { get => megnevezes; set { megnevezes = value; Valtozott(); } }
    public string Kategoria { get => kategoria; set { kategoria = value; Valtozott(); } }
    public DateTime Datum { get => datum; set { datum = value; Valtozott(); } }

    // Pozitív: bevétel, negatív: kiadás
    public int Osszeg
    {
        get => osszeg;
        set { osszeg = value; Valtozott(); Valtozott(nameof(Kiadas)); }
    }

    public bool Kiadas => Osszeg < 0;

    public event PropertyChangedEventHandler? PropertyChanged;

    private void Valtozott([CallerMemberName] string? nev = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nev));
}
