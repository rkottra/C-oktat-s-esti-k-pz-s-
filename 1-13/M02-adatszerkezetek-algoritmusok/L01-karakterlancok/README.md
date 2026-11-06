---
id: 13-M02-L01
cim: Karakterláncok kezelése
evfolyam: 1-13
modul: M02 – Adatszerkezetek és algoritmusok
orak: 9–12.
het:
oraszam: 4
elofeltetel: 13-M01-L02
leiras: A string típus, szövegkezelő metódusok (Substring, IndexOf, Split, Replace, ToUpper), karakterenkénti feldolgozás.
---

# 13-M02-L01 · Karakterláncok kezelése

## Célok

A lecke végére a tanuló:

- tudja, miben különbözik a C# `string` és `char` típusa, és hogyan éri el a szöveg egyes karaktereit;
- a Pythonban megismert `str`-műveleteknek megtalálja a C#-os megfelelőjét (`Length`, `IndexOf`, `Substring`, `Split`, `Replace`, `ToUpper` …);
- karakterenként bejár egy szöveget, és a `char` osztály segédmetódusaival (`char.IsLetter`, `char.IsDigit` …) elemzi;
- szöveget darabol (`Split`) és fűz össze (`string.Join`, interpoláció), és tudja, mikor érdemes `StringBuilder`-t használni.

## Elmélet

### Python után: ami ugyanaz, és ami más

A szövegkezelés gondolkodásmódja ugyanaz, mint Pythonban: a szöveg karakterek sorozata, 0-tól indexelünk, és a szöveg **nem módosítható** (immutable) – minden „módosító” metódus *új* szöveget ad vissza. A különbségek:

| | Python | C# |
|---|---|---|
| szöveg típusa | `str` | `string` (= `System.String`) |
| egy karakter | nincs külön típus, 1 hosszú `str` | külön típus: **`char`**, aposztróffal: `'a'` |
| szövegliterál | `"alma"` vagy `'alma'` | csak `"alma"` – a `'a'` már `char`! |
| metódusnevek | kisbetűs: `s.upper()` | PascalCase: `s.ToUpper()` |
| hossz | `len(s)` | `s.Length` (tulajdonság, nincs zárójel) |
| formázott szöveg | `f"{nev} {kor} éves"` | `$"{nev} {kor} éves"` |

### Megfeleltetési táblázat

| Python | C# | Megjegyzés |
|---|---|---|
| `s[0]` | `s[0]` | C#-ban az eredmény **`char`** |
| `s[-1]` | `s[^1]` | a `^` a végétől számol |
| `s[2:5]` | `s[2..5]` vagy `s.Substring(2, 3)` | a `Substring` 2. paramétere **hossz**, nem végindex! |
| `s[3:]` / `s[:3]` | `s[3..]` / `s[..3]` | |
| `s.find("x")` | `s.IndexOf("x")` | nincs találat: `-1` |
| `"x" in s` | `s.Contains("x")` | `bool` |
| `s.startswith("x")` | `s.StartsWith("x")` | `EndsWith` is van |
| `s.upper()` / `s.lower()` | `s.ToUpper()` / `s.ToLower()` | |
| `s.strip()` | `s.Trim()` | `TrimStart`, `TrimEnd` is van |
| `s.replace("a", "b")` | `s.Replace("a", "b")` | |
| `s.split(";")` | `s.Split(';')` | `string[]` tömböt ad vissza |
| `";".join(lista)` | `string.Join(";", lista)` | statikus metódus! |
| `s.count("a")` | nincs közvetlen megfelelő | ciklussal (lásd lent) |
| `s[::-1]` | `new string(s.Reverse().ToArray())` | vagy ciklus visszafelé |
| `str(x)` / `int(s)` | `x.ToString()` / `int.Parse(s)` | |
| `s.isdigit()` | `char.IsDigit(c)` | **karakterenként** vizsgál |

Ezeken felül hasznos még: `Insert(index, szoveg)`, `Remove(index, db)`, `PadLeft(hossz, '0')` / `PadRight`, `LastIndexOf`, `IndexOf(mit, honnan)` (keresés egy adott helytől), `string.IsNullOrWhiteSpace(s)`.

### Karakterenkénti feldolgozás

Ha nincs kész metódus, végigmegyünk a szövegen – pontosan úgy, mint Pythonban a `for c in s:`:

```csharp
int maganhangzo = 0;
foreach (char c in mondat.ToLower())
{
    if ("aáeéiíoóöőuúüű".Contains(c))
        maganhangzo++;
}
```

Ha az index is kell (pl. a következő karaktert is nézzük), `for` ciklus: `for (int i = 0; i < s.Length; i++) { char c = s[i]; … }`.

A `char` osztály hasznos vizsgálatai: `char.IsLetter(c)`, `char.IsDigit(c)`, `char.IsWhiteSpace(c)`, `char.IsUpper(c)`, `char.IsPunctuation(c)`, valamint `char.ToUpper(c)`. Ezek az ékezetes magyar betűkkel is jól működnek.

