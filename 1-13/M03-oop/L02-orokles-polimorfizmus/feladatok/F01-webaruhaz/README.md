# 13-M03-L02-F01 · Webáruház termékei

**Nehézség:** ★☆☆ **Becsült idő:** 45 perc

## Feladat

Egy kis webáruház könyveket, élelmiszert és egyéb termékeket árul. A termékek bruttó árát a nettó árból az ÁFA-kulcs alapján számoljuk. Az ÁFA-kulcs általában 27%, könyvnél 5%, élelmiszernél 18%. Készíts .NET 8 konzolos alkalmazást `Webaruhaz` néven. Minden osztály külön fájlba kerüljön.

1. **`Termek`** (ősosztály):
   - `Nev` (nem lehet üres) és `NettoAr` (pozitív egész) tulajdonság, ellenőrzéssel;
   - konstruktor a két adattal;
   - `public virtual int AfaKulcs()`, amely 27-et ad vissza;
   - `public int BruttoAr()`, amely a nettó ár × (100 + ÁFA) / 100, egészre kerekítve. Ezt **nem** kell felülírni, mert a felülírt `AfaKulcs()`-ot hívja;
   - `ToString()` felülírva: név, nettó ár, ÁFA-kulcs, bruttó ár.
2. **`Konyv : Termek`**: plusz `Szerzo` tulajdonság. Az `AfaKulcs()` 5-öt ad vissza. A kiírásban a cím után a szerző is jelenjen meg (`cím – szerző`). Ezt megoldhatod a `ToString()` felülírásával, vagy úgy is, hogy a konstruktor a `base(...)`-nek már az összefűzött nevet adja át.
3. **`Elelmiszer : Termek`**: plusz `Romlando` (`bool`) tulajdonság. Az `AfaKulcs()` 18-at ad vissza, a `ToString()` romlandó terméknél a `(hűtve tárolandó)` megjegyzést is kiírja.
4. A főprogram egy `List<Termek>` **kosárba** tegyen legalább 6 vegyes terméket, majd:
   - írja ki a kosár tartalmát;
   - írja ki a kosár bruttó végösszegét és ebből az ÁFA összegét;
   - számolja meg, hány könyv van a kosárban (`is`);
   - írja ki a kosár legdrágább termékét bruttó ár alapján;
   - sorolja fel a hűtve tárolandó termékeket.

## Minta kimenet

```
Kosár tartalma:
  A Pál utcai fiúk – Molnár Ferenc       nettó   3 800 Ft  ÁFA  5%  bruttó   3 990 Ft
  Trappista sajt                         nettó   2 200 Ft  ÁFA 18%  bruttó   2 596 Ft  (hűtve tárolandó)
  USB-pendrive 64 GB                     nettó   3 500 Ft  ÁFA 27%  bruttó   4 445 Ft
  Tej 2,8% 1 l                           nettó     390 Ft  ÁFA 18%  bruttó     460 Ft  (hűtve tárolandó)
  Egri csillagok – Gárdonyi Géza         nettó   4 200 Ft  ÁFA  5%  bruttó   4 410 Ft
  Zabpehely 500 g                        nettó     650 Ft  ÁFA 18%  bruttó     767 Ft
  Vonalas füzet A4                       nettó     690 Ft  ÁFA 27%  bruttó     876 Ft
Végösszeg: 17 544 Ft, ebből ÁFA: 2 114 Ft
Könyvek száma: 2
Legdrágább: USB-pendrive 64 GB (4 445 Ft)
Hűtve tárolandó: Trappista sajt, Tej 2,8% 1 l
```

## Tippek

- A kerekítéshez: `(int)Math.Round(NettoAr * (100 + AfaKulcs()) / 100.0)`.
- A `BruttoAr()` az ősben van, mégis a leszármazott `AfaKulcs()`-át hívja: ez a többalakúság. Próbáld ki, mi történik, ha a `Konyv` osztályban elhagyod az `override` szót!
- Ezres tagolás kiíráskor: `{ar:N0}`.

Megoldás: [../../megoldasok/F01-webaruhaz/](../../megoldasok/F01-webaruhaz/)
