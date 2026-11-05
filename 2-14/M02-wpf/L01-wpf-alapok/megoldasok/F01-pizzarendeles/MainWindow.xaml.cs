using System.Windows;
using System.Windows.Controls;

namespace Pizzarendeles;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    // Kiszámolja a végösszeget; hibás darabszám esetén -1-et ad vissza.
    private int Vegosszeg()
    {
        if (!int.TryParse(DarabMezo.Text, out int darab) || darab < 1 || darab > 10)
            return -1;

        var pizza = (ComboBoxItem)PizzaValaszto.SelectedItem;
        int egysegar = int.Parse(pizza.Tag.ToString()!);

        if (NagyRadio.IsChecked == true) egysegar += 1200;
        if (SajtCheck.IsChecked == true) egysegar += 400;
        if (KukoricaCheck.IsChecked == true) egysegar += 300;
        if (JalapenoCheck.IsChecked == true) egysegar += 300;

        int osszeg = egysegar * darab;
        if (KiszallitasCheck.IsChecked == true) osszeg += 590;   // a kiszállítás rendelésenként egyszer
        return osszeg;
    }

    private void ArGomb_Click(object sender, RoutedEventArgs e)
    {
        int osszeg = Vegosszeg();
        if (osszeg < 0)
        {
            MessageBox.Show("A darabszám 1 és 10 közötti egész szám legyen!", "Hibás adat",
                MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }
        OsszegCimke.Content = $"Fizetendő: {osszeg} Ft";
    }

    private void RendelesGomb_Click(object sender, RoutedEventArgs e)
    {
        int osszeg = Vegosszeg();
        if (osszeg < 0)
        {
            MessageBox.Show("A darabszám 1 és 10 közötti egész szám legyen!", "Hibás adat",
                MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        // A pizza neve: a ComboBoxItem szövegéből a zárójel előtti rész
        string nev = ((ComboBoxItem)PizzaValaszto.SelectedItem).Content.ToString()!.Split(" (")[0];
        string meret = NagyRadio.IsChecked == true ? "45 cm" : "32 cm";
        string sor = $"{DarabMezo.Text} db {nev}, {meret} – {osszeg} Ft";

        if (MessageBox.Show($"Leadod a rendelést?\n{sor}", "Megerősítés",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
        {
            RendelesLista.Items.Add(sor);
            OsszegCimke.Content = $"Fizetendő: {osszeg} Ft";
        }
    }

    private void TorlesGomb_Click(object sender, RoutedEventArgs e)
    {
        if (RendelesLista.SelectedItem == null)
        {
            MessageBox.Show("Nincs kijelölt rendelés.");
            return;
        }
        RendelesLista.Items.Remove(RendelesLista.SelectedItem);
    }
}
