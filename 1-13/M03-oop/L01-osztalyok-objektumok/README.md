---
id: 13-M03-L01
cim: Osztályok és objektumok
evfolyam: 1-13
modul: M03 – Objektumorientált programozás
orak: 25–28.
het:
oraszam: 4
elofeltetel: 13-M02-L04
leiras: Osztály, objektum, adattag, tulajdonság (property), konstruktor, ToString felüldefiniálása, objektumok listában.
---

# 13-M03-L01 · Osztályok és objektumok

## Célok

A lecke végére a tanuló:

- saját szavaival elmagyarázza, mi az osztály és mi az objektum, és miért jó az összetartozó adatokat és műveleteket egy egységbe zárni;
- osztályt ír mezőkkel, metódusokkal és konstruktorral, majd a `new` kulcsszóval objektumokat hoz létre belőle;
- tudja, mit jelent a `public` és a `private`, és miért rejtjük el a mezőket (adatrejtés);
- tulajdonságot (property) készít: automatikusat, ellenőrzöttet (`get`/`set`) és számítottat;
- felüldefiniálja a `ToString()` metódust, hogy az objektum olvashatóan jelenjen meg;
- objektumokat tárol `List<T>`-ben, és a már ismert algoritmusokat (összegzés, megszámlálás, maximumkeresés) objektumlistán is használja.

## Elmélet

### Honnan jövünk? A struktúrától az osztályig

Az előző leckében (13-M02-L04) a `struct` segítségével már összefogtunk több adatot egy egységbe, például egy termék nevét és árát. Az **objektumorientált programozás** (OOP) ezt viszi tovább: nemcsak az **adatokat**, hanem a rajtuk dolgozó **műveleteket** (metódusokat) is egy egységbe zárjuk. Ezt nevezzük **egységbezárásnak** (encapsulation).

Miért jó ez?

- A program a valós világ „dolgait” modellezi (bankszámla, termék, mozielőadás), ezért könnyebb átlátni.
- Az elkészült osztályt több programban is újra fel lehet használni.
- Az osztály maga vigyáz az adataira: nem engedi, hogy hibás érték kerüljön bele.

Ára is van: több kódot kell írni, mint egy egyszerű, „egy fájlba mindent” programnál. Kis feladatnál ez feleslegesnek tűnhet, nagyobbnál viszont nagyon megtérül.

### Osztály és objektum

| Fogalom | Hasonlat | C#-ban |
|---|---|---|
| **osztály** (class) | sütiforma, tervrajz | `public class Bankszamla { ... }` |
| **objektum**, példány (object, instance) | a kiszúrt süti, a megépült ház | `new Bankszamla(...)` |
| **példányosítás** | a forma használata | a `new` kulcsszó |

Az osztály önmagában csak leírás: megmondja, *milyen* adatai és műveletei lesznek az objektumoknak. Az objektum ennek egy konkrét, a memóriában létező darabja, saját adatokkal. Egy osztályból tetszőleges számú objektum készülhet, és mindegyiknek külön értékei vannak:

```csharp
Bankszamla anna = new Bankszamla("Kiss Anna");
Bankszamla bela = new Bankszamla("Nagy Béla");

anna.Befizet(10000);         // csak Anna egyenlege változik
Console.WriteLine(bela.Egyenleg);   // 0
```

Az osztályt általában **külön fájlba** írjuk, amelynek neve megegyezik az osztály nevével (`Bankszamla.cs`). Visual Studióban: *Project → Add Class…*

### Az osztály részei

```csharp
public class Bankszamla
{
    private int egyenleg;                    // mező (adattag)

    public string Tulajdonos { get; set; }   // tulajdonság

    public Bankszamla(string tulajdonos)     // konstruktor
    {
        Tulajdonos = tulajdonos;
    }

    public void Befizet(int osszeg)          // metódus
    {
        egyenleg += osszeg;
    }
}
```

| Rész | Más néven | Mire való |
|---|---|---|
| **mező** | adattag, field | az objektum adatainak tényleges tárolása |
| **tulajdonság** | jellemző, property | ellenőrzött hozzáférés az adathoz |
| **metódus** | tagfüggvény, method | művelet, amit az objektum el tud végezni |
| **konstruktor** | constructor | a kezdőértékek beállítása példányosításkor |

A mezők és tulajdonságok az objektum **állapotát** írják le, a metódusok a **viselkedését**.

### Láthatóság és adatrejtés

Minden tag elé odaírjuk, hogy ki érheti el:

| Módosító | Ki éri el? |
|---|---|
| `public` (nyilvános) | bárki, például a `Program.cs` is |
| `private` (privát) | csak az osztály saját kódja – ez az alapértelmezés, ha nem írunk semmit |

*(A `protected`-del a következő leckében, az öröklődésnél találkozunk.)*

**Adatrejtés:** a mezőket `private`-tá tesszük, és kívülről csak tulajdonságokon, metódusokon keresztül engedjük elérni őket. Miért? Mert ha az `egyenleg` publikus lenne, a főprogramban bárki leírhatná, hogy `szamla.egyenleg = -1000000;` – és az osztály ezt nem tudná megakadályozni.

