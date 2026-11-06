---
id: 13-M05-L04
cim: Szövegfeldolgozás grafikus felületen – Titkosítás
evfolyam: 1-13
modul: M05 – Grafikus alkalmazások – WPF bevezetés
orak: 65–68.
het:
oraszam: 4
elofeltetel: 13-M05-L03
leiras: Egyszerű titkosító algoritmusok (pl. Caesar) megvalósítása és WPF felületre illesztése.
---

# 13-M05-L04 · Szövegfeldolgozás grafikus felületen – Titkosítás

## Célok

A lecke végére a tanuló:

- karaktereket a kódjuk alapján alakít át (`char` ↔ `int`), és helyesen kezeli az ábécé végén történő „körbefordulást”;
- megvalósítja a Caesar-, az Atbas- és a Vigenère-titkosítást, valamint a visszafejtésüket;
- a titkosító **logikát külön statikus osztályba** szervezi, és ezt a felület csak meghívja;
- többsoros szövegdobozt, csúszkát (`Slider`) és élő frissítést (`TextChanged`, `ValueChanged`) használ;
- egy egyszerű rejtjelet gyakoriságelemzéssel és minden lehetőség kipróbálásával „feltör”.

## Elmélet

### Karakterek és kódok

Minden karakter egy számkóddal van tárolva. Az angol ábécé betűi egymás után következnek: `'A'` = 65, `'B'` = 66, …, `'Z'` = 90, és `'a'` = 97 … `'z'` = 122. Ezért egy betű ábécébeli **sorszáma** (0–25) így kapható meg, és így alakítható vissza:

```csharp
int sorszam = c - 'A';            // 'D' → 3
char betu = (char)('A' + sorszam); // 3 → 'D'
```

Ellenőrzéshez: `char.IsUpper(c)`, `char.IsLower(c)`, `char.IsLetter(c)`. A magyar ékezetes betűk (`á`, `ő` …) nem ebben a tartományban vannak. Vagy változatlanul hagyjuk őket, vagy titkosítás előtt ékezet nélkülire cseréljük.

### Caesar-rejtjel

Minden betűt az ábécében `k` hellyel arrébb tolunk; az ábécé végén elölről folytatjuk. `k = 3` esetén: `A → D`, `X → A`, `alma → dopd`.

```csharp
public static char Tol(char c, int k)
{
    if (char.IsUpper(c) && c <= 'Z') return (char)('A' + Mod(c - 'A' + k, 26));
    if (char.IsLower(c) && c <= 'z') return (char)('a' + Mod(c - 'a' + k, 26));
    return c;                                    // írásjel, szóköz, ékezetes betű: változatlan
}

private static int Mod(int a, int m) => ((a % m) + m) % m;   // negatív számra is 0..m-1
```

- **Visszafejtés** = eltolás `-k`-val (vagy `26 - k`-val).
- C#-ban a `%` eredménye **negatív** lehet (`-3 % 26 == -3`), ezért kell a `Mod` segédmetódus.
- Szöveg összerakásához a `StringBuilder` hatékonyabb, mint a `+=`, mert nem hoz létre minden lépésben új szöveget: `sb.Append(Tol(c, k));` … `sb.ToString()`.

### Atbas és ROT13

- **Atbas** (ősi héber rejtjel): az ábécét megfordítjuk: `A ↔ Z`, `B ↔ Y`, … Képlettel: `'Z' - (c - 'A')`.
- **ROT13**: Caesar `k = 13`-mal.

Mindkettő **önmaga inverze**: kétszer alkalmazva visszakapjuk az eredeti szöveget.

### Vigenère-rejtjel

A Caesar gyengéje, hogy minden betű ugyanannyival tolódik. A Vigenère egy **kulcsszó** betűi szerint betűnként más-más eltolást használ:

```
szöveg:  T A M A D A S   H A J N A L B A N
kulcs:   K U L C S K U   L C S K U L C S K
eltolás: 10 20 11 2 18 …
rejtett: D U X C V K M   S C B X U W D S X
```

A kulcs betűi ismétlődnek. A nem betű karakterek (szóköz, írásjel) változatlanok, és a kulcsban sem lépnek tovább.

### Feltörés

