using Microsoft.EntityFrameworkCore;
using PilotakSzures.Models;

namespace PilotakSzures;

public class PilotakContext : DbContext
{
    public DbSet<Pilota> Pilotak => Set<Pilota>();

    private const string KapcsolatiSzoveg = "server=localhost;database=estis_f1pilots;user=root;password=";

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySql(KapcsolatiSzoveg, ServerVersion.AutoDetect(KapcsolatiSzoveg));
}
