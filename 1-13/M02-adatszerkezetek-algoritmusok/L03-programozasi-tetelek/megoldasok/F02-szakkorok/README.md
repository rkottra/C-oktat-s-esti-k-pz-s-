# Megoldás – 13-M02-L03-F02 · Szakkörök

- A metszet, a különbség és az unió második fele **ugyanaz a minta**: kiválogatás, amelynek feltétele egy eldöntés (`while … j++`). Csak az számít, hogy `j < n` (benne van) vagy `j == n` (nincs benne) alapján válogatunk.
- Az eredménytömböt a legrosszabb esetre méretezzük (metszetnél a kisebbik tömb hossza, uniónál a kettő összege), a végén pedig `Array.Resize`-zal levágjuk.
- A szövegek rendezése ugyanúgy működik, mint a számoké. A `<` reláció helyett a `string.Compare(a, b) < 0` dönti el, hogy `a` előrébb van-e az ábécében. Az aktuális (magyar) kultúrában a `Compare` az ékezetes betűket is jó helyre sorolja.
- Az 1. feladatnál a tömbbeli, „szép” alakot írjuk vissza, nem a felhasználó által begépeltet. Ezt a keresés indexe teszi lehetővé.
- A szétválogatás `nev[0] <= 'K'` feltétele karakterkódokat hasonlít össze. Az `Á`, `Ö`, `Ü` kódja nagyobb a `Z`-énél, így például egy „Ábel” vezetéknév a 2. buszra kerülne. Pontosabb: `string.Compare(nev[..1], "L") < 0`.
- A `Kiir` lokális függvény nélkül hatszor kellene ugyanazt a kiíró sort megírni. A saját metódusokról a következő lecke szól.
