# 13-M02-L04-F03 · Felvételi rangsor

**Nehézség:** ★★★ **Becsült idő:** 90 perc

## Feladat

Egy technikum három szakra vesz fel tanulókat. A jelentkezők adatai pontosvesszővel tagolt sorokban érkeznek (név; szak; hozott pont; írásbeli pont). Másold be őket a programodba:

```csharp
string[] sorok =
{
    "Kiss Anna;Szoftverfejleszto;48;41",
    "Nagy Bence;Rendszergazda;39;35",
    "Tóth Csaba;Szoftverfejleszto;45;44",
    "Szabó Dóra;Grafikus;50;38",
    "Horváth Emese;Szoftverfejleszto;37;29",
    "Varga Gergő;Rendszergazda;44;47",
    "Molnár Hanna;Grafikus;41;45",
    "Farkas Ivett;Szoftverfejleszto;49;46",
    "Balogh János;Rendszergazda;30;26",
    "Papp Kitti;Grafikus;35;31",
    "Lakatos Márk;Szoftverfejleszto;42;44",
    "Fekete Nóra;Rendszergazda;46;40",
};
```

Az egyes szakokra felvehető létszám: szoftverfejlesztő 3, rendszergazda 2, grafikus 2.

Készíts `FelveteliRangsor` nevű programot.

1. Definiálj egy `enum Szak { Szoftverfejleszto, Rendszergazda, Grafikus }` típust és egy `struct Jelentkezo` struktúrát a `Nev`, `Szak`, `Hozott` és `Irasbeli` mezőkkel. Legyen egy `Osszpont` tulajdonsága vagy metódusa is (hozott + írásbeli).
2. Írj egy `Jelentkezo Feldolgoz(string sor)` metódust, amely egy sorból jelentkezőt készít, majd töltsd fel a jelentkezőket egy `List<Jelentkezo>` listába.
3. Írd ki, hány jelentkező van összesen, és szakonként hányan jelentkeztek. Mennyi az összpontszámok átlaga szakonként?
4. Ki érte el a **legmagasabb összpontszámot**, és melyik szakra jelentkezett?
5. Volt-e olyan jelentkező, akinek az írásbelije **jobb** volt, mint a hozott pontja? Sorold fel őket.
6. Készítsd el szakonként a **rangsort**: összpontszám szerint csökkenő sorrend, egyenlőség esetén a magasabb írásbeli pont dönt. Jelöld, ki fér bele a keretbe (`FELVÉVE`), és ki kerül várólistára.
7. Kérj be egy nevet, és írd ki az illető eredményét: a szakját, az összpontszámát, a helyezését a szakon belül, és hogy felvették-e. Ha nincs ilyen jelentkező, írd ki.

Minden kérdést külön **metódus** oldjon meg (például `double Atlag(List<Jelentkezo> lista, Szak szak)`), és a tételeket saját kóddal írd meg; csak a 6. pontban használhatod a `List.Sort`-ot.

## Elvárt kimenet (részlet)

```
Jelentkezők: 12
  Szoftverfejleszto: 5 fő, átlag 85,0 pont
  Rendszergazda: 4 fő, átlag 76,8 pont
  Grafikus: 3 fő, átlag 80,0 pont
Legmagasabb összpontszám: Farkas Ivett (Szoftverfejleszto), 95 pont
Írásbelin jobb, mint a hozott pont: Varga Gergő, Molnár Hanna, Lakatos Márk

Szoftverfejleszto (keret: 3)
  1. Farkas Ivett          95  FELVÉVE
  2. Tóth Csaba            89  FELVÉVE
  3. Kiss Anna             89  FELVÉVE
  4. Lakatos Márk          86  várólista
  5. Horváth Emese         66  várólista
...
Név: tóth csaba
Tóth Csaba – Szoftverfejleszto, 89 pont, 2. hely, felvéve
```

## Tippek

- Darabolás: `string[] d = sor.Split(';');`. A szak szövegből: `Enum.Parse<Szak>(d[1])`.
- A szakonkénti adatokhoz használhatsz `enum`-mal indexelt tömböt: `int[] keret = { 3, 2, 2 };`, és `keret[(int)szak]`.
- A szakok bejárása: `foreach (Szak sz in Enum.GetValues<Szak>())`.
- A 6. pont rendezése egy összehasonlító lambdával: `rangsor.Sort((a, b) => b.Osszpont != a.Osszpont ? b.Osszpont.CompareTo(a.Osszpont) : b.Irasbeli.CompareTo(a.Irasbeli));` (a `b` és az `a` sorrendje adja a **csökkenő** rendezést).
- Egy struktúrába metódust is írhatsz: `public int Osszpont() => Hozott + Irasbeli;`
- Igazított kiírás: `$"{nev,-20}{pont,4}"`.

Megoldás: [../../megoldasok/F03-felveteli-rangsor/](../../megoldasok/F03-felveteli-rangsor/)
