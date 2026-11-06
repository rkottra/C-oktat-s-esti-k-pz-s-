using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GombRacs;

public partial class MainWindow : Window
{
    private Button[,] gombok = new Button[0, 0];   // a gombok koordináta szerint
    private int kattintasok;

    public MainWindow()
    {
        InitializeComponent();
        for (int n = 3; n <= 10; n++)
            MeretCombo.Items.Add(n);
        MeretCombo.SelectedItem = 5;                // ez kiváltja a SelectionChanged-et → Felepit
    }

    private void MeretCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        Felepit((int)MeretCombo.SelectedItem);
    }

    // A rács felépítése kódból
    private void Felepit(int n)
    {
        Tabla.Children.Clear();                     // különben az előző gombok is megmaradnának
        Tabla.Rows = n;
        Tabla.Columns = n;
        gombok = new Button[n, n];
        kattintasok = 0;

        for (int sor = 0; sor < n; sor++)
        {
            for (int oszlop = 0; oszlop < n; oszlop++)
            {
                Button gomb = new Button
                {
                    Content = $"{sor + 1},{oszlop + 1}",
                    Margin = new Thickness(1),
                    Tag = (sor, oszlop),            // a hely eltárolása a gombban
                };
                gomb.Click += Cella_Click;          // MINDEN gomb ugyanazt a kezelőt kapja
                gombok[sor, oszlop] = gomb;
                Tabla.Children.Add(gomb);           // UniformGrid: sorfolytonos kitöltés
            }
        }
        InfoLabel.Content = $"{n * n} gomb létrehozva";
    }

    private void Cella_Click(object sender, RoutedEventArgs e)
    {
        Button gomb = (Button)sender;
        (int sor, int oszlop) = ((int, int))gomb.Tag;
        kattintasok++;

        int n = gombok.GetLength(0);
        for (int s = 0; s < n; s++)
            for (int o = 0; o < n; o++)
            {
                // A kattintott cella sora és oszlopa világos, maga a cella erős színt kap
                Brush szin = s == sor && o == oszlop ? Brushes.Orange
                           : s == sor || o == oszlop ? Brushes.LightYellow
                           : Brushes.LightGray;
                gombok[s, o].Background = szin;
            }

        InfoLabel.Content = $"Kattintás: {sor + 1}. sor, {oszlop + 1}. oszlop   (összesen {kattintasok})";
    }
}
