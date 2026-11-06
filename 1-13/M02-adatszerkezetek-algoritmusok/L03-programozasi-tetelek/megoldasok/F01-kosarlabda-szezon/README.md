# Megoldás – 13-M02-L03-F01 · Kosárlabda-szezon

- A **párhuzamos tömbök** ugyanazon indexe ugyanahhoz a mérkőzéshez tartozik. Ezért elég egyetlen ciklusváltozó, és mindkét tömböt `[i]`-vel indexeljük. A 13-M02-L04 leckében ezt egy `struct Merkozes` tömbje váltja ki.
- A 4. kérdésnél a kiválasztás azért biztonságos, mert a feladat szerint tudjuk, hogy van vereség. Ha ez nem garantált, mindig keresést kell írni.
- Az 5. kérdés feltételét tagadva írtuk: `!(különbség > 20)`, vagyis „addig lépünk, amíg **nem** találunk megfelelőt”. Ugyanez rövidebben: `sajat[s] - ellenfel[s] <= 20`.
- A 6. kérdésnél a maximumkiválasztás nem a tömb elemein, hanem egy **belőlük számolt értéken** fut. A tétel ugyanaz, csak a feltétel változik.
- A 7. kérdés eredménye „nem”, mert a 6. mérkőzésen 59 pont született. Ellenőrzéshez LINQ-val: `sajat.All(x => x >= 60)`.
