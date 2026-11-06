---
id: 13-M03-L02
cim: Öröklődés és többalakúság
evfolyam: 1-13
modul: M03 – Objektumorientált programozás
orak: 29–32.
het:
oraszam: 4
elofeltetel: 13-M03-L01
leiras: Ősosztály és leszármazott, base, virtual/override, többalakúság a gyakorlatban.
---

# 13-M03-L02 · Öröklődés és többalakúság

## Célok

A lecke végére a tanuló:

- elmagyarázza, mire jó az öröklődés, és felismeri az „…egy fajta …” (*is-a*) kapcsolatot;
- ősosztályból leszármazott osztályt készít, és a leszármazott konstruktorából a `base(...)` segítségével meghívja az ős konstruktorát;
- érti a `private`, `protected` és `public` láthatóság közti különbséget az öröklődésnél;
- az ős metódusát `virtual`-ként jelöli, a leszármazottban `override`-dal felülírja, és a `base.Metodus()` hívással az ős változatát is felhasználja;
- ős típusú listában különböző leszármazott objektumokat tárol, és megfigyeli, hogy mindig a valódi típus metódusa fut le (többalakúság);
- a leszármazott saját tagjait típusvizsgálattal (`is`) éri el.

## Elmélet

### Miért kell öröklődés?

Képzeljük el, hogy egy cég dolgozóit tartjuk nyilván. Van **alkalmazott**, **vezető** és **gyakornok**. Mindegyiknek van neve és alapbére, és mindegyiknek ki kell számolni a fizetését – de a vezető pótlékot kap, a gyakornok pedig csak az alapbér egy részét. Ha mindhármat külön osztályként írnánk meg, a közös részeket (név, alapbér, ellenőrzés, kiírás) háromszor kellene leírni, és ha valamit javítani kell, mindhárom helyen.

Az **öröklődéssel** (inheritance) a közös részt egyszer írjuk meg egy **ősosztályban**, a különbségeket pedig a **leszármazott** osztályokban:

```
            Alkalmazott            ← ősosztály (szülő, bázisosztály)
           /           \
       Vezeto        Gyakornok     ← leszármazottak (gyerek, származtatott osztály)
```

Az öröklődés akkor helyes, ha igaz rá a mondat: **„A vezető *egy fajta* alkalmazott.”** Ugyanígy: a négyzet egy fajta téglalap, a kutya egy fajta emlős. Ha a mondat nem igaz („a kerék egy fajta autó”), akkor nem öröklődésről van szó.

### Szintaxis

```csharp
public class Alkalmazott                 // ős
{
    public string Nev { get; set; }
    public int Alapber { get; set; }
    ...
}

public class Vezeto : Alkalmazott        // a kettőspont után az ős neve
{
    public int Potlek { get; set; }      // új, saját tulajdonság
}
```

A `Vezeto` **mindent megkap** (örököl), ami az `Alkalmazott`-ban van: a `Nev` és az `Alapber` tulajdonságot és a metódusokat is. Ezeket nem kell újra leírni. Ezen felül saját tagokat is kaphat (`Potlek`).

Szabályok:

- C#-ban egy osztálynak **csak egy közvetlen őse** lehet (`class C : A, B` nem megy).
- Az öröklődés több szinten is folytatódhat: `Gyakornok : Alkalmazott`, `NyariGyakornok : Gyakornok`.
- Minden osztály végső őse az **`object`** – akkor is, ha nem írjuk ki. Innen jön a `ToString()`, ezért kellett az előző leckében `override`-dal felülírni.

### Láthatóság öröklődésnél

| Módosító | Az osztály saját kódja | A leszármazott kódja | Más osztály (pl. `Program.cs`) |
|---|---|---|---|
| `private` | igen | **nem** | nem |
| `protected` | igen | **igen** | nem |
| `public` | igen | igen | igen |

A leszármazott tehát megkapja az ős privát mezőit is (az objektumban ott vannak), de **a saját kódjából nem éri el őket** – csak az ős tulajdonságain, metódusain keresztül. Ha azt szeretnénk, hogy a leszármazott közvetlenül hozzáférjen, de kívülről senki, akkor `protected`-et használunk.

### Konstruktorok és a `base`

A konstruktorok **nem öröklődnek**: a leszármazottnak sajátot kell írnia. Ebben a `: base(...)` hívással adjuk át az ősnek azokat az adatokat, amelyeket ő kezel:

