# Megoldás – 13-M05-L05-F01 · Profilkártya

- `Szemely.cs`: `INotifyPropertyChanged`-et megvalósító osztály. A setterek a saját tulajdonságukon túl a belőlük számolt tulajdonságokról is értesítenek: a `Nev` a `Monogram`-ról, a `SzuletesiEv` a `Kor`-ról és az `Alcim`-ről.
- `MainWindow.xaml`: bal oldalon az űrlap kétirányú kötésekkel, jobb oldalon a kártya csak egyirányú kötésekkel. A betűméret `ElementName` kötéssel jön a csúszkától; a `TextElement.FontSize` a kártya minden szövegére öröklődik. A közös megjelenést implicit stílusok adják.
- `MainWindow.xaml.cs`: csak a `DataContext` beállítása és a két gomb. Ezek az objektumot módosítják, nem a vezérlőket.

Megjegyzések:

- Ha az „Egy évvel idősebb” gomb után nem változik a kor a kártyán, a `SzuletesiEv` setteréből hiányzik a `Valtozott(nameof(Kor))`.
- A szövegdobozba írt, számmá nem alakítható születési év (például `20a8`) nem kerül át az objektumba. A WPF piros kerettel jelzi az átalakítási hibát; a hibakezelést (validáció) a 2/14. évfolyamon tanuljuk.
- A `DataContext` beállítása az `Alaphelyzet()` után történik, de fordított sorrendben is működne, hiszen a kötések az értesítések alapján frissülnek.

Futtatás: F5 (Visual Studio).
