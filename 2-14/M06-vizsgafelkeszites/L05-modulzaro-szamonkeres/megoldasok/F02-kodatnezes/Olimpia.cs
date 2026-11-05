namespace OlimpiaJavitott;

// [R1] Indexelt string-tömbök helyett beszédes tulajdonságok
public class Olimpia
{
    public int Ev { get; }
    public string Varos { get; }
    public string Orszag { get; }
    public string Kontinens { get; }

    private Olimpia(int ev, string varos, string orszag, string kontinens)
    {
        Ev = ev;
        Varos = varos;
        Orszag = orszag;
        Kontinens = kontinens;
    }

    // [H1] Skip(1): pontosan a fejléc marad ki – az utolsó adatsor nem vész el
    public static List<Olimpia> BetoltCsv(string fajlnev)
        => File.ReadAllLines(fajlnev)
            .Skip(1)
            .Where(sor => sor.Trim() != "")
            .Select(sor =>
            {
                string[] a = sor.Split(';');
                return new Olimpia(int.Parse(a[0]), a[1], a[2], a[3]);
            })
            .ToList();
}
