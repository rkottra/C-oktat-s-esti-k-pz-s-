using System.Text;

namespace CaesarAblak;

// A titkosító logika: nincs benne semmi a felületről, így bárhonnan hívható és tesztelhető.
public static class Caesar
{
    private const string Ekezetes = "áéíóöőúüűÁÉÍÓÖŐÚÜŰ";
    private const string Ekezetlen = "aeiooouuuAEIOOOUUU";

    // Pozitív maradék negatív számra is: Mod(-3, 26) == 23
    public static int Mod(int a, int m) => ((a % m) + m) % m;

    public static char Tol(char c, int k)
    {
        if (c >= 'A' && c <= 'Z') return (char)('A' + Mod(c - 'A' + k, 26));
        if (c >= 'a' && c <= 'z') return (char)('a' + Mod(c - 'a' + k, 26));
        return c;                                   // minden más változatlan
    }

    public static string Titkosit(string szoveg, int k)
    {
        StringBuilder sb = new StringBuilder(szoveg.Length);
        foreach (char c in szoveg)
            sb.Append(Tol(c, k));
        return sb.ToString();
    }

    public static string Visszafejt(string szoveg, int k) => Titkosit(szoveg, -k);

    public static string Ekezettelenit(string szoveg)
    {
        StringBuilder sb = new StringBuilder(szoveg.Length);
        foreach (char c in szoveg)
        {
            int i = Ekezetes.IndexOf(c);
            sb.Append(i >= 0 ? Ekezetlen[i] : c);
        }
        return sb.ToString();
    }
}
