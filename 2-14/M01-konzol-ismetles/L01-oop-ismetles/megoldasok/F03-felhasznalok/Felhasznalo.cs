namespace Felhasznalok;

public class Felhasznalo
{
    private string felhasznalonev = "";
    private string jelszo = "";          // privát: kívülről sem olvasni, sem írni nem lehet

    public string Felhasznalonev
    {
        get => felhasznalonev;
        private set                       // csak az osztályon belül írható
        {
            string nev = value.Trim().ToLower();
            if (nev.Length < 3)
                throw new ArgumentException("A felhasználónév legalább 3 karakter legyen.");
            felhasznalonev = nev;
        }
    }

    public DateTime Letrehozva { get; }

    // Opcionális paraméter: ha nem adják meg, a kezdőjelszó lesz beállítva.
    public Felhasznalo(string felhasznalonev, string jelszo = "Kezdo123")
    {
        Felhasznalonev = felhasznalonev;
        Letrehozva = DateTime.Now;
        if (jelszo != "")
            BeallitJelszo(jelszo);
    }

    // protected: a leszármazott (pl. Vendeg) is meghívhatja, kívülről nem érhető el.
    protected static bool ErosJelszo(string jelszo) => jelszo.Length >= 8 && jelszo.Any(char.IsDigit);

    private void BeallitJelszo(string uj)
    {
        if (!ErosJelszo(uj))
            throw new ArgumentException("A jelszó legalább 8 karakter legyen, és tartalmazzon számjegyet.");
        jelszo = uj;
    }

    public virtual bool Ellenoriz(string jelszo) => this.jelszo == jelszo;

    public bool JelszoCsere(string regi, string uj)
    {
        if (!Ellenoriz(regi)) return false;
        BeallitJelszo(uj);
        return true;
    }

    public virtual string Jogosultsag => "olvasó";

    // A ToString soha ne írja ki a jelszót!
    public override string ToString() => $"{Felhasznalonev,-12} [{Jogosultsag}]";
}
