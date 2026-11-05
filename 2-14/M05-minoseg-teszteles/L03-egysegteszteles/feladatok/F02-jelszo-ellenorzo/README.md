# 14-M05-L03-F02 · Jelszóerősség-ellenőrző

**Nehézség:** ★★☆ **Becsült idő:** 40 perc

## Feladat

A [kiindulo/JelszoEllenorzo](kiindulo/JelszoEllenorzo/) osztálykönyvtár `Ellenorzo` osztálya egy regisztrációs űrlap jelszavát ellenőrzi.
A `Hibak(jelszo)` a megsértett szabályok üzeneteit adja vissza (a szövegek az osztály konstansai), az `Eros(jelszo)` pedig akkor igaz, ha nincs hiba.

A specifikáció:

| Szabály | Üzenet (konstans) |
|---|---|
| legalább 8 karakter (a 8 karakteres jelszó már jó) | `TulRovid` |
| van benne legalább egy nagybetű | `NincsNagybetu` |
| van benne legalább egy számjegy (0–9) | `NincsSzamjegy` |
| nincs benne szóköz | `SzokozVanBenne` |
| `null` jelszó esetén `ArgumentNullException` | – |

A kódban **több hiba** is van. Készíts `JelszoEllenorzoTesztek` MSTest-projektet (ProjectReference a kiinduló projektre), és tesztekkel derítsd ki, melyek ezek!

1. Írj legalább 3 olyan jelszót `[DataRow]`-ban, amelynek **erősnek** kell lennie (köztük egy pontosan 8 karakterest).
2. Minden szabályhoz írj egy tesztet olyan jelszóval, amely **csak azt az egy** szabályt sérti, és ellenőrizd, hogy a hibalista pontosan azt az egy üzenetet tartalmazza
   (`CollectionAssert.AreEqual`).
3. Teszteld az üres szöveget (melyik három szabályt sérti?) és a `null`-t.
4. Jegyezd fel a piros teszteket, javítsd a kódot, és futtasd újra a teszteket.

## Minta (javítás előtt)

```
✖ Eros_MindenSzabalyTeljesul_Igaz (Alma1234)   … hibák: Legalább 8 karakter hosszú legyen.
✖ Hibak_NincsSzamjegy_CsakEzAHiba              Expected … Actual: (üres lista)
✖ Hibak_Null_ArgumentNullException             … threw exception NullReferenceException
```

## Tippek

- Egy teszt egy dolgot ellenőrizzen: ha a „nincs számjegy” tesztben a jelszó rövid is, nem tudod, melyik szabály miatt bukott.
- `CollectionAssert.Contains(lista, elem)` – benne van-e az elem a listában.
- Hasznos üzenet a hibához: `Assert.IsTrue(feltetel, "magyarázat")` – a Test Explorerben látszik.
- Gondolkodj el: a `char.IsLetterOrDigit` és a `char.IsDigit` mire ad igazat?

Megoldás: [../../megoldasok/F02-jelszo-ellenorzo/](../../megoldasok/F02-jelszo-ellenorzo/)
