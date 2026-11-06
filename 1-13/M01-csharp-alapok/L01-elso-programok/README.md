---
id: 13-M01-L01
cim: Első C# programok: típusok, operátorok, konzol
evfolyam: 1-13
modul: M01 – C# alapok
orak: 1–4.
het:
oraszam: 4
elofeltetel: 
leiras: Visual Studio és a C# program szerkezete, egyszerű típusok, változók, operátorok, kiírás és bekérés a konzolon, megjegyzések.
---

# 13-M01-L01 · Első C# programok: típusok, operátorok, konzol

## Célok

A lecke végére a tanuló:

- Visual Studióban új .NET 8 konzolos projektet hoz létre, lefordítja és futtatja;
- felismeri a C# program szerkezetét (klasszikus `Main` és felső szintű utasítások), és összeveti a Pythonnal;
- a megfelelő egyszerű típussal deklarál változókat, és tudja, miért nem változhat egy változó típusa;
- helyesen használja az aritmetikai, relációs, logikai és értékadó operátorokat, különösen az egész osztást;
- formázottan ír a konzolra (szövegbe ágyazás, szélesség, tizedesjegyek, színek, kurzorpozíció), és adatot kér be (`ReadLine` + `Parse`);
- megjegyzéseket ír, és töréspont segítségével lépésenként követi a program futását.

## Elmélet

### Pythonból C#-ra – a legfontosabb különbségek

Az 1. félévben Pythonban tanultuk a programozás alapjait. A **gondolkodás ugyanaz** (változó, elágazás, ciklus, függvény), a **leírás** más:

| | Python | C# |
|---|---|---|
| Futtatás | értelmező (interpreter) sorról sorra futtatja | a **fordító** (compiler) előbb lefordítja az egész programot; ha hiba van, el sem indul |
| Típusok | dinamikus: `x = 5`, később `x = "alma"` is mehet | **statikus**: `int x = 5;` – `x` mindig `int` marad |
| Utasítás vége | sorvége | **pontosvessző** `;` |
| Blokk | behúzás (`:` után) | **kapcsos zárójel** `{ … }` (a behúzás csak az olvashatóságot segíti) |
| Kiírás | `print("Szia")` | `Console.WriteLine("Szia");` |
| Bekérés | `x = int(input("x = "))` | `Console.Write("x = "); int x = int.Parse(Console.ReadLine()!);` |
| Szövegbe ágyazás | `f"Összeg: {s}"` | `$"Összeg: {s}"` |
| Logikai értékek | `True`, `False` | `true`, `false` |
| Logikai műveletek | `and`, `or`, `not` | `&&`, `||`, `!` |
| Megjegyzés | `# …` | `// …` és `/* … */` |
| Kis- és nagybetű | számít | számít (`Console` ≠ `console`) |

### Visual Studio – az első projekt

