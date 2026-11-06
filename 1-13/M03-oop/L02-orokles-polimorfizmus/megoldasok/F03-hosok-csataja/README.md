# Megoldás – 13-M03-L02-F03 · Hősök csatája

- `Hos.cs`: csak olvasható és `private set` tulajdonságok, `protected` megjegyzés mező, négy `virtual` metódus (`Sebzes`, `Serul`, `KorVege`, `Kaszt`), valamint a **nem virtuális** `Megtamad`.
- `Harcos.cs`, `Ijasz.cs`, `Magus.cs`: mindegyik csak azt írja felül, amiben különbözik, és a `base.Sebzes` / `base.Serul` hívással az ős működésére épít.
- `Program.cs`: két polimorf `List<Hos>` csapat, körök `while` ciklussal, eldöntés (`VanElo`), kiválogatás (élő ellenfelek), maximumkiválasztás (legtöbb sebzés).

Megjegyzések:

- A `Megtamad` a többalakúság kulcsa. Az ősben van, egyszer megírva, és nem tudja, kit támad meg és ki támad. Mégis a harcos kritikusat üt, a mágus varázsol, a harcos páncélja pedig elnyel, mert a benne hívott `Sebzes` és `Serul` virtuális.
- Az `OkozottSebzes` a **ténylegesen levont** életet számolja (`eletElotte - cel.Elet`), így a páncél és a 0 alatti sebzés nem torzítja a statisztikát.
- Az `Elet` `private set`-je miatt az életet csak a `Hos` osztály kódja, vagyis a `Serul` metódus írhatja. A `Harcos` is csak a `base.Serul` hívásával tudja csökkenteni, ezért a „0 alá nem mehet” szabály minden kasztra érvényes.
- A fix mag (`new Random(7)`) miatt a csata minden futáskor ugyanígy zajlik. Más maggal ki lehet próbálni, mennyire kiegyensúlyozottak a kasztok.
- Bővítési ötlet: egy `Pap : Hos`, amely támadás helyett a saját csapat legsebesültebb tagját gyógyítja. Ehhez a `Megtamad`-ot is `virtual`-lá kell tenni, vagy egy új `virtual void Cselekszik(...)` metódust bevezetni.

Futtatás: F5 (Visual Studio) vagy `dotnet run`.
