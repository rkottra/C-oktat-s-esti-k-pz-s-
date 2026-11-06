# Megoldás – 13-M02-L04-F01 · Geometriai számológép

- A menü és a számolás szétválik: a `switch` csak beolvas, metódust hív és kiír. Így a számoló metódusok egymástól függetlenül tesztelhetők és újrahasznosíthatók.
- A `BekerPozitiv` háromszor szerepel a háromszögnél, de a beolvasás és az ellenőrzés kódja csak egyszer van megírva. Ez a metódusok legfőbb haszna.
- A `Szerkesztheto` közvetlenül egy logikai kifejezést ad vissza; nincs szükség `if (…) return true; else return false;` szerkezetre.
- A háromszög kerületéhez nem írtunk külön metódust, mert egy egyszerű összeg. Mindent nem érdemes metódusba tenni, csak azt, aminek neve és önálló jelentése van.
- A `case` ágakban deklarált változók (`r`, `a`, `b`) az egész `switch` blokkban láthatók, ezért a háromszögnél más neveket (`x`, `y`, `z`) használunk.
