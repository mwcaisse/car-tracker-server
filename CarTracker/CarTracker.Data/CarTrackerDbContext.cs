using System;
using System.Collections.Generic;
using System.Text;
using CarTracker.Common.Entities;
using CarTracker.Common.Entities.Auth;
using CarTracker.Common.Entities.Logging;
using CarTracker.Common.Entities.Places;
using CarTracker.Data.Mapping;
using CarTracker.Data.Mapping.Auth;
using CarTracker.Data.Mapping.Logging;
using CarTracker.Data.Mapping.Places;
using Microsoft.EntityFrameworkCore;

namespace CarTracker.Data
{
    public class CarTrackerDbContext : DbContext
    {

        public DbSet<User> Users { get; set; }

        public DbSet<UserAuthenticationToken> UserAuthenticationTokens { get; set; }

        public DbSet<UserRegistrationKey> UserRegistrationKeys { get; set; }

        public DbSet<UserRegistrationKeyUse> UserRegistrationKeyUses { get; set; }

        public DbSet<Car> Cars { get; set; }

        public DbSet<CarMaintenance> CarMaintenances { get; set; }

        public DbSet<CarSupportedCommands> CarSupportedCommands { get; set; }

        public DbSet<Place> Places { get; set; }

        public DbSet<UserPlace> UserPlaces { get; set; }

        public DbSet<GooglePlace> GooglePlaces { get; set; }

        public DbSet<PlaceVisit> PlaceVisits { get; set; }

        public DbSet<ReaderLog> ReaderLogs { get; set; }

        public DbSet<Reading> Readings { get; set; }

        public DbSet<Trip> Trips { get; set; }

        public DbSet<TripPossiblePlace> TripPossiblePlaces { get; set; }

        public DbSet<RequestLog> RequestLogs { get; set; }
        
        public DbSet<ServerLog> ServerLogs { get; set; }

        public CarTrackerDbContext(DbContextOptions<CarTrackerDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new UserAuthenticationTokenMap());
            modelBuilder.ApplyConfiguration(new UserRegistrationKeyMap());
            modelBuilder.ApplyConfiguration(new UserRegistrationKeyUseMap());

            modelBuilder.ApplyConfiguration(new CarMap());
            modelBuilder.ApplyConfiguration(new CarSupportedCommandsMap());
            modelBuilder.ApplyConfiguration(new CarMaintenanceMap());
            
            modelBuilder.ApplyConfiguration(new ReaderLogMap());
            modelBuilder.ApplyConfiguration(new ReadingMap());
            modelBuilder.ApplyConfiguration(new TripMap());
            modelBuilder.ApplyConfiguration(new TripPossiblePlaceMap());
            
            modelBuilder.ApplyConfiguration(new PlaceMap());
            modelBuilder.ApplyConfiguration(new UserPlaceMap());
            modelBuilder.ApplyConfiguration(new GooglePlaceMap());
            modelBuilder.ApplyConfiguration(new PlaceVisitMap());

            modelBuilder.ApplyConfiguration(new RequestLogMap());
            modelBuilder.ApplyConfiguration(new ServerLogMap());
        }

    }
}
