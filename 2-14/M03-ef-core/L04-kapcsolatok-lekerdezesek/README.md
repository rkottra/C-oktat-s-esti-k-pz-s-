---
id: 14-M03-L04
cim: Kapcsolatok és LINQ-lekérdezések EF-ben
evfolyam: 2-14
modul: M03 – Adatbázis-kezelés Entity Framework Core-ral
orak: 33–36.
het:
oraszam: 4
elofeltetel: 14-M03-L03
leiras: Navigációs tulajdonságok, Include, egy-a-többhöz kapcsolatok, összetett lekérdezések.
---

# 14-M03-L04 · Kapcsolatok és LINQ-lekérdezések EF-ben

## Célok

A lecke végére a tanuló:

- Code First módszerrel két, egy-a-többhöz (1:N) kapcsolatban álló entitást hoz létre idegen kulccsal és navigációs tulajdonságokkal;
- kapcsolt adatokat tölt be `Include` és `ThenInclude` segítségével, és tudja, mikor **nem** kell `Include`;
- navigációs tulajdonságokon keresztül szűr, vetít, csoportosít és összesít (`Where`, `Select`, `Count`, `Sum`, `Any`, `GroupBy`);
- új „gyerek” rekordot vesz fel a navigációs listán vagy az idegen kulcson keresztül;
- egy scaffoldolt, több táblás (N:M kapcsolótáblás) adatbázison összetett lekérdezéseket ír.

## Elmélet

### Kapcsolat az adatbázisban és a C#-ban

A 14-M03-L02-ben láttuk: az 1:N kapcsolatot az **N oldali táblában lévő idegen kulcs** valósítja meg. Az EF Core
ugyanezt C#-ban **három tulajdonsággal** írja le:

```csharp
public class Szerzo                         // "1" oldal
{
    public int Id { get; set; }
    public string Nev { get; set; } = "";
    public List<Konyv> Konyvek { get; set; } = new();   // gyűjtemény-navigáció
}

public class Konyv                          // "N" oldal
{
    public int Id { get; set; }
    public string Cim { get; set; } = "";
    public int SzerzoId { get; set; }                     // idegenkulcs-tulajdonság → oszlop
    public Szerzo Szerzo { get; set; } = null!;           // hivatkozás-navigáció
}
```

| Tulajdonság | Van oszlopa? | Mire jó |
|---|---|---|
| `Konyv.SzerzoId` | **igen** (`SzerzoId`, FOREIGN KEY) | gyors hivatkozás, felvételkor elég megadni |
| `Konyv.Szerzo` | nem | a kapcsolt objektum: `k.Szerzo.Nev` |
| `Szerzo.Konyvek` | nem | a „gyerekek” listája: `sz.Konyvek.Count` |

**Konvenció:** ha a navigáció neve `Szerzo`, és van `SzerzoId` tulajdonság, az EF Core magától felismeri az
idegen kulcsot. A migráció (`Add-Migration`, `Update-Database`) létrehozza az oszlopot, az indexet és a
`FOREIGN KEY` megszorítást. Ha valamit másképp szeretnénk, a `OnModelCreating`-ben **Fluent API**-val írjuk le:

```csharp
modelBuilder.Entity<Konyv>()
    .HasOne(k => k.Szerzo)          // a könyvnek egy szerzője van
    .WithMany(s => s.Konyvek)       // a szerzőnek sok könyve
    .HasForeignKey(k => k.SzerzoId)
    .OnDelete(DeleteBehavior.Restrict);   // szerző nem törölhető, amíg van könyve
```

Database First esetén (14-M03-L03) a scaffold pontosan ilyen kódot generál a meglévő idegen kulcsokból
(`Fa.Megye`, `Megye.Fas`) – a lekérdezések ugyanúgy működnek.

### Betöltés: Include és ThenInclude

Az EF Core alapból **csak azt tölti be, amit kérünk**. A navigációs tulajdonság üres marad (`null`, illetve üres lista),
hacsak nem kérjük a kapcsolt adatokat is:

```csharp
using Microsoft.EntityFrameworkCore;     // az Include itt van!

var konyvek = db.Konyvek.Include(k => k.Szerzo).ToList();          // könyvek + szerzőik (JOIN)
var szerzok = db.Szerzok.Include(s => s.Konyvek).ToList();         // szerzők + könyveik
var rendelesek = db.Rendeles
    .Include(r => r.Vevo)
    .Include(r => r.Tetel).ThenInclude(t => t.Pizza)               // több szint: rendelés → tétel → pizza
    .ToList();
```

Szűrt / rendezett Include (EF Core 5+): `.Include(m => m.Fas.OrderByDescending(f => f.Kormeret))`.

### Mikor NEM kell Include?

Ha a navigációt a **lekérdezésen belül** használjuk (`Where`, `Select`, `OrderBy`, `GroupBy`), az EF Core
magától JOIN-t vagy al-lekérdezést készít:

```csharp
// csak a szükséges adatok jönnek át – Include nélkül
var lista = db.Konyvek
    .Where(k => k.Szerzo.SzuletesiEv < 1900)
    .Select(k => new { k.Cim, Szerzo = k.Szerzo.Nev })
    .ToList();
```

Ökölszabály: **objektumokat** akarunk (és a kapcsolt objektumot is) → `Include`; **csak néhány értéket** → `Select`.

### Lekérdezések a kapcsolaton át

