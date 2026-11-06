# Megoldás – 13-M04-L04-F01 · Sportesemények

- `Merkozes.cs`: a dátum `DateTime`, a kezdés `TimeSpan`. Számított tulajdonságok: `Bevetel` (`long`), `Dontetlen`, `Hetvege`, `Eredmeny`. Két metódus segíti a csapatszintű kérdéseket: a `Resztvevo` és a `Pontok`. Ez utóbbi egy **tuple**-t (`(sajat, ellenfel)`) ad vissza, és ezzel a hazai/vendég esetet egyetlen helyen kezeli.
- `CsapatEredmeny.cs`: a tabella egy sora. A `Rogzit` metódus egy mérkőzés eredményét könyveli; a `Pont` és a `Golkulonbseg` számított tulajdonság.
- `Program.cs`: a tíz feladat sorban. A sportágak listáját az adatokból gyűjti ki (csoportosítás), a havi bevételhez hónap szerint indexelt `long[13]` tömböt használ. A tabellánál a `Csapat` segédmetódus „keres, és ha nincs, létrehoz”.

Megjegyzések:

- A bevétel nagy szám lehet (32 millió Ft), ezért `long`. A `(long)Nezok * Jegyar` szorzásnál az átalakítás **a szorzás előtt** kell, különben `int`-ben számolna, és túlcsordulhatna.
- A 7. feladatban kisbetűvel beírt csapatnevet (`medvék`) is elfogad, de a kiírásban a fájl szerinti, pontos alakot használja.
- A tabella két szempont szerint rendez. A 2. és 3. helyezett közti sorrendet itt már a pontszám eldönti; azonos pontszámnál a gólkülönbség döntene.

Futtatás: F5 (Visual Studio).
