using Microsoft.EntityFrameworkCore;
using TelepulesekSzures.Models;

namespace TelepulesekSzures;

public class TelepulesekContext : DbContext
{
    public DbSet<Megye> Megyek => Set<Megye>();
    public DbSet<Telepules> Telepulesek => Set<Telepules>();

    private const string KapcsolatiSzoveg = "server=localhost;database=estis_telepulesek;user=root;password=";

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySql(KapcsolatiSzoveg, ServerVersion.AutoDetect(KapcsolatiSzoveg));
}
