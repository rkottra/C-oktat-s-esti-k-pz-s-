using System.Windows;
using ElemekWpf.Models;
using Microsoft.EntityFrameworkCore;

namespace ElemekWpf;

public partial class MainWindow : Window
{
    public const string Mind = "(mind)";
    public static readonly string[] Halmazallapotok = { "szilárd", "folyékony", "gáz" };

    private readonly ElemekContext db = new ElemekContext();
    private readonly Kategoria mindKategoria = new Kategoria { Id = 0, Nev = Mind };
    private bool kesz;   // amíg a szűrők feltöltése tart, ne fusson lekérdezés

    public MainWindow()
    {
        InitializeComponent();

        var kategoriak = new List<Kategoria> { mindKategoria };
        kategoriak.AddRange(db.Kategoriak.OrderBy(k => k.Nev));
        cbKategoria.ItemsSource = kategoriak;
        cbKategoria.SelectedIndex = 0;

        cbHalmaz.ItemsSource = new[] { Mind }.Concat(Halmazallapotok).ToList();
        cbHalmaz.SelectedIndex = 0;

        kesz = true;
        Betoltes();
    }

    private void Betoltes()
    {
        if (!kesz) return;

        IQueryable<Elem> lekerdezes = db.Elemek.Include(e => e.Kategoria);
        if (cbKategoria.SelectedItem is Kategoria k && k.Id != 0)
        {
            lekerdezes = lekerdezes.Where(e => e.KategoriaId == k.Id);
        }
        if (cbHalmaz.SelectedItem is string h && h != Mind)
        {
            lekerdezes = lekerdezes.Where(e => e.Halmazallapot == h);
        }

        List<Elem> lista = lekerdezes.OrderBy(e => e.Rendszam).ToList();
        dgElemek.ItemsSource = lista;
        txtAllapot.Text = $"Megjelenítve: {lista.Count} / {db.Elemek.Count()} elem";
    }

    private void Szuro_Valtozott(object sender, RoutedEventArgs e) => Betoltes();

    private void UjElem_Click(object sender, RoutedEventArgs e)
    {
        var ablak = new UjElemAblak(
            db.Kategoriak.OrderBy(k => k.Nev).ToList(),
            db.Elemek.Select(x => x.Rendszam).ToList(),
            db.Elemek.Select(x => x.Vegyjel).ToList())
        {
            Owner = this
        };

        if (ablak.ShowDialog() == true && ablak.UjElem is not null)
        {
            db.Elemek.Add(ablak.UjElem);
            db.SaveChanges();
            Betoltes();
        }
    }

    private void Torles_Click(object sender, RoutedEventArgs e)
    {
        if (dgElemek.SelectedItem is not Elem kijelolt)
        {
            MessageBox.Show("Előbb jelölj ki egy elemet!", "Törlés", MessageBoxButton.OK, MessageBoxImage.Information);
            return;
        }
        if (MessageBox.Show($"Biztosan törlöd: {kijelolt.Nev} ({kijelolt.Vegyjel})?", "Megerősítés",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
        {
            db.Elemek.Remove(kijelolt);
            db.SaveChanges();
            Betoltes();
        }
    }
}
