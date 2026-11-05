using System.Collections.ObjectModel;
using System.Windows;

namespace Termekkatalogus;

public partial class MainWindow : Window
{
    public ObservableCollection<Termek> Termekek { get; } = new();

    public MainWindow()
    {
        InitializeComponent();

        Termekek.Add(new Termek { Nev = "Vezeték nélküli egér", Kategoria = "periféria", Ar = 6990, Keszlet = 25 });
        Termekek.Add(new Termek { Nev = "Mechanikus billentyűzet", Kategoria = "periféria", Ar = 24990, Keszlet = 8 });
        Termekek.Add(new Termek { Nev = "27\" monitor", Kategoria = "kijelző", Ar = 79990, Keszlet = 5 });
        Termekek.Add(new Termek { Nev = "USB-C kábel", Kategoria = "kiegészítő", Ar = 2490, Keszlet = 60 });
        Termekek.Add(new Termek { Nev = "Webkamera", Kategoria = "periféria", Ar = 14990, Keszlet = 12 });

        DataContext = this;
    }

    private Termek? Kivalasztott => TermekLista.SelectedItem as Termek;

    private void Uj_Click(object sender, RoutedEventArgs e)
    {
        var uj = new Termek { Nev = "Új termék", Kategoria = "egyéb", Ar = 0, Keszlet = 0 };
        Termekek.Add(uj);
        TermekLista.SelectedItem = uj;
    }

    private void Torles_Click(object sender, RoutedEventArgs e)
    {
        if (Kivalasztott == null)
        {
            MessageBox.Show("Válassz ki egy terméket!");
            return;
        }
        if (MessageBox.Show($"Törlöd: {Kivalasztott.Nev}?", "Megerősítés",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
        {
            Termekek.Remove(Kivalasztott);
        }
    }

    private void Akcio_Click(object sender, RoutedEventArgs e)
    {
        if (Kivalasztott == null)
        {
            MessageBox.Show("Válassz ki egy terméket!");
            return;
        }
        // Csak az objektumot módosítjuk – a lista és a szerkesztőmező a PropertyChanged miatt frissül.
        Kivalasztott.Ar = (int)Math.Round(Kivalasztott.Ar * 0.9);
    }

    private void Osszesites_Click(object sender, RoutedEventArgs e)
    {
        // Az adatok mindig a gyűjteményben vannak, a vezérlőkből nem kell semmit kiolvasni.
        int darab = Termekek.Count;
        long ertek = Termekek.Sum(t => (long)t.Ar * t.Keszlet);
        var legdragabb = Termekek.OrderByDescending(t => t.Ar).FirstOrDefault();

        MessageBox.Show(
            $"Termékek száma: {darab}\n" +
            $"Teljes készletérték: {ertek:N0} Ft\n" +
            $"Legdrágább: {legdragabb?.Nev ?? "–"}",
            "Összesítés");
    }
}
