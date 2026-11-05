# Munkafolyamat és szinkronizálás

## Alapelv

**Minden változás a GitHubon születik.** A hálózati mappa és a Google Classroom a GitHub tartalmának *kiadott* állapotát mutatja; ott nem szerkesztünk tananyagot.

## GitHub Project tábla

Egy GitHub Project („Esti C# tananyag”) fogja össze a teendőket. Ajánlott oszlopok (Status mező):

| Status | Jelentés |
|---|---|
| Ötlet | felvetés, hiba, javítási igény |
| Készül | valaki dolgozik rajta (branch / PR nyitva) |
| Kész a GitHubon | PR beolvasztva a `main`-be |
| Kiadva | hálózati mappa frissítve **és** Classroom-anyag kint van |

Ajánlott egyéni mezők: **Évfolyam** (1-13 / 2-14), **Lecke-ID** (szöveg), **Hét** (szám) – így a tábla hetek szerint is szűrhető, és látszik, mi kell a jövő heti órákra.

Címkék (labels): `uj-lecke`, `hiba`, `feladat`, `tanmenet`, `1-13`, `2-14`.

## Egy változás útja

1. **Issue** a megfelelő sablonnal (*Új lecke* / *Hiba vagy javítás*) – automatikusan bekerül a Projectbe.
2. **Branch + szerkesztés** – új leckénél `scripts/Uj-Lecke.ps1`.
3. **Pull request** – a PR-sablon ellenőrzőlistájával (a projektek fordítását helyben, Visual Studióban ellenőrizzük).
4. **Beolvasztás** a `main`-be → Status: *Kész a GitHubon*.
5. **Kiadás**
   - `scripts/Tartalomjegyzek.ps1` – évfolyam-tartalomjegyzék frissítése (és commit),
   - `scripts/Export-Halozat.ps1` – a `_halozat/` frissítése, utána a saját szinkronod viszi a hálózati mappába,
   - `scripts/Classroom-Poszt.ps1 -Id …` – a Classroom-bejegyzés szövege (a Classroom-link nem kerül vissza a repóba).
6. Status: *Kiadva*, issue lezárva.

Egyszemélyes, gyors javításnál a 2–4. lépés lehet közvetlen commit a `main`-re, de akkor is legyen issue, hogy a Project mutassa, mi változott és ki lett-e adva.

## Félévek, tanévek

- Tanév elején/végén **git tag**: `2026-27`, `2027-28` – így bármikor visszanézhető, mi volt a kiadott anyag egy adott évben.
- Ha a Classroom-kurzust évente újra létrehozzátok, a repóban semmit nem kell módosítani; a linkek csak a helyi `ecosystem.local.json`-ban vannak.
