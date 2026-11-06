# 13-M05-L02-F01 · Teendőlista

**Nehézség:** ★☆☆ **Becsült idő:** 45 perc

## Feladat

Készíts `Teendolista` nevű WPF-alkalmazást, amelyben a napi teendőket lehet nyilvántartani.

1. **Felület:** felül egy `TextBox` és egy **Hozzáad** gomb egy sorban, alatta a teendők `ListBox`-a, legalul egy gombsor (**Kész**, **Fel**, **Le**, **Töröl**, **Mind törlése**) és egy állapotsor, például `5 teendő, ebből 2 kész`.
2. **Hozzáadás:** a beírt szöveg a lista végére kerül, a szövegdoboz kiürül, és visszakapja a fókuszt. Üres szöveg vagy olyan teendő, ami már szerepel, nem vehető fel. Az Enter is a Hozzáad gombot nyomja meg.
3. **Kész:** a kijelölt teendő elé `✓ ` kerül; ha már ott van, újabb nyomásra lekerül.
4. **Fel / Le:** a kijelölt teendőt egy hellyel feljebb vagy lejjebb viszi, és a kijelölés vele marad.
5. **Töröl:** törli a kijelölt teendőt. **Mind törlése:** megerősítés után (`MessageBox`, Igen/Nem) a teljes listát.
6. A gombok csak akkor legyenek engedélyezve, ha van értelmük. A Hozzáad csak nem üres szövegnél, a Kész és a Töröl csak kijelöléskor, a Fel nem az első, a Le nem az utolsó elemnél, a Mind törlése csak nem üres listánál. Mindezt egy `Frissit()` metódus állítsa be.

## Minta

```
┌───────────────────────────────────────────┐
│ [Matek házi                   ] [Hozzáad] │
│ ┌───────────────────────────────────────┐ │
│ │ ✓ Bevásárlás                          │ │
│ │ Edzés 17:00                           │ │
│ │▒Matek házi▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒│ │
│ └───────────────────────────────────────┘ │
│ [Kész] [Fel] [Le] [Töröl] [Mind törlése]  │
│ 3 teendő, ebből 1 kész                    │
└───────────────────────────────────────────┘
```

## Tippek

- Mozgatás: jegyezd meg az elemet és az indexét, töröld (`RemoveAt`), szúrd be az új helyre (`Insert`), majd állítsd be a `SelectedIndex`-et.
- A „kész” jelölés: `szoveg.StartsWith("✓ ")` vizsgálat; levétele: `szoveg[2..]`.
- A kész teendők megszámlálásához járd be az `Items`-et: `foreach (object o in TeendoLista.Items)`.

Megoldás: [../../megoldasok/F01-teendolista/](../../megoldasok/F01-teendolista/)
