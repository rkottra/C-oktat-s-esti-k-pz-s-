using System.Windows;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;
using RaktarKategoriak.Models;

namespace RaktarKategoriak;

public partial class MainWindow : Window
{
    private readonly RaktarContext db = new();

    public MainWindow()
    {
        InitializeComponent();
        db.Database.EnsureCreated();
        Frissites();
    }

    private void Frissites()
    {
        // Include: a ListBox a kategóriák termékszámát is kiírja.
        lbKategoriak.ItemsSource = db.Kategoriak.Include(k => k.Termekek).OrderBy(k => k.Nev).ToList();
        dgTermekek.ItemsSource = db.Termekek.Include(t => t.Kategoria).OrderBy(t => t.Cikkszam).ToList();
        btnAtnevezes.IsEnabled = btnTorles.IsEnabled = false;
    }

    private void LbKategoriak_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        bool vanKijelolt = lbKategoriak.SelectedItem is Kategoria;
        btnAtnevezes.IsEnabled = btnTorles.IsEnabled = vanKijelolt;
        if (lbKategoriak.SelectedItem is Kategoria k)
            txtNev.Text = k.Nev;
    }

    // Igaz, ha már van ilyen nevű kategória (átnevezéskor önmagát nem számítjuk).
    private bool LetezoNev(string nev, int kiveveId)
    {
        string kisbetus = nev.ToLower();
        return db.Kategoriak.Any(k => k.Nev.ToLower() == kisbetus && k.Id != kiveveId);
    }

    private void Felvetel_Click(object sender, RoutedEventArgs e)
    {
        string nev = txtNev.Text.Trim();
        if (nev == "")
        {
            MessageBox.Show("Add meg a kategória nevét!");
            return;
        }
        if (LetezoNev(nev, 0))
        {
            MessageBox.Show($"Már van „{nev}” nevű kategória.");
            return;
        }

        db.Kategoriak.Add(new Kategoria { Nev = nev });
        db.SaveChanges();
        Frissites();
        txtNev.Text = "";
        txtAllapot.Text = $"Új kategória: {nev}";
    }

    private void Atnevezes_Click(object sender, RoutedEventArgs e)
    {
        if (lbKategoriak.SelectedItem is not Kategoria kategoria)
            return;

        string ujNev = txtNev.Text.Trim();
        if (ujNev == "" || LetezoNev(ujNev, kategoria.Id))
        {
            MessageBox.Show("A név nem lehet üres, és nem egyezhet másik kategória nevével.");
            return;
        }

        string regiNev = kategoria.Nev;
        kategoria.Nev = ujNev;
        db.SaveChanges();
        Frissites();   // a Termékek fül Kategória oszlopa is az új nevet mutatja
        txtAllapot.Text = $"Átnevezve: {regiNev} → {ujNev}";
    }

    private void Torles_Click(object sender, RoutedEventArgs e)
    {
        if (lbKategoriak.SelectedItem is not Kategoria kategoria)
            return;

        // Ha a kategóriában van termék, nem töröljük – az EF alapbeállítással
        // a termékeit is törölné (kaszkádolt törlés)!
        if (kategoria.Termekek.Count > 0)
        {
            MessageBox.Show($"A(z) „{kategoria.Nev}” kategóriában {kategoria.Termekek.Count} termék van, ezért nem törölhető.",
                "Nem törölhető", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        var valasz = MessageBox.Show($"Biztosan törlöd a(z) „{kategoria.Nev}” kategóriát?",
            "Törlés megerősítése", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
        if (valasz != MessageBoxResult.Yes)
            return;

        db.Kategoriak.Remove(kategoria);
        db.SaveChanges();
        Frissites();
        txtNev.Text = "";
        txtAllapot.Text = $"Törölve: {kategoria.Nev}";
    }
}
