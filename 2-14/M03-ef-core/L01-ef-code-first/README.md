---
id: 14-M03-L01
cim: EF Core – Code First
evfolyam: 2-14
modul: M03 – Adatbázis-kezelés Entity Framework Core-ral
orak: 21–24.
het:
oraszam: 4
elofeltetel: 14-M02-L03
leiras: Entitásosztályok és DbContext, MySQL-kapcsolat, migrációk, adatok felvétele és lekérdezése konzolról.
---

# 14-M03-L01 · EF Core – Code First

## Célok

A lecke végére a tanuló:

- elmagyarázza, mit csinál egy ORM, és mi a különbség a Code First és a Database First megközelítés között;
- telepíti a szükséges NuGet-csomagokat, és MySQL-kapcsolatot állít be;
- entitásosztályból és `DbContext`-ből migrációval adatbázist hoz létre;
- C#-ból rekordot vesz fel, lekérdez, módosít és töröl (CRUD).

## Elmélet

### Mi az Entity Framework Core?

Az **EF Core** egy *ORM* (Object-Relational Mapper): C#-osztályokat feleltet meg adatbázistábláknak. SQL-utasítások írása helyett objektumokkal dolgozunk, és LINQ-lekérdezéseket írunk; ezekből az EF Core állítja elő az SQL-t.

| C# oldal | Adatbázis oldal |
|---|---|
| entitásosztály (`Konyv`) | tábla (`Konyvek`) |
| tulajdonság (`Cim`) | oszlop |
| objektum (`new Konyv {…}`) | sor (rekord) |
| `DbContext` | az adatbázis-kapcsolat |
| `DbSet<Konyv>` | a tábla, amelyen lekérdezünk |

### Code First vagy Database First?

- **Code First** *(ez a lecke)*: először a C#-osztályokat írjuk meg, és ezekből az EF Core **migrációkkal** hozza létre és frissíti az adatbázist.
- **Database First** *(14-M03-L03)*: már létező adatbázisból generáljuk az osztályokat (`Scaffold-DbContext`).

### A Code First lépései

1. **NuGet-csomagok** (Visual Studio: *Project → Manage NuGet Packages*), mindkettő **8.0.x** verzióban, a .NET 8-hoz illeszkedve:
   - `Pomelo.EntityFrameworkCore.MySql` – MySQL-szolgáltató (magával hozza az EF Core-t),
   - `Microsoft.EntityFrameworkCore.Tools` – a migrációs parancsokhoz.
2. **Entitásosztály** a `Models` mappában. Az `Id` nevű `int` tulajdonságból automatikusan elsődleges kulcs lesz (`AUTO_INCREMENT`). A `string?` / `int?` típusú tulajdonság NULL-t is felvehet, a többi kötelező.
3. **`DbContext`-leszármazott**: egy `DbSet<T>` tulajdonság táblánként, valamint az `OnConfiguring`-ban a kapcsolati szöveg:
   ```csharp
   optionsBuilder.UseMySql(kapcsolatiSzoveg, ServerVersion.AutoDetect(kapcsolatiSzoveg));
   ```
   XAMPP esetén: `server=localhost;database=estis_konyvtar;user=root;password=`. Az adatbázisnévben ne legyen ékezet és szóköz.
4. **Migráció** (*Tools → NuGet Package Manager → Package Manager Console*):
   ```
   Add-Migration Kezdo      ← a modell alapján elkészíti a változás leírását (Migrations mappa)
   Update-Database          ← végrehajtja az adatbázison (létrehozza az adatbázist is)
   ```
   Az entitás **minden későbbi módosítása után** új migráció és újabb `Update-Database` kell.
5. **Használat**: `using var db = new KonyvtarContext();`, majd LINQ-kel dolgozunk.

### CRUD műveletek

| Művelet | Kód | Megjegyzés |
|---|---|---|
| Felvétel | `db.Konyvek.Add(k);` / `AddRange(...)` | |
| Lekérdezés | `db.Konyvek.Where(...).OrderBy(...).ToList()` | a LINQ SQL-lé fordul |
| Módosítás | betöltött objektum tulajdonságának átírása | az EF Core követi a változást |
| Törlés | `db.Konyvek.Remove(k);` | |
| **Mentés** | `db.SaveChanges();` | **enélkül semmi nem kerül az adatbázisba!** |

### Tipikus hibák

- **„Unable to connect to any of the specified MySQL hosts”**: nem fut a MySQL (XAMPP Control Panel → MySQL → Start).
- **„Unknown column …”**: módosult az entitás, de elmaradt az `Add-Migration` és az `Update-Database`.
- **A `Add-Migration` nem ismert parancs**: hiányzik a `Microsoft.EntityFrameworkCore.Tools` csomag, vagy a Package Manager Console-ban nem a jó projekt van kiválasztva (*Default project*).
- **Verzióütközés**: a csomagok főverziója egyezzen a .NET-tel (.NET 8 → 8.0.x).

## Példák

| Projekt | Mit mutat be |
|---|---|
| [peldak/EfCodeFirstKonyvtar](peldak/EfCodeFirstKonyvtar/) | `Konyv` entitás, `KonyvtarContext`, mintaadatok felvétele, LINQ-lekérdezések, módosítás és törlés |

Futtatás: XAMPP-ban indítsd el a MySQL-t, majd a Package Manager Console-ban futtasd az `Add-Migration Kezdo` és az `Update-Database` parancsot, végül indítsd el a programot (F5). A `Migrations` mappa szándékosan nincs a repóban, ezt mindenki maga generálja.

## Feladatok

| Azonosító | Feladat | Nehézség | Megoldás |
|---|---|---|---|
| 14-M03-L01-F01 | [Filmtár Code First módszerrel](feladatok/F01-filmtar/) | ★★☆ | [megoldás](megoldasok/F01-filmtar/) |
| 14-M03-L01-F02 | [Új tulajdonság és migráció](feladatok/F02-uj-tulajdonsag-migracio/) | ★★☆ | [megoldás](megoldasok/F02-uj-tulajdonsag-migracio/) |

## Kapcsolódó

- Előző lecke: 14-M02-L03 (Integrált feladat: CSV-ből WPF-felület)
- Következő lecke: 14-M03-L02 (Relációs adatbázis tervezése, MySQL)
- Tanmenet: [2-14](../../../tanmenet/2-14.md)
