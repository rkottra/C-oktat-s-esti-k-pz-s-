using System.Collections.ObjectModel;
using System.Windows;

namespace DataContextSzemelyek;

public partial class MainWindow : Window
{
    // ObservableCollection: a List<T> „okos” változata – szól a felületnek, ha elem kerül bele vagy törlődik.
    // Csak (publikus) TULAJDONSÁGHOZ lehet kötni, mezőhöz nem!
    public ObservableCollection<Szemely> Szemelyek { get; } = new();

    public MainWindow()
    {
        InitializeComponent();

        Szemelyek.Add(new Szemely { Nev = "Minta Anna", Kor = 34, Varos = "Győr" });
        Szemelyek.Add(new Szemely { Nev = "Teszt Elek", Kor = 52, Varos = "Pécs" });
        Szemelyek.Add(new Szemely { Nev = "Példa Péter", Kor = 27, Varos = "Szeged" });

        // Az ablak minden Binding-ja ebből az objektumból (önmagából) indul ki.
        DataContext = this;
    }

    // Ha nincs kiválasztott elem, null-t ad vissza.
    private Szemely? Kivalasztott => SzemelyLista.SelectedItem as Szemely;

    private void Megmutat_Click(object sender, RoutedEventArgs e)
    {
        // A TextBoxba írt érték a kötés miatt már az OBJEKTUMBAN van – nem a TextBoxból olvassuk ki.
        if (Kivalasztott != null)
            MessageBox.Show(Kivalasztott.ToString(), "Az objektum tartalma");
    }

    private void Szuletesnap_Click(object sender, RoutedEventArgs e)
    {
        // Kódból módosítjuk az objektumot: a PropertyChanged miatt a felület magától frissül.
        if (Kivalasztott != null)
            Kivalasztott.Kor++;
    }

    private void Uj_Click(object sender, RoutedEventArgs e)
    {
        var uj = new Szemely { Nev = "Új személy", Kor = 18, Varos = "Budapest" };
        Szemelyek.Add(uj);              // ObservableCollection → a lista és a táblázat is frissül
        SzemelyLista.SelectedItem = uj; // rögtön szerkeszthető
    }

    private void Torles_Click(object sender, RoutedEventArgs e)
    {
        if (Kivalasztott == null)
            return;
        if (MessageBox.Show($"Törlöd: {Kivalasztott.Nev}?", "Megerősítés",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
        {
            Szemelyek.Remove(Kivalasztott);
        }
    }
}
