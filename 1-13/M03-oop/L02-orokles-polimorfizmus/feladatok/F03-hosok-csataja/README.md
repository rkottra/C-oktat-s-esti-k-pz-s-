# 13-M03-L02-F03 · Hősök csatája

**Nehézség:** ★★★ **Becsült idő:** 90 perc

## Feladat

Két háromfős csapat körökre osztott csatáját szimuláljuk. Minden hős támad és sebződik, de a kasztja szerint másképp. Készíts `HosokCsataja` nevű .NET 8 konzolos alkalmazást, minden osztályt külön fájlban.

1. **`Hos`** (ős):
   - `Nev`, `MaxElet`, `Tamadas` tulajdonság; `Elet` kívülről csak olvasható (`private set`), kezdetben `MaxElet`;
   - `OkozottSebzes`: az eddig kiosztott összes sebzés, kívülről csak olvasható;
   - `bool El`: igaz, ha `Elet > 0`;
   - `public virtual int Sebzes(Random rnd)`: `Tamadas` + 0 és 4 közötti véletlen szám;
   - `public virtual void Serul(int pont)`: csökkenti az életet, de 0 alá nem;
   - `public virtual void KorVege()`: alapból nem csinál semmit;
   - `public virtual string Kaszt()`: `"hős"`;
   - `public void Megtamad(Hos cel, Random rnd)`: kiszámolja a sebzést, a célpont `Serul`-ját hívja vele, növeli az `OkozottSebzes`-t, és kiír egy sort a csatanaplóba. Ez a metódus **nem virtuális**: a viselkedés a felülírt `Sebzes` és `Serul` metódusokon múlik;
   - `ToString()`: név, kaszt, élet / maximális élet.
2. **`Harcos : Hos`**: 25% eséllyel **kritikus** csapást mér, ilyenkor dupla a sebzése. Páncélja miatt minden kapott sebzésből 3 pontot elnyel, de legalább 1 pontot sebződik.
3. **`Ijasz : Hos`**: 30% eséllyel **két nyilat** lő ki, ilyenkor a sebzés a `base.Sebzes(rnd)` kétszeri hívásának összege.
4. **`Magus : Hos`**: van `Mana` tulajdonsága (kezdetben 20, legfeljebb 30). Ha a manája legalább 10, varázsol: a sebzése a `base.Sebzes` háromszorosa, és 10 manát fogyaszt. Ha nincs elég manája, csak a `base.Sebzes` felével üt. Minden kör végén 4 manát kap vissza (`KorVege`).
5. A főprogram:
   - hozzon létre két `List<Hos>` csapatot, mindkettőben legyen harcos, íjász és mágus. A `Random` objektum fix maggal készüljön (`new Random(7)`), hogy a csata megismételhető legyen;
   - egy körben előbb az első csapat minden élő hőse támad meg egy **véletlenszerűen választott élő** ellenfelet, aztán a második csapat hősei; a kör végén minden élő hős `KorVege()`-t hív;
   - a csata addig tart, amíg valamelyik csapatnak van élő tagja, de legfeljebb 30 körig;
   - a végén írja ki a győztes csapatot, a túlélőket, és azt a hőst, aki a legtöbb sebzést okozta.

## Minta kimenet (részlet; a számok a hősök adataitól és a véletlenszámok felhasználásának sorrendjétől függnek)

```
--- 1. kör ---
Brutusz (harcos) → Szilánk (íjász): 15 sebzés (élet: 55/70)
Fürge (íjász) → Vasököl (harcos): 13 sebzés (élet: 90/100)
Merlin (mágus) → Vasököl (harcos): 36 sebzés varázslat (élet: 57/100)
...
--- 3. kör ---
Brutusz (harcos) → Vasököl (harcos): 22 sebzés KRITIKUS! (élet: 2/100)
Fürge (íjász) → Vasököl (harcos): 10 sebzés (élet: 0/100)
   Vasököl elesett!
...
=== A csata 7 kör után véget ért. Győztes: Kék csapat ===
Túlélők: Brutusz (harcos) 55/100
Legtöbb sebzést okozta: Brutusz (harcos), 103 pont
```

## Tippek

- Hogy a naplóba a „KRITIKUS!” vagy a „varázslat” szó is bekerüljön, a leszármazott a `Sebzes`-ben beállíthat egy `protected string megjegyzes` mezőt, amelyet a `Megtamad` kiír, majd töröl.
- A `Harcos.Serul` felülírása: `base.Serul(Math.Max(1, pont - 3));`. Így az életet ténylegesen csak az ős módosítja.
- Véletlen élő ellenfél: előbb válogasd ki az élőket egy segédlistába, és abból válassz `rnd.Next(elok.Count)` indexszel.
- „Van-e még élő a csapatban?” Ez eldöntés, írd meg segédmetódusként.

Megoldás: [../../megoldasok/F03-hosok-csataja/](../../megoldasok/F03-hosok-csataja/)
