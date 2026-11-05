using System.Windows;
using System.Windows.Controls;

namespace WpfAlapokUrlap;

// A "partial" azért kell, mert az osztály másik fele a XAML-ből generálódik
// (onnan jönnek a Name-mel elnevezett mezők: NevMezo, TanfolyamLista, ...).
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();   // felépíti a XAML-ben leírt felületet – ez előtt a vezérlők még nem léteznek!
    }

    private void BetoltesGomb_Click(object sender, RoutedEventArgs e)
    {
        // Kódból is adhatunk elemeket a listához.
        TanfolyamLista.Items.Clear();
        TanfolyamLista.Items.Add("C# alapok");
        TanfolyamLista.Items.Add("WPF felületek");
        TanfolyamLista.Items.Add("Adatbázis-kezelés");
        TanfolyamLista.Items.Add("Webfejlesztés");

        BetoltesGomb.IsEnabled = false;   // egyszer elég betölteni
    }

    private void TanfolyamLista_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        // SelectedItem: a kiválasztott elem (vagy null, ha nincs kiválasztás)
        if (TanfolyamLista.SelectedItem != null)
            KivalasztottCimke.Content = $"Kiválasztva: {TanfolyamLista.SelectedItem}";
        else
            KivalasztottCimke.Content = "Nincs kiválasztott tanfolyam.";
    }

    private void KorMezo_TextChanged(object sender, TextChangedEventArgs e)
    {
        // Minden billentyűleütésnél lefut. Itt csak jelezzük a hibás értéket.
        bool jo = int.TryParse(KorMezo.Text, out int kor) && kor >= 14 && kor <= 99;
        KorMezo.Background = jo ? System.Windows.Media.Brushes.White : System.Windows.Media.Brushes.MistyRose;
    }

    private void JelentkezesGomb_Click(object sender, RoutedEventArgs e)
    {
        // Ellenőrzés: hiba esetén üzenet, és kilépünk a metódusból.
        if (string.IsNullOrWhiteSpace(NevMezo.Text))
        {
            MessageBox.Show("Add meg a neved!", "Hiányzó adat", MessageBoxButton.OK, MessageBoxImage.Warning);
            NevMezo.Focus();
            return;
        }
        if (!int.TryParse(KorMezo.Text, out int kor) || kor < 14 || kor > 99)
        {
            MessageBox.Show("Az életkor 14 és 99 közötti egész szám legyen!", "Hibás adat",
                MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }
        if (TanfolyamLista.SelectedItem == null)
        {
            MessageBox.Show("Válassz tanfolyamot a listából!", "Hiányzó adat");
            return;
        }

        // A RadioButton/CheckBox IsChecked tulajdonsága bool? típusú (lehet null is) – ezért az "== true".
        string idopont = DelelottiRadio.IsChecked == true ? "délelőtt"
                       : DelutaniRadio.IsChecked == true ? "délután"
                       : "este";
        string hirlevel = HirlevelCheck.IsChecked == true ? "kér" : "nem kér";

        MessageBox.Show(
            $"{NevMezo.Text} ({kor} éves) jelentkezett: {TanfolyamLista.SelectedItem}, {idopont}.\nHírlevelet {hirlevel}.",
            "Sikeres jelentkezés", MessageBoxButton.OK, MessageBoxImage.Information);
    }

    private void TorlesGomb_Click(object sender, RoutedEventArgs e)
    {
        // A MessageBox.Show visszaadja, melyik gombot nyomta meg a felhasználó.
        var valasz = MessageBox.Show("Biztosan törlöd az űrlapot?", "Megerősítés",
            MessageBoxButton.YesNo, MessageBoxImage.Question);
        if (valasz != MessageBoxResult.Yes)
            return;

        NevMezo.Text = "";
        KorMezo.Text = "25";
        DelelottiRadio.IsChecked = true;
        HirlevelCheck.IsChecked = false;
        TanfolyamLista.SelectedIndex = -1;   // -1 = nincs kiválasztott elem
    }
}
