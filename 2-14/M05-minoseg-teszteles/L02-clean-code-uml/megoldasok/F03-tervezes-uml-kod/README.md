# Megoldás – 14-M05-L02-F03 · Edzőterem

## Osztálydiagram

```mermaid
classDiagram
    class Berlet {
        <<abstract>>
        +int Ar
        +Ervenyes(DateOnly nap)* bool
        +Belepes(DateOnly nap)
    }
    class AlkalmasBerlet {
        +int MaradekAlkalom
        +Ervenyes(DateOnly nap) bool
        +Belepes(DateOnly nap)
    }
    class IdoszakosBerlet {
        +DateOnly Kezdet
        +DateOnly Vege
        +Ervenyes(DateOnly nap) bool
    }
    class Tag {
        +int TagSzam
        +string Nev
        +Berlet? Berlet
    }
    class Belepes {
        +int TagSzam
        +DateOnly Nap
    }
    class Terem {
        -List~Tag~ tagok
        -List~Belepes~ belepesek
        +UjTag(Tag tag)
        +Belep(int tagSzam, DateOnly nap) bool
        +NapiBelepesek(DateOnly nap) int
    }
    Berlet <|-- AlkalmasBerlet
    Berlet <|-- IdoszakosBerlet
    Tag "1" --> "0..1" Berlet : berlet
    Terem "1" o-- "*" Tag : tagok
    Terem "1" *-- "*" Belepes : belepesek
```

## Döntések

- A `Belepes(nap)` az ősosztályban **virtuális**, üres törzzsel: csak az alkalmas bérletnek kell felülírnia.
- A belépések rögzítése kompozíció (`*--`): a `Belepes` objektumok a teremhez tartoznak, nélküle nincs értelmük. A tagok aggregáció (`o--`): a tag a terem nélkül is létező személy.
- A `Terem.Belep` először minden feltételt ellenőriz, és csak siker esetén módosít (alkalom levonása, rögzítés) – így sikertelen belépés nem hagy nyomot.
- Az osztály neve `Terem`, mert a projekt (és a névtér) neve már `Edzoterem`; azonos névtér- és osztálynév kellemetlen ütközést okozna.
