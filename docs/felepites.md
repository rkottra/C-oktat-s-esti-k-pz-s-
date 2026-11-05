# Felépítés és elnevezési szabályok

## Mappaszerkezet

```
/
├─ README.md                  belépési pont, linkek
├─ ecosystem.config.json      GitHub-URL, hálózati útvonal, Classroom-linkek
├─ tanmenet/                  tanmenetek (hetekre/órákra bontva, leckeazonosítókkal)
│   ├─ 1-13-2felev.md
│   └─ 2-14.md
├─ 1-13/                      1/13. évfolyam, 2. félév
│   ├─ README.md              évfolyam-tartalomjegyzék (generált rész)
│   └─ M01-<modul-neve>/      modul = Classroom „téma”
│       ├─ README.md          modul-áttekintés
│       └─ L01-<lecke-neve>/  lecke = Classroom „anyag”
│           ├─ README.md      fejléc (metaadat) + elmélet/jegyzet
│           ├─ peldak/        bemutató programok (VS-projektek)
│           ├─ feladatok/     F01-…/README.md (+ kiindulo/ projekt, ha van)
│           └─ megoldasok/    F01-…/ megoldó projektek
├─ 2-14/                      2/14. évfolyam, ugyanígy
├─ kozos/                     évfolyamfüggetlen segédanyagok
├─ _sablon/lecke/             új lecke sablonja
├─ scripts/                   PowerShell-segédszkriptek
├─ .github/                   issue-sablonok, PR-sablon, build-ellenőrzés
├─ _bemenet/   (gitből kizárt) feltöltött nyers anyagok feldolgozás előtt
└─ _halozat/   (gitből kizárt) a hálózati mappába szinkronizálandó export
```

## Azonosítók

Minden lecke kap egy **állandó azonosítót**, amely mindhárom helyszínen megjelenik:

```
13-M02-L03
│  │   └── lecke sorszáma a modulon belül
│  └────── modul sorszáma az évfolyamon belül
└───────── évfolyam (13 = 1/13., 14 = 2/14.)
```

Feladat: `13-M02-L03-F02` (a lecke 2. feladata).

| Hol | Hogyan jelenik meg |
|---|---|
| GitHub | mappa: `1-13/M02-ciklusok/L03-while-ciklus/`, fejléc: `id: 13-M02-L03` |
| Hálózati mappa | ugyanaz a mappa, benne `_GitHub.url` parancsikon |
| Google Classroom | téma: `M02 – Ciklusok`, anyag címe: `13-M02-L03 · A while ciklus`, benne GitHub-link és hálózati útvonal |
| Tanmenet | a heti sorban az azonosító, linkkel a leckére |

Szabályok:

- Mappanevekben **nincs ékezet és szóköz** (kisbetű, kötőjel): `L03-while-ciklus`.
- Egy kiadott azonosítót **nem számozunk át**. Ha közbe kell szúrni, új sorszámot kap a modul végén, és a tanmenet sorrendje mondja meg a tanítási sorrendet.
- Ha egy leckét megszüntetünk, a mappa törlődik, de az azonosítója nem kerül újra kiosztásra.

## A lecke fejléce

A lecke `README.md` elején YAML-fejléc van; ebből dolgoznak a szkriptek:

```yaml
---
id: 13-M02-L03
cim: A while ciklus
evfolyam: 1-13
modul: M02 – Ciklusok
het: 5
oraszam: 2
elofeltetel: 13-M02-L02
leiras: Elöltesztelő ciklus, végtelen ciklus, kilépési feltétel.
classroom:
---
```

- `het`: a tanmenet szerinti tanítási hét.
- `classroom`: a Classroom-anyag linkje, kiadás után töltjük ki – így a GitHub is visszamutat a Classroomra.

## Példaprogramok és feladatok

- Visual Studio, **.NET 8** (`net8.0`), egy projekt = egy mappa, a projekt neve PascalCase (pl. `peldak/WhileSzamlalo/WhileSzamlalo.csproj`).
- A projektek önállóak (nincs közös `Directory.Build.props`), hogy a hálózati mappából egy-egy projektet külön is meg lehessen nyitni.
- Megoldás neve megegyezik a feladat mappájával: `feladatok/F02-osszegzes/` ↔ `megoldasok/F02-osszegzes/`.
- `bin/`, `obj/`, `.vs/` sosem kerül a repóba.
