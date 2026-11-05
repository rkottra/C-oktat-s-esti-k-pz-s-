# 14-M03-L03-F01 · Pizzéria adatbázis scaffoldolása

**Nehézség:** ★☆☆ **Becsült idő:** 35 perc

## Feladat

A 14-M03-L02-ben megismert pizzéria adatbázisból generálj C#-osztályokat, és kérdezd le az adatokat.

1. Importáld a [pizzeria.sql](pizzeria.sql) szkriptet phpMyAdminban (létrejön az `estis_pizzeria` adatbázis).
2. Hozz létre egy új .NET 8 konzolos projektet `PizzeriaScaffold` néven, és telepítsd a NuGet-csomagokat:
   `Pomelo.EntityFrameworkCore.MySql` (8.0.x) és `Microsoft.EntityFrameworkCore.Tools` (8.0.x).
3. Package Manager Console-ban generáld le a modellt (egy sorba!):
   ```
   Scaffold-DbContext "server=localhost;database=estis_pizzeria;user=root;password=" Pomelo.EntityFrameworkCore.MySql -OutputDir Models -NoPluralize -Force
   ```
4. Nézd meg a generált fájlokat, és válaszolj (megjegyzésként a `Program.cs` elején):
   - Hány osztály jött létre, és mi a context osztály neve?
   - Melyik osztálynak nincs `Id` tulajdonsága? Hol és hogyan van megadva az elsődleges kulcsa?
   - Melyik tulajdonság felel meg a `rendeles.vevo_id` oszlopnak, és milyen tulajdonság hivatkozik magára a vevőre?
   - Hol látszik a kódban, hogy a futár telefonszáma egyedi?
5. A program írja ki:
   - a pizzákat ár szerint növekvő sorrendben, és az átlagárat;
   - a 2800 Ft-nál olcsóbb pizzák nevét;
   - a futárokat névsorban, telefonszámmal;
   - a rendelések számát, az első és az utolsó rendelés időpontját, valamint a délelőtti (12 óra előtti) rendelések számát;
   - ha még nincs „Bolognai” nevű pizza, vegye fel 2990 Ft-os áron.

## Minta kimenet

```
Pizzák ár szerint:
  Margherita    2490 Ft
  Gombás        2690 Ft
  ...
Átlagár: 2823 Ft
2800 Ft alatt: Margherita, Sonkás, Gombás

Futárok:
  Gyors Gergő      +36301112233
  ...
Rendelések száma: 18
Első rendelés:    2026.10.01. 11:45
Utolsó rendelés:  2026.10.07. 21:20
Délelőtti rendelések: 3
```

## Tippek

- A `-NoPluralize` nélkül az angol többesszám-képzés a `rendeles` táblából `Rendele` osztályt csinálna
  (levágja a „végződést”). Próbáld ki egy másik mappába (`-OutputDir Proba`), aztán töröld!
- A dátum formázása: `{datum:yyyy.MM.dd. HH:mm}`.
- Ha a scaffold „Unable to connect” hibát ad, nem fut a MySQL, vagy elírtad az adatbázis nevét.

Megoldás: [../../megoldasok/F01-pizzeria-scaffold/](../../megoldasok/F01-pizzeria-scaffold/)
