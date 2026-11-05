# 14-M05-L01-F01 · Rétegekre bontás

**Nehézség:** ★★☆ **Becsült idő:** 60 perc

## Feladat

A [kiindulo](kiindulo/) mappában egy működő, de „egy tömbben” megírt raktárkészlet-program van (`KeszletMonolit`):
beolvassa a `termekek.csv` fájlt négy párhuzamos listába, és mindent a `Program.cs`-ben számol ki.
Alakítsd át rétegzett szerkezetűvé úgy, hogy **a kimenet ne változzon**.

1. Hozz létre új .NET 8 konzolos projektet `RetegzettKeszlet` néven, és másold bele a `termekek.csv`-t
   (*Properties → Copy to Output Directory: Copy if newer*).
2. **Modell** (`Modell` mappa): `Termek` osztály `Nev`, `Kategoria`, `Ar`, `Keszlet` tulajdonsággal,
   valamint egy számított `Keszletertek` tulajdonsággal (ár × készlet).
3. **Adatréteg** (`Adatretag` mappa):
   - `ITermekRepository` interfész egy `List<Termek> Osszes()` metódussal;
   - `CsvTermekRepository`, amely a konstruktorban kapja a fájlnevet. A hibás sorokat **ne írja ki**,
     hanem gyűjtse egy `HibasSorok` listába.
4. **Üzleti logika** (`UzletiLogika` mappa): `KeszletSzolgaltatas`, amely a konstruktorban egy `ITermekRepository`-t kap. Metódusai:
   `Utanrendelendok()` (5 db alatti készlet), `KeszletertekKategoriankent()`, `Legdragabb()`, `Kategoriaban(string)`.
   Az 5-ös határ legyen elnevezett konstans.
5. **Megjelenítés** (`Program.cs`): csak `Console.WriteLine` és `Console.ReadLine` maradjon benne, számolás ne.

## Minta kimenet (bemenet: `periféria`)

```
Hibás sor: Egérpad XL;Kiegészítő;4490;-
Beolvasott termékek: 10

Utánrendelendő (5 db alatt):
  Mechanikus billentyűzet – 3 db
  24 colos monitor – 0 db
  ...
Legdrágább termék: 27 colos monitor (79 990 Ft)

Kategória: periféria
  Vezeték nélküli egér: 6 990 Ft, 14 db
  ...
```

## Tippek

- Ellenőrzés: futtasd a régi és az új programot ugyanazzal a bemenettel, és vesd össze a kimenetet.
- A `for` ciklusok helyett használj LINQ-t: `Where`, `GroupBy` + `Sum`, `MaxBy`.
- Kérdezd meg minden sorról: *„Ez megjelenítés, számolás vagy adatelérés?”* – ebből tudod, melyik rétegbe kerül.

Megoldás: [../../megoldasok/F01-retegekre-bontas/](../../megoldasok/F01-retegekre-bontas/)
