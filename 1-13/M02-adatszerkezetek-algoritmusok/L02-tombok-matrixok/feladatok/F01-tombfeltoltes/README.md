# 13-M02-L02-F01 · Tömbök feltöltése

**Nehézség:** ★☆☆ **Becsült idő:** 30 perc

## Feladat

Készíts `Tombfeltoltes` nevű programot, amely háromféleképpen tölt fel egy-egy tömböt, és mindegyiket kiírja egy sorba, vesszővel elválasztva.

1. **Képlettel:** egy 10 elemű tömb az első 10 páros pozitív számmal (2, 4, …, 20), egy másik 10 elemű tömb az első 10 négyzetszámmal (1, 4, 9, …, 100).
2. **Véletlenszámokkal:** egy 15 elemű tömb 1 és 100 közötti (a határokat is beleértve) egész számokkal. Írd ki az elemeket, majd a beépített műveletekkel az összegüket, a legkisebb és a legnagyobb elemet. Ezután rendezd a tömböt növekvő sorrendbe, és írd ki újra.
3. **Billentyűzetről:** kérd be a hét 7 napjának délben mért hőmérsékletét (egész szám, ellenőrzött beolvasással). Írd ki a heti átlagot egy tizedesjegyre, majd a `foreach` ciklussal azokat a hőmérsékleteket, amelyek az átlagnál magasabbak.

## Minta bemenet és kimenet

```
Páros számok: 2, 4, 6, 8, 10, 12, 14, 16, 18, 20
Négyzetszámok: 1, 4, 9, 16, 25, 36, 49, 64, 81, 100

Véletlen tömb: 67, 3, 91, 45, 12, 88, 30, 57, 19, 74, 6, 99, 41, 23, 60
Összeg: 715, legkisebb: 3, legnagyobb: 99
Rendezve: 3, 6, 12, 19, 23, 30, 41, 45, 57, 60, 67, 74, 88, 91, 99

1. nap hőmérséklete: 18
2. nap hőmérséklete: huszonkettő
Egész számot adj meg!
2. nap hőmérséklete: 22
...
Heti átlag: 20,3 °C
Átlag feletti napok hőmérséklete: 22 24 21
```

## Tippek

- A páros számok képlete: `t[i] = 2 * (i + 1)`; a négyzetszámoké: `(i + 1) * (i + 1)`.
- A véletlenszámot `rnd.Next(1, 101)` adja: a felső határ **kizárt**.
- Kiírás egy sorban: `string.Join(", ", t)`.
- Az összeg, a minimum és a maximum: `t.Sum()`, `t.Min()`, `t.Max()`; a rendezés: `Array.Sort(t)`.
- Átlagnál osztás előtt alakítsd valóssá az összeget: `(double)osszeg / t.Length`.

Megoldás: [../../megoldasok/F01-tombfeltoltes/](../../megoldasok/F01-tombfeltoltes/)
