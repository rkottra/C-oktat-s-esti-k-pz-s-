# 13-M03-L02-F02 · Ingatlaniroda

**Nehézség:** ★★☆ **Becsült idő:** 60 perc

## Feladat

Egy ingatlaniroda lakásokat és családi házakat hirdet. Az irányár mindegyiknél az alapterületből és a négyzetméterárból indul, de a típustól függően módosul. Készíts `Ingatlaniroda` nevű .NET 8 konzolos alkalmazást, minden osztályt külön fájlban.

1. **`Ingatlan`** (ős):
   - `Cim` (nem üres), `Alapterulet` (m², 10 és 1000 között), `NegyzetmeterAr` (Ft, pozitív) tulajdonság;
   - `public virtual long Iranyar()`: alapterület × négyzetméterár;
   - `public virtual string Tipus()`: `"ingatlan"`;
   - `ToString()`: típus, cím, alapterület és irányár egy sorban.
2. **`Lakas : Ingatlan`**: plusz `Emelet` (0 = földszint) és `VanLift`. Ha a lakás a 3. emeleten vagy feljebb van, és **nincs lift**, az irányár 10%-kal kevesebb. Típusa `"lakás"`.
3. **`Panellakas : Lakas`**: a lakás irányárához képest további 15% kedvezmény jár, mert felújítandó. Típusa `"panellakás"`. A számításhoz a `base.Iranyar()`-t használd, így a liftes szabály itt is érvényes marad.
4. **`CsaladiHaz : Ingatlan`**: plusz `TelekTerulet` (m²). Az irányárhoz hozzáadódik a telek ára, négyzetméterenként 18 000 Ft. Ha van `Garazs`, további 2 500 000 Ft. Típusa `"családi ház"`.
5. A főprogram:
   - töltsön fel egy `List<Ingatlan>` listát legalább 8 vegyes hirdetéssel;
   - írja ki a hirdetéseket **irányár szerint növekvő** sorrendben;
   - kérjen be egy keretösszeget (millió Ft), és sorolja fel a keretbe férő hirdetéseket;
   - írja ki **típusonként** (lakás, panellakás, családi ház) a hirdetések számát és átlagos négyzetméterárát. Itt az irányárat oszd el az alapterülettel; ez a módosítók miatt eltér a megadott négyzetméterártól;
   - írja ki a legnagyobb telkű családi ház címét.

## Minta kimenet (a megoldás adataival)

```
Hirdetések irányár szerint:
  panellakás   Debrecen, Sámsoni út 12.           54 m²      23 546 700 Ft
  panellakás   Miskolc, Győri kapu 20.            68 m²      24 276 000 Ft
  lakás        Szeged, Kárász u. 3.               62 m²      37 200 000 Ft
  lakás        Pécs, Király u. 9.                 75 m²      42 000 000 Ft
  lakás        Budapest, Bartók Béla út 41.       48 m²      47 520 000 Ft
  családi ház  Kecskemét, Mező u. 15.             95 m²      57 300 000 Ft
  családi ház  Zalaegerszeg, Domb u. 2.          140 m²      80 100 000 Ft
  családi ház  Gödöllő, Fenyves u. 8.            120 m²      94 900 000 Ft

Keretösszeg (millió Ft): 45
A keretbe fér: 4 hirdetés
  ...

Típusonként:
  lakás        3 db, átlag 716 667 Ft/m²
  panellakás   2 db, átlag 396 525 Ft/m²
  családi ház  3 db, átlag 655 378 Ft/m²

Legnagyobb telek: Zalaegerszeg, Domb u. 2. (1200 m²)
```

## Tippek

- A `Panellakas` közvetlen őse a `Lakas`, ezért a `base.Iranyar()` már a lakás szabályával számol. Ez **többszintű öröklődés**.
- A `Tipus()` metódust felülírva a kiírás és a típusonkénti statisztika is egyszerűbb: a `t.Tipus() == "lakás"` vizsgálat nem keveri össze a lakást a panellakással. Az `is Lakas` vizsgálat ugyanis a panellakásokra is igaz!
- Rendezés: `lista.Sort((a, b) => a.Iranyar().CompareTo(b.Iranyar()));`
- A nagy összegek miatt használj `long` típust.

Megoldás: [../../megoldasok/F02-ingatlaniroda/](../../megoldasok/F02-ingatlaniroda/)
