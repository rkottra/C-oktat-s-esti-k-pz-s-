# Megoldás – 13-M04-L02-F03 · Osztálynapló két fájlból

- `Diak.cs`: a `Jegy` és a `Diak` osztály. A diák **tartalmazza** a jegyeit (`List<Jegy>`). Az `Atlag()` túlterhelt: paraméter nélkül az összesített, tantárgynévvel a tantárgyi átlagot adja. A `Bukik()` a tantárgyi átlagokra épül.
- `Program.cs`: kétlépcsős beolvasás. Előbb a diákok, aztán a jegyek; minden jegysornál a `Keres` metódus kikeresi a diákot az azonosító alapján. Utána a menü hat menüpont-metódussal.

Megjegyzések:

- **A két fájl összekapcsolása** a keresés tétele. Adatbázisban ezt *kapcsolatnak* (join) hívják; a 2/14. évfolyam EF Core-os leckéiben ugyanez a gondolat tér vissza.
- A `Keres` `Diak?` típust ad vissza, és `null`-lal jelzi, ha nincs találat. Ezt a hívó helyen mindig ellenőrizni kell.
- Az új jegyet a program **azonnal** a fájl végéhez fűzi (`File.AppendAllText`), így nem kell a teljes fájlt újraírni, és program-összeomlás esetén sem vész el.
- A tantárgyak és az osztályok listáját az adatokból gyűjtjük ki, így egy új tantárgy (például `Fizika`) jegyei maguktól megjelennek a táblázatban.
- A mintaadatokban a 13A átlaga matematikából 3,78, a 13B-é 3,52. Bukásra áll Csonka Márton (angol 1,75) és Gulyás Ádám (matematika 1,33).

Futtatás: F5 (Visual Studio).
