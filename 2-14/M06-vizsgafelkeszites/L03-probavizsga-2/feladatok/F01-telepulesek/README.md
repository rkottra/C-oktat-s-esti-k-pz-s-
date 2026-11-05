# 14-M06-L03-F01 · 2. próbavizsga: Magyarország városai

**Nehézség:** ★★★ **Időkeret:** 150 perc (ajánlott: konzolos rész 60, grafikus rész 90 perc) **Elérhető pontszám:** 60 pont

## Bevezetés

A feladatban Magyarország 346 városának nyilvános (KSH-alapú) adatait dolgozod fel. A feladat két, egymástól független részből áll.

Rendelkezésre álló állományok:

| Állomány | Tartalom |
|---|---|
| [settlements.csv](settlements.csv) | pontosvesszővel tagolt, UTF-8, első sor fejléc, tizedes **pont** |
| [settlements.json](settlements.json) | ugyanaz JSON-tömbként |
| [settlements.sql](settlements.sql) | az `estis_telepulesek` adatbázis két táblával (`county`, `settlement`) |
| [kiindulo/TelepulesekWpf](kiindulo/TelepulesekWpf/) | kiinduló WPF-projekt `Megye` és `Telepules` modellosztállyal |

A CSV/JSON mezői: `id` (egész), `name` (név), `countyname` (megye), `region` (járás; Budapestnél üres), `population` (lakosság, fő), `areasize` (terület, km², valós szám).

```
id;name;countyname;region;population;areasize
1;Aba;Fejér;Székesfehérvári;4619;88.05
```

**Általános szabályok:** a részeredmények előtt jelenjen meg a feladat sorszáma; a kiírások a mintát kövessék; ékezet nélküli azonosítók elfogadottak; a program más, azonos szerkezetű adatokkal is helyesen működjön.

## I. Konzolos rész (25 pont)

1. **(2 pont)** Hozz létre `TelepulesekKonzol` nevű konzolos projektet; a választott adatfájl (CSV **vagy** JSON) futáskor a program mellé másolódjon.
2. **(5 pont)** Készíts `Telepules` osztályt (`Id`, `Nev`, `Megye`, `Jaras`, `Nepesseg`, `Terulet`) és a választott formátumnak megfelelő statikus betöltő metódust: `BetoltCsv(string fajlnev)` vagy `BetoltJson(string fajlnev)`, amely `List<Telepules>`-t ad vissza. Elég az egyiket elkészíteni.
3. **(2 pont)** Írd ki a városok számát.
4. **(4 pont)** Határozd meg, melyik megye városaiban él összesen a legkevesebb ember. Írd ki a megye nevét, a városok összlakosságát és összterületét (két tizedesjeggyel).
5. **(4 pont)** Írd ki a legnagyobb népsűrűségű (lakos / km²) város nevét és népsűrűségét egészre kerekítve.
6. **(3 pont)** Kérj be egy városnevet (a kis- és nagybetű ne számítson). Ha van ilyen város, írd ki a megyéjét, a járását (ha nincs, `–`), a lakosságát és a népsűrűségét; ha nincs, írd ki: `Nincs ilyen város az állományban.`
7. **(5 pont)** Készítsd el a `telepulesek_megyenkent.txt` állományt: a megyék ábécérendben szerepeljenek, mindegyik után a járásai száma (üres járásnév nem számít), alatta tabulátorral beljebb a megye városai ábécérendben. Sikeres írás után jelezd a képernyőn; ha a fájl nem hozható létre, írd ki: `Hiba a fájl írása során!`

### Minta (konzol)

```
3. feladat: Városok száma: 346
4. feladat: A városokban legkevesebben élő megye:
    Nógrád: 83350 fő, összterület: 341,73 km²
5. feladat: Legnagyobb népsűrűség: Budapest (3314 fő/km²)
6. feladat: Adja meg egy város nevét: szolnok
    Megye: Jász-Nagykun-Szolnok, járás: Szolnoki, népesség: 74341 fő, népsűrűség: 397 fő/km²
7. feladat: A telepulesek_megyenkent.txt fájl elkészült.
```

A `telepulesek_megyenkent.txt` eleje:

