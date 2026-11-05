using System.Windows;
using System.Windows.Controls;

namespace Szamologep;

public partial class MainWindow : Window
{
    // Az ablak "állapota": ezekben a mezőkben jegyezzük meg, hol tart a számolás.
    private double elsoSzam = 0;
    private string muvelet = "";        // "", "+", "-", "*", "/"
    private bool ujSzamKezdodik = true; // a következő számjegy felülírja a kijelzőt?

    public MainWindow()
    {
        InitializeComponent();
    }

    private void Szamjegy_Click(object sender, RoutedEventArgs e)
    {
        // A sender az a gomb, amelyikre kattintottak.
        var gomb = (Button)sender;
        string szamjegy = gomb.Tag.ToString()!;

        if (ujSzamKezdodik || Kijelzo.Text == "0")
            Kijelzo.Text = szamjegy;
        else
            Kijelzo.Text += szamjegy;

        ujSzamKezdodik = false;
    }

    private void Muvelet_Click(object sender, RoutedEventArgs e)
    {
        // Ha már volt függő művelet (pl. 2 + 3 + ...), előbb azt végezzük el.
        if (muvelet != "" && !ujSzamKezdodik)
            Szamol();

        elsoSzam = double.Parse(Kijelzo.Text);
        muvelet = ((Button)sender).Content.ToString()!;
        ujSzamKezdodik = true;
    }

    private void Egyenlo_Click(object sender, RoutedEventArgs e)
    {
        if (muvelet == "")
            return;
        Szamol();
        muvelet = "";
        ujSzamKezdodik = true;
    }

    private void Szamol()
    {
        double masodikSzam = double.Parse(Kijelzo.Text);
        if (muvelet == "/" && masodikSzam == 0)
        {
            MessageBox.Show("Nullával nem lehet osztani!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
            Kijelzo.Text = "0";
            return;
        }

        double eredmeny = muvelet switch
        {
            "+" => elsoSzam + masodikSzam,
            "-" => elsoSzam - masodikSzam,
            "*" => elsoSzam * masodikSzam,
            "/" => elsoSzam / masodikSzam,
            _ => masodikSzam
        };
        Kijelzo.Text = eredmeny.ToString();
    }

    private void Torles_Click(object sender, RoutedEventArgs e)
    {
        Kijelzo.Text = "0";
        elsoSzam = 0;
        muvelet = "";
        ujSzamKezdodik = true;
    }
}
