# 13-M04-L03-F01 · Életkor-kalkulátor

**Nehézség:** ★☆☆ **Becsült idő:** 45 perc

## Feladat

Készíts `Eletkor` nevű .NET 8 konzolos alkalmazást, amely egy születési dátumból különféle érdekességeket számol.

1. Kérd be a **születési dátumot** `ÉÉÉÉ.HH.NN.` formátumban (például `2008.03.15.`). Érvénytelen dátumnál (rossz formátum, nem létező nap, például `2026.02.30.`) írjon hibaüzenetet, és kérdezzen újra.
2. Kérd be a **mai dátumot** ugyanígy; üres Enterre a valódi mai napot használja (`DateTime.Today`). Így a program ellenőrizhető. A születési dátum nem lehet a mai nap után.
3. Írd ki:
   - a hét melyik napján született, **magyarul**;
   - hány éves (helyesen, a születésnapot figyelembe véve);
   - hány napja és hány hete él;
   - mikor lesz a **következő születésnapja** (dátum, a hét napja), és hány nap múlva. Ha ma van, írd ki: „Boldog születésnapot!”;
   - mikor volt vagy lesz a **10 000. napja**;
   - mikor töltötte vagy tölti be a **18. évét**.

## Minta bemenet és kimenet

```
Születési dátum (ÉÉÉÉ.HH.NN.): 2008.02.30.
Érvénytelen dátum!
Születési dátum (ÉÉÉÉ.HH.NN.): 2008.03.15.
Mai dátum (ÉÉÉÉ.HH.NN., Enter = ma): 2026.05.15.

Születésed napja: szombat
Életkor: 18 év
Eddig 6635 napot (947 hetet) éltél.
Következő születésnap: 2027.03.15. (hétfő), 304 nap múlva
A 10 000. napod: 2035.07.31. (kedd) – még hátravan
18 éves lettél: 2026.03.15. (vasárnap)
```

## Tippek

- Érvényes dátum: `DateTime.TryParseExact(s, "yyyy.MM.dd.", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime d)`.
- A beolvasást írd metódusba: `static DateTime BekerDatum(string kerdes, bool lehetUres)`.
- A hét napja magyarul: `d.ToString("dddd", new CultureInfo("hu-HU"))`.
- Következő születésnap: `szuletes.AddYears(kor + 1)`. Ha idén még nem volt, akkor `AddYears(kor)` is jöhet, ezért gondold végig! A február 29-ét az `AddYears` magától február 28-ra igazítja.
- A 10 000. nap a születésnap utáni 9999. nap (az 1. nap maga a születés napja): `szuletes.AddDays(9999)`.

Megoldás: [../../megoldasok/F01-eletkor/](../../megoldasok/F01-eletkor/)
