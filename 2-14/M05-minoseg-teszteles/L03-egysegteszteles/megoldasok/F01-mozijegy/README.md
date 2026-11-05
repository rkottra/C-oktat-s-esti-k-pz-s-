# Megoldás – 14-M05-L03-F01 · Mozijegy

- `MozijegyTesztek/` – MSTest-projekt (ProjectReference a `Mozijegy`-re), 15 teszteset.
- `Mozijegy/` – a **javított** osztálykönyvtár.

## A tesztekkel megtalált hibák

| Teszt | Elvárt | Kapott (kiinduló kód) | Ok | Javítás |
|---|---|---|---|---|
| `Ar_EletkorHatarai (14, 2800)` | 2800 | 1400 | `eletkor <= 14` | `eletkor < 14` |
| `Ar_EletkorHatarai (65, 1960)` | 1960 | 2800 | `eletkor > 65` | `eletkor >= 65` |

Mindkettő tipikus „eggyel elcsúszott” (off-by-one) hiba – csak a határértékek tesztelése találja meg, a 10 és 70 éves tesztek zöldek voltak.

Futtatás parancssorból: `dotnet test MozijegyTesztek`
