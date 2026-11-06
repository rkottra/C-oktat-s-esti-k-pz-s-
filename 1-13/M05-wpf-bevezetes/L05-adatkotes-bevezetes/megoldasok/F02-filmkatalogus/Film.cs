using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Filmkatalogus;

public class Film : INotifyPropertyChanged
{
    private string cim = "";
    private string rendezo = "";
    private int ev;
    private string mufaj = "";
    private int ertekeles = 3;
    private bool megnezve;

    public string Cim { get => cim; set { cim = value; Valtozott(); } }
    public string Rendezo { get => rendezo; set { rendezo = value; Valtozott(); } }
    public int Ev { get => ev; set { ev = value; Valtozott(); } }
    public string Mufaj { get => mufaj; set { mufaj = value; Valtozott(); } }
    public bool Megnezve { get => megnezve; set { megnezve = value; Valtozott(); } }

    public int Ertekeles
    {
        get => ertekeles;
        set
        {
            ertekeles = Math.Clamp(value, 1, 5);
            Valtozott();
            Valtozott(nameof(Csillagok));
        }
    }

    public string Csillagok => new string('★', Ertekeles) + new string('☆', 5 - Ertekeles);

    public event PropertyChangedEventHandler? PropertyChanged;

    private void Valtozott([CallerMemberName] string? nev = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nev));
}
