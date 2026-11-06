using System.Windows;
using System.Windows.Controls;

namespace Teendolista;

public partial class MainWindow : Window
{
    private const string KeszJel = "✓ ";

    public MainWindow()
    {
        InitializeComponent();
        TeendoTextBox.Focus();
        Frissit();
    }

    private void HozzaadGomb_Click(object sender, RoutedEventArgs e)
    {
        string szoveg = TeendoTextBox.Text.Trim();
        if (szoveg == "" || Szerepel(szoveg))
            return;
        TeendoLista.Items.Add(szoveg);
        TeendoTextBox.Text = "";
        TeendoTextBox.Focus();
        Frissit();
    }

    // Eldöntés: van-e már ilyen teendő (a kész-jeltől függetlenül)?
    private bool Szerepel(string szoveg)
    {
        foreach (object o in TeendoLista.Items)
        {
            string s = o.ToString()!;
            if (s == szoveg || s == KeszJel + szoveg)
                return true;
        }
        return false;
    }

    private void KeszGomb_Click(object sender, RoutedEventArgs e)
    {
        int i = TeendoLista.SelectedIndex;
        if (i == -1) return;
        string s = TeendoLista.Items[i].ToString()!;
        string uj = s.StartsWith(KeszJel) ? s[KeszJel.Length..] : KeszJel + s;
        TeendoLista.Items[i] = uj;            // az elem cseréje a helyén
        TeendoLista.SelectedIndex = i;
        Frissit();
    }

    private void Mozgat(int irany)
    {
        int i = TeendoLista.SelectedIndex;
        int uj = i + irany;
        if (i == -1 || uj < 0 || uj >= TeendoLista.Items.Count) return;

        object elem = TeendoLista.Items[i];
        TeendoLista.Items.RemoveAt(i);
        TeendoLista.Items.Insert(uj, elem);
        TeendoLista.SelectedIndex = uj;       // a kijelölés megy az elemmel
        Frissit();
    }

    private void FelGomb_Click(object sender, RoutedEventArgs e) => Mozgat(-1);
    private void LeGomb_Click(object sender, RoutedEventArgs e) => Mozgat(+1);

    private void TorolGomb_Click(object sender, RoutedEventArgs e)
    {
        int i = TeendoLista.SelectedIndex;
        if (i == -1) return;
        TeendoLista.Items.RemoveAt(i);
        // Kényelmi funkció: a következő (vagy az utolsó) elemet jelöljük ki
        if (TeendoLista.Items.Count > 0)
            TeendoLista.SelectedIndex = Math.Min(i, TeendoLista.Items.Count - 1);
        Frissit();
    }

    private void MindTorlesGomb_Click(object sender, RoutedEventArgs e)
    {
        MessageBoxResult valasz = MessageBox.Show("Biztosan törlöd az összes teendőt?", "Mind törlése",
                                                  MessageBoxButton.YesNo, MessageBoxImage.Question);
        if (valasz == MessageBoxResult.Yes)
        {
            TeendoLista.Items.Clear();
            Frissit();
        }
    }

    private void TeendoTextBox_TextChanged(object sender, TextChangedEventArgs e) => Frissit();
    private void TeendoLista_SelectionChanged(object sender, SelectionChangedEventArgs e) => Frissit();

    // Minden állapotfüggő beállítás egy helyen
    private void Frissit()
    {
        int i = TeendoLista.SelectedIndex;
        int db = TeendoLista.Items.Count;

        HozzaadGomb.IsEnabled = TeendoTextBox.Text.Trim() != "";
        KeszGomb.IsEnabled = i != -1;
        TorolGomb.IsEnabled = i != -1;
        FelGomb.IsEnabled = i > 0;
        LeGomb.IsEnabled = i != -1 && i < db - 1;
        MindTorlesGomb.IsEnabled = db > 0;

        int kesz = 0;
        foreach (object o in TeendoLista.Items)
            if (o.ToString()!.StartsWith(KeszJel))
                kesz++;
        AllapotLabel.Content = $"{db} teendő, ebből {kesz} kész";
    }
}
