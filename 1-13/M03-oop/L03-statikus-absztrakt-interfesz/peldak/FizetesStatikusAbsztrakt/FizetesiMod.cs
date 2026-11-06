namespace FizetesStatikusAbsztrakt;

// Absztrakt ős: „fizetési mód” önmagában nem létezik, csak készpénz, kártya stb.
public abstract class FizetesiMod
{
    // Statikus tagok: az ÖSSZES fizetésre közösen vonatkoznak.
    public static int SikeresFizetesek { get; private set; }
    public static int Osszforgalom { get; private set; }

    public string Megnevezes { get; }

    protected FizetesiMod(string megnevezes)
    {
        Megnevezes = megnevezes;
    }

    // Absztrakt metódusok: minden fizetési mód MÁSHOGY csinálja, ezért nincs törzsük.
    protected abstract int Fizetendo(int osszeg);
    protected abstract bool Engedelyez(int osszeg, out string ok);

    // Sablonmetódus: a menet közös, a lépéseket a leszármazottak adják.
    public bool Fizet(int osszeg, string tetel)
    {
        int fizetendo = Fizetendo(osszeg);
        if (!Engedelyez(fizetendo, out string ok))
        {
            Console.WriteLine($"  ELUTASÍTVA  {Megnevezes,-12} {tetel,-18} {fizetendo,7:N0} Ft – {ok}");
            return false;
        }
        SikeresFizetesek++;
        Osszforgalom += fizetendo;
        Console.WriteLine($"  rendben     {Megnevezes,-12} {tetel,-18} {fizetendo,7:N0} Ft");
        return true;
    }
}
