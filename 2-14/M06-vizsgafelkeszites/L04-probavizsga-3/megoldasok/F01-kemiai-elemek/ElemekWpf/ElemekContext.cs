using ElemekWpf.Models;
using Microsoft.EntityFrameworkCore;

namespace ElemekWpf;

public class ElemekContext : DbContext
{
    public DbSet<Elem> Elemek => Set<Elem>();
    public DbSet<Kategoria> Kategoriak => Set<Kategoria>();

    private const string KapcsolatiSzoveg = "server=localhost;database=estis_elemek;user=root;password=";

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySql(KapcsolatiSzoveg, ServerVersion.AutoDetect(KapcsolatiSzoveg));
}
