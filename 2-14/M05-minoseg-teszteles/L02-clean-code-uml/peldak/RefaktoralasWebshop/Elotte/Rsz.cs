namespace RefaktoralasWebshop.Elotte;

// ROSSZ PÉLDA – szándékosan! A kód helyes eredményt ad, de nehéz olvasni és módosítani.
// Keresd meg benne: semmitmondó nevek, varázsszámok, ismétlődés, jelző (bool) paraméter,
// egy metódus túl sok feladattal, felesleges komment.
public class Rsz
{
    public double Szamol(List<string> l, string k, bool t)
    {
        double s = 0;
        // végigmegyünk a listán
        foreach (var x in l)
        {
            var a = x.Split(';');
            s = s + int.Parse(a[1]) * int.Parse(a[2]);
        }
        if (k == "TAVASZ10")
        {
            s = s - s * 0.1;
        }
        if (k == "HUSEG15")
        {
            s = s - s * 0.15;
        }
        double sz = 0;
        if (t)
        {
            if (s < 15000)
            {
                sz = 1490;
            }
        }
        else
        {
            if (s < 15000)
            {
                sz = 890;
            }
        }
        return Math.Round(s + sz);
    }
}
