namespace FizetesStatikusAbsztrakt;

// Statikus osztály: nincs belőle objektum, csak segédmetódusai vannak (mint a Math).
public static class Kerekites
{
    // Készpénzes fizetésnél a végösszeget 5 forintra kerekítjük:
    // 1–2 lefelé, 3–4 felfelé a 5-re, 6–7 lefelé a 5-re, 8–9 felfelé.
    public static int OtForintra(int osszeg)
    {
        int maradek = osszeg % 5;
        return maradek <= 2 ? osszeg - maradek : osszeg + (5 - maradek);
    }
}
