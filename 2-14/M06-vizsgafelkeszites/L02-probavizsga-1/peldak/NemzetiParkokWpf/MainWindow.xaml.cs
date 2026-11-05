using System.Collections.ObjectModel;
using System.Windows;
using NemzetiParkokWpf.Models;

namespace NemzetiParkokWpf;

public partial class MainWindow : Window
{
    // Egy DbContext az ablak teljes élettartamára – kis asztali alkalmazásban ez megfelelő
    private readonly ParkContext db = new ParkContext();

    // Az ObservableCollection változásait (Add/Remove) a DataGrid magától követi
    private readonly ObservableCollection<NemzetiPark> parkok = new ObservableCollection<NemzetiPark>();

    public MainWindow()
    {
        InitializeComponent();
        dgParkok.ItemsSource = parkok;
        Betoltes();
    }

    private void Betoltes()
    {
        parkok.Clear();
        foreach (NemzetiPark p in db.Parkok.OrderBy(p => p.AlapitasEve).ThenBy(p => p.Nev))
        {
            parkok.Add(p);
        }
        txtDarab.Text = $"Parkok száma: {parkok.Count}";
    }

    private void Mentes_Click(object sender, RoutedEventArgs e)
    {
        // Ellenőrzés: minden hibánál üzenet, majd return – így nem kerül rossz adat az adatbázisba
        if (string.IsNullOrWhiteSpace(txtNev.Text))
        {
            MessageBox.Show("A név megadása kötelező!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }
        if (string.IsNullOrWhiteSpace(txtSzekhely.Text))
        {
            MessageBox.Show("A székhely megadása kötelező!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }
        if (!int.TryParse(txtEv.Text, out int ev) || ev < 1900 || ev > DateTime.Now.Year)
        {
            MessageBox.Show($"Az alapítás éve 1900 és {DateTime.Now.Year} közötti egész szám legyen!", "Hiba",
                MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        var uj = new NemzetiPark { Nev = txtNev.Text.Trim(), Szekhely = txtSzekhely.Text.Trim(), AlapitasEve = ev };
        db.Parkok.Add(uj);
        db.SaveChanges();          // itt kap az új rekord Id-t (AUTO_INCREMENT)
        Betoltes();

        txtNev.Clear();
        txtSzekhely.Clear();
        txtEv.Clear();
    }

    private void Torles_Click(object sender, RoutedEventArgs e)
    {
        if (dgParkok.SelectedItem is not NemzetiPark kijelolt)
        {
            MessageBox.Show("Előbb jelölj ki egy sort!", "Törlés", MessageBoxButton.OK, MessageBoxImage.Information);
            return;
        }
        var valasz = MessageBox.Show($"Biztosan törlöd: {kijelolt.Nev}?", "Megerősítés",
            MessageBoxButton.YesNo, MessageBoxImage.Question);
        if (valasz == MessageBoxResult.Yes)
        {
            db.Parkok.Remove(kijelolt);
            db.SaveChanges();
            Betoltes();
        }
    }
}
