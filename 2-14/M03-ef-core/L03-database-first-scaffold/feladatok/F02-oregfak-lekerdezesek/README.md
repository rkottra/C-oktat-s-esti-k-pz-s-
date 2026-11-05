# 14-M03-L03-F02 · Öreg fák: scaffold kapcsolókkal és lekérdezések

**Nehézség:** ★★☆ **Becsült idő:** 50 perc

## Feladat

Az `estis_oregfak` adatbázist a bemutató példából ismered (ha még nincs meg, importáld a
[peldak/OregfakScaffold/oregfak.sql](../../peldak/OregfakScaffold/oregfak.sql) szkriptet).
Most te generálod le a modellt – a kapcsolók segítségével rendezettebb szerkezetben.

1. Új .NET 8 konzolos projekt `OregfakLekerdezesek` néven, a szokásos két NuGet-csomaggal.
2. Scaffold úgy, hogy
   - az entitások a `Models`, a context a `Data` mappába kerüljön (`-OutputDir`, `-ContextDir`);
   - a context neve `OregfakContext` legyen (`-Context`);
   - a DbSet-ek neve ne kapjon angol többes számot (`-NoPluralize`).
3. Készíts `Kiegeszitesek/FaKiegeszites.cs` fájlt, amelyben a `Fa` osztályt egy `Kategoria` nevű, csak olvasható
   tulajdonsággal bővíted: „óriás” (≥ 700 cm), „vastag” (≥ 500 cm), egyébként „átlagos”.
4. A program írja ki:
   1. hány különböző fafaj szerepel az adatbázisban;
   2. a legvastagabb fa faját, törzskerületét, települését és a mérés évét;
   3. fajonként a példányszámot és az átlagos törzskerületet, csak a legalább 5 példánnyal rendelkező fajokra,
      átlag szerint csökkenő sorrendben;
   4. mérési évenként a fák számát, egyszerű „csillagos” diagrammal;
   5. kategóriánként a fák számát;
   6. kérjen be egy fajnevet, és írja ki a legvastagabb példány települését (vagy hogy nincs ilyen faj).

## Minta kimenet

```
Különböző fajok: 32
Legvastagabb: fehér nyár, 1013 cm, Süttő (2006)

Átlagos törzskerület (legalább 5 példány):
  fekete nyár           12 db   717 cm
  szelídgesztenye       10 db   680 cm
  ...
Mérések évenként:
  2000: * 1
  2001: * 1
  2002: **** 4
  ...
Kategóriák:
  átlagos: 63, vastag: 79, óriás: 16

Faj: gyertyán
  A legvastagabb: Monostorapáti, 426 cm
```

## Tippek

- A parancs egy sorba írva: `Scaffold-DbContext "…" Pomelo.EntityFrameworkCore.MySql -OutputDir Models -ContextDir Data -Context OregfakContext -NoPluralize`
- A partial osztály névterének egyeznie kell a generált osztályéval (`OregfakLekerdezesek.Models`).
- A `Kategoria` nem oszlop, ezért az EF Core nem tudja SQL-lé fordítani: előbb `ToList()`, utána `GroupBy`.
- `GroupBy` után a `Where(g => g.Count() >= 5)` SQL-ben `HAVING`-gé fordul.

Megoldás: [../../megoldasok/F02-oregfak-lekerdezesek/](../../megoldasok/F02-oregfak-lekerdezesek/)
