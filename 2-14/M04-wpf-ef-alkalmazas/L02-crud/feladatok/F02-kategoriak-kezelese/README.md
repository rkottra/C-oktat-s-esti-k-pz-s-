# 14-M04-L02-F02 · Kategóriák kezelése

**Nehézség:** ★★★ **Becsült idő:** 60 perc

## Feladat

Készíts `RaktarKategoriak` néven alkalmazást, amely a raktár **kategóriáit** kezeli. Az ablakban egy `TabControl` legyen két füllel.

**„Kategóriák” fül**

1. `ListBox` a kategóriákkal, név szerint; minden elemnél a név (félkövéren) és zárójelben a termékek száma: `Festék (3 termék)`.
2. Mellette `TextBox` a névhez és három gomb: **Új kategória felvétele**, **Kijelölt átnevezése**, **Kijelölt törlése**.
   Kijelöléskor a név bekerül a `TextBox`-ba; kijelölés nélkül az átnevezés és a törlés le van tiltva.
3. Üres név és **már létező név** (kis/nagybetűtől függetlenül) nem vehető fel; átnevezéskor a saját nevét megtarthatja.
4. Törlés: ha a kategóriában van termék, **ne engedje** (üzenet), egyébként megerősítés után töröljön.

**„Termékek” fül**

5. Csak olvasható `DataGrid` a termékekkel és a kategóriájuk nevével. Átnevezés után itt is az új név látsszon.

## Minta működés

```
Kategóriák:                      Kategória neve: [Festék          ]
  Elektromos gép (3 termék)      [Új kategória felvétele]
  Festék (3 termék)   ◄          [Kijelölt átnevezése  ]
  Kertészet (3 termék)           [Kijelölt törlése     ]
  Kéziszerszám (3 termék)

[Kijelölt törlése] → „A(z) „Festék” kategóriában 3 termék van, ezért nem törölhető.”
```

## Tippek

- A termékszámhoz töltsd be a kapcsolt listát: `db.Kategoriak.Include(k => k.Termekek)`, a sablonban `{Binding Termekek.Count}`.
- `StringFormat`-ban szóközzel kezdődő vagy zárójeles szöveget aposztrófok közé tegyél: `StringFormat='({0} termék)'`.
- Létező név: `db.Kategoriak.Any(k => k.Nev.ToLower() == nev.ToLower() && k.Id != sajatId)`.
- Miért kell a törlés előtti ellenőrzés? A `Termek.KategoriaId` kötelező, ezért az EF Core alapbeállítással
  **a kategória termékeit is törölné** (kaszkádolt törlés). Próbáld ki egy tesztadatbázison, mi történne nélküle!

Megoldás: [../../megoldasok/F02-kategoriak-kezelese/](../../megoldasok/F02-kategoriak-kezelese/)
