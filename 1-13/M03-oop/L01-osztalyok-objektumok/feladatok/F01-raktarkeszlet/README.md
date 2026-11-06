# 13-M03-L01-F01 · Raktárkészlet

**Nehézség:** ★☆☆ **Becsült idő:** 35 perc

## Feladat

Egy kis bolt raktárkészletét kell nyilvántartani. Készíts .NET 8 konzolos alkalmazást `Raktarkeszlet` néven.

1. Hozz létre egy `Termek` osztályt **külön fájlban** (`Termek.cs`):
   - `Nev` – ellenőrzött tulajdonság privát mezővel; üres név esetén `ArgumentException`-t dobjon;
   - `Ar` – egységár forintban (egész); csak pozitív lehet, különben `ArgumentException`;
   - `Keszlet` – a raktáron lévő darabszám; **kívülről csak olvasható** (`private set`);
   - `Ertek` – számított tulajdonság: egységár × készlet;
   - konstruktor `(nev, ar, keszlet)`;
   - `void Bevetelez(int db)` – növeli a készletet (csak pozitív darabszámot fogadjon el);
   - `bool Elad(int db)` – ha van elég készlet, csökkenti, és `true`-t ad vissza; különben nem változtat semmit, és `false`-t ad vissza;
   - felüldefiniált `ToString()`: név, ár, készlet és érték egy sorban.
2. A főprogram:
   - töltsön fel egy `List<Termek>` listát legalább 5 termékkel (legyen köztük 0 készletű is), és írja ki őket;
   - adjon el 3 darabot az első termékből, és próbáljon eladni 100 darabot a másodikból – írja ki, sikerült-e;
   - vételezzen be 20 darabot a nulla készletű termékből;
   - írja ki a raktárkészlet teljes értékét, azt, hogy hány termékből van 5 darabnál kevesebb, és a legolcsóbb terméket (ciklussal);
   - próbáljon létrehozni egy negatív árú terméket, és a hibaüzenetet írja ki (a program ne álljon le).

## Minta kimenet

```
Raktárkészlet:
  Tej 1 l               459 Ft   24 db   11 016 Ft
  Kenyér 1 kg           899 Ft   12 db   10 788 Ft
  Vaj 250 g             1 190 Ft  0 db        0 Ft
  ...
Eladás (Tej, 3 db): sikerült
Eladás (Kenyér, 100 db): nincs elég készlet
Bevételezés: Vaj 250 g +20 db

Raktárkészlet értéke: 66 457 Ft
5 db alatti készlet: 1 termék
Legolcsóbb: Tej 1 l (459 Ft)
Hiba: Az ár csak pozitív lehet.
```

## Tippek

- Mintának nézd meg a [BankszamlaDemo](../../peldak/BankszamlaDemo/) példát: a `Keszlet` ugyanúgy működik, mint ott az `Egyenleg`.
- Igazításhoz: `$"{Nev,-20}{Ar,8:N0} Ft"` – a negatív szélesség balra, a pozitív jobbra igazít.
- A `Termek` osztály `public` legyen, és a `Program.cs` elején ne felejtsd el a `using Raktarkeszlet;` sort.

Megoldás: [../../megoldasok/F01-raktarkeszlet/](../../megoldasok/F01-raktarkeszlet/)
