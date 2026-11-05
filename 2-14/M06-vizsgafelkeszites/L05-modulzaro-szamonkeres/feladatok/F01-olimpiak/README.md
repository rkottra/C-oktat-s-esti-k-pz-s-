# 14-M06-L05-F01 · Számonkérés: Nyári olimpiák (tesztekkel)

**Nehézség:** ★★☆ **Időkeret:** 75 perc **Elérhető pontszám:** 30 pont

## Bevezetés

Az [olimpiak.csv](olimpiak.csv) az újkori nyári olimpiai játékok rendezőit tartalmazza 1896-tól 2024-ig (a háborúk miatt elmaradt játékok nélkül). UTF-8, `;`-vel tagolt, fejléces:

| Mező | Jelentés |
|---|---|
| `ev` | a játékok hivatalos éve |
| `varos`, `orszag`, `kontinens` | a rendező város, ország (a rendezéskori nevén) és kontinens |
| `megrendezes_eve` | a tényleges megrendezés éve (eltérhet a hivatalostól) |

```
ev;varos;orszag;kontinens;megrendezes_eve
1896;Athén;Görögország;Európa;1896
```

A számonkérés **két** projektből áll: egy konzolos alkalmazásból (`Olimpiak`) és egy MSTest-tesztprojektből (`Olimpiak.Tests`). Az értékelésnél a kód minősége is számít (a lecke kódátnézési ellenőrzőlistája szerint).

## Feladat

1. **(3 pont)** `Olimpiak` konzolos projekt; `Olimpia` osztály a mezőknek megfelelő tulajdonságokkal, a tesztekhez is használható konstruktorral (paraméterként kapja az értékeket), és statikus `BetoltCsv(string fajlnev)` metódus.
2. **(2 pont)** Írd ki az olimpiák számát.
3. **(4 pont)** Írd ki kontinensenként a rendezések számát, csökkenő sorrendben.
4. **(4 pont)** Írd ki egy sorban a többször is rendező városokat, zárójelben a rendezések számával, a darabszám szerint csökkenő sorrendben (azonos darabszámnál ábécérendben).
5. **(3 pont)** Írd ki azokat a játékokat, amelyeket nem a hivatalos évükben rendeztek meg.
6. **(4 pont)** Kérj be egy országnevet (kis- és nagybetű, valamint a szélső szóközök ne számítsanak), és írd ki, hányszor és mely években rendezett olimpiát. Ha egyszer sem, írj ki üzenetet.
7. **(4 pont)** A 3–6. feladat számításait egy `OlimpiaStatisztika` statikus osztály metódusai végezzék, amelyek **nem írnak a képernyőre és nem olvasnak fájlt**: paraméterként kapják a listát (és ha kell, az országot), és az eredményt visszaadják. A `Program.cs` csak beolvas, meghívja őket és kiír.
8. **(6 pont)** Készíts `Olimpiak.Tests` MSTest-projektet, amely hivatkozik a konzolos projektre, és legalább **öt** tesztmetódussal ellenőrzi az `OlimpiaStatisztika` metódusait **kézzel összeállított, kis tesztadaton** (nem a CSV-fájlon). Legyen köztük
   - „normál” eset (létező ország),
   - kis- és nagybetű-független keresés,
   - nem létező ország (0 / üres lista),
   - üres bemeneti lista.

## Minta kimenet

```
2. feladat: Nyári olimpiák száma: 30
3. feladat: Rendezések kontinensenként:
    Európa: 17
    Észak-Amerika: 6
    Ázsia: 4
    Óceánia: 2
    Dél-Amerika: 1
4. feladat: Többször rendező városok: London (3), Párizs (3), Athén (2), Los Angeles (2), Tokió (2)
5. feladat: A 2020. évi játékokat 2021-ben rendezték (Tokió).
6. feladat: Adjon meg egy országot: egyesült államok
    4 alkalommal: 1904, 1932, 1984, 1996
```

## Tippek

- Tesztprojekt: *Add → New Project → MSTest Test Project*, majd *Add → Project Reference…* a konzolos projektre.
- A teszt neve mondja el, mit vár: `RendezesekSzama_NemRendezoOrszag_Nulla`.
- Listák összehasonlítása: `CollectionAssert.AreEqual(vart, kapott)`.
- Tesztek futtatása: *Test → Run All Tests* (Ctrl+R, A).

Megoldás és javítókulcs: [../../megoldasok/F01-olimpiak/](../../megoldasok/F01-olimpiak/)
