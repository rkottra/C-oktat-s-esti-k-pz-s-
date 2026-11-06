# Megoldás – 13-M04-L03-F02 · Kölcsönzések és késedelmi díjak

- `Kolcsonzes.cs`: a dátumokat `ParseExact`-tal olvassa. A visszahozás **nullázható** `DateTime?`, amely üres mezőnél `null`. A határidő, a kint-lét, a késés és a díj számított tulajdonság, illetve metódus. A szabályok (21 nap, 50 Ft, 1500 Ft) `const` állandók.
- `Program.cs`: a rögzített „mai nap” után a nyolc kérdés tételekkel: megszámlálás és kiválogatás, feltételes összegzés, átlag, `DayOfWeek` és hónap szerint indexelt számlálótömbök, maximumkiválasztás, végül fájlba írás.

Megjegyzések:

- A `Visszahozas ?? ma` kifejezés a **null-egyesítő operátor**: ha a bal oldal `null`, a jobb oldalt veszi. Így a késés egyetlen képlettel számolható visszahozott és kint lévő könyvre is.
- A `DayOfWeek` enum egésszé alakítva (`(int)`) közvetlenül tömbindex lehet. Vasárnap a 0, ezért a magyar sorrendű kiíráshoz egy `{ 1, 2, 3, 4, 5, 6, 0 }` sorrendtömböt használunk. A maximumkiválasztás is ebben a sorrendben fut, így egyenlőségnél a hét korábbi napja nyer: a szerda a szombat előtt.
- A hónaponkénti tömb 13 elemű, hogy az index maga a hónap száma lehessen (1–12).
- A „mai nap” rögzítése (`new DateTime(2026, 5, 15)`) teszi az eredményt ellenőrizhetővé. Éles programban itt `DateTime.Today` állna.

Futtatás: F5 (Visual Studio).
