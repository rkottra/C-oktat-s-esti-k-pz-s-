using System.IO;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;

namespace KetLista;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    // ----- Hozzáadás és törlés -----

    // Közös segédmetódus: a két oldal ugyanúgy működik, csak más vezérlőkkel
    private void Hozzaad(TextBox szovegdoboz, ListBox lista)
    {
        string szoveg = szovegdoboz.Text.Trim();
        if (szoveg != "" && !lista.Items.Contains(szoveg))
        {
            lista.Items.Add(szoveg);
            szovegdoboz.Text = "";
        }
        szovegdoboz.Focus();
        Frissit();
    }

    private void Torol(ListBox lista)
    {
        // SelectedIndex == -1: nincs kijelölt elem
        if (lista.SelectedIndex == -1) return;
        lista.Items.RemoveAt(lista.SelectedIndex);
        Frissit();
    }

    private void BalHozzaadGomb_Click(object sender, RoutedEventArgs e) => Hozzaad(BalTextBox, BalLista);
    private void JobbHozzaadGomb_Click(object sender, RoutedEventArgs e) => Hozzaad(JobbTextBox, JobbLista);
    private void BalTorolGomb_Click(object sender, RoutedEventArgs e) => Torol(BalLista);
    private void JobbTorolGomb_Click(object sender, RoutedEventArgs e) => Torol(JobbLista);

    // ----- Áthelyezés -----

    private void Athelyez(ListBox honnan, ListBox hova, bool mindet)
    {
        if (mindet)
        {
            // Az Items-et nem módosíthatjuk, miközben foreach-csel bejárjuk,
            // ezért előbb listába másoljuk az elemeket
            List<string> elemek = honnan.Items.Cast<string>().ToList();
            foreach (string elem in elemek)
            {
                if (!hova.Items.Contains(elem)) hova.Items.Add(elem);
            }
            honnan.Items.Clear();
        }
        else if (honnan.SelectedItem is string kijelolt)
        {
            if (!hova.Items.Contains(kijelolt)) hova.Items.Add(kijelolt);
            honnan.Items.Remove(kijelolt);
        }
        Frissit();
    }

    private void JobbraGomb_Click(object sender, RoutedEventArgs e) => Athelyez(BalLista, JobbLista, false);
    private void MindJobbraGomb_Click(object sender, RoutedEventArgs e) => Athelyez(BalLista, JobbLista, true);
    private void BalraGomb_Click(object sender, RoutedEventArgs e) => Athelyez(JobbLista, BalLista, false);
    private void MindBalraGomb_Click(object sender, RoutedEventArgs e) => Athelyez(JobbLista, BalLista, true);

    // ----- A gombok állapota -----

    private void BalTextBox_TextChanged(object sender, TextChangedEventArgs e) => Frissit();
    private void JobbTextBox_TextChanged(object sender, TextChangedEventArgs e) => Frissit();
    private void Lista_SelectionChanged(object sender, SelectionChangedEventArgs e) => Frissit();

    private void Frissit()
    {
        // A TextChanged már az InitializeComponent közben lefuthat, amikor a később
        // definiált vezérlők még nem léteznek – ilyenkor nincs teendő
        if (BalLista == null || JobbLista == null || JobbTorolGomb == null) return;

        BalHozzaadGomb.IsEnabled = BalTextBox.Text.Trim() != "" && !BalLista.Items.Contains(BalTextBox.Text.Trim());
        JobbHozzaadGomb.IsEnabled = JobbTextBox.Text.Trim() != "" && !JobbLista.Items.Contains(JobbTextBox.Text.Trim());
        BalTorolGomb.IsEnabled = BalLista.SelectedIndex != -1;
        JobbTorolGomb.IsEnabled = JobbLista.SelectedIndex != -1;

        JobbraGomb.IsEnabled = BalLista.SelectedIndex != -1;
        MindJobbraGomb.IsEnabled = BalLista.Items.Count > 0;
        BalraGomb.IsEnabled = JobbLista.SelectedIndex != -1;
        MindBalraGomb.IsEnabled = JobbLista.Items.Count > 0;
    }

    // ----- Mentés és betöltés fájlpárbeszéd-ablakkal -----

    private const string Szuro = "Szöveges fájl (*.txt)|*.txt|Minden fájl (*.*)|*.*";

    private void MentesGomb_Click(object sender, RoutedEventArgs e)
    {
        SaveFileDialog ablak = new SaveFileDialog();
        ablak.Title = "Bal oldali lista mentése";
        ablak.FileName = "lista.txt";
        ablak.Filter = Szuro;
        // A ShowDialog bool?-t ad vissza: true, ha a felhasználó a Mentés gombot nyomta
        if (ablak.ShowDialog() == true)
        {
            File.WriteAllLines(ablak.FileName, BalLista.Items.Cast<string>());
        }
    }

    private void BetoltesGomb_Click(object sender, RoutedEventArgs e)
    {
        OpenFileDialog ablak = new OpenFileDialog();
        ablak.Title = "Bal oldali lista betöltése";
        ablak.Filter = Szuro;
        if (ablak.ShowDialog() != true) return;

        try
        {
            BalLista.Items.Clear();
            foreach (string sor in File.ReadAllLines(ablak.FileName))
            {
                if (sor.Trim() != "" && !BalLista.Items.Contains(sor.Trim()))
                    BalLista.Items.Add(sor.Trim());
            }
        }
        catch (IOException ex)
        {
            MessageBox.Show("Nem sikerült beolvasni a fájlt: " + ex.Message, "Hiba",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
        Frissit();
    }
}
