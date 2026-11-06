using System.Windows;
using System.Windows.Media;

namespace Vigenere;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        KulcsTextBox.TextChanged += (s, e) => KulcsEllenorzes();
        KulcsEllenorzes();
    }

    private void KulcsEllenorzes()
    {
        bool jo = VigenereRejtjel.ErvenyesKulcs(KulcsTextBox.Text);
        KulcsTextBox.Background = jo ? Brushes.White : Brushes.MistyRose;
        TitkositGomb.IsEnabled = jo;
        VisszafejtGomb.IsEnabled = jo;
    }

    private void TitkositGomb_Click(object sender, RoutedEventArgs e)
    {
        KimenetTextBox.Text = VigenereRejtjel.Titkosit(BemenetTextBox.Text, KulcsTextBox.Text);
        Szemleltet();
    }

    private void VisszafejtGomb_Click(object sender, RoutedEventArgs e)
    {
        KimenetTextBox.Text = VigenereRejtjel.Visszafejt(BemenetTextBox.Text, KulcsTextBox.Text);
        Szemleltet();
    }

    // Az első sor: bemenet / kulcssor / kimenet egymás alatt
    private void Szemleltet()
    {
        string be = ElsoSor(BemenetTextBox.Text);
        string ki = ElsoSor(KimenetTextBox.Text);
        SzemleltetoTextBlock.Text =
            $"bemenet: {be}\n" +
            $"kulcs:   {VigenereRejtjel.KulcsSor(be, KulcsTextBox.Text)}\n" +
            $"kimenet: {ki}";
    }

    private static string ElsoSor(string s)
    {
        int i = s.IndexOfAny(new[] { '\r', '\n' });
        return i >= 0 ? s[..i] : s;
    }
}
