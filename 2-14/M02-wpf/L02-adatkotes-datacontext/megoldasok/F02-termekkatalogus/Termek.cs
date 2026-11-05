using System.ComponentModel;

namespace Termekkatalogus;

public class Termek : INotifyPropertyChanged
{
    private string nev = "";
    private string kategoria = "";
    private int ar;
    private int keszlet;

    public event PropertyChangedEventHandler? PropertyChanged;

    private void Valtozott(string tulajdonsagNev) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(tulajdonsagNev));

    public string Nev
    {
        get => nev;
        set { nev = value; Valtozott(nameof(Nev)); }
    }

    public string Kategoria
    {
        get => kategoria;
        set { kategoria = value; Valtozott(nameof(Kategoria)); }
    }

    public int Ar
    {
        get => ar;
        set { ar = value; Valtozott(nameof(Ar)); }
    }

    public int Keszlet
    {
        get => keszlet;
        set { keszlet = value; Valtozott(nameof(Keszlet)); }
    }
}
