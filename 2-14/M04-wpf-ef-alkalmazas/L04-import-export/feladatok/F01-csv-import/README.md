# 14-M04-L04-F01 · Bevételezés CSV-fájlból

**Nehézség:** ★★☆ **Becsült idő:** 60 perc

## Feladat

A beszállító CSV-fájlban küldi az árut. Készíts `RaktarImport` néven alkalmazást, amely ezt bevételezi az `estis_raktar` adatbázisba.

Bemenet: [termekek-import.csv](termekek-import.csv) – UTF-8, pontosvesszővel tagolt, fejléces:

```
Cikkszam;Nev;Kategoria;Ar;Darab
KS-004;Vízmérték 60 cm;Kéziszerszám;3590;10
EG-003;Dekopírfűrész;Elektromos gép;21990;5
KT-005;Gereblye;Kertészet;3190;x
VL-001;Hosszabbító 5 m;Villanyszerelés;3490;15
...
```

1. Gomb: „Bevételezés CSV-ből…” – `OpenFileDialog`-gal választható ki a fájl.
2. Soronként:
   - ha a sor hibás (nem 5 mező, nem egész ár/darab, negatív darab) → kihagyja;
   - ha a **cikkszám már létezik** (az adatbázisban, vagy a fájlban korábban felvett új termékként) → csak a **darabszámát növeli**;
   - különben új termék; a kategóriát **név alapján** keresi, és ha nincs ilyen, újat hoz létre (pl. *Villanyszerelés*).
3. Az új termékeket `AddRange`-dzsel, a módosításokkal együtt **egyetlen** `SaveChanges`-szel mentse; hiba esetén semmi ne változzon.
4. Egy naplómezőben soronként írja ki, mi történt, a végén összesítést.
5. Frissüljön a táblázat.

## Minta kimenet (napló)

```
KS-004: új termék (Vízmérték 60 cm)
KS-005: új termék (Mérőszalag 5 m)
EG-004: új termék (Ütvefúrógép)
EG-003: készlet +5 → 5 db
KT-004: új termék (Sövénynyíró olló)
7. sor hibás, kihagyva: KT-005;Gereblye;Kertészet;3190;x
FS-004: új termék (Kültéri lazúr 2,5 l)
FS-005: új termék (Festőszalag 50 m)
Új kategória: Villanyszerelés
VL-001: új termék (Hosszabbító 5 m)
VL-002: új termék (Elosztó 4-es)
12. sor hibás, kihagyva: KS-006;Fémfűrész
KS-004: készlet +2 → 12 db
--- Kész: 8 új termék, 2 bevételezés, 2 hibás sor.
```

## Tippek

- `File.ReadAllLines(fajl, Encoding.UTF8)` – `try-catch (IOException)`-nel.
- Gyors keresés cikkszámra: `Dictionary<string, Termek> termekek = db.Termekek.ToDictionary(t => t.Cikkszam);` – az új termékeket is tedd bele.
- Az új termék kategóriáját objektumként add meg (`Kategoria = kategoria`), ne `KategoriaId`-vel: az új kategóriának még nincs `Id`-je.
- A meglévő termék darabszámának módosítását az EF követi, a `SaveChanges` azt is menti.
- Ha a fájlt a projektbe teszed *Copy if newer* beállítással, a program mappájából is megnyitható.

Megoldás: [../../megoldasok/F01-csv-import/](../../megoldasok/F01-csv-import/)
