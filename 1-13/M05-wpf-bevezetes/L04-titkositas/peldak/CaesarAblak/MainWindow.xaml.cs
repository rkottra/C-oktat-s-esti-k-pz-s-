using System.IO;
using System.Windows;
using Microsoft.Win32;

namespace CaesarAblak;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        // Élő frissítés: bármelyik beállítás változik, újraszámolunk.
        // Kódból kötjük be, hogy az XAML kezdőértékei ne váltsák ki félkész felületen.
        BemenetTextBox.TextChanged += (s, e) => Frissit();
        KulcsSlider.ValueChanged += (s, e) => Frissit();
        TitkositRadio.Checked += (s, e) => Frissit();
        VisszafejtRadio.Checked += (s, e) => Frissit();
        EkezetCheck.Checked += (s, e) => Frissit();
        EkezetCheck.Unchecked += (s, e) => Frissit();
        Frissit();
    }

    private void Frissit()
    {
        int k = (int)KulcsSlider.Value;
        KulcsLabel.Content = $"{k}  (A→{(char)('A' + k)})";

        string szoveg = BemenetTextBox.Text;
        if (EkezetCheck.IsChecked == true)
            szoveg = Caesar.Ekezettelenit(szoveg);

        // A felület csak hívja a logikát
        KimenetTextBox.Text = TitkositRadio.IsChecked == true
            ? Caesar.Titkosit(szoveg, k)
            : Caesar.Visszafejt(szoveg, k);
    }

    // A kimenet lesz a bemenet, és az irány megfordul – így látszik, hogy a visszafejtés visszaadja az eredetit
    private void CsereGomb_Click(object sender, RoutedEventArgs e)
    {
        string kimenet = KimenetTextBox.Text;
        if (TitkositRadio.IsChecked == true) VisszafejtRadio.IsChecked = true;
        else TitkositRadio.IsChecked = true;
        BemenetTextBox.Text = kimenet;
    }

    private void MasolasGomb_Click(object sender, RoutedEventArgs e)
    {
        Clipboard.SetText(KimenetTextBox.Text);
    }

    private void MegnyitasGomb_Click(object sender, RoutedEventArgs e)
    {
        OpenFileDialog ablak = new OpenFileDialog { Filter = "Szövegfájl|*.txt|Minden fájl|*.*" };
        if (ablak.ShowDialog() == true)
            BemenetTextBox.Text = File.ReadAllText(ablak.FileName);
    }

    private void MentesGomb_Click(object sender, RoutedEventArgs e)
    {
        SaveFileDialog ablak = new SaveFileDialog { Filter = "Szövegfájl|*.txt", FileName = "titkos.txt" };
        if (ablak.ShowDialog() == true)
            File.WriteAllText(ablak.FileName, KimenetTextBox.Text);
    }
}
