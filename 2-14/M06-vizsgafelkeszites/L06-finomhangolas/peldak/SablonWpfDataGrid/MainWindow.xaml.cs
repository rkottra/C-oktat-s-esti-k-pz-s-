using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace SablonWpfDataGrid;

// SABLON – főablak táblázattal, szűréssel, felvétellel (párbeszédablakkal) és törléssel.
// Adatbázisos változatban:  Osszes → db.Tabla.Include(...).ToList();  felvétel/törlés után db.SaveChanges().
public partial class MainWindow : Window
{
    private readonly List<Rekord> osszes;                                    // a teljes adathalmaz
    public ObservableCollection<Rekord> Lista { get; } = new ObservableCollection<Rekord>();  // ami látszik

    public MainWindow()
    {
        InitializeComponent();
        osszes = Rekord.BetoltCsv("minta.csv");
        DataContext = this;      // a XAML a Lista tulajdonsághoz kötődik
        Frissites();
    }

    private void Frissites()
    {
        string szuro = txtSzuro.Text.Trim();
        Lista.Clear();
        foreach (Rekord r in osszes
                     .Where(r => szuro == "" || r.Nev.Contains(szuro, StringComparison.OrdinalIgnoreCase))
                     .OrderBy(r => r.Id))
        {
            Lista.Add(r);
        }
    }

    private void Szuro_TextChanged(object sender, TextChangedEventArgs e) => Frissites();

    private void Uj_Click(object sender, RoutedEventArgs e)
    {
        int kovetkezoId = osszes.Count == 0 ? 1 : osszes.Max(r => r.Id) + 1;
        var ablak = new UjRekordAblak(kovetkezoId) { Owner = this };
        if (ablak.ShowDialog() == true && ablak.Eredmeny is not null)
        {
            osszes.Add(ablak.Eredmeny);        // EF: db.Tabla.Add(...); db.SaveChanges();
            Frissites();
        }
    }

    private void Torles_Click(object sender, RoutedEventArgs e)
    {
        if (dgLista.SelectedItem is not Rekord kijelolt)
        {
            Ellenor.Hiba("Előbb jelölj ki egy sort!");
            return;
        }
        if (Ellenor.Megerosites($"Biztosan törlöd: {kijelolt.Nev}?"))
        {
            osszes.Remove(kijelolt);           // EF: db.Tabla.Remove(kijelolt); db.SaveChanges();
            Frissites();
        }
    }
}
