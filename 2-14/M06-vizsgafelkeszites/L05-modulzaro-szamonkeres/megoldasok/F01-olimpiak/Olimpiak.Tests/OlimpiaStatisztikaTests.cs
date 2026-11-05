using Microsoft.VisualStudio.TestTools.UnitTesting;
using Olimpiak;

namespace Olimpiak.Tests;

[TestClass]
public class OlimpiaStatisztikaTests
{
    // Kicsi, kézzel összeállított tesztadat: így pontosan tudjuk, mi a helyes eredmény
    private static List<Olimpia> Tesztadat() => new List<Olimpia>
    {
        new Olimpia(1908, "London", "Egyesült Királyság", "Európa", 1908),
        new Olimpia(1932, "Los Angeles", "Egyesült Államok", "Észak-Amerika", 1932),
        new Olimpia(1948, "London", "Egyesült Királyság", "Európa", 1948),
        new Olimpia(1964, "Tokió", "Japán", "Ázsia", 1964),
        new Olimpia(2012, "London", "Egyesült Királyság", "Európa", 2012),
        new Olimpia(2020, "Tokió", "Japán", "Ázsia", 2021)
    };

    [TestMethod]
    public void RendezesekSzama_LetezoOrszag_HelyesDarab()
    {
        Assert.AreEqual(3, OlimpiaStatisztika.RendezesekSzama(Tesztadat(), "Egyesült Királyság"));
    }

    [TestMethod]
    public void RendezesekSzama_KisbetusBemenet_IsTalal()
    {
        Assert.AreEqual(2, OlimpiaStatisztika.RendezesekSzama(Tesztadat(), "  japán "));
    }

    [TestMethod]
    public void RendezesekSzama_NemRendezoOrszag_Nulla()
    {
        Assert.AreEqual(0, OlimpiaStatisztika.RendezesekSzama(Tesztadat(), "Magyarország"));
    }

    [TestMethod]
    public void RendezesiEvek_NovekvoSorrendben()
    {
        CollectionAssert.AreEqual(new List<int> { 1908, 1948, 2012 },
            OlimpiaStatisztika.RendezesiEvek(Tesztadat(), "Egyesült Királyság"));
    }

    [TestMethod]
    public void TobbszorRendezoVarosok_CsakAKetszerVagyTobbszor()
    {
        var eredmeny = OlimpiaStatisztika.TobbszorRendezoVarosok(Tesztadat());

        Assert.AreEqual(2, eredmeny.Count);
        Assert.AreEqual(("London", 3), eredmeny[0]);   // a legtöbbször rendező az első
        Assert.AreEqual(("Tokió", 2), eredmeny[1]);
    }

    [TestMethod]
    public void HalasztottJatekok_CsakAzElteroEvu()
    {
        var eredmeny = OlimpiaStatisztika.HalasztottJatekok(Tesztadat());

        Assert.AreEqual(1, eredmeny.Count);
        Assert.AreEqual(2021, eredmeny[0].MegrendezesEve);
    }

    [TestMethod]
    public void UresLista_NemDobKivetelt()
    {
        var ures = new List<Olimpia>();
        Assert.AreEqual(0, OlimpiaStatisztika.RendezesekSzama(ures, "Japán"));
        Assert.AreEqual(0, OlimpiaStatisztika.TobbszorRendezoVarosok(ures).Count);
    }
}
