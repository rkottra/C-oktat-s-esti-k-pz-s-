# Megoldás – 13-M05-L05-F03 · Költségvetés-napló

- `Tetel.cs`: `INotifyPropertyChanged`. Az `Osszeg` setter a `Kiadas`-ról is értesít, mert ettől függ a lista színezése.
- `MainWindow.xaml`:
  - **összesítő:** `Run` elemek, amelyek az ablak tulajdonságaihoz kötődnek, `StringFormat`-tal, az egyenlegen `DataTrigger`-rel;
  - **lista:** `DataTemplate`, a színt a `Kiadas`-ra figyelő `DataTrigger` adja;
  - **jobb oldal:** az új tétel űrlapja (`DatePicker`, szerkeszthető `ComboBox`) és a kijelölt tétel szerkesztőpanelje `SelectedItem` kötéssel.
- `MainWindow.xaml.cs`:
  - az ablak **maga is `INotifyPropertyChanged`**, a számított `Bevetel`, `Kiadasok`, `Egyenleg`, `Negativ` tulajdonságokkal;
  - feliratkozás a gyűjtemény és a tételek változására, mindkettő után az `OsszesitoValtozott()` értesít;
  - a szűrés a gyűjtemény **nézetén** történik (`ICollectionView.Filter`), és a szűrők változásakor `Refresh()` fut;
  - a kategóriák az adatokból jönnek, és új tételnél vagy törlésnél frissülnek.

Megjegyzések:

- **Szűrés és összesítés:** a szűrő csak azt befolyásolja, mi **látszik** a listában, az összesítő mindig a teljes hónapból számol. Egy költségvetésnél ez a helyes: az egyenleg nem változhat attól, hogy épp csak az élelmiszer-kiadásokat nézzük. Ha kategóriánkénti összeg is kellene, azt egy külön, kiemelt „szűrt összeg” mezőben lenne érdemes mutatni.
- **Az adat és a nézet szétválasztása:** a `Tetelek` gyűjtemény az adat, a `CollectionViewSource` által adott nézet a megjelenítés (szűrés, rendezés). Ez a 2/14. évfolyam MVVM-es adatkötésének alapja.
- A mintaadatokkal: bevétel 412 000 Ft, kiadás 317 340 Ft, egyenleg 94 660 Ft.

Futtatás: F5 (Visual Studio).
