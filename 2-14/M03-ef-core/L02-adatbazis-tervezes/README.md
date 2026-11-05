---
id: 14-M03-L02
cim: Relációs adatbázis tervezése, MySQL
evfolyam: 2-14
modul: M03 – Adatbázis-kezelés Entity Framework Core-ral
orak: 25–28.
het:
oraszam: 4
elofeltetel: 14-M03-L01
leiras: Táblák, elsődleges és idegen kulcsok, kapcsolatok, AUTO_INCREMENT; MySQL Workbench.
---

# 14-M03-L02 · Relációs adatbázis tervezése, MySQL

## Célok

A lecke végére a tanuló:

- megmagyarázza a tábla, rekord, mező, elsődleges kulcs és idegen kulcs fogalmát;
- felismeri az 1:1, 1:N és N:M kapcsolatot, és az N:M kapcsolatot kapcsolótáblával valósítja meg;
- felismeri a redundanciát és az általa okozott anomáliákat, egy rossz táblát szétbont (normalizál, 3NF-ig);
- `CREATE TABLE` utasítással kulcsokat, `AUTO_INCREMENT`-et és idegenkulcs-megszorítást hoz létre;
- phpMyAdminban vagy MySQL Workbenchben importál egy SQL-szkriptet, és megnézi a táblák kapcsolatait.

## Elmélet

Az előző leckében az EF Core **egyetlen** táblát hozott létre nekünk. A valódi alkalmazások adatai azonban
több, egymással összekapcsolt táblában élnek. Mielőtt a C#-osztályokat megírnánk (Code First), vagy egy kész
adatbázisból legeneráltatnánk őket (Database First, 14-M03-L03), meg kell értenünk, **miért és hogyan bontjuk
táblákra** az adatokat.

### Alapfogalmak

| Fogalom | Jelentés | Példa |
|---|---|---|
| **egyed** (entitás) | a valós világ egy dolga, amelyről adatot tárolunk | egy vevő, egy pizza |
| **tulajdonság** (attribútum) | az egyed egy jellemzője | a pizza neve, ára |
| **tábla** (reláció) | azonos típusú egyedek gyűjteménye | `pizza` |
| **rekord** (sor) | egy konkrét egyed adatai | `(3, 'Hawaii', 2890)` |
| **mező** (oszlop) | egy tulajdonság az összes soron át | `ar` |
| **DBMS** | adatbázis-kezelő rendszer | MySQL / MariaDB (a XAMPP része) |

A **relációs** adatbázis táblákból áll, a táblák között pedig **kulcsok** teremtenek kapcsolatot.

### Kulcsok

- **Elsődleges kulcs (PRIMARY KEY, PK)**: olyan mező (vagy mezőcsoport), amely **minden sort egyértelműen azonosít**.
  Nem lehet `NULL`, és nem ismétlődhet.
  - *természetes kulcs*: a valóságban is egyedi adat (rendszám, irányítószám, ISBN);
  - *mesterséges kulcs*: egy sorszám, amelyet mi vezetünk be (`id`). Ez a leggyakoribb, mert sosem változik.
  - *összetett kulcs*: több mező együtt egyedi (pl. `rendeles_id + pizza_id`).
- **AUTO_INCREMENT**: a MySQL maga adja az `id`-t (1, 2, 3, …) – beszúráskor ezt az oszlopot kihagyjuk.
  Az EF Core is így hozta létre az `Id` oszlopot a 14-M03-L01-ben.
- **Alternatív kulcs (UNIQUE)**: nem elsődleges kulcs, de nem ismétlődhet (pl. telefonszám, e-mail).
- **Idegen kulcs (FOREIGN KEY, FK)**: olyan mező, amely **egy másik tábla elsődleges kulcsára hivatkozik**.
  Az idegen kulcs mindig a kapcsolat **„N” (gyerek) oldalán** van.

