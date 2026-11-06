using System.Windows;
using System.Windows.Controls;

namespace Atbas;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        Allapot();
    }

    private void AlkalmazGomb_Click(object sender, RoutedEventArgs e)
    {
        string be = BemenetTextBox.Text;
        KimenetTextBox.Text = AtbasRadio.IsChecked == true ? Rejtjelek.Atbas(be) : Rejtjelek.Rot13(be);
    }

    private void CsereGomb_Click(object sender, RoutedEventArgs e)
    {
        BemenetTextBox.Text = KimenetTextBox.Text;
        KimenetTextBox.Text = "";
    }

    private void BemenetTextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (IsLoaded)            // az XAML-ben megadott kezdőszöveg már a felület felépítése közben kiváltja
            Allapot();
    }

    private void Allapot()
    {
        int betu = 0, egyeb = 0;
        foreach (char c in BemenetTextBox.Text)
        {
            if (Rejtjelek.Betu(c)) betu++;
            else egyeb++;
        }
        AllapotLabel.Content = $"Betűk: {betu}, egyéb karakter: {egyeb}";
    }
}
