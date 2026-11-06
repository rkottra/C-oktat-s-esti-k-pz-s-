# Megoldás – 13-M04-L02-F01 · Iskolai könyvtár katalógusa

- `Konyv.cs`: CSV-sorból építő konstruktor, két számított tulajdonság (`Elerheto`, `Kihasznaltsag`) és táblázatsor-formátumú `ToString()`.
- `Program.cs`: beolvasás `try`–`catch`-csel (hiányzó fájlnál `return`, ami a felső szintű kódban a program végét jelenti), a menüciklus, a hat menüpont külön metódusban, valamint a közös segédmetódusok.

Megjegyzések:

- A műfajok listáját a `Mufajok` metódus a beolvasott adatokból gyűjti ki. Ha a fájlba új műfaj kerül, az almenü magától bővül. Ugyanezt a metódust használja a statisztika is.
- A toplista egy **másolatot** rendez, így az 1-es menüpont továbbra is az eredeti sorrendben listáz.
- A kölcsönzött példányok aránya 58,0%: 231 példányból 134 van kint.

Futtatás: F5 (Visual Studio). A `toplista.txt` a `bin\Debug\net8.0` mappában jön létre.
