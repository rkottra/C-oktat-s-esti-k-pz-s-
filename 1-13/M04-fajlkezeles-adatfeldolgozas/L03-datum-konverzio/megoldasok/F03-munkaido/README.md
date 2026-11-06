# Megoldás – 13-M04-L03-F03 · Munkaidő-nyilvántartás

- `Jelenlet.cs`: a dátum `DateTime`, az érkezés és a távozás `TimeSpan` (a nap kezdete óta eltelt idő). A cég szabályai `static readonly TimeSpan` állandók. Ezek nem lehetnek `const`-ok, mert a `TimeSpan` értéke futáskor jön létre. Minden további adat (bent töltött idő, ledolgozott idő, túlóra, késés, hétvége) számított tulajdonság. Az `OraPerc` statikus segédmetódus 24 óránál hosszabb időtartamot is helyesen formáz.
- `Program.cs`: különböző dolgozók kigyűjtése, dolgozónkénti összegzés `TimeSpan`-okkal, minimum- és maximumkiválasztás időpontokon, kiválogatás (hétvége), majd egy összetett hiányzás-vizsgálat. Ez utóbbiban a munkanapok listáján minden dolgozóra egy **eldöntés** fut. Végül fájlba írás tizedesponttal.

Megjegyzések:

- A `TimeSpan`-ok ugyanúgy összeadhatók és összehasonlíthatók, mint a számok, a kezdőérték `TimeSpan.Zero`.
- A `{t:hh\\:mm}` formátum csak a napon belüli órát írja ki: 156 óra esetén `12:11`-et adna (156 = 6 · 24 + 12). Ezért kell a `(int)t.TotalHours` alapú `OraPerc`.
- Az interpolált szövegben a `TimeSpan` formátumában a kettőspontot kétszer kell „escape”-elni: `{j.Be:hh\\:mm}`.
- A jelentésbe a `TotalHours` tizedes óraként kerül (156:11 → 156.18), `InvariantCulture`-rel, hogy például egy táblázatkezelő vagy egy másik program biztosan számként olvassa be.
- Az adatokban április 6. (húsvéthétfő) nem munkanap, ezért senkinek nincs rá bejegyzése, és a 7. pont szabálya szerint nem is számít munkanapnak.

Futtatás: F5 (Visual Studio).
