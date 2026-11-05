using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using KonyvesboltKereses.Models;
using Microsoft.EntityFrameworkCore;

namespace KonyvesboltKereses;

public partial class MainWindow : Window
{
    private readonly KonyvesboltContext db = new();

    // Az összes könyv a memóriában – a keresés ezen a listán fut, nem kérdezi le újra az adatbázist.
    private List<Konyv> osszesKonyv = new();

    // Az eredeti keretszín, hogy a piros jelölést vissza tudjuk állítani.
    private readonly Brush alapKeret;

    public MainWindow()
    {
        InitializeComponent();
        alapKeret = txtCim.BorderBrush;

        // Az adatbázis elérése bármikor elromolhat (pl. nem fut a MySQL) → try-catch.
        try
        {
            db.Database.EnsureCreated();

            List<Kiado> kiadok = db.Kiadok.OrderBy(k => k.Nev).ToList();
            cmbKiado.ItemsSource = kiadok;

            var szuroLista = new List<Kiado> { new Kiado { Id = 0, Nev = "(összes)" } };
            szuroLista.AddRange(kiadok);
            cmbSzuroKiado.ItemsSource = szuroLista;

            AdatokBetoltese();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Nem sikerült kapcsolódni az adatbázishoz.\nFut a MySQL a XAMPP-ban?\n\n" + ex.Message,
                "Adatbázishiba", MessageBoxButton.OK, MessageBoxImage.Error);
            pnlUrlap.IsEnabled = false;   // adatbázis nélkül nincs mit szerkeszteni
            txtAllapot.Text = "Nincs adatbázis-kapcsolat.";
            return;
        }

