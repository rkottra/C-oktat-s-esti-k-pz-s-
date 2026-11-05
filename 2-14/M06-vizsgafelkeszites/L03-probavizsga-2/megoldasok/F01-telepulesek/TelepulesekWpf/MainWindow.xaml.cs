using System.Windows;
using Microsoft.EntityFrameworkCore;
using TelepulesekWpf.Models;

namespace TelepulesekWpf;

public partial class MainWindow : Window
{
    private readonly TelepulesekContext db = new TelepulesekContext();

    public MainWindow()
    {
        InitializeComponent();
        Betoltes();
    }

    private void Betoltes()
    {
        // Include nélkül a Megye navigációs tulajdonság null lenne, és üres maradna a Megye oszlop
        dgTelepulesek.ItemsSource = db.Telepulesek
            .Include(t => t.Megye)
            .OrderBy(t => t.Nev)
            .ToList();
    }

    private void UjFelvetel_Click(object sender, RoutedEventArgs e)
    {
        // A megyék listáját átadjuk a párbeszédablaknak a legördülő listához
        var ablak = new UjTelepulesAblak(db.Megyek.OrderBy(m => m.Nev).ToList())
        {
            Owner = this
        };

        // ShowDialog: modális ablak; true, ha a felhasználó sikeresen mentett
        if (ablak.ShowDialog() == true && ablak.UjTelepules is not null)
        {
            db.Telepulesek.Add(ablak.UjTelepules);
            db.SaveChanges();
            Betoltes();
            dgTelepulesek.SelectedItem = ablak.UjTelepules;
            dgTelepulesek.ScrollIntoView(ablak.UjTelepules);
        }
    }
}
