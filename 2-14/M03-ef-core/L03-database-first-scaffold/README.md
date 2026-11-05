---
id: 14-M03-L03
cim: EF Core – Database First (Scaffold)
evfolyam: 2-14
modul: M03 – Adatbázis-kezelés Entity Framework Core-ral
orak: 29–32.
het:
oraszam: 4
elofeltetel: 14-M03-L02
leiras: NuGet-csomagok, Scaffold-DbContext meglévő adatbázisból, a generált osztályok, újragenerálás -Force kapcsolóval.
---

# 14-M03-L03 · EF Core – Database First (Scaffold)

## Célok

A lecke végére a tanuló:

- elmagyarázza, mikor érdemes Database First megközelítést választani;
- egy meglévő MySQL-adatbázisból a `Scaffold-DbContext` paranccsal C#-osztályokat generál;
- eligazodik a generált context és entitásosztályok kódjában (kulcsok, oszlopnevek, kapcsolatok);
- a generált osztályokat `partial` osztállyal bővíti úgy, hogy az újragenerálás ne írja felül;
- az adatbázis módosítása után `-Force` kapcsolóval újragenerálja a modellt.

## Elmélet

### Mikor Database First?

A 14-M03-L01-ben a C#-osztályokból készült az adatbázis (**Code First**). Gyakran azonban **már létezik** az
adatbázis: egy másik program használja, a rendszergazda tervezte, vagy – mint a vizsgákon – egy kész `.sql`
fájlt kapunk. Ilyenkor az irány megfordul: az adatbázisból generáljuk a C#-kódot (**Database First**, angolul
*reverse engineering* vagy *scaffolding*).

| | Code First (L01) | Database First (ez a lecke) |
|---|---|---|
| Kiindulás | C#-osztályok | kész adatbázis (pl. `.sql` szkriptből) |
| Irány | C# → adatbázis | adatbázis → C# |
| Eszköz | `Add-Migration`, `Update-Database` | `Scaffold-DbContext` |
| Változáskor | módosítod az osztályt, új migráció | módosul az adatbázis, újragenerálás `-Force`-szal |
| `Migrations` mappa | van | nincs |

### Előkészületek

1. Az adatbázis legyen meg: XAMPP-ban fusson a MySQL, és importáld a szkriptet (pl. [oregfak.sql](peldak/OregfakScaffold/oregfak.sql)).
2. Új .NET 8 konzolos (később WPF) projekt, NuGet-csomagok **8.0.x** verzióban – ugyanazok, mint Code Firstnél:
   - `Pomelo.EntityFrameworkCore.MySql` – a MySQL-szolgáltató; a scaffold is ezt használja az adatbázis „kiolvasásához”;
   - `Microsoft.EntityFrameworkCore.Tools` – a Package Manager Console parancsai (magával hozza a `…Design` csomagot).
3. **Fordítsd le** a projektet (Ctrl+Shift+B) – hibás projektre a scaffold nem fut le.

### A Scaffold-DbContext parancs

*Tools → NuGet Package Manager → Package Manager Console*, egyetlen sorban:

```
Scaffold-DbContext "server=localhost;database=estis_oregfak;user=root;password=" Pomelo.EntityFrameworkCore.MySql -OutputDir Models -Force
```

| Rész | Jelentés |
|---|---|
| `"server=…;database=…;user=…;password="` | a kapcsolati szöveg – ugyanaz, mint az L01-ben az `OnConfiguring`-ban |
| `Pomelo.EntityFrameworkCore.MySql` | a szolgáltató (provider) csomag neve |
| `-OutputDir Models` | ebbe a mappába kerülnek a generált fájlok |
| `-Force` | felülírja a már létező generált fájlokat (újrageneráláskor kell) |

Hasznos további kapcsolók:

