using System;
using System.Collections.Generic;
using System.Text;
using CarTracker.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarTracker.Data.Mapping
{
    public class TripPossiblePlaceMap : TrackedMap<TripPossiblePlace>
    {
        public override void Configure(EntityTypeBuilder<TripPossiblePlace> builder)
        {
            base.Configure(builder);

            builder.ToTable("TRIP_POSSIBLE_PLACE")
                .HasKey(p => p.TripPossiblePlaceId);

            builder.Property(p => p.TripPossiblePlaceId)
                .HasColumnName("ID")
                .ValueGeneratedOnAdd();

            builder.Property(p => p.TripId)
                .HasColumnName("TRIP_ID");

            builder.Property(p => p.PlaceId)
                .HasColumnName("PLACE_ID");

            builder.Property(p => p.PlaceType)
                .HasColumnName("PLACE_TYPE")
                .IsRequired();

            builder.Property(p => p.Distance)
                .HasColumnName("DISTANCE")
                .IsRequired();

            builder.HasOne(p => p.Trip)
                .WithMany(t => t.TripPossiblePlaces)
                .HasForeignKey(p => p.TripId);

            builder.HasOne(t => t.Place)
                .WithMany(p => p.TripPossiblePlaces)
                .HasForeignKey(t => t.PlaceId);
        }
    }
}
