namespace AlkalmazottakOrokles;

// "A vezető egy fajta alkalmazott" → öröklődés.
public class Vezeto : Alkalmazott
{
    public int Potlek { get; set; }             // új, saját tulajdonság
    public int BeosztottakSzama { get; set; }

    public Vezeto(string nev, int alapber, int potlek, int beosztottakSzama)
        : base(nev, alapber)                    // előbb az ős konstruktora fut le
    {
        Potlek = potlek;
        BeosztottakSzama = beosztottakSzama;
        beosztas = "vezető";                    // protected mező: itt elérhető
    }

    // Felülírás: az ős változatát (base.Fizetes()) egészítjük ki.
    public override int Fizetes()
    {
        return base.Fizetes() + Potlek;
    }

    public override string ToString()
    {
        return base.ToString() + $"  ({BeosztottakSzama} beosztott)";
    }
}
