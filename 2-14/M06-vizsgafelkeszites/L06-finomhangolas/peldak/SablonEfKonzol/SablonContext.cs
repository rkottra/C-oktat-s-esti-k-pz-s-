using Microsoft.EntityFrameworkCore;
using SablonEfKonzol.Models;

namespace SablonEfKonzol;

// SABLON – DbContext váz. Teendő: adatbázisnév, (jelszó), DbSet-ek.
//
// Alternatíva: generálás meglévő adatbázisból (Package Manager Console, a projekt legyen a Default project):
//   Scaffold-DbContext "server=localhost;database=ADATBAZIS;user=root;password=" Pomelo.EntityFrameworkCore.MySql -OutputDir Models -Context SablonContext
//   (újragenerálás: a végére -Force)
public class SablonContext : DbContext
{
    public DbSet<Szulo> Szulok => Set<Szulo>();
    public DbSet<Gyerek> Gyerekek => Set<Gyerek>();

    public const string KapcsolatiSzoveg = "server=localhost;database=estis_sablon;user=root;password=";

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySql(KapcsolatiSzoveg, ServerVersion.AutoDetect(KapcsolatiSzoveg));
}
