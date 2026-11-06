namespace HomeroTulajdonsagok;

// Egyetlen adatot tárolunk (Celsius-fok), de többféle mértékegységben
// is írható és olvasható – számított tulajdonságokkal.
public class Homero
{
    public const double AbszolutNulla = -273.15;

    private double celsius;     // az egyetlen tárolt adat

    public string Helyszin { get; set; }

    // Ellenőrzött tulajdonság: a set minden értékadáskor lefut.
    public double Celsius
    {
        get { return celsius; }
        set
        {
            if (value < AbszolutNulla)
                throw new ArgumentException($"{value:F2} °C nem lehetséges (abszolút nulla: {AbszolutNulla} °C).");
            celsius = value;
        }
    }

    // Számított tulajdonság get-tel ÉS set-tel: olvasáskor átszámol,
    // íráskor visszaszámol Celsiusra. Az ellenőrzést a Celsius set-je végzi.
    public double Fahrenheit
    {
        get { return Celsius * 9 / 5 + 32; }
        set { Celsius = (value - 32) * 5 / 9; }
    }

    // Rövid írásmód (=>), ha a get és a set is egyetlen kifejezés.
    public double Kelvin
    {
        get => Celsius - AbszolutNulla;
        set => Celsius = value + AbszolutNulla;
    }

    // Csak get: kívülről nem írható, mindig újraszámolódik.
    public bool Fagy => Celsius < 0;

    public Homero(string helyszin, double celsius)
    {
        Helyszin = helyszin;
        Celsius = celsius;      // a tulajdonságnak adunk értéket → ellenőrzés
    }

    public override string ToString()
    {
        string fagy = Fagy ? " (fagy!)" : "";
        return $"{Helyszin}: {Celsius:F1} °C = {Fahrenheit:F1} °F = {Kelvin:F2} K{fagy}";
    }
}

// ----- Összehasonlításképp: így NE csináljuk -----

// 1. lépés: publikus mező – bárki bármit beleírhat.
public class HomeroPublikusMezovel
{
    public double celsius;
}

// 2. lépés: privát mező + metódusok – védett, de nehézkes a használata.
public class HomeroMetodusokkal
{
    private double celsius;

    public void CelsiusBeallit(double ertek)
    {
        if (ertek >= Homero.AbszolutNulla)
            celsius = ertek;
    }

    public double CelsiusLekerdez()
    {
        return celsius;
    }
}
