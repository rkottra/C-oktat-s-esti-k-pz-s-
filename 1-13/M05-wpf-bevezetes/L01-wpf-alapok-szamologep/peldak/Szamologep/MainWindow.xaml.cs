using System.Windows;
using System.Windows.Controls;

namespace Szamologep;

public partial class MainWindow : Window
{
    // Az ablak "állapota": ezekre a mezőkre több eseménykezelőnek is szüksége van
    private long elsoSzam;              // a műveleti jel előtt beírt szám
    private string muvelet = "";        // "+", "-", "*", "/" vagy "" (nincs művelet)
    private bool ujSzamKezdodik = true; // a következő számjegy felülírja a kijelzőt

    public MainWindow()
    {
        InitializeComponent();
    }

    // Közös eseménykezelő mind a 10 számgombhoz
    private void Szamjegy_Click(object sender, RoutedEventArgs e)
    {
        // A sender az a gomb, amelyikre kattintottak
        Button gomb = (Button)sender;
        string szamjegy = gomb.Content.ToString()!;

        if (ujSzamKezdodik || Kijelzo.Text == "0")
        {
            Kijelzo.Text = szamjegy;          // így nem lesz "007"
            ujSzamKezdodik = false;
        }
        else if (Kijelzo.Text.Length < 15)    // ne fusson túl a long tartományán
        {
            Kijelzo.Text += szamjegy;
        }
        Frissit();
    }

    // Közös eseménykezelő a négy műveleti gombhoz
    private void Muvelet_Click(object sender, RoutedEventArgs e)
    {
        Button gomb = (Button)sender;
        elsoSzam = long.Parse(Kijelzo.Text);  // a kijelzőre csak számjegy kerülhet
        muvelet = gomb.Content.ToString()!;
        MuveletLabel.Content = $"{elsoSzam} {muvelet}";
        ujSzamKezdodik = true;
        Frissit();
    }

    private void Egyenlo_Click(object sender, RoutedEventArgs e)
    {
        long masodikSzam = long.Parse(Kijelzo.Text);
        long eredmeny;
        switch (muvelet)
        {
            case "+": eredmeny = elsoSzam + masodikSzam; break;
            case "-": eredmeny = elsoSzam - masodikSzam; break;
            case "*": eredmeny = elsoSzam * masodikSzam; break;
            case "/":
                if (masodikSzam == 0)
                {
                    MessageBox.Show("Nullával nem lehet osztani!", "Hiba",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                eredmeny = elsoSzam / masodikSzam;   // egész osztás: 7 / 2 = 3
                break;
            default: return;
        }

        MuveletLabel.Content = $"{elsoSzam} {muvelet} {masodikSzam} =";
        Kijelzo.Text = eredmeny.ToString();
        muvelet = "";
        ujSzamKezdodik = true;
        Frissit();
    }

    private void Torles_Click(object sender, RoutedEventArgs e)
    {
        Kijelzo.Text = "0";
        MuveletLabel.Content = " ";
        muvelet = "";
        elsoSzam = 0;
        ujSzamKezdodik = true;
        Frissit();
    }

    // A gombok engedélyezése az állapottól függ: ezt egy helyen számoljuk ki
    private void Frissit()
    {
        EgyenloGomb.IsEnabled = muvelet != "" && !ujSzamKezdodik;
    }
}