```csharp
public class Vezeto : Alkalmazott
{
    public int Potlek { get; set; }

    public Vezeto(string nev, int alapber, int potlek)
        : base(nev, alapber)        // előbb lefut az Alkalmazott konstruktora
    {
        Potlek = potlek;            // utána a saját rész
    }
}
```

A sorrend mindig: **először az ős konstruktora, aztán a leszármazotté** – hiszen a leszármazott építhet arra, amit az ős már beállított.

A `this` és a `base` összefoglalva:

| Kulcsszó | Jelentése | Példa |
|---|---|---|
| `this` | az aktuális objektum | `this.nev = nev;` |
| `: this(...)` | ugyanennek az osztálynak egy másik konstruktora | `: this(nev, 0)` |
| `: base(...)` | az ős konstruktora | `: base(nev, alapber)` |
| `base.Metodus()` | az ős (felülírt) metódusa | `base.Fizetes() + Potlek` |

### Metódusok felülírása: `virtual` és `override`

A leszármazott nemcsak új tagokat adhat hozzá, hanem **megváltoztathatja** az örökölt metódus működését is. Ehhez két kulcsszó kell:

- az ősben **`virtual`**: „ezt a metódust a leszármazott felülírhatja”;
- a leszármazottban **`override`**: „felülírom az ős változatát”.

```csharp
// Alkalmazott (ős)
public virtual int Fizetes()
{
    return Alapber;
}

// Vezeto (leszármazott)
public override int Fizetes()
{
    return base.Fizetes() + Potlek;     // az ős változata + a saját rész
}

// Gyakornok (leszármazott)
public override int Fizetes()
{
    return Alapber * 60 / 100;          // teljesen más számítás
}
```

A felülírt metódus **neve, paraméterei és visszatérési típusa** pontosan meg kell egyezzen az ősével. A `base.Fizetes()` hívással nem kell újra megírni azt, amit az ős már tud – csak kiegészítjük.

Ugyanígy írjuk felül a `ToString()`-et is: `return base.ToString() + $", pótlék: {Potlek} Ft";`

### Többalakúság (polimorfizmus)

Ez az öröklődés legerősebb következménye. Mivel a vezető *egy fajta* alkalmazott, egy `Alkalmazott` típusú változóba vagy listába **leszármazott objektumot is tehetünk**:

```csharp
List<Alkalmazott> dolgozok = new List<Alkalmazott>
{
    new Alkalmazott("Kovács Péter", 520000),
    new Vezeto("Szilágyi Réka", 650000, 200000),
    new Gyakornok("Horváth Bence", 400000)
};

int berkoltseg = 0;
foreach (Alkalmazott a in dolgozok)
    berkoltseg += a.Fizetes();          // mindegyiknél a SAJÁT Fizetes() fut le
```

A ciklus nem tudja és nem is kell tudnia, hogy éppen milyen dolgozóval van dolga: meghívja a `Fizetes()`-t, és **mindig az objektum valódi típusának** felülírt metódusa fut le. Ugyanaz a hívás (`a.Fizetes()`) többféle alakban működik – ezért **többalakúság**. Ha később új dolgozótípus jön (pl. `Alvallalkozo`), a ciklushoz hozzá sem kell nyúlni.

#### Korai és késői kötés

Hogy miért kell a `virtual`/`override` páros, azt a [ElfedesVagyFeluliras](peldak/ElfedesVagyFeluliras/) példa mutatja meg:

| | Ősben | Leszármazottban | `Os v = new Leszarmazott(); v.M();` |
|---|---|---|---|
| **elfedés** | `public void M()` | `public new void M()` | az **ős** metódusa fut (a *változó* típusa dönt, már fordításkor – korai kötés) |
| **felülírás** | `public virtual void M()` | `public override void M()` | a **leszármazott** metódusa fut (az *objektum* típusa dönt, futás közben – késői kötés) |

Ha a leszármazottban `override` nélkül írunk egy azonos nevű metódust, az csak **elfedi** az ősét (a fordító figyelmeztet: *„hides inherited member … Use the new keyword if hiding was intended”*). Ilyenkor a polimorf lista **nem** a leszármazott változatát hívja. A gyakorlatban szinte mindig a felülírás kell; az elfedést (`new`) csak felismerni kell tudni.

#### Felülírás vagy túlterhelés?

Két hasonló nevű, de egészen más fogalom:

