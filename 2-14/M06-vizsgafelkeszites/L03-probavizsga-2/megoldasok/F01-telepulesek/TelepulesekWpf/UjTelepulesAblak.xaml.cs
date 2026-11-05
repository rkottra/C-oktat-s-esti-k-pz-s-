using System.Globalization;
using System.Windows;
using TelepulesekWpf.Models;

namespace TelepulesekWpf;

public partial class UjTelepulesAblak : Window
{
    // Ide kerül a sikeresen ellenőrzött új település – a főablak innen veszi át
    public Telepules? UjTelepules { get; private set; }

    public UjTelepulesAblak(List<Megye> megyek)
    {
        InitializeComponent();
        cbMegye.ItemsSource = megyek;   // alapértelmezésben nincs kiválasztott elem
    }

    private static void Hiba(string uzenet)
        => MessageBox.Show(uzenet, "Hibás adat", MessageBoxButton.OK, MessageBoxImage.Warning);

    private void Mentes_Click(object sender, RoutedEventArgs e)
    {
        if (txtNev.Text.Trim() == "") { Hiba("A település nevét meg kell adni!"); return; }
        if (cbMegye.SelectedItem is not Megye megye) { Hiba("Válassz megyét a listából!"); return; }
        if (txtJaras.Text.Trim() == "") { Hiba("A járást meg kell adni!"); return; }

        if (!int.TryParse(txtNepesseg.Text.Trim(), out int nepesseg) || nepesseg <= 0)
        {
            Hiba("A népesség 0-nál nagyobb egész szám legyen!");
            return;
        }

        // Tizedesvesszőt és -pontot is elfogadunk
        string teruletSzoveg = txtTerulet.Text.Trim().Replace(',', '.');
        if (!double.TryParse(teruletSzoveg, NumberStyles.Float, CultureInfo.InvariantCulture, out double terulet)
            || terulet <= 0)
        {
            Hiba("A terület 0-nál nagyobb szám legyen!");
            return;
        }

        UjTelepules = new Telepules
        {
            Nev = txtNev.Text.Trim(),
            MegyeId = megye.Id,
            Jaras = txtJaras.Text.Trim(),
            Nepesseg = nepesseg,
            Terulet = terulet
        };
        DialogResult = true;   // ez be is zárja a modális ablakot
    }
}
