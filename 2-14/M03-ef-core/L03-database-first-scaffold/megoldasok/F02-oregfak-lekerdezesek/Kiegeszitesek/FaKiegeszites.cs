// Saját kiegészítés a generált Fa osztályhoz – az újragenerálás nem írja felül.
namespace OregfakLekerdezesek.Models;

public partial class Fa
{
    // Számított tulajdonság (nincs settere → nem lesz belőle oszlop).
    // Csak a memóriában használható, LINQ-to-SQL lekérdezésben nem!
    public string Kategoria =>
        Kormeret >= 700 ? "óriás" :
        Kormeret >= 500 ? "vastag" : "átlagos";
}
