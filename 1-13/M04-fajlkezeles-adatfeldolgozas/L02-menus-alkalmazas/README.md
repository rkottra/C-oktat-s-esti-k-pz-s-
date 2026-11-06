---
id: 13-M04-L02
cim: Menüs konzolos alkalmazás
evfolyam: 1-13
modul: M04 – Fájlkezelés és adatfeldolgozás
orak: 41–44.
het:
oraszam: 4
elofeltetel: 13-M04-L01
leiras: Többfunkciós konzolos program menüvel: adatbeolvasás fájlból, lekérdezések, kiíratás (Országok feladat).
---

# 13-M04-L02 · Menüs konzolos alkalmazás

## Célok

A lecke végére a tanuló:

- egy fájlból beolvasott adathalmazra **menüvel vezérelt** konzolos programot ír, amely addig fut, amíg a felhasználó ki nem lép;
- minden menüpontot külön **metódusba** szervez, és a beolvasást, a lekérdezést és a kiírást szétválasztja;
- ellenőrzött beolvasó segédmetódusokat (`BekerEgesz`, `BekerSzoveg`) ír és használ újra;
- a felhasználó által módosított adatokat (új rekord, törlés) **visszamenti** a fájlba;
- a hiányzó vagy hibás adatfájlt `try`–`catch`-csel kezeli, hogy a program ne álljon le.

## Elmélet

### A menüs program szerkezete

Minden menüs adatfeldolgozó program ugyanarra a vázra épül:

```
1. Indulás:      adatok beolvasása a fájlból EGYSZER → List<T>
2. Ciklus:       menü kiírása → választás bekérése → a választott metódus meghívása
3. Kilépés:      (ha kell) adatok visszamentése a fájlba → elköszönés
```

```csharp
List<Orszag> orszagok = Beolvas("europa.csv");

bool fut = true;
while (fut)
{
    MenuKiir();
    string valasztas = Console.ReadLine() ?? "";
    switch (valasztas)
    {
        case "1": Listaz(orszagok); break;
        case "2": KeresNevre(orszagok); break;
        case "3": Statisztika(orszagok); break;
        case "0": fut = false; break;
        default: Console.WriteLine("Nincs ilyen menüpont."); break;
    }
}
```

Fontos szabályok:

- **A fájlt egyszer olvassuk be**, a program elején. Ha minden menüpont újra beolvasná, lassú lenne, és elvesznének a menet közbeni módosítások.
- **Egy menüpont – egy metódus.** A `switch` csak elosztó: nem számol és nem ír ki hosszan. Így a program áttekinthető, a hibák könnyen megtalálhatók, és egy menüpont önállóan is kipróbálható.
- A metódusok **paraméterként** kapják a listát (`static void Listaz(List<Orszag> lista)`), nem globális változóként érik el.

### Kényelmi elemek

```csharp
static void Varakozas()
{
    Console.WriteLine("\nNyomj Entert a folytatáshoz...");
    Console.ReadLine();
    Console.Clear();                     // tiszta képernyő a menü előtt
}
```

- **Szünet és képernyőtörlés:** a menüpont eredménye ne tűnjön el azonnal a következő menü alatt.
- **Színek:** `Console.ForegroundColor = ConsoleColor.Yellow;` … `Console.ResetColor();`. A hibaüzenet piros, a fejléc sárga.
- **Táblázatos kiírás:** igazított mezőkkel (`{nev,-20}{nepesseg,12:N0}`) és egy fejlécsorral.
- **Almenü:** egy menüpont maga is indíthat egy kisebb menüt (például *Rendezés → 1 – név szerint, 2 – terület szerint*). Ez ugyanaz a minta, egy metóduson belül.

### Ellenőrzött beolvasás segédmetódusokkal

A felhasználó bármit begépelhet. A beolvasást egyszer írjuk meg jól, és mindenhol azt hívjuk:

```csharp
static int BekerEgesz(string kerdes, int min, int max)
{
    while (true)
    {
        Console.Write(kerdes);
        if (int.TryParse(Console.ReadLine(), out int x) && x >= min && x <= max)
            return x;
        Hiba($"{min} és {max} közötti egész számot adj meg!");
    }
}

static string BekerSzoveg(string kerdes)
{
    while (true)
    {
        Console.Write(kerdes);
        string s = (Console.ReadLine() ?? "").Trim();
        if (s != "")
            return s;
        Hiba("Nem lehet üres!");
    }
}
```

### Adatok módosítása és visszamentése

Ha a program új rekordot vesz fel vagy töröl, a változás csak a memóriában lévő listában történik meg. Hogy a következő indításkor is meglegyen, **vissza kell írni a fájlba**, ugyanabban a formátumban, fejléccel együtt:

