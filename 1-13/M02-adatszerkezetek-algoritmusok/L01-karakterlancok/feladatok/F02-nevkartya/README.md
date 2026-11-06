# 13-M02-L01-F02 · Névkártya – nevek feldolgozása

**Nehézség:** ★★☆ **Becsült idő:** 45 perc

## Feladat

Egy rendezvényen a résztvevők maguk gépelik be a nevüket – hol csupa kisbetűvel, hol felesleges szóközökkel. Készíts `Nevkartya` nevű programot, amely egy bekért teljes névből (vezetéknév + egy vagy több keresztnév):

1. **Rendbe teszi a nevet**: levágja a szóközöket az elejéről és a végéről, a szavak közötti többszörös szóközt egyre cseréli, és minden névtagot nagy kezdőbetűvel, a többit kisbetűvel írja.
2. Kiírja külön a **vezetéknevet** és a **keresztnevet** (több keresztnév esetén mindet).
3. Elkészíti a **monogramot** pontokkal: `K. A. M.`
4. Előállít egy **felhasználónevet**: a vezetéknév első legfeljebb 5 betűje + az első keresztnév első betűje, csupa kisbetűvel, **ékezetek nélkül** (`á→a`, `é→e`, `ő→o`, `ű→u` …).
5. Kiírja a nevet egy **névkártyára**: csillagokból álló keretben, középre igazítva, a kártya belső szélessége 30 karakter.

Ha a beírt név egyetlen szóból áll, a program írjon hibaüzenetet.

## Minta bemenet és kimenet

```
Teljes név:    kovács   anna MÁRIA 
Név: Kovács Anna Mária
Vezetéknév: Kovács, keresztnév: Anna Mária
Monogram: K. A. M.
Felhasználónév: kovaca
********************************
*      Kovács Anna Mária       *
********************************
```

## Tippek

- Szavakra bontás: `Split(' ', StringSplitOptions.RemoveEmptyEntries)` – ez a többszörös szóközt is „elnyeli”.
- Nagy kezdőbetű: `szo[..1].ToUpper() + szo[1..].ToLower()`.
- Keresztnevek összefűzése: `string.Join(" ", tagok[1..])` – a tömböknél is működik a tartomány.
- Ékezetek: egymás után több `Replace` (vagy egy ciklus két párhuzamos szöveggel: `"áéíóöőúüű"` → `"aeiooouuu"`).
- Középre igazítás: a bal oldali hely `(30 - nev.Length) / 2`, majd `PadLeft` és `PadRight`.

Megoldás: [../../megoldasok/F02-nevkartya/](../../megoldasok/F02-nevkartya/)
