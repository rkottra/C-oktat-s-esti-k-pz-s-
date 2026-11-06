# 13-M05-L03-F02 · Aknakereső: elárasztás, zászló, időmérés

**Nehézség:** ★★☆ **Becsült idő:** 90 perc

## Feladat

Készíts `Aknakereso` nevű WPF-alkalmazást, egy teljes, játszható aknakeresőt. Kiindulhatsz az F01 megoldásából.

1. **Játéktér:** 10×10 mező, 15 aknával. A tábla felett három kijelző: a hátralévő aknák száma (`Aknák: 15`), az eltelt idő (`Idő: 0 mp`) és egy állapotsor, mellettük az **Új játék** gomb.
2. **Biztonságos első lépés:** az aknák csak az **első kattintás után** kerülnek le, és a kattintott mezőre nem kerülhet akna.
3. **Elárasztásos felfedés:** ha a felfedett mezőn 0 áll, a szomszédjai is automatikusan felfedődnek, és ha köztük is van 0-s, az is tovább terjed. Ezt **rekurzív** metódussal oldd meg.
4. **Zászló:** a jobb egérgomb egy fedett mezőre zászlót tesz (`⚑`), újabb jobb kattintás leveszi. A zászlós mező bal kattintásra nem fedődik fel, és az elárasztás sem fedi fel. Az aknaszámláló a lerakott aknák és a zászlók különbségét mutatja (negatív is lehet).
5. **Időmérés:** az óra az első kattintással indul, másodpercenként frissül, és a játék végén megáll.
6. **Vesztés:** aknára lépve minden meg nem jelölt akna előkerül, a rálépett mező piros, a **tévesen** megjelölt mezőkön `✗` látszik.
7. **Nyerés:** ha minden biztonságos mező fel van fedve, a megmaradt aknák is zászlót kapnak, és egy `MessageBox` kiírja az időt.

## Tippek

- A modell az F01-hez képest: `bool[,] zaszlo`, `bool aknakLerakva`, `int zaszloDb`, `int masodperc`, valamint egy `DispatcherTimer ora`.
- Jobb klikk: `gomb.MouseRightButtonUp += Cella_JobbKlikk;`, a kezelő paramétere `MouseButtonEventArgs` (`using System.Windows.Input;`).
- A rekurzív `Felfed(s, o)` elején két feltétel állítja meg a hívást: a táblán kívül vagyunk, vagy a mező már fel van fedve (vagy zászlós). A második nélkül a program végtelen rekurzióba kerül!
- A nyerés ellenőrzése: `felfedettDb == N * N - AknaDb`. A `felfedettDb`-t a `Felfed` növelje, így az elárasztás során felfedett mezők is beleszámítanak.

Megoldás: [../../megoldasok/F02-aknakereso/](../../megoldasok/F02-aknakereso/)
