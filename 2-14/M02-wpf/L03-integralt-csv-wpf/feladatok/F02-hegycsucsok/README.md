# 14-M02-L03-F02 · Hegycsúcsok böngészője

**Nehézség:** ★★☆ **Becsült idő:** 50 perc

Adatfájl: [hegycsucsok.csv](hegycsucsok.csv) (UTF-8, pontosvesszővel tagolt, első sor fejléc, 20 csúcs)

```
Nev;Hegyseg;Orszag;Kontinens;Magassag;ElsoMegmaszas
Mount Everest;Himalája;Nepál–Kína;Ázsia;8849;1953
Mont Blanc;Alpok;Franciaország–Olaszország;Európa;4806;1786
```

## Feladat

Készíts WPF-alkalmazást `Hegycsucsok` néven, amely a fájlban lévő hegycsúcsokat jeleníti meg.

1. `Hegycsucs` osztály a mezőkkel, statikus `CsvSorbol` metódussal, és egy számított `Nyolcezres` tulajdonsággal (magasság ≥ 8000 m).
2. Indításkor olvasd be a fájlt egy `List<Hegycsucs>`-ba (a fájl a program mellé másolódjon), hibakezeléssel.
3. Felül: `ComboBox` a kontinensekkel (ábécérendben, elöl `(mind)`), mellette két `RadioButton`: **magasság szerint** (csökkenő, alapértelmezett) / **név szerint**.
4. Bal oldalon `ListBox`, `DataTemplate`-tel: név félkövéren, mellette a magasság `m`-rel.
5. Jobb oldalon „Részletek” panel adatkötéssel a kiválasztott csúcsra: név, hegység, ország, kontinens, magasság, első megmászás éve,
   és egy (nem kattintható) `CheckBox` „Nyolcezres” felirattal.
6. Alul egy sor a szűrt listáról: darabszám, ebből hány nyolcezres, átlagmagasság (egy tizedesjegyre), a legkorábban megmászott csúcs neve.

## Minta

```
Kontinens: [Európa   v]   (•) magasság szerint  ( ) név szerint
Elbrusz          5642 m     ┌ Részletek ─────────────────┐
Mont Blanc       4806 m     │ Mont Blanc                 │
Matterhorn       4478 m     │ Hegység: Alpok             │
...                         │ Első megmászás: 1786       │
                            └────────────────────────────┘
6 csúcs, ebből nyolcezres: 0 db, átlagmagasság: 4040,5 m, legkorábban megmászott: Triglav
```

Ellenőrzés `(mind)` esetén: 20 csúcs, ebből nyolcezres: 5 db, átlagmagasság: 5707,8 m, legkorábban megmászott: Triglav.

## Tippek

- A kontinensek listája: `csucsok.Select(c => c.Kontinens).Distinct().OrderBy(k => k)`.
- Rendezés feltételtől függően: `szurt = NevSzerint.IsChecked == true ? szurt.OrderBy(c => c.Nev) : szurt.OrderByDescending(c => c.Magassag);`.
- A „nem kattintható” jelölőnégyzet: `IsChecked="{Binding Nyolcezres, Mode=OneWay}" IsHitTestVisible="False"` (számított tulajdonsághoz csak egyirányú kötés lehet!).
- A legkorábbi: `lista.MinBy(c => c.ElsoMegmaszas)`.

Megoldás: [../../megoldasok/F02-hegycsucsok/](../../megoldasok/F02-hegycsucsok/)
