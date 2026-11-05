namespace Mozijegy;

// Mozijegy árának kiszámítása. A szabályokat a feladat README-je írja le.
// A napot paraméterként kapja (nem DateTime.Now-ból veszi), ezért bármelyik napra tesztelhető.
public static class JegyarSzamito
{
    public const int Alapar = 2800;
    public const int GyerekKedvezmenySzazalek = 50;
    public const int NyugdijasKedvezmenySzazalek = 30;
    public const int KeddiKedvezmenySzazalek = 20;

    public static int Ar(int eletkor, DayOfWeek nap)
    {
        if (eletkor < 0 || eletkor > 120)
            throw new ArgumentOutOfRangeException(nameof(eletkor), "Az életkor 0 és 120 között lehet.");

        int ar = Alapar;
        if (eletkor <= 14)
            ar = ar * (100 - GyerekKedvezmenySzazalek) / 100;
        else if (eletkor > 65)
            ar = ar * (100 - NyugdijasKedvezmenySzazalek) / 100;

        if (nap == DayOfWeek.Tuesday)
            ar = ar * (100 - KeddiKedvezmenySzazalek) / 100;

        return ar;
    }
}
