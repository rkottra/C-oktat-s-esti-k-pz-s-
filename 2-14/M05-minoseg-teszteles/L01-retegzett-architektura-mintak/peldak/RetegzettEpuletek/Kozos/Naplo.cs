namespace RetegzettEpuletek.Kozos;

// Közös szolgáltatás, mindegyik réteg használhatja.
// Singleton (egyke) minta: az osztályból pontosan egy példány létezik,
// és azt bárhonnan el lehet érni a Naplo.Peldany tulajdonságon keresztül.
public sealed class Naplo
{
    // A statikus mezőt a .NET egyszer, szálbiztosan hozza létre.
    private static readonly Naplo peldany = new();

    public static Naplo Peldany => peldany;

    private readonly List<string> bejegyzesek = new();

    // Privát konstruktor: kívülről nem lehet "new Naplo()"-t írni.
    private Naplo() { }

    public void Ir(string uzenet) => bejegyzesek.Add($"{DateTime.Now:HH:mm:ss} {uzenet}");

    public IReadOnlyList<string> Bejegyzesek => bejegyzesek;
}
