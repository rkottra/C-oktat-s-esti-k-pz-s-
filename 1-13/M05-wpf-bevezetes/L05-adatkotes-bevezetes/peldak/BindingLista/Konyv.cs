using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BindingLista;

// Az INotifyPropertyChanged miatt a felület értesül, ha egy tulajdonság megváltozik.
public class Konyv : INotifyPropertyChanged
{
    private string cim = "";
    private string szerzo = "";
    private int ev;
    private bool kolcsonozve;

    public string Cim
    {
        get => cim;
        set { cim = value; Valtozott(); Valtozott(nameof(Leiras)); }
    }

    public string Szerzo
    {
        get => szerzo;
        set { szerzo = value; Valtozott(); Valtozott(nameof(Leiras)); }
    }

    public int Ev
    {
        get => ev;
        set { ev = value; Valtozott(); Valtozott(nameof(Leiras)); }
    }

    public bool Kolcsonozve
    {
        get => kolcsonozve;
        set { kolcsonozve = value; Valtozott(); }
    }

    // Számított tulajdonság: a forrásai változásakor értesíteni kell róla is
    public string Leiras => $"{Szerzo}, {Ev}";

    public event PropertyChangedEventHandler? PropertyChanged;

    private void Valtozott([CallerMemberName] string? nev = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nev));
}
