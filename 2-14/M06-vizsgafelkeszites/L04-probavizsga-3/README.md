---
id: 14-M06-L04
cim: 3. próbavizsga: vizsgaszimuláció
evfolyam: 2-14
modul: M06 – Vizsgafelkészítés
orak: 77–80.
het:
oraszam: 4
elofeltetel: 14-M06-L03
leiras: Szigorú vizsgaszimuláció időkorláttal, pontozással.
---

# 14-M06-L04 · 3. próbavizsga: vizsgaszimuláció

## Célok

A lecke végére a tanuló:

- valós vizsgakörülmények között (szigorú időkeret, segédeszköz-korlátozás) önállóan megold egy 100 pontos asztali feladatsort;
- tudatosan osztja be az idejét a konzolos és a grafikus rész között;
- nem automatikusan számozott kulcsot, nullable mezőt és egyediségi feltételt is kezel;
- a javítókulcs alapján objektíven pontozza saját munkáját, és azonosítja a hiányosságait.

## Elmélet

### A szimuláció menete (4 × 45 perc)

| Idő | Tevékenység |
|---|---|
| 0–10. perc | eligazítás, gépek előkészítése: XAMPP indítása, Visual Studio megnyitása, adatfájlok kiosztása |
| 10–160. perc | **vizsgamunka (150 perc)** – kérdés csak a feladat értelmezésére vonatkozhat |
| 160–165. perc | leadás (`bin`/`obj` nélkül) |
| 165–180. perc | a javítókulcs kivetítése, önjavítás megkezdése (F02) |

### Stratégia

1. **Olvasd végig az egészet** (5 perc). Jelöld meg, mi tűnik nehéznek.
2. **Először azt, ami biztos pont**: projektek létrehozása, adatfájl másolása, osztály + betöltés, darabszám. Az SQL importja 1 perc – rögtön az elején csináld meg.
3. **Időkorlát részfeladatonként**: ha egy részfeladaton 10 percnél tovább akadsz el, lépj tovább, és jegyezd fel, hova kell visszatérni.
4. **Fordíts gyakran!** Nem forduló projektre nem jár pont – a félkész kódot inkább kommenteld ki.
5. **Az utolsó 10 perc**: ablakcímek, feliratok, kiírások összevetése a mintával; próbakód törlése.

Ajánlott arány: konzolos rész ~55 perc (40 pont), grafikus rész ~95 perc (60 pont).

### Új elemek ebben a feladatsorban

| Helyzet | Megoldás |
|---|---|
| Hiányzó szám a CSV-ben üres mezőként | `a[7] == "" ? null : double.Parse(a[7], CultureInfo.InvariantCulture)` → `double?` |
| `null` a táblázatban | a `StringFormat` mellett is üres cella marad – nincs teendő |
| A kulcsot a felhasználó adja meg | `[Key]` + `[DatabaseGenerated(DatabaseGeneratedOption.None)]`; mentés előtt ellenőrizd, hogy szabad-e |
| Egyedi mező (vegyjel) | mentés előtt `foglaltVegyjelek.Contains(…)`; különben az adatbázis kivételt dob (*Duplicate entry*) |
| Több szűrő egyszerre | `IQueryable`-re feltételesen fűzött `Where`-ek |
| Sok ellenőrzés | egy `string? Ellenorzes()` metódus, amely az első hiba szövegét adja vissza – átláthatóbb, mint tíz `if` a gombkezelőben |

### Pontozási elvek (a valódi vizsgán is hasonlók)

- Minden részfeladat **önállóan** pontozott – a hiányzó 4. feladat nem viszi el az 5. pontjait.
- Részpont jár a részben jó megoldásért (pl. jó a csoportosítás, de hiányzik a rendezés).
- Kiszámolás nélkül kiírt (beégetett) eredményért nem jár pont.
- Nem forduló projektre a részfeladatai nem kapnak pontot.

### Tipikus hibák a szimulációkon

- Túl sok idő megy el a felület „szépítésére”, és nem marad idő az ellenőrzésekre (amelyek a legtöbb pontot érik).
- A kulcsmező automatikus számozást feltételez, ezért a felvitt rendszám elvész, vagy kivétel keletkezik.
- `First()` a `null` értékek kiszűrése nélkül, vagy rendezés `null`-t tartalmazó mezőre (a `null` a sor elejére kerül).
- Az ellenőrzésnél `double.Parse` szerepel `TryParse` helyett – hibás bemenetre a program összeomlik.
- Leadáskor a `bin`/`obj` mappák is bekerülnek (több száz MB) – vagy éppen a forrásfájl marad ki.

## Példák

Ebben a leckében nincs új bemutató projekt – a vizsgán csak a saját tudásodra és sablonjaidra támaszkodhatsz. Felkészülésként nézd át a 14-M06-L01–L03 megoldásait.

## Feladatok

| Azonosító | Feladat | Nehézség | Megoldás |
|---|---|---|---|
| 14-M06-L04-F01 | [3. próbavizsga: Kémiai elemek (150 perc, 100 pont)](feladatok/F01-kemiai-elemek/) | ★★★ | [megoldás és javítókulcs](megoldasok/F01-kemiai-elemek/) |
| 14-M06-L04-F02 | [Önjavítás a javítókulcs alapján](feladatok/F02-onjavitas/) | ★☆☆ | [javítókulcs](megoldasok/F01-kemiai-elemek/) |

## Kapcsolódó

- Előző lecke: 14-M06-L03 (2. próbavizsga: több tábla, szűrések)
- Következő lecke: 14-M06-L05 (Számonkérés és kódátnézés)
- Tanmenet: [2-14](../../../tanmenet/2-14.md)