| Kapcsoló | Hatás |
|---|---|
| `-Context OregfakContext` | saját név a context osztálynak (alapból az adatbázis nevéből: `EstisOregfakContext`) |
| `-ContextDir Data` | a context külön mappába kerül |
| `-Tables fa,megye` | csak a felsorolt táblákat generálja |
| `-NoPluralize` | kikapcsolja az angol többes szám / egyes szám képzést (magyar tábláknál ajánlott!) |
| `-DataAnnotations` | a beállítások egy része attribútumként (`[Key]`, `[Column]`, `[StringLength]`) kerül az osztályokba |
| `-NoOnConfiguring` | nem írja a kapcsolati szöveget a kódba |

Ugyanez parancssorból (a `dotnet-ef` eszköz telepítése után):
`dotnet ef dbcontext scaffold "server=…" Pomelo.EntityFrameworkCore.MySql -o Models -f`

### Mit generál?

Az `estis_oregfak` adatbázis két táblájából (`megye` 1:N `fa`) három fájl készül:

```
Models/
├─ EstisOregfakContext.cs   a DbContext: DbSet-ek, kapcsolati szöveg, OnModelCreating
├─ Fa.cs                    entitás a "fa" táblához
└─ Megye.cs                 entitás a "megye" táblához
```

**Entitásosztály** (`Fa.cs`) – minden oszlopból egy tulajdonság, a `snake_case` nevekből `PascalCase`:

```csharp
public partial class Fa
{
    public int Id { get; set; }                 // id (PRIMARY KEY)
    public string Faj { get; set; } = null!;    // faj VARCHAR(50) NOT NULL
    public int MegyeId { get; set; }            // megye_id – az idegen kulcs
    public int? MeresEve { get; set; }          // meres_eve INT NULL → int?
    public virtual Megye Megye { get; set; } = null!;   // navigációs tulajdonság (14-M03-L04)
}
```

- `= null!;` – „megígérjük” a fordítónak, hogy az EF Core majd kitölti (NOT NULL oszlop).
- `int?` / `string?` – az oszlop `NULL`-t is megenged.
- `virtual Megye Megye` és a `Megye` osztályban `ICollection<Fa> Fas` – a kapcsolat két vége, a következő lecke témája.
- `partial` – az osztály több fájlban folytatható (lásd lent).

**Context** (`EstisOregfakContext.cs`):

- `DbSet<Fa> Fas`, `DbSet<Megye> Megyes` – a táblák. A furcsa nevek az **angol többesszám-képzésből** jönnek;
  `-NoPluralize`-zal `Fa` és `Megye` lesz belőlük.
- `OnConfiguring` – a kapcsolati szöveg. Előtte egy `#warning` figyelmeztet, hogy jelszót ne tároljunk a forráskódban.
  Iskolai, helyi adatbázisnál (root, üres jelszó) ez elfogadható; a figyelmeztetés sora törölhető.
- `ServerVersion.Parse("10.4.32-mariadb")` – a scaffold a kapcsolódáskor kiolvasott szerververziót írja be
  (a XAMPP valójában MariaDB-t tartalmaz).
- `OnModelCreating` – **Fluent API**: ide kerül minden, ami a névkonvenciókból nem következik:
  `ToTable("fa")`, `HasColumnName("megye_id")`, `HasMaxLength(50)`, `HasKey(...)`, és a kapcsolat:
  ```csharp
  entity.HasOne(d => d.Megye).WithMany(p => p.Fas)
      .HasForeignKey(d => d.MegyeId)
      .HasConstraintName("fk_fa_megye");
  ```
  Összetett kulcsnál (pl. a pizzéria `tetel` táblája): `entity.HasKey(e => new { e.RendelesId, e.PizzaId })`.

### A generált kód bővítése: partial osztály

A generált fájlokat **ne szerkeszd kézzel**: a következő `-Force` újragenerálás mindent felülír bennük.
Saját metódust, számított tulajdonságot, `ToString`-et egy **külön fájlba**, ugyanabba a névtérbe írt `partial`
osztályba tegyél:

