using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MvvmEpuletek.Model;

namespace MvvmEpuletek.ViewModel;

// ViewModel: a felület állapota (mit lát és mit gépelt be a felhasználó).
// Nincs benne egyetlen vezérlő (TextBox, DataGrid) sem – ezért a felület nélkül is kipróbálható, tesztelhető.
public class EpuletekViewModel : INotifyPropertyChanged
{
    private readonly List<Epulet> osszes = Epulet.Mintaadatok();
    private string szuro = "";

    // A DataGrid ehhez kötődik; az ObservableCollection maga szól a felületnek, ha változik.
    public ObservableCollection<Epulet> Lathato { get; } = new();

    // A TextBox ehhez kötődik. Ha a felhasználó gépel, a setter fut le.
    public string Szuro
    {
        get => szuro;
        set
        {
            szuro = value;
            OnPropertyChanged();
            Szur();
        }
    }

    public string Osszegzes => $"{Lathato.Count} épület, átlag {(Lathato.Count == 0 ? 0 : Lathato.Average(e => e.Magassag)):F0} m";

    public EpuletekViewModel()
    {
        Szur();
    }

    private void Szur()
    {
        Lathato.Clear();
        foreach (var e in osszes.Where(e => e.Nev.Contains(szuro, StringComparison.OrdinalIgnoreCase)
                                         || e.Varos.Contains(szuro, StringComparison.OrdinalIgnoreCase))
                                .OrderByDescending(e => e.Magassag))
            Lathato.Add(e);
        OnPropertyChanged(nameof(Osszegzes));
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? nev = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nev));
}
