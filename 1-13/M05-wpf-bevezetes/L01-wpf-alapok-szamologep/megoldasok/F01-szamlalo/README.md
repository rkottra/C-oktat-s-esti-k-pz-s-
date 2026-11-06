# Megoldás – 13-M05-L01-F01 · Számláló ablak

Nyisd meg a `Szamlalo.csproj`-t Visual Studióban, és indítsd el (F5).

- Az érték az `ertek` mezőben van; a felület csak megjeleníti.
- Minden gomb eseménykezelője ugyanúgy végződik: `Frissit()`. Ez írja ki az értéket, és ez dönti el, melyik gomb engedélyezett – így nem fordulhat elő, hogy egy gomb „elfelejt” letiltódni.
- A XAML-ben a Csökkent és a Nulláz gomb már indításkor letiltott (`IsEnabled="False"`), mert az érték 0.
