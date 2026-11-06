# Megoldás – 13-M02-L03-F03 · Rendezések versenye

- Minden rendezés a saját **másolatán** fut (`Clone()`), különben a második algoritmus már rendezett tömböt kapna, és a mérés értelmetlen lenne.
- A rendező függvények egy **tuple** (`(long, long)`) visszatérési értékkel adják vissza a két számlálót. A `Sor` függvény a rendezést paraméterként kapja (`Func<int[], (long, long)>`): így a három algoritmus mérése egyetlen kódrészlettel megoldható. Ez haladó megoldás; kezdőknek elég, ha a mérést háromszor írják le.
- A beszúrásos rendezés a csere helyett **léptet**: egy csere 3 mozgatás, egy léptetés csak 1. Ezért véletlen adatokon is körülbelül kétszer gyorsabb a buborékrendezésnél.
- A minimumkiválasztásos rendezés összehasonlításainak száma mindig pontosan n·(n−1)/2, például n = 1000-re 499 500, a bemenettől függetlenül. Mozgatásból viszont ennél van a legkevesebb (legfeljebb 3·(n−1)).
- Az első futáskor a mért idő a JIT-fordítás miatt kissé nagyobb lehet, ezért a kis tömbös bemutató egyben „bemelegítés” is.
- A tanulók a válaszokat mérés alapján fogalmazzák meg. A tanórán érdemes közösen felírni a táblára néhány gép számait.