```csharp
static void Ment(List<Orszag> lista, string fajlnev)
{
    List<string> sorok = new List<string> { "Nev;Fovaros;Terulet;Nepesseg;Gdp" };
    foreach (Orszag o in lista)
        sorok.Add(o.CsvSor());           // az osztály maga tudja, hogyan néz ki CSV-ként
    File.WriteAllLines(fajlnev, sorok);
}
```

- Az osztálynak érdemes egy `CsvSor()` metódust írni, a konstruktor párját: a konstruktor CSV-sorból objektumot csinál, a `CsvSor()` objektumból CSV-sort.
- A mentés történhet minden módosítás után azonnal, vagy kilépéskor egyszer. Ha kilépéskor mentünk, és a program közben hibával leáll, a módosítások elvesznek.
- Valós számot `CultureInfo.InvariantCulture`-rel írjunk ki, hogy ugyanúgy visszaolvasható legyen: `o.Terulet.ToString(CultureInfo.InvariantCulture)`.
- **Figyelem:** a program a `bin\Debug\net8.0` mappában lévő **másolatot** módosítja, nem a projektben lévő eredeti fájlt. Új fordításnál a *Copy if newer* beállítás felülírhatja a módosított másolatot.

### Hibakezelés a fájlnál

```csharp
static List<Orszag> Beolvas(string fajlnev)
{
    List<Orszag> lista = new List<Orszag>();
    try
    {
        string[] sorok = File.ReadAllLines(fajlnev);
        for (int i = 1; i < sorok.Length; i++)
            if (sorok[i].Trim() != "")
                lista.Add(new Orszag(sorok[i]));
    }
    catch (FileNotFoundException)
    {
        Hiba($"Nem található a(z) {fajlnev} fájl – üres listával indulunk.");
    }
    catch (FormatException)
    {
        Hiba($"Hibás adat a(z) {fajlnev} fájlban.");
    }
    return lista;
}
```

A `try` blokkban lévő kód hibája esetén a végrehajtás a megfelelő `catch` ágra ugrik, és a program fut tovább. Tipikus kivételek: `FileNotFoundException` (nincs fájl), `FormatException` (nem szám ott, ahol számnak kellene lennie), `IndexOutOfRangeException` (kevesebb mező a sorban). A `try`–`catch` nem helyettesíti a `TryParse`-t: a felhasználói bevitelt ellenőrizzük, a kivételkezelés a váratlan hibákra való.

### Tipikus hibák

- **A menü csak egyszer fut le:** hiányzik a ciklus, vagy a `fut = false` rossz ágban van.
- **`break` a `switch`-ben, de a ciklus nem áll le:** a `case "0": break;` csak a `switch`-ből lép ki, a `while`-ból nem. Kell egy `bool fut` változó (vagy `return`).
- **Minden menüpontban újra beolvassuk a fájlt:** a felvett adatok „eltűnnek”.
- **Mentés után nem lehet visszaolvasni:** más elválasztóval, fejléc nélkül vagy tizedesvesszővel mentettünk, mint ahogy a beolvasás várja.
- **Kis- és nagybetű:** a felhasználó `budapest`-et ír, a fájlban `Budapest` van. Hasonlítsunk `ToLower()`-rel vagy `StringComparison.OrdinalIgnoreCase`-zel.

## Példák

| Projekt | Mit mutat be |
|---|---|
| [peldak/MenuVaz](peldak/MenuVaz/) | újrahasznosítható menüváz adat nélkül: menüciklus, `switch` elosztó, `BekerEgesz` / `BekerSzoveg` / `Hiba` / `Varakozas` segédmetódusok, színes fejléc, almenü |
| [peldak/OrszagokMenu](peldak/OrszagokMenu/) | az Országok feladat menüvel az `europa.csv`-n: listázás, keresés névrészletre és fővárosra, népesség szerinti szűrés, statisztika, rendezés almenüből, új ország felvétele, törlés, mentés `CsvSor()`-ral, hibakezelés |

Futtatás: nyisd meg a `.csproj` fájlt Visual Studióban, majd F5.

## Feladatok

| Azonosító | Feladat | Nehézség | Megoldás |
|---|---|---|---|
| 13-M04-L02-F01 | [Iskolai könyvtár katalógusa](feladatok/F01-konyvtar-katalogus/) | ★★☆ | [megoldás](megoldasok/F01-konyvtar-katalogus/) |
| 13-M04-L02-F02 | [Edzésnapló](feladatok/F02-edzesnaplo/) | ★★☆ | [megoldás](megoldasok/F02-edzesnaplo/) |
| 13-M04-L02-F03 | [Osztálynapló két fájlból](feladatok/F03-osztalynaplo/) | ★★★ | [megoldás](megoldasok/F03-osztalynaplo/) |

## Kapcsolódó

- Előző lecke: 13-M04-L01 (Szöveges fájlok és CSV feldolgozása)
- Következő lecke: 13-M04-L03 (Dátum- és időkezelés, típuskonverziók)
- Tanmenet: [1-13](../../../tanmenet/1-13-2felev.md)
