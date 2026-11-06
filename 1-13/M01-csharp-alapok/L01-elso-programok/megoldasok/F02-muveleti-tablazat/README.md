# Megoldás – 13-M01-L01-F02 · Műveleti táblázat

Projekt: `MuveletiTablazat.csproj`. Futtatás: F5.

Figyeld meg a `b != 0 && a % b == 0` kifejezést: a `&&` rövidzáras, ezért nullával osztás esetén a jobb oldal ki sem értékelődik, így nem keletkezik `DivideByZeroException`.
