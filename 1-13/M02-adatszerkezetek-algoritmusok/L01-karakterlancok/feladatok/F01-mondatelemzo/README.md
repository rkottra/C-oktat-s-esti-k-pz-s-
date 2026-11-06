# 13-M02-L01-F01 · Mondatelemző

**Nehézség:** ★☆☆ **Becsült idő:** 30 perc

## Feladat

Készíts `Mondatelemzo` nevű konzolos programot, amely bekér egy mondatot, majd kiírja:

1. a mondatot **visszafelé** (ciklussal, ne kész metódussal);
2. hány **„a” betű** van benne – a kis- és nagybetű ne számítson (`A` és `a` is);
3. hány **betű** van benne (szóközök, számjegyek és írásjelek nem számítanak; az ékezetes betűk igen);
4. hány **szóból** áll (a szavakat egy vagy több szóköz választhatja el);
5. melyik a **leghosszabb szava** (az írásjeleket a szó végéről vágd le).

## Minta bemenet és kimenet

```
Írj be egy mondatot: Szép napunk van ma, Anna!
Visszafelé: !annA ,am nav knupan pézS
'a' betűk: 5
Betűk: 19
Szavak: 5
Leghosszabb szó: napunk
```

## Tippek

- Visszafelé: `for (int i = mondat.Length - 1; i >= 0; i--)`.
- Kis- és nagybetű: alakítsd az egészet kisbetűssé (`ToLower()`), és úgy számolj.
- Betű-e? `char.IsLetter(c)`.
- Szavak: `Split(' ', StringSplitOptions.RemoveEmptyEntries)`, írásjel levágása: `szo.Trim('.', ',', '!', '?')`.
- Extra: oldd meg a szavak számolását `Split` nélkül is (szóköz utáni nem szóköz karakter = új szó kezdete).

Megoldás: [../../megoldasok/F01-mondatelemzo/](../../megoldasok/F01-mondatelemzo/)