### Tulajdonságok lépésről lépésre

Nézzük meg, hogyan jutunk el a tulajdonságig! Egy hőmérő osztálynál a hőmérséklet nem lehet −273,15 °C-nál kisebb (abszolút nulla).

**1. Publikus mező** – működik, de nincs semmilyen védelem:

```csharp
public double celsius;
...
h.celsius = -500;          // hibás érték, mégis bekerül
```

**2. Privát mező + beállító és lekérdező metódus** – védett, de kényelmetlen a használata:

```csharp
private double celsius;
public void CelsiusBeallit(double ertek) { if (ertek >= -273.15) celsius = ertek; }
public double CelsiusLekerdez() { return celsius; }
...
h.CelsiusBeallit(21.5);
Console.WriteLine(h.CelsiusLekerdez());
```

**3. Tulajdonság** – kívülről úgy használjuk, mint egy változót, de belül metódusként fut:

```csharp
private double celsius;
public double Celsius
{
    get { return celsius; }               // olvasáskor fut le
    set
    {
        if (value < -273.15)               // value: az éppen értékül adott szám
            throw new ArgumentException("Az abszolút nullánál nem lehet hidegebb.");
        celsius = value;                   // íráskor fut le
    }
}
...
h.Celsius = 21.5;                          // lefut a set, value = 21.5
Console.WriteLine(h.Celsius);              // lefut a get
```

Szokás szerint a mező neve kisbetűs (`celsius`), a hozzá tartozó tulajdonságé nagybetűs (`Celsius`).

Ha hibás értéket kapunk, két szokásos megoldás van:

- **figyelmen kívül hagyjuk** (`if (value >= 0) mezo = value;`) – egyszerű, de a hívó nem veszi észre, hogy nem történt semmi;
- **kivételt dobunk** (`throw new ArgumentException("...")`) – a program jelzi a hibát, a főprogram `try`–`catch`-csel elkaphatja. Ez a jobb megoldás.

#### A tulajdonságok fajtái

| Fajta | Példa | Mikor használjuk? |
|---|---|---|
| automatikus | `public string Nev { get; set; }` | ha nem kell ellenőrzés – a mezőt a fordító hozza létre |
| ellenőrzött | `get { ... } set { if (...) ... }` | ha vigyázni kell az értékre (saját privát mezővel) |
| kívülről csak olvasható | `public int Egyenleg { get; private set; }` | kívülről csak olvasni lehet, az osztály metódusai módosítják |
| számított | `public double Fahrenheit => Celsius * 9 / 5 + 32;` | nincs mögötte mező, minden olvasáskor újra kiszámolódik |

A számított tulajdonságnak `set` része is lehet: ilyenkor a megadott értéket *visszaszámolja* a tárolt adatra. Így egyetlen tárolt adat (`Celsius`) többféle mértékegységben is írható és olvasható – lásd a [HomeroTulajdonsagok](peldak/HomeroTulajdonsagok/) példát.

### Konstruktor

A konstruktor egy különleges metódus, amely **példányosításkor automatikusan lefut**, és beállítja az objektum kezdőértékeit.

- A neve **megegyezik az osztály nevével**.
- **Nincs visszatérési típusa** (még `void` sem).
- Ha nem írunk konstruktort, a fordító készít egy üres, paraméter nélkülit.
- Lehet több is, eltérő paraméterekkel (**túlterhelés**, overload) – ilyenkor a `new` után megadott paraméterek döntik el, melyik fut le.

```csharp
public Bankszamla(string tulajdonos, int nyitoBefizetes)
{
    Tulajdonos = tulajdonos;
    Befizet(nyitoBefizetes);    // a konstruktor is hívhat metódust
}

public Bankszamla(string tulajdonos) : this(tulajdonos, 0) { }   // a másik konstruktort hívja
```

A `this` mindig **az aktuális objektumot** jelenti. Akkor hasznos, ha a paraméter neve megegyezik a mezőével: `this.nev = nev;` (balra a mező, jobbra a paraméter). A `: this(...)` pedig egy másik konstruktort hív meg, hogy ne kelljen ugyanazt a kódot kétszer megírni.

Tipp: a konstruktorban is **a tulajdonságnak** adjunk értéket, ne közvetlenül a mezőnek – így az ellenőrzés a példányosításkor is lefut.

Destruktort (az objektum „eltakarítását”) C#-ban nem kell írnunk: a már nem használt objektumokat a **szemétgyűjtő** (garbage collector) automatikusan felszabadítja.

### A ToString felüldefiniálása

Ha egy objektumot kiírunk (`Console.WriteLine(szamla)`), a C# a `ToString()` metódusát hívja meg. Alapból ez csak a típus nevét adja vissza, például `BankszamlaDemo.Bankszamla` – ami nem sokat mond. Ha megírjuk a saját változatunkat, az objektum úgy jelenik meg, ahogy szeretnénk:

```csharp
public override string ToString()
{
    return $"{Tulajdonos}: {Egyenleg} Ft";
}
```

