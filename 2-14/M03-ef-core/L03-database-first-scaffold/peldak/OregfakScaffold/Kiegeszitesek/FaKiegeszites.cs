// Ez a fájl NEM generált: a saját kiegészítéseink ide kerülnek.
// A generált Fa osztály "partial", ezért egy másik fájlban folytathatjuk.
// Az újragenerálás (-Force) csak a Models mappa fájljait írja felül, ezt nem.

// A névtérnek egyeznie kell a generált osztályéval!
namespace OregfakScaffold.Models;

public partial class Fa
{
    // Csak getterrel rendelkező tulajdonság: az EF Core nem képez belőle oszlopot.
    public double KormeretMeterben => Kormeret / 100.0;

    public override string ToString() =>
        $"{Faj,-22} {Kormeret,5} cm  {Telepules} ({MeresEve?.ToString() ?? "?"})";
}
