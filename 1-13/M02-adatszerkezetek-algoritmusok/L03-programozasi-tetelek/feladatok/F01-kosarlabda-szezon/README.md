# 13-M02-L03-F01 · Kosárlabda-szezon

**Nehézség:** ★☆☆ **Becsült idő:** 45 perc

## Feladat

Egy iskolai kosárcsapat 16 mérkőzést játszott a szezonban. A saját és az ellenfél pontjait két **párhuzamos tömb** tárolja: az `i`. mérkőzés eredménye `sajat[i] : ellenfel[i]`. Másold be az adatokat a programodba:

```csharp
int[] sajat    = { 78, 65, 91, 70, 84, 59, 102, 88, 73, 95, 67, 81, 99, 76, 85, 90 };
int[] ellenfel = { 72, 70, 85, 70, 61, 66,  98, 90, 80, 74, 69, 79, 80, 71, 85, 87 };
```

Készíts `KosarlabdaSzezon` nevű programot, amely a következő kérdésekre válaszol. Mindegyikhez írd oda megjegyzésben, melyik **programozási tételt** használod. A tételeket saját ciklussal írd meg, a LINQ-metódusok nem használhatók.

1. Hány pontot dobott a csapat összesen, és átlagosan hányat mérkőzésenként?
2. Hány győzelem, hány döntetlen és hány vereség született?
3. Volt-e olyan mérkőzés, amelyen a csapat legalább 100 pontot dobott?
4. Hányadik mérkőzés volt az **első vereség**? (Tudjuk, hogy volt.)
5. Volt-e 20 pontnál nagyobb különbségű győzelem? Ha igen, hányadik mérkőzésen és milyen eredménnyel?
6. Melyik mérkőzésen volt a **legnagyobb pontkülönbségű győzelem**?
7. Igaz-e, hogy a csapat **minden** mérkőzésen legalább 60 pontot dobott?

## Elvárt kimenet

```
1. Összesen 1303 pont, átlagosan 81,44 pont mérkőzésenként.
2. Győzelem: 9, döntetlen: 2, vereség: 5
3. 100 pontos mérkőzés: volt
4. Az első vereség a 2. mérkőzésen volt (65:70).
5. 20 pontnál nagyobb győzelem: a 5. mérkőzésen, 84:61
6. A legnagyobb győzelem: a 5. mérkőzés, 84:61 (+23)
7. Minden mérkőzésen legalább 60 pont: nem
```

## Tippek

- A győzelem / döntetlen / vereség három megszámlálás egy ciklusban: `if … else if … else`.
- A 4. kérdés kiválasztás, ezért nincs szükség `i < n` feltételre. Az 5. kérdés keresés, ott viszont kell.
- A 6. kérdés egy maximumkiválasztás a `sajat[i] - ellenfel[i]` különbségeken.
- A 7. kérdés „minden” típusú eldöntés: keresd azt az elemet, amelyik **nem** felel meg.

Megoldás: [../../megoldasok/F01-kosarlabda-szezon/](../../megoldasok/F01-kosarlabda-szezon/)
