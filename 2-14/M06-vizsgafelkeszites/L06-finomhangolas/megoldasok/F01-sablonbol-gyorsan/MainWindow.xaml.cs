using System.Collections.ObjectModel;
using System.IO;
using System.Windows;

namespace NemzetiParkokCsvWpf;

public partial class MainWindow : Window
{
    private const string Mind = "(mind)";

    private readonly List<NemzetiPark> osszes;
    public ObservableCollection<NemzetiPark> Lista { get; } = new ObservableCollection<NemzetiPark>();

    public MainWindow()
    {
        InitializeComponent();
        osszes = NemzetiPark.BetoltCsv("nemzeti_parkok.csv");

        // Évtizedek a tényleges adatokból: "1970–1979", …
        var evtizedek = osszes
            .Select(p => p.AlapitasEve / 10 * 10)
            .Distinct()
            .OrderBy(e => e)
            .Select(e => $"{e}–{e + 9}");
        cbEvtized.ItemsSource = new[] { Mind }.Concat(evtizedek).ToList();

        DataContext = this;
        cbEvtized.SelectedIndex = 0;   // kiváltja a Frissites()-t
    }

    private void Frissites()
    {
        string szuro = txtSzuro.Text.Trim();
        string evtized = cbEvtized.SelectedItem as string ?? Mind;
        int kezdoEv = evtized == Mind ? 0 : int.Parse(evtized[..4]);

        Lista.Clear();
        foreach (NemzetiPark p in osszes
                     .Where(p => szuro == ""
                                 || p.Nev.Contains(szuro, StringComparison.OrdinalIgnoreCase)
                                 || p.Szekhely.Contains(szuro, StringComparison.OrdinalIgnoreCase))
                     .Where(p => kezdoEv == 0 || p.AlapitasEve / 10 * 10 == kezdoEv)
                     .OrderBy(p => p.AlapitasEve)
                     .ThenBy(p => p.Nev))
        {
            Lista.Add(p);
        }
    }

    private void Szuro_Valtozott(object sender, RoutedEventArgs e)
    {
        if (osszes is not null) Frissites();   // az InitializeComponent alatt még nincs adat
    }

    private void Mentes_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            using StreamWriter iro = new StreamWriter("szurt_parkok.csv");
            iro.WriteLine("nev;szekhely;alapitas_eve");
            foreach (NemzetiPark p in Lista)
            {
                iro.WriteLine(p.CsvSor());
            }
            MessageBox.Show($"{Lista.Count} park mentve a szurt_parkok.csv fájlba.", "Mentés",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }
        catch (IOException hiba)
        {
            MessageBox.Show("Hiba a mentéskor: " + hiba.Message, "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
