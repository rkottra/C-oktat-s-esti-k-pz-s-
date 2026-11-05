# 14-M04-L04-F02 · Raktárkészlet exportja JSON-ba és XML-be

**Nehézség:** ★★☆ **Becsült idő:** 45 perc

## Feladat

Készíts `RaktarExport` néven alkalmazást, amely a raktár (szűrt) készletét fájlba menti.

1. Felül kategória-`ComboBox` „(összes)” elemmel; a táblázat a kiválasztott kategória termékeit mutatja.
2. Készíts exportosztályt (`TermekExport`): Cikkszám, Név, Kategória (a **neve**, szövegként), Ár, Darab.
3. **JSON-export…** gomb: a táblázatban látható termékeket `SaveFileDialog`-gal kiválasztott fájlba menti
   `System.Text.Json`-nel – tördelve, olvasható ékezetekkel, **kisbetűs (camelCase)** kulcsokkal.
4. **XML-export…** gomb: ugyanezt XML-be, `XmlSerializer`-rel. A gyökérelem `<Termekek>`, az elemek `<Termek>`,
   a cikkszám pedig **attribútum** legyen.
5. A javasolt fájlnév tartalmazza a kategóriát, pl. `termekek-festek.json` (összes esetén `termekek.json`).
6. **JSON-fájl ellenőrzése…** gomb: egy JSON-exportot visszaolvas (deszerializál), és kiírja, hány termék van benne,
   összesen hány darab és mekkora értékben. Hibás fájl esetén üzenetet adjon.

## Minta kimenet

`termekek-festek.json`:

```json
[
  {
    "cikkszam": "FS-001",
    "nev": "Beltéri falfesték 5 l",
    "kategoria": "Festék",
    "ar": 8990,
    "darab": 14
  },
  ...
]
```

`termekek-festek.xml`:

```xml
<?xml version="1.0" encoding="utf-8"?>
<Termekek xmlns:xsi="..." xmlns:xsd="...">
  <Termek cikkszam="FS-001">
    <Nev>Beltéri falfesték 5 l</Nev>
    <Kategoria>Festék</Kategoria>
    <Ar>8990</Ar>
    <Darab>14</Darab>
  </Termek>
  ...
</Termekek>
```

## Tippek

- Ne az entitást szerializáld: a `Termek → Kategoria → Termekek → …` körkörös hivatkozás miatt kivételt kapsz. Használd a `TermekExport` listát (`Select`).
- `new JsonSerializerOptions { WriteIndented = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase, Encoder = JavaScriptEncoder.Create(UnicodeRanges.All) }`.
- A visszaolvasáshoz ugyanezeket a beállításokat add meg: `JsonSerializer.Deserialize<List<TermekExport>>(json, beallitasok)`.
- XML: `[XmlType("Termek")]` az osztályon, `[XmlAttribute("cikkszam")]` a tulajdonságon, `new XmlRootAttribute("Termekek")` a szerializálónál.
- Az összértéknél `long`-ot használj, ha nagy számok jöhetnek.

Megoldás: [../../megoldasok/F02-json-xml-export/](../../megoldasok/F02-json-xml-export/)
