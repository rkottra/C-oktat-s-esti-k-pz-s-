namespace KeszletMintak;

// Singleton: az alkalmazás beállításai egyetlen, mindenhonnan elérhető példányban.
public sealed class Beallitasok
{
    private static readonly Beallitasok peldany = new();
    public static Beallitasok Peldany => peldany;

    private Beallitasok() { }

    public string CsvFajl { get; set; } = "termekek.csv";
    public int UtanrendelesiHatar { get; set; } = 5;
}
