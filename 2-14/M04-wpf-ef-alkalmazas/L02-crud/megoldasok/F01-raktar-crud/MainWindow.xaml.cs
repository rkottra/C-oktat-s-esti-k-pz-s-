using System.Windows;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;
using RaktarCrud.Models;

namespace RaktarCrud;

public partial class MainWindow : Window
{
    private readonly RaktarContext db = new();

    public MainWindow()
    {
        InitializeComponent();
        db.Database.EnsureCreated();

        cmbKategoria.ItemsSource = db.Kategoriak.OrderBy(k => k.Nev).ToList();
        Frissites(null);
        UrlapUrites();
    }

    private void Frissites(Termek? kijelolendo)
    {
        List<Termek> termekek = db.Termekek.Include(t => t.Kategoria).OrderBy(t => t.Cikkszam).ToList();
        dgTermekek.ItemsSource = termekek;
        dgTermekek.SelectedItem = kijelolendo;
        txtAllapot.Text = $"{termekek.Count} termék";
    }

    private void DgTermekek_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (dgTermekek.SelectedItem is not Termek t)
            return;

        txtUrlapCim.Text = "Termék módosítása";
        txtCikkszam.Text = t.Cikkszam;
        txtNev.Text = t.Nev;
        txtAr.Text = t.Ar.ToString();
        txtDarab.Text = t.Darab.ToString();
        cmbKategoria.SelectedValue = t.KategoriaId;
        btnTorles.IsEnabled = true;
    }

    private void UrlapUrites()
    {
        dgTermekek.SelectedItem = null;
        txtUrlapCim.Text = "Új termék";
        txtCikkszam.Text = txtNev.Text = txtAr.Text = txtDarab.Text = "";
        cmbKategoria.SelectedIndex = -1;
        btnTorles.IsEnabled = false;
        txtCikkszam.Focus();
    }

    private void Uj_Click(object sender, RoutedEventArgs e) => UrlapUrites();

    private void Mentes_Click(object sender, RoutedEventArgs e)
    {
        if (!int.TryParse(txtAr.Text, out int ar) ||
            !int.TryParse(txtDarab.Text, out int darab) ||
            cmbKategoria.SelectedValue is not int kategoriaId)
        {
            MessageBox.Show("Az ár és a darabszám egész szám legyen, és válassz kategóriát!",
                "Hibás adat", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        // Kijelölt sor → módosítás, különben új termék.
        Termek termek;
        if (dgTermekek.SelectedItem is Termek kijelolt)
        {
            termek = kijelolt;
        }
        else
        {
            termek = new Termek();
            db.Termekek.Add(termek);
        }

        termek.Cikkszam = txtCikkszam.Text.Trim().ToUpper();
        termek.Nev = txtNev.Text.Trim();
        termek.Ar = ar;
        termek.Darab = darab;
        termek.KategoriaId = kategoriaId;
        db.SaveChanges();

        Frissites(termek);
        txtAllapot.Text = $"Mentve: {termek.Cikkszam} {termek.Nev}";
    }

    private void Torles_Click(object sender, RoutedEventArgs e)
    {
        if (dgTermekek.SelectedItem is not Termek termek)
            return;

        string figyelmeztetes = termek.Darab > 0
            ? $"\n\nFigyelem: még {termek.Darab} db van belőle raktáron!"
            : "";

        var valasz = MessageBox.Show(
            $"Biztosan törlöd: {termek.Cikkszam} {termek.Nev}?{figyelmeztetes}",
            "Törlés megerősítése", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
        if (valasz != MessageBoxResult.Yes)
            return;

        db.Termekek.Remove(termek);
        db.SaveChanges();

        UrlapUrites();
        Frissites(null);
        txtAllapot.Text = $"Törölve: {termek.Cikkszam} {termek.Nev}";
    }
}
