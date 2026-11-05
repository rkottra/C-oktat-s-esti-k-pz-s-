---
id: 14-M05-L03
cim: Egységtesztelés
evfolyam: 2-14
modul: M05 – Szoftverminőség és tesztelés
orak: 61–64.
het:
oraszam: 4
elofeltetel: 14-M05-L02
leiras: Tesztprojekt Visual Studióban (MSTest), Assert, tesztesetek tervezése, tesztelhető kód írása.
---

# 14-M05-L03 · Egységtesztelés

## Célok

A lecke végére a tanuló:

- elmagyarázza, mi az egységteszt, és miben különbözik a „kézi kipróbálástól”;
- egy osztálykönyvtár mellé MSTest-tesztprojektet hoz létre Visual Studióban, és projekthivatkozással összeköti őket;
- az Arrange–Act–Assert szerkezetben teszteket ír `Assert`, `CollectionAssert` és `[DataRow]` használatával;
- ekvivalencia-osztályok és határértékek alapján teszteseteket tervez;
- teszteli, hogy a kód a megfelelő kivételt dobja;
- meglévő kódhoz írt tesztekkel hibát talál, javítja, és igazolja a javítást;
- felismeri, mitől lesz egy kód nehezen tesztelhető, és hogyan lehet ezen változtatni.

## Elmélet

### Mi az egységteszt?

Eddig úgy ellenőriztük a programjainkat, hogy elindítottuk, beírtunk valamit, és ránéztünk a kimenetre. Ez lassú, minden módosítás után újra kellene csinálni, és könnyű kihagyni valamit.

Az **egységteszt** (unit test) egy kis, automatikusan futó program, amely a kód egy **egységét** (jellemzően egy metódusát) meghívja adott bemenettel,
és ellenőrzi, hogy az eredmény megegyezik-e az elvárttal. Egy kattintással akár több száz teszt lefut néhány másodperc alatt: zöld = rendben, piros = hiba.

Előnyei:

- **Biztonsági háló** a módosításhoz és a refaktoráláshoz (14-M05-L02): ha valamit elrontunk, egy teszt azonnal pirosra vált.
- **Élő dokumentáció**: a tesztekből kiolvasható, hogyan kell használni az osztályt, és mit csinál határesetekben.
- **Hibakeresés**: egy megtalált hibához először írunk egy (piros) tesztet, majd javítunk, amíg zöld nem lesz – így a hiba nem jöhet vissza észrevétlenül.

A tesztelés szintjei (alulról felfelé egyre kevesebb, lassabb és drágább teszt – ez a „tesztpiramis”):

| Szint | Mit vizsgál | Példa |
|---|---|---|
| **Egységteszt** | egy osztály / metódus, elszigetelten | `Kosar.Szallitas()` 14 999 Ft-ra 1490-et ad |
| Integrációs teszt | több rész együttműködése (pl. kód + adatbázis) | a repository tényleg elmenti a rekordot MySQL-be |
| Rendszerteszt | a teljes alkalmazás | a WPF-ablakban felvett termék megjelenik a listában |
| Átvételi teszt | a megrendelő igényei teljesülnek-e | a felhasználó végig tud menni egy rendelésen |

### Tesztprojekt létrehozása Visual Studióban

A tesztelendő kód egy **osztálykönyvtárban** (*Class Library*) van, a tesztek egy külön **tesztprojektben**. A tesztprojekt hivatkozik az osztálykönyvtárra, fordítva nem.

1. A megoldáshoz (*Solution*) jobb klikk → *Add → New Project… → **MSTest Test Project***, .NET 8. Szokásos név: a tesztelt projekt neve + `Tesztek` (pl. `KosarLogikaTesztek`).
2. A tesztprojektben: *Dependencies* → jobb klikk → *Add Project Reference…* → pipáld be a tesztelt projektet.
3. A tesztfájl elejére: `using KosarLogika;`
4. Futtatás: *Test → Test Explorer* (**Ctrl+E, T**) → *Run All Tests* (**Ctrl+R, A**). Parancssorból: `dotnet test`.

A tesztprojekt `.csproj`-jában a lényeg:

```xml
<PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.12.0" />
<PackageReference Include="MSTest.TestAdapter" Version="3.6.4" />
<PackageReference Include="MSTest.TestFramework" Version="3.6.4" />
...
<ProjectReference Include="..\KosarLogika\KosarLogika.csproj" />
```

> Ha a tesztelendő kód egy konzolos vagy WPF-projektben van, a tesztprojekt arra is hivatkozhat. Tisztább megoldás azonban a logikát osztálykönyvtárba (vagy legalább külön osztályba) tenni – ez a rétegezés (14-M05-L01) haszna.

### Egy teszt felépítése

