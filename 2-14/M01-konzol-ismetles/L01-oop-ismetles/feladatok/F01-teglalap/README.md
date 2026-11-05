# 14-M01-L01-F01 · Téglalapok

**Nehézség:** ★☆☆ **Becsült idő:** 30 perc

## Feladat

Készíts .NET 8 konzolos alkalmazást `Teglalapok` néven.

1. Hozz létre egy `Teglalap` osztályt külön fájlban:
   - `Szelesseg` és `Magassag` (tört szám) – privát mező + tulajdonság; nem pozitív értékre `ArgumentException`-t dobjon;
   - csak olvasható, számított tulajdonságok: `Terulet`, `Kerulet`, `Atlo`, `Negyzet` (igaz, ha a két oldal egyenlő);
   - két konstruktor: `(szelesseg, magassag)` és `(oldal)` – az utóbbi négyzetet hoz létre, és a `this(...)` segítségével az elsőt hívja;
   - felüldefiniált `ToString()`, amely kiírja a típust (téglalap/négyzet), a méreteket, a területet, a kerületet és az átlót.
2. A főprogram:
   - töltsön fel egy `List<Teglalap>` listát legalább 5 elemmel (legyen köztük négyzet is), és írja ki az elemeit;
   - írja ki, hány négyzet van a listában, mennyi az összterület, és melyik a legnagyobb területű téglalap (ciklussal);
   - kérjen be egy új szélességet és magasságot, és vegye fel a listába; hibás bemenetnél (nem szám, nem pozitív) írjon érthető hibaüzenetet.

## Minta kimenet

```
Téglalapok:
  Téglalap 3 x 4: T = 12,00, K = 14,00, átló = 5,00
  Négyzet 5 x 5: T = 25,00, K = 20,00, átló = 7,07
  ...
Négyzetek száma: 2
Összterület: 88,50
Legnagyobb területű: Négyzet 5 x 5: T = 25,00, K = 20,00, átló = 7,07

Új téglalap szélessége: 4
Új téglalap magassága: 0
Hiba: A magasság csak pozitív lehet.
```

## Tippek

- Számított tulajdonság: `public double Terulet => Szelesseg * Magassag;` – nincs mögötte mező, nincs `set`.
- A konstruktorban is a tulajdonságnak adj értéket (ne a mezőnek), így az ellenőrzés ott is lefut.
- Két kivételt kell elkapnod: `FormatException` (nem szám) és `ArgumentException` (nem pozitív).

Megoldás: [../../megoldasok/F01-teglalap/](../../megoldasok/F01-teglalap/)
