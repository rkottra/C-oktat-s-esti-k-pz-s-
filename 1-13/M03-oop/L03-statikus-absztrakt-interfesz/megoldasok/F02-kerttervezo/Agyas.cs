namespace Kerttervezo;

public abstract class Agyas
{
    public static int Darabszam { get; private set; }

    public string Azonosito { get; }
    public string Noveny { get; }
    public double VetomagPerNm { get; }

    protected Agyas(string noveny, double vetomagPerNm)
    {
        if (vetomagPerNm <= 0)
            throw new ArgumentException("A vetőmagigény csak pozitív lehet.");

        Darabszam++;                     // közös számláló minden ágyásra
        Azonosito = $"Á{Darabszam}";
        Noveny = noveny;
        VetomagPerNm = vetomagPerNm;
    }

    // Az alakhoz tartozó számítás: minden leszármazott köteles megírni.
    public abstract double Terulet();
    public abstract double Kerulet();
    public abstract string Alak();

    // Kész metódusok, amelyek az absztraktakra épülnek.
    public double Vetomag() => Terulet() * VetomagPerNm;

    public override string ToString() =>
        $"{Azonosito,-3} {Alak(),-10} {Noveny,-12} T = {Terulet(),5:F2} m²  K = {Kerulet(),5:F2} m  vetőmag: {Vetomag(),5:F1} g";
}
