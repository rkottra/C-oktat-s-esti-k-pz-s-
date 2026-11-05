---
id: 14-M01-L01
cim: OOP ismétlés
evfolyam: 2-14
modul: M01 – Konzolos alkalmazások ismétlése
orak: 1–4.
het:
oraszam: 4
elofeltetel: 
leiras: Osztályok, tulajdonságok, konstruktorok, ToString felüldefiniálása, öröklődés; objektumok listában.
---

# 14-M01-L01 · OOP ismétlés

## Célok

A lecke végére a tanuló:

- megkülönbözteti az osztályt és az objektumot, a mezőt és a tulajdonságot;
- ellenőrzött tulajdonságot ír (`get`/`set`), és ismeri az automatikus, a csak olvasható és a számított tulajdonságot;
- konstruktort és konstruktor-túlterhelést készít, a `this(...)` és a `base(...)` hívással;
- felüldefiniálja a `ToString()` metódust;
- öröklődési hierarchiát épít absztrakt ősosztállyal, `virtual`/`abstract`/`override` metódusokkal;
- objektumokat tárol `List<T>`-ben, és ciklussal megszámol, összegez, maximumot keres benne.

## Elmélet

### Osztály és objektum

Az **osztály** egy típus „tervrajza”: leírja, milyen adatai (mezők, tulajdonságok) és műveletei (metódusok) vannak. Az **objektum** (példány) ennek egy konkrét, a memóriában létező darabja, amelyet a `new` kulcsszóval hozunk létre. Az adatok és a rajtuk dolgozó műveletek egy egységbe zárását **egységbezárásnak** (encapsulation) nevezzük.

```csharp
var h = new Haromszog("derékszögű", 3, 4, 5);   // példányosítás: lefut a konstruktor
Console.WriteLine(h.Kerulet);                   // tulajdonság olvasása
```

| Fogalom | Példa | Mire való |
|---|---|---|
| mező (field) | `private double a;` | az adat tényleges tárolása, általában privát |
| tulajdonság (property) | `public double A { get; set; }` | ellenőrzött hozzáférés a mezőhöz |
| metódus | `public string HangotAd()` | művelet az objektummal |
| konstruktor | `public Haromszog(...)` | kezdőértékek beállítása példányosításkor |

A C#-ban nincs szükség destruktorra: a már nem használt objektumokat a **szemétgyűjtő** (garbage collector) automatikusan felszabadítja.

### Láthatóság, adatrejtés

| Módosító | Ki éri el? |
|---|---|
| `public` | bárki |
| `private` | csak az osztály saját kódja (ez az alapértelmezés) |
| `protected` | az osztály és a leszármazottai |
| `internal` | az azonos projektben (assembly-ben) lévő kód |

**Adatrejtés:** a mezők privátak, kívülről csak tulajdonságokon és metódusokon keresztül érhetők el. Így az osztály maga dönti el, milyen érték kerülhet bele.

### Tulajdonságok

Ha a mező publikus lenne, bárki negatív oldalhosszt írhatna bele. A tulajdonság `set` része **minden értékadáskor lefut**, a `get` minden olvasáskor:

```csharp
private double a;
public double A
{
    get { return a; }
    set
    {
        if (value <= 0) throw new ArgumentException("Az oldal csak pozitív lehet.");
        a = value;                 // value: az éppen értékül adott érték
    }
}
```

| Fajta | Példa | Megjegyzés |
|---|---|---|
| automatikus | `public string Nev { get; set; }` | ha nem kell ellenőrzés; a mezőt a fordító készíti |
| ellenőrzött | `set { if (...) ...; a = value; }` | saját privát mezővel |
| csak olvasható | `public DateTime Letrehozva { get; }` | csak a konstruktorban kaphat értéket |
| kívülről csak olvasható | `public string Nev { get; private set; }` | az osztályon belül írható |
| számított | `public double Kerulet => A + B + C;` | nincs mögötte mező, mindig újraszámolódik |

Az ellenőrzésre két szokásos megoldás van: érvénytelen értéknél **figyelmen kívül hagyjuk** (`if (value > 0) a = value;`), vagy **kivételt dobunk** (`throw new ArgumentException(...)`). Az utóbbi jobb, mert a hívó észreveszi a hibát, és `try`–`catch`-csel kezelheti.

### Konstruktorok

- Neve megegyezik az osztály nevével, nincs visszatérési típusa.
- Lehet több is, eltérő paraméterlistával (**túlterhelés**, overload).
- A `: this(...)` ugyanannak az osztálynak egy másik konstruktorát hívja meg – így nem kell a kódot ismételni.
- Opcionális paraméter: `public Felhasznalo(string nev, string jelszo = "Kezdo123")`.
- A konstruktorban is **a tulajdonságnak** adjunk értéket (ne a mezőnek), így az ellenőrzés ott is érvényes.

```csharp
public Haromszog(string nev, double a, double b, double c) { Nev = nev; A = a; B = b; C = c; }
public Haromszog(string nev, double oldal) : this(nev, oldal, oldal, oldal) { }
```

### A ToString felüldefiniálása

Minden osztály az `object` leszármazottja, ezért van `ToString()` metódusa. Alapértelmezésben csak a típus nevét adja vissza (`HaromszogTulajdonsagok.Haromszog`). Felüldefiniálva a `Console.WriteLine(h)`, a `$"{h}"` – és később a WPF-es listák is – a saját szövegünket jelenítik meg:

