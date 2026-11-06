# Megoldás – 13-M02-L04-F02 · Bevásárlólista

- Az `enum` miatt a `switch` ágai beszédesek (`case Menupont.Rendezes`), nem „bűvös számok” (`case 5`). Ha később új menüpont kerül be, elég az `enum`-ot bővíteni.
- Az `Enum.IsDefined` kiszűri az érvénytelen számokat. Enélkül a `(Menupont)42` is lefordulna, és egy nem létező értéket kapnánk.
- A `Hely` metódus egyetlen helyen valósítja meg a kisbetű-független keresést. A hozzáadás (eldöntés: van-e már?) és a névvel törlés (keresés: hol van?) is ezt használja.
- A beépített `lista.Remove(nev)` nem lenne jó, mert kis- és nagybetű-érzékeny: a „Tej” nem törölné a „tej” tételt.
- A metódusok `List<string>` paramétert kapnak. Mivel a lista referenciatípus, a metódusban végzett `Add` / `RemoveAt` az eredeti listát módosítja, `ref` nem kell.
- A `BekerMenupont` a bemenet végén (`null`) kilépést ad vissza; így a program átirányított bemenettel is kipróbálható.
