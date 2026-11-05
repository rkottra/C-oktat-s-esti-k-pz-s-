using System.IO;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Serialization;
using KonyvesboltImportExport.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;

namespace KonyvesboltImportExport;

public partial class MainWindow : Window
{
    private readonly KonyvesboltContext db = new();

    public MainWindow()
    {
        InitializeComponent();
        try
        {
            db.Database.EnsureCreated();
            KiadokBetoltese();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Az adatbázis nem érhető el:\n" + ex.Message, "Hiba",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    // ---------- Megjelenítés ----------

    private void KiadokBetoltese()
    {
        var lista = new List<Kiado> { new Kiado { Id = 0, Nev = "(összes)" } };
        lista.AddRange(db.Kiadok.OrderBy(k => k.Nev));
        cmbSzuroKiado.ItemsSource = lista;
        cmbSzuroKiado.SelectedIndex = 0;   // → Frissites()
    }

    private void CmbSzuroKiado_SelectionChanged(object sender, SelectionChangedEventArgs e) => Frissites();

    private void Frissites()
    {
        IQueryable<Konyv> lekerdezes = db.Konyvek.Include(k => k.Kiado);
        if (cmbSzuroKiado.SelectedItem is Kiado kiado && kiado.Id != 0)
            lekerdezes = lekerdezes.Where(k => k.KiadoId == kiado.Id);

        List<Konyv> konyvek = lekerdezes.OrderBy(k => k.Cim).ToList();
        dgKonyvek.ItemsSource = konyvek;
        txtAllapot.Text = $"{konyvek.Count} könyv látható";
    }

    // ---------- CSV-import ----------

    private void CsvImport_Click(object sender, RoutedEventArgs e)
    {
        var dialogus = new OpenFileDialog
        {
            Title = "CSV-fájl kiválasztása",
            Filter = "CSV-fájl (*.csv)|*.csv|Minden fájl (*.*)|*.*",
            InitialDirectory = AppContext.BaseDirectory   // ide másolódik a minta konyvek-import.csv
        };
        if (dialogus.ShowDialog() != true)
            return;

        string[] sorok;
        try
        {
            sorok = File.ReadAllLines(dialogus.FileName, Encoding.UTF8);
        }
        catch (IOException ex)   // pl. Excelben nyitva van a fájl
        {
            MessageBox.Show("A fájl nem olvasható:\n" + ex.Message, "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        // A meglévő adatok a memóriába: így soronként nem kell az adatbázishoz fordulni.
        List<Kiado> kiadok = db.Kiadok.ToList();
        List<Konyv> meglevok = db.Konyvek.ToList();

        var ujKonyvek = new List<Konyv>();
        int hibasSor = 0, marLetezik = 0, ujKiado = 0;

        // Skip(1): az első sor a fejléc
        foreach (string sor in sorok.Skip(1))
        {
            if (string.IsNullOrWhiteSpace(sor))
                continue;

            string[] m = sor.Split(';');
            if (m.Length != 6 ||
                !int.TryParse(m[2], out int ev) ||
                !int.TryParse(m[3], out int ar) ||
                !int.TryParse(m[4], out int keszlet))
            {
                hibasSor++;            // a hibás sort kihagyjuk, a többit attól még beolvassuk
                continue;
            }

            string cim = m[0].Trim();
            string szerzo = m[1].Trim();
            string kiadoNev = m[5].Trim();

            // Duplikátum: már az adatbázisban van, vagy ebben a fájlban korábban szerepelt
            bool duplikalt = meglevok.Concat(ujKonyvek).Any(k =>
                k.Cim.Equals(cim, StringComparison.OrdinalIgnoreCase) &&
                k.Szerzo.Equals(szerzo, StringComparison.OrdinalIgnoreCase));
            if (duplikalt)
            {
                marLetezik++;
                continue;
            }

            // Kapcsolt tábla: a kiadót név alapján keressük; ha nincs, újat hozunk létre.
            Kiado? kiado = kiadok.FirstOrDefault(k => k.Nev.Equals(kiadoNev, StringComparison.OrdinalIgnoreCase));
            if (kiado == null)
            {
                kiado = new Kiado { Nev = kiadoNev };
                kiadok.Add(kiado);     // a következő sor már megtalálja
                ujKiado++;
            }

            // Nem KiadoId-t, hanem a Kiado objektumot adjuk meg: az új kiadónak még nincs Id-je,
            // az EF a mentéskor előbb a kiadót szúrja be, majd annak Id-jével a könyvet.
            ujKonyvek.Add(new Konyv
            {
                Cim = cim, Szerzo = szerzo, KiadasEve = ev, Ar = ar, Keszlet = keszlet, Kiado = kiado
            });
        }

        try
        {
            db.Konyvek.AddRange(ujKonyvek);   // egyetlen hívással az összes új könyv
            db.SaveChanges();                 // egyetlen mentés (egy tranzakció)
        }
        catch (DbUpdateException ex)
        {
            db.ChangeTracker.Clear();         // a félbemaradt import ne maradjon a contextben
            MessageBox.Show("Az import nem sikerült, semmi nem került mentésre:\n" + (ex.InnerException?.Message ?? ex.Message),
                "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        KiadokBetoltese();   // új kiadó is jöhetett
        txtAllapot.Text = $"Import kész: {ujKonyvek.Count} új könyv, {ujKiado} új kiadó, " +
                          $"{marLetezik} már létezett, {hibasSor} hibás sor kihagyva.";
    }

    // ---------- Export ----------

    // A táblázatban éppen látható könyvek "lapos" másolata
    private List<KonyvExport> ExportLista()
    {
        var konyvek = dgKonyvek.ItemsSource as List<Konyv> ?? new List<Konyv>();
        return konyvek.Select(k => new KonyvExport
        {
            Cim = k.Cim,
            Szerzo = k.Szerzo,
            KiadasEve = k.KiadasEve,
            Ar = k.Ar,
            Keszlet = k.Keszlet,
            Kiado = k.Kiado?.Nev ?? ""
        }).ToList();
    }

    private static string? MentesHelye(string szuro, string alapNev)
    {
        var dialogus = new SaveFileDialog { Filter = szuro, FileName = alapNev };
        return dialogus.ShowDialog() == true ? dialogus.FileName : null;
    }

    private void JsonExport_Click(object sender, RoutedEventArgs e)
    {
        string? fajl = MentesHelye("JSON-fájl (*.json)|*.json", "konyvek.json");
        if (fajl == null)
            return;

        var beallitasok = new JsonSerializerOptions
        {
            WriteIndented = true,                                // olvasható, tördelt kimenet
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All) // az ékezetek ne é alakban legyenek
        };

        try
        {
            List<KonyvExport> lista = ExportLista();
            File.WriteAllText(fajl, JsonSerializer.Serialize(lista, beallitasok), Encoding.UTF8);
            txtAllapot.Text = $"{lista.Count} könyv exportálva: {fajl}";
        }
        catch (Exception ex)
        {
            MessageBox.Show("Az export nem sikerült:\n" + ex.Message, "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void XmlExport_Click(object sender, RoutedEventArgs e)
    {
        string? fajl = MentesHelye("XML-fájl (*.xml)|*.xml", "konyvek.xml");
        if (fajl == null)
            return;

        try
        {
            List<KonyvExport> lista = ExportLista();

            // A gyökérelem neve <Konyvek> legyen (alapból <ArrayOfKonyv> lenne).
            var szerializalo = new XmlSerializer(typeof(List<KonyvExport>), new XmlRootAttribute("Konyvek"));
            using (var iro = new StreamWriter(fajl, false, Encoding.UTF8))
            {
                szerializalo.Serialize(iro, lista);
            }   // a using végén a fájl lezáródik
            txtAllapot.Text = $"{lista.Count} könyv exportálva: {fajl}";
        }
        catch (Exception ex)
        {
            MessageBox.Show("Az export nem sikerült:\n" + ex.Message, "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
