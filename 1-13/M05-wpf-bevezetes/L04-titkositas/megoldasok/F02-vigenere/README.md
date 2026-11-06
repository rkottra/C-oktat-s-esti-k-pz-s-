# Megoldás – 13-M05-L04-F02 · Vigenère-titkosító

- `VigenereRejtjel.cs`: a titkosítás és a visszafejtés egyetlen privát `Alkalmaz(szoveg, kulcs, irany)` metódus, amely csak az eltolás előjelében különbözik. A `j` számláló **csak betűnél lép**, így a szóközök és az írásjelek nem „fogyasztanak” kulcsbetűt. A `KulcsSor` ugyanezzel a logikával építi a szemléltető sort.
- `MainWindow.xaml.cs`: a kulcs minden változásakor ellenőriz (piros háttér, letiltott gombok); a gombok a logikát hívják, a `Szemleltet` pedig az első sort, a kulcssort és a kimenetet írja egymás alá.

Megjegyzések:

- Ellenőrzés: `Tamadas hajnalban!` + `KULCS` → `Duxcvkm scbxuwdsx!`; a visszafejtés pedig visszaadja az eredetit.
- A kulcs kis- és nagybetűje ugyanazt jelenti (`char.ToUpper`), a szöveg betűinek kis- vagy nagybetűs volta megmarad.
- Ha a kulcs egyetlen betű, a Vigenère a Caesarral egyezik meg: például a `D` kulcs 3-as eltolás.

Futtatás: F5 (Visual Studio).
