namespace KonyvtarMigracio.Models;

// Entitásosztály: minden példánya egy sor lesz a Konyvek táblában.
public class Konyv
{
    // Az "Id" nevű int tulajdonságból az EF Core automatikusan
    // elsődleges kulcsot (PRIMARY KEY, AUTO_INCREMENT) készít.
    public int Id { get; set; }

    public string Cim { get; set; } = "";
    public string Szerzo { get; set; } = "";
    public int KiadasEve { get; set; }
    public int Ar { get; set; }

    // A ? miatt ez az oszlop NULL értéket is felvehet.
    public string? Mufaj { get; set; }

    // ÚJ tulajdonság – utána: Add-Migration KolcsonozhetoMezo, Update-Database
    public bool Kolcsonozheto { get; set; } = true;

    public override string ToString() => $"{Id,3}. {Szerzo}: {Cim} ({KiadasEve}) – {Ar} Ft";
}
