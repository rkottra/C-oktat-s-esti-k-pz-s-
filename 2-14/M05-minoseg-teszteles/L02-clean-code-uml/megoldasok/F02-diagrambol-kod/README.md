# Megoldás – 14-M05-L02-F02 · Állatorvosi rendelő

A megoldás a feladat diagramját követi, két kis bővítéssel, amelyeket a diagramba is berajzoltunk
(a kódban és a diagramban mindig ugyanannak kell szerepelnie):

- `Allat.Leiras()` absztrakt metódus – így a kiíráshoz nem kell `is Kutya` típusvizsgálat;
- `Rendelo.Gazdik` csak olvasható lista a kiíráshoz.

```mermaid
classDiagram
    class Allat {
        <<abstract>>
        +string Nev
        +int SzuletesiEv
        +Kor(int evszam) int
        +OltasiDij()* int
        +Leiras()* string
    }
    class Kutya {
        +string Fajta
        +OltasiDij() int
        +Leiras() string
    }
    class Macska {
        +bool Benti
        +OltasiDij() int
        +Leiras() string
    }
    class Gazdi {
        +string Nev
        +string Telepules
        +List~Allat~ Allatok
        +AllatotFelvesz(Allat allat)
    }
    class Rendelo {
        -List~Gazdi~ gazdik
        +IReadOnlyList~Gazdi~ Gazdik
        +UjGazdi(Gazdi gazdi)
        +OsszesAllat() List~Allat~
        +OltasiBevetel() int
        +Legidosebb(int evszam) Allat
    }
    Allat <|-- Kutya
    Allat <|-- Macska
    Gazdi "1" o-- "1..*" Allat : allatai
    Rendelo "1" o-- "*" Gazdi : gazdik
```

A `Legidosebb` a C#-ban `Allat?` típusú, mert üres rendelőben nincs legidősebb állat.
