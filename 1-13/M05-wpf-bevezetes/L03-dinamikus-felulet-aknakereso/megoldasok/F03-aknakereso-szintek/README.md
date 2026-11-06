# Megoldás – 13-M05-L03-F03 · Aknakereső: szintek, gyors felfedés, rekordok

- `MainWindow.xaml`: szintválasztó `ComboBox`, **Új játék** gomb, rekordkijelző; alatta aknaszámláló, óra és állapotsor; középen az üres `UniformGrid`.
- `MainWindow.xaml.cs`:
  - **`Szint` rekord** és a három szint egy tömbben; a `ToString()` adja a `ComboBox` feliratát;
  - **`UjJatek()`:** az aktuális szint méretével hozza létre a modell tömbjeit és a gombokat (`Children.Clear()` után); a betűméret `200 / n`;
  - **`Szomszedok(sor, oszlop)`:** `yield return`-nel adja a táblán belüli szomszédokat. Ezt használja a számok kiszámolása, a gyors felfedés és az elárasztás;
  - **`Lerak`:** az első kattintás 3×3-as környékét kihagyja;
  - **`Felfed`:** elárasztás `Queue<(int, int)>`-vel, rekurzió nélkül;
  - **`GyorsFelfedes`:** a zászlók megszámolása után a többi szomszéd felfedése;
  - **`jel` tömb:** 0 = semmi, 1 = zászló, 2 = kérdőjel; a jobb klikk `(jel + 1) % 3`-mal léptet;
  - **rekordok:** `Dictionary<string, int>`, betöltés indításkor, mentés új rekordnál.

Megjegyzések:

- **Sor vagy rekurzió?** A 16×16-os táblán egy nagy üres terület több száz mezős elárasztást jelent. Rekurzióval ez is működne, de egy sokkal nagyobb táblán a hívási verem elfogyhat. A várakozó sorral a „még felfedendő” mezők egy közönséges adatszerkezetben vannak, ezért a méret nem korlát.
- A sorba ugyanaz a mező többször is bekerülhet, ha két 0-s szomszédja is van. Ezért a kivételkor újra ellenőrizzük, hogy fel van-e már fedve.
- Ha a 8×8-as táblán 9 mezőt (az első kattintás környékét) kihagyunk, még 55 hely marad a 9 aknának. Kis táblán, sok aknával ez a feltétel nem teljesülne, és a sorsoló ciklus soha nem érne véget. Saját szintnél erre figyelni kell.
- A `rekordok.txt` a futtatható állomány mellé (`bin/Debug/…`) kerül. Projekt újrafordítása nem törli, a `bin` mappa törlése igen.

Futtatás: F5 (Visual Studio).
