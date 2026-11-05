using System.Globalization;
using System.Windows;
using ElemekWpf.Models;

namespace ElemekWpf;

public partial class UjElemAblak : Window
{
    private readonly List<int> foglaltRendszamok;
    private readonly List<string> foglaltVegyjelek;

    public Elem? UjElem { get; private set; }

    public UjElemAblak(List<Kategoria> kategoriak, List<int> foglaltRendszamok, List<string> foglaltVegyjelek)
    {
        InitializeComponent();
        this.foglaltRendszamok = foglaltRendszamok;
        this.foglaltVegyjelek = foglaltVegyjelek;
        cbKategoria.ItemsSource = kategoriak;
        cbHalmaz.ItemsSource = MainWindow.Halmazallapotok;
    }

    // Tizedesvessző és -pont egyaránt elfogadott
    private static bool ValosSzam(string szoveg, out double ertek)
        => double.TryParse(szoveg.Trim().Replace(',', '.'), NumberStyles.Float, CultureInfo.InvariantCulture, out ertek);

    // Egy helyen az összes szabály: az első hiba szövegét adja vissza, vagy null-t, ha minden rendben
    private string? Ellenorzes()
    {
        if (!int.TryParse(txtRendszam.Text, out int rendszam) || rendszam <= 0)
            return "A rendszám pozitív egész szám legyen!";
        if (foglaltRendszamok.Contains(rendszam))
            return $"A(z) {rendszam} rendszámú elem már szerepel az adatbázisban!";

        string vegyjel = txtVegyjel.Text.Trim();
        bool joForma = vegyjel.Length is 1 or 2 && vegyjel.All(char.IsLetter)
                       && char.IsUpper(vegyjel[0]) && (vegyjel.Length == 1 || char.IsLower(vegyjel[1]));
        if (!joForma)
            return "A vegyjel 1–2 betű, nagy kezdőbetűvel (pl. Rb, K)!";
        if (foglaltVegyjelek.Contains(vegyjel))
            return $"A(z) {vegyjel} vegyjel már foglalt!";

        if (txtNev.Text.Trim() == "") return "A név megadása kötelező!";
        if (cbKategoria.SelectedItem is null) return "Válassz kategóriát!";
        if (!int.TryParse(txtPeriodus.Text, out int p) || p < 1 || p > 7) return "A periódus 1 és 7 közötti egész szám!";
        if (!int.TryParse(txtCsoport.Text, out int cs) || cs < 1 || cs > 18) return "A csoport 1 és 18 közötti egész szám!";
        if (!ValosSzam(txtAtomtomeg.Text, out double tomeg) || tomeg <= 0) return "Az atomtömeg pozitív szám legyen!";
        if (txtElektroneg.Text.Trim() != "" && (!ValosSzam(txtElektroneg.Text, out double en) || en < 0 || en > 4))
            return "Az elektronegativitás üres, vagy 0 és 4 közötti szám legyen!";
        if (cbHalmaz.SelectedItem is null) return "Válassz halmazállapotot!";
        return null;
    }

    private void Mentes_Click(object sender, RoutedEventArgs e)
    {
        string? hiba = Ellenorzes();
        if (hiba is not null)
        {
            MessageBox.Show(hiba, "Hibás adat", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        // Ide már csak érvényes adattal jutunk, a Parse nem dobhat kivételt
        ValosSzam(txtAtomtomeg.Text, out double tomeg);
        double? en = ValosSzam(txtElektroneg.Text, out double e2) ? e2 : null;

        UjElem = new Elem
        {
            Rendszam = int.Parse(txtRendszam.Text),
            Vegyjel = txtVegyjel.Text.Trim(),
            Nev = txtNev.Text.Trim(),
            KategoriaId = ((Kategoria)cbKategoria.SelectedItem).Id,
            Periodus = int.Parse(txtPeriodus.Text),
            Csoport = int.Parse(txtCsoport.Text),
            Atomtomeg = tomeg,
            Elektronegativitas = en,
            Halmazallapot = (string)cbHalmaz.SelectedItem
        };
        DialogResult = true;
    }
}
