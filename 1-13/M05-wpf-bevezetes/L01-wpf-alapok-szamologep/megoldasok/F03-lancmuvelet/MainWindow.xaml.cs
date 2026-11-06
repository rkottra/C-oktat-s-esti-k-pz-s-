using System.Windows;
using System.Windows.Controls;

namespace SzamologepLanc;

public partial class MainWindow : Window
{
    private long reszeredmeny;          // az eddigi lánc értéke
    private string muvelet = "";        // a függőben lévő művelet
    private bool ujSzamKezdodik = true;

    // Az ismételt "=" lenyomásához megjegyezzük az utolsó műveletet és operandust
    private string utolsoMuvelet = "";
    private long utolsoOperandus;

    public MainWindow()
    {
        InitializeComponent();
    }

    private void Szamjegy_Click(object sender, RoutedEventArgs e)
    {
        string szamjegy = ((Button)sender).Content.ToString()!;
        if (ujSzamKezdodik || Kijelzo.Text == "0")
        {
            Kijelzo.Text = szamjegy;
            ujSzamKezdodik = false;
        }
        else if (Kijelzo.Text.Length < 15)
        {
            Kijelzo.Text += szamjegy;
        }
    }

    // Egy művelet elvégzése; false, ha nullával osztanánk
    private bool Szamol(string jel, long a, long b, out long eredmeny)
    {
        eredmeny = 0;
        switch (jel)
        {
            case "+": eredmeny = a + b; return true;
            case "-": eredmeny = a - b; return true;
            case "*": eredmeny = a * b; return true;
            case "/":
                if (b == 0)
                {
                    MessageBox.Show("Nullával nem lehet osztani!", "Hiba",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                eredmeny = a / b;
                return true;
        }
        return false;
    }

    private void Muvelet_Click(object sender, RoutedEventArgs e)
    {
        string jel = ((Button)sender).Content.ToString()!;
        long kijelzett = long.Parse(Kijelzo.Text);

        if (muvelet != "" && !ujSzamKezdodik)
        {
            // Van függő művelet és új szám is: előbb kiszámoljuk a részeredményt
            if (!Szamol(muvelet, reszeredmeny, kijelzett, out long eredmeny)) return;
            reszeredmeny = eredmeny;
            MuveletLabel.Content = $"{MuveletLabel.Content} {kijelzett} {jel}";
            Kijelzo.Text = reszeredmeny.ToString();
        }
        else if (muvelet != "" && ujSzamKezdodik)
        {
            // Két műveleti jel egymás után: csak lecseréljük a jelet
            string eddig = MuveletLabel.Content.ToString()!;
            MuveletLabel.Content = eddig.Substring(0, eddig.Length - 1) + jel;
        }
        else
        {
            reszeredmeny = kijelzett;
            MuveletLabel.Content = $"{kijelzett} {jel}";
        }
        muvelet = jel;
        ujSzamKezdodik = true;
    }

    private void Egyenlo_Click(object sender, RoutedEventArgs e)
    {
        long kijelzett = long.Parse(Kijelzo.Text);

        if (muvelet != "")
        {
            // Normál eset: a lánc lezárása
            if (!Szamol(muvelet, reszeredmeny, kijelzett, out long eredmeny)) return;
            MuveletLabel.Content = $"{MuveletLabel.Content} {kijelzett} =";
            utolsoMuvelet = muvelet;
            utolsoOperandus = kijelzett;
            Kijelzo.Text = eredmeny.ToString();
            muvelet = "";
        }
        else if (utolsoMuvelet != "")
        {
            // Ismételt "=": az utolsó műveletet végzi el újra (3 + 3 = = = → 12)
            if (!Szamol(utolsoMuvelet, kijelzett, utolsoOperandus, out long eredmeny)) return;
            MuveletLabel.Content = $"{kijelzett} {utolsoMuvelet} {utolsoOperandus} =";
            Kijelzo.Text = eredmeny.ToString();
        }
        ujSzamKezdodik = true;
    }

    private void Torles_Click(object sender, RoutedEventArgs e)
    {
        Kijelzo.Text = "0";
        MuveletLabel.Content = " ";
        muvelet = "";
        utolsoMuvelet = "";
        reszeredmeny = 0;
        ujSzamKezdodik = true;
    }
}
