# Megoldás – 13-M04-L01-F01 · Számok statisztikája fájlból

- Beolvasás `File.ReadAllLines`-szal egy `List<int>`-be, az üres sorok kihagyásával.
- Összegzés, minimum- és maximumkiválasztás egy ciklusban. A maximumnál az **indexet** tároljuk, így a sorszám is megvan (`maxIndex + 1`).
- Három megszámlálás `if … else if … else` szerkezettel.
- Eldöntés `while` ciklussal: az `i < n` feltétel áll elöl, hogy a ciklus ne lépjen túl a lista végén.
- A páros számok kiírásánál a sorokat egy `List<string>`-ben gyűjtjük, és egyetlen `File.WriteAllLines` hívással mentjük. A ciklus után a maradék, nem teli sort is hozzá kell adni.
- A mediánt egy rendezett **másolaton** számoljuk; a szórás képletében az átlagtól való eltérések négyzetét összegezzük.

A projekt `.csproj` fájljában a `szamok.txt`-hez `CopyToOutputDirectory` beállítás tartozik, ezért a program a futtatási könyvtárban (`bin\Debug\net8.0`) megtalálja. A `parosak.txt` is ide kerül.

Futtatás: F5 (Visual Studio).
