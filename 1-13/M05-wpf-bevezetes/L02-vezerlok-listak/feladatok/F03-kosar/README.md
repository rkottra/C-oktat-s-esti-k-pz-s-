# 13-M05-L02-F03 · Webshop-kosár

**Nehézség:** ★★★ **Becsült idő:** 90 perc

## Feladat

Készíts `Kosar` nevű WPF-alkalmazást, amely egy kis webshop kosarát kezeli.

1. **Adatok:**
   - `Termek` osztály: `Nev`, `Ar` (Ft), `Kategoria`; a `ToString()` a nevet és az árat mutatja. Legalább 10 termék a programban.
   - `Tetel` osztály: egy `Termek` és egy `Mennyiseg`; számított tulajdonság a `Reszosszeg`; a `ToString()` például `3 × Füzet A4 (390 Ft) = 1 170 Ft`.
2. **Felület (három oszlop):**
   - **bal:** kategóriaválasztó `ComboBox` (`(mind)` + a kategóriák), alatta a termékek `ListBox`-a;
   - **közép:** mennyiség (`TextBox`, 1–99), **Kosárba →** és **← Kivesz** gomb;
   - **jobb:** a kosár `ListBox`-a, alatta az összesítő rész.
3. **Kosárba:** ha a termék már a kosárban van, csak a mennyisége nő (legfeljebb 99-ig), különben új tétel kerül a kosárba. Érvénytelen mennyiségnél a `TextBox` háttere legyen piros, és a gomb ne legyen aktív.
4. **Kivesz:** a kijelölt tételt eltávolítja a kosárból.
5. **Összesítő:**
   - `CheckBox`: „Kuponkód: DIAK10” – bejelölve 10% kedvezmény jár a termékek árából;
   - `RadioButton`-csoport a szállításhoz: személyes átvétel (0 Ft), csomagautomata (990 Ft), futár (1 490 Ft). **20 000 Ft** feletti (kedvezmény utáni) termékérték felett a szállítás ingyenes;
   - kiírva: a termékek összege, a kedvezmény, a szállítási díj és a **fizetendő végösszeg**. Ezek minden változáskor azonnal frissüljenek.
6. **Megrendelés gomb** (csak nem üres kosárnál aktív): egy `MessageBox` összefoglalja a rendelést. Megerősítés után a program a `SaveFileDialog`-gal egy szöveges **nyugtát** ment, és kiüríti a kosarat.

## Tippek

- A kosarat a `KosarLista.Items` gyűjteményben `Tetel` objektumként tárold. A meglévő tétel kereséséhez járd be: `foreach (object o in KosarLista.Items) if (o is Tetel t && t.Termek == termek) …`
- Ha egy tétel mennyiségét módosítod, a `ListBox` nem tudja, hogy át kell rajzolnia a sort. Hívd meg a `KosarLista.Items.Refresh()`-t.
- A háttérszín beállítása: `MennyisegTextBox.Background = Brushes.MistyRose;` (`using System.Windows.Media;`).
- A pénzösszegeket egyetlen `Frissit()` metódus számolja: összegzés a tételeken, majd a kedvezmény és a szállítás szabályai.

Megoldás: [../../megoldasok/F03-kosar/](../../megoldasok/F03-kosar/)
