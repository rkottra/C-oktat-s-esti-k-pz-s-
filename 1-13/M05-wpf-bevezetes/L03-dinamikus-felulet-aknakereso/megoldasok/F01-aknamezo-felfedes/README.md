# Megoldás – 13-M05-L03-F01 · Aknamező felfedése

- `MainWindow.xaml`: állapotsor, **Új játék** gomb és egy üres `UniformGrid`.
- `MainWindow.xaml.cs`:
  - **modell:** `bool[,] akna`, `int[,] szam`, `bool[,] felfedve`, a felfedett mezők száma és a `vege` jelző;
  - **nézet:** `Button[,] gombok`, a gombok egyszer jönnek létre a konstruktorban; az `UjJatek` csak a modellt sorsolja újra, és mindent újrarajzol;
  - **`Rajzol(s, o)`:** egy cella megjelenése a modellből (fedett / akna / szám);
  - **`Cella_Click`:** aknánál a vesztés, különben felfedés és a nyerés vizsgálata.

Megjegyzések:

- A szomszédszámok **egyszer**, a sorsolás után készülnek el. Kattintáskor már csak ki kell olvasni őket.
- A játék vége után a `vege` jelző tiltja a kattintást. A gombokat nem tiltjuk le (`IsEnabled = false`), mert a letiltott gomb kiszürkíti a színes számokat.
- Ebben a változatban az első kattintás is lehet akna, és a 0-s mezők nem nyitják ki a környéküket. Ezt az F02 pótolja.

Futtatás: F5 (Visual Studio).
