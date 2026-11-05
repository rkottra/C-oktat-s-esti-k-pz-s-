using KonyvtarMigracio.Models;
using Microsoft.EntityFrameworkCore;

namespace KonyvtarMigracio;

// A DbContext képviseli az adatbázis-kapcsolatot.
// Minden DbSet<T> tulajdonságból egy tábla lesz.
public class KonyvtarContext : DbContext
{
    public DbSet<Konyv> Konyvek => Set<Konyv>();

    // XAMPP alapbeállítás: root felhasználó, üres jelszó.
    // Az adatbázist NEM kell előre létrehozni – az Update-Database megcsinálja.
    private const string KapcsolatiSzoveg = "server=localhost;database=estis_konyvtar;user=root;password=";

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql(KapcsolatiSzoveg, ServerVersion.AutoDetect(KapcsolatiSzoveg));
    }
}
