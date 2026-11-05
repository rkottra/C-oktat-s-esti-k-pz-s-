using Mozijegy;

namespace MozijegyTesztek;

[TestClass]
public class JegyarSzamitoTesztek
{
    // Egy átlagos (nem keddi) nap a tesztekhez.
    private const DayOfWeek Hetkoznap = DayOfWeek.Wednesday;

    [TestMethod]
    public void Ar_Felnott_Alapar()
    {
        Assert.AreEqual(2800, JegyarSzamito.Ar(30, Hetkoznap));
    }

    // Határértékek: 13 | 14 (gyerek/felnőtt határ) és 64 | 65 (felnőtt/nyugdíjas határ), valamint a két szélső érték.
    [TestMethod]
    [DataRow(0, 1400)]
    [DataRow(13, 1400)]
    [DataRow(14, 2800)]   // a kiinduló kód itt hibázott: 14 évesnek is gyerekárat adott
    [DataRow(64, 2800)]
    [DataRow(65, 1960)]   // a kiinduló kód itt hibázott: 65 évesnek még teljes árat adott
    [DataRow(120, 1960)]
    public void Ar_EletkorHatarai(int eletkor, int elvartAr)
    {
        Assert.AreEqual(elvartAr, JegyarSzamito.Ar(eletkor, Hetkoznap), $"Életkor: {eletkor}");
    }

    [TestMethod]
    [DataRow(30, 2240)]
    [DataRow(10, 1120)]
    [DataRow(70, 1568)]
    public void Ar_Kedden_TovabbiHuszSzazalek(int eletkor, int elvartAr)
    {
        Assert.AreEqual(elvartAr, JegyarSzamito.Ar(eletkor, DayOfWeek.Tuesday));
    }

    [TestMethod]
    [DataRow(DayOfWeek.Monday)]
    [DataRow(DayOfWeek.Saturday)]
    [DataRow(DayOfWeek.Sunday)]
    public void Ar_NemKedden_NincsNapiKedvezmeny(DayOfWeek nap)
    {
        Assert.AreEqual(2800, JegyarSzamito.Ar(30, nap));
    }

    [TestMethod]
    [DataRow(-1)]
    [DataRow(121)]
    public void Ar_ErvenytelenEletkor_Kivetel(int eletkor)
    {
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => JegyarSzamito.Ar(eletkor, Hetkoznap));
    }
}
