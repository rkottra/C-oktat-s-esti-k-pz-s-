using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using OregfakLekerdezesek.Models;

namespace OregfakLekerdezesek.Data;

public partial class OregfakContext : DbContext
{
    public OregfakContext()
    {
    }

    public OregfakContext(DbContextOptions<OregfakContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Fa> Fa { get; set; }

    public virtual DbSet<Megye> Megye { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;database=estis_oregfak;user=root;password=", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.32-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_hungarian_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Fa>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("fa");

            entity.HasIndex(e => e.MegyeId, "fk_fa_megye");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Faj)
                .HasMaxLength(50)
                .HasColumnName("faj");
            entity.Property(e => e.Kormeret)
                .HasComment("törzskerület cm-ben")
                .HasColumnType("int(11)")
                .HasColumnName("kormeret");
            entity.Property(e => e.MegyeId)
                .HasColumnType("int(11)")
                .HasColumnName("megye_id");
            entity.Property(e => e.MeresEve)
                .HasColumnType("int(11)")
                .HasColumnName("meres_eve");
            entity.Property(e => e.Telepules)
                .HasMaxLength(50)
                .HasColumnName("telepules");

            entity.HasOne(d => d.Megye).WithMany(p => p.Fa)
                .HasForeignKey(d => d.MegyeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_fa_megye");
        });

        modelBuilder.Entity<Megye>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("megye");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Nev)
                .HasMaxLength(30)
                .HasColumnName("nev");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