```
rendeles                                    vevo
┌────┬─────────┬─────────────────┐          ┌────┬─────────────────┐
│ id │ vevo_id │ idopont         │          │ id │ nev             │
├────┼─────────┼─────────────────┤    N:1   ├────┼─────────────────┤
│  1 │    1 ───┼─────────────────┼────────► │  1 │ Éhes Elemér     │
│  2 │    2    │ ...             │          │  2 │ Falatka Fanni   │
└────┴─────────┴─────────────────┘          └────┴─────────────────┘
          idegen kulcs (FK)                elsődleges kulcs (PK)
```

**Hivatkozási épség** (referenciális integritás): az InnoDB-motor nem enged olyan `vevo_id`-t beírni, amilyen vevő
nincs, és nem enged olyan vevőt törölni, akinek van rendelése (hacsak nem adtunk meg `ON DELETE CASCADE`-et).

### Kapcsolattípusok

| Típus | Jelentés | Megvalósítás | Példa |
|---|---|---|---|
| **1:1** | egy sorhoz legfeljebb egy sor tartozik a másik táblában | FK + `UNIQUE` az egyik táblában (vagy egy táblába vonjuk össze) | személy – személyi igazolvány |
| **1:N** | egy sorhoz több sor tartozhat a másik oldalon | FK az **N oldalon** | vármegye – település, vevő – rendelés |
| **N:M** | mindkét irányban több–több | **kapcsolótábla**, két FK-val, összetett PK-val | rendelés – pizza (`tetel`), tanuló – tantárgy |

Az N:M kapcsolatot a relációs modell közvetlenül **nem** tudja tárolni (egy mezőbe nem írhatunk listát), ezért
egy harmadik táblával két 1:N kapcsolatra bontjuk. A kapcsolat saját adatai (darabszám, dátum, jegy) is ide kerülnek.

**Leképezési szabályok röviden** (ER-modellből táblák):

1. minden egyedből tábla lesz, a kulcstulajdonságból elsődleges kulcs;
2. 1:N kapcsolat → az N oldali tábla kap egy idegen kulcsot az 1 oldal kulcsára;
3. N:M kapcsolat → új tábla a két kulccsal (összetett PK, mindkettő FK) és a kapcsolat tulajdonságaival;
4. 1:1 kapcsolat → az egyik tábla kap egy egyedi (`UNIQUE`) idegen kulcsot.

### Redundancia és anomáliák

Ha mindent egy táblába írunk, az adatok **ismétlődnek** (redundancia):

| alkalmazott | nev | foglalkozas_kod | foglalkozas |
|---|---|---|---|
| 101 | Kiss Ádám | bm | bányamérnök |
| 102 | Nagy Bea | va | vájár |
| 103 | Tóth Csaba | va | vájár |

Ebből három hiba (anomália) következik:

- **módosítási**: ha a „vájár” elnevezés változik, több sort kell átírni, és ha egyet kihagyunk, ellentmondás lesz;
- **törlési**: ha Nagy Bea és Tóth Csaba kilép, eltűnik az is, hogy létezik „vájár” foglalkozás;
- **bővítési**: új foglalkozást nem vehetünk fel, amíg nincs hozzá dolgozó (üres sorok kellenének).

Megoldás: a foglalkozás külön táblába kerül (`foglalkozas(kod PK, nev)`), az alkalmazott pedig csak a kódot tárolja (FK).

### Normalizálás (1NF – 3NF)

A normalizálás lépésenként szünteti meg a redundanciát. Gyakorlati megfogalmazás:

| Normálforma | Szabály | Ha nem teljesül… |
|---|---|---|
| **1NF** | minden mezőben **egyetlen** érték van, nincsenek ismétlődő oszlopok (`telefon1`, `telefon2`) | a listát külön sorokba / külön táblába tesszük |
| **2NF** | 1NF, és **összetett kulcsnál** minden többi mező a **teljes** kulcstól függ, nem csak egy részétől | a kulcs egy részétől függő mezőket külön táblába visszük |
| **3NF** | 2NF, és nincs **tranzitív függés**: nem kulcs mező nem függhet másik nem kulcs mezőtől | a „közvetítő” mezőt és a tőle függőket külön táblába visszük |

Példák:

