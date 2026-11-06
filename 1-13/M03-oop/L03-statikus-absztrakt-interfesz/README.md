---
id: 13-M03-L03
cim: Statikus tagok, absztrakt osztályok, interfészek
evfolyam: 1-13
modul: M03 – Objektumorientált programozás
orak: 33–36.
het:
oraszam: 4
elofeltetel: 13-M03-L02
leiras: Statikus osztályok és metódusok, absztrakt osztályok és metódusok, interfészek definiálása és megvalósítása.
---

# 13-M03-L03 · Statikus tagok, absztrakt osztályok, interfészek

## Célok

A lecke végére a tanuló:

- megkülönbözteti a példányszintű és az osztályszintű (`static`) tagokat, és tudja, mikor melyik kell;
- statikus segédosztályt ír (mint a `Math` vagy a `Console`), és statikus számlálót vezet az objektumokról;
- absztrakt osztályt és absztrakt metódust definiál, és érti, miért nem lehet belőle objektumot létrehozni;
- interfészt definiál és valósít meg, egy osztályban több interfészt is;
- interfész típusú listán polimorf módon dolgozik, és a beépített `IComparable<T>` interfésszel rendezhetővé teszi a saját osztályát;
- el tudja dönteni, hogy egy feladatban absztrakt osztály vagy interfész a jobb választás.

## Elmélet

### Példányszintű és statikus tagok

Eddig minden tulajdonság és metódus **egy-egy objektumhoz** tartozott: minden `Bankszamla`-nak saját `Egyenleg`-e van, és a `Befizet` mindig egy konkrét számlán dolgozik. Ezek a **példányszintű** tagok.

Néha viszont olyan adatra vagy műveletre van szükség, amely **az osztályhoz mint egészhez** tartozik, nem egy-egy objektumhoz:

- hány számlát nyitottunk eddig összesen?
- mennyi a közös kamatláb, amely minden számlára vonatkozik?
- egy számítás (pl. `Math.Sqrt`), amelyhez semmilyen objektum nem kell.

Ezeket a `static` kulcsszóval jelöljük. A statikus tagból **egyetlen példány** van, amelyen az összes objektum osztozik, és az **osztály nevén keresztül** érjük el:

```csharp
public class Bankszamla
{
    public static int NyitottSzamlak { get; private set; }   // közös számláló
    public static double Kamatlab { get; set; } = 0.03;      // közös beállítás

    public string Tulajdonos { get; }                        // példányszintű
    public int Egyenleg { get; private set; }

    public Bankszamla(string tulajdonos)
    {
        Tulajdonos = tulajdonos;
        NyitottSzamlak++;              // minden új objektum növeli a KÖZÖS számlálót
    }

    public void Kamatoz() => Egyenleg += (int)(Egyenleg * Kamatlab);

    public static bool ErvenyesSzamlaszam(string sz) => sz.Length == 24 && sz.All(char.IsDigit);
}

Console.WriteLine(Bankszamla.NyitottSzamlak);           // az osztály nevén keresztül
bool ok = Bankszamla.ErvenyesSzamlaszam("1177...");
```

Szabályok:

- Statikus metódusban **nincs `this`**, ezért nem érhetők el benne a példányszintű tagok. Honnan tudná, melyik számla egyenlegét kell nézni? Fordítva viszont működik: a példánymetódus eléri a statikus tagokat.
- A `Main` metódus és az eddig írt `static` segédmetódusaink is ilyenek, ezért lehetett objektum nélkül meghívni őket.

### Statikus osztály

Ha egy osztálynak **csak** statikus tagjai vannak, és nincs értelme objektumot létrehozni belőle, `static class`-ként definiáljuk. A beépített `Math`, `Console`, `File` és `Convert` is ilyen.

```csharp
public static class Atvalto
{
    public const double KmPerMerfold = 1.609344;          // const: fordításkor rögzített

    public static double MerfoldbolKm(double merfold) => merfold * KmPerMerfold;
    public static double CelsiusbolFahrenheit(double c) => c * 9 / 5 + 32;
}

double km = Atvalto.MerfoldbolKm(26.2);    // new Atvalto() – fordítási hiba
```

