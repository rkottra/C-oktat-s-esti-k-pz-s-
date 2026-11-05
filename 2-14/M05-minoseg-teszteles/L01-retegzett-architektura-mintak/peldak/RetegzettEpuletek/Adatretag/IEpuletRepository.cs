using RetegzettEpuletek.Modell;

namespace RetegzettEpuletek.Adatretag;

// Repository minta: az üzleti logika csak ezt a felületet (interfészt) ismeri,
// azt nem, hogy az adat fájlból, adatbázisból vagy a memóriából jön.
public interface IEpuletRepository
{
    List<Epulet> Osszes();
    void Hozzaad(Epulet epulet);
}
