# 13-M05-L05-F02 · Filmkatalógus

**Nehézség:** ★★☆ **Becsült idő:** 75 perc

## Feladat

Készíts `Filmkatalogus` nevű WPF-alkalmazást, amely egy saját filmgyűjteményt kezel adatkötéssel.

1. **`Film`** osztály `INotifyPropertyChanged`-del: `Cim`, `Rendezo`, `Ev`, `Mufaj`, `Ertekeles` (1–5), `Megnezve` (`bool`). Számított tulajdonság a `Csillagok`, például `★★★★☆`.
2. A filmek egy `ObservableCollection<Film>`-ben legyenek, legalább 8 filmmel; a lista ehhez legyen kötve (`ItemsSource`).
3. **Lista (`DataTemplate`):** a cím félkövéren, alatta a rendező és az év, jobbra a csillagok. A még **meg nem nézett** filmek címe legyen dőlt és szürke (`DataTrigger`).
4. **Részletek panel** a kijelölt filmhez kötve (`SelectedItem`): a mezők szerkeszthetők, az értékelés egy `Slider` (1–5), a „megnézve” egy `CheckBox`. Minden módosítás azonnal látszik a listában is.
5. **Gombok:** **Új film** (felvesz egy alapértelmezett filmet, és kijelöli), **Törlés** (megerősítéssel), **Rendezés értékelés szerint** és **Rendezés cím szerint**.
6. Az ablak alján egy összesítő: `10 film, ebből 7 megnézve, átlagos értékelés: 4,2`. Ezt egy metódus számolja, és minden változáskor frissíteni kell.
7. A közös megjelenést stílusok és erőforrások adják.

## Tippek

- Az `ObservableCollection`-nek nincs `Sort` metódusa. Rendezéshez készíts egy rendezett másolatot (`List<Film> r = new List<Film>(filmek); r.Sort(…)`), majd a gyűjteményt ürítsd ki, és töltsd fel újra. A kijelölést jegyezd meg előtte!
- Az összesítő frissítéséhez figyeld a gyűjtemény (`filmek.CollectionChanged`) és az egyes filmek (`film.PropertyChanged`) változását. Új filmnél az utóbbira is fel kell iratkozni.
- A csúszka `Value` értéke `double`; a kötés automatikusan átalakítja `int`-re, ha `IsSnapToTickEnabled="True"` és `TickFrequency="1"`.

Megoldás: [../../megoldasok/F02-filmkatalogus/](../../megoldasok/F02-filmkatalogus/)
