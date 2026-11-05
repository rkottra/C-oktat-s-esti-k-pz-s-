# 14-M04-L03-F02 · Ellenőrzött mentés és hibakezelés

**Nehézség:** ★★★ **Becsült idő:** 60 perc

## Feladat

Az L02-F01 termékkezelő alkalmazást (új projekt: `RaktarValidacio`) tedd „bolondbiztossá”.

**Validáció** – mentés előtt ellenőrizd az összes mezőt, és **az összes hibát egyszerre** írd ki az űrlap alá, pirossal;
a hibás mezők kerete legyen piros:

| Mező | Szabály |
|---|---|
| Cikkszám | két betű, kötőjel, három számjegy (`KS-004`); nagybetűsítés után ellenőrizd |
| Cikkszám | egyedi – módosításkor a saját cikkszáma megmaradhat |
| Név | 2–80 karakter |
| Ár | egész, 1 és 10 000 000 között |
| Darab | egész, 0 és 100 000 között |
| Kategória | kötelező kiválasztani |

**Kivételkezelés**

1. Az adatbázis betöltése `try-catch`-ben: hiba esetén üzenet, és az űrlap legyen letiltva.
2. A `SaveChanges` hívás `try-catch`-ben: `DbUpdateException` esetén az `InnerException` üzenetét mutasd, minden más hibánál az általánosat.
3. Sikertelen mentés után dobd el a mentetlen változásokat (`ChangeTracker.Clear()`), és töltsd újra az adatokat.
4. A törlés is ugyanezen a hibakezelésen menjen át.

## Minta kimenet

```
Cikkszám [ks4        ]  ← piros keret
Név      [A          ]  ← piros keret
Ár       [olcsó      ]  ← piros keret
Darab    [5          ]
Kategória[           ]  ← piros keret
[Új] [Mentés] [Törlés]
A cikkszám formátuma: két betű, kötőjel, három számjegy (pl. KS-004).
A név 2–80 karakter hosszú legyen.
Az ár 1 és 10 000 000 Ft közötti egész szám legyen.
Válassz kategóriát!
```

## Tippek

- `using System.Text.RegularExpressions;` → `Regex.IsMatch(cikkszam, @"^[A-Z]{2}-\d{3}$")`.
- Az ellenőrzés adjon vissza `List<string>`-et; ha üres, mehet a mentés.
- Az eredeti keretszínt a konstruktorban mentsd el (`txtNev.BorderBrush`), azzal állítsd vissza.
- Próba: állítsd le a MySQL-t futás közben, és nyomj Mentést – az alkalmazás nem omolhat össze.

Megoldás: [../../megoldasok/F02-validacio/](../../megoldasok/F02-validacio/)
