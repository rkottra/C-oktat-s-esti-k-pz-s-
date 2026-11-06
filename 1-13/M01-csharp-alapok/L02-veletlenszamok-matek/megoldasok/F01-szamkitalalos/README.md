# Megoldás – 13-M01-L02-F01 · Számkitalálós játék

Projekt: `Szamkitalalos.csproj`. Futtatás: F5.

Két egymásba ágyazott `do … while` ciklus: a belső a tippelést, a külső az új játékot kezeli. A bemenet ellenőrzése `int.TryParse`-szal történik, így rossz bemenetre sem áll le a program. Hibakereséshez a `new Random(1)` (rögzített mag) mindig ugyanazt a számot adja.
