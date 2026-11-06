namespace TanfolyamJelentkezes;

// Egy jelentkezés adatai. A ListBox az objektum ToString()-jét írja ki.
public class Jelentkezes
{
    public string Nev { get; set; } = "";
    public string Tanfolyam { get; set; } = "";
    public string Napszak { get; set; } = "";
    public bool Tankonyv { get; set; }
    public bool Vizsga { get; set; }

    public override string ToString() => $"{Nev} – {Tanfolyam} ({Napszak})";
}