- **nem 1NF**: `zenekar(nev, tagok)` – a `tagok` mezőben „Anna, Béla, Csaba” → külön `tag` tábla FK-val.
- **nem 2NF**: `eladas(datum, termek, mennyiseg, egysegar)`, kulcs: `datum + termek`. Az `egysegar` csak a terméktől
  függ → `termek(nev PK, egysegar)` külön táblába.
- **nem 3NF**: `telepules(irsz PK, nev, megye_id, megye_nev)`. A `megye_nev` a `megye_id`-tól függ, nem a kulcstól →
  `megye(id PK, nev)` külön táblába.

Ökölszabály: **minden adat egyszer szerepeljen**, a többi helyen csak hivatkozunk rá (kulccsal).

### A tervezés lépései

1. **Feladat megértése**: milyen adatokat kell tárolni, milyen kérdésekre kell válaszolni?
2. **Egyedek és tulajdonságok** összegyűjtése (főnevek a leírásban → egyedek; jellemzőik → tulajdonságok).
3. **Kapcsolatok** és számosságuk (1:1, 1:N, N:M) meghatározása – rajzolj ábrát!
4. **Táblák** a leképezési szabályokkal; kulcsok kijelölése.
5. **Ellenőrzés** normálformákkal; **adattípusok** megválasztása.
6. **Megvalósítás** SQL-lel vagy grafikus eszközzel.

### MySQL-adattípusok és a C# megfelelőjük

| MySQL | Mire | C# (EF Core) |
|---|---|---|
| `INT` | egész szám, azonosító | `int` |
| `DECIMAL(10,2)` | pénzösszeg pontosan | `decimal` |
| `DOUBLE` | tört szám (mérés) | `double` |
| `VARCHAR(n)` | legfeljebb *n* karakteres szöveg | `string` |
| `CHAR(n)` | pontosan *n* karakter (irányítószám) | `string` |
| `TEXT` | hosszú szöveg | `string` |
| `DATE` / `DATETIME` | dátum / dátum és idő | `DateOnly` / `DateTime` |
| `TINYINT(1)` / `BOOL` | logikai érték | `bool` |
| `NULL` engedélyezése | nem kötelező mező | `int?`, `string?` |

### SQL: táblák kulcsokkal

```sql
CREATE TABLE futar (
  id      INT NOT NULL AUTO_INCREMENT,
  nev     VARCHAR(40) NOT NULL,
  telefon VARCHAR(15) NOT NULL,
  PRIMARY KEY (id),
  UNIQUE KEY uq_futar_telefon (telefon)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

CREATE TABLE rendeles (
  id       INT NOT NULL AUTO_INCREMENT,
  futar_id INT NOT NULL,
  idopont  DATETIME NOT NULL,
  PRIMARY KEY (id),
  CONSTRAINT fk_rendeles_futar FOREIGN KEY (futar_id) REFERENCES futar (id)
);

-- utólag, meglévő táblára:
ALTER TABLE rendeles ADD CONSTRAINT fk_rendeles_futar FOREIGN KEY (futar_id) REFERENCES futar (id);
```

Az `ON DELETE` beállítás mondja meg, mi történjen a gyerek sorokkal, ha a szülőt törlik:
`RESTRICT` / `NO ACTION` (alapértelmezés: nem engedi), `CASCADE` (a gyerekek is törlődnek), `SET NULL` (az FK `NULL` lesz).

### Eszközök: phpMyAdmin és MySQL Workbench

| Feladat | phpMyAdmin (XAMPP, böngésző) | MySQL Workbench |
|---|---|---|
| SQL-szkript futtatása | **Importálás** fül, vagy **SQL** fül | *File → Run SQL Script…* / lekérdezőablak + ⚡ |
| Tábla tervezése | **Szerkezet** fül, *Index* oszlop: Elsődleges, Egyedi; *A_I* jelölő = AUTO_INCREMENT | jobb klikk a táblán → *Alter Table…*, PK / NN / AI jelölők |
| Idegen kulcs | **Szerkezet → Kapcsolati nézet** | *Alter Table → Foreign Keys* fül |
| Diagram | **Tervező** fül (táblák és kapcsolatok vonalakkal) | *Database → Reverse Engineer…* → EER-diagram; új modellnél *Forward Engineer* SQL-t generál |
| Exportálás | **Exportálás** fül → SQL | *Server → Data Export* |

