# Megoldás – 14-M05-L02-F01 · Parkolóház – refaktorálás

| Probléma a kiinduló kódban | Javítás |
|---|---|
| `P`, `D`, `p`, `m`, `tv`, `o`, `d`, `x` nevek | `ParkolasiDijSzamito`, `Dij`, `percek`, `jarmu`, `torzsvendeg` |
| varázsszámok (30, 1440, 200, 400, 2000, 4000, 20) | elnevezett konstansok; a napi maximum = 10 × óradíj, így nem kell két helyen átírni |
| a motoros és az autós ág szinte azonos (ismétlődés) | egy számolás, a különbség (`Oradij`) külön metódusban |
| `bool m` jelző paraméter | `JarmuTipus` felsorolás |
| `-1` mint hibakód | `ArgumentOutOfRangeException` |
| három párhuzamos tömb a `Program.cs`-ben | egy lista névvel ellátott elemekkel (tuple) |
| négyszer ismétlődő kiírás | `Leiras` és `DijSzovegesen` helyi függvény |

A kimenet karakterre egyezik a kiinduló programéval (ellenőrizve).

**Megjegyzés:** a kiinduló kód negatív percre `0`-t ad. Ez valószínűleg hiba, de a refaktorálás **nem** változtat a viselkedésen – a javítás külön lépés legyen
(és jó alkalom egy egységtesztre, lásd 14-M05-L03).
