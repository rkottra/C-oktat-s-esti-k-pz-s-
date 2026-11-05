# 14-M04-L03-F01 · Élő keresés és rendezés a raktárban

**Nehézség:** ★★☆ **Becsült idő:** 45 perc

## Feladat

Készíts `RaktarKereses` néven csak olvasó raktárböngészőt (`estis_raktar`):

1. Felül keresősáv:
   - `TextBox`: gépelés közben szűr a termék **nevére vagy cikkszámára** (részlet, kis/nagybetű nem számít);
   - kategória-`ComboBox` „(összes)” elemmel;
   - rendezés-`ComboBox`: név szerint / ár szerint növekvő / ár szerint csökkenő / érték szerint csökkenő;
   - `CheckBox`: „csak hiánycikkek” (0 darab).
2. A szűrők tetszőlegesen kombinálhatók, és bármelyik változásakor azonnal frissül a táblázat.
3. Alul: a találatok száma és összértéke.
4. Az adatokat induláskor **egyszer** töltsd be a memóriába; a keresés ne forduljon minden leütésnél az adatbázishoz.
5. Ha az adatbázis nem érhető el, a program ne omoljon össze: írjon üzenetet, és induljon el üres listával.

## Minta működés

```
Keresés: [fes  ]  Kategória: [(összes) ▼]  Rendezés: [ár szerint ↓ ▼]  [ ] csak hiánycikkek

FS-001  Beltéri falfesték 5 l   Festék      8 990   14   125 860
FS-003  Festőhenger             Festék      1 490    0         0
2 találat, összérték: 125 860 Ft

+ [x] csak hiánycikkek  →  csak az FS-003 marad: 1 találat, összérték: 0 Ft
```

## Tippek

- `IEnumerable<Termek> talalat = osszesTermek;` majd feltételesen `talalat = talalat.Where(...)`.
- `t.Nev.Contains(keresett, StringComparison.OrdinalIgnoreCase)`.
- Rendezés: `talalat = cmbRendezes.SelectedIndex switch { 1 => talalat.OrderBy(t => t.Ar), ..., _ => talalat.OrderBy(t => t.Nev) };`
- Mind a négy vezérlő ugyanazt az eseménykezelőt hívhatja (`TextChanged`, `SelectionChanged`, `Click`).
- Ha a ComboBox egyik eleme `IsSelected="True"`, az esemény már az `InitializeComponent` közben lefut – a metódus elején nézd meg, létezik-e már a `DataGrid`.

Megoldás: [../../megoldasok/F01-elo-kereses/](../../megoldasok/F01-elo-kereses/)
