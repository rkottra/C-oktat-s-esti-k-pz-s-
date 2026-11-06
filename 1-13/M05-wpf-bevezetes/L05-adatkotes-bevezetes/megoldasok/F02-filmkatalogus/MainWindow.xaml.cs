using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;

namespace Filmkatalogus;

public partial class MainWindow : Window
{
    public ObservableCollection<Film> Filmek { get; } = new ObservableCollection<Film>
    {
        new Film { Cim = "A Pál utcai fiúk", Rendezo = "Fábri Zoltán", Ev = 1969, Mufaj = "ifjúsági dráma", Ertekeles = 5, Megnezve = true },
        new Film { Cim = "Szegénylegények", Rendezo = "Jancsó Miklós", Ev = 1966, Mufaj = "történelmi dráma", Ertekeles = 4, Megnezve = false },
        new Film { Cim = "Kincskereső kisködmön", Rendezo = "Mihályfi Imre", Ev = 1973, Mufaj = "családi film", Ertekeles = 4, Megnezve = true },
        new Film { Cim = "Indul a bakterház", Rendezo = "Sándor Pál", Ev = 1979, Mufaj = "vígjáték", Ertekeles = 5, Megnezve = true },
        new Film { Cim = "Macskafogó", Rendezo = "Ternovszky Béla", Ev = 1986, Mufaj = "animáció", Ertekeles = 5, Megnezve = true },
        new Film { Cim = "Valami Amerika", Rendezo = "Herendi Gábor", Ev = 2002, Mufaj = "vígjáték", Ertekeles = 4, Megnezve = true },
        new Film { Cim = "Kontroll", Rendezo = "Antal Nimród", Ev = 2003, Mufaj = "thriller", Ertekeles = 4, Megnezve = false },
        new Film { Cim = "Saul fia", Rendezo = "Nemes Jeles László", Ev = 2015, Mufaj = "dráma", Ertekeles = 5, Megnezve = false },
        new Film { Cim = "Testről és lélekről", Rendezo = "Enyedi Ildikó", Ev = 2017, Mufaj = "romantikus dráma", Ertekeles = 4, Megnezve = true },
    };

    public MainWindow()
    {
        InitializeComponent();
        DataContext = this;

        // Az összesítőt frissíteni kell, ha a gyűjtemény VAGY egy film változik
        foreach (Film f in Filmek)
            f.PropertyChanged += Film_PropertyChanged;
        Filmek.CollectionChanged += Filmek_CollectionChanged;

        FilmLista.SelectedIndex = 0;
        Osszesit();
    }

    private void Filmek_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        // Új elemre feliratkozunk, a törölt elemről leiratkozunk
        if (e.NewItems != null)
            foreach (Film f in e.NewItems) f.PropertyChanged += Film_PropertyChanged;
        if (e.OldItems != null)
            foreach (Film f in e.OldItems) f.PropertyChanged -= Film_PropertyChanged;
        Osszesit();
    }

    private void Film_PropertyChanged(object? sender, PropertyChangedEventArgs e) => Osszesit();

    private void Osszesit()
    {
        int megnezve = 0, osszeg = 0;
        foreach (Film f in Filmek)
        {
            if (f.Megnezve) megnezve++;
            osszeg += f.Ertekeles;
        }
        double atlag = Filmek.Count == 0 ? 0 : (double)osszeg / Filmek.Count;
        OsszesitoTextBlock.Text = $"{Filmek.Count} film, ebből {megnezve} megnézve, átlagos értékelés: {atlag:F1}";
    }

    private void UjGomb_Click(object sender, RoutedEventArgs e)
    {
        Film uj = new Film { Cim = "Új film", Rendezo = "?", Ev = DateTime.Today.Year, Mufaj = "?", Ertekeles = 3 };
        Filmek.Add(uj);
        FilmLista.SelectedItem = uj;
        FilmLista.ScrollIntoView(uj);
    }

    private void TorlesGomb_Click(object sender, RoutedEventArgs e)
    {
        if (FilmLista.SelectedItem is not Film f) return;
        if (MessageBox.Show($"Törlöd: {f.Cim}?", "Törlés", MessageBoxButton.YesNo, MessageBoxImage.Question)
            == MessageBoxResult.Yes)
            Filmek.Remove(f);
    }

    // Az ObservableCollection-nek nincs Sort-ja: rendezett másolat, majd újratöltés
    private void Rendez(Comparison<Film> osszehasonlitas)
    {
        Film? kijelolt = FilmLista.SelectedItem as Film;
        List<Film> rendezett = new List<Film>(Filmek);
        rendezett.Sort(osszehasonlitas);

        Filmek.CollectionChanged -= Filmek_CollectionChanged;   // ne iratkozzunk le/fel feleslegesen
        Filmek.Clear();
        foreach (Film f in rendezett)
            Filmek.Add(f);
        Filmek.CollectionChanged += Filmek_CollectionChanged;

        FilmLista.SelectedItem = kijelolt;
        Osszesit();
    }

    private void RendezesErtekelesGomb_Click(object sender, RoutedEventArgs e) =>
        Rendez((a, b) => a.Ertekeles != b.Ertekeles ? b.Ertekeles.CompareTo(a.Ertekeles) : string.Compare(a.Cim, b.Cim));

    private void RendezesCimGomb_Click(object sender, RoutedEventArgs e) =>
        Rendez((a, b) => string.Compare(a.Cim, b.Cim));
}
