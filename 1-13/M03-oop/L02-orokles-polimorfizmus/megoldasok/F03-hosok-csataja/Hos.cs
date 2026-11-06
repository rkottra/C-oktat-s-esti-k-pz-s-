namespace HosokCsataja;

public class Hos
{
    public string Nev { get; }
    public int MaxElet { get; }
    public int Tamadas { get; }
    public int Elet { get; private set; }
    public int OkozottSebzes { get; private set; }
    public bool El => Elet > 0;

    // A leszármazottak ide írhatnak megjegyzést (pl. "KRITIKUS!") a csatanaplóhoz.
    protected string megjegyzes = "";

    public Hos(string nev, int maxElet, int tamadas)
    {
        Nev = nev;
        MaxElet = maxElet;
        Tamadas = tamadas;
        Elet = maxElet;
    }

    public virtual int Sebzes(Random rnd) => Tamadas + rnd.Next(0, 5);

    public virtual void Serul(int pont)
    {
        Elet = Math.Max(0, Elet - pont);
    }

    public virtual void KorVege() { }

    public virtual string Kaszt() => "hős";

    // Nem virtuális: a menet mindenkinél ugyanaz, a részleteket a felülírt metódusok adják.
    public void Megtamad(Hos cel, Random rnd)
    {
        megjegyzes = "";
        int sebzes = Sebzes(rnd);
        int eletElotte = cel.Elet;
        cel.Serul(sebzes);
        OkozottSebzes += eletElotte - cel.Elet;     // a ténylegesen levont élet számít

        Console.WriteLine($"{Nev} ({Kaszt()}) → {cel.Nev} ({cel.Kaszt()}): {sebzes} sebzés {megjegyzes}".TrimEnd()
                          + $" (élet: {cel.Elet}/{cel.MaxElet})");
        if (!cel.El)
            Console.WriteLine($"   {cel.Nev} elesett!");
    }

    public override string ToString() => $"{Nev} ({Kaszt()}) {Elet}/{MaxElet}";
}
