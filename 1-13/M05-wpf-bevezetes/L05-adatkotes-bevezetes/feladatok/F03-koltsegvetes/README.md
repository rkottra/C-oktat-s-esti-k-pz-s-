# 13-M05-L05-F03 · Költségvetés-napló

**Nehézség:** ★★★ **Becsült idő:** 120 perc

## Feladat

Készíts `Koltsegvetes` nevű WPF-alkalmazást, amely a havi bevételeket és kiadásokat tartja nyilván, élő összesítéssel. A cél, hogy az összesítő, a lista és a szűrés **adatkötéssel** működjön, kézi frissítés nélkül.

1. **`Tetel`** osztály `INotifyPropertyChanged`-del: `Megnevezes`, `Kategoria`, `Datum` (`DateTime`), `Osszeg` (`int`; pozitív = bevétel, negatív = kiadás). Számított tulajdonság a `Kiadas` (`bool`).
2. **Az ablak maga is `INotifyPropertyChanged`**:
   - `ObservableCollection<Tetel> Tetelek` legalább 10 mintatétellel;
   - számított tulajdonságok: `Bevetel`, `Kiadasok` (a negatív összegek abszolút értékének összege), `Egyenleg`, `Negativ` (`bool`);
   - ezekről értesíteni kell, ha a gyűjtemény vagy bármelyik tétel változik.
3. **Lista:** `DataTemplate`-tel; dátum (`yyyy.MM.dd.`), megnevezés, kategória, és jobbra igazítva az összeg. A kiadások pirosak, a bevételek zöldek (`DataTrigger` a `Kiadas`-ra).
4. **Szűrés:** egy `ComboBox` a kategóriákkal (`(mind)` + a tételekből kigyűjtött kategóriák) és egy `TextBox` a megnevezés részletére. A lista csak a feltételeknek megfelelő tételeket mutassa. Ehhez a gyűjtemény **nézetét** szűrd (`CollectionViewSource.GetDefaultView(Tetelek).Filter`), ne a gyűjteményt magát.
5. **Új tétel:** egy űrlap (megnevezés, kategória szerkeszthető `ComboBox`-szal, dátum `DatePicker`-rel, összeg, és egy „kiadás” `CheckBox`). Ellenőrzés: nem üres megnevezés, pozitív összeg. **Hozzáadás** után az űrlap kiürül.
6. **Törlés:** a kijelölt tétel törlése. **Szerkesztés:** a kijelölt tétel összege és megnevezése egy kis panelen, kétirányú kötéssel szerkeszthető, és az összesítő azonnal követi.
7. **Összesítő** az ablak alján, kötéssel: `Bevétel: 412 000 Ft · Kiadás: 317 340 Ft · Egyenleg: 94 660 Ft` (a megoldás mintaadataival). Negatív egyenlegnél az egyenleg piros (`DataTrigger` a `Negativ`-ra).

## Tippek

- Az ablak a saját `DataContext`-je: `DataContext = this;`. A `{Binding Egyenleg, StringFormat={}{0:N0} Ft}` így az ablak tulajdonságára mutat.
- Az összesítő értesítése egy metódusban: `Valtozott(nameof(Bevetel)); Valtozott(nameof(Kiadasok)); Valtozott(nameof(Egyenleg)); Valtozott(nameof(Negativ));`. Ezt hívd a `CollectionChanged`-ből és minden tétel `PropertyChanged`-jéből.
- Szűrés: `nezet.Filter = o => o is Tetel t && (kategoria == null || t.Kategoria == kategoria) && t.Megnevezes.Contains(resz, StringComparison.OrdinalIgnoreCase);`, majd minden szűrőváltozáskor `nezet.Refresh()`.
- A szűrés csak a **nézetre** hat: az összesítő továbbra is az összes tételből számol. Gondold végig, melyik a helyes egy költségvetésnél, és írd le a README-dben!

Megoldás: [../../megoldasok/F03-koltsegvetes/](../../megoldasok/F03-koltsegvetes/)
