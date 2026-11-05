# 14-M05-L01-F02 · Repository-csere és Singleton

**Nehézség:** ★★☆ **Becsült idő:** 40 perc

## Feladat

Az F01 megoldását (saját vagy a [mintamegoldás](../../megoldasok/F01-retegekre-bontas/)) fejleszd tovább két tervezési mintával.

1. **Repository-csere**: készíts egy `MemoriaTermekRepository` osztályt, amely szintén megvalósítja az `ITermekRepository`-t,
   de fájl helyett 4–5 beégetett terméket ad vissza.
2. A program induláskor kérdezze meg: `Adatforrás (1 = CSV-fájl, 2 = memória):`, és ennek megfelelően hozza létre
   a repositoryt. A `KeszletSzolgaltatas` kódját ehhez **nem szabad** módosítani!
3. **Singleton**: készíts `Beallitasok` osztályt a Singleton minta szerint (`sealed`, privát konstruktor,
   statikus `Peldany` tulajdonság). Tárolja a CSV-fájl nevét (`CsvFajl`) és az utánrendelési határt (`UtanrendelesiHatar`, alapértéke 5).
4. A `KeszletSzolgaltatas` a határt a `Beallitasok.Peldany`-ból vegye. A program elején lehessen megadni a határt
   (üres bemenet esetén maradjon 5).
5. Írd ki, hogy a `Beallitasok.Peldany` kétszeri lekérése ugyanazt az objektumot adja-e (`ReferenceEquals`).

## Minta kimenet

```
Ugyanaz a példány? True
Utánrendelési határ (Enter = 5): 3
Adatforrás (1 = CSV-fájl, 2 = memória): 2
Beolvasott termékek: 4

Utánrendelendő (3 db alatt):
  Külső SSD 1 TB – 2 db
  HDMI kábel 2 m – 1 db
...
```

## Ellenőrző kérdések

- Mi történik, ha a `Program.cs`-ben `new Beallitasok()`-t írsz? Miért?
- Mit kellene módosítani, ha holnaptól az adatok MySQL-adatbázisból (EF Core) jönnének? Melyik osztályok maradnak változatlanok?

## Tippek

- Az interfész típusú változóba bármelyik megvalósítás betehető: `ITermekRepository repo = ... ? new A() : new B();`
- A hibás sorok kiírásánál: `if (repository is CsvTermekRepository csv) ...`

Megoldás: [../../megoldasok/F02-repository-singleton/](../../megoldasok/F02-repository-singleton/)
