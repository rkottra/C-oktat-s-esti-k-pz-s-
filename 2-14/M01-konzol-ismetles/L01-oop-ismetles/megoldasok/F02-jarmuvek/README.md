# Megoldás – 14-M01-L01-F02 · Járműflotta

- `Jarmu.cs`: absztrakt ősosztály, ellenőrzött `Rendszam` és `GyartasiEv`, számított `Kor`, absztrakt `EvesDij()`, virtuális `Tipus`.
- `Szemelyauto.cs`, `Teherauto.cs`, `Motor.cs`: `base(...)` konstruktorhívás, `override` a díjszámításra és a `ToString()`-re.
- `Program.cs`: polimorf lista, összegzés, típusvizsgálat `is` mintával.

A kor az aktuális évből számolódik, ezért a kiírt életkorok és a 10 évnél idősebb autók díja évről évre változhat.
