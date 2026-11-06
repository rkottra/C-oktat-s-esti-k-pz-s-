using System.Collections.ObjectModel;
using System.Windows;

namespace BindingLista;

public partial class MainWindow : Window
{
    // ObservableCollection: hozzáadáskor és törléskor értesíti a felületet (a List<T> nem!)
    public ObservableCollection<Konyv> Konyvek { get; } = new ObservableCollection<Konyv>
    {
        new Konyv { Cim = "Egri csillagok", Szerzo = "Gárdonyi Géza", Ev = 1899 },
        new Konyv { Cim = "A Pál utcai fiúk", Szerzo = "Molnár Ferenc", Ev = 1906, Kolcsonozve = true },
        new Konyv { Cim = "Abigél", Szerzo = "Szabó Magda", Ev = 1970 },
        new Konyv { Cim = "Tüskevár", Szerzo = "Fekete István", Ev = 1957, Kolcsonozve = true },
    };

    public MainWindow()
    {
        InitializeComponent();
        DataContext = this;              // a {Binding Konyvek} az ablak Konyvek tulajdonságára mutat
        KonyvLista.SelectedIndex = 0;
    }

    private void UjGomb_Click(object sender, RoutedEventArgs e)
    {
        Konyv uj = new Konyv { Cim = "Új könyv", Szerzo = "Ismeretlen", Ev = DateTime.Today.Year };
        Konyvek.Add(uj);                 // nincs ListBox.Items.Add – a lista magától frissül
        KonyvLista.SelectedItem = uj;
    }

    private void TorlesGomb_Click(object sender, RoutedEventArgs e)
    {
        if (KonyvLista.SelectedItem is Konyv k)
            Konyvek.Remove(k);
    }
}
