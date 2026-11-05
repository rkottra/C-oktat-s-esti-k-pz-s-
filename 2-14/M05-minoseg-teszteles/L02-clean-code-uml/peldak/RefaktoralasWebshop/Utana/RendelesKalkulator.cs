namespace RefaktoralasWebshop.Utana;

public class RendelesKalkulator
{
    // Varázsszámok helyett elnevezett konstansok: egy helyen módosíthatók, és látszik a jelentésük.
    private const decimal IngyenesSzallitasHatara = 15000;
    private const decimal HazhozSzallitasDija = 1490;
    private const decimal CsomagpontDija = 890;

    private static readonly Dictionary<string, decimal> KuponKedvezmenyek = new()
    {
        ["TAVASZ10"] = 0.10m,
        ["HUSEG15"] = 0.15m,
    };

    // A fő metódus úgy olvasható, mint a szabály leírása – a részleteket kisebb metódusok végzik.
    public decimal Vegosszeg(List<RendelesTetel> tetelek, string kuponKod, SzallitasiMod szallitasiMod)
    {
        decimal kedvezmenyesOsszeg = KedvezmenyLevonasa(Reszosszeg(tetelek), kuponKod);
        return Math.Round(kedvezmenyesOsszeg + SzallitasiDij(kedvezmenyesOsszeg, szallitasiMod));
    }

    private static decimal Reszosszeg(List<RendelesTetel> tetelek) =>
        tetelek.Sum(t => t.Osszeg);

    private static decimal KedvezmenyLevonasa(decimal osszeg, string kuponKod)
    {
        decimal kedvezmenyArany = KuponKedvezmenyek.GetValueOrDefault(kuponKod, 0m);
        return osszeg - osszeg * kedvezmenyArany;
    }

    private static decimal SzallitasiDij(decimal osszeg, SzallitasiMod szallitasiMod)
    {
        if (osszeg >= IngyenesSzallitasHatara)
            return 0;
        return szallitasiMod == SzallitasiMod.Hazhoz ? HazhozSzallitasDija : CsomagpontDija;
    }
}
