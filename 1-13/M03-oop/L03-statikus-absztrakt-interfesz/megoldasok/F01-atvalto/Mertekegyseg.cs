namespace Atvalto;

public static class Mertekegyseg
{
    public const double KmPerMerfold = 1.609344;
    public const double KgPerFont = 0.45359237;

    private static double euroArfolyam = 395;

    public static double EuroArfolyam
    {
        get => euroArfolyam;
        set
        {
            if (value <= 0)
                throw new ArgumentException("Az árfolyam csak pozitív lehet.");
            euroArfolyam = value;
        }
    }

    public static int Atvaltasok { get; private set; }

    public static double KmbolMerfold(double km) { Atvaltasok++; return km / KmPerMerfold; }
    public static double MerfoldbolKm(double merfold) { Atvaltasok++; return merfold * KmPerMerfold; }

    public static double CelsiusbolFahrenheit(double c) { Atvaltasok++; return c * 9 / 5 + 32; }
    public static double FahrenheitbolCelsius(double f) { Atvaltasok++; return (f - 32) * 5 / 9; }

    public static double KgbolFont(double kg) { Atvaltasok++; return kg / KgPerFont; }
    public static double FontbolKg(double font) { Atvaltasok++; return font * KgPerFont; }

    public static double ForintbolEuro(double ft) { Atvaltasok++; return ft / EuroArfolyam; }
    public static double EurobolForint(double euro) { Atvaltasok++; return euro * EuroArfolyam; }
}
