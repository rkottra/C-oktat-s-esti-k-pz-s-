using System.Windows;

namespace ElsoAblak;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        // Felépíti a XAML-ben leírt felületet – előtte a vezérlők még nem léteznek
        InitializeComponent();
    }

    // Eseménykezelő: a XAML-ben a Click="UdvozlesGomb_Click" köti a gombhoz
    private void UdvozlesGomb_Click(object sender, RoutedEventArgs e)
    {
        string nev = NevTextBox.Text.Trim();
        if (nev == "")
        {
            MessageBox.Show("Előbb írd be a neved!", "Hiányzó adat",
                MessageBoxButton.OK, MessageBoxImage.Warning);
            NevTextBox.Focus();
            return;
        }
        // A vezérlőt a XAML-ben megadott Name alapján érjük el
        EredmenyLabel.Content = $"Szia, {nev}!";
    }

    private void TorlesGomb_Click(object sender, RoutedEventArgs e)
    {
        NevTextBox.Text = "";
        SzamTextBox.Text = "";
        EredmenyLabel.Content = "-";
    }

    private void DuplazasGomb_Click(object sender, RoutedEventArgs e)
    {
        // A felhasználó bármit beírhat: TryParse-szal ellenőrizzük
        if (int.TryParse(SzamTextBox.Text, out int szam))
        {
            EredmenyLabel.Content = $"{szam} kétszerese: {szam * 2}";
        }
        else
        {
            MessageBox.Show("Ez nem egész szám!", "Hibás adat",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
