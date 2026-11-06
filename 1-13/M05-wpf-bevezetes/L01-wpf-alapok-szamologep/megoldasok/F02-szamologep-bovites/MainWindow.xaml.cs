using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace SzamologepBovitett;

public partial class MainWindow : Window
{
    // A kijelzőn tizedesvessző van, ezért magyar formátummal alakítunk
    // (így más nyelvi beállítású gépen is ugyanúgy működik)
    private static readonly CultureInfo Magyar = new("hu-HU");

    private double elsoSzam;
    private string muvelet = "";
    private bool ujSzamKezdodik = true;

    public MainWindow()
    {
        InitializeComponent();
    }

    private double KijelzoErteke() => double.Parse(Kijelzo.Text, Magyar);

    private void Szamjegy_Click(object sender, RoutedEventArgs e)
    {
        string szamjegy = ((Button)sender).Content.ToString()!;
        if (ujSzamKezdodik || Kijelzo.Text == "0")
        {
            Kijelzo.Text = szamjegy;
            ujSzamKezdodik = false;
        }
        else if (Kijelzo.Text == "-0")
        {
            Kijelzo.Text = "-" + szamjegy;
        }
        else if (Kijelzo.Text.Length < 16)
        {
            Kijelzo.Text += szamjegy;
        }
        Frissit();
    }

    private void Tizedesvesszo_Click(object sender, RoutedEventArgs e)
    {
        if (ujSzamKezdodik)
        {
            Kijelzo.Text = "0,";
            ujSzamKezdodik = false;
        }
        else if (!Kijelzo.Text.Contains(','))   // egy számban csak egy vessző lehet
        {
            Kijelzo.Text += ",";
        }
        Frissit();
    }

    private void Elojel_Click(object sender, RoutedEventArgs e)
    {
        if (Kijelzo.Text.StartsWith("-"))
            Kijelzo.Text = Kijelzo.Text.Substring(1);
        else
            Kijelzo.Text = "-" + Kijelzo.Text;
        ujSzamKezdodik = false;
        Frissit();
    }

    private void Visszatorles_Click(object sender, RoutedEventArgs e)
    {
        if (ujSzamKezdodik) return;          // eredményből nem törlünk számjegyet
        string szoveg = Kijelzo.Text.Substring(0, Kijelzo.Text.Length - 1);
        Kijelzo.Text = (szoveg == "" || szoveg == "-") ? "0" : szoveg;
        Frissit();
    }

    // CE: csak az épp beírt számot törli, a félbehagyott művelet megmarad
    private void TorlesKijelzo_Click(object sender, RoutedEventArgs e)
    {
        Kijelzo.Text = "0";
        ujSzamKezdodik = true;
        Frissit();
    }

    // C: mindent alaphelyzetbe állít
    private void TorlesMinden_Click(object sender, RoutedEventArgs e)
    {
        Kijelzo.Text = "0";
        MuveletLabel.Content = " ";
        muvelet = "";
        elsoSzam = 0;
        ujSzamKezdodik = true;
        Frissit();
    }

    private void Muvelet_Click(object sender, RoutedEventArgs e)
    {
        elsoSzam = KijelzoErteke();
        muvelet = ((Button)sender).Content.ToString()!;
        MuveletLabel.Content = $"{elsoSzam.ToString(Magyar)} {muvelet}";
        ujSzamKezdodik = true;
        Frissit();
    }

    private void Egyenlo_Click(object sender, RoutedEventArgs e)
    {
        double masodikSzam = KijelzoErteke();
        if (muvelet == "/" && masodikSzam == 0)
        {
            MessageBox.Show("Nullával nem lehet osztani!", "Hiba",
                MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        double eredmeny = muvelet switch
        {
            "+" => elsoSzam + masodikSzam,
            "-" => elsoSzam - masodikSzam,
            "*" => elsoSzam * masodikSzam,
            _ => elsoSzam / masodikSzam
        };

        MuveletLabel.Content = $"{elsoSzam.ToString(Magyar)} {muvelet} {masodikSzam.ToString(Magyar)} =";
        // Kerekítés: így 0,1 + 0,2 eredménye 0,3 lesz, nem 0,30000000000000004
        Kijelzo.Text = Math.Round(eredmeny, 10).ToString(Magyar);
        muvelet = "";
        ujSzamKezdodik = true;
        Frissit();
    }

    private void Frissit()
    {
        EgyenloGomb.IsEnabled = muvelet != "" && !ujSzamKezdodik;
    }
}
