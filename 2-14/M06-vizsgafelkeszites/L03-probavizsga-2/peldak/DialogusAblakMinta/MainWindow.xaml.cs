using System.Collections.ObjectModel;
using System.Windows;
using DialogusAblakMinta.Models;

namespace DialogusAblakMinta;

// Adatbázis nélküli minta: a listák a memóriában vannak, hogy csak a párbeszédablakra figyeljünk.
// A vizsgán ugyanez a szerkezet, csak a listák a DbContext-ből jönnek, és mentéskor SaveChanges() kell.
public partial class MainWindow : Window
{
    private readonly List<Mufaj> mufajok = new List<Mufaj>
    {
        new Mufaj { Id = 1, Nev = "regény" },
        new Mufaj { Id = 2, Nev = "elbeszélő költemény" },
        new Mufaj { Id = 3, Nev = "dráma" }
    };

    private readonly ObservableCollection<Konyv> konyvek = new ObservableCollection<Konyv>();

    public MainWindow()
    {
        InitializeComponent();
        Felvesz("Egri csillagok", "Gárdonyi Géza", 1, 1901);
        Felvesz("A Pál utcai fiúk", "Molnár Ferenc", 1, 1907);
        Felvesz("Toldi", "Arany János", 2, 1847);
        Felvesz("Az ember tragédiája", "Madách Imre", 3, 1861);
        Felvesz("Bánk bán", "Katona József", 3, 1820);
        dgKonyvek.ItemsSource = konyvek;
    }

    private void Felvesz(string cim, string szerzo, int mufajId, int ev)
    {
        konyvek.Add(new Konyv
        {
            Cim = cim,
            Szerzo = szerzo,
            MufajId = mufajId,
            Mufaj = mufajok.First(m => m.Id == mufajId),   // EF-ben ezt az Include végezné
            KiadasEve = ev
        });
    }

    private void UjKonyv_Click(object sender, RoutedEventArgs e)
    {
        // 1. A párbeszédablak megkapja, amire szüksége van (a műfajok listáját)
        var ablak = new UjKonyvAblak(mufajok) { Owner = this };

        // 2. ShowDialog: a kód itt megáll, amíg az ablak be nem zárul.
        //    Az eredmény true, ha az ablak DialogResult = true-val zárult.
        if (ablak.ShowDialog() == true && ablak.Eredmeny is not null)
        {
            // 3. Az ellenőrzött új objektumot a főablak veszi át és menti
            konyvek.Add(ablak.Eredmeny);
        }
    }
}
