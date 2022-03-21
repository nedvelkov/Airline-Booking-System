using Microsoft.EntityFrameworkCore;
using ABS_SystemManager.Data.DbModels;
using ABS_SystemManager.Data.UserDefineModels;
using ABS_Models;


namespace ABS_SystemManager.Data
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

        public virtual DbSet<AirportName> GetAirportNames { get; set; }

        public virtual DbSet<AirlineName> GetAirlineNames { get; set; }

        public virtual DbSet<AirlineSystemDisplay> GetAirlineWithFlightsCount { get; set; }

        public virtual DbSet<IdColumn> GetIds { get; set; }

        public virtual DbSet<AirportExist> AirportExists { get; set; }

        public virtual DbSet<FlightsModel> GetAvailableFlights { get; set; }

        public virtual DbSet<FlightsModel> GetFlightsByAirlineName { get; set; }

        public virtual DbSet<SeatNumber> GetSeatNumbers { get; set; }

        public virtual DbSet<AirlineTableView> GetAirlineTableView { get; set; }

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

            modelBuilder.Entity<AirportName>(entity => {
                entity.HasNoKey();
                entity.ToView("GetAirportNames");
                });

            modelBuilder.Entity<AirlineName>(entity => {
                entity.HasNoKey();
                entity.ToView("GetAirlineNames");
            });

            modelBuilder.Entity<AirlineSystemDisplay>(entity => {
                entity.HasNoKey();
                entity.ToView("GetAirlinesWithFlightsCount");
            });

            modelBuilder.Entity<IdColumn>(entity => entity.HasNoKey());

            modelBuilder.Entity<FlightsModel>(entity => entity.HasNoKey());

            modelBuilder.Entity<AirportExist>(entity => entity.HasNoKey());

            modelBuilder.Entity<SeatNumber>(entity => entity.HasNoKey());

            modelBuilder.Entity<AirlineTableView>(entity => entity.HasNoKey());

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
