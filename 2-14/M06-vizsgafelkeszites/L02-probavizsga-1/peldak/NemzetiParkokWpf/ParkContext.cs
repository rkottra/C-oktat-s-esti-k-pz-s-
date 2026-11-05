using Microsoft.EntityFrameworkCore;
using NemzetiParkokWpf.Models;

namespace NemzetiParkokWpf;

public class ParkContext : DbContext
{
    public DbSet<NemzetiPark> Parkok => Set<NemzetiPark>();

    private const string KapcsolatiSzoveg = "server=localhost;database=estis_nemzetiparkok;user=root;password=";

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySql(KapcsolatiSzoveg, ServerVersion.AutoDetect(KapcsolatiSzoveg));
}
