# 14-M01-L01-F02 · Járműflotta

**Nehézség:** ★★☆ **Becsült idő:** 45 perc

## Feladat

Egy cég járműveinek éves díját kell nyilvántartani. Készíts .NET 8 konzolos alkalmazást `Jarmuvek` néven.

1. **`Jarmu`** absztrakt ősosztály:
   - `Rendszam` – nem lehet üres; tárolás előtt vágd le a szóközöket, és alakítsd nagybetűssé;
   - `GyartasiEv` – 1950 és az aktuális év között; különben `ArgumentException`;
   - `Kor` – számított tulajdonság (aktuális év − gyártási év);
   - absztrakt `int EvesDij()` metódus;
   - virtuális `Tipus` tulajdonság (alapértéke `"jármű"`);
   - `ToString()`: rendszám, típus, gyártási év, kor, éves díj.
2. Leszármazottak (mindegyik `base(...)`-szel hívja az ős konstruktorát, és felülírja a `Tipus`-t, az `EvesDij()`-at és a `ToString()`-et):

   | Osztály | Saját adat | Éves díj |
   |---|---|---|
   | `Szemelyauto` | `Ulesek` (int) | 30 000 Ft, 10 évnél idősebbnél 40 000 Ft |
   | `Teherauto` | `Teherbiras` (tonna, double) | 50 000 Ft + tonnánként 15 000 Ft |
   | `Motor` | `Hengerurtartalom` (cm³, int) | 12 000 Ft |

3. A főprogram egy `List<Jarmu>` listában tároljon legalább 5 járművet (mindhárom típusból), és:
   - próbáljon felvenni egy hibás gyártási évű járművet is – a hibaüzenetet írja ki, a program ne álljon le;
   - listázza a járműveket;
   - írja ki az éves díjak összegét, a típusonkénti darabszámot, a legidősebb jármű rendszámát és a teherautók átlagos teherbírását.

## Minta kimenet

```
Nem vettük fel: Érvénytelen gyártási év: 1890

A flotta járművei:
  ABC-123   személyautó  2019 ( 7 éves)  díj:  30 000 Ft  5 ülés
  TRK-404   teherautó    2015 (11 éves)  díj: 162 500 Ft  7,5 t
  M-777     motor        2012 (14 éves)  díj:  12 000 Ft  650 cm3
  ...
Éves díjak összesen: 347 000 Ft
Személyautó: 2, teherautó: 2, motor: 1
Legidősebb jármű: AA-BB-101 (2008)
Teherautók átlagos teherbírása: 5,50 t
```

## Tippek

- Az összegzésnél nem kell tudnod, milyen jármű van a lista adott helyén: `osszDij += j.EvesDij();` – mindig a saját változat fut le (polimorfizmus).
- A teherbírás csak `Teherauto`-ként érhető el: `if (j is Teherauto t) osszTeherbiras += t.Teherbiras;`
- Ezres tagolás kiíráskor: `{osszeg:N0}`.

Megoldás: [../../megoldasok/F02-jarmuvek/](../../megoldasok/F02-jarmuvek/)
