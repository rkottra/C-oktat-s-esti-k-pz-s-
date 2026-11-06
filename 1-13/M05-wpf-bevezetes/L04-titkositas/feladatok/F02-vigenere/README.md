# 13-M05-L04-F02 · Vigenère-titkosító

**Nehézség:** ★★☆ **Becsült idő:** 60 perc

## Feladat

Készíts `Vigenere` nevű WPF-alkalmazást, amely a Vigenère-rejtjellel titkosít és fejt vissza.

1. **Logika** egy `static class VigenereRejtjel` osztályban:
   - `static bool ErvenyesKulcs(string kulcs)`: legalább egy karakter, és csak az angol ábécé betűiből áll;
   - `static string Titkosit(string szoveg, string kulcs)` és `static string Visszafejt(string szoveg, string kulcs)`:
     - a kulcs `i`. betűje adja az eltolást (`A`/`a` = 0, `B`/`b` = 1, …), a kulcs kis- és nagybetűje ugyanazt jelenti;
     - a szöveg betűinek kis- vagy nagybetűs volta megmarad;
     - a **nem betű** karakterek változatlanok, és a kulcsban **nem lépnek tovább**.
   - `static string KulcsSor(string szoveg, string kulcs)`: ugyanolyan hosszú szöveg, mint a bemenet, amelyben minden betű alatt a hozzá tartozó kulcsbetű áll, a többi helyen szóköz. Ez a szemléltetéshez kell.
2. **Felület:**
   - kulcs `TextBox`; érvénytelen kulcsnál a háttere legyen piros, és a gombok legyenek letiltva;
   - bemeneti `TextBox`, alatta **Titkosít** és **Visszafejt** gomb;
   - kimeneti `TextBox`;
   - egy egysoros, `Consolas` betűs „szemléltető” rész: a bemenet első sora, alatta a kulcssor, alatta a kimenet első sora.

## Elvárt eredmény

```
Szöveg:  Tamadas hajnalban!
Kulcs:   KULCS
         KULCSKU LCSKULCSK
Rejtett: Duxcvkm scbxuwdsx!
```

## Tippek

- Az eltolás egy betűre: `int k = char.ToUpper(kulcs[j % kulcs.Length]) - 'A';`, ahol `j` a **betűk** számlálója, nem a szöveg indexe.
- A Caesar `Tol(c, k)` metódusát (lásd a példát) újrahasznosíthatod: titkosításnál `+k`, visszafejtésnél `-k`. A közös részt egyetlen privát metódusba teheted, amely egy `irany` (+1/−1) paramétert kap.
- Monospace betűtípus (`FontFamily="Consolas"`) nélkül a kulcssor betűi nem kerülnek a szöveg betűi alá.

Megoldás: [../../megoldasok/F02-vigenere/](../../megoldasok/F02-vigenere/)
