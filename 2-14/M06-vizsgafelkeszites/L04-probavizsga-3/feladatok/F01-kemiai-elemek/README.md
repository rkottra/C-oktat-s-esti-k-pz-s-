# 14-M06-L04-F01 · 3. próbavizsga: Kémiai elemek (vizsgaszimuláció)

**Nehézség:** ★★★ **Időkeret:** 150 perc, szigorúan **Elérhető pontszám:** 100 pont (konzol 40 + grafikus 60)

## Vizsgaszabályok

- Az időkeret lejártakor a munkát be kell fejezni; a leadott állapot számít.
- Használható: Visual Studio, a gépen lévő súgó, a saját, előre elkészített kódsablonjaid (14-M06-L06). **Nem** használható: internet, mesterséges intelligencia, más tanuló segítsége, korábbi megoldások.
- Leadás: a két projekt mappája (`bin` és `obj` nélkül) a tanár által megadott helyre.
- Csak forduló projekt értékelhető. Nem forduló részt kommenteld ki leadás előtt!
- Minden képernyőre írt eredmény előtt a feladat sorszáma; a kiírások kövessék a mintát; ékezet nélküli azonosítók elfogadottak; a mintában szereplő értékek kódba írása pontlevonással jár.

## Az adatok

A periódusos rendszer első négy periódusának 36 eleme (nyilvános adatok: IUPAC szerinti standard atomtömeg kerekítve, Pauling-féle elektronegativitás, halmazállapot szobahőmérsékleten).

| Állomány | Tartalom |
|---|---|
| [elemek.csv](elemek.csv) | `;`-vel tagolt, UTF-8, fejléccel, tizedes **pont**; a hiányzó elektronegativitás **üres mező** |
| [elemek.json](elemek.json) | ugyanaz JSON-ben; a hiányzó érték `null` |
| [elemek.sql](elemek.sql) | `estis_elemek` adatbázis: `kategoria` (8 sor), `elem` (36 sor) |
| [kiindulo/ElemekWpf](kiindulo/ElemekWpf/) | üres WPF-projekt (modellosztályok nélkül!) |

```
rendszam;vegyjel;nev;kategoria;periodus;csoport;atomtomeg;elektronegativitas;halmazallapot
1;H;hidrogén;nemfém;1;1;1.008;2.20;gáz
2;He;hélium;nemesgáz;1;18;4.003;;gáz
```

Az `elem` tábla: `rendszam` (INT, elsődleges kulcs, **nem** AUTO_INCREMENT), `vegyjel` (egyedi), `nev`, `kategoriaid` (→ `kategoria.id`), `periodus`, `csoport`, `atomtomeg` (DOUBLE), `elektronegativitas` (DOUBLE, NULL lehet), `halmazallapot` (`szilárd` / `folyékony` / `gáz`).

## I. Konzolos rész – `ElemekKonzol` (40 pont)

1. **(2 pont)** Konzolos projekt; a választott adatfájl a program mellé másolódik.
2. **(6 pont)** `Elem` osztály a fájl mezőinek megfelelő, magyar nevű tulajdonságokkal és helyes típusokkal (az elektronegativitás hiányozhat!). Statikus `BetoltCsv` vagy `BetoltJson` metódus, amely `List<Elem>`-et ad vissza.
3. **(2 pont)** Írd ki az elemek számát.
4. **(4 pont)** Írd ki a szobahőmérsékleten gáz halmazállapotú elemek számát és vegyjelüket vesszővel elválasztva, rendszám szerinti sorrendben.
5. **(5 pont)** Írd ki a legnagyobb elektronegativitású elem nevét, vegyjelét és az értéket két tizedesjeggyel; külön sorban azt is, hány elemnél hiányzik az érték.
6. **(6 pont)** Kategóriánként (ábécérendben) írd ki az elemek számát és átlagos atomtömegét két tizedesjeggyel.
7. **(6 pont)** Kérj be egy vegyjelet; a kis- és nagybetű ne számítson (`fe`, `FE`, `Fe` egyaránt a vas). Ha a bemenet nem 1–2 betűből áll, írd ki: `Érvénytelen vegyjel.`; ha nincs ilyen elem: `Nincs ilyen vegyjelű elem.`; egyébként a rendszámot, a nevet, a kategóriát, a periódust és a csoportot a minta szerint.
8. **(7 pont)** Készítsd el a `periodusok.txt` állományt: periódusonként egy sor a periódus számával, az elemek számával és a vegyjelekkel szóközzel elválasztva, rendszám szerint; az utolsó sor: `Összesen: N elem`. Sikeres írásnál üzenet; hibánál a program ne álljon le, hanem írja ki: `Hiba a fájl írásakor!`
9. **(2 pont)** Kódminőség: beszédes azonosítók, nincs ismétlődő vagy kikommentezett „szemét”, a feladatok sorrendben követhetők.

