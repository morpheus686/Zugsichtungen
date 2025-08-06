using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Zugsichtungen.Infrastructure.SQLServer.Models;

public partial class TrainspottingContext : DbContext
{
    public TrainspottingContext(DbContextOptions<TrainspottingContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Context> Contexts { get; set; }

    public virtual DbSet<Manufacturer> Manufacturers { get; set; }

    public virtual DbSet<Model> Models { get; set; }

    public virtual DbSet<Series> Series { get; set; }

    public virtual DbSet<Sighting> Sightings { get; set; }

    public virtual DbSet<SightingList> SightingLists { get; set; }

    public virtual DbSet<Vehicle> Vehicles { get; set; }

    public virtual DbSet<Vehiclelist> Vehiclelists { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Context>(entity =>
        {
            entity.ToTable("Context");

            entity.Property(e => e.Description).HasMaxLength(100);
        });

        modelBuilder.Entity<Manufacturer>(entity =>
        {
            entity.ToTable("Manufacturer");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Model>(entity =>
        {
            entity.Property(e => e.ManufactorerId).HasColumnName("Manufactorer_Id");
            entity.Property(e => e.Model1)
                .HasMaxLength(100)
                .HasColumnName("Model");

            entity.HasOne(d => d.Manufactorer).WithMany(p => p.Models)
                .HasForeignKey(d => d.ManufactorerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Models_Manufacturer1");
        });

        modelBuilder.Entity<Series>(entity =>
        {
            entity.Property(e => e.ModelId).HasColumnName("Model_Id");
            entity.Property(e => e.Number).HasMaxLength(10);

            entity.HasOne(d => d.Model).WithMany(p => p.Series)
                .HasForeignKey(d => d.ModelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Series_Models");
        });

        modelBuilder.Entity<Sighting>(entity =>
        {
            entity.ToTable("Sighting");

            entity.Property(e => e.ContextId).HasColumnName("Context_Id");
            entity.Property(e => e.Location).HasMaxLength(100);
            entity.Property(e => e.VehicleId).HasColumnName("Vehicle_Id");

            entity.HasOne(d => d.Context).WithMany(p => p.Sightings)
                .HasForeignKey(d => d.ContextId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sighting_Context");

            entity.HasOne(d => d.Vehicle).WithMany(p => p.Sightings)
                .HasForeignKey(d => d.VehicleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sighting_Vehicle");
        });

        modelBuilder.Entity<SightingList>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("SightingList");

            entity.Property(e => e.Description).HasMaxLength(100);
            entity.Property(e => e.Location).HasMaxLength(100);
            entity.Property(e => e.VehicleNumber).HasMaxLength(21);
        });

        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.Property(e => e.Number).HasMaxLength(10);
            entity.Property(e => e.SeriesId).HasColumnName("Series_Id");

            entity.HasOne(d => d.Series).WithMany(p => p.Vehicles)
                .HasForeignKey(d => d.SeriesId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Vehicles_Series");
        });

        modelBuilder.Entity<Vehiclelist>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Vehiclelist");

            entity.Property(e => e.SeriesId).HasColumnName("Series_Id");
            entity.Property(e => e.VehicleDesignation).HasMaxLength(21);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
