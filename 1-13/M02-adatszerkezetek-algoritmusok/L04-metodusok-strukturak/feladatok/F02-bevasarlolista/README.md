# 13-M02-L04-F02 · Bevásárlólista

**Nehézség:** ★★☆ **Becsült idő:** 60 perc

## Feladat

Készíts `Bevasarlolista` nevű menüs programot, amely egy `List<string>` típusú bevásárlólistát kezel. A menüpontokat egy `enum` írja le:

```csharp
enum Menupont { Kilepes, Hozzaadas, TorlesNevvel, TorlesSorszammal, Listazas, Rendezes, Kereses }
```

A menü a felhasználónak a menüpont **számát** (`0`–`6`) mutatja. A beírt számot alakítsd `Menupont` típusúvá, a `switch` pedig az `enum` értékein dolgozzon.

| Menüpont | Működés |
|---|---|
| Hozzáadás | Bekér egy tételt. Ha üres, nem csinál semmit. Ha már szerepel a listán (kis- és nagybetűtől függetlenül), szól, és nem veszi fel újra. |
| Törlés névvel | Bekér egy nevet, és törli. Ha nincs ilyen, szól. |
| Törlés sorszámmal | Bekér egy sorszámot (1-től számozva), és törli az adott tételt. Érvénytelen sorszámnál szól. |
| Listázás | Sorszámozva kiírja a tételeket, a végén a darabszámmal. Üres listánál: `A lista üres.` |
| Rendezés | Ábécérendbe rendezi a listát. |
| Keresés | Bekér egy szövegrészletet, és kiírja azokat a tételeket, amelyek tartalmazzák (kiválogatás egy új listába). |

Minden menüpontot külön **metódus** valósítson meg, amely paraméterként kapja a listát, például `static void Hozzaad(List<string> lista)`. A program a kezdéskor a `tej`, `kenyér` és `alma` tételeket tartalmazza.

## Minta bemenet és kimenet

```
1 – Hozzáadás  2 – Törlés névvel  3 – Törlés sorszámmal
4 – Listázás  5 – Rendezés  6 – Keresés  0 – Kilépés
Választás: 1
Tétel: Tej
A(z) „Tej” már szerepel a listán.
Választás: 1
Tétel: sajt
Választás: 5
Választás: 4
1. alma
2. kenyér
3. sajt
4. tej
Összesen 4 tétel.
Választás: 3
Sorszám: 9
Nincs ilyen sorszám (1–4).
Választás: 6
Keresett részlet: e
Találatok (2): kenyér, tej
Választás: 0
```

## Tippek

- Az `int` → `enum` átalakítás: `(Menupont)szam`. Hogy a szám érvényes-e, azt az `Enum.IsDefined(typeof(Menupont), szam)` dönti el.
- Kisbetű-független keresés a listában: egy saját `int Hely(List<string> lista, string nev)` metódus, amely a keresés tételét valósítja meg (`string.Equals(…, StringComparison.OrdinalIgnoreCase)`), és `-1`-et ad vissza, ha nincs találat. A törléshez és a hozzáadáshoz is ezt használd.
- A lista a metódus paramétereként is ugyanaz a lista (referenciatípus), ezért a metódusban végzett `Add` és `RemoveAt` a hívónál is látszik.
- Szövegrészlet keresése: `tetel.Contains(reszlet, StringComparison.OrdinalIgnoreCase)`.

Megoldás: [../../megoldasok/F02-bevasarlolista/](../../megoldasok/F02-bevasarlolista/)
