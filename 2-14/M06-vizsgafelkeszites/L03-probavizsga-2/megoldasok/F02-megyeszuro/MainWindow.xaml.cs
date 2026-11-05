using System.Windows;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;
using TelepulesekSzures.Models;

namespace TelepulesekSzures;

public partial class MainWindow : Window
{
    private readonly TelepulesekContext db = new TelepulesekContext();

    // Álmegye a lista elején: Id = 0 jelenti, hogy nincs szűrés
    private readonly Megye osszes = new Megye { Id = 0, Nev = "(összes megye)" };

    public MainWindow()
    {
        InitializeComponent();
        var lista = new List<Megye> { osszes };
        lista.AddRange(db.Megyek.OrderBy(m => m.Nev));
        cbSzuro.ItemsSource = lista;
        cbSzuro.SelectedIndex = 0;   // ez kiváltja a SelectionChanged-et → Betoltes()
    }

    private void Betoltes()
    {
        int megyeId = (cbSzuro.SelectedItem as Megye)?.Id ?? 0;

        IQueryable<Telepules> lekerdezes = db.Telepulesek.Include(t => t.Megye);
        if (megyeId != 0)
        {
            lekerdezes = lekerdezes.Where(t => t.MegyeId == megyeId);
        }
        List<Telepules> varosok = lekerdezes.OrderBy(t => t.Nev).ToList();
        dgTelepulesek.ItemsSource = varosok;

        // Összesítés a megjelenített sorokra
        int lakos = varosok.Sum(t => t.Nepesseg);
        double terulet = varosok.Sum(t => t.Terulet);
        double suruseg = terulet > 0 ? lakos / terulet : 0;
        txtOsszesito.Text = $"{varosok.Count} város, {lakos:N0} lakos, átlagos népsűrűség: {suruseg:F0} fő/km²";
    }

    private void Szuro_SelectionChanged(object sender, SelectionChangedEventArgs e) => Betoltes();

    private void UjFelvetel_Click(object sender, RoutedEventArgs e)
    {
        var ablak = new UjTelepulesAblak(db.Megyek.OrderBy(m => m.Nev).ToList()) { Owner = this };
        if (ablak.ShowDialog() == true && ablak.UjTelepules is not null)
        {
            db.Telepulesek.Add(ablak.UjTelepules);
            db.SaveChanges();
            Betoltes();   // a szűrő megmarad; ha az új város más megyében van, nem látszik
        }
    }
}
