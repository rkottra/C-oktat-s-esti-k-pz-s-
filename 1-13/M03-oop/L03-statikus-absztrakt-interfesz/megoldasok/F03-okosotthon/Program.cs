using Okosotthon;

Console.OutputEncoding = System.Text.Encoding.UTF8;

List<Eszkoz> otthon = new List<Eszkoz>
{
    new Lampa("mennyezeti lámpa", "konyha", 12),
    new Konnektor("kávéfőző-konnektor", "konyha", "kávéfőző", 1200),
    new Lampa("állólámpa", "nappali", 9),
    new Lampa("mennyezeti lámpa", "nappali", 18),
    new Redony("redőny", "nappali"),
    new Termosztat("termosztát", "nappali"),
    new Konnektor("tévé-konnektor", "nappali", "tévé", 110),
    new Lampa("éjjeli lámpa", "hálószoba", 5),
    new Redony("redőny", "hálószoba"),
    new Konnektor("töltő-konnektor", "dolgozószoba", "laptoptöltő", 65),
};
Console.WriteLine($"Regisztrált eszközök: {Eszkoz.Darab}");

// --- Reggel ---
Vezerlo.Be(otthon, "konyha");
Vezerlo.Szintre(otthon, "konyha", 100);
foreach (Eszkoz e in otthon)
    if (e is Lampa l && e.Helyiseg == "nappali")
    {
        l.Be();
        l.Szint = 100;
    }
Vezerlo.Szintre<Redony>(otthon, 100);
Vezerlo.Szintre<Termosztat>(otthon, 22);
Jelentes("Reggel", otthon);

// --- Elmegyek otthonról ---
Vezerlo.MindentKi(otthon);
Vezerlo.Szintre<Redony>(otthon, 0);
Vezerlo.Szintre<Termosztat>(otthon, 17);
Jelentes("Elmegyek otthonról", otthon);

// --- Este ---
Vezerlo.Be(otthon, "nappali");           // lámpák és a tévé konnektora
Vezerlo.Szintre<Lampa>(otthon, 40);
Vezerlo.Szintre<Redony>(otthon, 0);
Vezerlo.Szintre<Termosztat>(otthon, 21);
Jelentes("Este", otthon);

// Próba: a határokon kívüli érték a határra szorul
Vezerlo.Szintre<Termosztat>(otthon, 35);
Console.WriteLine($"Termosztát 35 °C-ra állítva → {otthon.Find(e => e is Termosztat)}");

static void Jelentes(string cim, List<Eszkoz> eszkozok)
{
    Console.WriteLine();
    Console.WriteLine($"=== {cim} ===");
    Vezerlo.Allapotlista(eszkozok);
    Console.WriteLine($"Összfogyasztás: {Vezerlo.OsszFogyasztas(eszkozok):N0} W");
    Vezerlo.FogyasztasHelyisegenkent(eszkozok);
}
