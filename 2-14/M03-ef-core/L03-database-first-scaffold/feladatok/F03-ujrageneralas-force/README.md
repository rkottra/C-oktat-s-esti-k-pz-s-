# 14-M03-L03-F03 · Változott az adatbázis: újragenerálás -Force kapcsolóval

**Nehézség:** ★★☆ **Becsült idő:** 35 perc

## Feladat

Database First esetén az adatbázis a „főnök”: ha valaki módosítja a szerkezetét, a C#-modellt újra kell generálni.
Ebben a feladatban azt is kipróbálod, mi marad meg a saját kódodból, és mi vész el.

1. Készíts másolatot a bemutató példáról ([OregfakScaffold](../../peldak/OregfakScaffold/)) `OregfakForce` néven
   (vagy scaffoldold újra egy új projektbe a példa parancsával).
2. **Szándékos hiba:** a generált `Models/Fa.cs`-be írj be kézzel egy metódust:
   ```csharp
   public bool Ritka() => Faj.Contains("platán");
   ```
   és használd is a `Program.cs`-ben. Fordítsd le – működik.
3. Futtasd phpMyAdminban a [valtozas.sql](valtozas.sql) szkriptet (két új oszlop a `fa` táblában).
4. Futtasd a programot **újragenerálás nélkül**. Működik? Mit gondolsz, miért? (A `vedett` oszlopot a program még nem ismeri.)
5. Generáld újra a modellt:
   ```
   Scaffold-DbContext "server=localhost;database=estis_oregfak;user=root;password=" Pomelo.EntityFrameworkCore.MySql -OutputDir Models -Force
   ```
   Mi történt a `Ritka()` metódussal? Megmaradt-e a `Kiegeszitesek/FaKiegeszites.cs` tartalma?
   Milyen típusú lett a két új tulajdonság, és miért pont az?
6. Javítsd ki a hibát: a `Ritka()` metódus kerüljön a `FaKiegeszites.cs`-be.
7. A program írja ki a védett fák számát, és fajonként a védett fák listáját (faj, település, törzskerület),
   majd a legvastagabb fához írja be megjegyzésnek: „Az adatbázis legvastagabb fája.” – és mentse.

## Minta kimenet

```
Védett fák: 16
fehér nyár             1013 cm  Süttő
fekete nyár            1010 cm  Kőröstarcsa
juharlevelű platán      992 cm  Pápa
...
Megjegyzés mentve: fehér nyár (Süttő)
```

## Ellenőrző kérdések

- Miért nem szabad a generált fájlokat kézzel szerkeszteni?
- Mi a különbség a Code First migráció és a Database First újragenerálás iránya között?
- Mi történne `-Force` nélkül?

Megoldás: [../../megoldasok/F03-ujrageneralas-force/](../../megoldasok/F03-ujrageneralas-force/)
