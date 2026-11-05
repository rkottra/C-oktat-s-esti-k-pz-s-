using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace HiresEpuletekVizsga;

public partial class MainWindow : Window
{
    private const string Osszes = "(összes)";

    // Az összes beolvasott épület – a szűrés mindig ebből indul.
    private readonly List<Epulet> epuletek = new();

    public MainWindow()
    {
        InitializeComponent();

        // 1. feladat: beolvasás
        string fajl = Path.Combine(AppContext.BaseDirectory, "epuletek.csv");
        try
        {
            foreach (string sor in File.ReadAllLines(fajl).Skip(1))
            {
                if (!string.IsNullOrWhiteSpace(sor))
                    epuletek.Add(Epulet.CsvSorbol(sor));
            }
        }
        catch (Exception ex) when (ex is IOException or FormatException or IndexOutOfRangeException)
        {
            MessageBox.Show($"Hiba az adatfájl beolvasásakor:\n{ex.Message}", "Hiba",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }

        // 3. feladat: országlista ABC-sorrendben, elöl az "(összes)"
        OrszagValaszto.Items.Add(Osszes);
        foreach (string orszag in epuletek.Select(e => e.Orszag).Distinct().OrderBy(o => o))
            OrszagValaszto.Items.Add(orszag);
        OrszagValaszto.SelectedIndex = 0;   // ez kiváltja a SelectionChanged-et → Szures()

        EpuletTabla.SelectionChanged += (s, e) => LatogathatoFrissitese();
        Szures();
    }

    // Minden szűrő ugyanezt a kezelőt hívja. A paraméterek különböző típusúak lehetnek,
    // ezért az általános RoutedEventArgs-ot használjuk (a SelectionChanged és TextChanged is abból származik).
    private void Szuro_Valtozott(object sender, RoutedEventArgs e)
    {
        Szures();
    }

    private void Szures()
    {
        // Az InitializeComponent közben is lefuthat, amikor még nem minden vezérlő létezik.
        if (EpuletTabla == null || KeresoMezo == null || CsakLatogathato == null)
            return;

        IEnumerable<Epulet> szurt = epuletek;

        string? orszag = OrszagValaszto.SelectedItem as string;
        if (orszag != null && orszag != Osszes)
            szurt = szurt.Where(e => e.Orszag == orszag);

        string resz = KeresoMezo.Text.Trim();
        if (resz != "")
            szurt = szurt.Where(e => e.Nev.Contains(resz, StringComparison.CurrentCultureIgnoreCase));

        if (CsakLatogathato.IsChecked == true)
            szurt = szurt.Where(e => e.Latogathato);

        List<Epulet> eredmeny = szurt.OrderBy(e => e.Nev).ToList();
        EpuletTabla.ItemsSource = eredmeny;

        StatisztikaFrissitese(eredmeny);
    }

    // 4. feladat: statisztika a szűrt listáról
    private void StatisztikaFrissitese(List<Epulet> lista)
    {
        DarabSzoveg.Text = $"Találatok: {lista.Count} db";
        if (lista.Count == 0)
        {
            AtlagSzoveg.Text = "Átlagmagasság: –";
            LegmagasabbSzoveg.Text = "Legmagasabb: –";
            return;
        }
        AtlagSzoveg.Text = $"Átlagmagasság: {lista.Average(e => e.Magassag):0.0} m";
        Epulet legmagasabb = lista.MaxBy(e => e.Magassag)!;
        LegmagasabbSzoveg.Text = $"Legmagasabb: {legmagasabb.Nev} ({legmagasabb.Magassag} m)";
    }

    // 5. feladat: a látogathatóság szöveges kiírása (kódból, mert feltételtől függ a szín is)
    private void LatogathatoFrissitese()
    {
        if (EpuletTabla.SelectedItem is Epulet e)
        {
            LatogathatoSzoveg.Text = e.Latogathato ? "Látogatható" : "Nem látogatható";
            LatogathatoSzoveg.Foreground = e.Latogathato
                ? System.Windows.Media.Brushes.SeaGreen
                : System.Windows.Media.Brushes.Firebrick;
        }
        else
        {
            LatogathatoSzoveg.Text = "";
        }
    }
}
