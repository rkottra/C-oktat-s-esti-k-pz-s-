# 14-M05-L02-F01 · Parkolóház – refaktorálás

**Nehézség:** ★★☆ **Becsült idő:** 45 perc

## Feladat

A [kiindulo](kiindulo/) mappában lévő `ParkolohazRegi` program helyesen számolja a parkolási díjat, de a kódja nehezen olvasható.
**Refaktoráld** a lecke elvei szerint – a program kimenete egy karakterrel se változzon!

A díjszabás (ezt valósítja meg a régi kód is):

- legfeljebb 24 órát (1440 percet) lehet parkolni, ennél hosszabb idő hiba;
- az első 30 perc ingyenes;
- utána minden **megkezdett** óráért fizetni kell: autó 400 Ft, motor 200 Ft;
- a napi díj legfeljebb 10 óra díja lehet (autó 4000 Ft, motor 2000 Ft);
- a törzsvendég a fenti díjból 20% kedvezményt kap (egész forintra lefelé).

Lépések:

1. Hozz létre új projektet `Parkolohaz` néven, és másold át a kódot.
2. Nevezd át az osztályt, a metódust, a paramétereket és a változókat beszédes nevekre.
3. A varázsszámokat cseréld elnevezett konstansokra.
4. Szüntesd meg a motoros és az autós ág ismétlődését. A `bool m` paraméter helyett használj `JarmuTipus` felsorolást.
5. A `-1` hibakód helyett dobj `ArgumentOutOfRangeException`-t, és a `Program.cs` kapja el.
6. A `Program.cs`-ben a három párhuzamos tömb helyett egy listát használj, és szüntesd meg a négyszer ismétlődő kiíró sort.
7. Minden lépés után futtasd a programot, és hasonlítsd össze a kimenetet az eredetivel!

## Minta kimenet (a régi és az új programé is)

```
Parkolási díjak
20 perc, autó: 0 Ft
30 perc, autó: 0 Ft
31 perc, autó: 400 Ft
90 perc, autó, törzsvendég: 640 Ft
61 perc, motor: 400 Ft
600 perc, autó: 4000 Ft
700 perc, autó, törzsvendég: 3200 Ft
1440 perc, motor: 2000 Ft
1500 perc, autó: HIBA (24 óránál hosszabb)
```

## Tippek

- Visual Studióban az átnevezés: kattints a névre, majd **Ctrl+R, Ctrl+R** – minden előfordulást átír.
- Kód kiemelése új metódusba: jelöld ki, majd **Ctrl+.** → *Extract method*.
- Megkezdett órák: `(int)Math.Ceiling(percek / 60.0)`.
- Ha valami gyanúsat találsz (pl. negatív percre mit ad a program?), **ne javítsd** most – írd fel. A refaktorálás nem változtat a viselkedésen.

Megoldás: [../../megoldasok/F01-refaktoralas/](../../megoldasok/F01-refaktoralas/)
