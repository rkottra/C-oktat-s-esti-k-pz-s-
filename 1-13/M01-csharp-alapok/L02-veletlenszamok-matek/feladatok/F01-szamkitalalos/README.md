# 13-M01-L02-F01 · Számkitalálós játék

**Nehézség:** ★☆☆ **Becsült idő:** 30 perc

## Feladat

Készíts konzolos programot `Szamkitalalos` néven: a gép „gondol” egy számot, a felhasználó pedig kitalálja.

1. A program generáljon egy véletlen egész számot 1 és 100 között (mindkét végpont lehessen).
2. Kérjen be tippeket egészen addig, amíg a felhasználó el nem találja. Minden tipp után írja ki: „Nagyobbra gondoltam.” vagy „Kisebbre gondoltam.”
3. Ha a beírt szöveg nem szám, vagy nem 1 és 100 közé esik, a program jelezze, és ezt a próbálkozást ne számolja.
4. Találat esetén írja ki, hány tippből sikerült (érvényes tippek).
5. Ezután kérdezze meg: „Új játék? (i/n)”. Csak az `i` és `n` billentyűt fogadja el (kis- és nagybetűvel is); `i` esetén induljon új játék, `n` esetén lépjen ki.

## Minta kimenet

```
Gondoltam egy számra 1 és 100 között.
Tipp: 50
Kisebbre gondoltam.
Tipp: huszonöt
Ez nem 1 és 100 közötti egész szám!
Tipp: 25
Nagyobbra gondoltam.
Tipp: 37
Eltaláltad 3 tippből!
Új játék? (i/n): x
Új játék? (i/n): n
Viszlát!
```

## Tippek

- `r.Next(1, 101)` – a felső határ kizárt.
- A rossz bemenetet a `TryParse` kezeli kivétel nélkül: `if (!int.TryParse(Console.ReadLine(), out int tipp) || tipp < 1 || tipp > 100) { … }`
- A tippelés ciklusa jól megírható `do … while (tipp != gondolt)` szerkezettel, az egész játék pedig egy külső `do … while (ujra)` ciklussal.
- A válasz ellenőrzésénél figyelj a logikára! „Addig kérdezünk, amíg a válasz **nem i és nem n**”: `while (v != 'i' && v != 'n')`. A `||`-os változat végtelen ciklus lenne – miért?

Megoldás: [../../megoldasok/F01-szamkitalalos/](../../megoldasok/F01-szamkitalalos/)
