using System.Windows;
using System.Windows.Controls;
using KonyvesboltCrud.Models;
using Microsoft.EntityFrameworkCore;

namespace KonyvesboltCrud;

public partial class MainWindow : Window
{
    private readonly KonyvesboltContext db = new();

    public MainWindow()
    {
        InitializeComponent();
        db.Database.EnsureCreated();

        cmbKiado.ItemsSource = db.Kiadok.OrderBy(k => k.Nev).ToList();
        Frissites(null);
        UrlapUrites();
    }

    // ---------- Megjelenítés ----------

    // Újratölti a táblázatot, és ha megadtuk, újra kijelöli az adott könyvet.
    private void Frissites(Konyv? kijelolendo)
    {
        List<Konyv> konyvek = db.Konyvek.Include(k => k.Kiado).OrderBy(k => k.Cim).ToList();
        dgKonyvek.ItemsSource = konyvek;
        dgKonyvek.SelectedItem = kijelolendo;
        txtAllapot.Text = $"{konyvek.Count} könyv az adatbázisban";
    }

    // Kijelölés → az űrlap a kijelölt könyv adatait mutatja.
    private void DgKonyvek_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (dgKonyvek.SelectedItem is Konyv k)
        {
            txtUrlapCim.Text = "Kijelölt könyv módosítása";
            txtCim.Text = k.Cim;
            txtSzerzo.Text = k.Szerzo;
            txtEv.Text = k.KiadasEve.ToString();
            txtAr.Text = k.Ar.ToString();
            txtKeszlet.Text = k.Keszlet.ToString();
            cmbKiado.SelectedValue = k.KiadoId;
            btnTorles.IsEnabled = true;
        }
    }

    private void UrlapUrites()
    {
        dgKonyvek.SelectedItem = null;
        txtUrlapCim.Text = "Új könyv";
        txtCim.Text = txtSzerzo.Text = txtEv.Text = txtAr.Text = txtKeszlet.Text = "";
        cmbKiado.SelectedIndex = -1;
        btnTorles.IsEnabled = false;   // nincs mit törölni
        txtCim.Focus();
    }

    // ---------- Gombok ----------

    private void Uj_Click(object sender, RoutedEventArgs e)
    {
        UrlapUrites();
    }

    private void Mentes_Click(object sender, RoutedEventArgs e)
    {
        // Minimális ellenőrzés – a teljes validáció a következő lecke (14-M04-L03) témája.
        if (!int.TryParse(txtEv.Text, out int ev) ||
            !int.TryParse(txtAr.Text, out int ar) ||
            !int.TryParse(txtKeszlet.Text, out int keszlet) ||
            cmbKiado.SelectedValue is not int kiadoId)
        {
            MessageBox.Show("Az év, az ár és a készlet egész szám legyen, és válassz kiadót!",
                "Hibás adat", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        // Ha van kijelölt sor → MÓDOSÍTÁS, ha nincs → FELVÉTEL.
        Konyv konyv;
        if (dgKonyvek.SelectedItem is Konyv kijelolt)
        {
            konyv = kijelolt;               // a betöltött entitást írjuk át, az EF követi a változást
        }
        else
        {
            konyv = new Konyv();
            db.Konyvek.Add(konyv);          // új entitás: Added állapotba kerül
        }

        konyv.Cim = txtCim.Text.Trim();
        konyv.Szerzo = txtSzerzo.Text.Trim();
        konyv.KiadasEve = ev;
        konyv.Ar = ar;
        konyv.Keszlet = keszlet;
        konyv.KiadoId = kiadoId;

        db.SaveChanges();                   // INSERT vagy UPDATE; új sornál itt kap Id-t

        // A navigációs tulajdonságot (Kiado) is frissíteni kell, ezért újratöltjük a listát.
        Frissites(konyv);
        txtAllapot.Text = $"Mentve: {konyv.Cim} (Id = {konyv.Id})";
    }

    private void Torles_Click(object sender, RoutedEventArgs e)
    {
        if (dgKonyvek.SelectedItem is not Konyv konyv)
            return;

        // Törlés előtt mindig kérjünk megerősítést!
        MessageBoxResult valasz = MessageBox.Show(
            $"Biztosan törlöd ezt a könyvet?\n\n{konyv.Cim} – {konyv.Szerzo}",
            "Törlés megerősítése", MessageBoxButton.YesNo, MessageBoxImage.Question,
            MessageBoxResult.No);   // az alapértelmezett gomb a "Nem"

        if (valasz != MessageBoxResult.Yes)
            return;

        db.Konyvek.Remove(konyv);
        db.SaveChanges();

        UrlapUrites();
        Frissites(null);
        txtAllapot.Text = $"Törölve: {konyv.Cim}";
    }
}
