# Megoldás – 13-M04-L04-F02 · Parkolóház díjai

- `Parkolas.cs`: a `Be` és a `Ki` egyetlen `DateTime`-ként olvasódik be (`"yyyy-MM-dd HH:mm"`). Az `Idotartam` `TimeSpan`, a `Dij` több soros számított tulajdonság (`get { … }` blokkal). Benne van a `BentVolt` vizsgálat és két statikus formátumellenőrző metódus is. A díjszabás `const` állandókban van.
- `Program.cs`: a nyolc feladat sorban. Különböző értékek kigyűjtése (autók, napok), összegzés és megszámlálás, maximumkiválasztás `TimeSpan`-on, formátum szerinti megszámlálás, bekért rendszám kiválogatása, napi csoportosítás fájlba, és az óránkénti telítettség egy beágyazott ciklusban, maximumkereséssel.

Megjegyzések:

- **A díj képlete** teljesen egész számokkal dolgozik: `(maradek + 59) / 60` a felfelé kerekített órák száma. A `Math.Ceiling` is jó, de akkor `double`-lel kell számolni, majd visszaalakítani.
- **A napi plafon** a teljes 24 órákra és a maradékra külön érvényes. Ezért a 26:15-ös parkolás 4000 + 1200 = 5200 Ft, nem 4000 Ft. A feladatszöveg pontos értelmezése vizsgán is sokat számít!
- **A rendszámellenőrzés** a hossz vizsgálatával kezdődik. Az `&&` rövidzáras: ha a hossz nem stimmel, a karakterek indexelése már le sem fut, így egy rövidebb szövegnél sem keletkezik `IndexOutOfRangeException`.
- **A telítettség** a „bent volt-e” feltételt (`Be <= t < Ki`) vizsgálja minden parkolásra, így az előző napról bent maradt autó is beleszámít.
- A `Date` tulajdonság a `DateTime` dátumrészét adja (0:00 időponttal); napi csoportosításra ez a legegyszerűbb.

Futtatás: F5 (Visual Studio).