A `const` mező automatikusan statikus, és értéke csak fordításkor ismert állandó lehet (szám, szöveg). Ha az érték futáskor dől el (például fájlból olvassuk), de utána nem változhat, akkor `static readonly`.

### Absztrakt osztály

Az előző leckében az ősosztályból is lehetett objektumot létrehozni (`new Alkalmazott(...)`). Sokszor azonban az ős csak egy **általános fogalom**, amelyből önmagában nincs értelme objektumot csinálni. Mi lenne egy „síkidom” területe, ha nem tudjuk, kör-e vagy téglalap? Az ilyen őst **absztrakt** osztálynak jelöljük:

```csharp
public abstract class Sikidom
{
    public string Nev { get; }

    protected Sikidom(string nev) { Nev = nev; }       // konstruktora lehet – a leszármazottak hívják

    public abstract double Terulet();                  // absztrakt metódus: NINCS törzse
    public abstract double Kerulet();

    public override string ToString() =>               // rendes (nem absztrakt) metódus is lehet
        $"{Nev}: T = {Terulet():F2}, K = {Kerulet():F2}";
}

public class Kor : Sikidom
{
    public double Sugar { get; }
    public Kor(double r) : base("kör") { Sugar = r; }

    public override double Terulet() => Sugar * Sugar * Math.PI;   // KÖTELEZŐ megvalósítani
    public override double Kerulet() => 2 * Sugar * Math.PI;
}
```

- Az **absztrakt metódusnak** nincs törzse, csak „ígéret”: minden (nem absztrakt) leszármazott **köteles** `override`-dal megvalósítani. Ha kihagyja, fordítási hibát kap.
- Absztrakt osztályból **nem lehet objektumot** létrehozni (`new Sikidom(...)` hiba), de lehet **ilyen típusú változó és lista**: `List<Sikidom>`. Ez a többalakúság alapja.
- Az absztrakt osztályban lehetnek mezők, tulajdonságok, konstruktor és kész metódusok is. A közös részt itt írjuk meg, a kötelezően eltérőt absztraktnak hagyjuk.
- A `virtual` és az `abstract` különbsége: a `virtual` metódusnak **van** alapértelmezett működése, amelyet *felül lehet* írni; az `abstract` metódusnak **nincs**, ezért *felül kell* írni.

### Interfész

Az **interfész** (`interface`) egy **szerződés**: felsorolja, milyen metódusokat és tulajdonságokat kell tudnia annak az osztálynak, amely megvalósítja. Azt viszont nem mondja meg, hogyan. Neve megállapodás szerint nagy `I` betűvel kezdődik.

```csharp
public interface IKapcsolhato
{
    bool Bekapcsolva { get; }
    void Be();
    void Ki();
}

public interface IFogyaszto
{
    double Teljesitmeny();          // watt
}

public class Lampa : IKapcsolhato, IFogyaszto      // több interfész is megvalósítható
{
    public bool Bekapcsolva { get; private set; }
    public void Be() => Bekapcsolva = true;
    public void Ki() => Bekapcsolva = false;
    public double Teljesitmeny() => Bekapcsolva ? 9 : 0;
}
```

- Az interfész tagjai automatikusan `public`-ok és „absztraktak”; a megvalósító osztály `public` tagként írja meg őket, **`override` nélkül**.
- Egy osztálynak **csak egy őse** lehet, de **akárhány interfészt** megvalósíthat: `class Lampa : Eszkoz, IKapcsolhato, IFogyaszto` (az ős mindig elöl áll).
- Interfészből nem lehet objektum, de lehet **interfész típusú változó és lista**: `List<IKapcsolhato>`. Ebbe teljesen különböző osztályú objektumok is bekerülhetnek, ha teljesítik a szerződést, akár lámpa, akár kávéfőző.
- Hogy egy objektum megvalósít-e egy interfészt, azt az `is` vizsgálja: `if (e is IFogyaszto f) osszes += f.Teljesitmeny();`

### Beépített interfész: IComparable&lt;T&gt;

A `List<int>.Sort()` azért tudja rendezni a számokat, mert az `int` megvalósítja az `IComparable<int>` interfészt. Ha a saját osztályunk is megvalósítja, a lista `Sort()`-ja lambda nélkül is rendezi:

