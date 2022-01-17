using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ABS_SystemManager.DbModels
{
    public partial class ABS_databaseContext : DbContext
    {
        public ABS_databaseContext()
        {
        }

        public ABS_databaseContext(DbContextOptions<ABS_databaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Airline> Airline { get; set; }
        public virtual DbSet<Airport> Airport { get; set; }
        public virtual DbSet<Flight> Flight { get; set; }
        public virtual DbSet<FlightSection> FlightSection { get; set; }
        public virtual DbSet<Seat> Seat { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.;Database=ABS_database;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Airline>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("CHK_Unique_AirlineName")
                    .IsUnique();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Airport>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("CHK_Unique_AirportName")
                    .IsUnique();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Flight>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Date).HasColumnType("date");

                entity.HasOne(d => d.Airline)
                    .WithMany(p => p.Flight)
                    .HasForeignKey(d => d.AirlineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Airline_Name");

                entity.HasOne(d => d.Destination)
                    .WithMany(p => p.FlightDestination)
                    .HasForeignKey(d => d.DestinationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Airport_Destination");

                entity.HasOne(d => d.Origin)
                    .WithMany(p => p.FlightOrigin)
                    .HasForeignKey(d => d.OriginId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Airport_Origin");
            });

            modelBuilder.Entity<FlightSection>(entity =>
            {
                entity.Property(e => e.FlightId)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.HasOne(d => d.Flight)
                    .WithMany(p => p.FlightSection)
                    .HasForeignKey(d => d.FlightId)
                    .HasConstraintName("FK_Flight_Id");
            });

            modelBuilder.Entity<Seat>(entity =>
            {
                entity.Property(e => e.Column)
                    .IsRequired()
                    .HasColumnName("COLUMN")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Row).HasColumnName("ROW");

                entity.HasOne(d => d.FlightSection)
                    .WithMany(p => p.Seat)
                    .HasForeignKey(d => d.FlightSectionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FlightSection_Id");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
