using System.ComponentModel;

namespace DataContextSzemelyek;

// Az INotifyPropertyChanged felülettel az objektum SZÓL a felületnek, ha egy tulajdonsága megváltozott.
// Enélkül a kódból végzett módosítás (pl. Nev = "...") nem jelenne meg a képernyőn.
public class Szemely : INotifyPropertyChanged
{
    private string nev = "";
    private int kor;
    private string varos = "";

    public event PropertyChangedEventHandler? PropertyChanged;

    // Segédmetódus: értesítést küld a megadott nevű tulajdonság változásáról.
    private void Valtozott(string tulajdonsagNev)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(tulajdonsagNev));
    }

    public string Nev
    {
        get => nev;
        set { nev = value; Valtozott(nameof(Nev)); }
    }

    public int Kor
    {
        get => kor;
        set { kor = value; Valtozott(nameof(Kor)); }
    }

    public string Varos
    {
        get => varos;
        set { varos = value; Valtozott(nameof(Varos)); }
    }

    public override string ToString() => $"{Nev} ({Kor}), {Varos}";
}
