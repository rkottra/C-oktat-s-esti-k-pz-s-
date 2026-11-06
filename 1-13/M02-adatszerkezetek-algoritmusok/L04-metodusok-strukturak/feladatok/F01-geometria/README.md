# 13-M02-L04-F01 · Geometriai számológép metódusokkal

**Nehézség:** ★☆☆ **Becsült idő:** 45 perc

## Feladat

Készíts `Geometria` nevű menüs programot, amely síkidomok kerületét és területét számolja. A cél a program **metódusokra bontása**: a felső szintű kódban (vagy a `Main`-ben) csak a menü és a metódushívások legyenek.

A menü:

```
1 – Kör
2 – Téglalap
3 – Háromszög (három oldalból)
0 – Kilépés
```

Írd meg legalább a következő metódusokat:

| Metódus | Feladata |
|---|---|
| `double BekerPozitiv(string kerdes)` | addig kérdez, amíg pozitív valós számot nem kap |
| `double KorKerulet(double r)`, `double KorTerulet(double r)` | kör kerülete és területe |
| `double TeglalapKerulet(double a, double b)`, `double TeglalapTerulet(double a, double b)` | téglalap kerülete és területe |
| `bool Szerkesztheto(double a, double b, double c)` | igaz, ha bármely két oldal összege nagyobb a harmadiknál |
| `double HaromszogTerulet(double a, double b, double c)` | terület Hérón képletével: `s = (a+b+c)/2`, `T = √(s(s−a)(s−b)(s−c))` |
| `void Eredmeny(string idom, double kerulet, double terulet)` | egységes, két tizedesre kerekített kiírás |

A program a kilépésig újra és újra feldobja a menüt. Érvénytelen menüpontnál írjon hibaüzenetet.

## Minta bemenet és kimenet

```
1 – Kör
2 – Téglalap
3 – Háromszög (három oldalból)
0 – Kilépés
Választás: 3
a = 3
b = 4
c = -5
Pozitív számot adj meg!
c = 5
Háromszög – kerület: 12,00, terület: 6,00

Választás: 3
a = 1
b = 2
c = 10
Ebből a három oldalból nem szerkeszthető háromszög.

Választás: 0
Viszlát!
```

## Tippek

- A `Math.PI` a π értéke, a `Math.Sqrt` a négyzetgyök.
- Az egysoros számoló metódusokat nyíl szintaxissal is írhatod: `static double KorTerulet(double r) => r * r * Math.PI;`
- A `BekerPozitiv` belsejében egy `while (true)` ciklus a `return` utasítással lép ki, amikor jó adatot kapott.
- A menü egy `do … while (valasztas != "0")` ciklusban egy `switch` utasítás legyen.

Megoldás: [../../megoldasok/F01-geometria/](../../megoldasok/F01-geometria/)
