# Megoldás – 13-M02-L01-F03 · Szövegszámológép

- Az `IndexOfAny(jelek, 1)` az első olyan helyet adja, ahol a felsorolt karakterek bármelyike áll, az 1. indextől kezdve.
- A `TryParse` hibás bemenetnél nem dob kivételt, hanem `false`-t ad – ezért alkalmas ellenőrzésre (az `int.Parse` itt kivétellel leállna).
- Osztásnál a `(double)a / b` kell, különben egész osztás történne (`100 / 8 = 12`).
- **Extra:** a `4*-2` esetén az `IndexOfAny` a `*`-ot találja meg (az áll előbb), a jobb oldal `-2` lesz, amit a `TryParse` elfogad – ez tehát működik. A `4--2` is működik: az első `-` a műveleti jel, a második a `-2` része. Hibás viszont a `4-*2`: ott a jobb oldal `*2`, amit a program helyesen hibának jelez.
