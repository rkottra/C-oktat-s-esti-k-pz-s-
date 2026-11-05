# Megoldás – 14-M01-L02-F02 · Időjárás-mérések hibás sorokkal

- `Meres.cs`: `Feldolgoz` – hibás sorra `null`-t ad (`TryParse`, `TryParseExact`, `NumberStyles.Float`); `FajlbolBetolt` – a hibás sorokat sorszámmal gyűjti.
- `Program.cs`: csoportosítás városra és dátumra, `MaxBy`, `Any`, aggregálás; `varosok.txt` kiírása tizedesponttal.

A fájl 4 szándékosan hibás sort tartalmaz (8., 25., 42., 59. sor) – ezeket kell a programnak kiszűrnie.
