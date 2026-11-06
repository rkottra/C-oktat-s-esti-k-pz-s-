# 13-M04-L02-F01 · Iskolai könyvtár katalógusa

**Nehézség:** ★★☆ **Becsült idő:** 60 perc

## Feladat

Az iskolai könyvtár állományát a [konyvek.csv](konyvek.csv) fájl tartalmazza:

```
Azonosito;Cim;Szerzo;Mufaj;BeszerzesEve;Peldany;Kolcsonzott
K001;Egri csillagok;Gárdonyi Géza;regény;2015;12;9
```

A `Peldany` a könyvtár összes példánya, a `Kolcsonzott` azoké, amelyek éppen ki vannak kölcsönözve. Készíts `KonyvtarKatalogus` nevű menüs .NET 8 konzolos alkalmazást.

1. **`Konyv`** osztály: a hét adat és a konstruktor, amely egy CSV-sorból tölti ki őket. Számított tulajdonságok: `Elerheto` (a polcon lévő példányok száma) és `Kihasznaltsag` (a kölcsönzött példányok aránya százalékban). Legyen `ToString()`-je is.
2. A program induláskor olvassa be a fájlt egy `List<Konyv>` listába. Ha a fájl hiányzik, írjon hibaüzenetet, és álljon le.
3. A menü:

   ```
   1 – Teljes katalógus
   2 – Keresés szerzőre (névrészlet)
   3 – Böngészés műfaj szerint
   4 – Kölcsönözhető-e? (cím alapján)
   5 – Statisztika
   6 – Népszerűségi toplista mentése
   0 – Kilépés
   ```

   - **1:** táblázatosan, fejléccel, az összes könyv.
   - **2:** a megadott részletet tartalmazó szerzőnevű könyvek (kis- és nagybetűtől függetlenül). Ha nincs találat, írd ki.
   - **3:** almenü. A program a fájlban előforduló **műfajokat** sorszámozva kínálja fel; ezeket a beolvasott adatokból gyűjtsd ki, ne írd be kézzel. A választott műfaj könyveit listázza.
   - **4:** bekér egy címet. Ha van ilyen könyv, kiírja, hány példány van a polcon; ha mind ki van kölcsönözve, akkor ezt.
   - **5:** az összes példány száma, a kikölcsönzött példányok aránya, a teljesen kikölcsönzött címek listája, és műfajonként a címek száma.
   - **6:** a könyveket kihasználtság szerint csökkenő sorrendben a `toplista.txt` fájlba menti, `helyezés;cím;kihasználtság%` formában.
4. Minden menüpontot külön metódus valósítson meg, és a beolvasáshoz használj ellenőrző segédmetódusokat (`BekerEgesz`, `BekerSzoveg`).

## Minta kimenet (részlet)

```
Választás: 4
Cím: abigél
Abigél (Szabó Magda): minden példány ki van kölcsönözve (10/10).

Választás: 5
Összes példány: 231, ebből kikölcsönözve: 134 (58,0 %)
Teljesen kikölcsönzött címek: A Pál utcai fiúk, Abigél, Állatfarm, Harry Potter és a bölcsek köve, A Gyűrűk Ura
Műfajonként:
  regény          10 cím
  ifjúsági         5 cím
  ...
```

## Tippek

- A különböző műfajok kigyűjtése: `if (!mufajok.Contains(k.Mufaj)) mufajok.Add(k.Mufaj);`
- Az almenüben a választott sorszámból index lesz: `mufajok[valasztas - 1]`.
- A rendezés egy másolaton történjen (`new List<Konyv>(konyvek)`), hogy a katalógus sorrendje ne változzon.
- A százalékos arányhoz `(double)` átalakítás kell.

Megoldás: [../../megoldasok/F01-konyvtar-katalogus/](../../megoldasok/F01-konyvtar-katalogus/)
