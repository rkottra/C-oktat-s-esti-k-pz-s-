# Megoldás – 14-M06-L04-F01 · 3. próbavizsga: Kémiai elemek

| Projekt | Rész |
|---|---|
| [ElemekKonzol](ElemekKonzol/) | I. konzolos rész (CSV-betöltéssel; az `elemek.json` is a projektben van, ha valaki azzal dolgozott) |
| [ElemekWpf](ElemekWpf/) | II. grafikus rész: `Models/Kategoria.cs`, `Models/Elem.cs` (`[Key]` + `[DatabaseGenerated(None)]`), `ElemekContext`, főablak szűrőkkel, `UjElemAblak` |

Futtatás (grafikus rész): XAMPP-ban fusson a MySQL, importáld az [elemek.sql](../../feladatok/F01-kemiai-elemek/elemek.sql) fájlt, majd F5. Migráció nem kell.

## Javítókulcs (100 pont)

### I. Konzolos rész (40 pont)

| # | Szempont | Pont |
|---|---|---|
| 1 | projekt, adatfájl másolása | 2 |
| 2 | osztály helyes típusokkal (`double`, `double?`) | 2 |
| | konstruktor/deszerializálás: tizedes pont `InvariantCulture`-rel, üres mező → `null` | 2 |
| | statikus betöltő a megadott néven, `List<Elem>` | 2 |
| 3 | elemek száma (36) | 2 |
| 4 | darabszám (9) | 2 |
| | vegyjelek vesszővel, rendszám szerint | 2 |
| 5 | maximum a `null` értékek kizárásával (fluor, F, 3,98) | 3 |
| | hiányzó értékek száma (3) | 2 |
| 6 | csoportosítás kategóriánként, darabszám | 2 |
| | átlag két tizedesre | 2 |
| | ábécérend (magyar rendezés: az *átmenetifém* az *alkáliföldfém* után) | 2 |
| 7 | bekérés, kis-/nagybetű-független keresés | 2 |
| | „Érvénytelen vegyjel.” ág (pl. `xy1`, üres, 3 betű) | 1 |
| | „Nincs ilyen vegyjelű elem.” ág (pl. `xy`) | 1 |
| | helyes kiírás a minta szerint | 2 |
| 8 | periódusonkénti csoportosítás, sorrend | 2 |
| | darabszám és vegyjelek szóközzel, rendszám szerint | 2 |
| | `Összesen` sor | 1 |
| | `try`–`catch`, a fájl lezárása, üzenetek | 2 |
| 9 | kódminőség | 2 |

### II. Grafikus rész (60 pont)

| # | Szempont | Pont |
|---|---|---|
| 1 | adatbázis importálva | 3 |
| 2 | csomagok | 1 |
| | entitások, `[Table]`/`[Column]` (vagy scaffold), idegen kulcs + navigáció | 4 |
| | rendszám kulcs, nem generált (`DatabaseGenerated(None)`) | 2 |
| | DbContext, működő kapcsolat | 2 |
| 3 | szűrősáv két listával, helyes elemekkel | 3 |
| | táblázat kilenc oszloppal, kategória neve, formázott számok, üres cella `null`-nál | 4 |
| | gombok, felirat, cím, reszponzív elrendezés | 3 |
| 4 | minden elem rendszám szerint | 2 |
| | kategória- és halmazállapot-szűrő külön is, együtt is működik | 4 |
| | felirat frissül | 2 |
| 5 | modális ablak a mezőkkel, *Mentés*/*Mégse* | 4 |
| | legördülő listák feltöltve, üres kezdőállapot | 4 |
| 6 | ellenőrzések a feladatlap szerinti pontokkal | 14 |
| 7 | mentés és frissítés | 3 |
| | törlés: figyelmeztetés, megerősítés, frissítés | 3 |
| 8 | kódminőség | 2 |
| | **Összesen** | **100** |

Ellenőrző értékek a grafikus részhez: *nemesgáz* → 4 elem; *gáz* → 9 elem; *nemfém* + *gáz* → 3 elem (H, N, O); *halogén* + *folyékony* → 1 elem (Br).
