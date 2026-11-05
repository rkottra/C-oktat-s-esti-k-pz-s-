namespace ParkolohazRegi;

// Működő, de nehezen olvasható kód. Refaktoráld úgy, hogy a program kimenete ne változzon!
class P
{
    // p: percek, m: motor-e, tv: törzsvendég-e
    public int D(int p, bool m, bool tv)
    {
        if (p > 1440) return -1;
        if (p <= 30) return 0;
        int o = p / 60;
        if (p % 60 != 0) o++;
        int d;
        if (m)
        {
            d = o * 200;
            if (d > 2000) d = 2000;
            if (tv) d = d - d * 20 / 100;
        }
        else
        {
            d = o * 400;
            if (d > 4000) d = 4000;
            if (tv) d = d - d * 20 / 100;
        }
        return d;
    }
}