### Minta (konzol)

```
3. feladat: Elemek száma: 36
4. feladat: Gázhalmazállapotú elemek (9 db): H, He, N, O, F, Ne, Cl, Ar, Kr
5. feladat:
    Legnagyobb elektronegativitás: fluor (F): 3,98
    Elektronegativitás nélkül: 3 elem
6. feladat: Kategóriák:
    alkálifém       3 elem, átlagos atomtömeg: 23,01
    alkáliföldfém   3 elem, átlagos atomtömeg: 24,47
    átmenetifém    10 elem, átlagos atomtömeg: 55,31
    ...
7. feladat: Adjon meg egy vegyjelet: fe
    26. vas – átmenetifém, 4. periódus, 8. csoport
8. feladat: A periodusok.txt elkészült.
```

`periodusok.txt`:

```
1. periódus (2 elem): H He
2. periódus (8 elem): Li Be B C N O F Ne
3. periódus (8 elem): Na Mg Al Si P S Cl Ar
4. periódus (18 elem): K Ca Sc Ti V Cr Mn Fe Co Ni Cu Zn Ga Ge As Se Br Kr
Összesen: 36 elem
```

## II. Grafikus rész – `ElemekWpf` (60 pont)

1. **(3 pont)** Importáld az `elemek.sql` szkriptet.
2. **(9 pont)** EF Core: csomagok (8.0.x); `Kategoria` és `Elem` entitás a táblákhoz kötve, idegen kulccsal és navigációs tulajdonsággal; a rendszám kulcs, amelyet **nem** az adatbázis generál; `ElemekContext`.
3. **(10 pont)** Főablak `Periódusos rendszer – kémiai elemek` címmel, reszponzívan:
   - felül szűrősáv: *Kategória* legördülő lista (első eleme `(mind)`, utána a kategóriák nevei ábécérendben) és *Halmazállapot* legördülő lista (`(mind)`, `szilárd`, `folyékony`, `gáz`);
   - középen nem szerkeszthető táblázat: *Rendszám, Vegyjel, Név, Kategória* (név!), *Periódus, Csoport, Atomtömeg* (3 tizedes), *Elektronegativitás* (2 tizedes, hiányzó érték esetén üres cella), *Halmazállapot*;
   - alul *Új elem…* és *Kijelölt törlése* gomb, jobb oldalon `Megjelenítve: x / y elem` felirat.
4. **(8 pont)** Induláskor minden elem megjelenik rendszám szerint. A két szűrő együtt is működik; a felirat mindig a megjelenített és az összes elem számát mutatja.
5. **(8 pont)** Az *Új elem…* gomb modális `Új elem` ablakot nyit a kilenc mezővel (a kategória és a halmazállapot legördülő lista), *Mentés* és *Mégse* gombbal, üres kezdőállapottal.
6. **(14 pont)** Mentés előtti ellenőrzés, hibánként külön üzenettel; hiba esetén az ablak nyitva marad:
   - a rendszám pozitív egész, és még nem szerepel az adatbázisban *(3)*;
   - a vegyjel 1–2 betű, nagy kezdőbetű, a második betű kicsi, és még nem foglalt *(3)*;
   - a név nem üres *(1)*, a kategória ki van választva *(1)*;
   - a periódus 1–7, a csoport 1–18 közötti egész *(2)*;
   - az atomtömeg pozitív szám (tizedesvessző és -pont is jó) *(1)*;
   - az elektronegativitás üresen hagyható, de ha meg van adva, 0 és 4 közötti szám *(2)*;
   - a halmazállapot ki van választva *(1)*.
7. **(6 pont)** Sikeres ellenőrzés után az új elem az adatbázisba kerül, és a táblázat frissül *(3)*. A *Kijelölt törlése* kijelölés nélkül figyelmeztet, egyébként megerősítés után töröl és frissít *(3)*.
8. **(2 pont)** Kódminőség (mint a konzolos részben).

Próbaadat az új elemhez: `37; Rb; rubídium; alkálifém; 5; 1; 85,468; 0,82; szilárd`.

Önjavítás: [F02](../F02-onjavitas/) · Megoldás és javítókulcs: [../../megoldasok/F01-kemiai-elemek/](../../megoldasok/F01-kemiai-elemek/)
