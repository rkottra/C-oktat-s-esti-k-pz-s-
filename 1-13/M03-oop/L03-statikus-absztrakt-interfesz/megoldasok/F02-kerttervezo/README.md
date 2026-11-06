# Megoldás – 13-M03-L03-F02 · Kerttervező

- `Agyas.cs`: absztrakt ős `protected` konstruktorral, statikus `Darabszam` számlálóval, három absztrakt metódussal és két kész metódussal (`Vetomag`, `ToString`), amelyek az absztraktakat hívják.
- `Agyasok.cs`: a négy leszármazott, mindegyik a saját képleteivel. Itt egy fájlban vannak, hogy egyben látszódjanak; a feladat szerint külön fájlba is tehetők.
- `Program.cs`: polimorf lista, `try`–`catch` a hibás háromszögre, összegzés, különböző értékek kigyűjtése, növényenkénti összegzés, maximumkiválasztás, rendezés.

Megjegyzések:

- **A számláló csapdája:** a `HaromszogAgyas` konstruktorában az ellenőrzés a `base(...)` hívás **után** fut, ezért a hibás háromszögnél a `Darabszam` már nőtt. A futás végén 7 létrehozott objektum áll szemben 6 ágyással, és az `Á6` azonosító kimarad. Kétféle javítás lehetséges:
  1. az ellenőrzés egy statikus segédmetódusba kerül, amelyet a `base(...)` paraméterében hívunk meg, így az ős konstruktora előtt fut;
  2. a számlálót nem a konstruktor növeli, hanem az, aki az ágyást a kertbe teszi (pl. egy `Kert` osztály `Hozzaad` metódusa).
- Az L-alak kerülete ugyanannyi, mint a befoglaló téglalapé: a kivágás két „befelé” lépése pont annyi, amennyivel a két külső oldal rövidebb lett.
- A `Vetomag()` és a `ToString()` csak egyszer, az ősben van megírva, mégis minden alakra helyes, mert a benne hívott `Terulet()` absztrakt, vagyis mindig a leszármazott változata fut.

Futtatás: F5 (Visual Studio) vagy `dotnet run`.
