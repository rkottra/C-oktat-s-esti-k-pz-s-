using System.Windows;

namespace SablonWpfDataGrid;

// SABLON – modális párbeszédablak, amely ellenőrzött új objektumot ad vissza.
public partial class UjRekordAblak : Window
{
    private readonly int ujId;

    public Rekord? Eredmeny { get; private set; }

    public UjRekordAblak(int ujId)
    {
        InitializeComponent();
        this.ujId = ujId;
    }

    private void Mentes_Click(object sender, RoutedEventArgs e)
    {
        // Az ellenőrzések sorban; az első hibánál üzenet és kilépés – az ablak nyitva marad
        if (!Ellenor.KotelezoSzoveg(txtNev.Text, "név")) return;
        if (!Ellenor.PozitivValos(txtErtek.Text, "érték", out double ertek)) return;
        if (!Ellenor.MultbeliDatum(dpDatum.SelectedDate, "dátum")) return;

        Eredmeny = new Rekord
        {
            Id = ujId,
            Nev = txtNev.Text.Trim(),
            Ertek = ertek,
            Datum = dpDatum.SelectedDate!.Value.Date
        };
        DialogResult = true;
    }
}
