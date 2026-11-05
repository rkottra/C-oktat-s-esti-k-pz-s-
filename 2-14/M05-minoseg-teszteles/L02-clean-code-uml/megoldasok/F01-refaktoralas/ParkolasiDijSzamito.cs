namespace Parkolohaz;

public enum JarmuTipus
{
    Auto,
    Motor
}

public class ParkolasiDijSzamito
{
    public const int IngyenesPercek = 30;
    public const int LegfeljebbPercek = 24 * 60;
    private const int AutoOradij = 400;
    private const int MotorOradij = 200;
    private const int NapiMaximumOrak = 10;
    private const int TorzsvendegKedvezmenySzazalek = 20;

    public int Dij(int percek, JarmuTipus jarmu, bool torzsvendeg)
    {
        if (percek > LegfeljebbPercek)
            throw new ArgumentOutOfRangeException(nameof(percek), "Legfeljebb 24 órát lehet parkolni.");
        if (percek <= IngyenesPercek)
            return 0;

        int dij = Math.Min(MegkezdettOrak(percek) * Oradij(jarmu), NapiMaximum(jarmu));
        return torzsvendeg ? KedvezmenyLevonasa(dij) : dij;
    }

    private static int MegkezdettOrak(int percek) => (int)Math.Ceiling(percek / 60.0);   // felfelé kerekítés

    private static int Oradij(JarmuTipus jarmu) => jarmu == JarmuTipus.Motor ? MotorOradij : AutoOradij;

    private static int NapiMaximum(JarmuTipus jarmu) => NapiMaximumOrak * Oradij(jarmu);

    private static int KedvezmenyLevonasa(int dij) => dij - dij * TorzsvendegKedvezmenySzazalek / 100;
}
