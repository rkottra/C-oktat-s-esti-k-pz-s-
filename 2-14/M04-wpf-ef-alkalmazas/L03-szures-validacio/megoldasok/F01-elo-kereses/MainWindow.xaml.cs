using System.Windows;
using Microsoft.EntityFrameworkCore;
using RaktarKereses.Models;

namespace RaktarKereses;

public partial class MainWindow : Window
{
    private readonly RaktarContext db = new();
    private List<Termek> osszesTermek = new();

    public MainWindow()
    {
        InitializeComponent();

        try
        {
            db.Database.EnsureCreated();
            osszesTermek = db.Termekek.Include(t => t.Kategoria).ToList();

            var kategoriak = new List<Kategoria> { new Kategoria { Id = 0, Nev = "(összes)" } };
            kategoriak.AddRange(db.Kategoriak.OrderBy(k => k.Nev));
            cmbKategoria.ItemsSource = kategoriak;
            cmbKategoria.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            MessageBox.Show("Az adatbázis nem érhető el:\n" + ex.Message, "Hiba",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }

        Szures();
    }

    private void Szuro_Valtozott(object sender, RoutedEventArgs e) => Szures();

    private void Szures()
    {
        // Az InitializeComponent közben is jöhet esemény (IsSelected="True"), amikor még nincs minden vezérlő kész.
        if (dgTermekek == null || txtAllapot == null)
            return;

        string keresett = txtKereses.Text.Trim();
        IEnumerable<Termek> talalat = osszesTermek;

        if (keresett != "")
            talalat = talalat.Where(t => t.Nev.Contains(keresett, StringComparison.OrdinalIgnoreCase)
                                      || t.Cikkszam.Contains(keresett, StringComparison.OrdinalIgnoreCase));

        if (cmbKategoria.SelectedItem is Kategoria k && k.Id != 0)
            talalat = talalat.Where(t => t.KategoriaId == k.Id);

        if (chkHiany.IsChecked == true)
            talalat = talalat.Where(t => t.Darab == 0);

        talalat = cmbRendezes.SelectedIndex switch
        {
            1 => talalat.OrderBy(t => t.Ar),
            2 => talalat.OrderByDescending(t => t.Ar),
            3 => talalat.OrderByDescending(t => t.Ertek),
            _ => talalat.OrderBy(t => t.Nev)
        };

        List<Termek> lista = talalat.ToList();
        dgTermekek.ItemsSource = lista;
        txtAllapot.Text = $"{lista.Count} találat, összérték: {lista.Sum(t => t.Ertek):N0} Ft";
    }
}
