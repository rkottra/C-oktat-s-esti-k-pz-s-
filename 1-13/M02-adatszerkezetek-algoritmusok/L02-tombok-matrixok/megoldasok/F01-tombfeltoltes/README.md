# Megoldás – 13-M02-L02-F01 · Tömbök feltöltése

- A két képlettel feltöltött tömb ugyanakkora, ezért egyetlen ciklusban tölthetjük fel mindkettőt.
- A `Random` objektumot **egyszer** hozzuk létre, a ciklus előtt. Teszteléshez adhatunk neki magot (`new Random(42)`), így minden futáskor ugyanazt a sorozatot kapjuk.
- Az `Array.Sort` **helyben** rendez, vagyis az eredeti sorrend elvész. Ha az eredetire is szükség van, előbb készíts másolatot: `(int[])veletlen.Clone()`.
- A `while (!int.TryParse(...))` addig kérdez újra, amíg érvényes egész számot nem kap; a beolvasott érték az `out` változóba kerül.
- Az átlag kiszámításánál a `(double)` átalakítás nélkül egész osztás lenne, és elveszne a tört rész.
- A feltöltéshez `for` kell, az olvasáshoz elég a `foreach`.