| | Felülírás (override) | Túlterhelés (overload) |
|---|---|---|
| Hol? | ős és leszármazott között | egy osztályon belül |
| Név | azonos | azonos |
| Paraméterek | **azonosak** | **eltérők** |
| Példa | `Fizetes()` az `Alkalmazott`-ban és a `Vezeto`-ben | két konstruktor: `(nev)` és `(nev, alapber)` |

### A leszármazott saját tagjainak elérése

Az `Alkalmazott` típusú változón keresztül csak azokat a tagokat látjuk, amelyek az `Alkalmazott`-ban is megvannak. A `Potlek` a `Vezeto` saját tulajdonsága, ezért előbb meg kell vizsgálni, hogy az objektum tényleg vezető-e:

```csharp
foreach (Alkalmazott a in dolgozok)
{
    if (a is Vezeto v)                  // ha vezető, akkor v néven elérjük vezetőként
        Console.WriteLine($"{v.Nev} pótléka: {v.Potlek} Ft");
}
```

Az `is` egyszerre **ellenőriz** és **átalakít**. Ha csak a darabszám kell: `if (a is Gyakornok) gyakornokDb++;`

### Lezárt osztály: `sealed`

Ha egy osztályból nem szeretnénk, hogy tovább örököljenek, `sealed`-dé tesszük: `public sealed class Gyakornok : Alkalmazott`. Ugyanígy egy `override` metódus is lezárható (`public sealed override int Fizetes()`), hogy a további leszármazottak már ne írhassák felül.

### Tipikus hibák

- **„There is no argument given that corresponds to the required parameter …”**: a leszármazott konstruktorából hiányzik a `: base(...)`, az ősnek pedig nincs paraméter nélküli konstruktora.
- **„… is inaccessible due to its protection level”** a leszármazottban: az ős mezője `private`. Használd az ős tulajdonságát, vagy tedd a mezőt `protected`-dé.
- **„cannot override inherited member … because it is not marked virtual”**: az ősben lemaradt a `virtual`.
- **Lemaradt `override`**: lefordul (csak figyelmeztetés!), de a polimorf listában az ős metódusa fut. Ha a lista „rosszul számol”, először ezt nézd meg.
- **`a.Potlek` egy `Alkalmazott` változón**: fordítási hiba – előbb `is`-szel át kell alakítani.
- **Öröklődés ott, ahol nincs „egy fajta” kapcsolat** (pl. `class Auto : Kerek`): ez tervezési hiba; ilyenkor az egyik osztály a másikat *tartalmazza* (tulajdonságként).

## Példák

| Projekt | Mit mutat be |
|---|---|
| [peldak/AlkalmazottakOrokles](peldak/AlkalmazottakOrokles/) | `Alkalmazott` ősosztály, `Vezeto` és `Gyakornok` leszármazott, `base(...)` a konstruktorban, `protected` tag, `virtual`/`override` (`Fizetes`, `ToString`), `base.Fizetes()`, polimorf lista, `is` típusvizsgálat |
| [peldak/ElfedesVagyFeluliras](peldak/ElfedesVagyFeluliras/) | ugyanaz a hívás elfedéssel (`new`) és felülírással (`virtual`/`override`) – korai és késői kötés egymás mellett |

Futtatás: nyisd meg a `.csproj` fájlt Visual Studióban, majd F5. Az `ElfedesVagyFeluliras` példában próbáld ki: töröld a `virtual` és az `override` szót, és figyeld meg, hogyan változik a kimenet és milyen figyelmeztetést ad a fordító.

## Feladatok

| Azonosító | Feladat | Nehézség | Megoldás |
|---|---|---|---|
| 13-M03-L02-F01 | [Webáruház termékei](feladatok/F01-webaruhaz/) | ★☆☆ | [megoldás](megoldasok/F01-webaruhaz/) |
| 13-M03-L02-F02 | [Ingatlaniroda](feladatok/F02-ingatlaniroda/) | ★★☆ | [megoldás](megoldasok/F02-ingatlaniroda/) |
| 13-M03-L02-F03 | [Hősök csatája](feladatok/F03-hosok-csataja/) | ★★★ | [megoldás](megoldasok/F03-hosok-csataja/) |

## Kapcsolódó

- Előző lecke: 13-M03-L01 (Osztályok és objektumok)
- Következő lecke: 13-M03-L03 (Statikus tagok, absztrakt osztályok, interfészek)
- Tanmenet: [1-13](../../../tanmenet/1-13-2felev.md)
