using System.IO;
using System.Text;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using RaktarImport.Models;

namespace RaktarImport;

public partial class MainWindow : Window
{
    private readonly RaktarContext db = new();

    public MainWindow()
    {
        InitializeComponent();
        try
        {
            db.Database.EnsureCreated();
            Frissites();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Az adatbázis nem érhető el:\n" + ex.Message);
        }
    }

    private void Frissites()
    {
        dgTermekek.ItemsSource = db.Termekek.Include(t => t.Kategoria).OrderBy(t => t.Cikkszam).ToList();
    }

    private void Naplo(string sor) => txtNaplo.AppendText(sor + Environment.NewLine);

    private void Import_Click(object sender, RoutedEventArgs e)
    {
        var dialogus = new OpenFileDialog
        {
            Filter = "CSV-fájl (*.csv)|*.csv",
            InitialDirectory = AppContext.BaseDirectory
        };
        if (dialogus.ShowDialog() != true)
            return;

        string[] sorok;
        try
        {
            sorok = File.ReadAllLines(dialogus.FileName, Encoding.UTF8);
        }
        catch (IOException ex)
        {
            MessageBox.Show("A fájl nem olvasható:\n" + ex.Message);
            return;
        }

        txtNaplo.Clear();

        // Cikkszám → termék szótár: a meglévők és az ebben a fájlban újonnan felvettek is benne lesznek.
        Dictionary<string, Termek> termekek = db.Termekek.ToDictionary(t => t.Cikkszam);
        List<Kategoria> kategoriak = db.Kategoriak.ToList();

        var ujTermekek = new List<Termek>();
        int bevetelezve = 0, hibas = 0;

        for (int i = 1; i < sorok.Length; i++)   // i = 0 a fejléc
        {
            if (string.IsNullOrWhiteSpace(sorok[i]))
                continue;

            string[] m = sorok[i].Split(';');
            if (m.Length != 5 || !int.TryParse(m[3], out int ar) || !int.TryParse(m[4], out int darab) || darab < 0)
            {
                hibas++;
                Naplo($"{i + 1}. sor hibás, kihagyva: {sorok[i]}");
                continue;
            }

            string cikkszam = m[0].Trim().ToUpper();

            // Létező cikkszám → csak a készlet nő (bevételezés).
            if (termekek.TryGetValue(cikkszam, out Termek? meglevo))
            {
                meglevo.Darab += darab;
                bevetelezve++;
                Naplo($"{cikkszam}: készlet +{darab} → {meglevo.Darab} db");
                continue;
            }

            string kategoriaNev = m[2].Trim();
            Kategoria? kategoria = kategoriak.FirstOrDefault(k => k.Nev.Equals(kategoriaNev, StringComparison.OrdinalIgnoreCase));
            if (kategoria == null)
            {
                kategoria = new Kategoria { Nev = kategoriaNev };
                kategoriak.Add(kategoria);
                Naplo($"Új kategória: {kategoriaNev}");
            }

            var uj = new Termek { Cikkszam = cikkszam, Nev = m[1].Trim(), Ar = ar, Darab = darab, Kategoria = kategoria };
            ujTermekek.Add(uj);
            termekek.Add(cikkszam, uj);
            Naplo($"{cikkszam}: új termék ({uj.Nev})");
        }

        try
        {
            db.Termekek.AddRange(ujTermekek);   // az új termékek egyszerre
            db.SaveChanges();                   // + a módosított darabszámok egy mentéssel
        }
        catch (DbUpdateException ex)
        {
            db.ChangeTracker.Clear();
            MessageBox.Show("A mentés nem sikerült, semmi nem változott:\n" + (ex.InnerException?.Message ?? ex.Message));
            Frissites();
            return;
        }

        Frissites();
        Naplo($"--- Kész: {ujTermekek.Count} új termék, {bevetelezve} bevételezés, {hibas} hibás sor.");
    }
}
