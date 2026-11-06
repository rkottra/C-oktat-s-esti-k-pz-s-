# 13-M03-L01-F02 · Időtartam-átváltó

**Nehézség:** ★★☆ **Becsült idő:** 45 perc

## Feladat

Egy futó edzésnaplójához kell olyan osztály, amely egy időtartamot másodpercben tárol, de percben és órában is lehet írni és olvasni. Készíts .NET 8 konzolos alkalmazást `Idotartamok` néven.

1. **`Idotartam`** osztály (külön fájlban):
   - `Masodpercek` (egész) – ellenőrzött tulajdonság privát mezővel; negatív értékre `ArgumentException`. **Ez az egyetlen tárolt adat.**
   - `Percek` és `Orak` (tört szám) – számított tulajdonságok **`get`-tel és `set`-tel**: olvasáskor átszámolnak, íráskor visszaszámolnak másodpercre (kerekítve: `(int)Math.Round(...)`);
   - `Ora`, `Perc`, `Mp` (egész) – csak olvasható, számított tulajdonságok: a „óra : perc : másodperc” alak részei (pl. 3725 mp → 1 óra, 2 perc, 5 mp);
   - két konstruktor: `(masodpercek)` és `(ora, perc, mp)` – a második a `this(...)` segítségével hívja az elsőt;
   - `void Hozzaad(Idotartam masik)` – hozzáadja a másik időtartamot;
   - `ToString()`: `óó:pp:mm` formában, pl. `01:02:05` (`{Ora:D2}`).
2. A főprogram:
   - hozzon létre egy 5400 másodperces időtartamot, írja ki percben és órában, majd állítsa át `Percek = 45`-re, és írja ki újra;
   - tárolja egy `List<Idotartam>` listában a heti futóedzések hosszát (legalább 5 edzés, a konstruktorokat vegyesen használva);
   - írja ki az edzéseket, a heti összidőt (egy új, 0 hosszú `Idotartam`-hoz adja hozzá az összeset), a leghosszabb edzést és az átlagos edzéshosszt percben;
   - kérjen be a felhasználótól egy edzéshosszt óra, perc, másodperc formában (három szám), vegye fel a listába, és írja ki az új összidőt. Hibás bemenetnél írjon érthető üzenetet.

## Minta kimenet

```
5400 mp = 90 perc = 1,5 óra (01:30:00)
Percek = 45 után: 00:45:00 (2700 mp)

Heti edzések:
  1. 00:42:10
  2. 01:05:00
  ...
Heti összidő: 04:37:55
Leghosszabb edzés: 01:25:30
Átlagos edzés: 55,6 perc

Új edzés – óra: 0
perc: 38
másodperc: 20
Új heti összidő: 05:16:15
```

## Tippek

- Egész osztás és maradék: `Ora = Masodpercek / 3600`, `Perc = Masodpercek % 3600 / 60`, `Mp = Masodpercek % 60`.
- A `Percek` `set`-je ne a mezőt írja, hanem a `Masodpercek` tulajdonságot – így az ellenőrzés itt is lefut.
- `Percek` olvasásakor figyelj, hogy ne egész osztás legyen: `Masodpercek / 60.0`.
- Az összidőhöz: `var ossz = new Idotartam(0); foreach (...) ossz.Hozzaad(e);`

Megoldás: [../../megoldasok/F02-idotartam/](../../megoldasok/F02-idotartam/)
