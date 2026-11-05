# 14-M05-L03-F03 · Bankszámla – teljes tesztkészlet

**Nehézség:** ★★★ **Becsült idő:** 60 perc

## Feladat

A [kiindulo/Bank](kiindulo/Bank/) osztálykönyvtár `Bankszamla` osztályához nincs egyetlen teszt sem. Készíts hozzá `BankTesztek` nevű MSTest-projektet,
és **tervezd meg a teszteseteket** a specifikáció alapján: normál esetek, határesetek, hibás bemenetek.

A specifikáció:

- **Konstruktor** `(tulajdonos, nyitoEgyenleg = 0)`: a nyitóegyenleg nem lehet negatív (`ArgumentOutOfRangeException`).
- **Befizet(osszeg)**: csak pozitív összeg (különben `ArgumentOutOfRangeException`); az egyenleg nő.
- **Kivesz(osszeg)**: csak pozitív összeg (különben `ArgumentOutOfRangeException`); ha nincs rá fedezet, `InvalidOperationException`.
  **A teljes egyenleg felvehető** (utána 0 marad).
- **Atutal(cel, osszeg)**: a pénz a küldő számlájáról a cél számlájára kerül.
  - `null` cél: `ArgumentNullException`; saját számlára: `InvalidOperationException`;
  - fedezethiány: `InvalidOperationException`, és **egyik számla egyenlege sem változhat**.
- Bármilyen kivétel esetén az egyenleg(ek) változatlan(ok) maradnak.

Lépések:

1. Először **papíron** készíts tesztesettáblát (Mit tesztelek? Bemenet? Elvárt eredmény?) – legalább 12 esettel.
2. Valósítsd meg a teszteket (egy teszt – egy eset; ahol csak az adat más, `[DataRow]`).
3. Ahol kivételt vársz, ott ellenőrizd azt is, hogy az egyenleg nem változott.
4. Futtasd: a kódban **három hiba** van. Írd le mindegyikről: melyik teszt találta meg, mi volt az oka, hogyan javítottad.
5. Javítás után minden teszt legyen zöld.

## Minta tesztesettábla (részlet)

| # | Eset | Bemenet | Elvárt |
|---|---|---|---|
| 1 | befizetés | egyenleg 1000, befizet 500 | egyenleg 1500 |
| 2 | befizetés 0 | egyenleg 1000, befizet 0 | `ArgumentOutOfRangeException`, egyenleg 1000 |
| 3 | teljes egyenleg kivétele | egyenleg 1000, kivesz 1000 | egyenleg 0 |
| … | | | |

## Tippek

- Mindegyik teszt hozza létre a saját számláit, így a tesztek nem függenek egymástól és tetszőleges sorrendben futhatnak.
- Átutalásnál **mindkét** számla egyenlegét ellenőrizd.
- A `Assert.AreEqual(elvart, tenyleges, "üzenet")` harmadik paramétere sokat segít, ha egy tesztben több Assert van.

Megoldás: [../../megoldasok/F03-bankszamla/](../../megoldasok/F03-bankszamla/)
