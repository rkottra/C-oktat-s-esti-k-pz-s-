# 13-M05-L03-F01 · Aknamező felfedése

**Nehézség:** ★☆☆ **Becsült idő:** 45 perc

## Feladat

Készíts `AknamezoFelfedes` nevű WPF-alkalmazást, az aknakereső egyszerűsített első változatát.

1. **Játéktér:** 8×8 gomb egy `UniformGrid`-ben, **kódból létrehozva**. Az XAML-ben csak az üres rács, egy állapotsor (`Label`) és egy **Új játék** gomb szerepeljen.
2. **Modell:** a táblán 9 akna rejtőzik, véletlen helyen. Minden mezőhöz előre számold ki, hány akna van a 8 szomszédjában.
3. **Kezdetben** minden mező fedett: üres felirat, egyszínű (például kékes) háttér.
4. **Kattintás** egy fedett mezőre:
   - ha **nincs** rajta akna, felfedődik: világos háttér, és a szomszédos aknák száma (a 0 helyett üres mező). A számok színe számonként más legyen;
   - ha **akna** van rajta, a játék véget ér: minden akna előkerül (`✱`), a rálépett mező piros, az állapotsorban „Akna! Vesztettél.” áll.
5. **Állapot:** játék közben az állapotsor mutatja, hány biztonságos mező van már felfedve: `Felfedve: 12 / 55`.
6. **Nyerés:** ha mind az 55 biztonságos mező fel van fedve, az aknák zöld háttérrel látszanak, és az állapotsor gratulál.
7. A játék vége után a táblára kattintva ne történjen semmi. Az **Új játék** gomb új aknamezőt sorsol, és mindent alaphelyzetbe állít.

## Tippek

- Modell: `bool[,] akna`, `int[,] szam`, `bool[,] felfedve`, egy `int felfedettDb` számláló és egy `bool vege` jelző.
- Az aknák lerakása: sorsolj sort és oszlopot, és csak akkor rakd le, ha ott még nincs akna. Addig ismételd, amíg 9 nem lesz.
- A szomszédok számolásához lásd a lecke elméletét és a [peldak/Aknaterkep](../../peldak/Aknaterkep/) példát.
- Írj egy `Rajzol(sor, oszlop)` metódust, amely a modellből állítja be egy gomb feliratát és színét. A kattintás csak a modellt módosítja, utána meghívja a `Rajzol`-t.
- Saját színhez: `new SolidColorBrush(Color.FromRgb(0x5B, 0x7D, 0xB1))`.

Megoldás: [../../megoldasok/F01-aknamezo-felfedes/](../../megoldasok/F01-aknamezo-felfedes/)
