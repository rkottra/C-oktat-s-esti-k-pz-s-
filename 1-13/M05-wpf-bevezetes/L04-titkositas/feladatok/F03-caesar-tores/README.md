# 13-M05-L04-F03 · Caesar-törő gyakoriságelemzéssel

**Nehézség:** ★★★ **Becsült idő:** 90 perc

## Feladat

Elkaptunk egy Caesar-rejtjellel titkosított üzenetet, de nem ismerjük a kulcsot. Készíts `CaesarTores` nevű WPF-alkalmazást, amely segít feltörni. A próbaüzenet (ékezetek nélkül):

```
Rclefwlwfyv! L aczrclxzkldt gpcdpyj fezwdz qpwlolel pkkpw l etevzdtelddlw vlandzwlezd.
Pdkcpgpeepo, szrj lxtvzc l vfwndze ypx tdxpcefv, l mpefv rjlvzctdlrl pwpr gzwe l xprqpuepdspk?
```

1. **Logika** egy `static class Tores` osztályban:
   - `static int[] Gyakorisag(string szoveg)`: 26 elemű tömb a betűk előfordulásával, kis- és nagybetűtől függetlenül (megszámlálás);
   - `static int KulcsLeggyakoribbAlapjan(string szoveg)`: **1. ötlet**: feltételezi, hogy a leggyakoribb rejtett betű az `e` képe, és ebből számolja a kulcsot;
   - `static int ValoszinuKulcs(string szoveg)`: **2. ötlet**: mind a 26 kulcsot **pontozza**, és a legjobbat adja vissza. Egy kulcs pontszáma: minden rejtett betű darabszáma szorozva annak a betűnek a magyar gyakoriságával, amivé az adott kulccsal visszafejtve válna, ezek összege. A közelítő magyar gyakoriságok (%), az ékezetes betűket az ékezet nélküli párjukhoz számolva:

     | A | B | C | D | E | F | G | H | I | J | K | L | M | N | O | P | Q | R | S | T | U | V | W | X | Y | Z |
     |---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|
     | 12,4 | 1,9 | 0,6 | 1,9 | 13,2 | 0,9 | 3,6 | 1,3 | 4,9 | 1,1 | 4,7 | 6,5 | 3,5 | 5,7 | 7,2 | 1,1 | 0 | 4,6 | 6,2 | 7,4 | 2,7 | 1,9 | 0 | 0 | 2,1 | 4,3 |

   - `static string Visszafejt(string szoveg, int k)`: Caesar-visszafejtés körbefordulással.
2. **Felület:**
   - bal oldalon a rejtett szöveg (többsoros `TextBox`) és egy **Elemzés** gomb;
   - középen a **gyakorisági oszlopdiagram**: 26 oszlop `A`-tól `Z`-ig, kódból létrehozott `Rectangle`-ökkel. A magasság arányos a gyakorisággal, a leggyakoribb betű oszlopa más színű;
   - alatta egy `Label`, amely a két ötlet eredményét írja ki, például `Leggyakoribb = e alapján: k = 7 · pontozással: k = 11`;
   - jobb oldalon egy `ListBox` mind a **26 lehetséges kulcs** visszafejtésével (`k = 3: …`), soronként az első 40 karakterrel. A pontozással kapott kulcs legyen kijelölve;
   - alul a kijelölt kulcshoz tartozó **teljes** visszafejtett szöveg.
3. Futtasd le a próbaüzeneten! Melyik ötlet találja el a kulcsot, és miért téved a másik? Írd a választ a program egy `TextBlock`-jába vagy a megoldásod README-jébe.

## Tippek

- A diagram egy `UniformGrid Rows="1" Columns="26"`. Minden cellája egy `DockPanel`, alul a betű (`TextBlock`), felette a `Rectangle` (`VerticalAlignment="Bottom"`).
- Arányos magasság: `teglalap.Height = maxMagassag * db[i] / maxDb;` (figyelj, hogy a `maxDb` ne legyen 0).
- A pontszám képletében a rejtett `i`. betű a `k` kulccsal visszafejtve az `(i - k) mod 26`. betű lesz.
- A `ListBox`-ba a kulcsokat 0-tól 25-ig sorban veszed fel, így a kijelölt sor indexe maga a kulcs.

Megoldás: [../../megoldasok/F03-caesar-tores/](../../megoldasok/F03-caesar-tores/)
