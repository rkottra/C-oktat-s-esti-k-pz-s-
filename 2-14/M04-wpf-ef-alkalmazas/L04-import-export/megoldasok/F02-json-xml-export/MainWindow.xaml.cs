using System.IO;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using RaktarExport.Models;

namespace RaktarExport;

public partial class MainWindow : Window
{
    private readonly RaktarContext db = new();

    // Közös JSON-beállítások íráshoz és olvasáshoz
    private static readonly JsonSerializerOptions JsonBeallitasok = new()
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,    // "cikkszam", "nev", ... kisbetűvel
        Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)  // olvasható ékezetek
    };

    public MainWindow()
    {
        InitializeComponent();
        try
        {
            db.Database.EnsureCreated();
            var lista = new List<Kategoria> { new Kategoria { Id = 0, Nev = "(összes)" } };
            lista.AddRange(db.Kategoriak.OrderBy(k => k.Nev));
            cmbKategoria.ItemsSource = lista;
            cmbKategoria.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            MessageBox.Show("Az adatbázis nem érhető el:\n" + ex.Message);
        }
    }

    private void CmbKategoria_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        IQueryable<Termek> q = db.Termekek.Include(t => t.Kategoria);
        if (cmbKategoria.SelectedItem is Kategoria k && k.Id != 0)
            q = q.Where(t => t.KategoriaId == k.Id);
        dgTermekek.ItemsSource = q.OrderBy(t => t.Cikkszam).ToList();
    }

    private List<TermekExport> ExportLista()
    {
        var termekek = dgTermekek.ItemsSource as List<Termek> ?? new List<Termek>();
        return termekek.Select(t => new TermekExport
        {
            Cikkszam = t.Cikkszam,
            Nev = t.Nev,
            Kategoria = t.Kategoria?.Nev ?? "",
            Ar = t.Ar,
            Darab = t.Darab
        }).ToList();
    }

    // Fájlnév-javaslat a kiválasztott kategóriából, pl. termekek-festek.json
    private string AlapFajlnev(string kiterjesztes)
    {
        string kategoria = cmbKategoria.SelectedItem is Kategoria k && k.Id != 0 ? "-" + k.Nev.ToLower() : "";
        return $"termekek{kategoria}.{kiterjesztes}".Replace(' ', '-');
    }

    private void JsonExport_Click(object sender, RoutedEventArgs e)
    {
        var dialogus = new SaveFileDialog { Filter = "JSON-fájl (*.json)|*.json", FileName = AlapFajlnev("json") };
        if (dialogus.ShowDialog() != true)
            return;

        try
        {
            List<TermekExport> lista = ExportLista();
            File.WriteAllText(dialogus.FileName, JsonSerializer.Serialize(lista, JsonBeallitasok), Encoding.UTF8);
            txtAllapot.Text = $"{lista.Count} termék mentve: {dialogus.FileName}";
        }
        catch (Exception ex)
        {
            MessageBox.Show("Az export nem sikerült:\n" + ex.Message);
        }
    }

    private void XmlExport_Click(object sender, RoutedEventArgs e)
    {
        var dialogus = new SaveFileDialog { Filter = "XML-fájl (*.xml)|*.xml", FileName = AlapFajlnev("xml") };
        if (dialogus.ShowDialog() != true)
            return;

        try
        {
            List<TermekExport> lista = ExportLista();
            var xs = new XmlSerializer(typeof(List<TermekExport>), new XmlRootAttribute("Termekek"));
            using var iro = new StreamWriter(dialogus.FileName, false, Encoding.UTF8);
            xs.Serialize(iro, lista);
            txtAllapot.Text = $"{lista.Count} termék mentve: {dialogus.FileName}";
        }
        catch (Exception ex)
        {
            MessageBox.Show("Az export nem sikerült:\n" + ex.Message);
        }
    }

    // Visszaolvasás: a JSON-ból ugyanilyen objektumlista lesz (deszerializálás).
    private void JsonBeolvasas_Click(object sender, RoutedEventArgs e)
    {
        var dialogus = new OpenFileDialog { Filter = "JSON-fájl (*.json)|*.json" };
        if (dialogus.ShowDialog() != true)
            return;

        try
        {
            string json = File.ReadAllText(dialogus.FileName, Encoding.UTF8);
            List<TermekExport> lista = JsonSerializer.Deserialize<List<TermekExport>>(json, JsonBeallitasok) ?? new();

            int osszDarab = lista.Sum(t => t.Darab);
            long osszErtek = lista.Sum(t => (long)t.Ar * t.Darab);
            MessageBox.Show($"A fájlban {lista.Count} termék van.\nÖsszesen {osszDarab} db, {osszErtek:N0} Ft értékben.",
                "JSON-ellenőrzés");
        }
        catch (JsonException ex)   // nem érvényes JSON, vagy nem ilyen szerkezetű
        {
            MessageBox.Show("A fájl nem a várt JSON-formátumú:\n" + ex.Message);
        }
        catch (IOException ex)
        {
            MessageBox.Show("A fájl nem olvasható:\n" + ex.Message);
        }
    }
}
