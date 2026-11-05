# Megoldás – 14-M06-L05-F02 · Kódátnézés

A javított program: `OlimpiaJavitott` (ebben a mappában). A kódban a `[H1]`, `[R2]` stb. megjegyzések az alábbi listára utalnak.

## A kiinduló program problémái (javítókulcs)

### Hibák – rossz eredmény

| # | Hol | Probléma | Következmény | Javítás |
|---|---|---|---|---|
| H1 | `for (int i = 1; i < s.Length - 1; i++)` | a ciklus az utolsó adatsort is kihagyja | 29 olimpia 30 helyett; Európa 16 a 17 helyett (a 2024-es párizsi hiányzik) | `Skip(1)` vagy `i < s.Length` |
| H2 | 4. feladat dupla ciklusa | minden párt kiír, mindkét irányban | London 6-szor, Párizs 2-szer (a hiányzó 2024 miatt) jelenik meg | `GroupBy(o => o.Varos).Where(g => g.Count() > 1)` |
| H3 | 6. feladat `else` ága | a „Nem rendezett.” üzenet minden nem egyező sornál kiíródik | akár 29 felesleges sor a kimeneten | előbb keresés, utána egyetlen üzenet |
| H4 | `l[i][2] == x` | kis-/nagybetű- és szóközérzékeny | `japán` → nincs találat | `Trim()` + `StringComparison.OrdinalIgnoreCase` |
| H5 | `StreamWriter` lezárás nélkül | a puffer nem íródik ki | üres `europai.txt` | `using` |

### Egyéb problémák

| # | Kategória | Probléma | Javítás |
|---|---|---|---|
| R1 | olvashatóság | `s`, `l`, `x`, `db2` nevek; `l[i][3]` „varázsindexek” | `Olimpia` osztály beszédes tulajdonságokkal |
| R2 | ismétlés | kontinensenként külön, szinte azonos számláló ciklus; a többi kontinens kimarad | egy `GroupBy` |
| T1 | hatékonyság | O(n²) dupla ciklus | `GroupBy`: O(n) |
| R3 | hibakezelés | üres `catch { }` – a hiba nyomtalanul eltűnik; a „kész” üzenet hiba esetén is megjelenhet | `catch (IOException)` tájékoztató üzenettel |
| R4 | felesleges kód | kikommentezett ciklus; `Console.ReadKey()` (vizsgán zavaró, átirányított bemenetnél kivételt dob) | törlés |
| R5 | olvashatóság | `"2. feladat: " + l.Count` – a kiírás nem követi a mintát | interpolált szöveg a minta szerint |
| R6 | beállítás | `<Nullable>disable</Nullable>` – a fordító nem figyelmeztet a lehetséges `null`-ra (`Console.ReadLine()`) | `enable`, és `?? ""` |

Értékelés a gyakorláshoz: 5 hiba + 4 egyéb megállapítás = „megfelelt”; a H1–H5 mind megtalálva + legalább 5 egyéb = „kiváló”.
