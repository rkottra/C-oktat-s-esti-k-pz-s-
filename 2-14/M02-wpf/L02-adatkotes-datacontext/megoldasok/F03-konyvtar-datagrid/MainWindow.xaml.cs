using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace KonyvtarDataGrid;

// Az ablak maga is INotifyPropertyChanged: a saját tulajdonságai (Kivalasztott, KolcsonzottDb)
// változásáról is értesíti a felületet.
public partial class MainWindow : Window, INotifyPropertyChanged
{
    public ObservableCollection<Konyv> Konyvek { get; } = new();

    private Konyv? kivalasztott;
    public Konyv? Kivalasztott
    {
        get => kivalasztott;
        set { kivalasztott = value; Valtozott(nameof(Kivalasztott)); }
    }

    public int KolcsonzottDb => Konyvek.Count(k => k.Kolcsonozve);

    public event PropertyChangedEventHandler? PropertyChanged;

    private void Valtozott(string tulajdonsagNev) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(tulajdonsagNev));

    public MainWindow()
    {
        InitializeComponent();

        UjKonyvFelvetele(new Konyv { Cim = "Egri csillagok", Szerzo = "Gárdonyi Géza", KiadasEve = 1901 });
        UjKonyvFelvetele(new Konyv { Cim = "A Pál utcai fiúk", Szerzo = "Molnár Ferenc", KiadasEve = 1907, Kolcsonozve = true });
        UjKonyvFelvetele(new Konyv { Cim = "Tüskevár", Szerzo = "Fekete István", KiadasEve = 1957 });
        UjKonyvFelvetele(new Konyv { Cim = "Abigél", Szerzo = "Szabó Magda", KiadasEve = 1970, Kolcsonozve = true });
        UjKonyvFelvetele(new Konyv { Cim = "Az ember tragédiája", Szerzo = "Madách Imre", KiadasEve = 1861 });

        DataContext = this;
    }

    // Minden könyv változását figyeljük, hogy a számláló (KolcsonzottDb) frissüljön –
    // akkor is, ha a DataGrid jelölőnégyzetével módosítják.
    private void UjKonyvFelvetele(Konyv konyv)
    {
        konyv.PropertyChanged += (s, e) =>
        {
            if (e.PropertyName == nameof(Konyv.Kolcsonozve))
                Valtozott(nameof(KolcsonzottDb));
        };
        Konyvek.Add(konyv);
        Valtozott(nameof(KolcsonzottDb));
    }

    private void Kolcsonzes_Click(object sender, RoutedEventArgs e)
    {
        if (Kivalasztott == null)
        {
            MessageBox.Show("Válassz ki egy könyvet a táblázatban!");
            return;
        }
        Kivalasztott.Kolcsonozve = !Kivalasztott.Kolcsonozve;
    }

    private void Uj_Click(object sender, RoutedEventArgs e)
    {
        var uj = new Konyv { Cim = "Új könyv", Szerzo = "?", KiadasEve = DateTime.Now.Year };
        UjKonyvFelvetele(uj);
        Kivalasztott = uj;   // a TwoWay kötés miatt a táblázatban is kijelölődik
    }

    private void Torles_Click(object sender, RoutedEventArgs e)
    {
        if (Kivalasztott == null)
            return;
        if (MessageBox.Show($"Törlöd: {Kivalasztott.Cim}?", "Megerősítés",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
        {
            Konyvek.Remove(Kivalasztott);
            Valtozott(nameof(KolcsonzottDb));
        }
    }
}
