using System.Windows;
using System.Windows.Controls;

namespace TanfolyamJelentkezes;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void JelentkezesGomb_Click(object sender, RoutedEventArgs e)
    {
        string nev = NevTextBox.Text.Trim();
        if (nev == "")
        {
            MessageBox.Show("Add meg a nevet!", "Hiányzó adat", MessageBoxButton.OK, MessageBoxImage.Warning);
            NevTextBox.Focus();
            return;
        }

        // A ComboBox kiválasztott eleme egy ComboBoxItem, ennek a Content-je a szöveg
        ComboBoxItem tanfolyam = (ComboBoxItem)TanfolyamComboBox.SelectedItem;

        // RadioButton, CheckBox: az IsChecked típusa bool?, ezért == true-val vizsgáljuk
        string napszak;
        if (DelelottRadio.IsChecked == true) napszak = "délelőtt";
        else if (DelutanRadio.IsChecked == true) napszak = "délután";
        else napszak = "este";

        Jelentkezes uj = new Jelentkezes
        {
            Nev = nev,
            Tanfolyam = tanfolyam.Content.ToString()!,
            Napszak = napszak,
            Tankonyv = TankonyvCheck.IsChecked == true,
            Vizsga = VizsgaCheck.IsChecked == true
        };

        // Objektumot is betehetünk a listába – a ToString() eredménye jelenik meg
        JelentkezokLista.Items.Add(uj);
        JelentkezokLista.SelectedItem = uj;

        // Az űrlap alaphelyzetbe
        NevTextBox.Text = "";
        TankonyvCheck.IsChecked = false;
        VizsgaCheck.IsChecked = false;
        NevTextBox.Focus();
        Frissit();
    }

    private void TorlesGomb_Click(object sender, RoutedEventArgs e)
    {
        if (JelentkezokLista.SelectedItem is not Jelentkezes kijelolt) return;

        MessageBoxResult valasz = MessageBox.Show($"Biztosan törlöd: {kijelolt.Nev}?", "Törlés",
            MessageBoxButton.YesNo, MessageBoxImage.Question);
        if (valasz == MessageBoxResult.Yes)
        {
            JelentkezokLista.Items.Remove(kijelolt);
            Frissit();
        }
    }

    private void JelentkezokLista_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        Frissit();
    }

    private void Frissit()
    {
        DarabLabel.Content = $"Jelentkezők: {JelentkezokLista.Items.Count}";
        TorlesGomb.IsEnabled = JelentkezokLista.SelectedIndex != -1;

        // A SelectedItem object típusú – visszaalakítjuk Jelentkezes-re
        if (JelentkezokLista.SelectedItem is Jelentkezes j)
        {
            ReszletekTextBlock.Text =
                $"{j.Nev}\n{j.Tanfolyam}, {j.Napszak}\n" +
                $"Tankönyv: {(j.Tankonyv ? "igen" : "nem")}, vizsga: {(j.Vizsga ? "igen" : "nem")}";
        }
        else
        {
            ReszletekTextBlock.Text = "Válassz ki egy jelentkezőt!";
        }
    }
}
