using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Zugsichtungen.Models;

public partial class ZugbeobachtungenContext : DbContext
{
    public ZugbeobachtungenContext()
    {
    }

    public ZugbeobachtungenContext(DbContextOptions<ZugbeobachtungenContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Baureihen> Baureihens { get; set; }

    public virtual DbSet<Fahrzeuge> Fahrzeuges { get; set; }

    public virtual DbSet<Hersteller> Herstellers { get; set; }

    public virtual DbSet<Kontexte> Kontextes { get; set; }

    public virtual DbSet<Modelle> Modelles { get; set; }

    public virtual DbSet<Sichtungen> Sichtungens { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlite("Data Source=zugbeobachtungen.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Baureihen>(entity =>
        {
            entity.ToTable("Baureihen");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Bemerkung).HasColumnName("bemerkung");
            entity.Property(e => e.ModellId).HasColumnName("modell_id");
            entity.Property(e => e.Nummer).HasColumnName("nummer");

            entity.HasOne(d => d.Modell).WithMany(p => p.Baureihens).HasForeignKey(d => d.ModellId);
        });

        modelBuilder.Entity<Fahrzeuge>(entity =>
        {
            entity.ToTable("Fahrzeuge");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BaureiheId).HasColumnName("baureihe_id");
            entity.Property(e => e.Bemerkung).HasColumnName("bemerkung");
            entity.Property(e => e.Nummer).HasColumnName("nummer");

            entity.HasOne(d => d.Baureihe).WithMany(p => p.Fahrzeuges).HasForeignKey(d => d.BaureiheId);
        });

        modelBuilder.Entity<Hersteller>(entity =>
        {
            entity.ToTable("hersteller");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
        });

        modelBuilder.Entity<Kontexte>(entity =>
        {
            entity.ToTable("Kontexte");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
        });

        modelBuilder.Entity<Modelle>(entity =>
        {
            entity.ToTable("Modelle");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.HerstellerId).HasColumnName("hersteller_id");
            entity.Property(e => e.Modell).HasColumnName("modell");

            entity.HasOne(d => d.Hersteller).WithMany(p => p.Modelles)
                .HasForeignKey(d => d.HerstellerId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Sichtungen>(entity =>
        {
            entity.ToTable("Sichtungen");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Datum).HasColumnName("datum");
            entity.Property(e => e.FahrzeugId).HasColumnName("fahrzeug_id");
            entity.Property(e => e.KontextId).HasColumnName("kontext_id");
            entity.Property(e => e.Ort).HasColumnName("ort");

            entity.HasOne(d => d.Fahrzeug).WithMany(p => p.Sichtungens).HasForeignKey(d => d.FahrzeugId);

            entity.HasOne(d => d.Kontext).WithMany(p => p.Sichtungens).HasForeignKey(d => d.KontextId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
