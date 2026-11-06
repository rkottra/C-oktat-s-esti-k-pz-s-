---
id: 13-M04-L04
cim: Komplex feladatok, házidolgozat
evfolyam: 1-13
modul: M04 – Fájlkezelés és adatfeldolgozás
orak: 49–52.
het:
oraszam: 4
elofeltetel: 13-M04-L03
leiras: Önálló, vizsgaszerű konzolos feladatok fájlból olvasott adatokon (Sportesemények, Autós díjak), házidolgozat kiadása.
---

# 13-M04-L04 · Komplex feladatok, házidolgozat

## Célok

A lecke végére a tanuló:

- egy hosszabb, vizsgaszerű feladatleírást önállóan részfeladatokra bont, és lépésenként megold;
- a modul eddigi eszközeit együtt használja: osztály CSV-sorból, beolvasás listába, programozási tételek, dátum- és időkezelés, fájlba írás;
- a kiírást a feladat által előírt formában (sorszámozva, pontos szövegezéssel) készíti el, és a programját a mintakimenettel ellenőrzi;
- ismeri a házidolgozat követelményeit és értékelési szempontjait.

## Elmélet

### Hogyan oldjunk meg egy vizsgaszerű feladatot?

A szoftverfejlesztő vizsga és a programozás érettségi konzolos feladatai mindig ugyanarra a mintára épülnek: **egy adatfájl és 6–10 egymásra épülő kérdés**. A biztos megoldás menete:

1. **Nézd meg az adatfájlt** (Jegyzettömbben vagy Visual Studióban). Van-e fejléc? Mi az elválasztó? Milyen formátumú a dátum, az idő, a tizedesjel? Vannak-e üres mezők?
2. **Olvasd végig az összes kérdést**, mielőtt kódolni kezdesz. Így látod, milyen tulajdonságokra és számított értékekre lesz szükség.
3. **Írd meg az osztályt**: tulajdonságok a fájl oszlopai szerint, konstruktor CSV-sorból, és azok a számított tulajdonságok, amelyek több kérdésben is kellenek (például `Gyoztes`, `Bevetel`, `Idotartam`).
4. **Beolvasás és ellenőrzés**: az első kérdés szinte mindig a rekordok száma. Ha ez stimmel, a beolvasás jó.
5. **Kérdésenként**: melyik tétel kell hozzá? Írd ki a kérdés sorszámát (`3. feladat:`), és pontosan azt, amit a feladat kér. A felesleges kiírás zavaró, a hiányzó adat pontlevonás.
6. **Ellenőrizz** a mintakimenettel, és gondolj a szélsőséges esetekre: nincs találat, egyenlőség, üres mező.
7. **Mentsd** a projektet a megadott néven és helyre, és ellenőrizd, hogy az adatfájl a futtatási könyvtárba másolódik-e.

### Gyakori kérdéstípusok és tételeik

| A kérdés | Tétel / eszköz |
|---|---|
| „Hány … van az állományban?” | `lista.Count` |
| „Hány … volt, amelyik …?” | megszámlálás |
| „Mennyi az összes / átlagos …?” | összegzés (+ osztás) |
| „Melyik a legtöbb / legnagyobb …?” | maximumkiválasztás (indexszel vagy objektummal) |
| „Volt-e …? Ha igen, az első …” | keresés |
| „Kérjen be egy …-t, és írja ki …” | beolvasás + keresés / kiválogatás |
| „Kategóriánként / havonta / naponta …” | csoportosítás: a különböző értékek kigyűjtése, majd mindegyikre megszámlálás vagy összegzés |
| „Írja ki a … fájlba” | `File.WriteAllLines` vagy `StreamWriter` |

### A megoldás szerkezete

```
Projekt
├── Merkozes.cs        ← osztály: tulajdonságok, konstruktor(sor), számított tulajdonságok, ToString
├── Program.cs         ← beolvasás, majd a feladatok sorban, sorszámozott kiírással
└── adatfajl.csv       ← Copy to Output Directory: Copy if newer
```

Kisebb feladatnál a kérdések egymás után, a `Program.cs`-ben is elférnek. Hosszabb feladatnál érdemes minden kérdést külön metódusba tenni: `static void Feladat5(List<Merkozes> lista)`. Ha az egyik metódus elakad, a többi pontot ettől még megkapod.

### Ellenőrzőlista beadás előtt

- [ ] A program hiba nélkül lefordul, és figyelmeztetés sincs.
- [ ] A fejléc kimarad, a rekordszám stimmel.
- [ ] Minden kérdésre van kiírás, sorszámmal.
- [ ] A bekért adatnál a nem létező értéket is kezeli a program.
- [ ] A kiírt fájl létrejön, a formátuma pontosan a kért.
- [ ] A kód olvasható: beszédes nevek, egységes behúzás, rövid megjegyzések a tételeknél.

## Példák

| Projekt | Mit mutat be |
|---|---|
| [peldak/VizsgaSablon](peldak/VizsgaSablon/) | vizsgafeladat-megoldás váza: adatosztály CSV-sorból, beolvasás, feladatonként egy metódus sorszámozott kiírással, segédmetódus a csoportosításhoz, fájlba írás – egy apró mintaadattal futtatható, a saját feladatodhoz átírható |

## Feladatok

| Azonosító | Feladat | Nehézség | Megoldás |
|---|---|---|---|
| 13-M04-L04-F01 | [Sportesemények](feladatok/F01-sportesemenyek/) | ★★☆ | [megoldás](megoldasok/F01-sportesemenyek/) |
| 13-M04-L04-F02 | [Parkolóház díjai](feladatok/F02-parkolohaz/) | ★★★ | [megoldás](megoldasok/F02-parkolohaz/) |
| 13-M04-L04-F03 | [Házidolgozat](feladatok/F03-hazidolgozat/) | ★★★ | – (egyéni munka) |

## Kapcsolódó

- Előző lecke: 13-M04-L03 (Dátum- és időkezelés, típuskonverziók)
- Következő modul: 13-M05 (WPF bevezetés)
- Tanmenet: [1-13](../../../tanmenet/1-13-2felev.md)