1. *Create a new project* → **Console App** (C#, *nem* a „.NET Framework” változat!) → *Next*.
2. Projekt neve PascalCase, ékezet és szóköz nélkül (pl. `ElsoProgram`), helye a saját mappád → *Next*.
3. *Framework*: **.NET 8.0 (Long Term Support)**.
4. *Do not use top-level statements*: ha bepipálod, a klasszikus szerkezetet kapod (lásd lent); ha nem, a rövidebbet. Mindkettő jó.
5. *Create*.

| Mit szeretnék | Billentyű / menü |
|---|---|
| Fordítás (build) | Ctrl+Shift+B |
| Futtatás hibakeresővel | **F5** |
| Futtatás hibakereső nélkül (az ablak nyitva marad a végén) | **Ctrl+F5** |
| Futó program leállítása | Shift+F5 |
| Hibák listája | *View → Error List* |
| Kód automatikus formázása | Ctrl+K, Ctrl+D |
| Kijelölt sorok megjegyzésbe / vissza | Ctrl+K, Ctrl+C / Ctrl+K, Ctrl+U |

A *Solution Explorer* ablakban látod a projekt fájljait. A `.csproj` a projektleíró (itt van a `net8.0` cél), a `Program.cs` a forráskód. A lefordított program a `bin\Debug\net8.0\` mappába kerül.

### A program szerkezete

**Klasszikus szerkezet** – így néz ki a régebbi tankönyvekben és a legtöbb példában:

```csharp
namespace ElsoProgram           // névtér: a program osztályainak „mappája”
{
    internal class Program      // C#-ban minden kód osztályban van
    {
        static void Main(string[] args)   // innen indul a program
        {
            Console.WriteLine("Helló, világ!");
        }
    }
}
```

**Felső szintű utasítások (top-level statements)** – ugyanez röviden; a `Main`-t a fordító „odaképzeli”:

```csharp
Console.WriteLine("Helló, világ!");
```

Rövid feladatoknál a második a kényelmesebb, de a klasszikus formát is fel kell ismerni. A `using System;` sorokat a projektben bekapcsolt *ImplicitUsings* miatt nem kell kiírni.

### Egyszerű típusok

| C# típus | Mit tárol | Példa | Python megfelelője |
|---|---|---|---|
| `int` | egész, kb. ±2,1 milliárd | `int db = 12;` | `int` |
| `long` | nagy egész, kb. ±9,2 · 10¹⁸ | `long mp = 3_000_000_000;` | `int` (Pythonban nincs felső határ!) |
| `byte` | 0–255 | `byte honap = 12;` | – |
| `double` | valós szám (15–16 értékes jegy) | `double atlag = 4.25;` | `float` |
| `decimal` | pontos tizedes tört, pénzhez | `decimal ar = 1999.90m;` | `Decimal` |
| `bool` | logikai | `bool kesz = false;` | `bool` |
| `char` | **egy** karakter, aposztróffal | `char jel = 'A';` | – (1 hosszú `str`) |
| `string` | szöveg, idézőjellel | `string nev = "Anna";` | `str` |

- **Deklaráció**: típus + név (`int x;`), **definíció** kezdőértékkel (`int x = 5;`). Használat előtt a változónak értéket kell kapnia, különben fordítási hiba.
- `var x = 5;` – a fordító kikövetkezteti a típust (`int`), de **ettől még statikus**: később nem kaphat szöveget.
- `const double Afa = 0.27;` – állandó, nem módosítható.
- **Hatókör**: a `{ }` blokkban deklarált változó csak abban a blokkban létezik (Pythonban az `if`-en belül létrehozott változó utána is él – C#-ban nem!).
- A forráskódban a tizedesjel **mindig pont** (`4.25`), de kiírásnál és bekérésnél magyar Windowson **vessző** (`4,25`).

### Operátorok

| Csoport | Operátorok | Megjegyzés |
|---|---|---|
| Aritmetikai | `+  -  *  /  %` | `/` két egész között **egész osztás**: `17 / 5 == 3` (Pythonban `17 // 5`) |
| Hatványozás | `Math.Pow(2, 10)` | nincs `**` operátor; az eredmény `double` |
| Növelés, csökkentés | `x++`, `x--` | Pythonban nincs; `x = x + 1` rövidítése |
| Összevont értékadás | `+=  -=  *=  /=  %=` | mint Pythonban |
| Relációs | `<  >  <=  >=  ==  !=` | `10 <= x <= 99` láncolás **nincs**, helyette `x >= 10 && x <= 99` |
| Logikai | `&&  ||  !` | rövidzáras: ha az eredmény már eldőlt, a jobb oldal nem értékelődik ki |
| Feltételes | `feltetel ? a : b` | Pythonban `a if feltetel else b` |
| Típuskényszerítés | `(double)x`, `(int)3.9` | a `(int)` levágja a törtrészt (3) |

**Precedencia** (erősebbtől a gyengébbig, röviden): zárójel → `!`, előjel, `++` → `* / %` → `+ -` → relációk → `== !=` → `&&` → `||` → `?:` → értékadás. Kétség esetén: zárójel.

```csharp
int a = 7, b = 2;
double rossz = a / b;           // 3   ← előbb egész osztás, utána alakul double-lé!
double jo = (double)a / b;      // 3,5
```

### Kiírás a konzolra

```csharp
Console.Write("Nincs utána sortörés. ");
Console.WriteLine("Ez után van.");
Console.WriteLine($"Átlag: {atlag:F2}");        // 2 tizedesjegy
Console.WriteLine($"[{nev,-10}][{db,5}]");       // -10: balra, 5: jobbra igazítva
Console.WriteLine("{0} + {1} = {2}", 2, 3, 5);   // régebbi, sorszámos forma
```

| Formátum | Jelentés | `$"{17:…}"` / `$"{3.14159:…}"` |
|---|---|---|
| `F2` | fixpontos, 2 tizedes | `17,00` / `3,14` |
| `D5` | egész, 5 jegyre nullákkal kiegészítve | `00017` |
| `N2` | ezres tagolás, 2 tizedes | `1 234 567,89` (`1234567.891:N2`) |
| `X` | hexadecimális | `11` |
| `,8` / `,-8` | 8 széles mező, jobbra / balra igazítva | `      17` / `17      ` |

**Színek és pozíció:**

| Utasítás | Hatás |
|---|---|
| `Console.ForegroundColor = ConsoleColor.Yellow;` | betűszín (16 szín: `Black`, `Red`, `DarkGreen`, …) |
| `Console.BackgroundColor = ConsoleColor.DarkBlue;` | háttérszín (a következő kiírástól) |
| `Console.Clear();` | képernyőtörlés a háttérszínnel, a kurzor a bal felső sarokba kerül |
| `Console.ResetColor();` | alapszínek vissza |
| `Console.SetCursorPosition(oszlop, sor);` | kurzor áthelyezése (0-tól számozva) |
| `Console.CursorTop`, `Console.CursorLeft` | a kurzor aktuális sora, oszlopa |
| `Console.WindowWidth`, `Console.WindowHeight` | az ablak mérete karakterben |

### Bekérés a konzolról

A `Console.ReadLine()` mindig **szöveget** (`string?`) ad vissza – akárcsak Pythonban az `input()`. Számmá alakítani külön kell:

```csharp
string nev = Console.ReadLine()!;                 // a ! jelzi a fordítónak: „tudom, hogy nem null”
int kor = int.Parse(Console.ReadLine()!);         // Pythonban: int(input())
double magassag = double.Parse(Console.ReadLine()!);
int ev = Convert.ToInt32(Console.ReadLine());     // ugyanez másképp
char betu = Console.ReadKey().KeyChar;            // egyetlen billentyű, Enter nélkül
bool igen = Console.ReadLine() == "igen";         // logikai érték összehasonlítással

if (int.TryParse(Console.ReadLine(), out int szam))   // hibatűrő változat: nem dob kivételt
    Console.WriteLine($"Ez szám: {szam}");
else
    Console.WriteLine("Ez nem szám!");
```

### Megjegyzések

```csharp
// egysoros megjegyzés
/* több soros
   megjegyzés */

/// <summary>Dokumentációs megjegyzés: a Visual Studio súgóként mutatja.</summary>

#region Bekérés      ← összecsukható kódrész (a bal margón a – jellel)
...
#endregion
```

### Hibakeresés (debugging)

- **Fordítási (szintaktikai) hiba**: piros hullámos aláhúzás, *Error List*. A program el sem indul. Pythonban ezek egy része csak futás közben derült ki – C#-ban a fordító előre szól (pl. típushiba).
- **Futási hiba (kivétel)**: pl. `int.Parse("alma")` → `FormatException`. A Visual Studio megáll a hibás sornál, és kiírja az üzenetet.
- **Logikai (szemantikai) hiba**: a program lefut, de rossz eredményt ad. Ennek felderítésére való a **töréspont**:

| Lépés | Billentyű |
|---|---|
| Töréspont be/ki az aktuális soron (piros pötty a margón) | **F9** (vagy kattintás a bal margón) |
| Indítás hibakeresővel – megáll a törésponton | **F5** |
| Következő sor (a metódushívásba nem lép be) | **F10** (*Step Over*) |
| Következő sor (a metódusba is belép) | **F11** (*Step Into*) |
| Folytatás a következő töréspontig | F5 |
| Változók értéke | egér a változó fölé, vagy *Locals* / *Watch* ablak |

### Tipikus hibák

- **Hiányzó pontosvessző** vagy párosítatlan `{ }` → *„; expected”*, *„} expected”*. A hibaüzenet sokszor a *következő* sorra mutat.
- **`Console.writeline`** – kis- és nagybetű számít: `Console.WriteLine`.
- **`int x = 5.5;`** vagy **`int x = "5";`** → *„Cannot implicitly convert type…”*. A statikus típus miatt a fordító nem engedi.
- **`double atlag = osszeg / db;`** két `int`-tel → egész osztás, a törtrész elvész. Megoldás: `(double)osszeg / db`.
- **`FormatException`** bekérésnél: magyar Windowson `3.5` helyett `3,5`-öt kell beírni (vagy szöveg került a szám helyére).
- **`'alma'`** – aposztróf csak egy karakterhez való; szöveghez idézőjel: `"alma"`.
- **Az `=` és `==` összekeverése**: `if (x = 5)` fordítási hiba – összehasonlítás `==`.
- **A konzolablak azonnal bezárul**: Ctrl+F5-tel indítsd, vagy a program végére tegyél `Console.ReadKey();`-t.

