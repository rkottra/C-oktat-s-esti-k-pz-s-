using KonyvesboltMegjelenites.Models;
using Microsoft.EntityFrameworkCore;

namespace KonyvesboltMegjelenites;

public class KonyvesboltContext : DbContext
{
    public DbSet<Konyv> Konyvek => Set<Konyv>();
    public DbSet<Kiado> Kiadok => Set<Kiado>();

    // A modul mind a négy példája ugyanezt az adatbázist használja.
    private const string KapcsolatiSzoveg = "server=localhost;database=estis_konyvesbolt;user=root;password=";

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql(KapcsolatiSzoveg, ServerVersion.AutoDetect(KapcsolatiSzoveg));
    }

    // Kezdő adatok: az adatbázis létrehozásakor (EnsureCreated / Update-Database) kerülnek be.
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Kiado>().HasData(
            new Kiado { Id = 1, Nev = "Móra" },
            new Kiado { Id = 2, Nev = "Magvető" },
            new Kiado { Id = 3, Nev = "Európa" },
            new Kiado { Id = 4, Nev = "Animus" });

        modelBuilder.Entity<Konyv>().HasData(
            new Konyv { Id = 1, Cim = "Egri csillagok", Szerzo = "Gárdonyi Géza", KiadasEve = 1901, Ar = 3490, Keszlet = 12, KiadoId = 1 },
            new Konyv { Id = 2, Cim = "A Pál utcai fiúk", Szerzo = "Molnár Ferenc", KiadasEve = 1907, Ar = 2990, Keszlet = 8, KiadoId = 1 },
            new Konyv { Id = 3, Cim = "Tüskevár", Szerzo = "Fekete István", KiadasEve = 1957, Ar = 3190, Keszlet = 0, KiadoId = 1 },
            new Konyv { Id = 4, Cim = "Sorstalanság", Szerzo = "Kertész Imre", KiadasEve = 1975, Ar = 3999, Keszlet = 5, KiadoId = 2 },
            new Konyv { Id = 5, Cim = "Az ajtó", Szerzo = "Szabó Magda", KiadasEve = 1987, Ar = 3699, Keszlet = 3, KiadoId = 3 },
            new Konyv { Id = 6, Cim = "Abigél", Szerzo = "Szabó Magda", KiadasEve = 1970, Ar = 3899, Keszlet = 9, KiadoId = 3 },
            new Konyv { Id = 7, Cim = "Utas és holdvilág", Szerzo = "Szerb Antal", KiadasEve = 1937, Ar = 3299, Keszlet = 0, KiadoId = 2 },
            new Konyv { Id = 8, Cim = "A kőszívű ember fiai", Szerzo = "Jókai Mór", KiadasEve = 1869, Ar = 3590, Keszlet = 6, KiadoId = 1 },
            new Konyv { Id = 9, Cim = "1984", Szerzo = "George Orwell", KiadasEve = 1949, Ar = 3999, Keszlet = 11, KiadoId = 3 },
            new Konyv { Id = 10, Cim = "Légy jó mindhalálig", Szerzo = "Móricz Zsigmond", KiadasEve = 1920, Ar = 2890, Keszlet = 4, KiadoId = 1 },
            new Konyv { Id = 11, Cim = "Harry Potter és a bölcsek köve", Szerzo = "J. K. Rowling", KiadasEve = 1997, Ar = 4499, Keszlet = 15, KiadoId = 4 },
            new Konyv { Id = 12, Cim = "A Gyűrűk Ura", Szerzo = "J. R. R. Tolkien", KiadasEve = 1954, Ar = 9999, Keszlet = 2, KiadoId = 3 });
    }
}