```csharp
[TestClass]                                   // tesztosztály
public class KosarTesztek
{
    [TestMethod]                              // egy teszt
    public void Reszosszeg_KetKulonbozoTetel_OsszeadjaAzArakat()
    {
        // Arrange – előkészítés
        var kosar = new Kosar();
        kosar.Hozzaad("Bögre", 2490, 2);
        kosar.Hozzaad("Teafilter", 890, 3);

        // Act – a tesztelt művelet végrehajtása
        int eredmeny = kosar.Reszosszeg();

        // Assert – ellenőrzés: ELVÁRT, TÉNYLEGES sorrendben!
        Assert.AreEqual(7650, eredmeny);
    }
}
```

**Elnevezés:** `Metodus_Helyzet_ElvartEredmeny` – a Test Explorerben a névből látszik, mi romlott el.

| Attribútum | Jelentése |
|---|---|
| `[TestClass]` | tesztosztály (publikus) |
| `[TestMethod]` | tesztmetódus (`public void`, paraméter nélkül – vagy `DataRow`-val) |
| `[DataRow(…)]` | egy adatsor a paraméteres teszthez; minden sor külön tesztként fut |
| `[TestInitialize]` | **minden** teszt előtt lefut (pl. új, üres objektum létrehozása) |
| `[TestCleanup]` | minden teszt után lefut |

### Assert-ek

| Hívás | Mikor sikeres? |
|---|---|
| `Assert.AreEqual(elvart, tenyleges)` | a két érték egyenlő |
| `Assert.AreEqual(1.5, x, 0.001)` | tört számoknál: legfeljebb 0,001 az eltérés |
| `Assert.AreNotEqual(a, b)` | nem egyenlők |
| `Assert.IsTrue(feltetel)` / `Assert.IsFalse(feltetel)` | a feltétel igaz / hamis |
| `Assert.IsNull(x)` / `Assert.IsNotNull(x)` | `null` / nem `null` |
| `Assert.ThrowsException<T>(() => …)` | a lambda `T` típusú kivételt dob (visszaadja a kivételt további vizsgálatra) |
| `CollectionAssert.AreEqual(elvartLista, lista)` | ugyanazok az elemek, ugyanabban a sorrendben |
| `CollectionAssert.Contains(lista, elem)` | az elem benne van |
| `StringAssert.Contains(szoveg, resz)` | a szöveg tartalmazza a részt |

Minden Assert-nek lehet utolsó paramétere egy üzenet, amely bukáskor megjelenik: `Assert.IsTrue(eros, "Az Alma1234 erős jelszó.")`.

### Paraméteres teszt: DataRow

Ha ugyanazt kell több bemenettel kipróbálni, nem másolunk tesztet (DRY!):

```csharp
[TestMethod]
[DataRow(14999, 1490)]   // határ alatt
[DataRow(15000, 0)]      // pontosan a határon
[DataRow(15001, 0)]      // határ fölött
public void Szallitas_ReszosszegFuggvenyeben(int egysegar, int elvartDij)
{
    var kosar = new Kosar();
    kosar.Hozzaad("Termék", egysegar);

    Assert.AreEqual(elvartDij, kosar.Szallitas());
}
```

### Kivétel tesztelése

```csharp
[TestMethod]
public void Vegosszeg_TulNagyKedvezmeny_Kivetel()
{
    var kosar = new Kosar();
    kosar.Hozzaad("Kabát", 20000);

    var kivetel = Assert.ThrowsException<ArgumentOutOfRangeException>(() => kosar.Vegosszeg(51));
    Assert.AreEqual("kedvezmenySzazalek", kivetel.ParamName);
}
```

Az `Assert.ThrowsException` a **pontos** típust várja (`ArgumentException` helyett dobott `ArgumentOutOfRangeException` piros!).
Régebbi kódokban az `[ExpectedException(typeof(…))]` attribútummal is találkozhatsz; az újabb MSTest-változatok már az `Assert.ThrowsException` (illetve `Assert.ThrowsExactly`) használatát javasolják,
mert pontosan megmutatja, melyik sornak kell kivételt dobnia.

### Tesztesetek tervezése

Mindent nem lehet kipróbálni, ezért okosan választunk:

- **Ekvivalencia-osztályok**: a bemeneteket csoportokra bontjuk, amelyeken belül a program ugyanúgy viselkedik, és mindegyikből választunk egy képviselőt.
  Mozijegy: gyerek (0–13), felnőtt (14–64), nyugdíjas (65–120), érvénytelen (< 0, > 120).
- **Határértékek**: a hibák nagy része a határokon van (`<` vagy `<=`?). A határ mindkét oldalát és magát a határt is teszteljük: 13, 14 – 64, 65 – 0, -1 – 120, 121.
- **Speciális bemenetek**: üres szöveg, `null`, üres lista, 0, negatív szám, nagyon nagy szám.
- **Hibás használat**: a dokumentált kivételek tényleg keletkeznek-e, és utána változatlan-e az objektum állapota?

A tesztesetekről érdemes előbb táblázatot készíteni (eset – bemenet – elvárt eredmény), és csak utána kódolni.

### Jó tesztek tulajdonságai

