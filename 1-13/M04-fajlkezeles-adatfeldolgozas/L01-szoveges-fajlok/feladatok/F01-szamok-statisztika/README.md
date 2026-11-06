# 13-M04-L01-F01 · Számok statisztikája fájlból

**Nehézség:** ★☆☆ **Becsült idő:** 45 perc

## Feladat

A [szamok.txt](szamok.txt) fájl minden sorában egy -1000 és 1000 közötti egész szám áll. Készíts `SzamokStatisztika` nevű .NET 8 konzolos alkalmazást, amely beolvassa a számokat egy `List<int>` listába, majd válaszol a kérdésekre. A tételeket saját ciklussal írd meg.

1. Hány szám van a fájlban?
2. Mennyi a számok összege és átlaga (két tizedesre)?
3. Melyik a legkisebb és a legnagyobb szám? A legnagyobbnál add meg azt is, hányadik sorban van.
4. Hány pozitív, negatív és nulla érték van?
5. Van-e 900-nál nagyobb abszolút értékű szám? Ha igen, melyik az első?
6. Írd ki a **páros** számokat a `parosak.txt` fájlba, soronként 10 darabot, vesszővel elválasztva (szóköz nélkül). Az utolsó sor lehet rövidebb.
7. *Szorgalmi:* számold ki a számok **mediánját** (a rendezett sorozat középső eleme, páros darabszámnál a két középső átlaga) és **szórását**: `√( Σ(x − átlag)² / n )`.

Az adatfájlt add hozzá a projekthez, és állítsd be a tulajdonságát: *Copy to Output Directory → Copy if newer*.

## Elvárt kimenet

```
1. A fájlban 200 szám van.
2. Összeg: 13456, átlag: 67,28
3. Legkisebb: -992, legnagyobb: 978 (a fájl 109. sorában)
4. Pozitív: 104, negatív: 96, nulla: 0
5. Van 900-nál nagyobb abszolút értékű szám, az első: 973
6. A páros számok kiírva: parosak.txt (10 sor)
7. Medián: 39, szórás: 579,96
```

A `parosak.txt` eleje:

```
-188,-990,458,-396,-892,-68,920,476,582,752
392,668,-608,-888,836,-136,930,-938,-734,-120
```

## Tippek

- Az üres sorokat (például a fájl végén) hagyd ki: `if (sor.Trim() != "")`.
- Az átlagnál osztás előtt `(double)`-re alakíts, különben egész osztás lesz.
- Az 5. kérdés eldöntés `while` ciklussal; az abszolút érték: `Math.Abs(x)`.
- A 6. pontban az aktuális sort egy szövegben gyűjtsd; ha összejött 10 szám, tedd egy `List<string>`-be, és kezdj új sort. A végén a maradékot se felejtsd el!
- A mediánhoz egy **másolatot** rendezz (`new List<int>(szamok)`), hogy az eredeti sorrend megmaradjon.

Megoldás: [../../megoldasok/F01-szamok-statisztika/](../../megoldasok/F01-szamok-statisztika/)
