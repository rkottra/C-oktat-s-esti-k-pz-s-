# 13-M04-L04-F01 · Sportesemények

**Nehézség:** ★★☆ **Becsült idő:** 90 perc (vizsgaszerű feladat)

## Feladat

Egy városi sportcsarnok és a mellette lévő pálya 2026 tavaszi mérkőzéseit a [sportesemenyek.csv](sportesemenyek.csv) fájl tartalmazza. A csapatok kitaláltak. Az első sor fejléc, az adatok pontosvesszővel tagoltak:

```
Datum;Kezdes;Sportag;Hazai;Vendeg;HazaiPont;VendegPont;Nezok;Jegyar
2026-03-03;18:00;labdarúgás;Kék Sasok;Zöld Farkasok;1;2;965;1000
```

| Mező | Jelentés |
|---|---|
| `Datum`, `Kezdes` | a mérkőzés napja (`yyyy-MM-dd`) és kezdési ideje (`HH:mm`) |
| `Sportag` | `labdarúgás`, `kosárlabda` vagy `kézilabda` |
| `Hazai`, `Vendeg` | a két csapat neve |
| `HazaiPont`, `VendegPont` | a végeredmény (gól, illetve pont) |
| `Nezok` | a nézők száma |
| `Jegyar` | a belépőjegy ára forintban (0 = ingyenes mérkőzés) |

Készíts `Sportesemenyek` nevű .NET 8 konzolos alkalmazást. Minden feladat kiírása előtt jelenjen meg a feladat sorszáma (`3. feladat:`). A tételeket saját ciklussal valósítsd meg.

1. Készíts `Merkozes` osztályt, és olvasd be az adatokat egy listába. A dátum `DateTime`, a kezdés `TimeSpan` típusú legyen.
2. Hány mérkőzést rendeztek összesen, és ebből hányat sportáganként?
3. Hány néző látogatott ki összesen a mérkőzésekre, és mennyi volt a teljes jegybevétel (nézők × jegyár)?
4. Volt-e ingyenes mérkőzés? Ha igen, írd ki az elsőt: dátum, sportág, a két csapat.
5. Melyik mérkőzésen volt a legtöbb néző? Írd ki a dátumát a hét napjával együtt, a két csapatot és az eredményt.
6. Hány mérkőzés végződött döntetlenre? Sportáganként add meg.
7. Kérj be egy csapatnevet, és írd ki a **mérlegét**: lejátszott mérkőzések, győzelmek, döntetlenek, vereségek, szerzett és kapott pontok. Ha nincs ilyen csapat, írd ki: `Nincs ilyen csapat.`
8. Mennyi volt az átlagos nézőszám a hétvégi és a hétköznapi mérkőzéseken (egy tizedesre)?
9. Hónaponként mennyi volt a jegybevétel? A hónap nevét magyarul írd ki.
10. Készítsd el a **labdarúgó-tabellát** a `tabella.txt` fájlba. A győzelem 3, a döntetlen 1 pontot ér. A sorrendet a pontszám dönti el, egyenlőség esetén a gólkülönbség. Egy sor: `helyezés. csapat;mérkőzés;győzelem;döntetlen;vereség;lőtt:kapott;pont`.

## Elvárt kimenet

```
2. feladat: Mérkőzések száma: 60
   labdarúgás: 15, kézilabda: 24, kosárlabda: 21
3. feladat: Nézők összesen: 30 240 fő, jegybevétel: 32 825 700 Ft
4. feladat: Volt ingyenes mérkőzés: 2026.04.25., labdarúgás, Kék Sasok – Sárga Darazsak
5. feladat: A legtöbb néző (1178 fő): 2026.05.24. vasárnap, labdarúgás, Vörös Bikák – Sárga Darazsak 3:3
6. feladat: Döntetlenek: 6 (labdarúgás: 5, kézilabda: 1, kosárlabda: 0)
7. feladat: Csapat neve: medvék
   Medvék: 13 mérkőzés, 2 győzelem, 0 döntetlen, 11 vereség, pontok: 369:420
8. feladat: Átlagos nézőszám hétvégén: 581,5, hétköznap: 473,3
9. feladat: Havi jegybevétel:
   március     13 027 200 Ft
   április      9 972 900 Ft
   május        9 825 600 Ft
10. feladat: A tabella elkészült: tabella.txt – az első helyezett: Zöld Farkasok (13 pont)
```

A `tabella.txt`:

```
1. Zöld Farkasok;7;4;1;2;18:19;13
2. Sárga Darazsak;10;3;2;5;23:26;11
3. Kék Sasok;8;2;3;3;17:14;9
4. Vörös Bikák;5;1;4;0;13:12;7
```

## Tippek

- Hasznos számított tulajdonságok: `Bevetel` (`long`!), `Dontetlen`, `Hetvege`, valamint egy `bool Resztvevo(string csapat)` metódus.
- A 7. feladatnál egy mérkőzésen a csapat lehet hazai **vagy** vendég. Erre írj két ágat, vagy egy segédmetódust, amely visszaadja a csapat saját és az ellenfél pontjait.
- A 10. feladathoz készíts egy kis `CsapatEredmeny` osztályt (név, mérkőzés, győzelem, …, és a számított `Pont`, `Golkulonbseg`). Minden labdarúgó-mérkőzésnél keresd meg mindkét csapat eredményobjektumát, vagy hozd létre, ha még nincs, és frissítsd.
- Két szempontú rendezés: `t.Sort((a, b) => a.Pont != b.Pont ? b.Pont.CompareTo(a.Pont) : b.Golkulonbseg.CompareTo(a.Golkulonbseg));`

Megoldás: [../../megoldasok/F01-sportesemenyek/](../../megoldasok/F01-sportesemenyek/)
