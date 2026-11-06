---
id: 13-M04-L03
cim: Dátum- és időkezelés, típuskonverziók
evfolyam: 1-13
modul: M04 – Fájlkezelés és adatfeldolgozás
orak: 45–48.
het:
oraszam: 4
elofeltetel: 13-M04-L02
leiras: DateTime és TimeSpan, dátumok beolvasása és formázása, típuskonverziók (Parse, TryParse, Convert).
---

# 13-M04-L03 · Dátum- és időkezelés, típuskonverziók

## Célok

A lecke végére a tanuló:

- `DateTime` értéket hoz létre, lekérdezi az összetevőit (év, hónap, nap, a hét napja), és dátumokat összehasonlít;
- dátumokhoz napot, hónapot ad (`AddDays`, `AddMonths`), két dátum különbségét `TimeSpan`-ként kezeli;
- szövegből dátumot olvas be adott formátum szerint (`ParseExact`, `TryParseExact`), és dátumot formázva ír ki;
- fájlból beolvasott dátumokon és időpontokon programozási tételeket alkalmaz;
- ismeri a típuskonverziók fajtáit (automatikus, kényszerített, `Parse`/`TryParse`, `Convert`), és tudja, melyik mikor veszít adatot vagy dob kivételt.

## Elmélet

### DateTime – egy időpont

```csharp
DateTime most = DateTime.Now;                  // dátum + idő
DateTime ma = DateTime.Today;                  // ma 0:00
DateTime szuletes = new DateTime(2008, 3, 15); // év, hónap, nap
DateTime vizsga = new DateTime(2026, 5, 20, 8, 0, 0);   // + óra, perc, mp

Console.WriteLine(szuletes.Year);       // 2008
Console.WriteLine(szuletes.Month);      // 3
Console.WriteLine(szuletes.Day);        // 15
Console.WriteLine(szuletes.DayOfWeek);  // Saturday (DayOfWeek enum)
Console.WriteLine(szuletes.DayOfYear);  // 75 – az év hányadik napja
```

A `DateTime` **nem módosítható**: az `AddDays` stb. új értéket ad vissza, az eredetit nem változtatja meg.

```csharp
DateTime hatarido = kolcsonzes.AddDays(21);
DateTime jovoHonap = ma.AddMonths(1);           // a hónap végét is helyesen kezeli
bool lejart = ma > hatarido;                    // a dátumok összehasonlíthatók
bool hetvege = d.DayOfWeek == DayOfWeek.Saturday || d.DayOfWeek == DayOfWeek.Sunday;
int napokAhonapban = DateTime.DaysInMonth(2028, 2);   // 29 – szökőév!
```

### TimeSpan – időtartam

Két `DateTime` különbsége egy `TimeSpan`:

```csharp
TimeSpan eltelt = DateTime.Today - szuletes;
Console.WriteLine(eltelt.Days);           // egész napok száma
Console.WriteLine(eltelt.TotalDays);      // ugyanez valós számként
TimeSpan munka = new TimeSpan(8, 30, 0);  // 8 óra 30 perc
Console.WriteLine(munka.TotalMinutes);    // 510
Console.WriteLine(munka.Hours);           // 8 – csak az „óra” összetevő!
```

A `Days`, `Hours`, `Minutes` az **összetevők** (pl. 1 nap 3 óra → `Hours == 3`), a `TotalDays`, `TotalHours`, `TotalMinutes` a **teljes** időtartam az adott egységben (→ `TotalHours == 27`). A kettő keverése gyakori hiba.

**Életkor években** nem a `TotalDays / 365`, hanem így (a szökőévek és a születésnap miatt):

```csharp
int kor = ma.Year - szuletes.Year;
if (szuletes.AddYears(kor) > ma)     // idén még nem volt születésnapja
    kor--;
```

### Dátum beolvasása szövegből

A fájlokban a dátum mindig szöveg, ezért át kell alakítani. A legbiztonságosabb a **formátum megadása**:

```csharp
using System.Globalization;

DateTime d1 = DateTime.ParseExact("2026-03-15", "yyyy-MM-dd", CultureInfo.InvariantCulture);
DateTime d2 = DateTime.ParseExact("2026.03.15.", "yyyy.MM.dd.", CultureInfo.InvariantCulture);
TimeSpan ido = TimeSpan.ParseExact("08:45", @"hh\:mm", CultureInfo.InvariantCulture);

if (DateTime.TryParseExact(bemenet, "yyyy.MM.dd.", CultureInfo.InvariantCulture,
                           DateTimeStyles.None, out DateTime d))
    Console.WriteLine("Érvényes dátum");     // a 2026.02.30. is érvénytelen!
```

| Formátumjel | Jelentés | Példa |
|---|---|---|
| `yyyy` | év 4 jeggyel | 2026 |
| `MM` / `M` | hónap 2 jeggyel / ahogy van | 03 / 3 |
| `dd` / `d` | nap | 05 / 5 |
| `HH` | óra 24 órás formában | 14 |
| `mm` | **perc** (kis m!) | 07 |
| `ss` | másodperc | 30 |
| `dddd` | a hét napjának neve | szombat (magyar kultúrával) |
| `MMMM` | a hónap neve | március |

