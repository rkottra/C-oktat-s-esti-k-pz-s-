# Megoldás – 13-M04-L01-F02 · Raktárkészlet CSV-ből

- `Termek.cs`: a konstruktor egy CSV-sort kap, és `Split(';')` után a darabokat a megfelelő típusra alakítja. Ez a **CSV-sor → objektum** minta. A `KeszletErtek` számított tulajdonság, a `ToString()` egy sorban írja ki a terméket.
- `Program.cs`: a fejléc kihagyásával `List<Termek>`-be olvas. Utána a kérdések sorban: összegzés, kiválogatás, minimumkiválasztás, összegzés számított tulajdonságon, megszámlálás, majd megszámlálás és keresés egy ciklusban. Végül fájlba ír `StreamWriter`-rel.

Megjegyzések:

- A 7. kérdésnél egyetlen bejárás alatt számolunk és keresünk. A `dragaTalalat == null` feltétel gondoskodik róla, hogy az **első** drága terméket jegyezzük meg.
- Ha a `rendelendo.txt` írásakor elmarad a `using` (vagy a `Close()`), a fájl üres maradhat, mert az adat a pufferben ragad.
- A `termekek.csv`-hez a `.csproj`-ban `CopyToOutputDirectory` beállítás tartozik.

Futtatás: F5 (Visual Studio).
