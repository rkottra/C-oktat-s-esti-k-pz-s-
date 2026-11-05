---
id: 14-M04-L04
cim: Adatimport és -export (CSV, JSON, XML)
evfolyam: 2-14
modul: M04 – WPF + EF Core adatkezelő alkalmazás
orak: 49–52.
het:
oraszam: 4
elofeltetel: 14-M04-L03
leiras: CSV importálása adatbázisba (AddRange), exportálás JSON és XML formátumba.
---

# 14-M04-L04 · Adatimport és -export (CSV, JSON, XML)

## Célok

A lecke végére a tanuló:

- fájlt választat a felhasználóval (`OpenFileDialog`, `SaveFileDialog`);
- CSV-fájlt sorról sorra feldolgoz, a hibás sorokat kihagyja, és a kapcsolt táblát név alapján tölti ki;
- az új rekordokat `AddRange`-dzsel, egyetlen `SaveChanges`-szel menti;
- adatokat JSON formátumba exportál (`System.Text.Json`), és onnan vissza is olvas;
- adatokat XML formátumba exportál (`XmlSerializer`), és érti, miért kell exporthoz külön (DTO) osztály.

## Elmélet

### A három formátum

| | CSV | JSON | XML |
|---|---|---|---|
| szerkezet | táblázat (sorok, elválasztó karakter) | objektumok, tömbök, egymásba ágyazva | elemek (tagek), attribútumok, fa |
| ember olvassa? | igen, Excel is megnyitja | igen | igen, de bőbeszédű |
| tipikus használat | táblázatkezelőből jövő adat | webes API-k, konfiguráció | régebbi rendszerek, konfiguráció, XAML |
| .NET-ben | kézzel: `Split` | `System.Text.Json` | `System.Xml.Serialization`, `System.Xml.Linq` |

Ugyanaz a könyv háromféleképpen:

```text
Egri csillagok;Gárdonyi Géza;1901;3490;12;Móra
```
```json
{ "Cim": "Egri csillagok", "Szerzo": "Gárdonyi Géza", "KiadasEve": 1901, "Ar": 3490, "Keszlet": 12, "Kiado": "Móra" }
```
```xml
<Konyv ev="1901">
  <Cim>Egri csillagok</Cim>
  <Szerzo>Gárdonyi Géza</Szerzo>
  ...
</Konyv>
```

**XML-szabályok röviden:** pontosan egy gyökérelem; minden nyitó elemnek van záró párja (vagy önzáró: `<elem />`); az elemek nem fedhetik át egymást; kis- és nagybetű különbözik; a `<`, `>`, `&`, `"`, `'` karakterek helyett `&lt;`, `&gt;`, `&amp;`, `&quot;`, `&apos;` áll. Az a dokumentum, amely ezeket betartja, *jól formált* (well-formed). A WPF felületleíró nyelve, a XAML is XML.

### Fájlválasztó ablakok

```csharp
using Microsoft.Win32;

var dialogus = new OpenFileDialog { Filter = "CSV-fájl (*.csv)|*.csv|Minden fájl (*.*)|*.*" };
if (dialogus.ShowDialog() != true) return;     // Mégse → null/false
string fajl = dialogus.FileName;
```

A `SaveFileDialog` ugyanígy működik; a `FileName` tulajdonsággal fájlnevet is javasolhatunk.

### CSV-import lépései

1. Fájl beolvasása: `File.ReadAllLines(fajl, Encoding.UTF8)` – `try-catch (IOException)`, mert a fájl lehet nyitva Excelben.
2. Fejléc kihagyása: `sorok.Skip(1)`.
3. Soronként: `Split(';')`, a mezőszám és a számok ellenőrzése `TryParse`-szal. **A hibás sort kihagyjuk és megszámoljuk** – egy rossz sor miatt ne vesszen el az egész import.
4. Duplikátumok kiszűrése (már az adatbázisban van, vagy a fájlban kétszer szerepel).
5. **Kapcsolt tábla:** a CSV-ben a kiadó *neve* szerepel, nem az `Id`-je. Név alapján keressük; ha nincs ilyen, újat hozunk létre:
   ```csharp
   Kiado? kiado = kiadok.FirstOrDefault(k => k.Nev.Equals(kiadoNev, StringComparison.OrdinalIgnoreCase));
   if (kiado == null)
   {
       kiado = new Kiado { Nev = kiadoNev };
       kiadok.Add(kiado);                         // a következő sor már megtalálja
   }
   ujKonyvek.Add(new Konyv { Cim = cim, ..., Kiado = kiado });   // objektum, nem Id!
   ```
   Az új kiadónak még nincs `Id`-je, ezért a navigációs tulajdonságot adjuk meg. Mentéskor az EF előbb a kiadót szúrja be, majd annak `Id`-jével a könyvet.
6. Mentés egyszerre:
   ```csharp
   db.Konyvek.AddRange(ujKonyvek);   // egy hívás, sok rekord
   db.SaveChanges();                 // egy tranzakció: vagy minden bekerül, vagy semmi
   ```
7. Visszajelzés: hány rekord került be, hány volt hibás vagy már létező.

A meglévő rekordokat **egyszer** töltjük a memóriába (lista vagy `Dictionary`), és ebben keresünk – soronkénti adatbázis-lekérdezés helyett.

### Export: miért kell DTO?

Ha az entitásokat közvetlenül szerializálnánk, két gond adódik:

