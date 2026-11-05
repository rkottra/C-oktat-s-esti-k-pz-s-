# 14-M06-L02-F01 · 1. próbavizsga: F1-pilóták – adatbázis és WPF-felület

**Nehézség:** ★★☆ **Időkeret:** 120 perc **Elérhető pontszám:** 50 pont

## Bevezetés

Az előző lecke konzolos feladatának adatai most adatbázisból érkeznek. Rendelkezésedre áll:

- [f1pilots.sql](f1pilots.sql) – létrehozza az `estis_f1pilots` adatbázist (UTF-8, magyar rendezés) és benne a `pilots` táblát 800 rekorddal;
- [pilots.csv](pilots.csv) – ugyanezek az adatok CSV-ben (a 0. feladathoz);
- [kiindulo/PilotakWpf](kiindulo/PilotakWpf/) – kiinduló WPF-projekt üres főablakkal és egy `Pilota` modellosztállyal.

A `pilots` tábla szerkezete:

| Oszlop | Típus | Megjegyzés |
|---|---|---|
| `id` | INT, AUTO_INCREMENT | elsődleges kulcs |
| `name` | VARCHAR(100) | kötelező |
| `gender` | CHAR(1) | `M` vagy `F` |
| `birthdate` | DATE | NULL is lehet |
| `nation` | VARCHAR(50) | kötelező |

## Feladat

0. *(Szorgalmi, 0 pont)* Az SQL-szkript helyett hozd létre a táblát kézzel phpMyAdminban, és töltsd fel a `pilots.csv` importálásával (*Importálás → CSV, mezőelválasztó: `;`, az első sor kihagyása*). A további feladatokhoz mindegy, melyik úton jött létre az adatbázis.
1. **(4 pont)** Indítsd el a XAMPP-ban a MySQL-t, és importáld az `f1pilots.sql` szkriptet. Ellenőrizd, hogy a `pilots` táblában 800 sor van.
2. **(10 pont)** Nyisd meg a kiinduló projektet.
   - Telepítsd a `Pomelo.EntityFrameworkCore.MySql` és a `Microsoft.EntityFrameworkCore.Tools` csomagot (8.0.x). *(2 pont)*
   - Kösd a `Pilota` osztályt a `pilots` táblához (attribútumokkal), vagy generáld az osztályokat `Scaffold-DbContext`-tel. *(4 pont)*
   - Készíts `PilotakContext` osztályt, amely az `estis_f1pilots` adatbázishoz kapcsolódik. *(4 pont)*
3. **(10 pont)** Alakítsd ki a főablakot a vázlat szerint! Az ablak címe `F1 pilóták` legyen. Az ablak **reszponzív** legyen: átméretezéskor a táblázat és az űrlap arányosan nőjön, ne lógjon ki semmi.
   ```
   +-----------------------------------------------+-------------------------+
   | Azonosító | Név        | Nem | Szül. | Nemz.  | ┌ Új pilóta ──────────┐ |
   |-----------+------------+-----+-------+--------| | Név:        [     ] | |
   |   1       | Juan-...   |  M  | 1911… | Arg…   | | Nem:        [ ▼   ] | |
   |   ...                                         | | Születési dátum: [📅]| |
   |                                               | | Nemzetiség: [   ▼ ] | |
   | [Kijelölt pilóta törlése]    Pilóták száma: 800| |     [ Mentés ]      | |
   +-----------------------------------------------+-------------------------+
   ```
   - bal oldalon a táblázat (kb. a szélesség kétharmada), alatta a törlés gomb és a darabszám; *(4 pont)*
   - jobb oldalon „Új pilóta” feliratú csoportban a beviteli mezők: név (szövegdoboz), nem (legördülő lista: *Férfi*, *Nő*), születési dátum (dátumválasztó), nemzetiség (legördülő lista, amelybe gépelni is lehet), *Mentés* gomb. *(6 pont)*
4. **(6 pont)** Induláskor a táblázat jelenítse meg az adatbázis összes pilótáját a fenti oszlopokkal, magyar fejlécekkel; a születési dátum `éééé.hh.nn` formában jelenjen meg. A táblázat ne legyen szerkeszthető. A táblázat alatt jelenjen meg a pilóták száma. A nemzetiség-lista a már meglévő nemzetiségeket tartalmazza ábécérendben.
5. **(12 pont)** A *Mentés* gomb vegyen fel új pilótát. Mentés előtt ellenőrizd, és hiba esetén hibaüzenet-ablakban (hiba ikonnal) jelezd, ha
   - a név üres; *(2 pont)*
   - nincs kiválasztva a nem; *(2 pont)*
   - nincs megadva a születési dátum, vagy az a mai napnál nem korábbi; *(2 pont)*
   - a nemzetiség üres. *(2 pont)*

   Sikeres ellenőrzés után az új pilóta kerüljön az adatbázisba és a táblázatba (a darabszám is frissüljön), majd a mezők ürüljenek ki. *(4 pont)*
6. **(8 pont)** A *Kijelölt pilóta törlése* gomb
   - ha nincs kijelölt sor, figyelmeztessen; *(2 pont)*
   - megerősítést kérjen (Igen/Nem) a pilóta nevével; *(3 pont)*
   - igen válasz esetén törölje a pilótát az adatbázisból, és frissítse a táblázatot és a darabszámot. *(3 pont)*

## Tippek

- Attribútumos megfeleltetés: `[Table("pilots")]` az osztályon, `[Column("name")] public string Nev { get; set; }` a tulajdonságon (`using System.ComponentModel.DataAnnotations.Schema;`).
- A dátum formázása a táblázatban: `Binding="{Binding SzuletesiDatum, StringFormat=yyyy.MM.dd}"`.
- A nem kódja a `ComboBoxItem` `Tag` tulajdonságában tárolható: `<ComboBoxItem Content="Férfi" Tag="M" />`.
- `ObservableCollection<Pilota>`-ba töltsd az adatokat – a táblázat magától frissül.
- Mentés után ne felejtsd el a `SaveChanges()`-t!

Megoldás és javítókulcs: [../../megoldasok/F01-pilotak-wpf/](../../megoldasok/F01-pilotak-wpf/)
