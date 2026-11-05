namespace JelszoEllenorzo;

// Jelszóerősség-ellenőrző. A szabályokat a feladat README-je írja le.
public static class Ellenorzo
{
    public const int MinimalisHossz = 8;

    public const string TulRovid = "Legalább 8 karakter hosszú legyen.";
    public const string NincsNagybetu = "Legyen benne nagybetű.";
    public const string NincsSzamjegy = "Legyen benne számjegy.";
    public const string SzokozVanBenne = "Ne legyen benne szóköz.";

    // Visszaadja a megsértett szabályok üzeneteit. Üres lista = a jelszó megfelelő.
    public static List<string> Hibak(string jelszo)
    {
        var hibak = new List<string>();

        if (jelszo.Length <= MinimalisHossz)
            hibak.Add(TulRovid);
        if (!jelszo.Any(char.IsUpper))
            hibak.Add(NincsNagybetu);
        if (!jelszo.Any(char.IsLetterOrDigit))
            hibak.Add(NincsSzamjegy);
        if (jelszo.Any(char.IsWhiteSpace))
            hibak.Add(SzokozVanBenne);

        return hibak;
    }

    public static bool Eros(string jelszo) => Hibak(jelszo).Count == 0;
}
