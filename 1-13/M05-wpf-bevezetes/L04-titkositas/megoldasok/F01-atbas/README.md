# Megoldás – 13-M05-L04-F01 · Atbas és ROT13

- `Rejtjelek.cs`: statikus osztály. A `Nagybetu`, `Kisbetu` és `Betu` segédmetódusok tartományt vizsgálnak, így az ékezetes betűk változatlanul maradnak. Az `Atbas` és a `Rot13` `StringBuilder`-rel építi a kimenetet.
- `MainWindow.xaml`: bemenet, vezérlősáv (rádiógombok, Alkalmaz, csere), kimenet, állapotsor és a magyarázat.
- `MainWindow.xaml.cs`: a gombok csak a `Rejtjelek` metódusait hívják; az `Allapot()` megszámolja a betűket és az egyéb karaktereket.

Megjegyzések:

- A `TextChanged` az XAML-ben megadott kezdőszöveg miatt már az `InitializeComponent()` közben lefut, amikor az `AllapotLabel` még nem létezik. Az `if (IsLoaded)` feltétel ezt kerüli el, a konstruktor végén pedig egyszer kézzel hívjuk az `Allapot()`-ot.
- Ellenőrzés: `Hello, World!` → Atbas: `Svool, Dliow!`, ROT13: `Uryyb, Jbeyq!`.

Futtatás: F5 (Visual Studio).
