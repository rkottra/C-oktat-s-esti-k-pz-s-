# Megoldás – 14-M01-L01-F03 · Felhasználók és jogosultságok

- `Felhasznalo.cs`: privát jelszómező, `private set` a felhasználónéven, opcionális konstruktorparaméter, virtuális `Ellenoriz` és `Jogosultsag`.
- `Admin.cs`: `params string[]` paraméter, saját lista, felülírt `Jogosultsag` és `ToString()`.
- `Vendeg.cs`: `sealed` osztály, felülírt `Ellenoriz` (jelszó nélküli belépés).
- `Program.cs`: hibás objektumok kivételkezeléssel, jelszócsere, bejelentkezés 3 próbálkozással.

Kipróbálható belépések: `anna.k` / `Tavasz2026`, `admin` / `Admin2026!`, `vendeg` / üres jelszó.
A `bela` jelszava a program futása közben `Uj12345678`-ra változik.
