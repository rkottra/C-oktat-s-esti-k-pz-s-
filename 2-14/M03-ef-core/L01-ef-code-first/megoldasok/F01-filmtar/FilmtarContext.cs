using Filmtar.Models;
using Microsoft.EntityFrameworkCore;

namespace Filmtar;

public class FilmtarContext : DbContext
{
    public DbSet<Film> Filmek => Set<Film>();

    private const string KapcsolatiSzoveg = "server=localhost;database=estis_filmtar;user=root;password=";

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySql(KapcsolatiSzoveg, ServerVersion.AutoDetect(KapcsolatiSzoveg));
}
