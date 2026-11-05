using KosarLogika;

namespace KosarLogikaTesztek;

[TestClass]
public class KosarTesztek
{
    private Kosar kosar = null!;

    // Minden teszt előtt lefut: minden teszt új, üres kosárral indul, így a tesztek nem hatnak egymásra.
    [TestInitialize]
    public void Elokeszites()
    {
        kosar = new Kosar();
    }

    // Elnevezés: Metodus_Helyzet_ElvartEredmeny
    [TestMethod]
    public void Reszosszeg_UresKosar_Nulla()
    {
        int eredmeny = kosar.Reszosszeg();

        Assert.AreEqual(0, eredmeny);
    }

    [TestMethod]
    public void Reszosszeg_KetKulonbozoTetel_OsszeadjaAzArakat()
    {
        // Arrange (előkészítés)
        kosar.Hozzaad("Bögre", 2490, 2);
        kosar.Hozzaad("Teafilter", 890, 3);

        // Act (végrehajtás)
        int eredmeny = kosar.Reszosszeg();

        // Assert (ellenőrzés): elvárt érték az első, a tényleges a második paraméter!
        Assert.AreEqual(2 * 2490 + 3 * 890, eredmeny);
    }

    [TestMethod]
    public void Hozzaad_UgyanazATermekKetszer_EgyTetelNoveltMennyiseggel()
    {
        kosar.Hozzaad("Bögre", 2490);
        kosar.Hozzaad("Bögre", 2490, 2);

        Assert.AreEqual(1, kosar.Tetelek.Count, "Ugyanaz a termék ne kerüljön be kétszer.");
        Assert.AreEqual(3, kosar.Tetelek[0].Mennyiseg);
    }

    [TestMethod]
    public void Torol_LetezoTermek_IgazatAdEsEltavolitja()
    {
        kosar.Hozzaad("Bögre", 2490);

        bool sikerult = kosar.Torol("Bögre");

        Assert.IsTrue(sikerult);
        Assert.AreEqual(0, kosar.Tetelek.Count);
    }

    [TestMethod]
    public void Torol_NemLetezoTermek_HamisatAd()
    {
        Assert.IsFalse(kosar.Torol("Nincs ilyen"));
    }

    // Határeset-tesztelés: a 15 000 Ft-os határ alatt, pontosan rajta és fölötte.
    // Egy tesztmetódus, több adatsor: mindegyik DataRow külön tesztként fut.
    [TestMethod]
    [DataRow(14999, 1490)]
    [DataRow(15000, 0)]
    [DataRow(15001, 0)]
    [DataRow(1, 1490)]
    public void Szallitas_ReszosszegFuggvenyeben(int egysegar, int elvartDij)
    {
        kosar.Hozzaad("Termék", egysegar);

        Assert.AreEqual(elvartDij, kosar.Szallitas());
    }

    [TestMethod]
    public void Szallitas_UresKosar_Ingyenes()
    {
        Assert.AreEqual(0, kosar.Szallitas());
    }

    [TestMethod]
    [DataRow(0, 20000)]
    [DataRow(10, 18000)]
    [DataRow(50, 10000)]   // kedvezmény után 10 000 Ft, de a szállítás a kedvezmény előtti részösszegtől függ: ingyenes
    public void Vegosszeg_Kedvezmennyel(int szazalek, int elvart)
    {
        kosar.Hozzaad("Kabát", 20000);

        Assert.AreEqual(elvart, kosar.Vegosszeg(szazalek));
    }

    // Kivétel tesztelése: a teszt akkor sikeres, ha a megadott típusú kivétel keletkezik.
    [TestMethod]
    [DataRow(0)]
    [DataRow(-100)]
    public void Hozzaad_NemPozitivAr_ArgumentOutOfRangeException(int ar)
    {
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => kosar.Hozzaad("Bögre", ar));
    }

    [TestMethod]
    [DataRow("")]
    [DataRow("   ")]
    [DataRow(null)]
    public void Hozzaad_UresNev_ArgumentException(string? nev)
    {
        Assert.ThrowsException<ArgumentException>(() => kosar.Hozzaad(nev!, 1000));
    }

    [TestMethod]
    [DataRow(-1)]
    [DataRow(51)]
    public void Vegosszeg_ErvenytelenKedvezmeny_Kivetel(int szazalek)
    {
        kosar.Hozzaad("Kabát", 20000);

        var kivetel = Assert.ThrowsException<ArgumentOutOfRangeException>(() => kosar.Vegosszeg(szazalek));
        Assert.AreEqual("kedvezmenySzazalek", kivetel.ParamName);
    }

    [TestMethod]
    public void Tetelek_HozzaadasUtan_ANevekSorrendbenSzerepelnek()
    {
        kosar.Hozzaad("Bögre", 2490);
        kosar.Hozzaad("Kanál", 590);

        var nevek = kosar.Tetelek.Select(t => t.Nev).ToList();

        CollectionAssert.AreEqual(new List<string> { "Bögre", "Kanál" }, nevek);
    }
}
