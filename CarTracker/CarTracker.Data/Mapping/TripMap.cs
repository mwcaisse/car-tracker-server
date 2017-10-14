using System;
using System.Collections.Generic;
using System.Text;
using CarTracker.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarTracker.Data.Mapping
{
    public class TripMap : TrackedMap<Trip>
    {
        public override void Configure(EntityTypeBuilder<Trip> builder)
        {
            base.Configure(builder);

            builder.ToTable("TRIP").HasKey(t => t.TripId);

            builder.Property(t => t.TripId)
                .HasColumnName("ID")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.StartDate)
                .HasColumnName("START_DATE")
                .IsRequired();

            builder.Property(t => t.EndDate)
                .HasColumnName("END_DATE");

            builder.Property(t => t.Name)
                .HasColumnName("NAME")
                .HasMaxLength(250);

            builder.Property(t => t.CarId)
                .HasColumnName("CAR_ID")
                .IsRequired();

            builder.Property(t => t.AverageSpeed)
                .HasColumnName("AVERAGE_SPEED");

            builder.Property(t => t.MaximumSpeed)
                .HasColumnName("MAX_SPEED");

            builder.Property(t => t.AverageEngineRpm)
                .HasColumnName("AVERAGE_ENGINE_RPM");

            builder.Property(t => t.MaxEngineRpm)
                .HasColumnName("MAX_ENGINE_RPM");

            builder.Property(t => t.DistanceTraveled)
                .HasColumnName("DISTANCE_TRAVELED");

            builder.Property(t => t.IdleTime)
                .HasColumnName("IDLE_TIME");

            builder.Property(t => t.StartPlaceId)
                .HasColumnName("START_PLACE");

            builder.Property(t => t.DestinationPlaceId)
                .HasColumnName("DESTINATION_PLACE");

            builder.Property(t => t.Status)
                .HasColumnName("STATUS")
                .HasMaxLength(50);

            builder.HasOne(t => t.StartPlace)
                .WithMany(p => p.TripStarts)
                .HasForeignKey(t => t.StartPlaceId);

            builder.HasOne(t => t.DestinationPlace)
                .WithMany(p => p.TripDestinations)
                .HasForeignKey(t => t.DestinationPlaceId);

            builder.HasOne(t => t.Car)
                .WithMany(c => c.Trips);

        }
    }
}
