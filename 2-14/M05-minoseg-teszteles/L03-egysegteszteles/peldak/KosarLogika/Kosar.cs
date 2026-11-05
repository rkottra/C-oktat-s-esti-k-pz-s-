namespace KosarLogika;

public class Tetel
{
    public string Nev { get; }
    public int Egysegar { get; }
    public int Mennyiseg { get; internal set; }

    public Tetel(string nev, int egysegar, int mennyiseg)
    {
        Nev = nev;
        Egysegar = egysegar;
        Mennyiseg = mennyiseg;
    }

    public int Osszeg => Egysegar * Mennyiseg;
}

// Webshop-kosár: a tesztelt osztály (SUT – system under test).
// Nincs benne Console, fájl vagy adatbázis, ezért könnyen tesztelhető.
public class Kosar
{
    public const int IngyenesSzallitasHatara = 15000;
    public const int SzallitasiDij = 1490;
    public const int MaxKedvezmenySzazalek = 50;

    private readonly List<Tetel> tetelek = new();

    public IReadOnlyList<Tetel> Tetelek => tetelek;

    public void Hozzaad(string nev, int egysegar, int mennyiseg = 1)
    {
        if (string.IsNullOrWhiteSpace(nev))
            throw new ArgumentException("A termék neve nem lehet üres.", nameof(nev));
        if (egysegar <= 0)
            throw new ArgumentOutOfRangeException(nameof(egysegar), "Az egységár csak pozitív lehet.");
        if (mennyiseg <= 0)
            throw new ArgumentOutOfRangeException(nameof(mennyiseg), "A mennyiség csak pozitív lehet.");

        // Ha már van ilyen termék, csak a mennyiség nő.
        Tetel? meglevo = tetelek.FirstOrDefault(t => t.Nev == nev);
        if (meglevo != null)
            meglevo.Mennyiseg += mennyiseg;
        else
            tetelek.Add(new Tetel(nev, egysegar, mennyiseg));
    }

    public bool Torol(string nev) => tetelek.RemoveAll(t => t.Nev == nev) > 0;

    public int Reszosszeg() => tetelek.Sum(t => t.Osszeg);

    public int Szallitas()
    {
        if (tetelek.Count == 0 || Reszosszeg() >= IngyenesSzallitasHatara)
            return 0;
        return SzallitasiDij;
    }

    // A kedvezmény a termékek árából jár, a szállítási díjból nem.
    public int Vegosszeg(int kedvezmenySzazalek = 0)
    {
        if (kedvezmenySzazalek < 0 || kedvezmenySzazalek > MaxKedvezmenySzazalek)
            throw new ArgumentOutOfRangeException(nameof(kedvezmenySzazalek), $"A kedvezmény 0 és {MaxKedvezmenySzazalek}% között lehet.");

        int reszosszeg = Reszosszeg();
        int kedvezmeny = reszosszeg * kedvezmenySzazalek / 100;
        return reszosszeg - kedvezmeny + Szallitas();
    }
}
