using KonyvtarKapcsolatok.Models;
using Microsoft.EntityFrameworkCore;

namespace KonyvtarKapcsolatok;

public class KonyvtarContext : DbContext
{
    public DbSet<Szerzo> Szerzok => Set<Szerzo>();
    public DbSet<Konyv> Konyvek => Set<Konyv>();

    // Új adatbázis, hogy ne ütközzön az L01 estis_konyvtar adatbázisával.
    private const string KapcsolatiSzoveg = "server=localhost;database=estis_konyvtar_kapcsolat;user=root;password=";

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql(KapcsolatiSzoveg, ServerVersion.AutoDetect(KapcsolatiSzoveg));
    }

    // A kapcsolatot a konvenciók már felismerik; itt csak "kimondjuk" (Fluent API),
    // és megadjuk, hogy szerzőt NE lehessen törölni, amíg van könyve.
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Konyv>()
            .HasOne(k => k.Szerzo)
            .WithMany(s => s.Konyvek)
            .HasForeignKey(k => k.SzerzoId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
