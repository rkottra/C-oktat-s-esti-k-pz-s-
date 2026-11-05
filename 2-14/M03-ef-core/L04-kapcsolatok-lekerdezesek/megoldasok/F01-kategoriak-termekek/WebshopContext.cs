using Microsoft.EntityFrameworkCore;
using Webshop.Models;

namespace Webshop;

public class WebshopContext : DbContext
{
    public DbSet<Kategoria> Kategoriak => Set<Kategoria>();
    public DbSet<Termek> Termekek => Set<Termek>();

    private const string KapcsolatiSzoveg = "server=localhost;database=estis_webshop;user=root;password=";

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySql(KapcsolatiSzoveg, ServerVersion.AutoDetect(KapcsolatiSzoveg));
}
