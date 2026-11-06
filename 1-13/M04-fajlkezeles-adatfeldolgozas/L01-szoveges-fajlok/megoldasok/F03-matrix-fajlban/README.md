# Megoldás – 13-M04-L01-F03 · Mátrix fájlba írása és visszaolvasása

- A feltöltés után a program `StreamWriter`-rel, soronként írja ki a mátrixot. A sort szövegként fűzi össze, az utolsó elem után nem tesz vesszőt.
- A visszaolvasás **egy másik** mátrixba történik (`beolvasott`), és a méreteket a fájl tartalmából számolja. Ez bizonyítja, hogy a további számítások valóban a fájlból dolgoznak.
- A kiíró kettős ciklus egyszerre számolja a sorösszeget, gyűjti az oszlopösszegeket egy tömbbe, és keresi a maximumot a helyével együtt.
- A teljes összeg az oszlopösszegek összege; ebből jön az átlag is.

Bővítési ötlet: a program kérje be a fájl nevét, és ha a fájl létezik (`File.Exists`), ne generáljon új mátrixot, hanem csak olvassa be és dolgozza fel.

Futtatás: F5 (Visual Studio). A `matrix.txt` a `bin\Debug\net8.0` mappában jön létre.