```csharp
public abstract class Sikidom : IComparable<Sikidom>
{
    ...
    public int CompareTo(Sikidom? masik) =>
        masik == null ? 1 : Terulet().CompareTo(masik.Terulet());
}

sikidomok.Sort();      // terület szerint növekvő
```

A `CompareTo` szerződése: negatív, ha `this` előrébb kerül; 0, ha egyenlők; pozitív, ha hátrébb.

### Absztrakt osztály vagy interfész?

| | Absztrakt osztály | Interfész |
|---|---|---|
| Kapcsolat | „… egy fajta …” (*is-a*): a kör egy fajta síkidom | „… képes valamire” (*can-do*): a lámpa kapcsolható |
| Hány lehet? | egy osztálynak csak **egy** őse | **több** interfész is |
| Tartalmazhat | mezőt, konstruktort, kész és absztrakt metódust | csak tagok leírását (mezőt és konstruktort nem) |
| Mikor? | rokon osztályoknak sok közös kódja van | nem rokon osztályoknak közös képessége van |
| Példa | `Sikidom` → `Kor`, `Teglalap` | `IKapcsolhato`: lámpa, tévé, konnektor |

A kettő jól megfér egymás mellett: `class Lampa : Eszkoz, IKapcsolhato, ISzabalyozhato`.

### Tipikus hibák

- **„An object reference is required for the non-static field …”**: statikus metódusból (pl. `Main`) példánytagot próbálunk elérni. Vagy objektumon keresztül hívjuk, vagy a tag is legyen statikus.
- **„Member … cannot be accessed with an instance reference”**: statikus tagot objektumon keresztül hívunk (`szamla.NyitottSzamlak`). Helyesen: `Bankszamla.NyitottSzamlak`.
- **„Cannot create an instance of the abstract type”**: `new Sikidom()`. Csak leszármazottból lehet objektum.
- **„… does not implement inherited abstract member …”** / **„… does not implement interface member …”**: lemaradt egy kötelező metódus, vagy eltér a neve, a paraméterei vagy a visszatérési típusa.
- **`override` interfészmetódusnál**: nem kell (hibát is ad). Az `override` az ősosztály `virtual`/`abstract` metódusaihoz kell.
- **Minden statikus**: ha minden tag `static`, elveszik az objektumok lényege. Statikus csak az legyen, ami valóban közös vagy objektumtól független.

## Példák

| Projekt | Mit mutat be |
|---|---|
| [peldak/FizetesStatikusAbsztrakt](peldak/FizetesStatikusAbsztrakt/) | `static class` segédmetódussal (készpénzes kerekítés 5 Ft-ra), statikus számlálók az absztrakt ősben, absztrakt `FizetesiMod` osztály absztrakt metódussal, három leszármazott (`Keszpenz`, `Bankkartya`, `SzepKartya`), sablonmetódus az ősben |
| [peldak/AlakzatokInterfesz](peldak/AlakzatokInterfesz/) | `ISikidom` és `IAtmeretezheto` interfész, több interfész egy osztályban, interfész típusú lista, `is` vizsgálat interfészre, interfész öröklése (`ISikidom : IComparable<ISikidom>`) és `Sort()` |

Futtatás: nyisd meg a `.csproj` fájlt Visual Studióban, majd F5.

## Feladatok

| Azonosító | Feladat | Nehézség | Megoldás |
|---|---|---|---|
| 13-M03-L03-F01 | [Mértékegység-átváltó](feladatok/F01-atvalto/) | ★☆☆ | [megoldás](megoldasok/F01-atvalto/) |
| 13-M03-L03-F02 | [Kerttervező](feladatok/F02-kerttervezo/) | ★★☆ | [megoldás](megoldasok/F02-kerttervezo/) |
| 13-M03-L03-F03 | [Okosotthon](feladatok/F03-okosotthon/) | ★★★ | [megoldás](megoldasok/F03-okosotthon/) |

## Kapcsolódó

- Előző lecke: 13-M03-L02 (Öröklődés és többalakúság)
- Következő modul: 13-M04 (Fájlkezelés és adatfeldolgozás)
- Tanmenet: [1-13](../../../tanmenet/1-13-2felev.md)
