using System.Windows;
using System.Windows.Controls;
using PilotakWpf.Models;

namespace PilotakWpf;

public partial class MainWindow : Window
{
    private readonly PilotakViewModel vm = new PilotakViewModel();

    public MainWindow()
    {
        InitializeComponent();
        DataContext = vm;
    }

    private static void Hiba(string uzenet)
        => MessageBox.Show(uzenet, "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);

    private void Mentes_Click(object sender, RoutedEventArgs e)
    {
        string nev = txtNev.Text.Trim();
        string nemzetiseg = cbNemzetiseg.Text.Trim();

        if (nev == "")
        {
            Hiba("A név megadása kötelező!");
            return;
        }
        if (cbNem.SelectedItem is not ComboBoxItem nemElem)
        {
            Hiba("Válaszd ki a pilóta nemét!");
            return;
        }
        if (dpSzuletes.SelectedDate is not DateTime szuletes)
        {
            Hiba("A születési dátum megadása kötelező!");
            return;
        }
        if (szuletes.Date >= DateTime.Today)
        {
            Hiba("A születési dátum nem lehet mai vagy jövőbeli dátum!");
            return;
        }
        if (nemzetiseg == "")
        {
            Hiba("A nemzetiség megadása kötelező!");
            return;
        }

        vm.Hozzaad(new Pilota
        {
            Nev = nev,
            Nem = (string)nemElem.Tag,
            SzuletesiDatum = szuletes.Date,
            Nemzetiseg = nemzetiseg
        });

        // Az új (legnagyobb azonosítójú) sor kijelölése és odagörgetés
        dgPilotak.SelectedItem = vm.Pilotak.Last();
        dgPilotak.ScrollIntoView(dgPilotak.SelectedItem);

        // Mezők alaphelyzetbe
        txtNev.Clear();
        cbNem.SelectedIndex = -1;
        dpSzuletes.SelectedDate = null;
        cbNemzetiseg.Text = "";
    }

    private void Torles_Click(object sender, RoutedEventArgs e)
    {
        if (dgPilotak.SelectedItem is not Pilota kijelolt)
        {
            MessageBox.Show("Előbb jelölj ki egy pilótát a táblázatban!", "Törlés",
                MessageBoxButton.OK, MessageBoxImage.Information);
            return;
        }

        var valasz = MessageBox.Show($"Biztosan törölni szeretnéd {kijelolt.Nev} adatait?", "Megerősítés",
            MessageBoxButton.YesNo, MessageBoxImage.Question);
        if (valasz == MessageBoxResult.Yes)
        {
            vm.Torol(kijelolt);
        }
    }
}
