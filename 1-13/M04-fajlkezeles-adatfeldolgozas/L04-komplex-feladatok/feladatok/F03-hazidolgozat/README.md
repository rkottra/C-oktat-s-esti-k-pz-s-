# 13-M04-L04-F03 · Házidolgozat

**Nehézség:** ★★★ **Időkeret:** a kiadástól számított 3 hét (a pontos határidő a Classroomban)

## A feladat

Készíts egy önállóan választott adathalmazra **menüs, fájlkezelő konzolos alkalmazást** C#-ban (.NET 8), amely bemutatja a félév során tanultakat: osztályok, listák, programozási tételek, fájlkezelés, dátumkezelés, hibakezelés.

A dolgozat **egyéni munka**. Segítséget kérni szabad (tanártól, társtól, dokumentációból, MI-asszisztenstől), de a beadott kódot sorról sorra érteni és magyarázni kell tudnod. A védésen erre kérdezünk rá.

## Követelmények

### 1. Az adatfájl

- Saját készítésű vagy nyílt forrásból származó CSV-fájl, **legalább 50 rekorddal** és **legalább 5 mezővel**.
- A mezők között legyen legalább egy **szöveg**, egy **egész**, egy **valós szám** és egy **dátum** (vagy időpont).
- Az első sor fejléc. A fájl UTF-8 kódolású, az elválasztó pontosvessző.
- Ha nyílt forrásból dolgozol, a README-ben add meg a forrást.

### 2. A program

| # | Követelmény |
|---|---|
| 1 | Legalább egy **adatosztály** tulajdonságokkal, CSV-sorból építő konstruktorral, legalább **két számított tulajdonsággal** és `ToString()`-gel |
| 2 | Beolvasás `List<T>`-be, a hiányzó fájl és a hibás sor kezelése (`try`–`catch`, a hibás sor kihagyása figyelmeztetéssel) |
| 3 | **Menü**, amely a kilépésig fut; minden menüpont külön metódus |
| 4 | Legalább **8 lekérdezés** a menüben, amelyek között szerepel: összegzés vagy átlag, megszámlálás, maximum- vagy minimumkiválasztás, keresés vagy eldöntés, kiválogatás, csoportosítás (kategóriánként / havonta), rendezés, és legalább egy **dátumokkal** dolgozó kérdés |
| 5 | Legalább egy lekérdezés a felhasználótól **bekért** értékkel dolgozik, ellenőrzött beolvasással (`TryParse`) |
| 6 | **Új rekord felvétele** a menüből, ellenőrzéssel, és az adatok **visszamentése** a fájlba |
| 7 | Legalább egy **kimeneti fájl** (jelentés, kimutatás, toplista) |
| 8 | A programozási tételeket **saját ciklussal** írd meg; LINQ legfeljebb ellenőrzésre szerepelhet, megjegyzésben |

### 3. Dokumentáció

A projekt mappájában egy `README.md`:

- a téma és az adatforrás rövid leírása, a mezők jelentése;
- a menüpontok listája, mindegyiknél egy mondat arról, mit csinál és melyik tételt használja;
- egy képernyőkép vagy szöveges mintafuttatás;
- az ismert hibák, hiányosságok őszinte felsorolása (ezért nem jár pontlevonás).

### 4. Beadás

- A teljes projektmappa (a `bin` és az `obj` nélkül) tömörítve, `Vezeteknev_Keresztnev_hazidolgozat.zip` néven, a Classroomban.
- A program a Visual Studióban megnyitva, F5-re hiba nélkül induljon, és az adatfájl a futtatási könyvtárba másolódjon.

## Értékelés (100 pont)

| Szempont | Pont |
|---|---|
| Adatfájl (méret, mezőtípusok, igényesség) | 10 |
| Adatosztály, beolvasás, hibakezelés (1–2.) | 15 |
| Menü és szerkezet: metódusok, beszédes nevek, olvasható kód (3.) | 15 |
| Lekérdezések helyessége és változatossága (4–5.) | 30 |
| Új rekord és mentés (6.) | 10 |
| Kimeneti fájl (7.) | 5 |
| Dokumentáció | 5 |
| **Védés**: a kód magyarázata, egy kisebb módosítás élőben | 10 |

**Jegyek:** 0–39: elégtelen (1) · 40–54: elégséges (2) · 55–69: közepes (3) · 70–84: jó (4) · 85–100: jeles (5)

## Témaötletek

Bármilyen téma választható, ami érdekel. Néhány ötlet:

- **Sport:** egy bajnokság mérkőzései, futóverseny eredményei, edzésnapló
- **Zene, film, játék:** saját lejátszási lista (hossz, megjelenés, értékelés), filmadatbázis, játékmentések statisztikája
- **Közlekedés:** buszmenetrend és késések, saját autó tankolási naplója (dátum, liter, ár, km-óra)
- **Időjárás:** egy város napi adatai (dátum, min/max hőmérséklet, csapadék)
- **Pénzügy:** háztartási kiadások kategóriákkal, zsebpénz-napló
- **Iskola:** könyvtári állomány, versenyeredmények, büfé eladásai

Nyílt adatforrásnak jó például a KSH, az OMSZ vagy a data.gov.hu adatkészlete, de a saját magad által összeállított, valószerű adat is teljesen megfelel.

## Mire figyelj?

- Kezdd az **adatfájllal és az osztállyal**, és csak akkor írd a menüt, ha a beolvasás biztosan működik.
- Minden menüpontot próbálj ki **szélsőséges esetekre** is: nincs találat, üres lista, hibás bemenet.
- Commitolj vagy ments gyakran. A határidő előtti utolsó estén nem szabad az első futtatásnak lennie.

> A házidolgozathoz szándékosan **nincs** közzétett megoldás. Mintának a [Sportesemények](../F01-sportesemenyek/) és a [Parkolóház](../F02-parkolohaz/) feladat megoldásai, valamint a 13-M04-L02 lecke menüs példái használhatók.
