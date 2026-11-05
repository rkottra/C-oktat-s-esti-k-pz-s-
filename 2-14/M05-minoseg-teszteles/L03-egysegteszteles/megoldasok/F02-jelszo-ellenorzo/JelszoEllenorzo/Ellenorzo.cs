namespace JelszoEllenorzo;

// Jelszóerősség-ellenőrző – javított változat.
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
        // 3. hiba javítása: null esetén "beszédes" kivétel, nem NullReferenceException.
        ArgumentNullException.ThrowIfNull(jelszo);

        var hibak = new List<string>();

        if (jelszo.Length < MinimalisHossz)          // 1. hiba javítása: <= helyett <
            hibak.Add(TulRovid);
        if (!jelszo.Any(char.IsUpper))
            hibak.Add(NincsNagybetu);
        if (!jelszo.Any(char.IsDigit))               // 2. hiba javítása: IsLetterOrDigit helyett IsDigit
            hibak.Add(NincsSzamjegy);
        if (jelszo.Any(char.IsWhiteSpace))
            hibak.Add(SzokozVanBenne);

        return hibak;
    }

    public static bool Eros(string jelszo) => Hibak(jelszo).Count == 0;
}
