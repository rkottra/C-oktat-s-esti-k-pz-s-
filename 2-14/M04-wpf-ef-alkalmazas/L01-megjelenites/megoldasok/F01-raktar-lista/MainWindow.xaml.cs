using System.Windows;
using Microsoft.EntityFrameworkCore;
using RaktarLista.Models;

namespace RaktarLista;

public partial class MainWindow : Window
{
    private readonly RaktarContext db = new();

    public MainWindow()
    {
        InitializeComponent();
        db.Database.EnsureCreated();

        List<Termek> termekek = db.Termekek
            .Include(t => t.Kategoria)
            .OrderBy(t => t.Nev)
            .ToList();

        dgTermekek.ItemsSource = termekek;

        // Az Ertek nem adatbázis-oszlop, ezért csak a ToList() után (memóriában) összegezhető.
        txtOsszesites.Text = $"{termekek.Count} termék, összérték: {termekek.Sum(t => t.Ertek):N0} Ft";
    }
}
