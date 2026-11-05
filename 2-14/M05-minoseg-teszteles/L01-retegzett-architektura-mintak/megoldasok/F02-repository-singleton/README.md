# Megoldás – 14-M05-L01-F02 · Repository-csere és Singleton

Az F01 megoldására épül. Újdonságok:

- `Adatretag/MemoriaTermekRepository.cs` – a második `ITermekRepository`-megvalósítás. A `KeszletSzolgaltatas`-t a repository-csere miatt nem kellett módosítani (csak a 4. lépés, a határ forrása miatt változott egy sora).
- `Beallitasok.cs` – Singleton (privát konstruktor, `static readonly` példány, `sealed` osztály).
- `Program.cs` – a felhasználó választja ki az adatforrást; a `ReferenceEquals` igazolja, hogy a beállításokból egy példány van.

Ellenőrző kérdés megoldása: a `new Beallitasok()` sor fordítási hibát ad (*„inaccessible due to its protection level”*), mert a konstruktor privát.

MySQL-re váltásnál elég egy új `EfTermekRepository : ITermekRepository` osztály (a `DbContext`-en keresztül olvas), és a `Program.cs`-ben egy sor módosítása. A `Termek`, a `KeszletSzolgaltatas` és a kiíró kód változatlan marad.
