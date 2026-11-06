# 13-M03-L03-F02 · Kerttervező

**Nehézség:** ★★☆ **Becsült idő:** 60 perc

## Feladat

Egy iskolakertben különböző alakú ágyásokat terveznek. Az ágyások köré szegélyt raknak, és mindegyikbe egy-egy növényt vetnek. Készíts `Kerttervezo` nevű .NET 8 konzolos alkalmazást, minden osztályt külön fájlban.

1. **`abstract class Agyas`**:
   - `Noveny` (szöveg) és `VetomagPerNm` (gramm/m², pozitív) tulajdonság;
   - `static int Darabszam`: hány ágyást hoztak létre eddig; a konstruktor növeli. Az `Azonosito` tulajdonság ebből képződik: `Á1`, `Á2`, …;
   - **absztrakt** metódusok: `double Terulet()`, `double Kerulet()`, `string Alak()`;
   - kész metódusok: `double Vetomag()` (terület × vetőmag/m²) és `ToString()` (azonosító, alak, növény, terület, kerület, vetőmag).
2. Leszármazottak, mindegyik a saját méreteivel:
   - **`TeglalapAgyas`** (a, b);
   - **`KorAgyas`** (sugár);
   - **`HaromszogAgyas`** (három oldal). Ha az oldalakból nem szerkeszthető háromszög, a konstruktor `ArgumentException`-t dobjon;
   - **`LAgyas`**: egy nagy téglalapból (a × b) kivágunk egy kisebbet (c × d) az egyik sarokban. A kerülete ugyanannyi, mint a nagy téglalapé!
3. A főprogram:
   - hozzon létre legalább 6 ágyást egy `List<Agyas>` listában. Próbálj meg egy szerkeszthetetlen háromszöget is felvenni, és a `try`–`catch` írja ki a hibát;
   - írja ki az ágyásokat, majd az összes területet és a szegélyhez szükséges összes hosszt;
   - írja ki növényenként az összes szükséges vetőmagot. Ehhez előbb gyűjtsd ki egy listába a különböző növényneveket;
   - írja ki a legnagyobb területű ágyást, és az ágyásokat terület szerint csökkenő sorrendben;
   - írja ki, hány ágyás készült (`Agyas.Darabszam`). Figyeld meg, hogy a hibás háromszög is beleszámít-e!
4. Írd be a programba (kikommentezve) a `new Agyas(...)` sort, és jegyezd fel a fordító hibaüzenetét.

## Minta kimenet (a megoldás ágyásaival)

```
Hiba: 1, 2, 10 oldalakból nem szerkeszthető háromszög.

Á1  téglalap   paradicsom   T =  6,00 m²  K = 10,00 m  vetőmag:  12,0 g
Á2  kör        sárgarépa    T =  3,14 m²  K =  6,28 m  vetőmag:  12,6 g
Á3  háromszög  hagyma       T =  6,00 m²  K = 12,00 m  vetőmag:  36,0 g
Á4  L-alak     paradicsom   T =  9,00 m²  K = 14,00 m  vetőmag:  18,0 g
Á5  téglalap   bab          T =  4,80 m²  K = 10,40 m  vetőmag:  38,4 g
Á7  kör        saláta       T =  7,07 m²  K =  9,42 m  vetőmag:  21,2 g

Összes terület: 36,01 m², szegély: 62,11 m
Vetőmag növényenként:
  paradicsom     30,0 g
  sárgarépa      12,6 g
  ...
Legnagyobb ágyás: Á4 (L-alak, 9,00 m²)
...
Létrehozott ágyás-objektumok: 7, a kertben: 6
```

Hová lett az `Á6`?

## Tippek

- Az absztrakt osztály konstruktora `protected` legyen; a leszármazottak `: base(noveny, vetomagPerNm)` hívással érik el.
- A `Darabszam` növelése a konstruktor elején vagy végén történjen? Ha a leszármazott konstruktora kivételt dob, az ős konstruktora **már lefutott**. Gondold végig, mit jelent ez a számlálóra!
- Az L-alak területe: `a·b − c·d`, a kerülete: `2·(a + b)`. Ellenőrizd, hogy `c < a` és `d < b`.
- Rendezés csökkenő sorrendben: `kert.Sort((x, y) => y.Terulet().CompareTo(x.Terulet()));`

Megoldás: [../../megoldasok/F02-kerttervezo/](../../megoldasok/F02-kerttervezo/)
