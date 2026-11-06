# Megoldás – 13-M05-L02-F02 · Névsor kereséssel és szűréssel

- `Diak.cs`: az adatosztály; a `ToString()` határozza meg, mi látszik a `ListBox`-ban.
- `MainWindow.xaml`: bal oldalon a szűrők `StackPanel`-ben, jobb oldalon `DockPanel` (fejléc, lista, részletek keretben).
- `MainWindow.xaml.cs`: a teljes névsor egy `List<Diak>` mezőben van. A konstruktor az adatokból tölti fel az osztályválasztót, és beköti az eseményeket. A `Szur()` kiválogat, rendez, és újraépíti a listát; a `Reszletek()` a kijelölt diák adatait írja ki.

Megjegyzések:

- **Eseménykezelők kódból:** a `NevTextBox.TextChanged += (s, e) => Szur();` sor egy lambdát köt az eseményre. Azért kódból és az `InitializeComponent()` után kötjük be őket, mert az XAML-ben `IsChecked="True"`-val beállított rádiógomb már a felület felépítése közben kiváltaná a `Checked` eseményt. Akkor pedig a `Szur()` még nem létező vezérlőkre hivatkozna (`NullReferenceException`).
- **Adat és nézet szétválasztása:** a `diakok` lista az adat, a `TalalatLista.Items` csak a nézet. Ezért lehet a szűrést bármikor, veszteség nélkül újraszámolni. Ez a gondolat a 13-M05-L05 lecke adatkötésében folytatódik.
- A kijelölés megtartása referencia szerint működik (`talalatok.Contains(kijelolt)`), mert ugyanazok a `Diak` objektumok kerülnek újra a listába.

Futtatás: F5 (Visual Studio).