## Példák

| Projekt | Mit mutat be |
|---|---|
| [peldak/ElsoProgram](peldak/ElsoProgram/) | Klasszikus programszerkezet (névtér, osztály, `Main`), bekérés és `double.Parse`, téglalap kerülete és területe, megjegyzésfajták és `#region`, feltételes operátor; töréspont-gyakorlásra is |
| [peldak/TipusokOperatorok](peldak/TipusokOperatorok/) | Felső szintű utasítások; egyszerű típusok és határaik, egész és valós osztás, `++` / `+=`, logikai operátorok, formázott kiírás, színek, `Clear`, `SetCursorPosition` |

Futtatás: nyisd meg a `.csproj` fájlt Visual Studióban, és indítsd el F5-tel (a `TipusokOperatorok`-ot inkább Ctrl+F5-tel, mert a képernyőt színezi és pozicionál). Az `ElsoProgram`-ban tegyél töréspontot a `double kerulet = …` sorra, és F10-zel lépkedve figyeld a *Locals* ablakot.

## Feladatok

| Azonosító | Feladat | Nehézség | Megoldás |
|---|---|---|---|
| 13-M01-L01-F01 | [Üzemidő átváltása](feladatok/F01-uzemido/) | ★☆☆ | [megoldás](megoldasok/F01-uzemido/) |
| 13-M01-L01-F02 | [Műveleti táblázat](feladatok/F02-muveleti-tablazat/) | ★★☆ | [megoldás](megoldasok/F02-muveleti-tablazat/) |
| 13-M01-L01-F03 | [Jobbra és középre igazítás](feladatok/F03-igazitas/) | ★★★ | [megoldás](megoldasok/F03-igazitas/) |

## Kapcsolódó

- Előző lecke: – (a félév első leckéje; előzmény az 1. félév Python-anyaga)
- Következő lecke: 13-M01-L02 (Elágazások, ciklusok, véletlenszámok)
- Tanmenet: [1-13](../../../tanmenet/1-13-2felev.md)
