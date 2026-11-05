# Megoldás – 14-M03-L02-F02 · Könyvtárak: kulcsok és kapcsolatok utólag

[kulcsok-es-lekerdezesek.sql](kulcsok-es-lekerdezesek.sql) – a feladat kiinduló szkriptje után futtatandó.

```
megye (id PK)  1 ──── N  telepules (irsz PK, megye_id FK)  1 ──── N  konyvtar (id PK, irsz FK)
```

- A `telepules.irsz` **természetes kulcs**: a valóságban is azonosít, ezért nem kell mellé `id`.
  (Az egész országra ez már nem igaz: egy irányítószámhoz több kisebb település is tartozhat – ott mesterséges kulcs kellene.)
- Az „árva” sor (id = 52, irányítószám `7099`) miatt a második idegen kulcs először nem jön létre. Ezért érdemes
  a kapcsolatokat **már a tábla létrehozásakor** megadni: akkor hibás adat be sem kerülhet.
- Eredmény: Nógrád 29, Tolna 23 könyvtár; a legtöbb Szekszárdon (8) és Salgótarjánban (7) van.
