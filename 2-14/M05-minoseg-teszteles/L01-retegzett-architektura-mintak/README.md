---
id: 14-M05-L01
cim: Rétegzett architektúra, tervezési minták
evfolyam: 2-14
modul: M05 – Szoftverminőség és tesztelés
orak: 53–56.
het:
oraszam: 4
elofeltetel: 14-M04-L04
leiras: Többrétegű programozási technika, a leggyakoribb tervezési minták (pl. Singleton, Repository, MVVM alapgondolata).
---

# 14-M05-L01 · Rétegzett architektúra, tervezési minták

## Célok

A lecke végére a tanuló:

- megnevezi a háromrétegű felépítés rétegeit (adat, üzleti logika, megjelenítés), és egy kódrészletről eldönti, melyikbe tartozik;
- egy „egy tömbben” megírt konzolos programot modellre, adatrétegre, üzleti logikára és felületre bont;
- interfész mögé rejtett adatréteget (Repository minta) készít, és két megvalósítás között cserél;
- felismeri és megírja a Singleton mintát, és tudja, mikor nem érdemes használni;
- elmondja az MVVM lényegét, és megmutatja egy WPF-alkalmazásban a Model, a View és a ViewModel részt.

## Elmélet

### Miért rétegezünk?

Az eddigi programjaink többsége „egyrétegű” volt: a `Program.cs` (vagy a `MainWindow.xaml.cs`) egyszerre olvasta a fájlt, számolt és írt ki.
Kis programnál ez rendben van, de ahogy nő a kód, egyre nehezebb:

- **módosítani** – ha CSV helyett adatbázisból kell olvasni, az egész programot át kell írni;
- **újrahasznosítani** – ugyanazt a számolást a WPF-felületen újra meg kell írni;
- **tesztelni** – a számolást nem lehet kipróbálni a konzol és a fájl nélkül;
- **csapatban fejleszteni** – mindenki ugyanabban a fájlban dolgozik.

A megoldás: a programot **felelősség szerint** szétválasztjuk. Minden réteg csak a saját dolgával foglalkozik, és csak az alatta lévőt ismeri.

### A három réteg

| Réteg | Más nevei | Feladata | Példa a kódban |
|---|---|---|---|
| **Megjelenítés** | felhasználói felület, UI, presentation layer | bekér, kiír, megjelenít | `Program.cs`, `MainWindow.xaml` |
| **Üzleti logika** | alkalmazáslogika, BLL, business layer | számol, szűr, ellenőriz, szabályokat érvényesít | `EpuletSzolgaltatas` |
| **Adatréteg** | adatelérési réteg, DAL, data layer | adatot olvas és ír (fájl, adatbázis) | `CsvEpuletRepository`, `DbContext` |

A rétegeken átível a **modell** (`Epulet`): egyszerű adatosztály, amelyet mindegyik réteg használ.

```
 Megjelenítés      Program.cs  ──────────────┐
      │ hívja                                │
 Üzleti logika     EpuletSzolgaltatas        │  mindegyik ismeri
      │ hívja (interfészen át)               │  a modellt
 Adatréteg         IEpuletRepository         │  (Epulet)
                     ├─ CsvEpuletRepository  │
                     └─ MemoriaEpuletRepository
```

Szabály: **a hívás csak lefelé mehet**. Az üzleti logika nem ír a konzolra, az adatréteg nem számol statisztikát, a felület nem nyit fájlt.

Előnyök és árak:

| Előny | Ár |
|---|---|
| az adatforrás cserélhető (CSV → MySQL) | több osztály, több fájl |
| a felület cserélhető (konzol → WPF), a logika marad | több tervezést igényel |
| a logika külön tesztelhető (14-M05-L03) | kis programnál túlzás lehet |
| csapatban jól szétosztható a munka | |

> Nagy, többfelhasználós rendszereknél a rétegek külön gépen is futhatnak (pl. adatbázis-szerver, alkalmazásszerver, kliens); ilyenkor *tier*-ről beszélünk.
> Mi egy programon belül, mappákkal és osztályokkal választjuk szét a rétegeket (*layer*) – a gondolkodásmód ugyanaz.

### Tervezési minták

A **tervezési minta** (design pattern) egy gyakran előforduló programozási problémára adott, bevált, újrahasznosítható megoldás-*vázlat*.
Nem kész kód, hanem „recept”, amelynek neve van – így a fejlesztők egy szóval megértik egymást („ide egy repository kell”).
A klasszikus csoportosítás: **létrehozási** (pl. Singleton, Factory), **szerkezeti** (pl. Adapter) és **viselkedési** (pl. Observer, Iterator) minták.
Ezek közül hármat nézünk meg részletesen.

### Repository (tároló) minta

Az adatelérést egy **interfész** mögé rejtjük. Az üzleti logika csak az interfészt ismeri, azt nem, hogy mögötte fájl, adatbázis vagy memória van.

```csharp
public interface IEpuletRepository
{
    List<Epulet> Osszes();
    void Hozzaad(Epulet epulet);
}

public class EpuletSzolgaltatas
{
    private readonly IEpuletRepository repository;

    public EpuletSzolgaltatas(IEpuletRepository repository)   // kívülről kapja!
    {
        this.repository = repository;
    }
    ...
}

// A Program.cs dönti el, melyik megvalósítás legyen:
var szolgaltatas = new EpuletSzolgaltatas(new CsvEpuletRepository("epuletek.csv"));
// var szolgaltatas = new EpuletSzolgaltatas(new MemoriaEpuletRepository());
```

