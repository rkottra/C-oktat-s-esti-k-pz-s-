using System.Windows;
using System.Windows.Controls;
using KonyvesboltMegjelenites.Models;
using Microsoft.EntityFrameworkCore;

namespace KonyvesboltMegjelenites;

public partial class MainWindow : Window
{
    // Egy ablak = egy DbContext, amíg az ablak nyitva van.
    private readonly KonyvesboltContext db = new();

    public MainWindow()
    {
        InitializeComponent();

        // Ha még nincs adatbázis, létrehozza a táblákat és a kezdő adatokat.
        db.Database.EnsureCreated();

        // A kiadók listája a részletek-panel ComboBoxába
        List<Kiado> kiadok = db.Kiadok.OrderBy(k => k.Nev).ToList();
        cmbKiado.ItemsSource = kiadok;

        // A szűrő ComboBox első eleme egy "álkiadó": Id = 0 jelenti, hogy nincs szűrés.
        var szuroLista = new List<Kiado> { new Kiado { Id = 0, Nev = "(összes kiadó)" } };
        szuroLista.AddRange(kiadok);
        cmbSzuroKiado.ItemsSource = szuroLista;
        cmbSzuroKiado.SelectedIndex = 0;   // ez kiváltja a SelectionChanged eseményt → betöltés
    }

    private void CmbSzuroKiado_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        Betoltes();
    }

    private void Betoltes()
    {
        if (cmbSzuroKiado.SelectedItem is not Kiado szuro)
            return;

        // Include nélkül a Kiado navigációs tulajdonság null lenne → üres "Kiadó" oszlop!
        IQueryable<Konyv> lekerdezes = db.Konyvek.Include(k => k.Kiado);

        if (szuro.Id != 0)
            lekerdezes = lekerdezes.Where(k => k.KiadoId == szuro.Id);

        // A ToList() futtatja le az SQL-t; a DataGrid már a memóriabeli listát kapja.
        List<Konyv> konyvek = lekerdezes.OrderBy(k => k.Cim).ToList();
        dgKonyvek.ItemsSource = konyvek;

        int darab = konyvek.Sum(k => k.Keszlet);
        int ertek = konyvek.Sum(k => k.Keszlet * k.Ar);
        txtOsszesites.Text = $"{konyvek.Count} cím, {darab} példány készleten, készletérték: {ertek:N0} Ft";
    }
}
