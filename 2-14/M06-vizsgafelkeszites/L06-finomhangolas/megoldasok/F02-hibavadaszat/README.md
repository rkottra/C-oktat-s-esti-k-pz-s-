# Megoldás – 14-M06-L06-F02 · Hibavadászat

A javított program `HibakJavitva` néven (ebben a mappában); a `[n]` jelölések a kódban az alábbi sorokra utalnak.

| # | Tünet | Ok | Javítás |
|---|---|---|---|
| 1 | `FileNotFoundException: elemek.csv` (Visual Studióból indítva) | a CSV nem másolódik a `bin/Debug/net8.0` mappába | *Copy to Output Directory: Copy if newer* (a `.csproj`-ban `CopyToOutputDirectory`) |
| 2 | `FormatException: 'rendszam'` | a fejléc sort is adatként dolgozza fel | `Skip(1)` |
| 3 | `FormatException: '1.008'` (magyar területi beállításnál) | a `double.Parse` tizedesvesszőt vár | `double.Parse(…, CultureInfo.InvariantCulture)` |
| 4 | `FormatException: ''` a héliumnál | hiányzó elektronegativitás, nem nullable típus | `double?`, üres mező → `null` |
| 5 | „Legnehezebb: hidrogén” | `OrderBy` + `First()` a legkisebbet adja | `MaxBy` vagy `OrderByDescending` |
| 6 | `InvalidOperationException: Sequence contains no elements` (`Halogén`, `xyz`) | kis-/nagybetű-érzékeny összehasonlítás; `Average` üres sorozaton | `Equals(…, OrdinalIgnoreCase)`; üres esetben üzenet |
| 7 | üres `gazok.txt` (ha a 8. hibát már javítottad) | `"gaz"` ≠ `"gáz"` | a fájlban szereplő érték pontos használata |
| 8 | üres `gazok.txt`, nincs hibakezelés | a `StreamWriter` nincs lezárva, nincs `try`–`catch` | `using`, `try`–`catch (IOException)` |

Tanulság: a 8 hibából 4 kivételt dob (gyorsan megtalálható), 4 viszont csendben ad rossz eredményt – ezeket csak a minta kimenettel való **összevetés** fedi fel.
