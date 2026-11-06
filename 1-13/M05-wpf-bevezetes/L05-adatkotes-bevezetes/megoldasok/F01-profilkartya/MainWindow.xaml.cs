using System.Windows;

namespace Profilkartya;

public partial class MainWindow : Window
{
    private readonly Szemely szemely = new Szemely();

    public MainWindow()
    {
        InitializeComponent();
        Alaphelyzet();
        DataContext = szemely;           // minden {Binding …} ennek a tulajdonságaira mutat
    }

    private void Alaphelyzet()
    {
        szemely.Nev = "Kiss Anna Mária";
        szemely.Foglalkozas = "szoftverfejlesztő tanuló";
        szemely.Varos = "Budapest";
        szemely.SzuletesiEv = 2008;
        szemely.Szin = "LightSteelBlue";
    }

    // Kódból módosítjuk az objektumot – az INotifyPropertyChanged miatt a felület magától frissül
    private void IdosebbGomb_Click(object sender, RoutedEventArgs e) => szemely.SzuletesiEv--;

    private void AlaphelyzetGomb_Click(object sender, RoutedEventArgs e) => Alaphelyzet();
}