| Kérdés | LINQ |
|---|---|
| szerzőnként a könyvek száma | `db.Szerzok.Select(s => new { s.Nev, Db = s.Konyvek.Count })` |
| szerzőnként az összár | `s.Konyvek.Sum(k => k.Ar)` |
| könyv nélküli szerzők | `db.Szerzok.Where(s => !s.Konyvek.Any())` |
| van-e 3000 Ft feletti könyve | `s.Konyvek.Any(k => k.Ar > 3000)` |
| csoportosítás a szülő neve szerint | `db.Fas.GroupBy(f => f.Megye.Nev).Select(g => new { g.Key, Db = g.Count() })` |
| két lépés „kilapítása” (vevő → rendelések → tételek) | `v.Rendeles.SelectMany(r => r.Tetel).Sum(t => t.Db * t.Pizza.Ar)` |
| különböző értékek száma csoportban | `g.Select(f => f.MegyeId).Distinct().Count()` |

Üres gyűjtemény `Average`/`Max` értéke kivételt dobna – nullázható típussal kerüljük el:
`m.Fas.Max(f => (int?)f.Kormeret)`.

### Felvétel kapcsolattal

```csharp
// a) a szülővel együtt, a navigációs listán át – az EF Core kitölti a SzerzoId-t
db.Szerzok.Add(new Szerzo { Nev = "Fekete István", Konyvek = { new Konyv { Cim = "Vuk" } } });

// b) meglévő szülőhöz, az idegen kulccsal
db.Konyvek.Add(new Konyv { Cim = "Abigél", SzerzoId = szabo.Id });

// c) meglévő, betöltött szülő listájába
szerzo.Konyvek.Add(new Konyv { Cim = "Kele" });

db.SaveChanges();
```

### Törlés kapcsolattal

| `OnDelete` | Mi történik a szülő törlésekor? |
|---|---|
| `Cascade` (kötelező kapcsolat alapértéke) | a gyerekek is törlődnek |
| `Restrict` / `NoAction` | hiba, amíg van gyerek |
| `SetNull` | a gyerekek idegen kulcsa `NULL` lesz (csak `int?` FK-nál) |

### N:M kapcsolat röviden

A pizzéria `tetel` táblája egy **kapcsolótábla saját adattal** (`db`); EF Core-ban ez egy önálló entitás
(`Tetel`) két 1:N kapcsolattal – így kérdezzük le az F03 feladatban is. Ha a kapcsolótáblának nincs saját adata,
Code First-ben elég mindkét oldalra egy-egy listát tenni (`List<Tantargy>` és `List<Tanulo>`), a kapcsolótáblát
az EF Core magától létrehozza.

### Tipikus hibák

- **`NullReferenceException` a `k.Szerzo.Nev`-nél**: kimaradt az `Include` (a memóriában lévő objektumokon a
  navigáció nincs betöltve).
- **Az `Include` nem található**: hiányzik a `using Microsoft.EntityFrameworkCore;`.
- **„The LINQ expression … could not be translated”**: saját metódust vagy számított tulajdonságot használtál a
  lekérdezésben. Tedd elé a `ToList()`-et, vagy írd át lefordítható formára.
- **„Cannot add or update a child row”**: nem létező szülő `Id`-t adtál meg idegen kulcsnak.
- **Végtelen ciklus kiírásnál / JSON-ban**: `Szerzo.Konyvek[0].Szerzo.Konyvek…` – a navigációk körbe mutatnak;
  kiíráskor csak az egyik irányt járd be.
- **Felesleges `Include`**: ha a végén úgyis `Select`-tel vetítesz, az `Include` hatástalan – nem hiba, de zavaró.

## Példák

| Projekt | Mit mutat be |
|---|---|
| [peldak/KonyvtarKapcsolatok](peldak/KonyvtarKapcsolatok/) | az L01 könyvtár-példájának továbbfejlesztése: `Szerzo` 1:N `Konyv` Code First-ben, Fluent API, felvétel navigáción át, `Include` mindkét irányban, navigáció `Where`/`Select`-ben, `Count`/`Sum`/`Any`, Include nélküli `null` bemutatása |
| [peldak/OregfakKapcsolatok](peldak/OregfakKapcsolatok/) | a scaffoldolt `estis_oregfak` adatbázis (L03): fák vármegyével (`Include`), szűrés a kapcsolt táblára, vármegyénkénti összesítés gyűjtemény-navigációval és `GroupBy`-jal, rendezett `Include` |

Futtatás: XAMPP-ban indítsd el a MySQL-t.
`KonyvtarKapcsolatok`: Package Manager Console-ban `Add-Migration Kezdo`, `Update-Database`, majd F5 (a `Migrations`
mappa szándékosan nincs a repóban). `OregfakKapcsolatok`: előbb importáld az
[oregfak.sql](../L03-database-first-scaffold/peldak/OregfakScaffold/oregfak.sql) szkriptet, aztán F5.

## Feladatok

| Azonosító | Feladat | Nehézség | Megoldás |
|---|---|---|---|
| 14-M03-L04-F01 | [Webshop: kategóriák és termékek (Code First, 1:N)](feladatok/F01-kategoriak-termekek/) | ★★☆ | [megoldás](megoldasok/F01-kategoriak-termekek/) |
| 14-M03-L04-F02 | [Öreg fák vármegyénként](feladatok/F02-oregfak-megyenkent/) | ★★☆ | [megoldás](megoldasok/F02-oregfak-megyenkent/) |
| 14-M03-L04-F03 | [Pizzéria-statisztika (több tábla, N:M)](feladatok/F03-pizzeria-statisztika/) | ★★★ | [megoldás](megoldasok/F03-pizzeria-statisztika/) |

## Kapcsolódó

- Előző lecke: 14-M03-L03 (EF Core – Database First (Scaffold))
- Következő lecke: 14-M04-L01 (Adatok megjelenítése: DataGrid, ComboBox) – a kapcsolt táblák WPF-felületen
- Tanmenet: [2-14](../../../tanmenet/2-14.md)
