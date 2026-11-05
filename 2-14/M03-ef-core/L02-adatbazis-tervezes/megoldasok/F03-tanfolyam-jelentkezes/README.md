# Megoldás – 14-M03-L02-F03 · Tanfolyami jelentkezések (N:M)

[tanfolyam.sql](tanfolyam.sql)

```
oktato (id PK, nev, szakterulet)
   1
   └── N  tanfolyam (id PK, nev, kezdes, ar, max_letszam, oktato_id FK)
              1
              └── N  jelentkezes (resztvevo_id PK+FK, tanfolyam_id PK+FK, datum, fizetve)
                         N
                         └── 1  resztvevo (id PK, nev, email UNIQUE)
```

Várt eredmények:

| Lekérdezés | Eredmény |
|---|---|
| betelt tanfolyam | C# alapok (4/4) |
| legalább két tanfolyam | Arany Ábel (3), Bodor Bella (2), Csiszár Csongor (2) |
| ki nem fizetett díjak | 245 000 Ft |
| oktatónként | Kódoló Kornél 7, Lekérdező Lili 3, Táblás Tivadar 2 |

Megjegyzés: ha a tanfolyam árát a `jelentkezes` táblába is beírnánk, megsértenénk a 2NF-et (az ár csak a
kulcs egyik részétől, a tanfolyamtól függ). Kivétel, ha a *ténylegesen fizetett* (pl. kedvezményes) árat
kell tárolni – az már a jelentkezés saját adata.
