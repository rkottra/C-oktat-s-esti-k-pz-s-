# Megoldás – 14-M03-L02-F01 · Zenetár normalizálása

[zenetar.sql](zenetar.sql) – az elemzés (redundancia, anomáliák, normálformák) a fájl elején, megjegyzésben.

```
mufaj (id PK, nev UNIQUE)      eloado (id PK, nev, orszag)
        1                            1
        └──── N  album (id PK, cim, ev, eloado_id FK, mufaj_id FK)  N ────┘
                   1
                   └──── N  dal (id PK, cim, album_id FK)
```

Futtatás: phpMyAdmin → Importálás → `zenetar.sql`. A végén lévő két lekérdezés eredménye az SQL fülön látszik.

Megjegyzés: a `dalok_szama` lekérdezésben `LEFT JOIN` szerepel, így a dal nélküli album is megjelenne 0-val.
