# 14-M02-L02-F02 · Termékkatalógus ListBoxszal

**Nehézség:** ★★☆ **Becsült idő:** 50 perc

## Feladat

Készíts WPF-alkalmazást `Termekkatalogus` néven egy webáruház termékeinek kezelésére.

1. Készíts `Termek` osztályt: `Nev` (szöveg), `Kategoria` (szöveg), `Ar` (egész, Ft), `Keszlet` (egész, db).
   Az osztály valósítsa meg az `INotifyPropertyChanged` felületet.
2. Az ablakban legyen egy publikus `ObservableCollection<Termek> Termekek` tulajdonság, a konstruktorban tölts bele legalább 5 terméket, és állítsd be: `DataContext = this;`.
3. Bal oldalon `ListBox` (`ItemsSource` kötéssel). Egy elem `DataTemplate`-tel jelenjen meg: **név** (félkövér), ár `Ft`-tal, készlet `db`-vel.
4. Jobb oldalon egy `GroupBox` „Kiválasztott termék” fejléccel, benne 4 `TextBox` a kiválasztott termék adataival.
   A név átírása **gépelés közben** frissítse a listát.
5. Gombok:
   - **Új termék** – „Új termék” nevű elemet vesz fel, és ki is jelöli;
   - **Törlés** – megerősítés után törli a kiválasztottat;
   - **10% akció** – a kiválasztott termék árát 10%-kal csökkenti (kerekítve). **Csak az objektumot módosítsd**, a felület magától frissüljön!
   - **Összesítés** – `MessageBox`: termékek száma, teljes készletérték (ár × készlet összege), a legdrágább termék neve.

## Minta

```
Vezeték nélküli egér   6990 Ft   25 db      ┌ Kiválasztott termék ──┐
Mechanikus billentyűzet 24990 Ft  8 db      │ Név:      [Webkamera] │
27" monitor            79990 Ft   5 db      │ Kategória:[periféria] │
...                                          │ Ár (Ft):  [14990]     │
                                             └───────────────────────┘
[Új termék] [Törlés] [10% akció] [Összesítés]
```

Összesítés (a megoldás mintaadataival): `Termékek száma: 5 / Teljes készletérték: 1 103 900 Ft / Legdrágább: 27" monitor`

## Tippek

- Ha nincs kiválasztott elem, a `TermekLista.SelectedItem as Termek` értéke `null` – ezt minden gombnál vizsgáld.
- Az összesítéshez LINQ: `Termekek.Sum(t => (long)t.Ar * t.Keszlet)`, `Termekek.OrderByDescending(t => t.Ar).First()`.
- Próbáld ki: mi történik, ha a `Termek` osztályból kiveszed az `INotifyPropertyChanged`-et, és megnyomod a 10% akció gombot?

Megoldás: [../../megoldasok/F02-termekkatalogus/](../../megoldasok/F02-termekkatalogus/)
