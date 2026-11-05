# Megoldás – 14-M06-L03-F01 · 2. próbavizsga: Magyarország városai

| Projekt | Rész |
|---|---|
| [TelepulesekKonzol](TelepulesekKonzol/) | I. konzolos rész – a `Telepules` osztályban a CSV- és a JSON-betöltő is szerepel (a vizsgán elég az egyik) |
| [TelepulesekWpf](TelepulesekWpf/) | II. grafikus rész – `Megye`/`Telepules` entitás attribútumokkal, `TelepulesekContext`, főablak, `UjTelepulesAblak` párbeszédablak |

Futtatás (grafikus rész): XAMPP-ban fusson a MySQL, importáld a [settlements.sql](../../feladatok/F01-telepulesek/settlements.sql) fájlt, majd F5. Migráció nem kell. Jelszóval védett `root` esetén írd át a kapcsolati szöveget.

## Javítókulcs (60 pont)

### I. Konzolos rész (25 pont)

| # | Szempont | Pont |
|---|---|---|
| 1 | projekt, az adatfájl a kimeneti mappába másolódik | 2 |
| 2 | `Telepules` osztály megfelelő típusokkal (`Terulet`: `double`) | 2 |
| | statikus betöltő a megadott néven, a beolvasás ezzel történik; CSV-nél tizedes pont helyes kezelése | 3 |
| 3 | városok száma (346) | 2 |
| 4 | csoportosítás megye szerint, összlakosság | 2 |
| | a legkisebb kiválasztása (Nógrád, 83350 fő), összterület két tizedesre (341,73) | 2 |
| 5 | népsűrűség számítása, maximum (Budapest, 3314 fő/km²) | 4 |
| 6 | bekérés, kis-/nagybetű-független keresés | 1 |
| | helyes adatok (Budapestnél `–` járás) | 1 |
| | „nincs ilyen” ág | 1 |
| 7 | megyék ábécérendben, járások száma (üres nélkül; Budapest: 0) | 2 |
| | városok tabulátorral, ábécérendben | 1 |
| | `try`–`catch`, sikeres/hiba üzenet, a fájl lezárása | 2 |

### II. Grafikus rész (35 pont)

| # | Szempont | Pont |
|---|---|---|
| 1 | adatbázis importálva (20 megye, 346 város) | 3 |
| 2 | NuGet-csomagok | 1 |
| | entitások a táblákhoz kötve, idegen kulcs és navigációs tulajdonság | 4 |
| | DbContext, működő kapcsolat | 3 |
| 3 | ablakcím, táblázat az előírt oszlopokkal, *Új felvétel* gomb | 3 |
| | a Megye oszlopban a megye neve; terület két tizedesre | 2 |
| | reszponzív elrendezés | 1 |
| 4 | összes város név szerint, nem szerkeszthető táblázat | 4 |
| 5 | modális ablak a mezőkkel, *Mentés*/*Mégse* | 3 |
| | megyelista nevekkel, üres kezdőállapot | 3 |
| 6 | öt ellenőrzés, külön üzenetekkel, az ablak nyitva marad | 5 |
| | mentés adatbázisba, megjelenik a főablakban | 3 |
| | **Összesen** | **60** |
