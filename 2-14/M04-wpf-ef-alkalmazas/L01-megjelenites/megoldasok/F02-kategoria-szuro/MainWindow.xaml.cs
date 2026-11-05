using System.Windows;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;
using RaktarSzuro.Models;

namespace RaktarSzuro;

public partial class MainWindow : Window
{
    private readonly RaktarContext db = new();

    public MainWindow()
    {
        InitializeComponent();
        db.Database.EnsureCreated();

        List<Kategoria> kategoriak = db.Kategoriak.OrderBy(k => k.Nev).ToList();
        cmbKategoria.ItemsSource = kategoriak;

        // Id = 0: "minden kategória" – ilyen Id-jű rekord az adatbázisban nincs.
        var szuroLista = new List<Kategoria> { new Kategoria { Id = 0, Nev = "(összes)" } };
        szuroLista.AddRange(kategoriak);
        cmbSzuro.ItemsSource = szuroLista;
        cmbSzuro.SelectedIndex = 0;
    }

    private void CmbSzuro_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (cmbSzuro.SelectedItem is not Kategoria szuro)
            return;

        IQueryable<Termek> lekerdezes = db.Termekek.Include(t => t.Kategoria);
        if (szuro.Id != 0)
            lekerdezes = lekerdezes.Where(t => t.KategoriaId == szuro.Id);

        List<Termek> termekek = lekerdezes.OrderBy(t => t.Nev).ToList();
        dgTermekek.ItemsSource = termekek;

        int hiany = termekek.Count(t => t.Darab == 0);
        txtOsszesites.Text = $"{termekek.Count} termék, összérték: {termekek.Sum(t => t.Ertek):N0} Ft, ebből {hiany} hiánycikk";
    }
}
