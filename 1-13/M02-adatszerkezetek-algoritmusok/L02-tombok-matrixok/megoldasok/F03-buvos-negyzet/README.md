# Megoldás – 13-M02-L02-F03 · Bűvös négyzet

- A beolvasásnál a `int.TryParse(darabok[o], out negyzet[s, o])` közvetlenül a mátrix cellájába ír. Ha valamelyik darab hibás, a `jo` hamis lesz, és a sort újra bekérjük.
- Az ellenőrző függvény az **első** hibánál `return`-nel kilép. Ez a „keresés” gondolatmenete: amint találunk egy rossz sort, a többit fölösleges vizsgálni.
- A `bool[] volt` tömb a „láttuk-e már ezt a számot?” kérdésre ad gyors választ; az indexe maga a szám. Ugyanez a minta jól használható a megszámlálásra is (`int[] db`).
- A sziámi módszerben a `% pn` adja a „körbeérést”. A `sor - 1 + pn` azért kell, mert C#-ban negatív szám maradéka negatív (`-1 % 5 == -1`), Pythonban viszont `4`.
- A program végén lévő `static` lokális függvények a felső szintű utasítások (top-level statements) után is meghívhatók a fájl elejéről. Ezek már a 13-M02-L04 lecke saját metódusait vezetik be.
- Páros n esetén a sziámi módszer nem működik, ezért ilyenkor a program egy 5×5-ös példát állít elő.
