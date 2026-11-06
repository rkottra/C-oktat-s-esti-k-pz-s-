# Megoldás – 13-M05-L01-F03 · Láncművelet

Nyisd meg a `SzamologepLanc.csproj`-t, és indítsd el (F5). A XAML megegyezik a bemutató példáéval (az `=` gomb itt mindig engedélyezett).

A `Muvelet_Click` három esete:

| Helyzet | Teendő |
|---|---|
| nincs függő művelet | a kijelzett szám lesz a részeredmény |
| van függő művelet, és új számot írtak be | kiszámolja a részeredményt, kiírja a kijelzőre |
| van függő művelet, de nem jött új szám | csak lecseréli a műveleti jelet |

Az `=` kétféleképpen működik: ha van függő művelet, lezárja a láncot, és megjegyzi az utolsó műveletet és operandust; ha nincs, ezt a megjegyzett műveletet ismétli meg.
