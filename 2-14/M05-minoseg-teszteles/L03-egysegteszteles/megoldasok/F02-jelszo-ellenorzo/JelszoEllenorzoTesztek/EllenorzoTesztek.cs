using JelszoEllenorzo;

namespace JelszoEllenorzoTesztek;

[TestClass]
public class EllenorzoTesztek
{
    [TestMethod]
    [DataRow("Alma1234")]        // pontosan 8 karakter – határeset
    [DataRow("Biztonsagos2026")]
    [DataRow("ÁrvíztűrőTükör9")] // ékezetes nagybetű is nagybetű
    public void Eros_MindenSzabalyTeljesul_Igaz(string jelszo)
    {
        Assert.IsTrue(Ellenorzo.Eros(jelszo), $"„{jelszo}” erős kellene legyen, hibák: {string.Join(" ", Ellenorzo.Hibak(jelszo))}");
    }

    [TestMethod]
    public void Hibak_ErosJelszo_UresLista()
    {
        Assert.AreEqual(0, Ellenorzo.Hibak("Alma1234").Count);
    }

    // Minden szabályt külön teszt ellenőriz, olyan jelszóval, amely CSAK azt az egy szabályt sérti.
    [TestMethod]
    public void Hibak_HetKarakter_TulRovid()
    {
        CollectionAssert.AreEqual(new List<string> { Ellenorzo.TulRovid }, Ellenorzo.Hibak("Alma123"));
    }

    [TestMethod]
    public void Hibak_NincsNagybetu_CsakEzAHiba()
    {
        CollectionAssert.AreEqual(new List<string> { Ellenorzo.NincsNagybetu }, Ellenorzo.Hibak("alma1234"));
    }

    [TestMethod]
    public void Hibak_NincsSzamjegy_CsakEzAHiba()
    {
        CollectionAssert.AreEqual(new List<string> { Ellenorzo.NincsSzamjegy }, Ellenorzo.Hibak("Almafaliget"));
    }

    [TestMethod]
    public void Hibak_SzokozVanBenne_CsakEzAHiba()
    {
        CollectionAssert.AreEqual(new List<string> { Ellenorzo.SzokozVanBenne }, Ellenorzo.Hibak("Alma 1234"));
    }

    [TestMethod]
    public void Hibak_UresSzoveg_HaromHiba()
    {
        var hibak = Ellenorzo.Hibak("");

        Assert.AreEqual(3, hibak.Count);
        CollectionAssert.Contains(hibak, Ellenorzo.TulRovid);
        CollectionAssert.Contains(hibak, Ellenorzo.NincsNagybetu);
        CollectionAssert.Contains(hibak, Ellenorzo.NincsSzamjegy);
        CollectionAssert.DoesNotContain(hibak, Ellenorzo.SzokozVanBenne);
    }

    [TestMethod]
    public void Hibak_Null_ArgumentNullException()
    {
        Assert.ThrowsException<ArgumentNullException>(() => Ellenorzo.Hibak(null!));
    }
}
