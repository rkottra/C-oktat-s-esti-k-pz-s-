using System.Text;

namespace Vigenere;

public static class VigenereRejtjel
{
    private static bool Betu(char c) => (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z');

    private static int Mod(int a, int m) => ((a % m) + m) % m;

    private static char Tol(char c, int k)
    {
        if (c >= 'A' && c <= 'Z') return (char)('A' + Mod(c - 'A' + k, 26));
        if (c >= 'a' && c <= 'z') return (char)('a' + Mod(c - 'a' + k, 26));
        return c;
    }

    public static bool ErvenyesKulcs(string kulcs)
    {
        if (kulcs.Length == 0) return false;
        foreach (char c in kulcs)
            if (!Betu(c)) return false;
        return true;
    }

    // A titkosítás és a visszafejtés csak az irányban különbözik
    private static string Alkalmaz(string szoveg, string kulcs, int irany)
    {
        StringBuilder sb = new StringBuilder(szoveg.Length);
        int j = 0;                                   // a BETŰK számlálója – csak betűnél lép
        foreach (char c in szoveg)
        {
            if (Betu(c))
            {
                int k = char.ToUpper(kulcs[j % kulcs.Length]) - 'A';
                sb.Append(Tol(c, irany * k));
                j++;
            }
            else
            {
                sb.Append(c);
            }
        }
        return sb.ToString();
    }

    public static string Titkosit(string szoveg, string kulcs) => Alkalmaz(szoveg, kulcs, +1);
    public static string Visszafejt(string szoveg, string kulcs) => Alkalmaz(szoveg, kulcs, -1);

    public static string KulcsSor(string szoveg, string kulcs)
    {
        StringBuilder sb = new StringBuilder(szoveg.Length);
        int j = 0;
        foreach (char c in szoveg)
        {
            if (Betu(c))
            {
                sb.Append(char.ToUpper(kulcs[j % kulcs.Length]));
                j++;
            }
            else
            {
                sb.Append(' ');
            }
        }
        return sb.ToString();
    }
}
