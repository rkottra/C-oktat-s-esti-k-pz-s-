using System.Windows;
using System.Windows.Input;

namespace Bevasarlolista;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        AllapotFrissitese();
        TetelMezo.Focus();
    }

    private void AllapotFrissitese()
    {
        AllapotCimke.Content = $"Hátra: {MegveendoLista.Items.Count} db, kosárban: {KosarLista.Items.Count} db";
    }

    private void HozzaadGomb_Click(object sender, RoutedEventArgs e)
    {
        string tetel = TetelMezo.Text.Trim();
        if (tetel == "")
        {
            MessageBox.Show("Írj be egy tételt!", "Hiányzó adat");
            return;
        }
        // Items.Contains: benne van-e már a listában (pontos egyezés)
        if (MegveendoLista.Items.Contains(tetel))
        {
            MessageBox.Show($"A(z) „{tetel}” már szerepel a listán.", "Ismétlődés");
            return;
        }

        MegveendoLista.Items.Add(tetel);
        TetelMezo.Clear();
        TetelMezo.Focus();
        AllapotFrissitese();
    }

    private void MegveendoLista_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        // Áthelyezés: hozzáadás a másik listához, törlés az eredetiből
        object? kijelolt = MegveendoLista.SelectedItem;
        if (kijelolt == null)
            return;
        KosarLista.Items.Add(kijelolt);
        MegveendoLista.Items.Remove(kijelolt);
        AllapotFrissitese();
    }

    private void TorlesGomb_Click(object sender, RoutedEventArgs e)
    {
        if (MegveendoLista.SelectedItem == null)
        {
            MessageBox.Show("Jelölj ki egy tételt a bal oldali listában!");
            return;
        }
        MegveendoLista.Items.Remove(MegveendoLista.SelectedItem);
        AllapotFrissitese();
    }

    private void UritesGomb_Click(object sender, RoutedEventArgs e)
    {
        if (MessageBox.Show("Biztosan üríted mindkét listát?", "Megerősítés",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
        {
            MegveendoLista.Items.Clear();
            KosarLista.Items.Clear();
            AllapotFrissitese();
        }
    }
}