        cmbSzuroKiado.SelectedIndex = 0;  // → Szures()
        UrlapUrites();
    }

    // ---------- Betöltés és élő keresés ----------

    private void AdatokBetoltese()
    {
        osszesKonyv = db.Konyvek.Include(k => k.Kiado).ToList();
    }

    // A három szűrőmező (TextBox, ComboBox, CheckBox) ugyanezt az eseménykezelőt használja.
    private void Szuromezo_Valtozott(object sender, RoutedEventArgs e)
    {
        Szures(dgKonyvek.SelectedItem as Konyv);
    }

    private void Szures(Konyv? kijelolendo)
    {
        string keresett = txtKereses.Text.Trim();

        // LINQ a memóriabeli listán. A feltételeket egymás után fűzzük, csak ha kellenek.
        IEnumerable<Konyv> talalatok = osszesKonyv;

        if (keresett != "")
        {
            talalatok = talalatok.Where(k =>
                k.Cim.Contains(keresett, StringComparison.OrdinalIgnoreCase) ||
                k.Szerzo.Contains(keresett, StringComparison.OrdinalIgnoreCase));
        }

        if (cmbSzuroKiado.SelectedItem is Kiado kiado && kiado.Id != 0)
            talalatok = talalatok.Where(k => k.KiadoId == kiado.Id);

        if (chkCsakKeszleten.IsChecked == true)
            talalatok = talalatok.Where(k => k.Keszlet > 0);

        List<Konyv> lista = talalatok.OrderBy(k => k.Cim).ToList();
        dgKonyvek.ItemsSource = lista;

        // A kijelölést csak akkor tartjuk meg, ha a könyv benne maradt a szűrt listában;
        // ha kiesett, az űrlapot is ürítjük, nehogy egy nem látható könyvet módosítsunk.
        if (kijelolendo != null && lista.Contains(kijelolendo))
            dgKonyvek.SelectedItem = kijelolendo;
        else if (kijelolendo != null)
            UrlapUrites();
        txtAllapot.Text = $"{lista.Count} találat / {osszesKonyv.Count} könyv";
    }

    // ---------- Űrlap ----------

    private void DgKonyvek_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (dgKonyvek.SelectedItem is not Konyv k)
            return;

        HibakTorlese();
        txtUrlapCim.Text = "Kijelölt könyv módosítása";
        txtCim.Text = k.Cim;
        txtSzerzo.Text = k.Szerzo;
        txtEv.Text = k.KiadasEve.ToString();
        txtAr.Text = k.Ar.ToString();
        txtKeszlet.Text = k.Keszlet.ToString();
        cmbKiado.SelectedValue = k.KiadoId;
        btnTorles.IsEnabled = true;
    }

    private void UrlapUrites()
    {
        dgKonyvek.SelectedItem = null;
        HibakTorlese();
        txtUrlapCim.Text = "Új könyv";
        txtCim.Text = txtSzerzo.Text = txtEv.Text = txtAr.Text = txtKeszlet.Text = "";
        cmbKiado.SelectedIndex = -1;
        btnTorles.IsEnabled = false;
    }

    private void Uj_Click(object sender, RoutedEventArgs e) => UrlapUrites();

    // ---------- Validáció ----------

    private void HibakTorlese()
    {
        foreach (Control c in new Control[] { txtCim, txtSzerzo, txtEv, txtAr, txtKeszlet, cmbKiado })
            c.BorderBrush = alapKeret;
        txtHibak.Text = "";
    }

    // Megjelöli a hibás mezőt, és felírja az üzenetet a listára.
    private static void Hiba(List<string> hibak, Control mezo, string uzenet)
    {
        mezo.BorderBrush = Brushes.Red;
        hibak.Add(uzenet);
    }

    // Ellenőrzi az űrlapot. Hiba esetén null-t ad vissza (a hibák a listában),
    // különben egy új Konyv objektumot a beírt adatokkal.
    private Konyv? UrlapEllenorzese(out List<string> hibak)
    {
        HibakTorlese();
        hibak = new List<string>();

        string cim = txtCim.Text.Trim();
        string szerzo = txtSzerzo.Text.Trim();

        if (cim == "")
            Hiba(hibak, txtCim, "A cím nem lehet üres.");
        else if (cim.Length > 100)
            Hiba(hibak, txtCim, "A cím legfeljebb 100 karakter lehet.");

        if (szerzo == "")
            Hiba(hibak, txtSzerzo, "A szerző nem lehet üres.");
        else if (szerzo.Length > 60)
            Hiba(hibak, txtSzerzo, "A szerző neve legfeljebb 60 karakter lehet.");

        // A TryParse nem dob kivételt hibás szövegnél, csak false-t ad vissza.
        if (!int.TryParse(txtEv.Text, out int ev) || ev < 1450 || ev > DateTime.Now.Year)
            Hiba(hibak, txtEv, $"A kiadás éve 1450 és {DateTime.Now.Year} közötti egész szám legyen.");

        if (!int.TryParse(txtAr.Text, out int ar) || ar <= 0 || ar > 100_000)
            Hiba(hibak, txtAr, "Az ár 1 és 100 000 Ft közötti egész szám legyen.");

        if (!int.TryParse(txtKeszlet.Text, out int keszlet) || keszlet < 0)
            Hiba(hibak, txtKeszlet, "A készlet nem negatív egész szám legyen.");

        if (cmbKiado.SelectedValue is not int kiadoId)
        {
            Hiba(hibak, cmbKiado, "Válassz kiadót!");
            kiadoId = 0;
        }

        // Üzleti szabály: ugyanaz a cím + szerző páros nem szerepelhet kétszer.
        int sajatId = (dgKonyvek.SelectedItem as Konyv)?.Id ?? 0;
        if (osszesKonyv.Any(k => k.Id != sajatId &&
                                 k.Cim.Equals(cim, StringComparison.OrdinalIgnoreCase) &&
                                 k.Szerzo.Equals(szerzo, StringComparison.OrdinalIgnoreCase)))
            Hiba(hibak, txtCim, "Ez a könyv (cím + szerző) már szerepel az adatbázisban.");

        if (hibak.Count > 0)
            return null;

        return new Konyv { Cim = cim, Szerzo = szerzo, KiadasEve = ev, Ar = ar, Keszlet = keszlet, KiadoId = kiadoId };
    }

    // ---------- Mentés és törlés kivételkezeléssel ----------

    private void Mentes_Click(object sender, RoutedEventArgs e)
    {
        Konyv? adatok = UrlapEllenorzese(out List<string> hibak);
        if (adatok == null)
        {
            txtHibak.Text = string.Join("\n", hibak);
            return;   // hibás adattal az adatbázishoz sem nyúlunk
        }

        Konyv konyv;
        if (dgKonyvek.SelectedItem is Konyv kijelolt)
        {
            konyv = kijelolt;
        }
        else
        {
            konyv = new Konyv();
            db.Konyvek.Add(konyv);
        }

        konyv.Cim = adatok.Cim;
        konyv.Szerzo = adatok.Szerzo;
        konyv.KiadasEve = adatok.KiadasEve;
        konyv.Ar = adatok.Ar;
        konyv.Keszlet = adatok.Keszlet;
        konyv.KiadoId = adatok.KiadoId;

        if (Mentes())
        {
            AdatokBetoltese();
            Szures(konyv);
            txtAllapot.Text = $"Mentve: {konyv.Cim}";
        }
    }

    private void Torles_Click(object sender, RoutedEventArgs e)
    {
        if (dgKonyvek.SelectedItem is not Konyv konyv)
            return;

        var valasz = MessageBox.Show($"Biztosan törlöd?\n\n{konyv.Cim} – {konyv.Szerzo}",
            "Törlés megerősítése", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
        if (valasz != MessageBoxResult.Yes)
            return;

        db.Konyvek.Remove(konyv);
        if (Mentes())
        {
            AdatokBetoltese();
            UrlapUrites();
            Szures(null);
            txtAllapot.Text = $"Törölve: {konyv.Cim}";
        }
    }

    // SaveChanges hibakezeléssel. Igazat ad vissza, ha sikerült.
    private bool Mentes()
    {
        try
        {
            db.SaveChanges();
            return true;
        }
        catch (DbUpdateException ex)
        {
            // Az adatbázis elutasította a műveletet (pl. túl hosszú szöveg, megsértett kulcs).
            // A valódi ok általában az InnerException-ben van.
            MessageBox.Show("Az adatbázis nem fogadta el a módosítást:\n\n" + (ex.InnerException?.Message ?? ex.Message),
                "Mentési hiba", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        catch (Exception ex)
        {
            // Bármi más (pl. közben leállt a MySQL).
            MessageBox.Show("Váratlan hiba mentés közben:\n\n" + ex.Message,
                "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        // Sikertelen mentés után a DbContext még "emlékszik" a félkész változásokra.
        // Eldobjuk őket, és újratöltünk mindent az adatbázisból, hogy a felület a valós állapotot mutassa.
        db.ChangeTracker.Clear();
        try
        {
            AdatokBetoltese();
            UrlapUrites();
            Szures(null);
        }
        catch (Exception)
        {
            txtAllapot.Text = "Az adatok újratöltése nem sikerült.";
        }
        return false;
    }
}