Az `override` szó azt jelenti, hogy egy már létező metódust (minden osztály örökli a `ToString`-et) **felülírunk**. Hogy ez pontosan honnan jön, a következő leckében (öröklődés) derül ki – addig elég megjegyezni a pontos formát: `public override string ToString()`.

### Objektumok listában

A `List<T>`-t már ismerjük számokkal és szövegekkel. Ugyanígy tárolhatunk benne objektumokat is:

```csharp
List<Bankszamla> szamlak = new List<Bankszamla>();
szamlak.Add(new Bankszamla("Kiss Anna", 25000));
szamlak.Add(new Bankszamla("Nagy Béla", 8000));

int osszes = 0;                                   // összegzés
foreach (Bankszamla sz in szamlak)
    osszes += sz.Egyenleg;

Bankszamla leggazdagabb = szamlak[0];             // maximumkeresés
foreach (Bankszamla sz in szamlak)
    if (sz.Egyenleg > leggazdagabb.Egyenleg)
        leggazdagabb = sz;
```

A programozási tételek (13-M02-L03) tehát változatlanok, csak most az objektum egy **tulajdonságát** vizsgáljuk (`sz.Egyenleg`). A maximumkeresés eredménye ráadásul maga az objektum, így minden adata elérhető.

### Az objektum referencia típus

Fontos különbség a `struct`-hoz és az `int`-hez képest: az osztály **referencia típus**. A változó nem magát az objektumot tárolja, hanem csak egy *hivatkozást* rá. Ha egy változót egy másiknak adunk értékül, **ugyanarra az egy objektumra** mutat mindkettő:

```csharp
Bankszamla a = new Bankszamla("Kiss Anna");
Bankszamla b = a;          // nem másolat! ugyanaz a számla
b.Befizet(5000);
Console.WriteLine(a.Egyenleg);   // 5000 – a is látja a változást
```

Ha egy változó még nem mutat objektumra, az értéke `null`; ilyenkor bármely tagjának elérése `NullReferenceException`-t okoz.

### Tipikus hibák

- **Elfelejtett `new`:** `Bankszamla sz; sz.Befizet(100);` – a változó létezik, de objektum még nincs mögötte (fordítási hiba vagy `NullReferenceException`).
- **„... is inaccessible due to its protection level”:** privát tagot próbálunk elérni a `Program.cs`-ből. Ilyenkor tulajdonságot vagy publikus metódust kell használni (vagy átgondolni, kell-e egyáltalán kívülről elérni).
- **Végtelen rekurzió a tulajdonságban:** `public double Celsius { get { return Celsius; } }` – a tulajdonság saját magát hívja (`StackOverflowException`). A `get` a kisbetűs *mezőt* adja vissza.
- **A konstruktor a mezőnek ad értéket** a tulajdonság helyett – ilyenkor az ellenőrzés kimarad, és hibás érték kerülhet az objektumba.
- **`ToString` `override` nélkül:** `public string ToString()` – a fordító figyelmeztet (CS0114), és a kiírás nem mindig a saját szövegünket mutatja. Mindig `public override string ToString()`.
- **Visszatérési típus a konstruktoron:** `public void Bankszamla(...)` már nem konstruktor, hanem egy furcsa nevű metódus – a példányosításkor nem fut le.

## Példák

| Projekt | Mit mutat be |
|---|---|
| [peldak/BankszamlaDemo](peldak/BankszamlaDemo/) | osztály külön fájlban, privát mező, kívülről csak olvasható tulajdonság, metódusok ellenőrzéssel, két konstruktor `this(...)`-szal, `ToString()`, referencia típus, objektumok listában (összegzés, maximumkeresés) |
| [peldak/HomeroTulajdonsagok](peldak/HomeroTulajdonsagok/) | a tulajdonság „születése”: publikus mező → metódusok → ellenőrzött tulajdonság; számított tulajdonság `get`-tel és `set`-tel (Celsius, Fahrenheit, Kelvin), kivételkezelés |

Futtatás: nyisd meg a `.csproj` fájlt Visual Studióban, majd F5. Érdemes lépésenként (F11) végigmenni a programon, és figyelni, mikor ugrik a vezérlés a `set`-be és a `get`-be.

## Feladatok

| Azonosító | Feladat | Nehézség | Megoldás |
|---|---|---|---|
| 13-M03-L01-F01 | [Raktárkészlet](feladatok/F01-raktarkeszlet/) | ★☆☆ | [megoldás](megoldasok/F01-raktarkeszlet/) |
| 13-M03-L01-F02 | [Időtartam-átváltó](feladatok/F02-idotartam/) | ★★☆ | [megoldás](megoldasok/F02-idotartam/) |
| 13-M03-L01-F03 | [Mozijegy-foglalás](feladatok/F03-mozijegy/) | ★★★ | [megoldás](megoldasok/F03-mozijegy/) |

## Kapcsolódó

- Előző lecke: 13-M02-L04 (Metódusok, struktúrák, listák)
- Következő lecke: 13-M03-L02 (Öröklődés és többalakúság)
- Tanmenet: [1-13](../../../tanmenet/1-13-2felev.md)
