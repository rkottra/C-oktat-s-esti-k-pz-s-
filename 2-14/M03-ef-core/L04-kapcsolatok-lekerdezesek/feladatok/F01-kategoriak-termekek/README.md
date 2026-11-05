# 14-M03-L04-F01 · Webshop: kategóriák és termékek (Code First, 1:N)

**Nehézség:** ★★☆ **Becsült idő:** 50 perc

## Feladat

Készíts konzolos alkalmazást Code First módszerrel, amelyben két tábla között egy-a-többhöz kapcsolat van.

1. Új .NET 8 konzolos projekt `Webshop` néven, a szokásos két NuGet-csomaggal.
2. Entitások a `Models` mappában:
   - `Kategoria`: `Id`, `Nev`, és a kategória termékei (`List<Termek> Termekek`);
   - `Termek`: `Id`, `Nev`, `Ar`, `Keszlet`, idegen kulcs (`KategoriaId`) és navigáció (`Kategoria`).
3. `WebshopContext` két DbSet-tel, adatbázis: `estis_webshop`. Migráció: `Add-Migration Kezdo`, `Update-Database`.
   Nézd meg phpMyAdminban: milyen idegen kulcsot hozott létre az EF Core?
4. A program:
   1. ha üres az adatbázis, vegyen fel 4 kategóriát és legalább 8 terméket – a termékeket a kategória
      `Termekek` listájába téve; egy kategória (pl. „Játékok”) maradjon üres;
   2. írja ki a termékeket a kategória nevével, kategória, azon belül ár szerint rendezve;
   3. kategóriánként a termékek számát és átlagárát – az üres kategória is jelenjen meg;
   4. azokat a kategóriákat, amelyekben van elfogyott (0 készletű) termék;
   5. kategóriánként a legdrágább terméket;
   6. az üres kategóriába vegyen fel egy új terméket a navigációs listán keresztül.

## Minta kimenet

```
Termékek:
  Elektronika  Egér              5990 Ft
  Elektronika  Fülhallgató      12990 Ft
  ...
Kategóriák:
  Elektronika  3 termék, átlagár: 11323 Ft
  Könyv        2 termék, átlagár: 6990 Ft
  Háztartás    3 termék, átlagár: 6490 Ft
  Játékok      0 termék, átlagár: –

Elfogyott termék van: Elektronika, Háztartás

Legdrágább kategóriánként:
  Elektronika  Billentyűzet (14990 Ft)
  ...
Felvéve: Társasjáték (Játékok)
```

## Tippek

- Az `Include` használatához: `using Microsoft.EntityFrameworkCore;`
- Üres gyűjtemény átlaga kivételt dobna; `Average(t => (double?)t.Ar)` üres esetben `null`-t ad.
- A `KategoriaId`-t nem kell kézzel kitölteni, ha a terméket a `Kategoria.Termekek` listába teszed.
- `MaxBy` (.NET 6+) a legnagyobb értékű **elemet** adja vissza, nem csak az értéket.

Megoldás: [../../megoldasok/F01-kategoriak-termekek/](../../megoldasok/F01-kategoriak-termekek/)
