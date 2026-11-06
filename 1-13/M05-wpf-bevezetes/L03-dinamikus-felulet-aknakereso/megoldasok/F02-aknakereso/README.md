# Megoldás – 13-M05-L03-F02 · Aknakereső: elárasztás, zászló, időmérés

- `MainWindow.xaml`: aknaszámláló, óra, állapotsor és **Új játék** gomb felül, a használati útmutató alul, középen az üres `UniformGrid`.
- `MainWindow.xaml.cs`:
  - **modell:** az F01-hez képest új a `zaszlo` tömb, az `aknakLerakva` jelző, a zászlók száma és a másodperc;
  - **`Lerak(kiSor, kiOszlop)`:** az első kattintáskor fut le, a kattintott mezőt kihagyja, utána kiszámolja a szomszédszámokat;
  - **`Felfed(s, o)`:** rekurzív elárasztás; a táblán kívüli, a már felfedett és a zászlós mezőnél azonnal visszatér;
  - **`Cella_JobbKlikk`:** a `MouseRightButtonUp` kezelője, a zászló ki-be kapcsolása;
  - **`Vesztett` / `Nyert`:** az óra leállítása és a végállapot kirajzolása.

Megjegyzések:

- **A rekurzió leállása:** minden mező legfeljebb egyszer kerül `felfedve` állapotba, és felfedett mezőnél a hívás azonnal visszatér. Így a hívások száma legfeljebb a mezők számával arányos. 10×10-es táblán ez bőven belefér a verembe.
- Az aknaszámláló lehet negatív is, ha a játékos több zászlót tett le, mint ahány akna van. Ez szándékos: jelzi, hogy valahol téves a jelölés.
- Az óra csak az aknák lerakásakor indul, így az **Új játék** után a gondolkodási idő nem számít bele.

Futtatás: F5 (Visual Studio).