### Dátum kiírása

```csharp
CultureInfo hu = new CultureInfo("hu-HU");
Console.WriteLine(d.ToString("yyyy.MM.dd."));              // 2026.03.15.
Console.WriteLine(d.ToString("yyyy. MMMM d., dddd", hu));  // 2026. március 15., vasárnap
Console.WriteLine(d.ToString("HH:mm"));                    // 08:00
Console.WriteLine($"{d:yyyy-MM-dd}");                      // interpolációban is
Console.WriteLine($"{munka:hh\\:mm}");                     // TimeSpan: 08:30
```

A `DayOfWeek` angol nevet ír ki. Magyarul a `ToString("dddd", hu)` adja, vagy egy saját tömb: `string[] napok = { "vasárnap", "hétfő", … }; napok[(int)d.DayOfWeek]` (a hét a vasárnappal kezdődik, 0-s indexszel!).

### Típuskonverziók

| Fajta | Példa | Megjegyzés |
|---|---|---|
| **automatikus** (implicit) | `int i = 5; double d = i;` | csak ha nem veszhet el adat (kisebb → nagyobb) |
| **kényszerített** (explicit, cast) | `int i = (int)3.99;` → `3` | **levágja** a tört részt, nem kerekít |
| | `int k = (int)'A';` → `65`, `char c = (char)66;` → `'B'` | karakter ↔ kód |
| | `Szak s = (Szak)2;` | szám → enum |
| **szövegből** | `int.Parse("42")`, `double.Parse("3.5", CultureInfo.InvariantCulture)` | hibás szövegnél `FormatException` |
| | `int.TryParse(s, out int x)` | nem dob kivételt, `bool`-t ad vissza |
| **Convert osztály** | `Convert.ToInt32(3.5)` → `4`, `Convert.ToInt32(2.5)` → `2` | **kerekít** (páros felé: „bankári kerekítés”) |
| | `Convert.ToInt32("42")`, `Convert.ToBoolean("true")` | `null`-ra 0 / false |
| **szöveggé** | `x.ToString()`, `x.ToString("F2")`, `$"{x:N0}"` | formázás |
| **kerekítés** | `Math.Round(2.5)` → `2`, `Math.Round(2.5, MidpointRounding.AwayFromZero)` → `3` | |

A **kultúra** a tizedesjelet is meghatározza: magyar beállításnál a `double.Parse("3.5")` hibát dob vagy 35-öt ad, mert a tizedesjel a vessző. Fájlból mindig `CultureInfo.InvariantCulture`-rel olvassunk és írjunk, a felhasználónak pedig a magyar formát mutassuk.

### Tipikus hibák

- **`mm` és `MM` felcserélése**: `"yyyy-mm-dd"` hónap helyett percet olvas.
- **`Hours` a `TotalHours` helyett**: 27 órából 3-at kapunk.
- **Életkor `TotalDays / 365`-tel**: a születésnap környékén rossz eredményt ad.
- **`DateTime.Now` a feladatban**: a program eredménye minden nap más. Ha ellenőrizhető eredmény kell, a „mai napot” rögzítsük (`new DateTime(2026, 5, 15)`).
- **`(int)` kerekítésnek használva**: `(int)2.9 == 2`. Kerekítéshez `Math.Round`.
- **`Parse` felhasználói bemenetre**: egy elgépelés leállítja a programot – ott `TryParse` kell.

## Példák

| Projekt | Mit mutat be |
|---|---|
| [peldak/DatumIdo](peldak/DatumIdo/) | `DateTime` létrehozása és összetevői, `AddDays`/`AddMonths`, összehasonlítás, `TimeSpan` (`Days` vs. `TotalDays`), életkor helyesen, `ParseExact`/`TryParseExact`, formázás magyarul, a hét napjai saját tömbbel, szökőév |
| [peldak/Konverziok](peldak/Konverziok/) | implicit és explicit átalakítás, `char` ↔ `int`, `int` → `enum`, `Parse`/`TryParse`/`Convert`, kerekítési különbségek (`(int)`, `Convert`, `Math.Round`), kultúrafüggő tizedesjel |

## Feladatok

| Azonosító | Feladat | Nehézség | Megoldás |
|---|---|---|---|
| 13-M04-L03-F01 | [Életkor-kalkulátor](feladatok/F01-eletkor/) | ★☆☆ | [megoldás](megoldasok/F01-eletkor/) |
| 13-M04-L03-F02 | [Kölcsönzések és késedelmi díjak](feladatok/F02-kolcsonzesek/) | ★★☆ | [megoldás](megoldasok/F02-kolcsonzesek/) |
| 13-M04-L03-F03 | [Munkaidő-nyilvántartás](feladatok/F03-munkaido/) | ★★★ | [megoldás](megoldasok/F03-munkaido/) |

## Kapcsolódó

- Előző lecke: 13-M04-L02 (Menüs konzolos alkalmazás)
- Következő lecke: 13-M04-L04 (Komplex feladatok, házidolgozat)
- Tanmenet: [1-13](../../../tanmenet/1-13-2felev.md)
