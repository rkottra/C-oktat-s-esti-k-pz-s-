# Megoldás – 13-M03-L03-F03 · Okosotthon

- `Interfeszek.cs`: a három képesség (`IKapcsolhato`, `ISzabalyozhato`, `IFogyaszto`).
- `Eszkoz.cs`: absztrakt ős statikus `Darab` számlálóval, absztrakt `Allapot()`-tal és `ToString()`-gel.
- `Eszkozok.cs`: a négy eszköz, mindegyik egy őssel és egy–három interfésszel. A szint a `Math.Clamp`-pel szorul a határok közé.
- `Vezerlo.cs`: statikus segédosztály. Minden metódusa interfészre vizsgál (`is IKapcsolhato k`), a konkrét osztályokat (`Lampa`, `Konnektor` …) nem ismeri.
- `Program.cs`: regisztrálja az eszközöket, és lefuttatja a három forgatókönyvet.

Megjegyzések:

- **Miért interfész és nem ős?** A lámpa és a konnektor egyaránt kapcsolható, a termosztát viszont nem. A lámpa és a termosztát szabályozható, a konnektor nem. Ezeket a keresztben álló képességeket egyetlen öröklési lánc nem tudná leírni, hiszen egy osztálynak csak egy őse lehet. Interfészből viszont akárhányat megvalósíthat.
- **Miért van mégis absztrakt ős?** A `Nev`, a `Helyiseg`, a számláló és a `ToString()` minden eszköznél ugyanaz. Ez közös *kód*, nem csak közös *képesség*, és ilyenkor az absztrakt osztály a jó választás.
- A `Vezerlo.Szintre<T>` egy **generikus** metódus: a `T` helyére a hívásnál írt típus kerül (`Szintre<Redony>(…)`), a `where T : ISzabalyozhato` pedig kiköti, hogy csak szabályozható típus lehet. Ez túlmutat a tananyagon. Helyette az is jó megoldás, ha a főprogram egy `foreach`-ben `if (e is Redony r) r.Szint = 100;` vizsgálattal állítja be.
- A `Find` a lista első olyan elemét adja vissza, amelyre a lambda igaz; ez a kiválasztás tételének beépített változata.
- Bővítési ötlet: egy `Mosogep : Eszkoz, IKapcsolhato, IFogyaszto` osztály a `Vezerlo` módosítása nélkül azonnal részt vesz a „Mindent ki” forgatókönyvben és a fogyasztásszámításban.

Futtatás: F5 (Visual Studio) vagy `dotnet run`.