```csharp
public override string ToString() => $"{Nev} ({A}; {B}; {C})";
```

### Öröklődés

Az **öröklődéssel** egy meglévő osztályból (ős, szülő) újat (leszármazott, gyerek) készítünk, amely megkapja az ős összes tagját, és újakat adhat hozzá. C#-ban egy osztálynak csak egy közvetlen őse lehet.

```csharp
public class Kutya : Allat
{
    public string Fajta { get; set; }
    public Kutya(int id, string nev, int kor, double suly, string fajta)
        : base("kutya", id, nev, kor, suly)      // az ős konstruktorának hívása
    {
        Fajta = fajta;
    }
}
```

| Kulcsszó | Hol | Jelentés |
|---|---|---|
| `abstract class` | ős | nem példányosítható, csak örökölni lehet belőle |
| `abstract` metódus | ős | nincs törzse, a leszármazottnak **kötelező** megírnia |
| `virtual` metódus | ős | van alapműködése, a leszármazott **felülírhatja** |
| `override` | leszármazott | felülírja az ős `virtual`/`abstract` metódusát |
| `base.Metodus()` | leszármazott | az ős változatának meghívása |
| `sealed` | osztály | belőle már nem lehet tovább örökölni |

### Polimorfizmus (többalakúság)

Egy ős típusú változó (vagy lista) leszármazott objektumokat is tárolhat. Metódushíváskor **mindig az objektum valódi típusának** felülírt metódusa fut le (késői kötés):

```csharp
var menhely = new List<Allat> { new Kutya(...), new Macska(...) };
foreach (var a in menhely)
    Console.WriteLine(a.HangotAd());     // "Vau-vau!", "Miaú!"
```

A leszármazott saját tagjait (pl. `Fajta`) csak típusvizsgálat után érjük el:

```csharp
if (a is Kutya k)                        // ellenőriz és át is alakít
    Console.WriteLine(k.Fajta);
```

### Objektumok listában

A `List<T>` dinamikus méretű lista: `Add`, `Remove`, `Count`, indexelés (`lista[0]`). A szokásos algoritmusok (megszámolás, összegzés, maximumkeresés) objektumlistán ugyanúgy működnek, csak egy tulajdonságot vizsgálunk:

```csharp
Allat legidosebb = menhely[0];
foreach (var a in menhely)
    if (a.Kor > legidosebb.Kor) legidosebb = a;
```

A következő leckében ugyanezeket egy sorban, LINQ-kel írjuk meg, és az objektumokat fájlból töltjük be.

### Tipikus hibák

- **`ToString` `override` nélkül:** a `public string ToString()` csak *elfedi* az örökölt metódust (CS0114 figyelmeztetés), így `object` vagy ős típusú változón keresztül továbbra is a típusnév jelenik meg. Mindig `public override string ToString()`.
- **A konstruktor a mezőnek ad értéket**, nem a tulajdonságnak – ilyenkor az ellenőrzés kimarad.
- **Végtelen rekurzió a tulajdonságban:** `public int Kor { get => Kor; ... }` – a tulajdonság saját magát hívja (`StackOverflowException`). A `get` a *mezőt* (`kor`) adja vissza.
- **„There is no argument given that corresponds to the required parameter”:** a leszármazott konstruktorából hiányzik a `: base(...)`, és az ősnek nincs paraméter nélküli konstruktora.
- **Absztrakt osztály példányosítása** (`new Allat(...)`): fordítási hiba – csak a leszármazottakat lehet példányosítani.
- **`protected` tag elérése kívülről:** a `protected` csak a leszármazott osztály *kódjában* érhető el, a `Program.cs`-ből nem.

## Példák

| Projekt | Mit mutat be |
|---|---|
| [peldak/HaromszogTulajdonsagok](peldak/HaromszogTulajdonsagok/) | privát mezők, ellenőrzött / automatikus / számított tulajdonságok, konstruktor-túlterhelés `this(...)`-szal, `ToString()`, kivételkezelés, objektumlista |
| [peldak/AllatmenhelyOop](peldak/AllatmenhelyOop/) | absztrakt `Allat` ősosztály, `Kutya` és `Macska` leszármazottak, `abstract`/`virtual`/`override`, `base(...)`, `protected`, polimorf lista, `is` típusvizsgálat |

Futtatás: nyisd meg a `.csproj` fájlt Visual Studióban, majd F5. Az `AllatmenhelyOop` osztályai a következő lecke CSV-s példájának előzményei.

## Feladatok

| Azonosító | Feladat | Nehézség | Megoldás |
|---|---|---|---|
| 14-M01-L01-F01 | [Téglalapok](feladatok/F01-teglalap/) | ★☆☆ | [megoldás](megoldasok/F01-teglalap/) |
| 14-M01-L01-F02 | [Járműflotta](feladatok/F02-jarmuvek/) | ★★☆ | [megoldás](megoldasok/F02-jarmuvek/) |
| 14-M01-L01-F03 | [Felhasználók és jogosultságok](feladatok/F03-felhasznalok/) | ★★★ | [megoldás](megoldasok/F03-felhasznalok/) |

## Kapcsolódó

- Előző lecke: – (a modul az 1/13. évfolyam OOP-ismereteire épít)
- Következő lecke: 14-M01-L02 (Fájlkezelés és LINQ)
- Tanmenet: [2-14](../../../tanmenet/2-14.md)
