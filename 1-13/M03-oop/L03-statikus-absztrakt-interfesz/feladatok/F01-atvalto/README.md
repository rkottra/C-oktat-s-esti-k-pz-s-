# 13-M03-L03-F01 · Mértékegység-átváltó

**Nehézség:** ★☆☆ **Becsült idő:** 45 perc

## Feladat

Készíts `Atvalto` nevű .NET 8 konzolos alkalmazást, amelynek lényege egy **statikus segédosztály**, a `Math` mintájára.

1. **`static class Mertekegyseg`** (külön fájlban):
   - állandók `const`-tal: `KmPerMerfold = 1.609344`, `KgPerFont = 0.45359237`;
   - `static double EuroArfolyam` tulajdonság (Ft/EUR), kezdetben 395. Csak pozitív érték állítható be, különben `ArgumentException`;
   - `static int Atvaltasok` tulajdonság: hány átváltás történt eddig. Kívülről csak olvasható, minden átváltó metódus növeli;
   - átváltó metódusok: `KmbolMerfold`, `MerfoldbolKm`, `CelsiusbolFahrenheit`, `FahrenheitbolCelsius`, `KgbolFont`, `FontbolKg`, `ForintbolEuro`, `EurobolForint`. Mindegyik egy `double` paramétert kap, és `double`-t ad vissza.
2. A főprogram menüből kínálja fel az átváltásokat. A bekért értékből kiírja az eredményt két tizedesre. Egy menüpontban az árfolyam is módosítható; érvénytelen értéknél a kivételt `try`–`catch` kapja el, és a program hibaüzenetet ír. Kilépéskor írja ki, hány átváltás történt.
3. A program végére írd megjegyzésbe a válaszodat: miért nem lehet `new Mertekegyseg()`-et írni, és miért jó, hogy az `Atvaltasok` statikus?

## Minta bemenet és kimenet

```
1 – km → mérföld      2 – mérföld → km
3 – °C → °F           4 – °F → °C
5 – kg → font         6 – font → kg
7 – Ft → EUR          8 – EUR → Ft
9 – Árfolyam beállítása (most: 395 Ft/EUR)
0 – Kilépés
Választás: 2
Érték: 26.2
26,2 mérföld = 42,16 km
Választás: 9
Új árfolyam: -5
Hiba: Az árfolyam csak pozitív lehet.
Választás: 0
Összesen 1 átváltás történt.
```

## Tippek

- Statikus osztályban **minden** tagnak statikusnak kell lennie, a fordító ezt ellenőrzi.
- A `°C → °F` képlet: `F = C · 9 / 5 + 32`.
- Ha tizedespontot és tizedesvesszőt is el akarsz fogadni: `double.TryParse(s.Replace('.', ','), out double x)`.
- Statikus tulajdonság ellenőrzéssel ugyanúgy írható, mint a példányszintű, csak a mező és a tulajdonság is `static`.

Megoldás: [../../megoldasok/F01-atvalto/](../../megoldasok/F01-atvalto/)
