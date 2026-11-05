# 14-M06-L05-F02 · Kódátnézés: hibák és rossz megoldások felderítése

**Nehézség:** ★★☆ **Becsült idő:** 45 perc (20 perc egyéni átnézés + 25 perc közös megbeszélés és javítás)

## Feladat

A [kiindulo/OlimpiaRossz](kiindulo/OlimpiaRossz/) program ugyanazokat a feladatokat próbálja megoldani, mint az F01 – lefordul és le is fut, de **hibás eredményeket ad**, és nehezen olvasható.

1. **Előbb csak olvasd** (ne futtasd!): a lecke ellenőrzőlistája alapján írj egy átnézési jegyzőkönyvet az alábbi formában. Minden megállapításhoz adj meg sorszámot vagy kódrészletet, kategóriát és javaslatot.

   | # | Hol? | Kategória | Megállapítás | Javaslat |
   |---|---|---|---|---|
   | 1 | `for (int i = 1; i < s.Length - 1; …)` | hiba | … | … |

   Kategóriák: **hiba** (rossz eredmény / összeomlás), **olvashatóság**, **ismétlés**, **hatékonyság**, **hibakezelés**, **felesleges kód**.
2. Futtasd a programot, és vesd össze a kimenetét az F01 minta kimenetével. Melyik megállapításodat igazolta a futás? Találtál-e újat?
3. Párban cseréljétek ki a jegyzőkönyvet: mit talált a társad, amit te nem?
4. Javítsd ki a programot úgy, hogy helyes eredményt adjon, és megfeleljen az ellenőrzőlistának (új projektként, `OlimpiaJavitott` néven).

Cél: legalább **5 hibát** és **4 egyéb** (olvashatósági, ismétlési stb.) problémát találj.

## Tippek

- Számold meg kézzel a fájl sorait, és hasonlítsd össze a program által kiírt darabszámmal.
- Nézd meg, mi történik, ha a 4. feladatban egy város háromszor szerepel.
- Írd be a 6. feladatnál: `japán`. És: `Japán`.
- Nézd meg az `europai.txt` méretét a futás után.

Megoldás (javított program + a teljes hibalista): [../../megoldasok/F02-kodatnezes/](../../megoldasok/F02-kodatnezes/)