- **körkörös hivatkozás:** `Konyv → Kiado → Konyvek → Konyv → …` – a `JsonSerializer` *„A possible object cycle was detected”* kivételt dob, az `XmlSerializer` pedig nem is tudja kezelni;
- a fájlba az adatbázis belső részletei (`Id`, `KiadoId`) is bekerülnének.

Megoldás: egy egyszerű, „lapos” **DTO** (*Data Transfer Object*) osztály, amelyet LINQ `Select`-tel töltünk ki:

```csharp
[XmlType("Konyv")]
public class KonyvExport
{
    public string Cim { get; set; } = "";
    [XmlAttribute("ev")] public int KiadasEve { get; set; }
    public string Kiado { get; set; } = "";     // a kiadó neve, szövegként
    ...
}

List<KonyvExport> lista = konyvek.Select(k => new KonyvExport
{
    Cim = k.Cim, KiadasEve = k.KiadasEve, Kiado = k.Kiado?.Nev ?? "", ...
}).ToList();
```

### JSON: System.Text.Json

```csharp
var beallitasok = new JsonSerializerOptions
{
    WriteIndented = true,                                  // tördelt, olvasható
    Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)  // "Gárdonyi", nem "Gárdonyi"
};
File.WriteAllText(fajl, JsonSerializer.Serialize(lista, beallitasok), Encoding.UTF8);

// visszaolvasás
var vissza = JsonSerializer.Deserialize<List<KonyvExport>>(File.ReadAllText(fajl), beallitasok);
```

| Lehetőség | Hatás |
|---|---|
| `PropertyNamingPolicy = JsonNamingPolicy.CamelCase` | a kulcsok kisbetűvel kezdődnek (`"cim"`) |
| `[JsonPropertyName("cylinders")]` | egy tulajdonság egyedi neve a JSON-ban |
| `[JsonIgnore]` | a tulajdonság kimarad |

Hibás vagy nem várt szerkezetű JSON beolvasásakor `JsonException` keletkezik.

### XML: XmlSerializer

```csharp
var szerializalo = new XmlSerializer(typeof(List<KonyvExport>), new XmlRootAttribute("Konyvek"));
using (var iro = new StreamWriter(fajl, false, Encoding.UTF8))
{
    szerializalo.Serialize(iro, lista);
}
```

- Az `XmlSerializer`-nek **publikus osztály** és **paraméter nélküli konstruktor** kell.
- Alapból a gyökér `<ArrayOfKonyvExport>` lenne – ezt az `XmlRootAttribute`, az elemek nevét az `[XmlType]` állítja be.
- `[XmlAttribute]`: elem helyett attribútum lesz; `[XmlElement("Ev")]`: más elemnév; `[XmlIgnore]`: kimarad.
- A `using` blokk végén a fájl lezáródik – enélkül a fájl üres vagy csonka maradhat.

Alternatíva kisebb feladatokra a LINQ to XML: `new XElement("Konyvek", lista.Select(k => new XElement("Konyv", new XAttribute("ev", k.KiadasEve), new XElement("Cim", k.Cim))))`, majd `.Save(fajl)`.

### Tipikus hibák

- **Ékezetek helyett kérdőjelek / krix-krax:** a CSV nem UTF-8 kódolású (Excelből „CSV UTF-8” formátumban ments), vagy olvasáskor nincs megadva a kódolás.
- **„A possible object cycle was detected”:** entitást szerializálsz navigációs tulajdonsággal – használj DTO-t.
- **`InvalidOperationException` az `XmlSerializer` létrehozásakor:** az osztály nem publikus, vagy nincs paraméter nélküli konstruktora.
- **`IndexOutOfRangeException` a CSV-nél:** rövidebb sor (kevesebb mező) – előbb a `Length`-et ellenőrizd.
- **Minden könyv kétszer került be:** az import duplikátumvizsgálat nélkül kétszer futott.
- **Új kiadó kétszer jött létre:** az újonnan létrehozott kiadót nem tetted be a keresési listába.

## Példák

| Projekt | Mit mutat be |
|---|---|
| [peldak/KonyvesboltImportExport](peldak/KonyvesboltImportExport/) | CSV-import (`konyvek-import.csv`: hibás sor, duplikátum, új kiadó is van benne) `AddRange`-dzsel; a látható (szűrt) könyvek exportja JSON-ba és XML-be DTO-val |

Futtatás: XAMPP MySQL, F5. A „CSV importálása…” gomb a program mappájában nyílik meg, ahová a minta CSV-fájl fordításkor bemásolódik. Az import második futtatása már semmit nem vesz fel (minden duplikátum). Saját alkalmazásodban ezek a gombok egyszerűen az L03-as ablakba kerülnek.

## Feladatok

| Azonosító | Feladat | Nehézség | Megoldás |
|---|---|---|---|
| 14-M04-L04-F01 | [Bevételezés CSV-fájlból](feladatok/F01-csv-import/) | ★★☆ | [megoldás](megoldasok/F01-csv-import/) |
| 14-M04-L04-F02 | [Raktárkészlet exportja JSON-ba és XML-be](feladatok/F02-json-xml-export/) | ★★☆ | [megoldás](megoldasok/F02-json-xml-export/) |

## Kapcsolódó

- Előző lecke: 14-M04-L03 (Szűrés, keresés, validáció, kivételkezelés)
- Következő lecke: 14-M05-L01 (Rétegzett architektúra, tervezési minták)
- Tanmenet: [2-14](../../../tanmenet/2-14.md)
