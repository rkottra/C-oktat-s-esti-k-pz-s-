# 14-M06-L01-F02 · Pilóták JSON-ból – kiegészítő feladat

**Nehézség:** ★★☆ **Becsült idő:** 45 perc

## Feladat

A vizsgán az adatforrás gyakran két formában is rendelkezésre áll (CSV **vagy** JSON), és választani lehet. Gyakorold a JSON-változatot is! A [pilots.json](pilots.json) ugyanazokat az adatokat tartalmazza, mint az F01 CSV-állománya:

```json
[
  { "id": 1, "name": "Juan-Manuel Fangio", "gender": "M", "birthdate": "1911-06-24", "nation": "Argentinian" },
  ...
  { "id": 134, "name": "Robert O'Brien", "gender": "M", "birthdate": null, "nation": "American" },
  ...
]
```

Készíts `PilotakJson` nevű konzolos projektet.

1. Készíts `Pilota` osztályt magyar nevű tulajdonságokkal (`Id`, `Nev`, `Nem`, `SzuletesiDatum`, `Nemzetiseg`), és statikus `BetoltJson(string fajlnev)` metódust, amely a `System.Text.Json` segítségével listába olvassa az állományt. Írd ki a beolvasott pilóták számát.
2. Írd ki, hány pilóta született az egyes évtizedekben (pl. `1910–1919: 117 fő`), időrendben. Az ismeretlen dátumú pilótákat hagyd ki.
3. Írd ki azoknak a pilótáknak a nevét, akik szökőnapon (február 29-én) születtek. Ha nincs ilyen, írj ki üzenetet.
4. Kérj be egy nemzetiséget (angolul, kis- és nagybetű nem számít), és listázd ennek a nemzetiségnek a pilótáit ábécérendben, születési dátummal. Ha nincs ilyen, írj ki üzenetet.
5. Mentsd el a női pilótákat születési dátum szerint növekvő sorrendben a `noi_pilotak.json` állományba, tördelt (olvasható) formában.

## Minta kimenet

```
1. feladat: Beolvasott pilóták: 800
2. feladat: Születések évtizedenként:
    1890–1899:   7 fő
    1900–1909:  40 fő
    1910–1919: 117 fő
    ...
    1980–1989:  25 fő
3. feladat: Szökőnapon született: Masten Gregory
4. feladat: Nemzetiség (angolul, pl. Hungarian): hungarian
    Zsolt Baumgartner (1981.01.01)
5. feladat: 5 női pilóta mentve a noi_pilotak.json fájlba.
```

## Tippek

- A JSON-kulcs és a C#-tulajdonság neve eltér, ezért: `[JsonPropertyName("name")] public string Nev { get; set; } = "";` (`using System.Text.Json.Serialization;`).
- A `"birthdate": null` csak `DateTime?` típusú tulajdonságba olvasható be hiba nélkül.
- Évtized: `ev / 10 * 10` (egész osztás!).
- Olvasható, ékezethelyes kimenet: `new JsonSerializerOptions { WriteIndented = true, Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping }`.

Megoldás: [../../megoldasok/F02-pilotak-json/](../../megoldasok/F02-pilotak-json/)