| Tulajdonság | Jelentése |
|---|---|
| **Gyors** | ezredmásodpercek alatt fut, hogy sokszor futtassuk |
| **Független** | nem függ más tesztektől és a futási sorrendtől – minden teszt maga készíti elő az adatait |
| **Megismételhető** | mindig ugyanazt adja: nem függ az aktuális dátumtól, fájltól, adatbázistól, hálózattól |
| **Önellenőrző** | zöld vagy piros – nem kell a kimenetet szemmel nézni |
| **Egy dolgot vizsgál** | ha piros, a nevéből tudjuk, mi a baj |

### Tesztelhető kód

Néhány dolog megnehezíti vagy lehetetlenné teszi a tesztelést – ezeket érdemes kerülni az üzleti logikában:

| Nehezen tesztelhető | Helyette |
|---|---|
| `Console.ReadLine()` / `Console.WriteLine()` a számoló metódusban | paraméter és visszatérési érték; a kiírás a felület dolga |
| `DateTime.Now` / `DateTime.Today` a metódus belsejében | a dátumot (napot) paraméterként kapja: `Ar(int eletkor, DayOfWeek nap)` |
| a metódus maga nyit fájlt vagy adatbázist | a repositoryt interfészként, kívülről kapja (14-M05-L01) – a tesztben a `MemoriaEpuletRepository` helyettesíti |
| `return -1;` hibakódként | kivétel, amelyet `Assert.ThrowsException`-nel tesztelünk |
| egy hatalmas `Main` metódus | kis, egyfeladatos, publikus metódusok (14-M05-L02) |

### Tipikus hibák

- **Felcserélt `AreEqual` paraméterek**: `Assert.AreEqual(eredmeny, 7650)` is lefut, de bukáskor az üzenet fordítva mutatja az elvárt és a kapott értéket.
- **A teszt a kódból számolja az elvárt értéket** (`Assert.AreEqual(kosar.Reszosszeg(), kosar.Reszosszeg())`): ez mindig zöld. Az elvárt értéket a specifikációból, kézzel számoljuk ki.
- **Assert nélküli teszt**: ha nincs benne ellenőrzés, akkor is zöld, ha a kód rossz.
- **`try { … } catch { }` a tesztben**: elnyeli a hibát – a teszt hamisan zöld. Kivételre `Assert.ThrowsException` való.
- **Egymásra épülő tesztek** (közös mező, amit az egyik teszt módosít): futási sorrendtől függően bukhatnak. Használj `[TestInitialize]`-t vagy helyi változót.
- **A teszt nem látja az osztályt**: hiányzik a *Project Reference*, vagy az osztály nem `public`.
- **Nem jelenik meg a teszt a Test Explorerben**: az osztály nem `public`, hiányzik a `[TestClass]` / `[TestMethod]`, vagy a metódus nem `public void`. Fordítsd újra (*Build*).

## Példák

| Projekt | Mit mutat be |
|---|---|
| [peldak/KosarLogika](peldak/KosarLogika/) | A tesztelt osztálykönyvtár: webshop-kosár tételekkel, szállítási díjjal (15 000 Ft-tól ingyenes), kedvezménnyel és bemenet-ellenőrzéssel |
| [peldak/KosarLogikaTesztek](peldak/KosarLogikaTesztek/) | MSTest-tesztprojekt (21 teszteset): `TestInitialize`, AAA-szerkezet, `DataRow`, határértékek, `Assert.ThrowsException`, `CollectionAssert` |

Futtatás: nyisd meg mindkét projektet egy megoldásban (vagy csak a tesztprojektet – a hivatkozott projektet a Visual Studio magától betölti), majd *Test Explorer → Run All*.
Parancssorból: `dotnet test peldak/KosarLogikaTesztek`.
Próbáld ki: rontsd el szándékosan a `Kosar.Szallitas()` feltételét (`>=` helyett `>`), és nézd meg, melyik teszt lesz piros!

## Feladatok

A feladatokban **meglévő kódhoz** írsz teszteket. A kiinduló kódokban szándékosan elrejtett hibák vannak: a teszteknek kell megtalálniuk őket, nem a kód olvasásának.

| Azonosító | Feladat | Nehézség | Megoldás |
|---|---|---|---|
| 14-M05-L03-F01 | [Mozijegy – határértékek tesztelése](feladatok/F01-mozijegy/) | ★☆☆ | [megoldás](megoldasok/F01-mozijegy/) |
| 14-M05-L03-F02 | [Jelszóerősség-ellenőrző](feladatok/F02-jelszo-ellenorzo/) | ★★☆ | [megoldás](megoldasok/F02-jelszo-ellenorzo/) |
| 14-M05-L03-F03 | [Bankszámla – teljes tesztkészlet](feladatok/F03-bankszamla/) | ★★★ | [megoldás](megoldasok/F03-bankszamla/) |

## Kapcsolódó

- Előző lecke: 14-M05-L02 (Tiszta kód és UML)
- Következő lecke: 14-M06-L01 (Konzolos vizsgafeladat)
- Tanmenet: [2-14](../../../tanmenet/2-14.md)
