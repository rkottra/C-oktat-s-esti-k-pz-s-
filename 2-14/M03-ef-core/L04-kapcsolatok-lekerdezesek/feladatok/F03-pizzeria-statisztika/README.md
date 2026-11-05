# 14-M03-L04-F03 · Pizzéria-statisztika (több tábla, N:M)

**Nehézség:** ★★★ **Becsült idő:** 70 perc

## Feladat

A pizzéria adatbázisban ([pizzeria.sql](pizzeria.sql), ismerős a 14-M03-L02-ből és az L03-F01-ből) öt tábla van;
a `rendeles` és a `pizza` között a `tetel` kapcsolótábla teremt N:M kapcsolatot. Készíts statisztikát EF Core-ral.

1. Importáld (újra) a `pizzeria.sql`-t – így az esetleg korábban felvett pizzák eltűnnek.
2. Új .NET 8 konzolos projekt `PizzeriaStatisztika` néven; scaffold `-NoPluralize` kapcsolóval a `Models` mappába.
3. A program írja ki:
   1. az időrendben első 3 rendelést: sorszám, időpont, vevő, futár, a rendelés értéke, és alatta a tételek
      (darab × pizzanév) – egyetlen lekérdezéssel betöltve (`Include` / `ThenInclude`);
   2. vevőnként az elköltött összeget, csökkenő sorrendben;
   3. futáronként a szállítások és a kivitt pizzák számát;
   4. a pizzák népszerűségi sorrendjét (eladott darab), és azokat a pizzákat, amelyekből még senki nem rendelt;
   5. a napi bevételeket és a legjobb napot;
   6. kérjen be egy vevőnevet, és írja ki a kedvenc pizzáját (amelyből a legtöbbet rendelte).

## Minta kimenet

```
Az első 3 rendelés:
  #1 10.01. 11:45  Éhes Elemér ← Gyors Gergő, 4980 Ft
       2 × Margherita
  #2 10.01. 18:20  Falatka Fanni ← Robogó Rita, 6080 Ft
       1 × Hawaii
       1 × Négysajtos
  ...
Vevők költése:
  Éhes Elemér       27100 Ft
  Sajtos Sára       25010 Ft
  ...
Futárok:
  Robogó Rita      6 szállítás, 13 pizza
  ...
Népszerűség:
  Hawaii        11 db
  Margherita     8 db
  Négysajtos     8 db
  ...
Még senki nem rendelte: Gombás

Napi bevétel:
  2026.10.01.   11060 Ft
  ...
A legjobb nap: 2026.10.07. (28900 Ft)

Vevő neve: Éhes Elemér
  Kedvence: Margherita (4 db)
```

## Tippek

- Egy rendelés értéke: `r.Tetel.Sum(t => t.Db * t.Pizza.Ar)` – ehhez a tételek **és** a pizzák is legyenek betöltve.
- Több szint betöltése: `.Include(r => r.Tetel).ThenInclude(t => t.Pizza)`.
- Vevő → rendelések → tételek „kilapítása”: `v.Rendeles.SelectMany(r => r.Tetel)`.
- Dátum szerinti csoportosítás: `GroupBy(t => t.Rendeles.Idopont.Date)`.
- Gondold végig, melyik táblából érdemes indulni: a „vevőnként” kérdésnél a `Vevo`-ból, a „naponta” kérdésnél a `Tetel`-ből.

Megoldás: [../../megoldasok/F03-pizzeria-statisztika/](../../megoldasok/F03-pizzeria-statisztika/)
