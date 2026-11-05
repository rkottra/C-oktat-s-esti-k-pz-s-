# 14-M06-L01-F01 · Pilóták – konzolos vizsgafeladat

**Nehézség:** ★★☆ **Időkeret:** 90 perc **Elérhető pontszám:** 30 pont

## Bevezetés

A [pilots.csv](pilots.csv) állomány a Formula–1 történetének 800 versenyzőjéről tartalmaz adatokat (nyilvános adatsor). Az állomány UTF-8 kódolású, pontosvesszővel tagolt, első sora fejléc:

| Mező | Jelentés | Típus |
|---|---|---|
| `id` | azonosító | egész |
| `name` | a pilóta neve | szöveg |
| `gender` | neme: `M` (férfi) vagy `F` (nő) | karakter |
| `birthdate` | születési dátum `éééé-hh-nn` formában; ha ismeretlen, értéke `null` | dátum |
| `nation` | nemzetiség (angolul) | szöveg |

```
id;name;gender;birthdate;nation
1;Juan-Manuel Fangio;M;1911-06-24;Argentinian
2;Sam Posey;M;1944-05-26;American
```

**Általános szabályok:** minden képernyőre írt eredmény előtt jelenjen meg a feladat sorszáma (pl. `3. feladat:`); a kiírások kövessék a mintát; ékezet nélküli azonosítók és kiírások is elfogadottak. A program bármely, azonos szerkezetű bemenettel is helyesen működjön – a mintában szereplő értékeket tilos beírni a kódba.

## Feladat

1. **(2 pont)** Hozz létre `PilotakKonzol` nevű .NET 8 konzolos projektet. A `pilots.csv` állományt add a projekthez úgy, hogy futtatáskor a program mellé másolódjon.
2. **(5 pont)** Készíts `Pilota` osztályt az alábbi tulajdonságokkal: `Id` (egész), `Nev` (szöveg), `Nem` (karakter), `SzuletesiDatum` (dátum, **lehet ismeretlen**), `Nemzetiseg` (szöveg).
   - Legyen az osztálynak egy olyan konstruktora, amely egy fájlbeli sort kap paraméterként, és abból tölti fel a tulajdonságokat.
   - Készíts statikus `BetoltCsv` metódust, amely a fájl nevét kapja paraméterként, és a pilóták listájával tér vissza. A beolvasást ezzel végezd.
3. **(2 pont)** Írd ki, hány pilóta adatai szerepelnek az állományban.
4. **(3 pont)** Írd ki a női pilóták számát, majd egy sorban, vesszővel elválasztva a nevüket.
5. **(4 pont)** Határozd meg az ismert születési dátumú pilóták közül a legidősebbet: írd ki a nevét, a születési dátumát `éééé.hh.nn` formában és a nemzetiségét. Külön sorban írd ki azt is, hány pilótának ismeretlen a születési dátuma.
6. **(5 pont)** Kérj be a felhasználótól egy névrészletet. Listázd azokat a pilótákat, akiknek a nevében **valamelyik szó** (a szóközzel vagy kötőjellel elválasztott részek bármelyike) a megadott részlettel kezdődik – a kis- és nagybetűk között ne tegyél különbséget. Soronként tabulátorral tagolva írd ki az azonosítót, a nevet, a születési dátumot (ismeretlen dátum helyett `ismeretlen`), a nemet és a nemzetiséget. Ha nincs ilyen pilóta, írj ki erről tájékoztató üzenetet.
7. **(4 pont)** Írd ki az 5 legtöbb pilótát adó nemzetiséget és pilótáik számát, a létszám szerint csökkenő sorrendben (azonos létszámnál ábécérendben).
8. **(5 pont)** Kérd be egy nemzetiség kezdőbetűjét. Hozd létre a `pilota_csoportok.txt` állományt, amelybe a megadott betűvel kezdődő nemzetiségek kerülnek ábécérendben. Minden nemzetiségnél először a nemzetiség neve és a létszám szerepeljen, alatta tabulátorral beljebb a pilóták neve és születési dátuma, születési dátum szerint növekvő sorrendben (ismeretlen dátumú pilóta a csoport végére kerüljön). Sikeres írás után jelezd a képernyőn, hogy a fájl elkészült; ha a fájl nem írható, a program ne álljon le, hanem írja ki: `Hiba! A fájl írása sikertelen.`

## Minta kimenet

```
3. feladat: Pilóták száma: 800
4. feladat: Női pilóták (5 fő): Divina Galica, Maria-Teresa de Filippis, Lella Lombardi, Giovanna Amati, Desire Wilson
5. feladat:
    Legidősebb pilóta: Philippe Etancelin (1896.12.28, French)
    Ismeretlen születési dátum: 1 fő
6. feladat: Adjon meg egy névrészletet: sch
    74      Michael Schumacher      1969.01.03      M       German
    103     Heinz Schiller          1930.01.25      M       Swiss
    114     Adolfo Schwelm-Cruz     1923.06.28      M       Argentinian
    ...
7. feladat: Az 5 legtöbb pilótát adó nemzet:
    American: 156 fő
    British: 152 fő
    ...
8. feladat: Adja meg a nemzetiség kezdőbetűjét: C
    A pilota_csoportok.txt fájl elkészült.
```

(A mintában a behúzások tabulátorok.) A `pilota_csoportok.txt` eleje `C` betű esetén:

```
Canadian (12 fő):
    Al Pease - 1921.10.15
    Peter Broeker - 1929.05.15
    ...
Chilean (1 fő):
    Eliseo Salazar - 1954.11.04
```

## Tippek

- A `"null"` szöveget a dátum feldolgozása **előtt** vizsgáld: `adatok[3] == "null" ? null : DateTime.Parse(…)`.
- A dátum formázása: `SzuletesiDatum?.ToString("yyyy.MM.dd") ?? "ismeretlen"`.
- A 6. feladathoz: `Nev.Split(' ', '-').Any(szo => szo.StartsWith(reszlet, StringComparison.OrdinalIgnoreCase))`.
- Ismeretlen dátum a rendezés végére: `OrderBy(p => p.SzuletesiDatum ?? DateTime.MaxValue)`.

Megoldás és javítókulcs: [../../megoldasok/F01-pilotak-konzol/](../../megoldasok/F01-pilotak-konzol/)
