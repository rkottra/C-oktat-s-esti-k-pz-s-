using System.Windows;
using DialogusAblakMinta.Models;

namespace DialogusAblakMinta;

public partial class UjKonyvAblak : Window
{
    // A hívó ablak ezen keresztül kapja vissza az új könyvet
    public Konyv? Eredmeny { get; private set; }

    public UjKonyvAblak(List<Mufaj> mufajok)
    {
        InitializeComponent();
        cbMufaj.ItemsSource = mufajok;
    }

    private void Mentes_Click(object sender, RoutedEventArgs e)
    {
        string? hiba = null;
        if (txtCim.Text.Trim() == "") hiba = "A cím megadása kötelező!";
        else if (txtSzerzo.Text.Trim() == "") hiba = "A szerző megadása kötelező!";
        else if (cbMufaj.SelectedItem is null) hiba = "Válassz műfajt!";
        else if (!int.TryParse(txtEv.Text, out int ev) || ev < 1000 || ev > DateTime.Now.Year)
            hiba = "Az évszám 1000 és az aktuális év közötti egész szám legyen!";

        if (hiba is not null)
        {
            MessageBox.Show(hiba, "Hibás adat", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;   // az ablak nyitva marad, javítható az adat
        }

        var mufaj = (Mufaj)cbMufaj.SelectedItem!;
        Eredmeny = new Konyv
        {
            Cim = txtCim.Text.Trim(),
            Szerzo = txtSzerzo.Text.Trim(),
            MufajId = (int)cbMufaj.SelectedValue,   // SelectedValuePath="Id" miatt az Id érkezik
            Mufaj = mufaj,
            KiadasEve = int.Parse(txtEv.Text)
        };
        DialogResult = true;   // bezárja az ablakot, a ShowDialog() true-val tér vissza
    }
}
