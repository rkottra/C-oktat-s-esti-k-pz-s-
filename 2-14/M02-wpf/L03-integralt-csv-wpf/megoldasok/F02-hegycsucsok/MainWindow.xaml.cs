using System.IO;
using System.Windows;

namespace Hegycsucsok;

public partial class MainWindow : Window
{
    private const string Mind = "(mind)";
    private readonly List<Hegycsucs> csucsok = new();

    public MainWindow()
    {
        InitializeComponent();

        try
        {
            string fajl = Path.Combine(AppContext.BaseDirectory, "hegycsucsok.csv");
            csucsok = File.ReadAllLines(fajl)
                          .Skip(1)
                          .Where(sor => sor.Trim() != "")
                          .Select(Hegycsucs.CsvSorbol)
                          .ToList();
        }
        catch (Exception ex) when (ex is IOException or FormatException or IndexOutOfRangeException)
        {
            MessageBox.Show($"Hiba a beolvasáskor: {ex.Message}", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        KontinensValaszto.Items.Add(Mind);
        foreach (string k in csucsok.Select(c => c.Kontinens).Distinct().OrderBy(k => k))
            KontinensValaszto.Items.Add(k);
        KontinensValaszto.SelectedIndex = 0;

        Frissites();
    }

    private void Szuro_Valtozott(object sender, RoutedEventArgs e) => Frissites();

    private void Frissites()
    {
        // Az InitializeComponent közben (a RadioButton Checked eseménye miatt) még null lehet a lista.
        if (CsucsLista == null || OsszegzesSzoveg == null)
            return;

        string? kontinens = KontinensValaszto.SelectedItem as string;
        IEnumerable<Hegycsucs> szurt = csucsok;
        if (kontinens != null && kontinens != Mind)
            szurt = szurt.Where(c => c.Kontinens == kontinens);

        szurt = NevSzerint.IsChecked == true
            ? szurt.OrderBy(c => c.Nev)
            : szurt.OrderByDescending(c => c.Magassag);

        List<Hegycsucs> lista = szurt.ToList();
        CsucsLista.ItemsSource = lista;

        OsszegzesSzoveg.Text = lista.Count == 0
            ? "Nincs találat."
            : $"{lista.Count} csúcs, ebből nyolcezres: {lista.Count(c => c.Nyolcezres)} db, " +
              $"átlagmagasság: {lista.Average(c => c.Magassag):0.0} m, " +
              $"legkorábban megmászott: {lista.MinBy(c => c.ElsoMegmaszas)!.Nev}";
    }
}
