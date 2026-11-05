# Esti C# oktatás – tananyag-ökoszisztéma

Ez az esti tagozatos C# programozásoktatás **egyetlen igazságforrása**: a tanmenetek, a leckék, a példaprogramok, a gyakorló feladatok és azok megoldásai mind itt vannak.

| Évfolyam | Időszak | Tanmenet | Tananyag | Google Classroom |
|---|---|---|---|---|
| 1/13. | 2. félév | [tanmenet/1-13-2felev.md](tanmenet/1-13-2felev.md) | [1-13/](1-13/) | *(link a `ecosystem.config.json`-ban)* |
| 2/14. | teljes tanév | [tanmenet/2-14.md](tanmenet/2-14.md) | [2-14/](2-14/) | *(link a `ecosystem.config.json`-ban)* |

Közös segédanyagok (telepítés, értékelés, kódolási szabályok): [kozos/](kozos/)

## A három helyszín

```
              ┌────────────────────────────┐
              │   GitHub repó (forrás)     │  ← minden változás itt történik
              │   issue → PR → main        │
              └──────┬──────────────┬──────┘
   Export-Halozat.ps1│              │Classroom-Poszt.ps1
                     ▼              ▼
     _halozat/ (gitből kizárt) Google Classroom
     → saját szinkron →       (téma = modul, anyag = lecke,
     iskolai hálózati mappa    link a GitHub-mappára és a hálózati útvonalra)
```

Mindhárom helyen **ugyanaz a mappaszerkezet és ugyanazok az azonosítók** (pl. `13-M02-L03`), így bármelyik helyről egyértelműen megtalálható ugyanaz az anyag a másik kettőben.

## Dokumentáció

- [docs/felepites.md](docs/felepites.md) – mappaszerkezet, azonosítók, a lecke felépítése
- [docs/munkafolyamat.md](docs/munkafolyamat.md) – változáskezelés GitHub Projecttel, kiadás Classroomba és a hálózati mappába

## Gyors parancsok (PowerShell, a repó gyökeréből)

```powershell
# Új lecke a sablonból
.\scripts\Uj-Lecke.ps1 -Evfolyam 1-13 -Modul M02-ciklusok -Lecke L03-while-ciklus -Cim "A while ciklus"

# Évfolyam-tartalomjegyzékek frissítése a leckék fejlécéből
.\scripts\Tartalomjegyzek.ps1

# A hálózati mappába szánt export elkészítése (_halozat/)
.\scripts\Export-Halozat.ps1

# Google Classroom-bejegyzés szövege egy leckéhez (vágólapra is másolja)
.\scripts\Classroom-Poszt.ps1 -Id 13-M02-L03
```
