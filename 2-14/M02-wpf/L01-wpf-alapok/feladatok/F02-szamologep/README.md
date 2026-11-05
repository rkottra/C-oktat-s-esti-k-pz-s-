# 14-M02-L01-F02 · Számológép Grid-elrendezéssel

**Nehézség:** ★★☆ **Becsült idő:** 60 perc

## Feladat

Készíts WPF-alkalmazást `Szamologep` néven, amely egy egyszerű négy alapműveletes számológép.

1. Az ablak egyetlen `Grid`, 6 sorral és 4 egyforma szélességű oszloppal (`*`). Az ablak átméretezésekor a gombok együtt nőjenek.
2. Az első sorban a **kijelző**: csak olvasható `TextBox`, mind a 4 oszlopon átnyúlik (`Grid.ColumnSpan`), jobbra igazított, félkövér szöveggel, kezdőértéke `0`.
3. Gombok a szokásos elrendezésben:
   ```
   7  8  9  /
   4  5  6  *
   1  2  3  -
   0  0  C  +      ← a 0 két oszlopot foglal
   =  =  =  =      ← az = a teljes sort
   ```
4. Működés:
   - számjegy: ha új szám kezdődik (vagy a kijelzőn `0` áll), felülírja a kijelzőt, különben hozzáfűzi;
   - művelet (`+ - * /`): megjegyzi a kijelzőn álló számot és a műveletet; a következő számjegy új számot kezd;
   - `=`: elvégzi a műveletet, és kiírja az eredményt;
   - `C`: mindent alaphelyzetbe állít;
   - nullával osztáskor hibaüzenet jelenjen meg.
5. **A 10 számgombnak egyetlen közös** eseménykezelője legyen, és a 4 műveleti gombnak is egy közös.

## Minta

`1` `2` `+` `3` `0` `=` → kijelző: `42` `/` `0` `=` → *„Nullával nem lehet osztani!”*

## Tippek

- A gomb számjegyét tedd a `Tag`-be (`Tag="7"`), a közös kezelőben: `var gomb = (Button)sender; string szamjegy = gomb.Tag.ToString()!;`.
- A műveleti gombnál a `Content` maga a jel: `((Button)sender).Content.ToString()`.
- Az állapotot (első szám, művelet, kezdődik-e új szám) az ablak osztályának mezőiben tárold.
- Szorgalmi: `2 + 3 *` esetén a második műveleti gomb előbb végezze el a függő összeadást.

Megoldás: [../../megoldasok/F02-szamologep/](../../megoldasok/F02-szamologep/)
