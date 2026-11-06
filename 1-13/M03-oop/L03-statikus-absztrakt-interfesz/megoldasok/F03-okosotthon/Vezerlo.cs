namespace Okosotthon;

// Statikus segédosztály: a vezérlő maga nem tárol állapotot, csak műveleteket végez a listán.
// Csak interfészeken keresztül dolgozik, ezért egy új eszköztípus miatt nem kell módosítani.
public static class Vezerlo
{
    public static void MindentKi(List<Eszkoz> eszkozok)
    {
        foreach (Eszkoz e in eszkozok)
            if (e is IKapcsolhato k)
                k.Ki();
    }

    public static void Be(List<Eszkoz> eszkozok, string helyiseg)
    {
        foreach (Eszkoz e in eszkozok)
            if (e.Helyiseg == helyiseg && e is IKapcsolhato k)
                k.Be();
    }

    public static void Szintre(List<Eszkoz> eszkozok, string helyiseg, int szint)
    {
        foreach (Eszkoz e in eszkozok)
            if (e.Helyiseg == helyiseg && e is ISzabalyozhato s)
                s.Szint = szint;
    }

    // Típusra (osztályra) szűrő változat, pl. „minden redőny”
    public static void Szintre<T>(List<Eszkoz> eszkozok, int szint) where T : ISzabalyozhato
    {
        foreach (Eszkoz e in eszkozok)
            if (e is T s)
                s.Szint = szint;
    }

    public static double OsszFogyasztas(List<Eszkoz> eszkozok)
    {
        double osszes = 0;
        foreach (Eszkoz e in eszkozok)
            if (e is IFogyaszto f)
                osszes += f.Teljesitmeny();
        return osszes;
    }

    public static void FogyasztasHelyisegenkent(List<Eszkoz> eszkozok)
    {
        List<string> helyisegek = new List<string>();
        foreach (Eszkoz e in eszkozok)
            if (!helyisegek.Contains(e.Helyiseg))
                helyisegek.Add(e.Helyiseg);

        List<string> reszek = new List<string>();
        foreach (string h in helyisegek)
        {
            double watt = 0;
            foreach (Eszkoz e in eszkozok)
                if (e.Helyiseg == h && e is IFogyaszto f)
                    watt += f.Teljesitmeny();
            reszek.Add($"{h} {watt:N0} W");
        }
        Console.WriteLine($"Helyiségenként: {string.Join(", ", reszek)}");
    }

    public static void Allapotlista(List<Eszkoz> eszkozok)
    {
        foreach (Eszkoz e in eszkozok)
            Console.WriteLine(e);
    }
}
