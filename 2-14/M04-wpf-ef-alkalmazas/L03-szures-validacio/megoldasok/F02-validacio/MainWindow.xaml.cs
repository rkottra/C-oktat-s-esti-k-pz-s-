using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.EntityFrameworkCore;
using RaktarValidacio.Models;

namespace RaktarValidacio;

public partial class MainWindow : Window
{
    private readonly RaktarContext db = new();
    private readonly Brush alapKeret;

    public MainWindow()
    {
        InitializeComponent();
        alapKeret = txtNev.BorderBrush;

        try
        {
            db.Database.EnsureCreated();
            cmbKategoria.ItemsSource = db.Kategoriak.OrderBy(k => k.Nev).ToList();
            Frissites(null);
            UrlapUrites();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Nem sikerült kapcsolódni az adatbázishoz. Fut a MySQL?\n\n" + ex.Message,
                "Adatbázishiba", MessageBoxButton.OK, MessageBoxImage.Error);
            pnlUrlap.IsEnabled = false;
            txtAllapot.Text = "Nincs adatbázis-kapcsolat.";
        }
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

        HibakTorlese();
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
        HibakTorlese();
        txtUrlapCim.Text = "Új termék";
        txtCikkszam.Text = txtNev.Text = txtAr.Text = txtDarab.Text = "";
        cmbKategoria.SelectedIndex = -1;
        btnTorles.IsEnabled = false;
    }

    private void Uj_Click(object sender, RoutedEventArgs e) => UrlapUrites();

    // ---------- Validáció ----------

    private void HibakTorlese()
    {
        foreach (Control c in new Control[] { txtCikkszam, txtNev, txtAr, txtDarab, cmbKategoria })
            c.BorderBrush = alapKeret;
        txtHibak.Text = "";
    }

    private static void Hiba(List<string> hibak, Control mezo, string uzenet)
    {
        mezo.BorderBrush = Brushes.Red;
        hibak.Add(uzenet);
    }

    private List<string> UrlapEllenorzese()
    {
        HibakTorlese();
        var hibak = new List<string>();

        string cikkszam = txtCikkszam.Text.Trim().ToUpper();
        string nev = txtNev.Text.Trim();

        // Cikkszám: két nagybetű, kötőjel, három számjegy (pl. KS-004)
        if (!Regex.IsMatch(cikkszam, @"^[A-Z]{2}-\d{3}$"))
        {
            Hiba(hibak, txtCikkszam, "A cikkszám formátuma: két betű, kötőjel, három számjegy (pl. KS-004).");
        }
        else
        {
            int sajatId = (dgTermekek.SelectedItem as Termek)?.Id ?? 0;
            if (db.Termekek.Any(t => t.Cikkszam == cikkszam && t.Id != sajatId))
                Hiba(hibak, txtCikkszam, $"A(z) {cikkszam} cikkszám már foglalt.");
        }

        if (nev.Length < 2 || nev.Length > 80)
            Hiba(hibak, txtNev, "A név 2–80 karakter hosszú legyen.");

        if (!int.TryParse(txtAr.Text, out int ar) || ar < 1 || ar > 10_000_000)
            Hiba(hibak, txtAr, "Az ár 1 és 10 000 000 Ft közötti egész szám legyen.");

        if (!int.TryParse(txtDarab.Text, out int darab) || darab < 0 || darab > 100_000)
            Hiba(hibak, txtDarab, "A darabszám 0 és 100 000 közötti egész szám legyen.");

        if (cmbKategoria.SelectedValue is not int)
            Hiba(hibak, cmbKategoria, "Válassz kategóriát!");

        return hibak;
    }

    // ---------- Mentés, törlés ----------

    private void Mentes_Click(object sender, RoutedEventArgs e)
    {
        List<string> hibak;
        try
        {
            hibak = UrlapEllenorzese();   // a foglalt cikkszám vizsgálata is az adatbázishoz fordul
        }
        catch (Exception ex)
        {
            MessageBox.Show("Az ellenőrzés közben hiba történt:\n" + ex.Message);
            return;
        }

        if (hibak.Count > 0)
        {
            txtHibak.Text = string.Join("\n", hibak);
            return;
        }

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
        termek.Ar = int.Parse(txtAr.Text);       // az ellenőrzés után biztosan szám
        termek.Darab = int.Parse(txtDarab.Text);
        termek.KategoriaId = (int)cmbKategoria.SelectedValue;

        if (Mentes())
        {
            Frissites(termek);
            txtAllapot.Text = $"Mentve: {termek.Cikkszam} {termek.Nev}";
        }
    }

    private void Torles_Click(object sender, RoutedEventArgs e)
    {
        if (dgTermekek.SelectedItem is not Termek termek)
            return;

        var valasz = MessageBox.Show($"Biztosan törlöd: {termek.Cikkszam} {termek.Nev}?",
            "Törlés megerősítése", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
        if (valasz != MessageBoxResult.Yes)
            return;

        db.Termekek.Remove(termek);
        if (Mentes())
        {
            UrlapUrites();
            Frissites(null);
            txtAllapot.Text = $"Törölve: {termek.Cikkszam}";
        }
    }

    private bool Mentes()
    {
        try
        {
            db.SaveChanges();
            return true;
        }
        catch (DbUpdateException ex)
        {
            MessageBox.Show("Az adatbázis elutasította a mentést:\n\n" + (ex.InnerException?.Message ?? ex.Message),
                "Mentési hiba", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Váratlan hiba:\n\n" + ex.Message, "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        // A sikertelen változásokat eldobjuk, és a valós állapotot töltjük vissza.
        db.ChangeTracker.Clear();
        try
        {
            UrlapUrites();
            Frissites(null);
        }
        catch (Exception)
        {
            txtAllapot.Text = "Az adatok újratöltése nem sikerült.";
        }
        return false;
    }
}
