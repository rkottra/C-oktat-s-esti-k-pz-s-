# 14-M01-L02-F02 · Időjárás-mérések hibás sorokkal

**Nehézség:** ★★☆ **Becsült idő:** 50 perc

## Feladat

A [meresek.csv](meresek.csv) öt magyar város kéthetes napi méréseit tartalmazza:

```
datum;varos;homerseklet;csapadek_mm
2026-10-01;Budapest;16.2;0.0
2026-10-01;Debrecen;8.6;14.6
```

A fájl kézi adatrögzítésből származik, ezért **néhány sora hibás** (hiányzó érték, szöveg szám helyett, tizedesvessző, rossz elválasztó). A programnak ezeket ki kell szűrnie, nem állhat le miattuk.

Készíts .NET 8 konzolos alkalmazást `IdojarasMeresek` néven (a CSV-fájl kerüljön a kimeneti mappába).

1. `Meres` osztály: `Datum` (`DateTime`), `Varos`, `Homerseklet`, `Csapadek` és egy számított `Esos` tulajdonság (volt-e csapadék).
2. Statikus `Feldolgoz(string sor)` metódus, amely helyes sorból `Meres` objektumot ad vissza, hibás sorból `null`-t. Hibás a sor, ha
   - nem pontosan 4 adat van benne,
   - a dátum nem `éééé-hh-nn` formátumú,
   - a város üres,
   - a hőmérséklet vagy a csapadék nem tizedespontos szám, illetve a csapadék negatív.
3. A beolvasás gyűjtse a hibás sorokat is (sorszámmal), majd válaszold meg LINQ-kel:
   1. Hány érvényes mérés és hány hibás sor van? Listázd a hibás sorokat!
   2. Városonként az átlag-, a minimum- és a maximum-hőmérséklet, városnév szerint rendezve.
   3. Melyik volt a legmelegebb mérés (dátum, város, érték)?
   4. Hány esős nap volt Budapesten?
   5. Melyik városban esett összesen a legtöbb csapadék, és mennyi?
   6. Volt-e fagy (0 °C vagy alacsonyabb)?
   7. Melyik napon volt a legmagasabb a városok átlaghőmérséklete?
   8. Írd ki a `varosok.txt` fájlba városonként: `város;átlaghőmérséklet;összcsapadék` (1 tizedesjegy, tizedesponttal).

## Minta kimenet

```
1. Érvényes mérések: 66, hibás sorok: 4
   8. sor: 2026-10-02;Debrecen;;0.0
   25. sor: 2026-10-05;Pécs;10,3;4.3
   42. sor: 2026-10-09;Budapest;n/a;0.0
   59. sor: 2026-10-12 Szeged 10.7 16.4
2. Városonként:
   Budapest  átlag  11,2 °C  min   6,4  max  16,2
   ...
3. Legmelegebb mérés: 2026.10.13. Pécs: 16,5 °C, 7,8 mm
4. Esős napok Budapesten: 5
5. Legcsapadékosabb város: Debrecen (67,3 mm)
6. Volt fagy? nem
7. Legmelegebb nap (városok átlaga): 2026.10.06. – 12,8 °C
```

## Tippek

- Kivétel helyett most `TryParse`-t használj – a `Feldolgoz` minden hibánál egyszerűen `return null;`.
- Számok: `double.TryParse(szoveg, NumberStyles.Float, CultureInfo.InvariantCulture, out double ertek)`. A `NumberStyles.Float` fontos: nélküle a `"10,3"`-at ezres elválasztós számnak venné (**103**), és nem kapnád el a hibát!
- Dátum: `DateTime.TryParseExact(szoveg, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime datum)`.
- A sorszámhoz `for` ciklus kell (`i + 1`, mert a tömb 0-tól indexel).
- A 7. kérdéshez a dátum szerint csoportosíts: `GroupBy(m => m.Datum)`.

Megoldás: [../../megoldasok/F02-idojaras-meresek/](../../megoldasok/F02-idojaras-meresek/)