```
Bács-Kiskun – járások száma: 11
    Bácsalmás
    Baja
    Dunavecse
    ...
```

(A tizedesjel a gép területi beállításától függően vessző vagy pont – mindkettő elfogadott.)

## II. Grafikus rész (35 pont)

1. **(3 pont)** Importáld a `settlements.sql` szkriptet. Létrejön az `estis_telepulesek` adatbázis: a `county` táblában 20 megye (`id`, `name`), a `settlement` táblában 346 város (`id`, `name`, `countyid` → `county.id`, `region`, `population`, `areasize`).
2. **(8 pont)** Nyisd meg a kiinduló projektet, telepítsd az EF Core-csomagokat (8.0.x), és kösd a két modellosztályt a táblákhoz úgy, hogy a `Telepules` navigációs tulajdonságán keresztül elérhető legyen a megyéje. Készíts `TelepulesekContext` osztályt.
3. **(6 pont)** A főablak címe `Magyarország városai`. Az ablak közepét kitöltő táblázat oszlopai: *Név*, *Megye* (a megye **neve**, nem azonosítója!), *Járás*, *Népesség (fő)*, *Terület (km²)* két tizedesjeggyel. Alatta középen *Új felvétel* gomb. Az ablak legyen reszponzív.
4. **(4 pont)** Induláskor jelenjen meg az összes város név szerinti sorrendben. A táblázat ne legyen szerkeszthető.
5. **(6 pont)** Az *Új felvétel* gomb nyisson meg egy **modális** `Új település` ablakot: *Név*, *Megye* (legördülő lista a megyék **nevével**), *Járás*, *Népesség (fő)*, *Terület (km²)* mezőkkel, *Mentés* és *Mégse* gombbal. Megnyitáskor egyik mező se legyen kitöltve, és a listában se legyen kiválasztott elem.
6. **(8 pont)** Mentés előtt ellenőrizd, hogy a név meg van-e adva; a megye ki van-e választva; a járás meg van-e adva; a népesség 0-nál nagyobb egész szám-e; a terület 0-nál nagyobb szám-e (tizedesvesszővel és -ponttal is). Minden hibát külön, a hibát leíró felugró üzenet jelezzen, és ilyenkor az ablak maradjon nyitva. Sikeres ellenőrzés után az új város kerüljön az adatbázisba, a párbeszédablak záródjon be, és a város jelenjen meg a főablak táblázatában.

### Vázlat (grafikus rész)

```
┌ Magyarország városai ─────────────────────────────────────────┐   ┌ Új település ──────────────┐
│ Név        │ Megye       │ Járás        │ Népesség │ Terület  │   │ Név:          [          ] │
│ Aba        │ Fejér       │ Székesfehér… │    4619  │   88,05  │   │ Megye:        [        ▼ ] │
│ Abádszalók │ Jász-Nagyk… │ Kunhegyesi   │    3922  │  132,23  │   │ Járás:        [          ] │
│ ...                                                           │   │ Népesség (fő):[          ] │
│                       [ Új felvétel ]                         │   │ Terület (km²):[          ] │
└───────────────────────────────────────────────────────────────┘   │       [Mentés] [Mégse]     │
                                                                     └────────────────────────────┘
```

## Tippek

- Konzol: `GroupBy(t => t.Megye)` után `Sum`, illetve `Select(t => t.Jaras).Where(j => j != "").Distinct().Count()`.
- Magyar ábécérend: `OrderBy(m => m.Nev, StringComparer.Create(new CultureInfo("hu-HU"), true))`.
- EF: `[Table("settlement")]`, `[Column("countyid")] public int MegyeId`, és `[ForeignKey(nameof(MegyeId))] public Megye? Megye`.
- A megye neve csak akkor jelenik meg, ha betöltöd: `db.Telepulesek.Include(t => t.Megye)`.
- Legördülő lista objektumokkal: `cbMegye.ItemsSource = megyek; DisplayMemberPath="Nev"`.
- Párbeszédablak: a hívó `if (ablak.ShowDialog() == true) …`, az ablak sikeres mentéskor `DialogResult = true;`.

Megoldás és javítókulcs: [../../megoldasok/F01-telepulesek/](../../megoldasok/F01-telepulesek/)
