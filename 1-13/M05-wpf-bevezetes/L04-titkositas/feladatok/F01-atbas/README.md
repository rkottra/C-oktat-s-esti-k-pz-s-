# 13-M05-L04-F01 · Atbas és ROT13

**Nehézség:** ★☆☆ **Becsült idő:** 45 perc

## Feladat

Készíts `Atbas` nevű WPF-alkalmazást két „önmagát visszafejtő” rejtjellel.

1. **Logika** egy `static class Rejtjelek` osztályban (külön fájlban):
   - `static string Atbas(string szoveg)`: az ábécét megfordítja (`A ↔ Z`, `B ↔ Y`, …, `a ↔ z`), minden más karakter változatlan;
   - `static string Rot13(string szoveg)`: minden betűt 13 hellyel tol el, körbefordulással;
   - `static bool Betu(char c)`: igaz, ha `c` az angol ábécé kis- vagy nagybetűje.
2. **Felület:**
   - felül egy többsoros bemeneti `TextBox`;
   - középen két `RadioButton` (Atbas / ROT13) és egy **Alkalmaz** gomb;
   - alul a csak olvasható kimeneti `TextBox`;
   - egy **⇅** gomb, amely a kimenetet visszamásolja a bemenetbe;
   - egy állapotsor: `Betűk: 23, egyéb karakter: 6`.
3. Próbáld ki: ugyanazt a rejtjelet kétszer alkalmazva vissza kell kapnod az eredeti szöveget. Írd az ablak aljára egy `TextBlock`-ba, hogy miért.

## Minta

```
Bemenet:  Hello, World!
Atbas:    Svool, Dliow!
ROT13:    Uryyb, Jbeyq!
```

## Tippek

- Atbas nagybetűre: `(char)('Z' - (c - 'A'))`, kisbetűre ugyanígy `'a'` és `'z'` mellett.
- ROT13: `(char)('A' + (c - 'A' + 13) % 26)`. Itt nem lehet negatív a maradék, mert csak pozitív irányba tolunk.
- A `char.IsLetter` az ékezetes betűkre is igaz, ezért a `Betu` metódusban tartományt vizsgálj (`c >= 'A' && c <= 'Z'`).

Megoldás: [../../megoldasok/F01-atbas/](../../megoldasok/F01-atbas/)
