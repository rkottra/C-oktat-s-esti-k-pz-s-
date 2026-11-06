# 13-M04-L01-F02 · Raktárkészlet CSV-ből

**Nehézség:** ★★☆ **Becsült idő:** 60 perc

## Feladat

Egy számítástechnikai bolt raktárkészletét a [termekek.csv](termekek.csv) fájl tartalmazza. Az első sor fejléc, utána minden sor egy termék, pontosvesszővel tagolva:

```
Nev;Kategoria;GyartasiEv;Ar;Keszlet
Logitech Egér;Periféria;2022;8500;45
```

Készíts `Raktarkeszlet` nevű .NET 8 konzolos alkalmazást.

1. Készíts egy **`Termek`** osztályt (külön fájlban) az öt adattal. A konstruktora egy CSV-sort kapjon, és abból töltse ki a tulajdonságokat. Legyen egy `KeszletErtek` számított tulajdonsága (ár × készlet) és egy `ToString()`-je. Olvasd be a fájlt egy `List<Termek>` listába, és írd ki, hány termék van.
2. Mennyi a termékek átlagára?
3. Sorold fel a `Hardver` kategóriájú termékek nevét.
4. Melyik terméknek van a legkisebb készlete?
5. Mennyi a teljes raktárkészlet értéke?
6. Hány termék készült 2021 előtt?
7. Kérj be egy kategórianevet, kis- és nagybetűtől függetlenül. Írd ki, hány termék tartozik bele, és van-e köztük 100 000 Ft-nál drágább (ha igen, egy példát). Ha nincs ilyen kategória, írd ki.
8. Készíts rendelési listát a `rendelendo.txt` fájlba: azokat a termékeket, amelyekből 10 darabnál kevesebb van, a hiányzó mennyiséggel együtt, hogy 10 darabra egészüljön ki. Az első sor fejléc legyen: `Nev;Keszlet;Rendelendo`.

## Elvárt kimenet

```
1. Beolvasott termékek: 30 db
2. Átlagár: 61 507 Ft
3. Hardver kategória:
   Samsung Monitor
   Kingston Pendrive
   ...
   Sandisk SSD
4. Legkisebb készlet: BenQ Projektor (Periféria, 2021) – 125 000 Ft, készlet: 2 db
5. A raktárkészlet értéke: 19 354 500 Ft
6. 2021 előtt gyártott termékek: 5 db
7. Kategória: hardver
   14 termék tartozik ide.
   Van 100 000 Ft-nál drágább, pl.: Intel CPU
8. A rendelési lista elkészült: rendelendo.txt
```

A `rendelendo.txt` eleje:

```
Nev;Keszlet;Rendelendo
Gamer Szék;5;5
HP Nyomtató;8;2
```

## Tippek

- A beolvasó ciklus `i = 1`-től induljon, hogy a fejlécsor kimaradjon.
- A raktárérték nagy szám lehet, ezért az összegző változó legyen `long`.
- Kisbetű-független összehasonlítás: `t.Kategoria.ToLower() == kategoria.ToLower()`.
- Fájlba írás soronként: `using (StreamWriter sw = new StreamWriter("rendelendo.txt")) { … }`. A `using` a végén bezárja a fájlt.

Megoldás: [../../megoldasok/F02-raktarkeszlet/](../../megoldasok/F02-raktarkeszlet/)
