# 13-M01-L02-F02 · Prímszámok

**Nehézség:** ★★☆ **Becsült idő:** 45 perc

## Feladat

Készíts konzolos programot `Primszamok` néven, amely több részfeladatban vizsgálja a prímszámokat. Minden részfeladat előtt jelenjen meg egy sárga címsor.

1. **Prímteszt:** kérj be egy 1-nél nagyobb egész számot, és döntsd el, prím-e. Ha nem prím, írd ki az összes 1-nél nagyobb, önmagánál kisebb osztóját.
2. **Az első 50 prím:** írd ki az első 50 prímszámot, soronként 10-et, jobbra igazítva 6 karakter széles oszlopokban.
3. **Az 5000. prím:** írd ki, melyik az 5000. prímszám.
4. **Ikerprímek:** írd ki az összes 200 alatti ikerprímpárt. (Ikerprímek: két prím, amelyek különbsége 2, pl. 11 és 13.)

## Minta kimenet

```
1. Prímteszt
Szám: 91
91 nem prím. Osztói: 7 13

2. Az első 50 prím
     2     3     5     7    11    13    17    19    23    29
    31    37    41    43    47    53    59    61    67    71
...
3. Az 5000. prím
48611

4. Ikerprímek 200 alatt
(3, 5) (5, 7) (11, 13) (17, 19) (29, 31) (41, 43) ...
```

## Tippek

- Egy `n` szám prím, ha 2 és √n között nincs osztója: `for (int oszto = 2; oszto * oszto <= n; oszto++)`.
- Egy logikai változó (`bool prim = true;`) és a `break` segít kilépni, ha találtál osztót.
- Ugyanazt a prímtesztet több helyen is használod. Ha már szeretnéd, írd meg függvényként (Pythonban `def`, C#-ban a fájl végén `static bool Prim(int n) { … }` – részletesen a 13-M02-L04 leckében); ha nem, másold be a ciklusokba.
- Az 5000. prímhez: számláld a prímeket, és addig vizsgáld a következő számot, amíg a számláló el nem éri az 5000-et (`while`).

Megoldás: [../../megoldasok/F02-primszamok/](../../megoldasok/F02-primszamok/)
