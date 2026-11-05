using RefaktoralasWebshop.Elotte;
using RefaktoralasWebshop.Utana;

// A refaktorálás szabálya: a kód szerkezete változik, a viselkedése NEM.
// Ezért ugyanazokra a rendelésekre kiszámoljuk a végösszeget a régi és az új kóddal is.

var rendelesek = new List<(List<string> Sorok, string Kupon, bool Hazhoz)>
{
    (new() { "Bögre;2490;2", "Teafilter;890;3" }, "", true),
    (new() { "Hátizsák;12990;1", "Kulacs;3490;1" }, "TAVASZ10", false),
    (new() { "Futócipő;24990;1" }, "HUSEG15", true),
    (new() { "Jegyzetfüzet;1290;4" }, "ISMERETLEN", false),
};

var regi = new Rsz();
var uj = new RendelesKalkulator();

Console.WriteLine($"{"Rendelés",-40} {"Előtte",10} {"Utána",10}");
foreach (var r in rendelesek)
{
    double regiEredmeny = regi.Szamol(r.Sorok, r.Kupon, r.Hazhoz);

    var tetelek = r.Sorok.Select(RendelesTetel.Feldolgoz).ToList();
    var mod = r.Hazhoz ? SzallitasiMod.Hazhoz : SzallitasiMod.Csomagpont;
    decimal ujEredmeny = uj.Vegosszeg(tetelek, r.Kupon, mod);

    string leiras = $"{string.Join(", ", tetelek.Select(t => t.Nev))} / {mod} / {r.Kupon}";
    string egyezik = (decimal)regiEredmeny == ujEredmeny ? "OK" : "ELTÉR!";
    Console.WriteLine($"{leiras,-40} {regiEredmeny,10:N0} {ujEredmeny,10:N0}  {egyezik}");
}
