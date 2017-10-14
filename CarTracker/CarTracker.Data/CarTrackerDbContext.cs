using System;
using System.Collections.Generic;
using System.Text;
using CarTracker.Common.Entities;
using CarTracker.Data.Mapping;
using CarTracker.Data.Mapping.Auth;
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

        public DbSet<CarSupportedCommands> CarSupportedCommands { get; set; }

        public DbSet<Place> Places { get; set; }

        public DbSet<ReaderLog> ReaderLogs { get; set; }

        public DbSet<Reading> Readings { get; set; }

        public DbSet<Trip> Trips { get; set; }

        public DbSet<TripPossiblePlace> TripPossiblePlaces { get; set; }

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
            modelBuilder.ApplyConfiguration(new PlaceMap());
            modelBuilder.ApplyConfiguration(new ReaderLogMap());
            modelBuilder.ApplyConfiguration(new ReadingMap());
            modelBuilder.ApplyConfiguration(new TripMap());
            modelBuilder.ApplyConfiguration(new TripPossiblePlaceMap());

        }

    }
}
