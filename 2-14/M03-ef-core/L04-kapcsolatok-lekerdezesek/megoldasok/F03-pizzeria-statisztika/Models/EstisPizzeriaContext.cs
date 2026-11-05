using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PizzeriaStatisztika.Models;

public partial class EstisPizzeriaContext : DbContext
{
    public EstisPizzeriaContext()
    {
    }

    public EstisPizzeriaContext(DbContextOptions<EstisPizzeriaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Futar> Futar { get; set; }

    public virtual DbSet<Pizza> Pizza { get; set; }

    public virtual DbSet<Rendeles> Rendeles { get; set; }

    public virtual DbSet<Tetel> Tetel { get; set; }

    public virtual DbSet<Vevo> Vevo { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;database=estis_pizzeria;user=root;password=", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.32-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_hungarian_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Futar>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("futar");

            entity.HasIndex(e => e.Telefon, "uq_futar_telefon").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Nev)
                .HasMaxLength(40)
                .HasColumnName("nev");
            entity.Property(e => e.Telefon)
                .HasMaxLength(15)
                .HasColumnName("telefon");
        });

        modelBuilder.Entity<Pizza>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("pizza");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Ar)
                .HasColumnType("int(11)")
                .HasColumnName("ar");
            entity.Property(e => e.Nev)
                .HasMaxLength(30)
                .HasColumnName("nev");
        });

        modelBuilder.Entity<Rendeles>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("rendeles");

            entity.HasIndex(e => e.FutarId, "fk_rendeles_futar");

            entity.HasIndex(e => e.VevoId, "fk_rendeles_vevo");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.FutarId)
                .HasColumnType("int(11)")
                .HasColumnName("futar_id");
            entity.Property(e => e.Idopont)
                .HasColumnType("datetime")
                .HasColumnName("idopont");
            entity.Property(e => e.VevoId)
                .HasColumnType("int(11)")
                .HasColumnName("vevo_id");

            entity.HasOne(d => d.Futar).WithMany(p => p.Rendeles)
                .HasForeignKey(d => d.FutarId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_rendeles_futar");

            entity.HasOne(d => d.Vevo).WithMany(p => p.Rendeles)
                .HasForeignKey(d => d.VevoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_rendeles_vevo");
        });

        modelBuilder.Entity<Tetel>(entity =>
        {
            entity.HasKey(e => new { e.RendelesId, e.PizzaId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("tetel");

            entity.HasIndex(e => e.PizzaId, "fk_tetel_pizza");

            entity.Property(e => e.RendelesId)
                .HasColumnType("int(11)")
                .HasColumnName("rendeles_id");
            entity.Property(e => e.PizzaId)
                .HasColumnType("int(11)")
                .HasColumnName("pizza_id");
            entity.Property(e => e.Db)
                .HasDefaultValueSql("'1'")
                .HasColumnType("int(11)")
                .HasColumnName("db");

            entity.HasOne(d => d.Pizza).WithMany(p => p.Tetel)
                .HasForeignKey(d => d.PizzaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_tetel_pizza");

            entity.HasOne(d => d.Rendeles).WithMany(p => p.Tetel)
                .HasForeignKey(d => d.RendelesId)
                .HasConstraintName("fk_tetel_rendeles");
        });

        modelBuilder.Entity<Vevo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("vevo");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Cim)
                .HasMaxLength(60)
                .HasColumnName("cim");
            entity.Property(e => e.Nev)
                .HasMaxLength(40)
                .HasColumnName("nev");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
