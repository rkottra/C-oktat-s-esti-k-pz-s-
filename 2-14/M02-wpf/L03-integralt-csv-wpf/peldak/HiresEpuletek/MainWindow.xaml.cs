using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using HiresEpuletek.Models;

namespace HiresEpuletek;

public partial class MainWindow : Window
{
    public ObservableCollection<Epulet> Epuletek { get; } = new();

    // A szerkesztőben látható másolat (null, ha nincs kiválasztás).
    private Epulet? szerkesztett;
    private bool modosult;   // volt-e mentett változtatás a fájl betöltése óta

    public MainWindow()
    {
        InitializeComponent();

        try
        {
            Epuletek = EpuletTar.Beolvas();
        }
        catch (Exception ex) when (ex is IOException or FormatException or IndexOutOfRangeException)
        {
            MessageBox.Show($"Az adatfájl nem olvasható:\n{ex.Message}", "Hiba",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }

        DataContext = this;
        Szerkeszto.DataContext = null;   // induláskor üres a szerkesztő
    }

    private void EpuletLista_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (EpuletLista.SelectedItem is Epulet kivalasztott)
        {
            // MÁSOLATOT szerkesztünk: a lista csak mentéskor változik.
            szerkesztett = kivalasztott.Masolat();
            Szerkeszto.DataContext = szerkesztett;
        }
    }

    private void Mentes_Click(object sender, RoutedEventArgs e)
    {
        if (szerkesztett == null || EpuletLista.SelectedItem is not Epulet eredeti)
            return;

        if (string.IsNullOrWhiteSpace(szerkesztett.Nev) || szerkesztett.Magassag <= 0)
        {
            MessageBox.Show("A név nem lehet üres, a magasság legyen pozitív!", "Hibás adat",
                MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        eredeti.AdatokAtvetele(szerkesztett);   // a PropertyChanged miatt a lista is frissül
        modosult = true;
        SzerkesztesVege();
    }

    private void Megse_Click(object sender, RoutedEventArgs e)
    {
        SzerkesztesVege();   // a másolatot egyszerűen eldobjuk
    }

    private void SzerkesztesVege()
    {
        szerkesztett = null;
        Szerkeszto.DataContext = null;
        EpuletLista.SelectedIndex = -1;
    }

    // Bezáráskor rákérdezünk a mentésre; a Cancel = true megakadályozza a bezárást.
    private void Window_Closing(object? sender, CancelEventArgs e)
    {
        if (!modosult)
            return;

        var valasz = MessageBox.Show("Mented a módosításokat a fájlba?", "Kilépés",
            MessageBoxButton.YesNoCancel, MessageBoxImage.Question);

        if (valasz == MessageBoxResult.Cancel)
            e.Cancel = true;
        else if (valasz == MessageBoxResult.Yes)
            EpuletTar.Ment(Epuletek);
    }
}