- **Nyers erő** (brute force): a Caesarnak csak 26 lehetséges kulcsa van; mindet kipróbáljuk, és megnézzük, melyik ad értelmes szöveget.
- **Gyakoriságelemzés**: egy hosszabb szövegben a betűk gyakorisága nyelvre jellemző. Magyarban és angolban is az `e` a leggyakoribb. Ha a rejtett szövegben például a `h` a leggyakoribb, a kulcs valószínűleg `h − e = 3`. Ez a megszámlálás tétele egy `int[26]` tömbbe: `db[c - 'a']++`.

### A felület

```xml
<TextBox Name="BemenetTextBox" AcceptsReturn="True" TextWrapping="Wrap"
         VerticalScrollBarVisibility="Auto" TextChanged="Bemenet_TextChanged"/>
<Slider Name="KulcsSlider" Minimum="0" Maximum="25" IsSnapToTickEnabled="True"
        TickFrequency="1" ValueChanged="KulcsSlider_ValueChanged"/>
```

- `AcceptsReturn` – az Enter új sort kezd; `TextWrapping="Wrap"` – sortörés; görgetősáv szükség esetén.
- A `Slider.Value` típusa `double`; egész lépésekhez `IsSnapToTickEnabled` és `TickFrequency`, a kódban `(int)KulcsSlider.Value`.
- **Élő frissítés:** a bemenet vagy a kulcs minden változásakor újraszámoljuk a kimenetet. Az XAML-ben beállított kezdőértékek már az `InitializeComponent` közben kiválthatják az eseményt, ezért vizsgáljuk az `IsLoaded`-et, vagy kódból kössük be az eseményt.
- Vágólap: `Clipboard.SetText(KimenetTextBox.Text);`

A titkosító algoritmus egy `static class` metódusaiban legyen (`Caesar.Titkosit(szoveg, k)`). A felület kódja így rövid marad, az algoritmus pedig más programból, például konzolos tesztből is hívható.

### Tipikus hibák

- **`(char)(c + k)` körbefordulás nélkül**: `'Z' + 3` → `']'`.
- **Negatív maradék** visszafejtésnél: `('A' - 'A' - 3) % 26 == -3`, és ebből nem betű lesz.
- **A kis- és a nagybetűk összekeverése**: mindkettőt külön kell kezelni, a saját kezdőbetűjükhöz (`'a'`, `'A'`) képest.
- **`char.IsLetter` az ékezetesre is igaz**: az `'á' - 'a'` nem 0 és 25 közé esik, ezért a tartományt is ellenőrizni kell.
- **Vigenère-ben a kulcs szóköznél is lép**: a rejtett szöveg ettől még visszafejthető, de nem egyezik az „elvárt” eredménnyel.

## Példák

| Projekt | Mit mutat be |
|---|---|
| [peldak/CaesarAblak](peldak/CaesarAblak/) | `static class Caesar` (eltolás körbefordulással, `Mod`, `StringBuilder`, ékezet nélkülire alakítás), többsoros bemenet és kimenet, `Slider` a kulcshoz, élő frissítés, titkosít/visszafejt választás, csere gomb, másolás a vágólapra, fájl megnyitása és mentése |

Futtatás: nyisd meg a `.csproj` fájlt Visual Studióban, és indítsd el (F5).

## Feladatok

| Azonosító | Feladat | Nehézség | Megoldás |
|---|---|---|---|
| 13-M05-L04-F01 | [Atbas és ROT13](feladatok/F01-atbas/) | ★☆☆ | [megoldás](megoldasok/F01-atbas/) |
| 13-M05-L04-F02 | [Vigenère-titkosító](feladatok/F02-vigenere/) | ★★☆ | [megoldás](megoldasok/F02-vigenere/) |
| 13-M05-L04-F03 | [Caesar-törő gyakoriságelemzéssel](feladatok/F03-caesar-tores/) | ★★★ | [megoldás](megoldasok/F03-caesar-tores/) |

## Kapcsolódó

- Előző lecke: 13-M05-L03 (Dinamikus felület – Aknakereső)
- Következő lecke: 13-M05-L05 (Adatkötés bevezetés)
- Karakterláncok alapjai: 13-M02-L01
- Tanmenet: [1-13](../../../tanmenet/1-13-2felev.md)
