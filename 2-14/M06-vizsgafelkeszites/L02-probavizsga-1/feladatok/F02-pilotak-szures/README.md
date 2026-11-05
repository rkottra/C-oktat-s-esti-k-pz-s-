# 14-M06-L02-F02 · Szűrés a pilóták között

**Nehézség:** ★★★ **Becsült idő:** 45 perc

## Feladat

Egészítsd ki az F01 alkalmazását (készíts róla másolatot `PilotakSzures` néven) egy szűrősávval a táblázat felett.

1. A szűrősávban legyen egy **nemzetiség** legördülő lista, amelynek első eleme `(összes)`, utána a nemzetiségek ábécérendben; induláskor az `(összes)` legyen kiválasztva.
2. Legyen egy **névrészlet** szövegdoboz: gépelés közben azonnal szűrjön (a nevet tartalmazó pilóták jelenjenek meg).
3. A két szűrő együtt is működjön (pl. `British` + `Hill` → Damon Hill, Graham Hill).
4. A táblázat alatt a felirat mutassa a megjelenített és az összes pilóta számát: `Megjelenítve: 2 / 800`.
5. A *Szűrés törlése* gomb állítsa vissza mindkét szűrőt.
6. Törlés után a szűrés maradjon érvényben; új pilóta felvétele után a szűrő álljon vissza alaphelyzetbe (az új nemzetiség is jelenjen meg a listában).

## Minta

```
Nemzetiség: [British ▼]  Névrészlet: [Hill    ]  [Szűrés törlése]
 Azonosító | Név          | Nem | Születési dátum | Nemzetiség
 14        | Damon Hill   | M   | 1960.09.17      | British
 731       | Graham Hill  | M   | 1929.02.15      | British
[Kijelölt pilóta törlése]                     Megjelenítve: 2 / 800
```

## Tippek

- A szűrés történhet az adatbázisban: `IQueryable<Pilota> q = db.Pilotak; if (…) q = q.Where(…);` – a feltételek egymás után fűzhetők.
- A `Contains` az EF Core-ban `LIKE '%…%'` lesz; a magyar rendezésű (`_ci`) oszlop miatt kis- és nagybetűt nem különböztet meg.
- A `TextChanged` és a `SelectionChanged` eseményhez ugyanaz a kezelőmetódus is köthető.
- A változó feliratot `INotifyPropertyChanged`-del vagy egyszerűen a `TextBlock.Text` beállításával frissítheted.

Megoldás: [../../megoldasok/F02-pilotak-szures/](../../megoldasok/F02-pilotak-szures/)
