# 13-M04-L04-F02 · Parkolóház díjai

**Nehézség:** ★★★ **Becsült idő:** 120 perc (vizsgaszerű feladat)

## Feladat

Egy parkolóház sorompórendszere a [parkolas.csv](parkolas.csv) fájlba naplózta 2026. május 4–8. (hétfő–péntek) forgalmát. Minden sor egy parkolás, a behajtás ideje szerint rendezve:

```
Rendszam;Be;Ki;Berlet
CJ VR-925;2026-05-04 07:08;2026-05-04 07:17;nem
```

A `Be` és a `Ki` formátuma `yyyy-MM-dd HH:mm`. A `Berlet` értéke `igen` vagy `nem`.

**Díjszabás:**

- bérletes autó: **ingyenes**;
- legfeljebb 30 perc: **ingyenes**;
- 30 perc felett minden **megkezdett óra 400 Ft**, de egy 24 órás időszakra legfeljebb **4000 Ft**. Ha az autó 24 óránál tovább áll, minden teljes 24 óra 4000 Ft, a maradék idő pedig a fenti szabály szerint számít, legfeljebb 4000 Ft-tal.

Készíts `Parkolohaz` nevű .NET 8 konzolos alkalmazást. Minden kiírás előtt jelenjen meg a feladat sorszáma.

1. Készíts `Parkolas` osztályt (`Rendszam`, `DateTime Be`, `DateTime Ki`, `bool Berlet`), `Idotartam` (`TimeSpan`) és `Dij` számított tulajdonsággal. Olvasd be az adatokat.
2. Hány parkolás történt, és hány **különböző** autó parkolt?
3. Mennyi volt a teljes bevétel, és hány parkolás volt ingyenes (bérlet vagy legfeljebb 30 perc)?
4. Melyik volt a leghosszabb parkolás? Rendszám, behajtás, időtartam `óó:pp` formában, díj.
5. **Rendszámok:** a régi formátum `ABC-123` (3 betű, kötőjel, 3 számjegy), az új `AB CD-123` (2 betű, szóköz, 2 betű, kötőjel, 3 számjegy). Hány régi és hány új formátumú rendszám szerepel a **különböző** autók között? Ha valamelyik egyikbe sem illik, írd ki.
6. Kérj be egy rendszámot. Írd ki az összes parkolását (behajtás, időtartam, díj) és a fizetett összeget, vagy azt, hogy ez az autó nem parkolt itt.
7. Készíts napi kimutatást a `napi.txt` fájlba: dátum, a hét napja magyarul, a parkolások száma és a bevétel (a behajtás napja szerint): `2026.05.04.;hétfő;26;41200`.
8. **Telítettség:** kérj be egy napot (`ÉÉÉÉ.HH.NN.`), és add meg a nap minden egész órájára 7:00 és 20:00 között, hány autó tartózkodott bent abban a pillanatban (`Be <= időpont < Ki`). Melyik órában volt a legtöbb? Rajzold ki `#` jelekkel is.

## Elvárt kimenet

```
2. feladat: Parkolások: 130, különböző autók: 114
3. feladat: Bevétel: 168 000 Ft, ingyenes parkolás: 33
4. feladat: Leghosszabb: PR ST-789, behajtás 2026.05.06. 16:31, 26:15, díj: 5 200 Ft
5. feladat: Régi formátum: 53, új formátum: 61
6. feladat: Rendszám: pr st-789
   2026.05.04. 13:06   3:44     1 600 Ft
   2026.05.05. 15:54   0:20         0 Ft
   2026.05.06. 16:31  26:15     5 200 Ft
   2026.05.07. 15:29   0:52       400 Ft
   2026.05.08. 16:29   2:12     1 200 Ft
   Összesen 5 parkolás, fizetett: 8 400 Ft
7. feladat: napi.txt elkészült (5 nap)
8. feladat: Nap (ÉÉÉÉ.HH.NN.): 2026.05.06.
   07:00   1 #
   08:00   2 ##
   09:00   3 ###
   ...
   14:00   7 #######
   ...
   20:00   4 ####
   A legtöbb autó 14:00-kor volt bent (7 db).
```

A `napi.txt` első sora: `2026.05.04.;hétfő;26;41200`. A 26:15-ös parkolás díja: 1 teljes nap (4000 Ft) + 2 óra 15 perc, azaz 3 megkezdett óra (1200 Ft) = 5200 Ft.

## Tippek

- A dátum és az idő együtt: `DateTime.ParseExact(m[1], "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture)`.
- A díj képlete percekkel, egész aritmetikával a legegyszerűbb:
  `napok = perc / 1440; maradek = perc % 1440; dij = napok * 4000 + Math.Min(MegkezdettOrak(maradek) * 400, 4000)`. A megkezdett órák: `(maradek + 59) / 60`.
- A rendszám ellenőrzését írd két `static bool` metódusba (`RegiFormatum`, `UjFormatum`), karakterenként vizsgálva: `char.IsLetter`, `char.IsDigit`, a kötőjel és a szóköz helye. A hossz is számít!
- Egy időpont: `nap.AddHours(ora)`. A „bent volt-e” feltétel: `p.Be <= idopont && idopont < p.Ki`. A két napon át tartó parkolásra is gondolj!

Megoldás: [../../megoldasok/F02-parkolohaz/](../../megoldasok/F02-parkolohaz/)
