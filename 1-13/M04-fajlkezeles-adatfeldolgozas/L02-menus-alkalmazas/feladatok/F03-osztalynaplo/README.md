# 13-M04-L02-F03 · Osztálynapló két fájlból

**Nehézség:** ★★★ **Becsült idő:** 90 perc

## Feladat

Két osztály jegyeit két fájl tárolja. A [diakok.csv](diakok.csv) a diákokat, a [jegyek.csv](jegyek.csv) a kapott jegyeket tartalmazza. A két fájlt a diák **azonosítója** köti össze:

```
diakok.csv                   jegyek.csv
Azonosito;Nev;Osztaly        Azonosito;Tantargy;Jegy
D01;Ács Benedek;13A          D01;Matematika;4
D02;Bárány Luca;13A          D01;Matematika;5
...                          D01;Magyar;4 ...
```

Készíts `Osztalynaplo` nevű menüs .NET 8 konzolos alkalmazást.

1. **Osztályok:**
   - `Jegy`: `Tantargy`, `Ertek`;
   - `Diak`: `Azonosito`, `Nev`, `Osztaly`, valamint egy `List<Jegy> Jegyek` lista;
   - metódusok a `Diak` osztályban: `double Atlag()` (az összes jegy átlaga, jegy nélkül 0), `double Atlag(string tantargy)` (egy tantárgy átlaga, **túlterheléssel**), `bool Bukik()` (igaz, ha van olyan tantárgya, amelynek átlaga 2,0 alatt van).
2. **Beolvasás:** előbb a diákokat olvasd be egy `List<Diak>` listába, majd a jegyeket: minden jegysornál **keresd meg** az azonosító alapján a diákot, és add hozzá a jegyet a jegylistájához. Ha egy jegy olyan azonosítóhoz tartozik, amely nincs a diákok között, a program írjon figyelmeztetést, és hagyja ki.
3. A menü:

   ```
   1 – Diákok listája átlaggal
   2 – Diák adatlapja
   3 – Tantárgyi átlagok osztályonként
   4 – Bukásra állók
   5 – Új jegy beírása
   6 – Rangsor mentése
   0 – Kilépés
   ```

   - **1:** osztály és név szerint rendezve: azonosító, név, osztály, jegyek száma, átlag.
   - **2:** névrészlet alapján keres. Ha több találat van, sorszámmal felkínálja őket. A kiválasztott diák minden tantárgyánál kiírja a jegyeket és a tantárgyi átlagot.
   - **3:** táblázat: soronként egy tantárgy, oszloponként egy osztály, a cellákban az átlag.
   - **4:** a bukásra álló diákok, és melyik tantárgyból.
   - **5:** bekéri a diák azonosítóját (ellenőrizve), a tantárgyat (a már létező tantárgyak közül sorszámmal választva) és a jegyet (1–5). A jegyet a diák listájához adja, és **azonnal hozzáfűzi** a `jegyek.csv` végéhez (`File.AppendAllText`).
   - **6:** osztályonként a diákok átlag szerint csökkenő rangsora a `rangsor.txt` fájlba.

## Minta kimenet (részlet)

```
Választás: 4
Bukásra áll:
  Csonka Márton (13A) – Angol: 1,75
  Gulyás Ádám (13B) – Matematika: 1,33
```

## Tippek

- A két fájl összekapcsolása a keresés tétele: `while (i < diakok.Count && diakok[i].Azonosito != azon) i++;`. Érdemes egy `static Diak? Keres(List<Diak> d, string azon)` metódusba tenni, mert a jegybeírásnál is kell.
- A tantárgyak és osztályok listáját a beolvasott adatokból gyűjtsd ki, ne írd be kézzel.
- Rendezés két szempont szerint: `diakok.Sort((a, b) => a.Osztaly != b.Osztaly ? string.Compare(a.Osztaly, b.Osztaly) : string.Compare(a.Nev, b.Nev));`
- Hozzáfűzés a fájl végére: `File.AppendAllText("jegyek.csv", $"{azon};{tantargy};{jegy}{Environment.NewLine}");`

Megoldás: [../../megoldasok/F03-osztalynaplo/](../../megoldasok/F03-osztalynaplo/)
