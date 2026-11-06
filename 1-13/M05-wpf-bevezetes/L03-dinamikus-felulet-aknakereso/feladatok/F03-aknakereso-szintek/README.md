# 13-M05-L03-F03 · Aknakereső: szintek, gyors felfedés, rekordok

**Nehézség:** ★★★ **Becsült idő:** 120 perc

## Feladat

Készíts `AknakeresoPlusz` nevű WPF-alkalmazást az F02 aknakeresőjének bővítéseként.

1. **Szintek:** egy `ComboBox`-ból választható nehézség. A szinteket egy `Szint` rekord írja le (név, táblaméret, aknaszám):

   | Szint | Tábla | Aknák |
   |---|---|---|
   | Kezdő | 8×8 | 9 |
   | Haladó | 12×12 | 22 |
   | Mester | 16×16 | 45 |

   Szintváltáskor (és az **Új játék** gombra) a táblát a program **újraépíti** a megfelelő mérettel. A gombok betűmérete igazodjon a táblamérethez.
2. **Nyitó első lépés:** az első kattintott mezőre **és a 8 szomszédjára** sem kerülhet akna, így az első lépés mindig egy 0-s mezőt fed fel, és „kinyitja” a táblát.
3. **Jelölések:** a jobb klikk körbe léptet: semmi → zászló (`⚑`) → kérdőjel (`?`) → semmi. Az aknaszámláló csak a zászlókat veszi figyelembe; a kérdőjeles mező felfedhető.
4. **Gyors felfedés:** bal kattintás egy **már felfedett számra**: ha körülötte pontosan annyi zászló van, amennyi a szám, a többi fedett szomszéd egyszerre felfedődik (elárasztással együtt). Ha valamelyik zászló rossz helyen volt, ez aknára léphet, és a játék elvész.
5. **Elárasztás várakozó sorral:** a felfedést most rekurzió helyett `Queue<(int, int)>`-vel írd meg.
6. **Rekordok:** szintenként a legjobb (legrövidebb) nyerő idő. A rekordok a program mellett egy `rekordok.txt` fájlban vannak, soronként `Szint;másodperc` alakban (például `Kezdő;37`). Indításkor betöltődnek, új rekordnál mentődnek. A tábla felett: `Rekord: 37 mp` vagy `Rekord: még nincs`. Új rekordnál a gratuláló üzenet ezt is közli.

## Tippek

- `public record Szint(string Nev, int Meret, int Akna)`, és a `ToString()` felülírásával a `ComboBox` szépen kiírja: `Kezdő (8×8, 9 akna)`.
- A tömböket és a `Button[,]`-t ne konstans mérettel hozd létre, hanem az `UjJatek()`-ban, az aktuális szint méretével.
- A jelölés állapota lehet egy `int[,] jel` (0 = semmi, 1 = zászló, 2 = kérdőjel); a léptetés `jel[s, o] = (jel[s, o] + 1) % 3;`.
- Írj egy `IEnumerable<(int, int)> Szomszedok(int sor, int oszlop)` metódust `yield return`-nel. A számok kiszámolása, a gyors felfedés és az elárasztás is ezt használja, így a határ-ellenőrzés egyetlen helyen van.
- A rekordokhoz `Dictionary<string, int>`, a fájl útja `Path.Combine(AppContext.BaseDirectory, "rekordok.txt")`. Betöltésnél a hiányzó fájl nem hiba: ilyenkor még nincs rekord.

Megoldás: [../../megoldasok/F03-aknakereso-szintek/](../../megoldasok/F03-aknakereso-szintek/)
