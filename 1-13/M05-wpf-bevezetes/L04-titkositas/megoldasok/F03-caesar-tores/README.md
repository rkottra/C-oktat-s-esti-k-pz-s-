# Megoldás – 13-M05-L04-F03 · Caesar-törő gyakoriságelemzéssel

- `Tores.cs`: a gyakoriság megszámlálása egy `int[26]` tömbbe, a leggyakoribb betű (maximumkiválasztás) és a két kulcsbecslés. Az egyik a „leggyakoribb = e” ötlet, a másik mind a 26 kulcs pontozása a magyar betűgyakoriság alapján, majd a legjobb kiválasztása.
- `MainWindow.xaml`: háromoszlopos elrendezés (rejtett szöveg, diagram, a 26 kulcs listája), alatta az eredmény és a teljes visszafejtés.
- `MainWindow.xaml.cs`: a konstruktor kódból építi fel a 26 oszlopot (`DockPanel` + `TextBlock` + `Rectangle`). Az elemzés kiszámolja a gyakoriságot és a két becslést, és feltölti a listát. A `DiagramRajzol` az aktuális panelmagassághoz igazítja az oszlopokat; a `SizeChanged` miatt átméretezéskor is.

**A próbaüzenet tanulsága:** a rejtett szövegben az `L` a leggyakoribb. Az „ez az `e`” feltételezés ebből `k = 7`-et ad, ami hibás. A helyes kulcs a `k = 11`, amellyel a visszafejtett szöveg: *„Gratulalunk! A programozasi verseny utolso feladata…”*. Ebben a szövegben a leggyakoribb betű az `a`, nem az `e`, mert a magyarban a kettő gyakorisága nagyon közel van egymáshoz. A pontozás nem egyetlen betűre épít, hanem az összes betű eloszlására, ezért sokkal megbízhatóbb, és rövid szövegeken is eltalálja a kulcsot.

Futtatás: F5 (Visual Studio).
