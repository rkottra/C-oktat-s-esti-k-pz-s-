# Automatikus feltöltés a Google Classroomba

A leckék Classroom-anyagai a repóból generálódnak, és egy Google Apps Script tölti fel őket a kurzusba.

```
leckék README-fejléce ──Classroom-Jegyzek.ps1──► scripts/classroom/anyagok.json ──push──► GitHub
                                                                                         │
                                         Classroom-kurzus ◄──szinkron()── Apps Script ◄──┘
```

- **Téma** = modul, évfolyam-előtaggal (például `13-M05 – Grafikus alkalmazások – WPF bevezetés`).
- **Anyag** = lecke: a cím a lecke azonosítója és címe, a leírás a lecke rövid leírása, a GitHub-link, a hálózati útvonal és a feladatok listája. Mellékletként a lecke és a feladatok GitHub-linkje.
- Az anyagok **közzétéve** jönnek létre, időzítés nélkül.
- A Classroom-azonosítók **nem** kerülnek a repóba: a kurzusazonosító csak az Apps Script szerkesztőben szerepel, a feltöltött anyagok azonosítói a szkript tulajdonságai között vannak.

## Egyszeri beállítás (kb. 5 perc)

1. Nyisd meg a [script.google.com](https://script.google.com) oldalt az iskolai fiókkal, és hozz létre egy új projektet (például „Esti C# – Classroom-szinkron”).
2. **Projektbeállítások** (fogaskerék) → jelöld be: *„appsscript.json” manifestfájl megjelenítése a szerkesztőben*.
3. A szerkesztőben:
   - az `appsscript.json` tartalmát cseréld le a repó [scripts/classroom/appsscript.json](../scripts/classroom/appsscript.json) fájljára (ez kapcsolja be a Classroom API-t);
   - a `Kód.gs` tartalmát cseréld le a [scripts/classroom/Kod.gs](../scripts/classroom/Kod.gs) fájlra.
4. A `KURZUS` konstansba írd be a célkurzus azonosítóját: a Classroom-link `/c/` utáni része (a helyi `ecosystem.local.json` `celKurzus.classroom` mezőjében megtalálod). Ha bizonytalan vagy, futtasd a `kurzusokListazasa` függvényt: a naplóba kiírja a kurzusaidat az azonosítóikkal.
5. Ments, válaszd ki a `szinkron` függvényt, és **Futtatás**. Az első futáskor a Google engedélyt kér (Classroom-kurzusok, -témák és -anyagok kezelése, külső URL elérése): fogadd el.

## Frissítés (minden kiadáskor)

1. `.\scripts\Classroom-Jegyzek.ps1` – újragenerálja az `anyagok.json`-t.
2. Commit és push a `main`-re. A jegyzéknek és a leckéknek is fent kell lennie a GitHubon, különben az Apps Script a régi jegyzéket látja, a linkek pedig nem működnek.
3. Az Apps Scriptben: `szinkron` → **Futtatás**.

A szinkron bármikor újrafuttatható:

- a változatlan leckékhez nem nyúl;
- ha csak a cím, a leírás vagy a téma változott, helyben frissíti az anyagot;
- ha a mellékletek (feladatok) változtak, az anyagot törli és újra létrehozza, mert az API a mellékleteket nem engedi módosítani;
- ha egy lecke kikerült a jegyzékből, csak figyelmeztet a naplóban, nem töröl.

Igény szerint az Apps Scriptben egy **időalapú eseményindító** (Eseményindítók → `szinkron`, például naponta) is beállítható; akkor a 3. lépés is elmarad.

## Tudnivalók

- **Sorrend:** a Classroom felülre teszi az újabb elemet, ezért a szkript visszafelé tölt fel. Így első feltöltéskor a témák és a leckék a tanmenet sorrendjében, felülről lefelé állnak. A később újra létrehozott anyag a témája tetejére kerül; ilyenkor a Classroomban húzással igazítható.
- **Kézzel feltett anyagok:** az API csak azokat az anyagokat módosíthatja, amelyeket ugyanez a szkript hozott létre. A korábban kézzel feltett lecke-bejegyzéseket a szkript nem látja, ezeket kézzel töröld, különben duplán lesznek meg.
- **Mindent elölről:** ha a feltöltött anyagokat a Classroomban kézzel törölted, futtasd az `allapotTorlese` függvényt, majd a `szinkron`-t.
- **Időkorlát:** egy Apps Script-futás legfeljebb 6 perc. A szkript 5 perc után megáll; újrafuttatva onnan folytatja, ahol abbahagyta.
- **Másik kurzus** (például új tanév): csak a `KURZUS` konstanst kell átírni, a feltöltési állapotot a szkript kurzusonként külön tartja nyilván.
