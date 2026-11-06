using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace CaesarTores;

public partial class MainWindow : Window
{
    private readonly Rectangle[] oszlopok = new Rectangle[26];
    private int[] gyakorisag = new int[26];

    public MainWindow()
    {
        InitializeComponent();

        // A diagram 26 oszlopa kódból: alul a betű, felette a téglalap
        for (int i = 0; i < 26; i++)
        {
            DockPanel cella = new DockPanel { Margin = new Thickness(1, 0, 1, 0) };
            TextBlock betu = new TextBlock { Text = ((char)('A' + i)).ToString(), HorizontalAlignment = HorizontalAlignment.Center };
            DockPanel.SetDock(betu, Dock.Bottom);
            cella.Children.Add(betu);

            oszlopok[i] = new Rectangle { VerticalAlignment = VerticalAlignment.Bottom, Fill = Brushes.SteelBlue };
            cella.Children.Add(oszlopok[i]);
            Diagram.Children.Add(cella);
        }
    }

    private void ElemzesGomb_Click(object sender, RoutedEventArgs e)
    {
        string rejtett = RejtettTextBox.Text;
        gyakorisag = Tores.Gyakorisag(rejtett);
        DiagramRajzol();

        int k1 = Tores.KulcsLeggyakoribbAlapjan(rejtett);
        int k2 = Tores.ValoszinuKulcs(rejtett);
        EredmenyLabel.Content = $"Leggyakoribb betű: {(char)('A' + Tores.Leggyakoribb(gyakorisag))}  →  " +
                                $"„ez az e” alapján: k = {k1}   ·   pontozással: k = {k2}";

        KulcsLista.Items.Clear();
        for (int k = 0; k < 26; k++)
        {
            string nyilt = Tores.Visszafejt(rejtett, k).Replace('\n', ' ').Replace('\r', ' ');
            KulcsLista.Items.Add($"k = {k,2}: {(nyilt.Length > 40 ? nyilt[..40] + "…" : nyilt)}");
        }
        KulcsLista.SelectedIndex = k2;            // a legvalószínűbb kijelölése
        KulcsLista.ScrollIntoView(KulcsLista.SelectedItem);
    }

    // Az oszlopok magassága a rendelkezésre álló helyhez igazodik
    private void DiagramRajzol()
    {
        int maxDb = 0;
        foreach (int d in gyakorisag)
            if (d > maxDb) maxDb = d;
        if (maxDb == 0) maxDb = 1;                // ne osszunk nullával

        double maxMagassag = Math.Max(0, Diagram.ActualHeight - 25);   // 25 px a betűfeliratnak
        int leggyakoribb = Tores.Leggyakoribb(gyakorisag);
        for (int i = 0; i < 26; i++)
        {
            oszlopok[i].Height = maxMagassag * gyakorisag[i] / maxDb;
            oszlopok[i].Fill = i == leggyakoribb && gyakorisag[i] > 0 ? Brushes.OrangeRed : Brushes.SteelBlue;
            oszlopok[i].ToolTip = $"{(char)('A' + i)}: {gyakorisag[i]} db";
        }
    }

    private void Diagram_SizeChanged(object sender, SizeChangedEventArgs e) => DiagramRajzol();

    private void KulcsLista_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        int k = KulcsLista.SelectedIndex;
        NyiltTextBox.Text = k >= 0 ? Tores.Visszafejt(RejtettTextBox.Text, k) : "";
    }
}
