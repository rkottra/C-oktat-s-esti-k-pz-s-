using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace Koltsegvetes;

// Az ablak maga is értesít a változásairól: így az összesítő kötéssel működik.
public partial class MainWindow : Window, INotifyPropertyChanged
{
    private const string Mind = "(mind)";

    public ObservableCollection<Tetel> Tetelek { get; } = new ObservableCollection<Tetel>
    {
        new Tetel { Datum = new DateTime(2026, 5, 1),  Megnevezes = "Fizetés",            Kategoria = "munka",       Osszeg = 385_000 },
        new Tetel { Datum = new DateTime(2026, 5, 2),  Megnevezes = "Albérlet",           Kategoria = "lakás",       Osszeg = -160_000 },
        new Tetel { Datum = new DateTime(2026, 5, 3),  Megnevezes = "Heti bevásárlás",    Kategoria = "élelmiszer",  Osszeg = -24_800 },
        new Tetel { Datum = new DateTime(2026, 5, 5),  Megnevezes = "Bérlet",             Kategoria = "közlekedés",  Osszeg = -8_950 },
        new Tetel { Datum = new DateTime(2026, 5, 8),  Megnevezes = "Rezsi",              Kategoria = "lakás",       Osszeg = -31_200 },
        new Tetel { Datum = new DateTime(2026, 5, 10), Megnevezes = "Heti bevásárlás",    Kategoria = "élelmiszer",  Osszeg = -21_350 },
        new Tetel { Datum = new DateTime(2026, 5, 12), Megnevezes = "Korrepetálás",       Kategoria = "munka",       Osszeg = 27_000 },
        new Tetel { Datum = new DateTime(2026, 5, 15), Megnevezes = "Mozi",               Kategoria = "szórakozás",  Osszeg = -5_400 },
        new Tetel { Datum = new DateTime(2026, 5, 17), Megnevezes = "Heti bevásárlás",    Kategoria = "élelmiszer",  Osszeg = -26_100 },
        new Tetel { Datum = new DateTime(2026, 5, 20), Megnevezes = "Telefon",            Kategoria = "kommunikáció", Osszeg = -6_990 },
        new Tetel { Datum = new DateTime(2026, 5, 24), Megnevezes = "Heti bevásárlás",    Kategoria = "élelmiszer",  Osszeg = -19_650 },
        new Tetel { Datum = new DateTime(2026, 5, 28), Megnevezes = "Koncertjegy",        Kategoria = "szórakozás",  Osszeg = -12_900 },
    };

    // ---- Számított összesítő tulajdonságok ----
    public int Bevetel
    {
        get
        {
            int s = 0;
            foreach (Tetel t in Tetelek) if (t.Osszeg > 0) s += t.Osszeg;
            return s;
        }
    }

    public int Kiadasok
    {
        get
        {
            int s = 0;
            foreach (Tetel t in Tetelek) if (t.Osszeg < 0) s -= t.Osszeg;
            return s;
        }
    }

    public int Egyenleg => Bevetel - Kiadasok;
    public bool Negativ => Egyenleg < 0;

    private readonly ICollectionView nezet;

    public MainWindow()
    {
        InitializeComponent();
        DataContext = this;

        foreach (Tetel t in Tetelek)
            t.PropertyChanged += Tetel_PropertyChanged;
        Tetelek.CollectionChanged += Tetelek_CollectionChanged;

        // Szűrés a NÉZETEN: a gyűjtemény (és így az összesítő) változatlanul az összes tételt tartalmazza
        nezet = CollectionViewSource.GetDefaultView(Tetelek);
        nezet.Filter = Megfelel;

        KategoriakFrissitese();
        SzuroKategoria.SelectionChanged += (s, e) => nezet.Refresh();
        SzuroSzoveg.TextChanged += (s, e) => nezet.Refresh();

        UjDatum.SelectedDate = new DateTime(2026, 5, 31);
    }

    private bool Megfelel(object o)
    {
        if (o is not Tetel t) return false;
        string? kategoria = SzuroKategoria.SelectedIndex > 0 ? (string)SzuroKategoria.SelectedItem : null;
        return (kategoria == null || t.Kategoria == kategoria)
            && t.Megnevezes.Contains(SzuroSzoveg.Text.Trim(), StringComparison.OrdinalIgnoreCase);
    }

    // A kategóriák az adatokból: a szűrő és az új tétel választója is ebből töltődik
    private void KategoriakFrissitese()
    {
        List<string> kategoriak = new List<string>();
        foreach (Tetel t in Tetelek)
            if (!kategoriak.Contains(t.Kategoria))
                kategoriak.Add(t.Kategoria);
        kategoriak.Sort();

        string? elozo = SzuroKategoria.SelectedItem as string;
        SzuroKategoria.Items.Clear();
        SzuroKategoria.Items.Add(Mind);
        UjKategoria.Items.Clear();
        foreach (string k in kategoriak)
        {
            SzuroKategoria.Items.Add(k);
            UjKategoria.Items.Add(k);
        }
        SzuroKategoria.SelectedItem = elozo != null && SzuroKategoria.Items.Contains(elozo) ? elozo : Mind;
    }

    // ---- Változásfigyelés ----
    private void Tetelek_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.NewItems != null)
            foreach (Tetel t in e.NewItems) t.PropertyChanged += Tetel_PropertyChanged;
        if (e.OldItems != null)
            foreach (Tetel t in e.OldItems) t.PropertyChanged -= Tetel_PropertyChanged;
        OsszesitoValtozott();
    }

    private void Tetel_PropertyChanged(object? sender, PropertyChangedEventArgs e) => OsszesitoValtozott();

    private void OsszesitoValtozott()
    {
        Valtozott(nameof(Bevetel));
        Valtozott(nameof(Kiadasok));
        Valtozott(nameof(Egyenleg));
        Valtozott(nameof(Negativ));
    }

    // ---- Gombok ----
    private void HozzaadasGomb_Click(object sender, RoutedEventArgs e)
    {
        string megnevezes = UjMegnevezes.Text.Trim();
        string kategoria = UjKategoria.Text.Trim().ToLower();
        bool joOsszeg = int.TryParse(UjOsszeg.Text.Replace(" ", ""), out int osszeg) && osszeg > 0;

        UjMegnevezes.Background = megnevezes == "" ? Brushes.MistyRose : Brushes.White;
        UjOsszeg.Background = joOsszeg ? Brushes.White : Brushes.MistyRose;
        if (megnevezes == "" || !joOsszeg)
            return;

        Tetel uj = new Tetel
        {
            Megnevezes = megnevezes,
            Kategoria = kategoria == "" ? "egyéb" : kategoria,
            Datum = UjDatum.SelectedDate ?? DateTime.Today,
            Osszeg = UjKiadas.IsChecked == true ? -osszeg : osszeg,
        };
        Tetelek.Add(uj);
        KategoriakFrissitese();
        TetelLista.SelectedItem = uj;

        UjMegnevezes.Text = "";
        UjOsszeg.Text = "";
        UjMegnevezes.Focus();
    }

    private void TorlesGomb_Click(object sender, RoutedEventArgs e)
    {
        if (TetelLista.SelectedItem is Tetel t)
        {
            Tetelek.Remove(t);
            KategoriakFrissitese();
        }
    }

    // ---- INotifyPropertyChanged ----
    public event PropertyChangedEventHandler? PropertyChanged;

    private void Valtozott([CallerMemberName] string? nev = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nev));
}
