using System.Text;

namespace CaesarTores;

public static class Tores
{
    private static int Mod(int a, int m) => ((a % m) + m) % m;

    // Megszámlálás 26 elemű tömbbe, kis- és nagybetűtől függetlenül
    public static int[] Gyakorisag(string szoveg)
    {
        int[] db = new int[26];
        foreach (char c in szoveg)
        {
            char n = char.ToUpper(c);
            if (n >= 'A' && n <= 'Z')
                db[n - 'A']++;
        }
        return db;
    }

    public static int Leggyakoribb(int[] db)
    {
        int max = 0;
        for (int i = 1; i < db.Length; i++)
            if (db[i] > db[max])
                max = i;
        return max;
    }

    // 1. ötlet: a leggyakoribb rejtett betű az 'e' képe. Egyszerű, de magyar szövegnél gyakran az 'a' nyer, és téved.
    public static int KulcsLeggyakoribbAlapjan(string szoveg) =>
        Mod(Leggyakoribb(Gyakorisag(szoveg)) - ('E' - 'A'), 26);

    // Közelítő magyar betűgyakoriság (%), az ékezetes betűket az ékezet nélküli párjukhoz számolva
    private static readonly double[] MagyarGyakorisag =
    {
        //  A     B    C    D     E    F    G    H    I    J    K    L    M
        12.4, 1.9, 0.6, 1.9, 13.2, 0.9, 3.6, 1.3, 4.9, 1.1, 4.7, 6.5, 3.5,
        //  N     O    P    Q    R    S    T    U    V    W    X    Y    Z
         5.7, 7.2, 1.1, 0.0, 4.6, 6.2, 7.4, 2.7, 1.9, 0.0, 0.0, 2.1, 4.3,
    };

    // Egy kulcs pontszáma: a visszafejtett szöveg betűi mennyire „magyarosak”
    public static double Pontszam(int[] db, int k)
    {
        double pont = 0;
        for (int i = 0; i < 26; i++)
            pont += db[i] * MagyarGyakorisag[Mod(i - k, 26)];   // a rejtett i. betű a visszafejtésben (i-k)
        return pont;
    }

    // 2. ötlet: mind a 26 kulcsot pontozzuk, és a legjobbat választjuk (maximumkiválasztás)
    public static int ValoszinuKulcs(string szoveg)
    {
        int[] db = Gyakorisag(szoveg);
        int legjobb = 0;
        for (int k = 1; k < 26; k++)
            if (Pontszam(db, k) > Pontszam(db, legjobb))
                legjobb = k;
        return legjobb;
    }

    public static string Titkosit(string szoveg, int k)
    {
        StringBuilder sb = new StringBuilder(szoveg.Length);
        foreach (char c in szoveg)
        {
            if (c >= 'A' && c <= 'Z') sb.Append((char)('A' + Mod(c - 'A' + k, 26)));
            else if (c >= 'a' && c <= 'z') sb.Append((char)('a' + Mod(c - 'a' + k, 26)));
            else sb.Append(c);
        }
        return sb.ToString();
    }

    public static string Visszafejt(string szoveg, int k) => Titkosit(szoveg, -k);
}
