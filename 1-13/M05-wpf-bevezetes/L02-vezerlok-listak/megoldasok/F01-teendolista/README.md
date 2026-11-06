# Megoldás – 13-M05-L02-F01 · Teendőlista

- `MainWindow.xaml`: `DockPanel` elrendezés. A beviteli sor felül van dokkolva, az állapotsor és a gombsor alul, a `ListBox` kitölti a maradék helyet. A Hozzáad gomb `IsDefault="True"`, így az Enter is megnyomja.
- `MainWindow.xaml.cs`: egy-egy eseménykezelő gombonként, a közös `Mozgat(irany)` metódus a Fel és a Le gombhoz, a `Szerepel` eldöntés a duplikátumok kiszűrésére, és a `Frissit()`, amely a gombok engedélyezését és az állapotsort állítja be.

Megjegyzések:

- A „Kész” jelölésnél a lista elemét **a helyén cseréljük** (`Items[i] = uj`). A kijelölés ilyenkor elveszhet, ezért újra beállítjuk.
- A törlés után a következő elem kapja a kijelölést, így a Töröl gombot többször egymás után is lehet nyomni.
- Ha az összes állapotfüggő beállítás a `Frissit()`-ben van, egy új szabály (például „a Le gomb ne legyen aktív az utolsó elemnél”) egyetlen sor.

Futtatás: F5 (Visual Studio).
