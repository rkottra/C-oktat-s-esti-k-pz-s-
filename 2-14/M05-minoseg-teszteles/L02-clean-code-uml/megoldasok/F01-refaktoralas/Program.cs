using Parkolohaz;

var szamito = new ParkolasiDijSzamito();

// A három párhuzamos tömb helyett egy lista, amelynek elemei összetartozó adatok.
var esetek = new List<(int Percek, JarmuTipus Jarmu, bool Torzsvendeg)>
{
    (20, JarmuTipus.Auto, false),
    (30, JarmuTipus.Auto, false),
    (31, JarmuTipus.Auto, false),
    (90, JarmuTipus.Auto, true),
    (61, JarmuTipus.Motor, false),
    (600, JarmuTipus.Auto, false),
    (700, JarmuTipus.Auto, true),
    (1440, JarmuTipus.Motor, false),
    (1500, JarmuTipus.Auto, false),
};

Console.WriteLine("Parkolási díjak");
foreach (var eset in esetek)
    Console.WriteLine($"{Leiras(eset.Percek, eset.Jarmu, eset.Torzsvendeg)}: {DijSzovegesen(eset.Percek, eset.Jarmu, eset.Torzsvendeg)}");

// A négyszer ismétlődő kiírás két kis függvénybe került (DRY).
static string Leiras(int percek, JarmuTipus jarmu, bool torzsvendeg)
{
    string jarmuNev = jarmu == JarmuTipus.Motor ? "motor" : "autó";
    return $"{percek} perc, {jarmuNev}{(torzsvendeg ? ", törzsvendég" : "")}";
}

string DijSzovegesen(int percek, JarmuTipus jarmu, bool torzsvendeg)
{
    try
    {
        return $"{szamito.Dij(percek, jarmu, torzsvendeg)} Ft";
    }
    catch (ArgumentOutOfRangeException)
    {
        return "HIBA (24 óránál hosszabb)";
    }
}
