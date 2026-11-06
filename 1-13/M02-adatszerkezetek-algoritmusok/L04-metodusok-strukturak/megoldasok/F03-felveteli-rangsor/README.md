# Megoldás – 13-M02-L04-F03 · Felvételi rangsor

- A `struct Jelentkezo` kiváltja a négy párhuzamos tömböt (`nevek`, `szakok`, `hozott`, `irasbeli`): egy jelentkező adatai együtt mozognak, rendezésnél sem csúszhatnak el egymáshoz képest.
- Az `Osszpont()` a struktúra metódusa: az összpontszámot nem tároljuk, hanem mindig a két mezőből számoljuk, így nem lehet ellentmondásos.
- Minden kérdés egy-egy **tétel egy metódusban**: `Letszam` (megszámlálás), `Atlag` (összegzés), `Legjobb` (maximumkiválasztás), `IrasbelinJobb` (kiválogatás), `Kereses` (keresés). A dolgozatban is ezt a szerkezetet várjuk.
- A `keret` tömböt a `Szak` értékével indexeljük (`keret[(int)sz]`). Ez akkor működik, ha az `enum` értékei 0-tól, folyamatosan számozódnak, és a tömb ugyanabban a sorrendben van.
- A rangsor rendezésénél a lambda előbb az összpontot hasonlítja, egyenlőségnél az írásbelit. A `b.CompareTo(a)` sorrend adja a csökkenő rendezést. Tóth Csaba és Kiss Anna egyaránt 89 pontos, a jobb írásbeli miatt Tóth Csaba a második.
- A 7. pontnál a helyezést úgy kapjuk meg, hogy a szak rangsorában újra keresünk. A `Kereses` metódust így kétszer is felhasználtuk.
