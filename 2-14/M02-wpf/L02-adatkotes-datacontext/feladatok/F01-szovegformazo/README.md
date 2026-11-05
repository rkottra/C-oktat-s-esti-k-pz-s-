# 14-M02-L02-F01 · Szövegformázó – kötések kód nélkül

**Nehézség:** ★☆☆ **Becsült idő:** 30 perc

## Feladat

Készíts WPF-alkalmazást `SzovegFormazo` néven. **A mögöttes kódba (`MainWindow.xaml.cs`) ne írj semmit** –
minden kapcsolat `Binding` legyen.

1. Az ablak két oszlopból áll: bal oldalon a beállítások, jobb oldalon egy keretes (`Border`) előnézet.
2. Beállítások:
   - `TextBox` a szöveghez (kezdőérték: `Helló, WPF!`);
   - `Slider` a betűmérethez (8–60, kezdőérték 24, egész lépésekkel), alatta egy kis `TextBox`, amely mutatja **és állítja** is a méretet;
   - `ComboBox` színnevekkel: `Black`, `DarkRed`, `SeaGreen`, `RoyalBlue`;
   - `CheckBox`: **Látható** (alapból bejelölve).
3. Az előnézet egy `TextBlock`:
   - a szövege már gépelés közben kövesse a beírt szöveget;
   - a betűmérete a csúszkát, a színe a kiválasztott színt kövesse;
   - a `CheckBox` kikapcsolásakor tűnjön el.
4. Az előnézet alatt szürkével: `Karakterek száma: 11`.

## Minta

```
Szöveg:     [Helló, WPF!   ]        ┌──────────────────────┐
Betűméret:  ───────●───────          │  Helló, WPF!         │  (24-es, RoyalBlue)
            [24]                     │                      │
Szín:       [RoyalBlue   v]          │  Karakterek száma: 11│
[x] Látható                          └──────────────────────┘
```

## Tippek

- Szín: `Foreground="{Binding ElementName=Szin, Path=SelectedItem.Content}"` – a szöveget a WPF maga alakítja ecsetté.
- Láthatóság: a `Window.Resources`-ba vegyél fel egy `<BooleanToVisibilityConverter x:Key="BoolToVis"/>` elemet, és használd: `Converter={StaticResource BoolToVis}`.
- Karakterszám: a `Path` több szintű is lehet: `Path=Text.Length`.
- Formázás: `StringFormat='Karakterek száma: {0}'`.

Megoldás: [../../megoldasok/F01-szovegformazo/](../../megoldasok/F01-szovegformazo/)
