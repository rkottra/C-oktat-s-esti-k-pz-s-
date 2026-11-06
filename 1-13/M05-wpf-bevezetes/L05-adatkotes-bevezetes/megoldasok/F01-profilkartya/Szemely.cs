using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Profilkartya;

public class Szemely : INotifyPropertyChanged
{
    public const int AktualisEv = 2026;

    private string nev = "";
    private string foglalkozas = "";
    private string varos = "";
    private int szuletesiEv;
    private string szin = "LightSteelBlue";

    public string Nev
    {
        get => nev;
        set { nev = value; Valtozott(); Valtozott(nameof(Monogram)); }
    }

    public string Foglalkozas
    {
        get => foglalkozas;
        set { foglalkozas = value; Valtozott(); }
    }

    public string Varos
    {
        get => varos;
        set { varos = value; Valtozott(); Valtozott(nameof(Alcim)); }
    }

    public int SzuletesiEv
    {
        get => szuletesiEv;
        set { szuletesiEv = value; Valtozott(); Valtozott(nameof(Kor)); Valtozott(nameof(Alcim)); }
    }

    public string Szin
    {
        get => szin;
        set { szin = value; Valtozott(); }
    }

    // Számított tulajdonságok – a forrásaik setterében értesítünk róluk
    public int Kor => AktualisEv - SzuletesiEv;
    public string Alcim => $"{Varos} · {Kor} éves";

    public string Monogram
    {
        get
        {
            string m = "";
            foreach (string tag in Nev.Split(' ', StringSplitOptions.RemoveEmptyEntries))
                m += char.ToUpper(tag[0]);
            return m;
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void Valtozott([CallerMemberName] string? nev = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nev));
}
