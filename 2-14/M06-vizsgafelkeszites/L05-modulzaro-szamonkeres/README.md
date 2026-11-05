---
id: 14-M06-L05
cim: Számonkérés és kódátnézés
evfolyam: 2-14
modul: M06 – Vizsgafelkészítés
orak: 81–84.
het:
oraszam: 4
elofeltetel: 14-M06-L04
leiras: Önálló feladatmegoldás időre, majd közös kódátnézés (code review).
---

# 14-M06-L05 · Számonkérés és kódátnézés

## Célok

A lecke végére a tanuló:

- időre, önállóan megold egy rövidebb konzolos feladatot, és a számítási logikát tesztelhető metódusokba szervezi;
- MSTest-tesztekkel igazolja a saját metódusai helyességét;
- egy ellenőrzőlista alapján átnézi a saját és a társa kódját, és a megállapításait érthetően, javaslattal együtt fogalmazza meg;
- a kódátnézés eredménye alapján javítja (refaktorálja) a kódot.

## Elmélet

### A lecke menete

| Idő | Tevékenység |
|---|---|
| 1–2. óra (75 perc) | **számonkérés**: F01 – önálló munka, pontozott |
| 2. óra vége | leadás; a megoldások párokba osztása |
| 3. óra | **kódátnézés párban**: a társ F01-megoldásának átnézése az ellenőrzőlistával, írásos visszajelzés |
| 4. óra | F02 – közös hibavadászat egy szándékosan rossz programon, javítás, összegzés |

### Mi a kódátnézés (code review)?

A kódátnézés során egy másik fejlesztő elolvassa a kódot, **mielőtt** az bekerülne a közös termékbe. Célja nem a „hibáztatás”, hanem hogy

- a hibák korán, olcsón derüljenek ki;
- a kód érthető legyen másnak is (és fél év múlva neked is);
- a csapat egymástól tanuljon.

Jó visszajelzés: **konkrét** (hol?), **indokolt** (miért baj?), **javaslatot tesz** (hogyan jobb?), és a kódról szól, nem a szerzőről.

| Kevésbé jó | Jobb |
|---|---|
| „Ez rossz.” | „A 12. sorban a ciklus `s.Length - 1`-ig megy, így az utolsó adatsor kimarad. Javaslat: `Skip(1)`.” |
| „Csúnya nevek.” | „Az `l` és `x` nevekből nem derül ki a tartalmuk; `olimpiak`, `keresettOrszag` jobb lenne.” |

### Kódátnézési ellenőrzőlista

**1. Helyesség**
- [ ] A program a minta szerinti eredményt adja? Kipróbáltad szélső esetekkel (üres bemenet, nem létező érték, kis-/nagybetű)?
- [ ] Nincs elcsúszott ciklushatár (első/utolsó elem kimarad)?
- [ ] A „nincs találat” üzenet egyszer, a keresés után jelenik meg?
- [ ] `First()`, `Max()`, `Average()` nem futhat üres sorozaton? A `null` értékek kezelve vannak?

**2. Olvashatóság**
- [ ] Beszédes nevek (`olimpiak`, nem `l`); osztályok és metódusok PascalCase, változók camelCase?
- [ ] Nincsenek „varázsszámok” és -indexek (`a[3]`, `40`) – helyettük tulajdonság, elnevezett konstans?
- [ ] A metódusok rövidek, egy dolgot csinálnak?
- [ ] A kommentek a *miért*-et magyarázzák, nem a nyilvánvalót?

**3. Ismétlés és szerkezet**
- [ ] Nincs másolt-beillesztett, csak egy értékben eltérő kódrészlet?
- [ ] A számítás el van választva a kiírástól (tesztelhető metódusok)?
- [ ] Az adat osztályban van, nem indexelt tömbökben?

**4. Hibakezelés és erőforrások**
- [ ] Fájlírás `using`-gal; nincs üres `catch { }`?
- [ ] A felhasználói bemenet `TryParse`-szal ellenőrzött?
- [ ] Hiba esetén érthető üzenet jelenik meg, és a program nem omlik össze?

**5. Hatékonyság** (csak ha nem rontja az olvashatóságot)
- [ ] Nincs fölösleges dupla ciklus, ahol egy `GroupBy`/`Dictionary` elég?
- [ ] Nem olvassuk be többször ugyanazt a fájlt, nem kérdezzük le többször ugyanazt?

**6. Tisztaság**
- [ ] Nincs kikommentezett kód, próba-`Console.WriteLine`, `Console.ReadKey()`?
- [ ] A projekt figyelmeztetés (warning) nélkül fordul?
- [ ] A tesztek zöldek, és a nevük elmondja, mit ellenőriznek?

### Tesztelhető kód – emlékeztető (14-M05-L03)

```csharp
// Nehezen tesztelhető: számol, ÉS ír a képernyőre
static void Kiir(List<Olimpia> l) { … Console.WriteLine(db); }

// Tesztelhető: bemenet → visszatérési érték, mellékhatás nélkül
public static int RendezesekSzama(List<Olimpia> olimpiak, string orszag) => …;

[TestMethod]
public void RendezesekSzama_NemRendezoOrszag_Nulla()
    => Assert.AreEqual(0, OlimpiaStatisztika.RendezesekSzama(tesztadat, "Magyarország"));
```

A teszt **kis, kézzel összeállított adaton** fusson – így pontosan tudjuk a várt eredményt, és a teszt nem függ a fájltól.

### Tipikus hibák

- A tesztprojekt nem látja az osztályt: hiányzik a *Project Reference*, vagy az osztály `internal` (legyen `public`).
- A teszt a CSV-fájlt olvassa – a tesztprojekt kimeneti mappájában nincs ott a fájl, és a teszt amúgy is törékeny lesz.
- Átnézéskor csak „stílus”-megjegyzések születnek, a futtatás és a szélső esetek kipróbálása elmarad.
- A visszajelzés a személyről szól („te mindig…”), nem a kódról.

## Példák

| Projekt | Mit mutat be |
|---|---|
| [peldak/RefaktoralasMinta](peldak/RefaktoralasMinta/) | ugyanaz a számítás kódátnézés előtt (`Elotte.cs`, a megállapítások a sorok mellett) és után (`Utana.cs`: adatosztály, számított tulajdonság, elnevezett konstans, tiszta metódusok); a futás megmutatja a rejtett hibát is |

## Feladatok

| Azonosító | Feladat | Nehézség | Megoldás |
|---|---|---|---|
| 14-M06-L05-F01 | [Számonkérés: Nyári olimpiák, tesztekkel (75 perc, 30 pont)](feladatok/F01-olimpiak/) | ★★☆ | [megoldás](megoldasok/F01-olimpiak/) |
| 14-M06-L05-F02 | [Kódátnézés: hibák és rossz megoldások felderítése](feladatok/F02-kodatnezes/) | ★★☆ | [megoldás](megoldasok/F02-kodatnezes/) |

## Kapcsolódó

- Előző lecke: 14-M06-L04 (3. próbavizsga: vizsgaszimuláció)
- Következő lecke: 14-M06-L06 (Finomhangolás és rutin)
- Tanmenet: [2-14](../../../tanmenet/2-14.md)
