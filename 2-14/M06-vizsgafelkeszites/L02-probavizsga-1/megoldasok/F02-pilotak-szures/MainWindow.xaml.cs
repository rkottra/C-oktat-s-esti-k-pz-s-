using System.Windows;
using System.Windows.Controls;
using PilotakSzures.Models;

namespace PilotakSzures;

public partial class MainWindow : Window
{
    private readonly PilotakViewModel vm = new PilotakViewModel();
    private bool betoltve;   // az InitializeComponent alatt még ne szűrjünk

    public MainWindow()
    {
        InitializeComponent();
        DataContext = vm;
        cbSzuroNemzet.SelectedIndex = 0;   // "(összes)"
        betoltve = true;
    }

    private void SzuresFrissitese()
    {
        if (!betoltve) return;
        string nemzet = cbSzuroNemzet.SelectedItem as string ?? PilotakViewModel.Osszes;
        vm.Szures(nemzet, txtSzuroNev.Text.Trim());
    }

    // Ugyanaz a kezelő fut a ComboBox és a TextBox változásakor is
    private void Szuro_Valtozott(object sender, RoutedEventArgs e) => SzuresFrissitese();

    private void SzuroTorles_Click(object sender, RoutedEventArgs e)
    {
        betoltve = false;
        cbSzuroNemzet.SelectedIndex = 0;
        txtSzuroNev.Clear();
        betoltve = true;
        SzuresFrissitese();
    }

    private static void Hiba(string uzenet)
        => MessageBox.Show(uzenet, "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);

    private void Mentes_Click(object sender, RoutedEventArgs e)
    {
        string nev = txtNev.Text.Trim();
        string nemzetiseg = txtNemzetiseg.Text.Trim();

        if (nev == "") { Hiba("A név megadása kötelező!"); return; }
        if (cbNem.SelectedItem is not ComboBoxItem nemElem) { Hiba("Válaszd ki a pilóta nemét!"); return; }
        if (dpSzuletes.SelectedDate is not DateTime szuletes) { Hiba("A születési dátum megadása kötelező!"); return; }
        if (szuletes.Date >= DateTime.Today) { Hiba("A születési dátum nem lehet mai vagy jövőbeli dátum!"); return; }
        if (nemzetiseg == "") { Hiba("A nemzetiség megadása kötelező!"); return; }

        vm.Hozzaad(new Pilota
        {
            Nev = nev,
            Nem = (string)nemElem.Tag,
            SzuletesiDatum = szuletes.Date,
            Nemzetiseg = nemzetiseg
        });

        // A nemzetiséglista újratöltése törli a kijelölést, ezért a szűrőt is alaphelyzetbe tesszük
        SzuroTorles_Click(sender, e);

        txtNev.Clear();
        cbNem.SelectedIndex = -1;
        dpSzuletes.SelectedDate = null;
        txtNemzetiseg.Clear();
    }

    private void Torles_Click(object sender, RoutedEventArgs e)
    {
        if (dgPilotak.SelectedItem is not Pilota kijelolt)
        {
            MessageBox.Show("Előbb jelölj ki egy pilótát a táblázatban!", "Törlés",
                MessageBoxButton.OK, MessageBoxImage.Information);
            return;
        }
        if (MessageBox.Show($"Biztosan törölni szeretnéd {kijelolt.Nev} adatait?", "Megerősítés",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
        {
            vm.Torol(kijelolt);
            SzuresFrissitese();   // a szűrés megmarad
        }
    }
}
