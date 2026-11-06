# Megoldás – 13-M05-L05-F02 · Filmkatalógus

- `Film.cs`: `INotifyPropertyChanged`; az `Ertekeles` setter 1 és 5 közé szorítja az értéket, és a `Csillagok`-ról is értesít.
- `MainWindow.xaml`: `DockPanel` elrendezés. Alul a gombok és az összesítő, jobbra a `SelectedItem`-hez kötött szerkesztőpanel, a maradék helyen a lista. A lista `DataTemplate`-je `DataTrigger`-rel szürkíti a meg nem nézett filmeket; a `Run` elemekkel több kötött érték is egy sorba fűzhető. Az erőforrások és az implicit stílusok a `Window.Resources`-ban vannak.
- `MainWindow.xaml.cs`: az `ObservableCollection<Film>` mintaadatokkal; feliratkozás a gyűjtemény és az egyes filmek változására; az `Osszesit()` (megszámlálás, átlag); új film, törlés megerősítéssel és rendezés.

Megjegyzések:

- **Két szintű figyelés:** a `CollectionChanged` hozzáadáskor és törléskor jelez, a filmek `PropertyChanged`-je az adataik változásakor. Az összesítőhöz mindkettő kell. Az új filmekre fel kell iratkozni, a törölt filmekről le.
- **Rendezésnél** a gyűjtemény újratöltése alatt ideiglenesen leiratkozunk a `CollectionChanged`-ről. Így nem fut le 9-szer feleslegesen az összesítés és a fel- és leiratkozás.
- A `Run` elemeknél `Mode=OneWay` kell, mert a `Run.Text` alapból kétirányú kötést próbálna, ami egy csak olvasható megjelenítésnél hibát okozhat.
- A kötött `TextBox`-ba írt érvénytelen év (például `19x9`) nem kerül át az objektumba, és a mező piros keretet kap.

Futtatás: F5 (Visual Studio).
