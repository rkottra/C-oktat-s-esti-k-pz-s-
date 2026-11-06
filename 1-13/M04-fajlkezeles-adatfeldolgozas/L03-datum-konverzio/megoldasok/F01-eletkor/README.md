# Megoldás – 13-M04-L03-F01 · Életkor-kalkulátor

- A `BekerDatum` metódus a `TryParseExact`-tal ellenőriz. Ez a formátumot és a dátum létezését is vizsgálja: a `2026.02.30.` is érvénytelen. Ha üres bemenet megengedett, `DateTime.Today`-t ad vissza.
- Az életkor számítása az évek különbségéből indul, és csökken eggyel, ha idén még nem volt születésnap. Ez a szökőnapoktól független, helyes módszer.
- A `(ma - szuletes).Days` egy `TimeSpan` egész napjainak száma.
- A következő születésnap `szuletes.AddYears(kor + 1)`, mert a `kor` már a betöltött éveket jelenti. Ha a születésnap ma van, az `AddYears(kor)` éppen a mai nap.
- A `Datum` egy **nem statikus** lokális függvény, ezért eléri a fenti `hu` változót. A statikus lokális függvények (`static …`) a környező változókat nem látják.

Ellenőrzés a mintaadatokkal (született 2008.03.15., ma 2026.05.15.): 18 év, 6635 nap. A következő születésnap 2027.03.15., 304 nap múlva; a 10 000. nap 2035.07.31.

Futtatás: F5 (Visual Studio).
