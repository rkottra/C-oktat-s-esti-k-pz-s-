using System.IO;
using System.Windows;
using System.Windows.Media;
using Microsoft.Win32;

namespace Kosar;

public partial class MainWindow : Window
{
    private const string Mind = "(mind)";
    private const int IngyenesSzallitasHatar = 20_000;

    private readonly List<Termek> termekek = new List<Termek>
    {
        new Termek { Nev = "Füzet A4",            Ar = 390,   Kategoria = "papír-írószer" },
        new Termek { Nev = "Golyóstoll (kék)",    Ar = 290,   Kategoria = "papír-írószer" },
        new Termek { Nev = "Szövegkiemelő-szett", Ar = 1290,  Kategoria = "papír-írószer" },
        new Termek { Nev = "Iskolatáska",         Ar = 14990, Kategoria = "táska" },
        new Termek { Nev = "Tornazsák",           Ar = 2490,  Kategoria = "táska" },
        new Termek { Nev = "USB-pendrive 64 GB",  Ar = 3990,  Kategoria = "informatika" },
        new Termek { Nev = "Vezeték nélküli egér", Ar = 5490, Kategoria = "informatika" },
        new Termek { Nev = "Fejhallgató",         Ar = 8990,  Kategoria = "informatika" },
        new Termek { Nev = "Kulacs 0,7 l",        Ar = 2990,  Kategoria = "egyéb" },
        new Termek { Nev = "Számológép",          Ar = 4590,  Kategoria = "egyéb" },
    };

    public MainWindow()
    {
        InitializeComponent();

        List<string> kategoriak = new List<string>();
        foreach (Termek t in termekek)
            if (!kategoriak.Contains(t.Kategoria))
                kategoriak.Add(t.Kategoria);
        KategoriaCombo.Items.Add(Mind);
        foreach (string k in kategoriak)
            KategoriaCombo.Items.Add(k);
        KategoriaCombo.SelectedIndex = 0;

        // Eseménykezelők bekötése a felület felépítése után
        KategoriaCombo.SelectionChanged += (s, e) => TermekekMutatasa();
        TermekLista.SelectionChanged += (s, e) => Frissit();
        KosarLista.SelectionChanged += (s, e) => Frissit();
        MennyisegTextBox.TextChanged += (s, e) => Frissit();
        KuponCheck.Checked += (s, e) => Frissit();
        KuponCheck.Unchecked += (s, e) => Frissit();
        SzemelyesRadio.Checked += (s, e) => Frissit();
        AutomataRadio.Checked += (s, e) => Frissit();
        FutarRadio.Checked += (s, e) => Frissit();

        TermekekMutatasa();
    }

    private void TermekekMutatasa()
    {
        string kategoria = (string)KategoriaCombo.SelectedItem;
        TermekLista.Items.Clear();
        foreach (Termek t in termekek)
            if (kategoria == Mind || t.Kategoria == kategoria)
                TermekLista.Items.Add(t);
        Frissit();
    }

    private bool ErvenyesMennyiseg(out int db) =>
        int.TryParse(MennyisegTextBox.Text, out db) && db >= 1 && db <= 99;

    private void KosarbaGomb_Click(object sender, RoutedEventArgs e)
    {
        if (TermekLista.SelectedItem is not Termek termek || !ErvenyesMennyiseg(out int db))
            return;

        // Keresés: van-e már ilyen tétel a kosárban?
        foreach (object o in KosarLista.Items)
        {
            if (o is Tetel t && t.Termek == termek)
            {
                t.Mennyiseg = Math.Min(99, t.Mennyiseg + db);
                KosarLista.Items.Refresh();          // a megváltozott tétel újrarajzolása
                KosarLista.SelectedItem = t;
                Frissit();
                return;
            }
        }
        Tetel uj = new Tetel(termek, db);
        KosarLista.Items.Add(uj);
        KosarLista.SelectedItem = uj;
        Frissit();
    }

    private void KiveszGomb_Click(object sender, RoutedEventArgs e)
    {
        if (KosarLista.SelectedItem is Tetel t)
        {
            KosarLista.Items.Remove(t);
            Frissit();
        }
    }

    // Az összegek és a gombok állapota egy helyen
    private void Frissit()
    {
        bool joMennyiseg = ErvenyesMennyiseg(out _);
        MennyisegTextBox.Background = joMennyiseg ? Brushes.White : Brushes.MistyRose;
        KosarbaGomb.IsEnabled = joMennyiseg && TermekLista.SelectedItem != null;
        KiveszGomb.IsEnabled = KosarLista.SelectedItem != null;
        RendelesGomb.IsEnabled = KosarLista.Items.Count > 0;

        Szamol(out int termekOsszeg, out int kedvezmeny, out int szallitas);
        OsszesitoTextBlock.Text =
            $"Termékek:   {termekOsszeg,10:N0} Ft\n" +
            $"Kedvezmény: {-kedvezmeny,10:N0} Ft\n" +
            $"Szállítás:  {szallitas,10:N0} Ft{(szallitas == 0 && SzemelyesRadio.IsChecked != true ? " (ingyenes)" : "")}\n" +
            $"Fizetendő:  {termekOsszeg - kedvezmeny + szallitas,10:N0} Ft";
    }

    private void Szamol(out int termekOsszeg, out int kedvezmeny, out int szallitas)
    {
        termekOsszeg = 0;
        foreach (object o in KosarLista.Items)
            if (o is Tetel t)
                termekOsszeg += t.Reszosszeg;

        kedvezmeny = KuponCheck.IsChecked == true ? (int)Math.Round(termekOsszeg * 0.10) : 0;

        int alapSzallitas = AutomataRadio.IsChecked == true ? 990 : FutarRadio.IsChecked == true ? 1490 : 0;
        szallitas = termekOsszeg - kedvezmeny > IngyenesSzallitasHatar ? 0 : alapSzallitas;
    }

    private void RendelesGomb_Click(object sender, RoutedEventArgs e)
    {
        Szamol(out int termekOsszeg, out int kedvezmeny, out int szallitas);
        int vegosszeg = termekOsszeg - kedvezmeny + szallitas;

        List<string> nyugta = new List<string> { "NYUGTA – Iskolai webshop", $"{DateTime.Now:yyyy.MM.dd. HH:mm}", "" };
        foreach (object o in KosarLista.Items)
            nyugta.Add(o.ToString()!);
        nyugta.Add("");
        nyugta.Add($"Termékek: {termekOsszeg:N0} Ft");
        if (kedvezmeny > 0) nyugta.Add($"Kedvezmény (DIAK10): -{kedvezmeny:N0} Ft");
        nyugta.Add($"Szállítás: {szallitas:N0} Ft");
        nyugta.Add($"FIZETENDŐ: {vegosszeg:N0} Ft");

        MessageBoxResult valasz = MessageBox.Show(string.Join("\n", nyugta) + "\n\nLeadod a rendelést?",
            "Megrendelés", MessageBoxButton.YesNo, MessageBoxImage.Question);
        if (valasz != MessageBoxResult.Yes)
            return;

        SaveFileDialog ablak = new SaveFileDialog { Filter = "Szövegfájl|*.txt", FileName = "nyugta.txt" };
        if (ablak.ShowDialog() == true)
            File.WriteAllLines(ablak.FileName, nyugta);

        KosarLista.Items.Clear();
        KuponCheck.IsChecked = false;
        Frissit();
        MessageBox.Show("Köszönjük a rendelést!", "Megrendelés", MessageBoxButton.OK, MessageBoxImage.Information);
    }
}
