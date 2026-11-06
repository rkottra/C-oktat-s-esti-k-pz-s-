# Megoldás – 13-M03-L02-F01 · Webáruház termékei

- `Termek.cs`: ellenőrzött `Nev` és `NettoAr`, `virtual AfaKulcs()`, nem virtuális `BruttoAr()`, felülírt `ToString()`.
- `Konyv.cs`, `Elelmiszer.cs`: `: base(...)` a konstruktorban, `override AfaKulcs()`. Az `Elelmiszer` a `ToString()`-et is felülírja, és a `base.ToString()`-et egészíti ki.
- `Program.cs`: polimorf `List<Termek>`, összegzés, megszámlálás `is`-szel, maximumkiválasztás, kiválogatás `is Elelmiszer e` mintával.

Megjegyzések:

- A lényeg a `BruttoAr()`. Csak egyszer, az ősben van megírva, mégis mindhárom típusnál jó eredményt ad, mert a benne hívott `AfaKulcs()` virtuális: futás közben mindig az objektum valódi típusának változata fut le.
- A `Konyv` a szerzőt a névbe is beépíti (`base($"{cim} – {szerzo}", …)`), ezért nem kellett felülírni a `ToString()`-et. A feladat szövege szerinti, `base.ToString()`-et kiegészítő megoldás is helyes.
- Ha a `Konyv`-ből kitöröljük az `override` szót, a fordító figyelmeztet (elfedés), és a könyvek bruttó ára hibásan, 27%-kal számolódik.

Futtatás: F5 (Visual Studio) vagy `dotnet run`.
