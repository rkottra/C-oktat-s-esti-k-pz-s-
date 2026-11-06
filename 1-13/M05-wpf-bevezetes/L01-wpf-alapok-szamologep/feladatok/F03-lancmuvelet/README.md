# 13-M05-L01-F03 · Láncművelet

**Nehézség:** ★★★ **Becsült idő:** 60 perc

## Feladat

A bemutató számológép ([peldak/Szamologep](../../peldak/Szamologep/)) egyszerre csak egy műveletet tud: `3 + 4 =`. Egy igazi számológép ennél többet tud. Készíts új projektet `SzamologepLanc` néven a példából kiindulva (egész számokkal), és valósítsd meg:

1. **Láncművelet**: ha egy szám után műveleti jelet nyomunk, és már van függő művelet, akkor a számológép előbb kiszámolja a részeredményt, és megjeleníti a kijelzőn.
   A műveleteket balról jobbra végezzük (a szorzásnak itt **nincs** elsőbbsége, ahogy az egyszerű számológépeken sem).
2. **Jelcsere**: ha két műveleti jelet nyomunk egymás után, a második lecseréli az elsőt.
3. **Ismételt egyenlőség**: az `=` újbóli megnyomása megismétli az utolsó műveletet az utolsó operandussal.
4. A felső sorban (`MuveletLabel`) mindig az egész lánc látszódjon.

## Minta

```
3 + 4 + 5 - 6 =       → kijelző: 7, majd 12, végül 6;  felső sor: 3 + 4 + 5 - 6 =
3 + 3 = = =           → 6, 9, 12
2 * 3 + 4 =           → 10  (balról jobbra)
5 + * 2 =             → 10  (a * lecserélte a +-t)
```

## Tippek

- Tárolj egy `reszeredmeny` mezőt az eddigi lánc értékével, és egy `muvelet` mezőt a függő művelettel.
- A számolást tedd külön metódusba, mert két helyről is hívod (műveleti gomb és `=`):
  `bool Szamol(string jel, long a, long b, out long eredmeny)` – `false`, ha nullával osztanánk.
- Az ismételt `=`-hez jegyezd meg az utolsó műveletet és a második operandust (`utolsoMuvelet`, `utolsoOperandus`).
- Gondold végig a három esetet műveleti jel lenyomásakor: nincs függő művelet / van, és új szám jött / van, de nem jött új szám.

Megoldás: [../../megoldasok/F03-lancmuvelet/](../../megoldasok/F03-lancmuvelet/)
