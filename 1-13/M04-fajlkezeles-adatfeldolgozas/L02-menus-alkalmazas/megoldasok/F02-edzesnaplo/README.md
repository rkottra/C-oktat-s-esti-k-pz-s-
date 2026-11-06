# Megoldás – 13-M04-L02-F02 · Edzésnapló

- `Edzes.cs`: két konstruktor (CSV-sorból és adatokból), a `CsvSor()` visszaalakító metódus és a `KcalPerPerc` számított tulajdonság.
- `Program.cs`: beolvasás (ha nincs fájl, üres naplóval indul), menüciklus, hat menüpont-metódus, mentés kilépéskor, ha a `modosult` jelző igaz.

Megjegyzések:

- **Mentés:** a `Ment` a teljes listát fejléccel írja vissza, ugyanabban a formátumban, ahogy a `Beolvas` várja. Ez a `CsvSor()` és a CSV-sort váró konstruktor párosának lényege.
- Az `ÉÉÉÉ-HH-NN` formátumú dátumoknál a szöveges rendezés egyben időrend is. Ezért lehetett a dátumot most még `string`-ként kezelni; a `DateTime` a következő leckében jön.
- A havi ellenőrzés a nap ellenőrzését újrahasznosítja: `ErvenyesDatum(honap + "-01")`.
- A havi „legtöbb sportág” két tétel egymásba ágyazva: minden sportágra megszámlálás, a darabszámokon maximumkiválasztás.
- A program a `bin\Debug\net8.0` mappában lévő **másolatot** módosítja. Ha a projektben lévő eredeti `edzesek.csv` újabb, a következő fordítás felülírja a mentett változatot.

Futtatás: F5 (Visual Studio).