```csharp
// Kiegeszitesek/FaKiegeszites.cs
namespace OregfakScaffold.Models;     // ugyanaz a névtér, mint a generált Fa-é!

public partial class Fa
{
    public double KormeretMeterben => Kormeret / 100.0;   // csak getter → nem lesz oszlop
    public override string ToString() => $"{Faj} {Kormeret} cm {Telepules}";
}
```

A számított tulajdonságot az EF Core nem tudja SQL-re fordítani: `Where`-ben, `OrderBy`-ban csak `ToList()` után használd.

### Újragenerálás

Ha módosul az adatbázis (új oszlop, új tábla), futtasd újra ugyanazt a parancsot **`-Force`-szal**. A `Models`
mappa generált fájljai felülíródnak, a saját `partial` fájljaid megmaradnak.

### Tipikus hibák

- **„The term 'Scaffold-DbContext' is not recognized”**: hiányzik a `Microsoft.EntityFrameworkCore.Tools` csomag,
  vagy nem a Package Manager Console-ban gépelted.
- **„Build failed.”**: a scaffold előtt a projektnek hibátlanul fordulnia kell (Error List!).
- **„Unable to connect to any of the specified MySQL hosts”**: nem fut a MySQL. **„Unknown database”**: elírt adatbázisnév.
- **„The following file(s) already exist…”**: kimaradt a `-Force`.
- **Kulcs nélküli tábla**: ha a táblának nincs elsődleges kulcsa, az entitás `HasNoKey()`-t kap, és **nem lehet
  módosítani** rajta keresztül. Megoldás: előbb adj kulcsot a táblának (14-M03-L02), aztán scaffoldolj újra.
- **Ékezetes oszlopnevek** (`körméret`): ékezetes C#-tulajdonságok lesznek (`Körméret`). Működik, de kerüld;
  ha teheted, nevezd át az oszlopokat ékezet nélkülire.
- **Furcsa osztálynevek** (`Rendele`, `Fas`): az angol egyes/többes szám képzése – használd a `-NoPluralize` kapcsolót.
- **Verzióeltérés**: a két NuGet-csomag főverziója egyezzen (.NET 8 → 8.0.x).

## Példák

| Projekt | Mit mutat be |
|---|---|
| [peldak/OregfakScaffold](peldak/OregfakScaffold/) | az `oregfak.sql` adatbázisból generált context és entitások (`Models`), `partial` kiegészítés (`Kiegeszitesek`), lekérdezések: rendezés, összesítés, csoportosítás, keresés |

Futtatás: XAMPP-ban indítsd el a MySQL-t, importáld az [oregfak.sql](peldak/OregfakScaffold/oregfak.sql) szkriptet,
majd indítsd a programot (F5). A `Models` mappa tartalmát a fenti paranccsal magad is előállíthatod – töröld a mappát,
és futtasd a `Scaffold-DbContext` parancsot (az eredmény a szerververziótól függően apróságokban eltérhet).

## Feladatok

| Azonosító | Feladat | Nehézség | Megoldás |
|---|---|---|---|
| 14-M03-L03-F01 | [Pizzéria adatbázis scaffoldolása](feladatok/F01-pizzeria-scaffold/) | ★☆☆ | [megoldás](megoldasok/F01-pizzeria-scaffold/) |
| 14-M03-L03-F02 | [Öreg fák: scaffold kapcsolókkal és lekérdezések](feladatok/F02-oregfak-lekerdezesek/) | ★★☆ | [megoldás](megoldasok/F02-oregfak-lekerdezesek/) |
| 14-M03-L03-F03 | [Változott az adatbázis: újragenerálás -Force kapcsolóval](feladatok/F03-ujrageneralas-force/) | ★★☆ | [megoldás](megoldasok/F03-ujrageneralas-force/) |

## Kapcsolódó

- Előző lecke: 14-M03-L02 (Relációs adatbázis tervezése, MySQL)
- Következő lecke: 14-M03-L04 (Kapcsolatok és LINQ-lekérdezések EF-ben)
- Tanmenet: [2-14](../../../tanmenet/2-14.md)
