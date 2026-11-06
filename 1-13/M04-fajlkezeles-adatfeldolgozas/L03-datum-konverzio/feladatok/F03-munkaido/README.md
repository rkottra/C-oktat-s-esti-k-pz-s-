# 13-M04-L03-F03 · Munkaidő-nyilvántartás

**Nehézség:** ★★★ **Becsült idő:** 90 perc

## Feladat

Egy kis cég beléptetőrendszere a [munkaido.csv](munkaido.csv) fájlba naplózza, ki mikor érkezett és mikor távozott 2026 áprilisában:

```
Dolgozo;Datum;Be;Ki
Antal Gábor;2026-04-01;08:01;16:05
```

A cég szabályai:

- **rugalmas kezdés:** legkésőbb 9:00-kor be kell érni, ennél később **késés**;
- **ebédszünet:** ha a bent töltött idő több mint 6 óra, 30 perc ebédszünet levonandó;
- **ledolgozott idő** = távozás − érkezés − (esetleges ebédszünet);
- **túlóra:** a napi 8 órát meghaladó ledolgozott idő;
- **hétvégi munka:** szombaton vagy vasárnap ledolgozott nap.

Készíts `Munkaido` nevű .NET 8 konzolos alkalmazást.

1. **`Jelenlet`** osztály: `Dolgozo`, `DateTime Datum`, `TimeSpan Be`, `TimeSpan Ki`. Számított tulajdonságok: `BentToltott`, `Ledolgozott`, `Tulora`, `Kesett`, `Hetvege`.
2. Hány bejegyzés van, és hány különböző dolgozó?
3. Dolgozónként egy **táblázat**: jelenléti napok, ledolgozott idő (`óó:pp`, például `163:40`), túlóra (`óó:pp`), késések száma.
4. Ki érkezett a legkorábban, és ki távozott a legkésőbb a hónapban (név, dátum, időpont)?
5. Melyik volt a leghosszabb ledolgozott munkanap?
6. Sorold fel a hétvégi munkanapokat (név, dátum a hét napjával, ledolgozott idő).
7. **Hiányzások:** munkanapnak tekintjük azokat a hétköznapokat, amelyeken legalább egy dolgozónak van bejegyzése. Dolgozónként hány munkanapon hiányzott, és melyik napokon?
8. Írd a `jelentes.txt` fájlba dolgozónként a nevet, a ledolgozott órákat és a túlórákat **tizedes órában**, két tizedesre, tizedes**ponttal**, valamint a késések számát: `Antal Gábor;156.18;8.08;0`.

## Elvárt kimenet

```
2. Bejegyzések: 79, dolgozók: 4
3. Dolgozó          Napok  Ledolgozott   Túlóra  Késés
   Antal Gábor         19       156:11     8:05      0
   Bodnár Kata         20       156:42     3:21      0
   Cseh Ferenc         21       171:47     9:22      6
   Dudás Eszter        19       148:56     6:45      0
4. Legkorábban: Bodnár Kata, 2026.04.13. 07:33;  legkésőbb: Cseh Ferenc, 2026.04.07. 19:31
5. Leghosszabb munkanap: Dudás Eszter, 2026.04.17., 07:42–17:40 (9:28)
6. Hétvégi munka:
   Dudás Eszter, 2026.04.25. szombat, 4:41
7. Hiányzások (21 munkanapból):
   Antal Gábor     2 nap: 04.09., 04.22.
   Bodnár Kata     1 nap: 04.22.
   Cseh Ferenc     0 nap
   Dudás Eszter    3 nap: 04.13., 04.16., 04.23.
8. A jelentés elkészült: jelentes.txt
```

## Tippek

- Időpont beolvasása: `TimeSpan.ParseExact("08:01", @"hh\:mm", CultureInfo.InvariantCulture)`.
- A `TimeSpan`-okat össze lehet adni: `osszes += j.Ledolgozott;` (kezdőérték: `TimeSpan.Zero`).
- 24 óránál hosszabb időtartam kiírása: a `hh` formátum csak a napon belüli órát mutatja, ezért írd ki így: `$"{(int)t.TotalHours}:{t.Minutes:00}"`.
- A túlóra csak pozitív lehet: `Ledolgozott > nyolcOra ? Ledolgozott - nyolcOra : TimeSpan.Zero`.
- A munkanapok listája (7. pont): gyűjtsd ki a hétköznapi bejegyzések különböző dátumait egy `List<DateTime>`-ba, majd minden dolgozónál vizsgáld, van-e bejegyzése azon a napon (eldöntés).
- Fájlba írásnál a tizedespont: `t.TotalHours.ToString("F2", CultureInfo.InvariantCulture)`.

Megoldás: [../../megoldasok/F03-munkaido/](../../megoldasok/F03-munkaido/)
