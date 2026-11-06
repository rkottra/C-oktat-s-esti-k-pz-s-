# 13-M05-L05-F01 · Profilkártya

**Nehézség:** ★☆☆ **Becsült idő:** 45 perc

## Feladat

Készíts `Profilkartya` nevű WPF-alkalmazást, amelyben egy űrlap kitöltése közben **élőben** frissül egy névjegykártya előnézete. Eseménykezelőt csak a gombokhoz írj, minden más adatkötéssel működjön.

1. **`Szemely`** osztály `INotifyPropertyChanged`-del:
   - `Nev`, `Foglalkozas`, `Varos`, `SzuletesiEv` (`int`), `Szin` (a kártya háttérszínének neve, például `"LightSteelBlue"`);
   - számított tulajdonságok: `Kor` (2026-hoz viszonyítva) és `Monogram` (a névtagok kezdőbetűi). Ezekről is értesíteni kell, ha a forrásuk változik.
2. **Bal oldal (űrlap):** `TextBox`-ok a négy adathoz (`UpdateSourceTrigger=PropertyChanged`), egy `ComboBox` 4–5 színnel, amely a `Szin`-hez van kötve, és egy `Slider` a kártya betűméretéhez (12–28).
3. **Jobb oldal (kártya):** egy `Border` lekerekített sarkokkal, háttere a `Szin`. Benne nagy betűvel a monogram, alatta a név, a foglalkozás, és `Budapest · 17 éves` formában a város és a kor. A betűméret a `Slider` értékéhez legyen kötve (`ElementName`).
4. Két gomb, amely **kódból** módosítja az objektumot: **Egy évvel idősebb** (`SzuletesiEv--`) és **Alaphelyzet**. A kártyának és az űrlapnak is frissülnie kell.
5. A közös megjelenést (címkék, szövegdobozok margója, gombok) **stílusok** adják a `Window.Resources`-ban.

## Tippek

- A `DataContext = szemely;` a konstruktorban.
- A kor kiírásához `StringFormat`: `Text="{Binding Kor, StringFormat={}{0} éves}"`. Két érték egy szövegben a `MultiBinding`-gal adható meg, de egyszerűbb egy `Alcim` számított tulajdonság.
- Szövegként megadott színnév közvetlenül köthető a `Background`-hoz, a WPF átalakítja ecsetté.
- A `ComboBox`-nál: `SelectedValuePath="Content"` és `SelectedValue="{Binding Szin}"`, az elemek `ComboBoxItem`-ek.

Megoldás: [../../megoldasok/F01-profilkartya/](../../megoldasok/F01-profilkartya/)
