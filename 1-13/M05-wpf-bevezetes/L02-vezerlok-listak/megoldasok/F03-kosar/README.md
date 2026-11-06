# Megoldás – 13-M05-L02-F03 · Webshop-kosár

- `Termek.cs`: a `Termek` és a `Tetel` osztály. A tétel **hivatkozik** a termékre, és a mennyiséget tárolja; a `Reszosszeg` számított tulajdonság.
- `MainWindow.xaml`: háromoszlopos `Grid`. Bal oldalt a kategória és a termékek, középen a mennyiség és a mozgató gombok, jobb oldalt a kosár `DockPanel`-ben, alul az összesítővel.
- `MainWindow.xaml.cs`: a kategóriák az adatokból jönnek, az események bekötése kódból történik. A `KosarbaGomb_Click` keres, és a meglévő tételt növeli vagy újat vesz fel. A `Frissit()` a gombokat, a mennyiségmező színét és az összesítőt állítja, a `Szamol` pedig `out` paraméterekkel adja vissza a három összeget. A megrendelés `MessageBox`-szal és `SaveFileDialog`-gal megy.

Megjegyzések:

- A `KosarLista.Items.Refresh()` azért kell, mert a `ListBox` csak a gyűjtemény változásáról (hozzáadás, törlés) értesül, egy benne lévő objektum tulajdonságának változásáról nem. A 13-M05-L05 leckében erre az adatkötés (`INotifyPropertyChanged`, `ObservableCollection`) ad elegáns megoldást.
- A pénzösszegek számítása (`Szamol`) külön metódusban van. A kijelzés (`Frissit`) és a nyugta (`RendelesGomb_Click`) is ezt használja, így a kettő biztosan ugyanazt a végösszeget mutatja.
- Az ingyenes szállítás a **kedvezmény utáni** összegre vonatkozik, ahogy a feladat kéri.

Futtatás: F5 (Visual Studio).
