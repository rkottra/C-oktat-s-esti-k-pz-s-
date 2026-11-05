# 14-M01-L02-F01 · Országok adatai

**Nehézség:** ★★☆ **Becsült idő:** 50 perc

## Feladat

Az [orszagok.csv](orszagok.csv) a világ 194 országának adatait tartalmazza, pontosvesszővel elválasztva, fejléccel:

```
nev;fovaros;foldresz;terulet_km2;allamforma;nepesseg_ezer_fo
Afganisztán;Kabul;Ázsia;652225;köztársaság;25814
Albánia;Tirana;Európa;28748;köztársaság;3490
```

A terület km²-ben (tizedesponttal), a népesség **ezer főben** van megadva. (Az adatok egy régebbi kiadványból származnak, a mai értékektől eltérhetnek.)

Készíts .NET 8 konzolos alkalmazást `Orszagok` néven. Add a projekthez a CSV-fájlt, és állítsd be: *Copy to Output Directory → Copy if newer*.

1. Készíts `Orszag` osztályt a hat adattal, egy CSV-sort feldolgozó konstruktorral és egy számított `Nepsuruseg` tulajdonsággal (fő/km²). Írj statikus `FajlbolBetolt` metódust, amely a fájl összes sorából objektumot készít; a főprogram ezt `List<Orszag>`-ba töltse.

A további kérdéseket **LINQ-kel** válaszold meg, a sorszámukkal együtt írd ki a választ:

2. Hány ország adatai vannak a fájlban?
3. Földrészenként hány ország van, és mennyi az össznépességük (millió főben)? Darabszám szerint csökkenő sorrendben.
4. Melyik a legnagyobb és a legkisebb területű ország?
5. Hány ország neve kezdődik „M” betűvel?
6. Mennyi az országok átlagos területe? Mely európai országok nagyobbak ennél (ábécérendben)?
7. Melyik az 5 legsűrűbben lakott ország? (név és népsűrűség)
8. Mely országok népessége haladja meg az 1 milliárd főt?
9. Hány ország államformájában szerepel a „monarchia” vagy a „királyság” szó? Hányféle államforma van összesen?
10. Kérj be egy fővárost, és írd ki, melyik országé! Ha nincs ilyen, írj üzenetet. (A kis- és nagybetű ne számítson.)
11. Írd ki az `europa.csv` fájlba az európai országok nevét, fővárosát és népességét, népesség szerint csökkenő sorrendben, fejléccel.

## Minta kimenet

```
2. Országok száma: 194
3. Földrészenként:
   Afrika    53 ország,    847,7 millió fő
   Európa    48 ország,    812,9 millió fő
   ...
4. Legnagyobb területű: Oroszország (17 075 400 km²), legkisebb: Vatikán (0,44 km²)
...
7. A legsűrűbben lakott 5 ország:
   Monaco             15 385 fő/km²
   Szingapúr           6 474 fő/km²
   ...
10. Főváros: bern
   Bern → Svájc
11. europa.csv kiírva (48 ország).
```

## Tippek

- A területben tizedespont van: `double.Parse(adatok[3], CultureInfo.InvariantCulture)` (kell hozzá: `using System.Globalization;`).
- Az ezer főben megadott népességből a népsűrűség: `Nepesseg * 1000.0 / Terulet`.
- Objektum a legnagyobb értékkel: `MaxBy(o => o.Terulet)`; csak az érték: `Max(o => o.Terulet)`.
- Csoportosítás: `GroupBy(o => o.Foldresz).Select(cs => new { Foldresz = cs.Key, Darab = cs.Count() })`.
- Kis- és nagybetű nélküli összehasonlítás: `a.Equals(b, StringComparison.OrdinalIgnoreCase)`.
- Fejléc a kiírandó sorok elé: `.Prepend("nev;fovaros;nepesseg_ezer_fo")`.

Megoldás: [../../megoldasok/F01-orszagok/](../../megoldasok/F01-orszagok/)
