using Bank;

namespace BankTesztek;

[TestClass]
public class BankszamlaTesztek
{
    // --- Konstruktor ---

    [TestMethod]
    public void Konstruktor_NyitoEgyenleggel_BeallitjaAzEgyenleget()
    {
        var szamla = new Bankszamla("Minta Anna", 5000);

        Assert.AreEqual("Minta Anna", szamla.Tulajdonos);
        Assert.AreEqual(5000, szamla.Egyenleg);
    }

    [TestMethod]
    public void Konstruktor_NyitoEgyenlegNelkul_Nulla()
    {
        Assert.AreEqual(0, new Bankszamla("Minta Anna").Egyenleg);
    }

    [TestMethod]
    public void Konstruktor_NegativNyitoEgyenleg_Kivetel()
    {
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Bankszamla("Minta Anna", -1));
    }

    // --- Befizetés ---

    [TestMethod]
    public void Befizet_PozitivOsszeg_NoAzEgyenleg()
    {
        var szamla = new Bankszamla("Minta Anna", 1000);

        szamla.Befizet(500);

        Assert.AreEqual(1500, szamla.Egyenleg);
    }

    [TestMethod]
    [DataRow(0)]
    [DataRow(-500)]
    public void Befizet_NemPozitivOsszeg_KivetelEsNemValtozikAzEgyenleg(int osszeg)
    {
        var szamla = new Bankszamla("Minta Anna", 1000);

        Assert.ThrowsException<ArgumentOutOfRangeException>(() => szamla.Befizet(osszeg));
        Assert.AreEqual(1000, szamla.Egyenleg);
    }

    // --- Kivét: határeset az egyenleg körül ---

    [TestMethod]
    [DataRow(999, 1)]
    [DataRow(1000, 0)]   // a teljes egyenleg felvehető – a kiinduló kód itt hibázott
    public void Kivesz_FedezetenBelul_CsokkenAzEgyenleg(int osszeg, int elvartEgyenleg)
    {
        var szamla = new Bankszamla("Minta Anna", 1000);

        szamla.Kivesz(osszeg);

        Assert.AreEqual(elvartEgyenleg, szamla.Egyenleg);
    }

    [TestMethod]
    public void Kivesz_TobbMintAzEgyenleg_InvalidOperationException()
    {
        var szamla = new Bankszamla("Minta Anna", 1000);

        Assert.ThrowsException<InvalidOperationException>(() => szamla.Kivesz(1001));
        Assert.AreEqual(1000, szamla.Egyenleg);
    }

    [TestMethod]
    [DataRow(0)]
    [DataRow(-1)]
    public void Kivesz_NemPozitivOsszeg_Kivetel(int osszeg)
    {
        var szamla = new Bankszamla("Minta Anna", 1000);

        Assert.ThrowsException<ArgumentOutOfRangeException>(() => szamla.Kivesz(osszeg));
    }

    // --- Átutalás ---

    [TestMethod]
    public void Atutal_VanFedezet_MindketEgyenlegValtozik()
    {
        var anna = new Bankszamla("Minta Anna", 10000);
        var bela = new Bankszamla("Teszt Béla", 2000);

        anna.Atutal(bela, 3000);

        Assert.AreEqual(7000, anna.Egyenleg);
        Assert.AreEqual(5000, bela.Egyenleg);
    }

    [TestMethod]
    public void Atutal_TeljesEgyenleg_Sikeres()
    {
        var anna = new Bankszamla("Minta Anna", 10000);
        var bela = new Bankszamla("Teszt Béla");

        anna.Atutal(bela, 10000);

        Assert.AreEqual(0, anna.Egyenleg);
        Assert.AreEqual(10000, bela.Egyenleg);
    }

    // A legfontosabb: sikertelen átutalás után SEMMI ne változzon (egyik számlán sem).
    [TestMethod]
    public void Atutal_NincsFedezet_KivetelEsEgyikEgyenlegSemValtozik()
    {
        var anna = new Bankszamla("Minta Anna", 1000);
        var bela = new Bankszamla("Teszt Béla", 2000);

        Assert.ThrowsException<InvalidOperationException>(() => anna.Atutal(bela, 5000));

        Assert.AreEqual(1000, anna.Egyenleg, "A küldő egyenlege nem változhat.");
        Assert.AreEqual(2000, bela.Egyenleg, "A fogadó nem kaphat pénzt sikertelen utaláskor.");
    }

    [TestMethod]
    public void Atutal_NullCel_ArgumentNullException()
    {
        var anna = new Bankszamla("Minta Anna", 1000);

        Assert.ThrowsException<ArgumentNullException>(() => anna.Atutal(null!, 100));
    }

    [TestMethod]
    public void Atutal_SajatSzamlara_InvalidOperationException()
    {
        var anna = new Bankszamla("Minta Anna", 1000);

        Assert.ThrowsException<InvalidOperationException>(() => anna.Atutal(anna, 100));
        Assert.AreEqual(1000, anna.Egyenleg);
    }
}
