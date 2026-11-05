# 14-M03-L01-F01 · Filmtár Code First módszerrel

**Nehézség:** ★★☆ **Becsült idő:** 45 perc

## Feladat

Készíts konzolos alkalmazást, amely EF Core Code First módszerrel egy filmtár adatbázist hoz létre és kezel.

1. Hozz létre egy új .NET 8 konzolos projektet `Filmtar` néven, és telepítsd a NuGet-csomagokat:
   `Pomelo.EntityFrameworkCore.MySql` (8.0.x) és `Microsoft.EntityFrameworkCore.Tools` (8.0.x).
2. Készíts `Film` entitásosztályt a `Models` mappában:
   `Id` (int), `Cim` (szöveg), `Rendezo` (szöveg), `Ev` (egész), `Hossz` (perc, egész), `Ertekeles` (tört szám, nem kötelező).
3. Készíts `FilmtarContext` osztályt `Filmek` táblával; az adatbázis neve `estis_filmtar` legyen.
4. A Package Manager Console-ban hozd létre az adatbázist: `Add-Migration Kezdo`, majd `Update-Database`.
5. A program:
   - ha a tábla üres, vegyen fel legalább 5 filmet;
   - írja ki a filmeket év szerint csökkenő sorrendben;
   - írja ki a 2 óránál hosszabb filmek címét;
   - írja ki, hány filmnek nincs értékelése;
   - kérjen be egy rendezőnevet, és listázza a filmjeit (ha nincs ilyen, írjon üzenetet).

## Minta kimenet

```
Filmek (legújabb elöl):
  2023  Oppenheimer – Christopher Nolan (180 perc)
  ...
2 óránál hosszabb: Oppenheimer, A Gyűrűk Ura: A király visszatér
Értékelés nélkül: 1 film
Rendező: Nolan
  Oppenheimer (2023)
  Eredet (2010)
```

## Tippek

- Az értékelés legyen `double?`, így az oszlop NULL-t is felvehet.
- Részleges egyezéshez: `Where(f => f.Rendezo.Contains(nev))`.
- Ha módosítod az entitást, új migráció kell (lásd F02).

Megoldás: [../../megoldasok/F01-filmtar/](../../megoldasok/F01-filmtar/)
