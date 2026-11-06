# Megoldás – 13-M02-L02-F02 · Domborzat

- A fix mag (`new Random(21)`) miatt mindenki ugyanazt a sorozatot kapja, így az eredmények összevethetők az osztályban.
- A kérdések a következő leckében tanult **programozási tételek** előfutárai: a 2. maximumkiválasztás, a 3. keresés, a 4. megszámlálás. A 13-M02-L03 leckében név szerint is előkerülnek.
- A tömb indexe 0-tól indul, a kilométerek 1-től, ezért kiíráskor mindenhol `+ 1` kell.
- Az 5. kérdésnél elég a szakasz **végét** és **hosszát** tárolni, mert a kezdőpont ezekből kiszámolható.
- A profilrajzban a mátrix 0. sora van felül, ezért a magasság „alulról” a `Sorok - 1 - k` sorba kerül. Előtte minden cellát szóközzel töltünk fel, mert a `char[,]` alapértéke a `'\0'` karakter, nem a szóköz.
- Bővítés: egy második, `0` és `14` közötti „felhőmagasság” tömbbel azt is meg lehet vizsgálni, hol takarták felhők a csúcsokat.
