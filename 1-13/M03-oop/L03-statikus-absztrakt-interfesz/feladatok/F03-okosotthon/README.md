# 13-M03-L03-F03 · Okosotthon

**Nehézség:** ★★★ **Becsült idő:** 90 perc

## Feladat

Egy okosotthon vezérlőprogramját készítjük el. Az eszközök nagyon különbözők: van, amelyik csak be- és kikapcsolható, van, amelyiknek szintje állítható, és van, amelyik áramot fogyaszt. A közös képességeket **interfészek** írják le, a közös adatokat egy **absztrakt ős**. Készíts `Okosotthon` nevű .NET 8 konzolos alkalmazást.

1. **Interfészek** (egy `Interfeszek.cs` fájlban):
   - `IKapcsolhato`: `bool Bekapcsolva { get; }`, `void Be()`, `void Ki()`;
   - `ISzabalyozhato`: `int Szint { get; set; }`, `int MinSzint { get; }`, `int MaxSzint { get; }`. A beállított szint a határok közé szorul: ami kisebb, abból `MinSzint`, ami nagyobb, abból `MaxSzint` lesz;
   - `IFogyaszto`: `double Teljesitmeny()`, a pillanatnyi teljesítmény wattban.
2. **`abstract class Eszkoz`**: `Nev`, `Helyiseg`, valamint `static int Darab` (hány eszköz van regisztrálva). Absztrakt `string Allapot()` metódusa és `ToString()`-je van: `[helyiség] név – állapot`.
3. **Eszközök**, mindegyik az `Eszkoz` leszármazottja, és a megfelelő interfészeket valósítja meg:

   | Osztály | Interfészek | Működés |
   |---|---|---|
   | `Lampa` | `IKapcsolhato`, `ISzabalyozhato`, `IFogyaszto` | fényerő 0–100%; fogyasztás: bekapcsolva `MaxWatt × fényerő / 100`, kikapcsolva 0 |
   | `Termosztat` | `ISzabalyozhato`, `IFogyaszto` | célhőmérséklet 16–28 °C; a fűtés teljesítménye `(cél − 16) × 150` W |
   | `Redony` | `ISzabalyozhato` | nyitottság 0–100% (nem fogyaszt, nem kapcsolható) |
   | `Konnektor` | `IKapcsolhato`, `IFogyaszto` | a rádugott készülék neve és teljesítménye; bekapcsolva ennyit fogyaszt |

4. **`static class Vezerlo`** statikus metódusokkal, amelyek egy `List<Eszkoz>` listát kapnak:
   - `MindentKi(lista)`: minden **kapcsolható** eszközt kikapcsol;
   - `Be(lista, helyiseg)`: egy helyiség minden **kapcsolható** eszközét bekapcsolja;
   - `Szintre(lista, helyiseg, szint)`: egy helyiség minden **szabályozható** eszközét a megadott szintre állítja;
   - `OsszFogyasztas(lista)`: a **fogyasztók** pillanatnyi összteljesítménye;
   - `FogyasztasHelyisegenkent(lista)`: helyiségenként kiírja a fogyasztást;
   - `Allapotlista(lista)`: kiírja az összes eszközt.
5. A főprogram regisztráljon legalább 8 eszközt 3–4 helyiségben, majd futtassa le a következő **forgatókönyveket**. Mindegyik után írja ki az állapotlistát és az összfogyasztást:
   - **Reggel:** a konyhában minden bekapcsol (a kávéfőző is), a konyhai és a nappali lámpák 100%-on, minden redőny 100%-on, a termosztát 22 °C;
   - **Elmegyek otthonról:** minden kikapcsolva, redőnyök 0%-on, a termosztát 17 °C;
   - **Este:** a nappaliban a lámpák 40%-on, a tévé konnektora bekapcsolva, a redőnyök 0%-on, a termosztát 21 °C.

## Minta kimenet (a megoldás eszközeivel, részlet)

```
Regisztrált eszközök: 10

=== Reggel ===
[konyha] mennyezeti lámpa – BE, 100%, 12 W
[konyha] kávéfőző-konnektor – BE (kávéfőző), 1200 W
[nappali] állólámpa – BE, 100%, 9 W
[nappali] mennyezeti lámpa – BE, 100%, 18 W
[nappali] redőny – 100% nyitva
[nappali] termosztát – 22 °C, fűtés 900 W
[nappali] tévé-konnektor – KI (tévé)
[hálószoba] éjjeli lámpa – KI
[hálószoba] redőny – 100% nyitva
[dolgozószoba] töltő-konnektor – KI (laptoptöltő)
Összfogyasztás: 2 139 W
Helyiségenként: konyha 1 212 W, nappali 927 W, hálószoba 0 W, dolgozószoba 0 W
...
=== Este ===
...
[nappali] állólámpa – BE, 40%, 3,6 W
[nappali] tévé-konnektor – BE (tévé), 110 W
...
Összfogyasztás: 871 W
```

## Tippek

- Az ős és az interfészek sorrendje: `public class Lampa : Eszkoz, IKapcsolhato, ISzabalyozhato, IFogyaszto`. Az ős mindig elöl áll.
- A szint határok közé szorítása egy sorban: `Math.Clamp(value, MinSzint, MaxSzint)`.
- A `Vezerlo` metódusai interfészre vizsgálnak: `if (e is IKapcsolhato k) k.Ki();`. Így egy később hozzáadott, például `Mosogep` osztályhoz a vezérlőt nem kell módosítani, elég, ha megvalósítja a megfelelő interfészt.
- A helyiségenkénti fogyasztáshoz gyűjtsd ki a különböző helyiségneveket (mint a Kerttervezőben a növényeket).

Megoldás: [../../megoldasok/F03-okosotthon/](../../megoldasok/F03-okosotthon/)
