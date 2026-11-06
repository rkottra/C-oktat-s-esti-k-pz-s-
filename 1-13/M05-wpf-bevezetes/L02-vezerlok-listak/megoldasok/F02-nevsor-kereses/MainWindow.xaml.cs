using System.Windows;
using System.Windows.Controls;

namespace NevsorKereses;

public partial class MainWindow : Window
{
    private const string Mind = "(mind)";

    // A teljes névsor – a ListBox mindig csak a szűrt nézetet mutatja
    private readonly List<Diak> diakok = new List<Diak>
    {
        new Diak { Nev = "Ács Benedek",      Osztaly = "13A", Szuletesiev = 2007, Atlag = 4.47 },
        new Diak { Nev = "Bárány Luca",      Osztaly = "13A", Szuletesiev = 2008, Atlag = 3.44 },
        new Diak { Nev = "Csonka Márton",    Osztaly = "13A", Szuletesiev = 2007, Atlag = 2.47 },
        new Diak { Nev = "Deák Zsófia",      Osztaly = "13A", Szuletesiev = 2008, Atlag = 4.61 },
        new Diak { Nev = "Erdős Kristóf",    Osztaly = "13A", Szuletesiev = 2007, Atlag = 3.31 },
        new Diak { Nev = "Fazekas Noémi",    Osztaly = "13B", Szuletesiev = 2008, Atlag = 4.22 },
        new Diak { Nev = "Gulyás Ádám",      Osztaly = "13B", Szuletesiev = 2006, Atlag = 2.31 },
        new Diak { Nev = "Hegedűs Réka",     Osztaly = "13B", Szuletesiev = 2008, Atlag = 4.50 },
        new Diak { Nev = "Illés Dániel",     Osztaly = "13B", Szuletesiev = 2007, Atlag = 3.33 },
        new Diak { Nev = "Jakab Petra",      Osztaly = "13B", Szuletesiev = 2008, Atlag = 4.38 },
        new Diak { Nev = "Kocsis Levente",   Osztaly = "13C", Szuletesiev = 2007, Atlag = 3.94 },
        new Diak { Nev = "Lengyel Boglárka", Osztaly = "13C", Szuletesiev = 2008, Atlag = 3.67 },
        new Diak { Nev = "Mészáros Bence",   Osztaly = "13C", Szuletesiev = 2007, Atlag = 4.80 },
        new Diak { Nev = "Nagy Eszter",      Osztaly = "13C", Szuletesiev = 2008, Atlag = 4.05 },
        new Diak { Nev = "Orosz Tamás",      Osztaly = "13C", Szuletesiev = 2006, Atlag = 2.95 },
        new Diak { Nev = "Pintér Anna",      Osztaly = "13A", Szuletesiev = 2008, Atlag = 4.92 },
        new Diak { Nev = "Rácz Dominik",     Osztaly = "13B", Szuletesiev = 2007, Atlag = 3.58 },
        new Diak { Nev = "Simon Kata",       Osztaly = "13C", Szuletesiev = 2008, Atlag = 4.66 },
    };

    public MainWindow()
    {
        InitializeComponent();

        // Az osztályok listája az adatokból (különböző értékek kigyűjtése)
        List<string> osztalyok = new List<string>();
        foreach (Diak d in diakok)
            if (!osztalyok.Contains(d.Osztaly))
                osztalyok.Add(d.Osztaly);
        osztalyok.Sort();

        OsztalyCombo.Items.Add(Mind);
        foreach (string o in osztalyok)
            OsztalyCombo.Items.Add(o);
        OsztalyCombo.SelectedIndex = 0;

        // Az eseménykezelőket az InitializeComponent UTÁN kötjük be,
        // így egyik sem fut le félkész felületen.
        NevTextBox.TextChanged += (s, e) => Szur();
        OsztalyCombo.SelectionChanged += (s, e) => Szur();
        JelesCheck.Checked += (s, e) => Szur();
        JelesCheck.Unchecked += (s, e) => Szur();
        NevSzerintRadio.Checked += (s, e) => Szur();
        AtlagSzerintRadio.Checked += (s, e) => Szur();
        TalalatLista.SelectionChanged += (s, e) => Reszletek();

        Szur();
    }

    private void Szur()
    {
        string resz = NevTextBox.Text.Trim();
        string? osztaly = OsztalyCombo.SelectedIndex > 0 ? (string)OsztalyCombo.SelectedItem : null;
        bool csakJeles = JelesCheck.IsChecked == true;

        // Kiválogatás több feltétellel
        List<Diak> talalatok = new List<Diak>();
        foreach (Diak d in diakok)
        {
            if (d.Nev.Contains(resz, StringComparison.OrdinalIgnoreCase)
                && (osztaly == null || d.Osztaly == osztaly)
                && (!csakJeles || d.Atlag > 4.5))
                talalatok.Add(d);
        }

        if (AtlagSzerintRadio.IsChecked == true)
            talalatok.Sort((a, b) => b.Atlag.CompareTo(a.Atlag));
        else
            talalatok.Sort((a, b) => string.Compare(a.Nev, b.Nev));

        // A korábban kijelölt diák megtartása, ha benne van a találatokban
        Diak? kijelolt = TalalatLista.SelectedItem as Diak;
        TalalatLista.Items.Clear();
        foreach (Diak d in talalatok)
            TalalatLista.Items.Add(d);
        if (kijelolt != null && talalatok.Contains(kijelolt))
            TalalatLista.SelectedItem = kijelolt;

        TalalatLabel.Content = $"{talalatok.Count} találat / {diakok.Count} diák";
        Reszletek();
    }

    private void Reszletek()
    {
        if (TalalatLista.SelectedItem is Diak d)
            ReszletekTextBlock.Text = $"{d.Nev}\nOsztály: {d.Osztaly}\nSzületési év: {d.Szuletesiev}\nÁtlag: {d.Atlag:F2}";
        else
            ReszletekTextBlock.Text = "Válassz ki egy diákot!";
    }

    private void TorlesGomb_Click(object sender, RoutedEventArgs e)
    {
        NevTextBox.Text = "";
        OsztalyCombo.SelectedIndex = 0;
        JelesCheck.IsChecked = false;
        NevSzerintRadio.IsChecked = true;
        Szur();
    }
}
