# 14-M02-L03-F01 · Híres épületek – vizsgaszerű feladat

**Nehézség:** ★★★ **Időkeret:** 90 perc **Pontszám:** 100 pont

Adatfájl: [epuletek.csv](epuletek.csv) (UTF-8, pontosvesszővel tagolt, első sor fejléc, 15 épület)

```
Id;Nev;Varos;Orszag;EpitettEv;MagassagMeter;Latogathato
1;Eiffel-torony;Párizs;Franciaország;1889;330;true
11;Parthenón;Athén;Görögország;-432;14;true
12;CN Tower;Toronto;Kanada;1976;553.3;true
```

| Mező | Jelentés |
|---|---|
| `EpitettEv` | az építés (befejezés) éve; **negatív szám = időszámításunk előtti** év (`-432` → i. e. 432) |
| `MagassagMeter` | magasság méterben, tizedes**ponttal** (`553.3`) |
| `Latogathato` | `true` / `false` |

## Feladat

Készíts WPF-alkalmazást (.NET 8) `HiresEpuletekVizsga` néven. Az adatfájlt másold a projekt mappájába, és állítsd be,
hogy a fordításkor a program mellé kerüljön (*Properties → Copy to Output Directory: Copy if newer*).

### 1. Adatmodell és beolvasás (15 pont)

- Készíts `Epulet` osztályt a fájl mezőinek megfelelő tulajdonságokkal (`Id`, `Nev`, `Varos`, `Orszag`, `EpitesEve`, `Magassag`, `Latogathato`). (5 p)
- Egy statikus `CsvSorbol(string sor)` metódus egy sorból hozzon létre objektumot. A magasság beolvasása a gép nyelvi beállításától függetlenül működjön! (5 p)
- Indításkor olvasd be a teljes fájlt egy listába. Ha a fájl hiányzik vagy hibás, jelenjen meg hibaüzenet, és a program ne álljon le. (5 p)

### 2. Megjelenítés táblázatban (15 pont)

- Az ablak felső részén szűrősáv, alatta bal oldalon `DataGrid`, jobb oldalon részletező panel, legalul állapotsor legyen; az ablak legyen átméretezhető (`Grid`, `*` méretek). (5 p)
- A `DataGrid` saját oszlopokkal (`AutoGenerateColumns="False"`), csak olvasható módban mutassa: Név, Város, Ország, Építés éve, Magasság (m), Látogatható (jelölőnégyzet). (5 p)
- Az építés éve negatív évszám esetén `i. e. 432` formában jelenjen meg (számított tulajdonsággal), a magasság egy tizedesjeggyel. (5 p)

### 3. Szűrés (25 pont)

- `ComboBox` az országokkal: ábécérendben, ismétlődés nélkül, az első elem `(összes)`, ez legyen alapból kiválasztva. (8 p)
- `TextBox`: csak azok az épületek látszanak, amelyek nevében szerepel a beírt szöveg (kis- és nagybetűtől függetlenül), már gépelés közben. (7 p)
- `CheckBox` „Csak látogatható”. (4 p)
- A három szűrő **együtt** érvényesüljön, a találatok név szerint rendezve jelenjenek meg. (6 p)

### 4. Statisztika (20 pont)

Az állapotsor mindig a **szűrt** listára vonatkozzon, és minden szűrésnél frissüljön:

- találatok száma (5 p);
- átlagmagasság egy tizedesjegyre (7 p);
- a legmagasabb épület neve és magassága (8 p);
- ha nincs találat, az átlag és a legmagasabb helyén `–` álljon (a program ne szálljon el!).

### 5. Kiválasztott épület részletei (15 pont)

A táblázatban kiválasztott épület adatai a jobb oldali panelen jelenjenek meg **adatkötéssel** (a panel `DataContext`-je a kiválasztott elem):

- név nagy, félkövér betűvel; alatta `Város, Ország` (5 p);
- építés éve és az épület **kora** években (az aktuális évhez képest; az i. e. évszámoknál ne feledd: nincs 0. év) (5 p);
- magasság, valamint „Látogatható” zöld vagy „Nem látogatható” piros színnel (5 p).

### 6. Kódminőség (10 pont)

Beszédes, ékezet nélküli azonosítók; nincs ismétlődő kód (a szűrés egyetlen metódusban van, amelyet minden szűrő meghív);
a vezérlőknek értelmes neve van; a program nem dob kezeletlen kivételt.

## Minta

```
Ország: [(összes)        v]   Név tartalmazza: [            ]   [ ] Csak látogatható
| Név                    | Város      | Ország          | Építés éve | Magasság (m) | Látogatható |   Kiválasztott épület
| Big Ben (Elizabeth …)  | London     | Egyesült Kir.   | 1859       | 96,0         | [x]         |   Parthenón
| Burdzs Kalifa          | Dubaj      | Egyesült Arab … | 2010       | 828,0        | [x]         |   Athén, Görögország
| ...                                                                                            |   Építés éve: i. e. 432
| Parthenón              | Athén      | Görögország     | i. e. 432  | 14,0         | [x]         |   Kora: 2457 év (2026-ban)
                                                                                                    Magasság: 14 m
Találatok: 15 db     Átlagmagasság: 219,3 m     Legmagasabb: Burdzs Kalifa (828 m)               Látogatható
```

Ellenőrző értékek: „Csak látogatható” bejelölve → 14 db, átlag 227,2 m; „Kanada” kiválasztva → 1 db (CN Tower, 553,3 m);
a névben „tor” szöveg → Eiffel-torony, Petronas-ikertornyok.

## Tippek

- `double.Parse(r[5], CultureInfo.InvariantCulture)` – magyar gépen a sima `double.Parse("553.3")` hibás!
- A fájl elérése: `Path.Combine(AppContext.BaseDirectory, "epuletek.csv")`; a WPF-projektben a `System.IO`-t külön be kell `using`-olni.
- A szűrők eseményeit (`SelectionChanged`, `TextChanged`, `Checked`, `Unchecked`) kösd egyetlen kezelőre: `(object sender, RoutedEventArgs e)`.
  Mivel az `InitializeComponent` közben is lefuthat, vizsgáld, hogy a vezérlők már léteznek-e.
- Szűrés LINQ-kel, lépésenként: `IEnumerable<Epulet> szurt = epuletek; if (...) szurt = szurt.Where(...);`, a végén `EpuletTabla.ItemsSource = szurt.ToList();`.
- Magyar számformátum a kötésekben: `Language="hu-HU"` a `Window`-n.
- Kiválasztás nélkül a részletező üres; a cím helyére `FallbackValue='Válassz épületet!'` írhat tájékoztatást.

Megoldás: [../../megoldasok/F01-hires-epuletek-vizsga/](../../megoldasok/F01-hires-epuletek-vizsga/)
