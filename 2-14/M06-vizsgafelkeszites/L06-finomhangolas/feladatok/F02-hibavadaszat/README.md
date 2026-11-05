# 14-M06-L06-F02 · Hibavadászat a hibakeresővel

**Nehézség:** ★★☆ **Becsült idő:** 40 perc

## Feladat

A [kiindulo/HibasElemek](kiindulo/HibasElemek/) projekt a 14-M06-L04 kémiaielem-adatait dolgozza fel. Lefordul, de futás közben egymás után több kivétellel leáll, és amikor már lefut, akkor is rossz eredményeket ad. **Nyolc** hibát rejtettünk el benne.

1. Futtasd Visual Studióban (F5). Amikor a program kivétellel megáll:
   - olvasd el a kivétel típusát és üzenetét;
   - nézd meg a *Locals* / *Autos* ablakban a változók értékét (pl. melyik sor feldolgozásánál állt meg?);
   - javítsd a hibát, és futtasd újra.
2. Ha már lefut, vesd össze az eredményt a lenti helyes kimenettel. A logikai hibákhoz tegyél töréspontot (F9), és lépkedj (F10/F11).
3. Minden hibáról írj egy sort: *tünet → ok → javítás*.

Próbabemenetek a 3. feladathoz: `halogén`, `Halogén`, `xyz`.

## Helyes kimenet

```
1. feladat: Elemek száma: 36
2. feladat: Legnehezebb: kripton (83,798)
3. feladat: Kategória: halogén
    Átlagos atomtömeg: 44,78
4. feladat: A gazok.txt elkészült.
```

A `gazok.txt` 9 sort tartalmaz (H, He, N, O, F, Ne, Cl, Ar, Kr).

## Tippek

- A kivétel ablakában a *View Details* megmutatja a teljes üzenetet; a *Call Stack* ablak, hogy melyik metódusból érkezett.
- Gyanús: minden `Parse`, minden `First()`/`Average()`, minden szöveg-összehasonlítás és minden fájlművelet.
- Nem minden hiba okoz kivételt: egy üres kimeneti fájl vagy egy „furcsa” érték is hiba.

Megoldás és a teljes hibalista: [../../megoldasok/F02-hibavadaszat/](../../megoldasok/F02-hibavadaszat/)
