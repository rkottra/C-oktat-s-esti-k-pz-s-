# 13-M02-L02-F02 · Domborzat – repülés a hegyek felett

**Nehézség:** ★★☆ **Becsült idő:** 60 perc

## Feladat

Egy kisrepülőgép egyenes vonalban repül, és kilométerenként megméri az alatta lévő terep tengerszint feletti magasságát (méterben). A 0 érték tengert jelent. Készíts `Domborzat` nevű programot, amely egy 40 elemű tömbben **szimulálja** a méréseket, majd válaszol a kérdésekre.

**Az adatok előállítása:** az első mérés 0 és 300 közötti véletlen szám. Minden további mérés az előzőből úgy adódik, hogy hozzáadunk egy -150 és +150 közötti véletlen számot. Ha az eredmény negatív, 0 lesz belőle (tenger), ha 1500 fölötti, akkor 1500. Hogy az eredmény ellenőrizhető legyen, a `Random` objektumot fix maggal hozd létre: `new Random(21)`.

1. Írd ki a méréseket egy sorban, szóközzel elválasztva.
2. Mekkora volt a **legnagyobb magasság**, és hányadik kilométernél mérték először? (Az 1. mérés az 1. km.)
3. Átrepült-e a gép **tenger** fölött? Ha igen, hányadik kilométernél először?
4. Hány **csúcs** van? Csúcsnak számít az a mérés, amely mindkét szomszédjánál magasabb. Az első és az utolsó mérés nem lehet csúcs.
5. Melyik a **leghosszabb emelkedő szakasz**, vagyis a leghosszabb olyan egymás utáni mérés-sorozat, ahol minden érték nagyobb az előzőnél? Add meg a kezdő- és a végpontját kilométerben.
6. Rajzold ki a domborzat **profilját** egy 10 soros, 40 oszlopos `char[,]` mátrixba. Egy oszlopban annyi `#` jel legyen alulról, amennyi a `magasság / 150` (felfelé kerekítve, de legfeljebb 10), a többi cella szóköz. A tenger oszlopába a legalsó sorba `~` kerüljön. Végül írd ki a mátrixot soronként.

## Elvárt kimenet (21-es maggal)

```
Mérések: 209 353 337 199 255 379 323 467 490 345 424 533 403 371 426 319 376 230 153 137 169 19 31 0 78 0 147 207 161 310 442 588 661 666 809 861 839 897 1029 1144
Legnagyobb magasság: 1144 m, először a 40. km-nél
Tenger fölött: igen, először a 24. km-nél
Csúcsok száma: 11
Leghosszabb emelkedő: 29. km – 36. km (8 mérés)

Profil:
                                        
                                        
                                       #
                                      ##
                                  ######
                                ########
       ##  #                   #########
 ##  ############            ###########
################### #      #############
#######################~#~##############
```

## Tippek

- A 2. kérdés maximumkiválasztás: tárold az **indexet**, ne az értéket; így a hely is megvan. Szigorú `>` relációval az első előfordulást kapod.
- A 3. kérdés keresés: `while (i < n && m[i] != 0) i++;`, a ciklus után `i < n` dönti el, hogy találtunk-e.
- A 4. kérdésnél a ciklus 1-től `n - 2`-ig megy, mert minden elemnek kell bal és jobb szomszéd.
- Az 5. kérdéshez tarts nyilván egy aktuális hosszt: ha `m[i] > m[i - 1]`, nő, különben 1-re áll vissza. Ha az aktuális hossz meghaladja az eddigi legjobbat, jegyezd fel a végpontot.
- Felfelé kerekítés egész számokkal: `(m + 149) / 150`, vagy `(int)Math.Ceiling(m / 150.0)`.
- A mátrix 0. sora van **felül**. A `#` jeleket ezért a `9 - k` indexű sorokba írd (k = 0, 1, …).

Megoldás: [../../megoldasok/F02-domborzat/](../../megoldasok/F02-domborzat/)
