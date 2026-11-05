using RaktarImport.Models;
using Microsoft.EntityFrameworkCore;

namespace RaktarImport;

public class RaktarContext : DbContext
{
    public DbSet<Termek> Termekek => Set<Termek>();
    public DbSet<Kategoria> Kategoriak => Set<Kategoria>();

    private const string KapcsolatiSzoveg = "server=localhost;database=estis_raktar;user=root;password=";

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql(KapcsolatiSzoveg, ServerVersion.AutoDetect(KapcsolatiSzoveg));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Kategoria>().HasData(
            new Kategoria { Id = 1, Nev = "Kéziszerszám" },
            new Kategoria { Id = 2, Nev = "Elektromos gép" },
            new Kategoria { Id = 3, Nev = "Kertészet" },
            new Kategoria { Id = 4, Nev = "Festék" });

        modelBuilder.Entity<Termek>().HasData(
            new Termek { Id = 1, Cikkszam = "KS-001", Nev = "Kalapács 500 g", Ar = 3290, Darab = 25, KategoriaId = 1 },
            new Termek { Id = 2, Cikkszam = "KS-002", Nev = "Csavarhúzókészlet 6 db", Ar = 4990, Darab = 12, KategoriaId = 1 },
            new Termek { Id = 3, Cikkszam = "KS-003", Nev = "Kombinált fogó", Ar = 2490, Darab = 3, KategoriaId = 1 },
            new Termek { Id = 4, Cikkszam = "EG-001", Nev = "Akkus fúrócsavarozó 18 V", Ar = 32990, Darab = 4, KategoriaId = 2 },
            new Termek { Id = 5, Cikkszam = "EG-002", Nev = "Sarokcsiszoló 125 mm", Ar = 18990, Darab = 7, KategoriaId = 2 },
            new Termek { Id = 6, Cikkszam = "EG-003", Nev = "Dekopírfűrész", Ar = 21990, Darab = 0, KategoriaId = 2 },
            new Termek { Id = 7, Cikkszam = "KT-001", Nev = "Metszőolló", Ar = 3990, Darab = 18, KategoriaId = 3 },
            new Termek { Id = 8, Cikkszam = "KT-002", Nev = "Locsolótömlő 20 m", Ar = 6490, Darab = 9, KategoriaId = 3 },
            new Termek { Id = 9, Cikkszam = "KT-003", Nev = "Ásó", Ar = 5490, Darab = 2, KategoriaId = 3 },
            new Termek { Id = 10, Cikkszam = "FS-001", Nev = "Beltéri falfesték 5 l", Ar = 8990, Darab = 14, KategoriaId = 4 },
            new Termek { Id = 11, Cikkszam = "FS-002", Nev = "Ecsetkészlet", Ar = 1990, Darab = 30, KategoriaId = 4 },
            new Termek { Id = 12, Cikkszam = "FS-003", Nev = "Festőhenger", Ar = 1490, Darab = 0, KategoriaId = 4 });
    }
}
