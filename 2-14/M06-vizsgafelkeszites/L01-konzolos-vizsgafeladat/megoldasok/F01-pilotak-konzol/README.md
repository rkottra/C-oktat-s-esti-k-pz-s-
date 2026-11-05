# Megoldás – 14-M06-L01-F01 · Pilóták – konzolos vizsgafeladat

Futtatás: Visual Studióban F5 (a `pilots.csv` automatikusan a program mellé másolódik). A `pilota_csoportok.txt` a `bin/Debug/net8.0/` mappában jön létre.

## Javítókulcs (30 pont)

| # | Szempont | Pont |
|---|---|---|
| 1 | `PilotakKonzol` projekt létezik, fordul; a CSV a kimeneti mappába másolódik | 2 |
| 2 | `Pilota` osztály a megadott tulajdonságokkal, megfelelő típusokkal (`DateTime?`) | 2 |
| | konstruktor sorból, a `null` dátum helyes kezelése | 2 |
| | statikus `BetoltCsv(string)` → `List<Pilota>`, fejléc kihagyva, a beolvasás ezzel történik | 1 |
| 3 | pilóták száma (800) | 2 |
| 4 | női pilóták száma (5) | 1 |
| | nevek egy sorban, vesszővel elválasztva | 2 |
| 5 | legidősebb pilóta (Philippe Etancelin), az ismeretlen dátum kiszűrve | 2 |
| | dátum `éééé.hh.nn` formában, nemzetiséggel | 1 |
| | ismeretlen dátumú pilóták száma (1) | 1 |
| 6 | névrészlet bekérése | 1 |
| | szavankénti, kis- és nagybetű-független kezdőegyezés (pl. `sch` → 17 pilóta) | 2 |
| | tabulátoros kiírás, ismeretlen dátum helyett `ismeretlen` | 1 |
| | „nincs találat” üzenet | 1 |
| 7 | csoportosítás nemzetiség szerint, csökkenő sorrend | 2 |
| | csak az első 5, létszámmal (American 156, British 152, Italian 96, French 67, German 57) | 2 |
| 8 | kezdőbetű bekérése, szűrés a nemzetiség kezdőbetűjére | 1 |
| | csoportfejléc `Nemzetiség (N fő):` ábécérendben | 1 |
| | pilóták tabulátorral, dátum szerint növekvő sorrend, ismeretlen a végén | 1 |
| | `try`–`catch`, a fájl lezárása (`using`), sikeres/sikertelen üzenet | 2 |
| | **Összesen** | **30** |

Levonás: a mintából átmásolt, kiszámolás nélkül kiírt értékért az adott részfeladat pontja nem jár.