Azt, hogy az osztály a függőségét a konstruktorban **kívülről kapja** ahelyett, hogy maga hozná létre, *függőséginjektálásnak* (dependency injection) hívják.
Ez teszi lehetővé a cserét, és ez kell majd a teszteléshez is.

### Singleton (egyke) minta

Akkor használjuk, ha egy osztályból **pontosan egy példány** kell, és azt **bárhonnan** el kell érni (napló, beállítások).

```csharp
public sealed class Naplo                       // sealed: nem lehet leszármaztatni
{
    private static readonly Naplo peldany = new();   // az egyetlen példány
    public static Naplo Peldany => peldany;          // ezen keresztül érhető el

    private Naplo() { }                              // privát: kívülről nincs "new Naplo()"

    public void Ir(string uzenet) { ... }
}

Naplo.Peldany.Ir("Program indult");
```

A `static readonly` mezőt a .NET egyszer és szálbiztosan hozza létre. Régebbi anyagokban `lock`-kal védett, „lusta” változat is szerepel – ugyanazt a célt szolgálja.

**Mértékkel!** A Singleton kényelmes, de *rejtett függőség*: kívülről nem látszik, hogy egy osztály használja, és teszteléskor nem lehet kicserélni.
Napló és beállítások esetén elfogadható; adatelérésre ne használjuk, arra a Repository + konstruktoros átadás való.

### Az MVVM gondolata

Az **MVVM** (Model–View–ViewModel) a WPF-alkalmazások szokásos szerkezete. A megjelenítési réteget bontja tovább:

| Rész | Mit tartalmaz | WPF-ben |
|---|---|---|
| **Model** | az adatok és szabályaik (és mögötte az adatréteg) | `Epulet`, repository, `DbContext` |
| **View** | a felület kinézete | `MainWindow.xaml` – a code-behind szinte üres |
| **ViewModel** | a felület *állapota*: a megjelenítendő lista, a beírt szűrőszöveg, a kijelölt elem | `EpuletekViewModel : INotifyPropertyChanged` |

A View és a ViewModel között **adatkötés** (`Binding`) teremt kapcsolatot, amelyet a 14-M02-L02 leckében már használtunk:
a `DataContext` a ViewModel, a vezérlők pedig a tulajdonságaihoz kötődnek. A ViewModelben nincs `TextBox` vagy `DataGrid` – ezért felület nélkül is kipróbálható.

| Háromrétegű felépítés | MVVM |
|---|---|
| Megjelenítés | View + ViewModel |
| Üzleti logika | Model |
| Adatréteg | Model (mögötte) |

> A kurzus feladataiban az eseménykezelős (code-behind) WPF is teljesen rendben van, az MVVM-et nem kérjük számon. A gondolatát azért érdemes ismerni: minél kevesebb logika van a `MainWindow.xaml.cs`-ben, annál könnyebb a hibakeresés.

### Tipikus hibák

- **Számolás a felületen**: `Console.WriteLine(lista.Where(...).Sum(...))` a `Program.cs`-ben – ez üzleti logika, a szolgáltatásba való.
- **Konzol az üzleti rétegben**: ha a `EpuletSzolgaltatas` `Console.WriteLine`-t hív, WPF-ből már nem használható. Adjon vissza értéket, vagy dobjon kivételt.
- **A szolgáltatás maga hozza létre a repositoryt** (`new CsvEpuletRepository(...)` a konstruktorban) – így nem cserélhető. Kívülről kapja!
- **Mindenből Singleton**: ha minden osztály `Peldany`-n keresztül érhető el, az olyan, mintha minden globális változó lenne.
- **Túlrétegezés**: egy 30 soros feladatnál nem kell öt mappa. A rétegezés a közepes és nagy programoknál térül meg.

## Példák

| Projekt | Mit mutat be |
|---|---|
| [peldak/RetegzettEpuletek](peldak/RetegzettEpuletek/) | Híres épületek CSV-ből, rétegekre bontva: `Modell`, `Adatretag` (CSV- és memória-repository), `UzletiLogika` (statisztika, ellenőrzés), `Megjelenites`, valamint egy Singleton napló (`Kozos/Naplo.cs`) |
| [peldak/MvvmEpuletek](peldak/MvvmEpuletek/) | WPF-ablak MVVM-szerkezetben: élő keresés a ViewModel `Szuro` tulajdonságán keresztül, üres code-behind |

Kipróbálás: a `RetegzettEpuletek` `Program.cs`-ében cseréld a `CsvEpuletRepository`-t `MemoriaEpuletRepository`-ra – a program többi része változatlanul működik.

## Feladatok

| Azonosító | Feladat | Nehézség | Megoldás |
|---|---|---|---|
| 14-M05-L01-F01 | [Rétegekre bontás](feladatok/F01-retegekre-bontas/) | ★★☆ | [megoldás](megoldasok/F01-retegekre-bontas/) |
| 14-M05-L01-F02 | [Repository-csere és Singleton](feladatok/F02-repository-singleton/) | ★★☆ | [megoldás](megoldasok/F02-repository-singleton/) |

## Kapcsolódó

- Előző lecke: 14-M04-L04 (Adatimport és -export (CSV, JSON, XML))
- Következő lecke: 14-M05-L02 (Tiszta kód és UML)
- Tanmenet: [2-14](../../../tanmenet/2-14.md)
