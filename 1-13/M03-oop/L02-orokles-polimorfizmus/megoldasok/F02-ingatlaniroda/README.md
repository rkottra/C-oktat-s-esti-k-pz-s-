# Megoldás – 13-M03-L02-F02 · Ingatlaniroda

- `Ingatlan.cs`: ellenőrzött tulajdonságok, `virtual Iranyar()` és `virtual Tipus()`. A `ToString()` mindkettőt hívja, ezért egyik leszármazottban sem kell felülírni.
- `Lakas.cs`: a `base.Iranyar()`-ból indul, és lift nélküli, magas emeletnél 10%-ot levon.
- `Panellakas.cs`: **többszintű öröklődés** (`Ingatlan → Lakas → Panellakas`). A `base.Iranyar()` itt a `Lakas` változatát hívja, így a liftes szabály és a 15% kedvezmény is érvényesül.
- `CsaladiHaz.cs`: a telek és a garázs árával egészíti ki az ős számítását. Az állandók `private const` mezők.
- `Program.cs`: rendezés lambdával, kiválogatás keretösszegre, típusonkénti átlag, maximumkiválasztás `is CsaladiHaz h` mintával.

Megjegyzések:

- A típusonkénti statisztikánál az `is Lakas` vizsgálat a panellakásokat is lakásnak számolná, hiszen a panellakás *egy fajta* lakás. Ezért a megoldás a felülírt `Tipus()` szövegét hasonlítja. Másik lehetőség az `i.GetType() == typeof(Lakas)` vizsgálat.
- A szorzások `long` típusban történnek. Egy 140 m²-es, 1 100 000 Ft/m²-es ingatlan ára már nem férne el egy `int`-ben.

Futtatás: F5 (Visual Studio) vagy `dotnet run`.
