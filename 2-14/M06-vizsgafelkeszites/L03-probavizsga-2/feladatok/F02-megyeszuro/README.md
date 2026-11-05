# 14-M06-L03-F02 · Megyeszűrő és összesítés

**Nehézség:** ★★★ **Becsült idő:** 40 perc

## Feladat

Egészítsd ki az F01 grafikus megoldását (másolat `TelepulesekSzures` néven):

1. A táblázat fölé kerüljön egy *Megye:* legördülő lista. Első eleme `(összes megye)`, utána a megyék ábécérendben. Induláskor az első elem legyen kiválasztva.
2. Megye kiválasztásakor a táblázat csak az adott megye városait mutassa (a szűrés az adatbázis-lekérdezésben történjen, `Where`-rel).
3. A lista mellett egy felirat összesítse a **megjelenített** városokat: darabszám, összlakosság ezres tagolással, átlagos népsűrűség (összlakosság / összterület) egészre kerekítve.
4. Új város felvétele után a szűrés maradjon érvényben.

## Minta

```
Megye: [Nógrád            ▼]  6 város, 83 350 lakos, átlagos népsűrűség: 244 fő/km²
```

## Tippek

- Egy „álmegye” (`new Megye { Id = 0, Nev = "(összes megye)" }`) a lista elejére tehető; `Id == 0` esetén nincs szűrés.
- `IQueryable<Telepules> q = db.Telepulesek.Include(t => t.Megye); if (id != 0) q = q.Where(t => t.MegyeId == id);`
- Ezres tagolás: `{lakos:N0}`.

Megoldás: [../../megoldasok/F02-megyeszuro/](../../megoldasok/F02-megyeszuro/)
