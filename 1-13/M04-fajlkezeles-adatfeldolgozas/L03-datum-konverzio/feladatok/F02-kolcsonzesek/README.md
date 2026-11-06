# 13-M04-L03-F02 · Kölcsönzések és késedelmi díjak

**Nehézség:** ★★☆ **Becsült idő:** 75 perc

## Feladat

Az iskolai könyvtár kölcsönzési naplója a [kolcsonzesek.csv](kolcsonzesek.csv) fájl:

```
Olvaso;Cim;Kolcsonzes;Visszahozas
Nagy Ákos;János vitéz;2026-02-26;2026-03-18
Kiss Olivér;A Pál utcai fiúk;2026-05-06;
```

Ha a `Visszahozas` mező üres, a könyv még kint van. A kölcsönzési idő **21 nap**. Késés esetén a díj **50 Ft naponta**, de egy kölcsönzésre legfeljebb **1500 Ft**. A vizsgálat napja (a „mai nap”) rögzített: **2026. május 15.**

Készíts `Kolcsonzesek` nevű .NET 8 konzolos alkalmazást.

1. **`Kolcsonzes`** osztály: `Olvaso`, `Cim`, `DateTime KolcsonzesDatum`, `DateTime? Visszahozas`; ez utóbbi `null`, ha a könyv még kint van. Számított tulajdonságok: `Hatarido` és `Kint`. Metódus: `int KesesNapok(DateTime ma)`, amely visszahozott könyvnél a visszahozás napjáig, kint lévőnél a mai napig számol, és 0, ha nincs késés. Valamint `int Dij(DateTime ma)`.
2. Olvasd be a kölcsönzéseket, és írd ki a számukat.
3. Hány könyv van még kint? Közülük melyeknek járt le a határideje (olvasó, cím, határidő, hány napja késik)?
4. Mennyi késedelmi díjat fizettek a már **visszahozott**, de késve visszahozott könyvek után összesen, és hány ilyen kölcsönzés volt?
5. Mennyi volt az átlagos kölcsönzési idő napokban a visszahozott könyveknél?
6. A hét melyik napján kölcsönöztek a legtöbbet? Írd ki mind a 7 nap darabszámát is.
7. Hónaponkénti bontás: hónap neve magyarul, és hány kölcsönzés kezdődött akkor.
8. Készíts **felszólító leveleket** a `felszolitasok.txt` fájlba a lejárt, még kint lévő kölcsönzésekhez:
   `Kedves Kiss Olivér! A(z) „Tüskevár” című könyv határideje 2026.04.28. volt. Eddigi késedelmi díj: 850 Ft.`

## Elvárt kimenet

```
2. Kölcsönzések száma: 36
3. Még kint: 7 könyv, ebből lejárt:
   Tóth Bálint        Állatfarm            határidő: 2026.04.22.  késés: 23 nap
   Tóth Bálint        Toldi                határidő: 2026.05.02.  késés: 13 nap
   Kiss Olivér        Tüskevár             határidő: 2026.03.08.  késés: 68 nap
   Tóth Bálint        János vitéz          határidő: 2026.02.23.  késés: 81 nap
4. Késve visszahozott: 10 kölcsönzés, befolyt díj: 4 550 Ft
5. Átlagos kölcsönzési idő: 19,2 nap
6. Kölcsönzések a hét napjai szerint:
   hétfő 6, kedd 3, szerda 8, csütörtök 5, péntek 4, szombat 8, vasárnap 2
   A legtöbb: szerda (8 db)
7. február: 17, március: 11, április: 4, május: 4
8. 4 felszólítás elkészült: felszolitasok.txt
```

Az első felszólítás:

```
Kedves Tóth Bálint! A(z) „Állatfarm” című könyv határideje 2026.04.22. volt. Eddigi késedelmi díj: 1 150 Ft.
```

## Tippek

- Az üres mező beolvasása: `Visszahozas = m[3] == "" ? null : DateTime.ParseExact(m[3], "yyyy-MM-dd", CultureInfo.InvariantCulture);`
- `DateTime?` értéknél a `HasValue` mondja meg, van-e érték, és a `.Value` adja vissza a dátumot. A `Kint => !Visszahozas.HasValue` egysoros.
- A hét napjainak megszámlálása egy `int[7]` tömbben: `db[(int)k.KolcsonzesDatum.DayOfWeek]++`. Kiírásnál hétfőtől érdemes kezdeni: az indexek sorrendje `1, 2, 3, 4, 5, 6, 0`.
- A díj felső korlátja: `Math.Min(KesesNapok(ma) * 50, 1500)`.
- Hónap neve magyarul: `new DateTime(2026, honap, 1).ToString("MMMM", new CultureInfo("hu-HU"))`.

Megoldás: [../../megoldasok/F02-kolcsonzesek/](../../megoldasok/F02-kolcsonzesek/)