A `char` valójában egy szám (Unicode-kód): `(int)'A'` értéke 65, és `(char)('a' + 2)` értéke `'c'`. Így lehet például betűt léptetni (Caesar-kódolás – lásd 13-M05-L04).

### Darabolás és összefűzés

```csharp
string sor = "Budapest;1;1 723 836";
string[] reszek = sor.Split(';');          // ["Budapest", "1", "1 723 836"]
string varos = reszek[0];

string[] szavak = mondat.Split(' ', StringSplitOptions.RemoveEmptyEntries);  // több szóköz sem gond
string[] szavak2 = mondat.Split(new[] { ' ', ',', '.', '-' }, StringSplitOptions.RemoveEmptyEntries);

string vissza = string.Join(" ", szavak);  // összefűzés elválasztóval
```

A `Split` lesz a fájlfeldolgozás (13-M04) alapja: minden sor egy rekord, a mezőket elválasztó karakter mentén vágjuk szét.

### Speciális karakterek, szó szerinti szöveg

| Jelölés | Jelentés |
|---|---|
| `\n` | új sor |
| `\t` | tabulátor |
| `\"` | idézőjel a szövegben |
| `\` | egy fordított perjel |
| `@"C:\adatok\be.txt"` | szó szerinti (verbatim) szöveg: a `\` nem vezérlőkarakter – fájlútvonalakhoz kényelmes |
| `$"Összeg: {osszeg,8:F2}"` | interpoláció igazítással (8 karakter széles) és formátummal (2 tizedes) |

### Összefűzés ciklusban: StringBuilder

Mivel a `string` nem módosítható, az `s += "x"` minden alkalommal új szöveget hoz létre. Néhány összefűzésnél ez mindegy, de **sok ezer** ismétlésnél lassú. Ilyenkor a `System.Text.StringBuilder` való: `sb.Append(…)`, a végén `sb.ToString()`.

### Tipikus hibák

- **`Substring(kezdet, hossz)`**: a második paraméter a kivágandó **darabszám**, nem a végindex. Ha túllóg a szöveg végén, `ArgumentOutOfRangeException` a vége.
- **Tartomány vége kizárt**: `s[6..7]` csak **egy** karaktert ad (a 6. indexűt), ugyanúgy, mint Pythonban az `s[6:7]`.
- **`IndexOf` eredményét nem ellenőrizzük**: ha `-1` jön vissza, és azzal hívjuk a `Substring`-et, kivétel keletkezik. Előbb `if (hely >= 0)`.
- **`'a'` és `"a"` összekeverése**: `s[0] == "A"` fordítási hiba (char-t stringgel hasonlítunk). Helyesen: `s[0] == 'A'`.
- **Kis- és nagybetű**: `"Alma" == "alma"` hamis. Vagy mindkettőt `ToLower()`-rel alakítsuk, vagy `string.Equals(a, b, StringComparison.OrdinalIgnoreCase)`.
- **A metódus nem módosítja a változót**: `s.ToUpper();` önmagában hatástalan – az eredményt el kell tárolni: `s = s.ToUpper();` (Pythonban is így van).
- **`Console.ReadLine()` figyelmeztetés**: `string?` típust ad (elvileg `null` is lehet). Egyszerű megoldás: `string mondat = Console.ReadLine() ?? "";`
- **Kérdőjelek ékezetek helyett a konzolon**: a program elején `Console.OutputEncoding = System.Text.Encoding.UTF8;`.

## Példák

| Projekt | Mit mutat be |
|---|---|
| [peldak/SzovegMetodusok](peldak/SzovegMetodusok/) | a `string` metódusai Python-megfelelőikkel kommentezve: indexelés, tartományok, keresés, kivágás, csere, darabolás, összefűzés, igazítás |
| [peldak/KarakterenkentFeldolgozas](peldak/KarakterenkentFeldolgozas/) | bekért mondat elemzése karakterenként: betűk, számjegyek, magánhangzók száma, megfordítás, szavankénti nagy kezdőbetű, betűstatisztika |

## Feladatok

| Azonosító | Feladat | Nehézség | Megoldás |
|---|---|---|---|
| 13-M02-L01-F01 | [Mondatelemző](feladatok/F01-mondatelemzo/) | ★☆☆ | [megoldás](megoldasok/F01-mondatelemzo/) |
| 13-M02-L01-F02 | [Névkártya – nevek feldolgozása](feladatok/F02-nevkartya/) | ★★☆ | [megoldás](megoldasok/F02-nevkartya/) |
| 13-M02-L01-F03 | [Egyszerű számológép szövegből](feladatok/F03-szovegszamologep/) | ★★★ | [megoldás](megoldasok/F03-szovegszamologep/) |

## Kapcsolódó

- Előző lecke: 13-M01-L02 (Elágazások, ciklusok, véletlenszámok)
- Következő lecke: 13-M02-L02 (Tömbök és mátrixok)
- Tanmenet: [1-13](../../../tanmenet/1-13-2felev.md)
