# Megoldás – 14-M06-L05-F01 · Nyári olimpiák (tesztekkel)

| Projekt | Tartalom |
|---|---|
| [Olimpiak](Olimpiak/) | `Olimpia` (adatosztály + `BetoltCsv`), `OlimpiaStatisztika` (tiszta számoló metódusok), `Program.cs` (csak beolvasás, hívás, kiírás) |
| [Olimpiak.Tests](Olimpiak.Tests/) | 7 MSTest-teszt kézi tesztadaton |

Futtatás: a konzolos projekt F5-tel; a tesztek *Test → Run All Tests* menüből, vagy parancssorból az `Olimpiak.Tests` mappában: `dotnet test`. (Mindkét projektet nyisd meg ugyanabban a Visual Studio-megoldásban: *File → Open → Project/Solution*, majd *Add → Existing Project*.)

## Javítókulcs (30 pont)

| # | Szempont | Pont |
|---|---|---|
| 1 | osztály, konstruktor paraméterekkel, `BetoltCsv` | 3 |
| 2 | darabszám (30) | 2 |
| 3 | csoportosítás, csökkenő sorrend | 4 |
| 4 | csak a többször rendezők, darabszámmal, előírt sorrend | 4 |
| 5 | eltérő év felismerése (2020 → 2021) | 3 |
| 6 | normalizált keresés (`Trim`, kis-/nagybetű), évek, „nem rendezett” ág | 4 |
| 7 | `OlimpiaStatisztika`: nincs benne `Console`/`File`, visszatérési értékkel dolgozik | 4 |
| 8 | tesztprojekt projekt-hivatkozással, fordul, a tesztek zöldek | 2 |
| | legalább 5 teszt kézi adaton, a négy kötelező esettel, beszédes nevekkel | 4 |
| | **Összesen** | **30** |

A kódátnézés (közös rész) szempontjai a lecke README-jének ellenőrzőlistájában vannak; a számonkérés pontszámán felül szóbeli visszajelzést adnak.
