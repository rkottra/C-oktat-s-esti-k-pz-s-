# Megoldás – 13-M05-L01-F02 · Számológép valós számokkal

Nyisd meg a `SzamologepBovitett.csproj`-t, és indítsd el (F5).

- A számok `double` típusúak; a kijelző szövegét a `KijelzoErteke()` alakítja számmá, rögzített magyar formátummal (`hu-HU`), így a vessző minden gépen tizedesvessző.
- A **CE** és a **C** különbsége: a CE csak a kijelzőt nullázza, a `muvelet` és az `elsoSzam` megmarad.
- A `-0` speciális eset: ha előjelváltás után számjegyet írunk, `-5` legyen, ne `-05`.
- Eredmény után a ⌫ nem töröl (az eredmény nem „beírt” szám).
