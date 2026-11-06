using System.Windows;

namespace Szamlalo;

public partial class MainWindow : Window
{
    private const int Minimum = 0;
    private const int Maximum = 100;

    // Az értéket változóban tartjuk, nem a Label szövegéből olvassuk vissza
    private int ertek = 0;

    public MainWindow()
    {
        InitializeComponent();
    }

    private void NovelGomb_Click(object sender, RoutedEventArgs e)
    {
        if (ertek < Maximum) ertek++;
        Frissit();
    }

    private void CsokkentGomb_Click(object sender, RoutedEventArgs e)
    {
        if (ertek > Minimum) ertek--;
        Frissit();
    }

    private void NullazGomb_Click(object sender, RoutedEventArgs e)
    {
        ertek = 0;
        Frissit();
    }

    // Kiírja az értéket, és a határokon letiltja a megfelelő gombot
    private void Frissit()
    {
        ErtekLabel.Content = ertek;
        NovelGomb.IsEnabled = ertek < Maximum;
        CsokkentGomb.IsEnabled = ertek > Minimum;
        NullazGomb.IsEnabled = ertek != 0;
    }
}
