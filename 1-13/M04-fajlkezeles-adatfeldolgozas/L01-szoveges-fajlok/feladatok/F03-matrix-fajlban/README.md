# 13-M04-L01-F03 · Mátrix fájlba írása és visszaolvasása

**Nehézség:** ★★★ **Becsült idő:** 60 perc

## Feladat

Készíts `MatrixFajlban` nevű .NET 8 konzolos alkalmazást, amely egy mátrixot fájlba ment, majd onnan visszaolvas és feldolgoz. Ehhez a feladathoz nincs adatfájl: a program maga készíti el.

1. Tölts fel egy 10×10-es `int[,]` mátrixot 10 és 99 közötti véletlen egészekkel.
2. Írd ki a mátrixot a `matrix.txt` fájlba: minden mátrixsor egy fájlsor legyen, az elemek vesszővel elválasztva, szóköz nélkül (például `45,12,98,…`).
3. Olvasd vissza a fájlt egy **új** mátrixba. A sorok és az oszlopok számát a fájlból állapítsd meg (sorok száma; az első sor darabjainak száma), ne a 10-es állandóból!
4. Írd ki a visszaolvasott mátrixot igazítva, minden sor végén a sorösszeggel.
5. A mátrix alá írd az oszlopösszegeket és jobbra a teljes összeget.
6. Írd ki a legnagyobb elemet és a helyét (sor, oszlop, 1-től számozva), valamint az elemek átlagát két tizedesre.

## Minta kimenet (a számok véletlenszerűek)

```
A mátrix kiírva: matrix.txt

   52   87   15   ...   61 |   498
   ...
------------------------------------------------------------
  518  612  608  648  474  463  538  454  571  481 |  5367

A legnagyobb elem: 98 (1. sor, 6. oszlop)
Az elemek átlaga: 53,67
```

## Tippek

- Egy sor összefűzése: az elemek után vessző, kivéve az utolsót (`if (j < N - 1) sor += ",";`). Másik lehetőség, hogy egy sort előbb `int[]`-be gyűjtesz, és `string.Join(",", t)`-vel fűzöd össze.
- A sorok száma `sorok.Length`, az oszlopoké `sorok[0].Split(',').Length`.
- Az oszlopösszegeket egy `int[] oszlopOsszeg` tömbben gyűjtsd, ugyanabban a kettős ciklusban, amelyikben kiírsz.
- Ha a visszaolvasás után kitörlöd a kiíró részt, és kézzel szerkeszted a `matrix.txt`-t (például 5×8-asra), a programnak akkor is működnie kell.

Megoldás: [../../megoldasok/F03-matrix-fajlban/](../../megoldasok/F03-matrix-fajlban/)
