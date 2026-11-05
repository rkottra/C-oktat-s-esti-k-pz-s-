# 14-M02-L01-F01 · Pizzarendelő űrlap

**Nehézség:** ★☆☆ **Becsült idő:** 45 perc

## Feladat

Készíts WPF-alkalmazást `Pizzarendeles` néven (.NET 8), amellyel pizzát lehet rendelni.

1. Az ablak egy kétoszlopos `Grid`: bal oldalon az űrlap, jobb oldalon a leadott rendelések listája.
2. Az űrlap (`StackPanel`) elemei:
   - `ComboBox` a pizzákkal: Margherita (2490 Ft), Sonkás (2890 Ft), Hawaii (3090 Ft), Négysajtos (3290 Ft); alapból az első legyen kiválasztva;
   - méret két `RadioButton`-nal egy `GroupBox`-ban: 32 cm (alapár), 45 cm (+1200 Ft);
   - extra feltétek `CheckBox`-okkal: extra sajt (+400 Ft), kukorica (+300 Ft), jalapeño (+300 Ft);
   - darabszám `TextBox`-ban (alapérték: 1), mellette `CheckBox`: Kiszállítás (+590 Ft, rendelésenként egyszer);
   - két gomb: **Ár számítása** és **Rendelés**, alattuk egy nagyobb betűs `Label` a fizetendő összeggel.
3. **Ár számítása**: (pizza ára + méretfelár + feltétek) × darab + kiszállítás. Ha a darabszám nem 1 és 10 közötti egész, figyelmeztető `MessageBox` jelenjen meg.
4. **Rendelés**: Igen/Nem kérdés után a rendelés egy sorban kerüljön a jobb oldali `ListBox`-ba.
5. A lista alatt egy **Kijelölt rendelés törlése** gomb legyen.

## Minta

```
Pizza: [Hawaii (3090 Ft)     v]
Méret: ( ) 32 cm  (•) 45 cm
[x] extra sajt  [ ] kukorica  [ ] jalapeño
Darab: [2]   [x] Kiszállítás
[Ár számítása] [Rendelés]
Fizetendő: 9970 Ft                    → (3090 + 1200 + 400) × 2 + 590
```

Lista: `2 db Hawaii, 45 cm – 9970 Ft`

## Tippek

- Az árat tárold a `ComboBoxItem` `Tag` tulajdonságában: `<ComboBoxItem Content="Hawaii (3090 Ft)" Tag="3090"/>`,
  kódból: `int.Parse(((ComboBoxItem)PizzaValaszto.SelectedItem).Tag.ToString()!)`.
- Az `IsChecked` értéke `bool?` – így vizsgáld: `if (SajtCheck.IsChecked == true)`.
- A két gomb ugyanazt számolja: írj rá egy közös metódust (pl. `int Vegosszeg()`).

Megoldás: [../../megoldasok/F01-pizzarendeles/](../../megoldasok/F01-pizzarendeles/)
