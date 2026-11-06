# 13-M02-L01-F03 · Egyszerű számológép szövegből

**Nehézség:** ★★★ **Becsült idő:** 60 perc

## Feladat

Készíts `Szovegszamologep` nevű programot, amely egy begépelt, **két számból és egy műveleti jelből** álló kifejezést kiértékel.

1. A program ismételten kérjen be kifejezést, amíg üres sort nem kap.
2. Megengedett műveletek: `+`, `-`, `*`, `/`. A számok egészek; előttük, utánuk és a jel körül tetszőleges szóköz lehet: `12+3`, `  7 * 6`, `100 / 8`.
3. Az **első szám negatív is lehet**: `-5 - 3` (ilyenkor az első `-` jel nem műveleti jel!).
4. Az eredményt írja ki (osztásnál 2 tizedesre kerekítve).
5. Kezelje a hibákat üzenettel, ne álljon le a program:
   - nincs műveleti jel, vagy valamelyik oldal nem egész szám,
   - nullával való osztás.

## Minta bemenet és kimenet

```
Kifejezés: 12+3
= 15
Kifejezés:   7 * 6
= 42
Kifejezés: -5 - 3
= -8
Kifejezés: 100 / 8
= 12,50
Kifejezés: 5 / 0
Hiba: nullával nem lehet osztani.
Kifejezés: 3 x 4
Hiba: nem találok műveleti jelet.
Kifejezés:
Viszlát!
```

## Tippek

- Szóköztelenítés: `kifejezes.Replace(" ", "")`.
- A műveleti jel helye: `IndexOfAny(new[] { '+', '-', '*', '/' }, 1)` – a második paraméter miatt az **1. indextől** keres, így a kezdő mínuszjelet átugorja.
- A két oldal: `Substring` vagy tartomány (`[..hely]`, `[(hely + 1)..]`).
- Szám-e? `int.TryParse(szoveg, out int a)` – `true`-t ad, ha sikerült (az `out` paraméterről bővebben: 13-M02-L04).
- Extra: működik-e a programod a `4*-2` kifejezésre? És a `4--2`-re?

Megoldás: [../../megoldasok/F03-szovegszamologep/](../../megoldasok/F03-szovegszamologep/)
