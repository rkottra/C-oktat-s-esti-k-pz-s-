# 13-M02-L03-F02 · Szakkörök

**Nehézség:** ★★☆ **Becsült idő:** 60 perc

## Feladat

Az iskolában két délutáni szakkör működik. A tagok névsorát két tömb tárolja; egy tömbön belül nincs ismétlődés:

```csharp
string[] robotika = { "Kiss Anna", "Nagy Bence", "Tóth Csaba", "Szabó Dóra", "Horváth Emese", "Varga Gergő", "Molnár Hanna" };
string[] sakk     = { "Farkas Ivett", "Nagy Bence", "Balogh János", "Szabó Dóra", "Papp Kitti", "Molnár Hanna", "Lakatos Márk", "Kiss Anna" };
```

Készíts `Szakkorok` nevű programot, amely saját kóddal (LINQ nélkül) a következőket végzi el:

1. **Eldöntés:** kérj be egy nevet, és írd ki, melyik szakkörnek tagja (az egyiknek, mindkettőnek vagy egyiknek sem). A kis- és nagybetűk ne számítsanak.
2. **Metszet:** kik járnak mindkét szakkörbe?
3. **Unió:** kik járnak legalább az egyik szakkörbe? Az eredmény legyen **ábécérendben**, a rendezést is saját kóddal írd meg.
4. **Különbség:** kik járnak csak robotikára?
5. **Kiválogatás:** az unióból válogasd ki azokat, akiknek a keresztneve magánhangzóval kezdődik.
6. **Szétválogatás:** az uniót válogasd szét két csoportba vezetéknév szerint: A–K és L–Z. Az iskolai versenyre ugyanis két buszt rendelnek.

Minden eredménylistát vesszővel elválasztva, a darabszámmal együtt írj ki.

## Elvárt bemenet és kimenet

```
Név: szabó dóra
Szabó Dóra mindkét szakkör tagja.

Mindkettőben (4): Kiss Anna, Nagy Bence, Szabó Dóra, Molnár Hanna
Legalább az egyikben (11): Balogh János, Farkas Ivett, Horváth Emese, Kiss Anna, Lakatos Márk, Molnár Hanna, Nagy Bence, Papp Kitti, Szabó Dóra, Tóth Csaba, Varga Gergő
Csak robotika (3): Tóth Csaba, Horváth Emese, Varga Gergő
Magánhangzós keresztnév (3): Farkas Ivett, Horváth Emese, Kiss Anna
1. busz, A–K (4): Balogh János, Farkas Ivett, Horváth Emese, Kiss Anna
2. busz, L–Z (7): Lakatos Márk, Molnár Hanna, Nagy Bence, Papp Kitti, Szabó Dóra, Tóth Csaba, Varga Gergő
```

## Tippek

- Kis- és nagybetű nélküli összehasonlítás: `string.Equals(a, b, StringComparison.OrdinalIgnoreCase)`, vagy mindkét oldalon `ToLower()`.
- Két szöveg ábécérendje: `string.Compare(a, b) > 0`, ha `a` a `b` után következik. Ez a rendezés feltétele.
- A különbség „fordított metszet”: azokat válogatod ki, akik a másik tömbben **nincsenek** benne.
- A keresztnév a szóköz utáni rész: `nev.Split(' ')[1]`. Az első betűjét egy `"AÁEÉIÍOÓÖŐUÚÜŰ".Contains(...)` vizsgálattal döntheted el.
- Szétválogatásnál a vezetéknév első betűjét hasonlítsd össze: `nev[0] <= 'K'`. Az ékezetes nagybetűk (pl. `Á`) kódja nagyobb, mint a `Z`-é; erre a mintaadatban nincs példa, de gondold végig, mit kellene tenni!

Megoldás: [../../megoldasok/F02-szakkorok/](../../megoldasok/F02-szakkorok/)