Workbench-csatlakozás XAMPP-hoz: *Hostname* `127.0.0.1`, *Port* `3306`, *Username* `root`, jelszó üres.

### Kitekintés: hogyan látja ezt az EF Core?

| Adatbázis | EF Core (14-M03-L03, L04) |
|---|---|
| tábla | entitásosztály + `DbSet<T>` |
| elsődleges kulcs | `Id` tulajdonság (vagy `[Key]`) |
| idegen kulcs `megye_id` | `int MegyeId` tulajdonság |
| 1:N kapcsolat | navigációs tulajdonságok: `Fa.Megye` és `Megye.Fak` (lista) |

### Tipikus hibák

- **`Cannot add foreign key constraint` / `errno: 150`**: az FK és a hivatkozott PK típusa eltér (pl. `INT` vs.
  `INT UNSIGNED`, vagy eltérő karakterkészletű `VARCHAR`), vagy a hivatkozott mező nem kulcs.
- **`Cannot add or update a child row`**: olyan értéket írnál az FK-ba, amelyhez nincs szülő sor – vagy a szülőt
  később szúrod be. Feltöltés sorrendje: **előbb a szülő, aztán a gyerek**; törlésnél fordítva.
- **`Duplicate entry … for key 'PRIMARY'`**: ismétlődő kulcsérték; AUTO_INCREMENT-es oszlopnak ne adj értéket.
- **MyISAM tábla**: nem ismeri az idegen kulcsot (csendben figyelmen kívül hagyja) – használj `ENGINE=InnoDB`-t.
- **Ékezet, szóköz a tábla- és mezőnevekben** (`körméret`, `megye neve`): működik, de minden eszközben gondot okoz
  (backtick, generált C#-nevek). Használj ékezet nélküli, kisbetűs neveket: `kormeret`, `megye_nev`.
- **Rossz karakterkészlet**: `utf8mb4` + `utf8mb4_hungarian_ci`, különben az ékezetes adatok elromlanak, és a
  rendezés sem magyar ábécé szerint történik.

## Példák

| Példa | Mit mutat be |
|---|---|
| [peldak/pizzeria](peldak/pizzeria/) | öt tábla: 1:N kapcsolatok, N:M kapcsolótábla összetett kulccsal, `UNIQUE`, `ON DELETE CASCADE`; JOIN-os és csoportosító lekérdezések, idegenkulcs-hibák kipróbálása |

Futtatás: XAMPP-ban indítsd el a MySQL-t, phpMyAdminban importáld a `pizzeria.sql`-t, nézd meg a **Tervező** fület,
majd futtasd egyenként a `lekerdezesek.sql` utasításait.

## Feladatok

| Azonosító | Feladat | Nehézség | Megoldás |
|---|---|---|---|
| 14-M03-L02-F01 | [Zenetár normalizálása](feladatok/F01-zenetar-normalizalas/) | ★☆☆ | [megoldás](megoldasok/F01-zenetar-normalizalas/) |
| 14-M03-L02-F02 | [Könyvtárak: kulcsok és kapcsolatok utólag](feladatok/F02-konyvtarak-kulcsok/) | ★★☆ | [megoldás](megoldasok/F02-konyvtarak-kulcsok/) |
| 14-M03-L02-F03 | [Tanfolyami jelentkezések (N:M)](feladatok/F03-tanfolyam-jelentkezes/) | ★★★ | [megoldás](megoldasok/F03-tanfolyam-jelentkezes/) |

## Kapcsolódó

- Előző lecke: 14-M03-L01 (EF Core – Code First)
- Következő lecke: 14-M03-L03 (EF Core – Database First (Scaffold))
- Tanmenet: [2-14](../../../tanmenet/2-14.md)
